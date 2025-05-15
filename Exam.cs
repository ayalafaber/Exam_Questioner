using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Exam_Questioner
{
    public partial class Exam : Form
    {
        private List<Question> _questions;
        private Dictionary<int, object> _userAnswers;
        private int _currentIndex;

        public Exam(string examId)
        {
            InitializeComponent();
            LoadQuestions(examId);
            _userAnswers = new Dictionary<int, object>();
            _currentIndex = 0;

            // הצגת מספר השאלות הכולל
            label1.Text = _questions.Count.ToString();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = _questions.Count;

            ShowQuestion(_currentIndex);
        }

        private void LoadQuestions(string examId)
        {
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet(examId);
                var rows = ws.RangeUsed().RowsUsed().Skip(1);
                _questions = rows.Select(r => new Question
                {
                    Text = r.Cell(1).GetString(),           // עמודה A
                    Type = r.Cell(3).GetString(),           // עמודה C
                    Choices = new List<string>
                    {
                        r.Cell(5).GetString(), // E
                        r.Cell(6).GetString(), // F
                        r.Cell(7).GetString(), // G
                        r.Cell(8).GetString()  // H
                    },
                    TrueFalseAnswer = r.Cell(5).GetString()
                        .Equals("True", StringComparison.OrdinalIgnoreCase)
                }).ToList();
            }
        }

        private void ShowQuestion(int index)
        {
            var q = _questions[index];
            label2.Text = q.Text;
            progressBar1.Value = index + 1;

            // הסתרת כל בקרי תשובות
            checkedListBox1.Visible = false;
            checkedListBox2.Visible = false;
            richTextBox1.Visible = false;
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            richTextBox1.Clear();

            switch (q.Type)
            {
                case "MCQ":
                    var rnd = new Random();
                    var shuffled = q.Choices.OrderBy(x => rnd.Next()).ToArray();
                    checkedListBox1.Items.AddRange(shuffled);
                    checkedListBox1.Visible = true;
                    break;
                case "TrueFalse":
                    checkedListBox2.Items.Add("True");
                    checkedListBox2.Items.Add("False");
                    checkedListBox2.Visible = true;
                    break;
                case "Open":
                    richTextBox1.Visible = true;
                    break;
            }

            button1.Enabled = _currentIndex > 0;
            button2.Enabled = _currentIndex < _questions.Count - 1;
        }

        private void SaveCurrentAnswer()
        {
            var q = _questions[_currentIndex];
            object answer = null;
            if (q.Type == "MCQ" && checkedListBox1.CheckedItems.Count == 1)
                answer = checkedListBox1.CheckedItems[0];
            else if (q.Type == "TrueFalse" && checkedListBox2.CheckedItems.Count == 1)
                answer = checkedListBox2.CheckedItems[0];
            else if (q.Type == "Open")
                answer = richTextBox1.Text;

            if (answer != null)
                _userAnswers[_currentIndex] = answer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // קודם שמירת תשובה ואז עמוד קודם
            SaveCurrentAnswer();
            if (_currentIndex > 0)
            {
                _currentIndex--;
                ShowQuestion(_currentIndex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // קודם שמירת תשובה ואז עמוד הבא
            SaveCurrentAnswer();
            if (_currentIndex < _questions.Count - 1)
            {
                _currentIndex++;
                ShowQuestion(_currentIndex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();
            var unanswered = Enumerable.Range(0, _questions.Count).Where(i => !_userAnswers.ContainsKey(i))
                .Select(i => (i + 1).ToString());
            if (unanswered.Any())
            {
                var list = string.Join(",", unanswered);
                var res = MessageBox.Show(
                    $"לא ענית על שאלות: {list}\nהאם ברצונך לסיים בכל זאת?",
                    "שאלות לא נענו", MessageBoxButtons.YesNo);
                if (res == DialogResult.No) return;
            }

            int score = CalculateScore();
            SaveGrade(score);
            MessageBox.Show($"המבחן הסתיים! ניקודך: {score}", "סיום");
            this.Close();
        }

        private int CalculateScore()
        {
            double totalWeight = _questions.Sum(q => GetWeight(q));
            double totalPoints = 0;
            foreach (var kv in _userAnswers)
            {
                var q = _questions[kv.Key];
                if (IsCorrect(q, kv.Value))
                    totalPoints += 100.0 * GetWeight(q) / totalWeight;
            }
            return (int)Math.Round(totalPoints);
        }

        private double GetWeight(Question q)
        {
            // משקל לפי סוג קושי (Default: Medium)
            return q.Type == "קל" ? 1 : q.Type == "בינוני" ? 2 : 3;
        }

        private bool IsCorrect(Question q, object ans)
        {
            switch (q.Type)
            {
                case "MCQ":
                    // תשובה נכונה היא הראשונה ברשימת Choices המקורית
                    return ans.ToString() == q.Choices[0];
                case "TrueFalse":
                    return ans.ToString().Equals(
                        q.TrueFalseAnswer ? "True" : "False",
                        StringComparison.OrdinalIgnoreCase);
                case "Open":
                    return ans.ToString().Trim()
                               .Equals(q.Text.Trim(), StringComparison.OrdinalIgnoreCase);
                default:
                    return false;
            }
        }

        private void SaveGrade(int score)
        {
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");
            using (var wb = new XLWorkbook(path))
            {
                var ws = wb.Worksheet("Grades");
                var studentName = Environment.UserName; // או name מסך כניסה
                var row = ws.RangeUsed().RowsUsed()
                    .FirstOrDefault(r => r.Cell(1).GetString() == studentName);
                if (row != null)
                {
                    int col = GetGradeColumn();
                    row.Cell(col).Value = score;
                    wb.Save();
                }
            }
        }

        private int GetGradeColumn()
        {
            var category = _questions[0].Type;
            switch (category)
            {
                case "אנגלית":
                    return 2;
                case "מתמטיקה":
                    return 3;
                case "היסטוריה":
                    return 4;
                case "תכנות":
                    return 5;
                default:
                    return 2;
            }
        }


        // ר"ת לאירועים שנוצרו ב-Designer
        private void label1_Click(object sender, EventArgs e) { }
        private void progressBar1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }
    }

    public class Question
    {
        public string Text { get; set; }
        public string Type { get; set; }
        public List<string> Choices { get; set; }
        public bool TrueFalseAnswer { get; set; }
    }
}
