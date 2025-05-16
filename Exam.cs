using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Exam_Questioner
{
    public partial class Exam : Form
    {
        // 1. שדות פרטיים
        private readonly List<Question> _questions;            // כל השאלות במבחן
        private readonly Dictionary<int, string> _userAnswers; // תשובות המשתמש (מפתח: אינדקס שאלה)
        private int _currentIndex;                             // אינדקס השאלה הנוכחית
        private readonly string _examId;                       // מזהה המבחן (שם הגיליון)
        private readonly string _category;                     // הקטגוריה של המבחן (עמודה B ב-ExamID)
        private readonly int _originalRichHeight;              // גובה התיבה הפתוחה המקורי

        // 2. בנאי – אתחול והגדרות ראשוניות
        public Exam(string examId)
        {
            InitializeComponent();

            // 2.1 נעילה של שני TextBox להצגה בלבד
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;

            // 2.2 שמירת הגובה המקורי של richTextBox1
            _originalRichHeight = richTextBox1.Height;

            // 2.3 אתחול מזהה מבחן, מילון תשובות ואינדקס התחלתי
            _examId = examId;
            _userAnswers = new Dictionary<int, string>();
            _currentIndex = 0;

            // 2.4 קריאת הקטגוריה מתוך גיליון "ExamID" (עמודה B)
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");
            using (var wb = new XLWorkbook(filePath))
            {
                var wsIds = wb.Worksheet("ExamID");
                var cell = wsIds
                    .Column(1)
                    .CellsUsed()
                    .FirstOrDefault(c => c.GetString() == _examId);
                _category = cell?.WorksheetRow().Cell(2).GetString() ?? "Unknown";
            }

            // 2.5 טעינת כל השאלות מהגליון של המבחן
            _questions = LoadQuestions(filePath, _examId);

            // 2.6 הגדרת טווח ה-ProgressBar לפי מספר השאלות
            progressBar1.Minimum = 0;
            progressBar1.Maximum = _questions.Count;

            // 2.7 הצגת השאלה הראשונה
            ShowQuestion();
        }

        // 3. טוען את כל השאלות מתוך גיליון המבחן
        private List<Question> LoadQuestions(string filePath, string examId)
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet(examId);
                return ws
                    .RangeUsed()
                    .RowsUsed()
                    .Skip(1) // דילוג על כותרת
                    .Select(r => new Question
                    {
                        Text = r.Cell(1).GetString().Trim(), // טקסט שאלה (A)
                        Type = r.Cell(2).GetString().Trim(), // סוג שאלה  (B)
                        Difficulty = r.Cell(3).GetString().Trim(), // רמת קושי (C)
                        Correct = r.Cell(5).GetString().Trim(), // תשובה נכונה (E)
                        Choices = new List<string>              // אפשרויות E–H
                        {
                            r.Cell(5).GetString().Trim(),
                            r.Cell(6).GetString().Trim(),
                            r.Cell(7).GetString().Trim(),
                            r.Cell(8).GetString().Trim()
                        }
                    })
                    .ToList();
            }
        }

        // 4. מציג את השאלה בהתאם ל־_currentIndex
        private void ShowQuestion()
        {
            var q = _questions[_currentIndex];

            // 4.1 הצגת אינדקס ושאלה
            textBox1.Text = $"{_currentIndex + 1} / {_questions.Count}";
            textBox2.Text = q.Text;
            progressBar1.Value = _currentIndex + 1;

            // 4.2 איפוס והסתרת כל בקרי המענה
            radioButton1.Visible = radioButton2.Visible =
            radioButton3.Visible = radioButton4.Visible = false;
            radioButton1.Checked = radioButton2.Checked =
            radioButton3.Checked = radioButton4.Checked = false;
            richTextBox1.Visible = false;
            richTextBox1.Clear();
            richTextBox1.Height = _originalRichHeight;

            // 4.3 הצגת בקרי מענה לפי סוג
            switch (q.Type)
            {
                case "אמריקאית":
                    // ערבול אפשרויות
                    var rnd = new Random();
                    var shuffled = q.Choices.OrderBy(x => rnd.Next()).ToArray();
                    radioButton1.Text = shuffled[0];
                    radioButton2.Text = shuffled[1];
                    radioButton3.Text = shuffled[2];
                    radioButton4.Text = shuffled[3];
                    radioButton1.Visible = radioButton2.Visible =
                    radioButton3.Visible = radioButton4.Visible = true;
                    break;

                case "נכון/לא נכון":
                    radioButton1.Text = "נכון";
                    radioButton2.Text = "לא נכון";
                    radioButton1.Visible = radioButton2.Visible = true;
                    radioButton3.Visible = radioButton4.Visible = false;
                    break;

                case "פתוחה":
                    richTextBox1.Height = _originalRichHeight * 3;
                    richTextBox1.Visible = true;
                    break;
            }

            // 4.4 שחזור תשובה קודמת אם קיימת
            if (_userAnswers.TryGetValue(_currentIndex, out var saved))
            {
                switch (q.Type)
                {
                    case "אמריקאית":
                        foreach (var rb in new[] { radioButton1, radioButton2, radioButton3, radioButton4 })
                            rb.Checked = rb.Text == saved;
                        break;

                    case "נכון/לא נכון":
                        radioButton1.Checked = saved == "נכון";
                        radioButton2.Checked = saved == "לא נכון";
                        break;

                    case "פתוחה":
                        richTextBox1.Text = saved;
                        break;
                }
            }

            // 4.5 עדכון מצב כפתורים
            button1.Enabled = _currentIndex > 0;
            button2.Enabled = _currentIndex < _questions.Count - 1;
        }

        // 5. שומר את תשובת המשתמש עבור השאלה הנוכחית
        private void SaveCurrentAnswer()
        {
            var q = _questions[_currentIndex];
            string ans = null;

            if (q.Type == "אמריקאית")
            {
                if (radioButton1.Checked) ans = radioButton1.Text;
                else if (radioButton2.Checked) ans = radioButton2.Text;
                else if (radioButton3.Checked) ans = radioButton3.Text;
                else if (radioButton4.Checked) ans = radioButton4.Text;
            }
            else if (q.Type == "נכון/לא נכון")
            {
                if (radioButton1.Checked) ans = "נכון";
                else if (radioButton2.Checked) ans = "לא נכון";
            }
            else if (q.Type == "פתוחה")
            {
                ans = richTextBox1.Text.Trim();
            }

            if (ans != null)
                _userAnswers[_currentIndex] = ans;
        }

        // 6. כפתור קודם
        private void button1_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();
            if (_currentIndex > 0)
            {
                _currentIndex--;
                ShowQuestion();
            }
        }

        // 7. כפתור הבא
        private void button2_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();
            if (_currentIndex < _questions.Count - 1)
            {
                _currentIndex++;
                ShowQuestion();
            }
        }

        // 8. כפתור סיום מבחן
        private void button3_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();
            // 8.1 איתור שאלות שלא נענו או ריקות
            var unanswered = Enumerable.Range(0, _questions.Count)
                .Where(i =>
                    !_userAnswers.ContainsKey(i)
                    || string.IsNullOrWhiteSpace(_userAnswers[i]))
                .Select(i => (i + 1).ToString())
                .ToList();

            if (unanswered.Any())
            {
                var list = string.Join(", ", unanswered);
                var res = MessageBox.Show(
                    $"לא ענית או השארת ריק בשאלות: {list}\nהאם ברצונך לסיים בכל זאת?",
                    "שאלות לא נענו", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.No)
                    return;
            }

            // 8.2 חישוב ושמירת ציון
            int score = CalculateScore();
            SaveGrade(score);
            MessageBox.Show($"המבחן הסתיים! ניקודך: {score}", "סיום");
            Close();
        }

        // 9. חישוב אחוז נכונות מתוך 100
        private int CalculateScore()
        {
            int correctCount = _questions
                .Where((q, i) =>
                    _userAnswers.TryGetValue(i, out var ans)
                    && IsCorrect(q, ans))
                .Count();
            return (int)Math.Round(100.0 * correctCount / _questions.Count);
        }

        // 10. בדיקת תשובה נכונה
        private bool IsCorrect(Question q, string ans)
        {
            return ans == q.Correct;
        }

        // 11. שמירת הציון בגליון "Grades"
        private void SaveGrade(int score)
        {
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");

            using (var wb = new XLWorkbook(path))
            {
                var ws = wb.Worksheet("Grades");

                // 11.1 מציאת עמודת הקטגוריה (שורה 2)
                int col = ws
                    .Row(2)
                    .CellsUsed()
                    .FirstOrDefault(c => c.GetString() == _category)
                    ?.Address.ColumnNumber ?? -1;
                if (col < 2) return;

                // 11.2 מציאת או הוספת שורת הסטודנט (עמודה A משורה 3)
                string student = Environment.UserName;
                var rowCell = ws
                    .Column(1)
                    .CellsUsed()
                    .Skip(1)
                    .FirstOrDefault(c => c.GetString() == student);
                if (rowCell == null)
                {
                    int newRow = ws.LastRowUsed().RowNumber() + 1;
                    ws.Cell(newRow, 1).Value = student;
                    ws.Cell(newRow, col).Value = score;
                }
                else
                {
                    rowCell.WorksheetRow().Cell(col).Value = score;
                }

                wb.Save();
            }
        }

        // 12. אירועי Designer ריקים (נדרשים ל־Designer)
        private void Exam_Load(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton3_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton4_CheckedChanged(object sender, EventArgs e) { }
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }
        private void progressBar1_Click(object sender, EventArgs e) { }

    }

    // 13. מחלקת Question לאחסון נתוני שאלה
    public class Question
    {
        public string Text { get; set; } // טקסט השאלה
        public string Type { get; set; } // סוג השאלה
        public string Difficulty { get; set; } // רמת הקושי
        public string Correct { get; set; } // התשובה הנכונה
        public List<string> Choices { get; set; } // כל האפשרויות
    }
}
