using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.IO;

namespace Exam_Questioner
{
    public partial class PracticeForm : Form
    {
        private readonly List<Question> _questions;
        private int _currentIndex;
        private readonly string _subject;
        private readonly string _difficulty;
        private readonly int _originalRichHeight;
        public PracticeForm(string subject, string difficulty)
        {
            InitializeComponent();

            _subject = subject;
            _difficulty = difficulty;
            _currentIndex = 0;

            textBox1.ReadOnly = true;    // למשל: תצוגת אינדקס
            textBox2.ReadOnly = true;    // תצוגת שאלה
            _originalRichHeight = richTextBox1.Height;

            // 1. טען את השאלות לתרגול
            _questions = LoadPracticeQuestions();

            // 2. אם אין שאלות — הודעה וסגירה
            if (_questions.Count == 0)
            {
                MessageBox.Show(
                    $"אין שאלות לתרגול בנושא \"{_subject}\" ברמת קושי \"{_difficulty}\".",
                    "אין שאלות"
                );
                Close();
                return;
            }

            progressBar1.Minimum = 0;
            progressBar1.Maximum = _questions.Count;

            ShowCurrentQuestion();
        }


        private void ShowCurrentQuestion()
        {
            var q = _questions[_currentIndex];

            // אינדקס ושאלה
            textBox1.Text = $"{_currentIndex + 1}/{_questions.Count}";
            textBox2.Text = q.Text;
            progressBar1.Value = _currentIndex + 1;

            // איפוס כל הבקרים
            foreach (var rb in new[] { radioButton1, radioButton2, radioButton3, radioButton4 })
            {
                rb.Visible = rb.Checked = false;
            }
            richTextBox1.Visible = false;
            richTextBox1.Clear();
            richTextBox1.Height = _originalRichHeight;

            // בחר סוג
            if (q.Type == "אמריקאית")
            {
                var rnd = new Random();
                var opts = q.Choices.OrderBy(_ => rnd.Next()).ToArray();
                radioButton1.Text = opts[0];
                radioButton2.Text = opts[1];
                radioButton3.Text = opts[2];
                radioButton4.Text = opts[3];
                radioButton1.Visible = radioButton2.Visible =
                radioButton3.Visible = radioButton4.Visible = true;
            }
            else if (q.Type == "נכון/לא נכון")
            {
                radioButton1.Text = "נכון";
                radioButton2.Text = "לא נכון";
                radioButton1.Visible = radioButton2.Visible = true;
            }
            else // פתוחה
            {
                richTextBox1.Height = _originalRichHeight * 3;
                richTextBox1.Visible = true;
            }
        }


        private List<Question> LoadPracticeQuestions()
        {
            var filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");

            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("Questions");
                // קרא כל השורות (שורה 2 ואילך) ותפילטר לפי עמודות D ו-E
                return ws.RowsUsed()
                         .Skip(1)
                         .Where(r =>
                             string.Equals(r.Cell(4).GetString().Trim(), _subject, StringComparison.OrdinalIgnoreCase) &&
                             string.Equals(r.Cell(5).GetString().Trim(), _difficulty, StringComparison.OrdinalIgnoreCase)
                         )
                         .Select(r => new Question
                         {
                             Text = r.Cell(2).GetString().Trim(), // עמודה B
                             Type = r.Cell(3).GetString().Trim(), // עמודה C
                             Correct = r.Cell(6).GetString().Trim(), // עמודה F
                             Choices = new List<string>
                             {
                                 r.Cell(6).GetString().Trim(), // F
                                 r.Cell(7).GetString().Trim(), // אם קיימת G
                                 r.Cell(8).GetString().Trim(), // אם קיימת H
                                 r.Cell(9).GetString().Trim()  // אם קיימת I
                             }
                         })
                         .ToList();
            }
        }



        private string GetUserAnswer()
        {
            if (_questions[_currentIndex].Type == "אמריקאית")
            {
                if (radioButton1.Checked) return radioButton1.Text;
                if (radioButton2.Checked) return radioButton2.Text;
                if (radioButton3.Checked) return radioButton3.Text;
                if (radioButton4.Checked) return radioButton4.Text;
            }
            else if (_questions[_currentIndex].Type == "נכון/לא נכון")
            {
                if (radioButton1.Checked) return "נכון";
                if (radioButton2.Checked) return "לא נכון";
            }
            else // פתוחה
            {
                return richTextBox1.Text.Trim();
            }
            return null;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var q = _questions[_currentIndex];
            var ans = GetUserAnswer();
            if (string.IsNullOrWhiteSpace(ans))
            {
                MessageBox.Show("אנא בחר/כתוב תשובה לפני להמשיך.", "שגיאה");
                return;
            }

            bool isCorrect;
            string feedback;
            if (q.Type == "פתוחה")
            {
                feedback = await GptAnswerChecker.CheckAnswerAsync(q.Text, q.Correct, ans);
                isCorrect = feedback.StartsWith("כן") || feedback.Contains("נכונה");
            }
            else
            {
                isCorrect = ans == q.Correct;
                feedback = isCorrect
                    ? "נכון! כל הכבוד!"
                    : $"לא נכון. התשובה הנכונה היא: {q.Correct}";
            }

            // הצג פידבק
            MessageBox.Show(feedback, isCorrect ? "נכון!" : "הערכה");

            // מעבר לשאלה הבאה או סיום
            if (_currentIndex < _questions.Count - 1)
            {
                _currentIndex++;
                ShowCurrentQuestion();
            }
            else
            {
                MessageBox.Show("נגמרו השאלות, התרגול הסתיים.", "סיום");
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                ShowCurrentQuestion();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
        public class Question
        {
            public string Text { get; set; }
            public string Type { get; set; }
            public string Correct { get; set; }
            public List<string> Choices { get; set; }
        }
    }

}
