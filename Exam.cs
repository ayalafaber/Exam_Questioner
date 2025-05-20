using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Exam_Questioner
{
    public partial class Exam : Form
    {
        private readonly List<Question> _questions;
        private readonly Dictionary<int, string> _userAnswers;
        private readonly Dictionary<int, string> _userFeedback;
        private int _currentIndex;
        private readonly string _examId;
        private readonly string _category;
        private readonly int _originalRichHeight;
        private readonly string _filePath;

        public Exam(string examId)
        {
            InitializeComponent();

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            _originalRichHeight = richTextBox1.Height;

            _examId = examId;
            _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.xlsx");
            _category = ExamLogic.GetGradeCategory(_filePath, _examId);

            _userAnswers = new Dictionary<int, string>();
            _userFeedback = new Dictionary<int, string>();
            _questions = ExamLogic.LoadQuestions(_filePath, _examId);
            _currentIndex = 0;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = _questions.Count;

            ShowQuestion();
        }

        private void ShowQuestion()
        {
            var q = _questions[_currentIndex];
            textBox1.Text = $"{_currentIndex + 1} / {_questions.Count}";
            textBox2.Text = q.Text;
            progressBar1.Value = _currentIndex + 1;

            radioButton1.Visible = radioButton2.Visible =
            radioButton3.Visible = radioButton4.Visible = false;
            radioButton1.Checked = radioButton2.Checked =
            radioButton3.Checked = radioButton4.Checked = false;
            richTextBox1.Visible = false;
            richTextBox1.Clear();
            richTextBox1.Height = _originalRichHeight;

            switch (q.Type)
            {
                case "אמריקאית":
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
                    break;

                case "פתוחה":
                    richTextBox1.Height = _originalRichHeight * 3;
                    richTextBox1.Visible = true;
                    break;
            }

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

            button1.Enabled = _currentIndex > 0;
            button2.Enabled = _currentIndex < _questions.Count - 1;
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();
            if (_currentIndex > 0)
            {
                _currentIndex--;
                ShowQuestion();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();
            if (_currentIndex < _questions.Count - 1)
            {
                _currentIndex++;
                ShowQuestion();
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();

            var unanswered = Enumerable.Range(0, _questions.Count)
                .Where(i => !_userAnswers.ContainsKey(i) || string.IsNullOrWhiteSpace(_userAnswers[i]))
                .Select(i => (i + 1).ToString())
                .ToList();

            if (unanswered.Any())
            {
                var list = string.Join(", ", unanswered);
                var res = MessageBox.Show($"לא ענית או השארת ריק בשאלות: {list}\nהאם לסיים?", "שאלות לא נענו", MessageBoxButtons.YesNo);
                if (res == DialogResult.No) return;
            }

            Dictionary<int, double> scores = new Dictionary<int, double>();

            for (int i = 0; i < _questions.Count; i++)
            {
                var q = _questions[i];
                var ans = _userAnswers.TryGetValue(i, out var a) ? a : "";

                if (q.Type == "פתוחה")
                {
                    string feedback = await GptAnswerChecker.CheckAnswerAsync(q.Text, q.Correct, ans);
                    _userFeedback[i] = feedback;
                    scores[i] = ExamLogic.EvaluateOpenAnswer(feedback);
                }
                else
                {
                    scores[i] = ExamLogic.EvaluateClosedAnswer(ans, q.Correct);
                }
            }

            int score = ExamLogic.CalculateScore(_questions, scores);

            var sb = new StringBuilder();
            sb.AppendLine($"ציונך: {score}\n");
            foreach (var kv in _userFeedback)
                sb.AppendLine($"{kv.Key + 1}. {kv.Value}");

            MessageBox.Show(sb.ToString(), "סיכום ותובנות");

            ExamLogic.SaveGrade(_filePath, Environment.UserName, _category, score);
            Close();
        }

        private void Exam_Load(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton3_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton4_CheckedChanged(object sender, EventArgs e) { }
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }
        private void progressBar1_Click(object sender, EventArgs e) { }
    }

    public class Question
    {
        public string Text { get; set; }
        public string Type { get; set; }
        public string Difficulty { get; set; }
        public string Correct { get; set; }
        public List<string> Choices { get; set; }
    }
}