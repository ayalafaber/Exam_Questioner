using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Threading.Tasks;
using System.Drawing;


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
        private readonly string _username;
        private readonly string _difficulty;
        private Timer _examTimer;
        private TimeSpan _remainingTime;
        private bool _timerEnabled = false;
        private DateTime _examStartTime;



        public Exam(string examId, string username)
        {
            _username = username;
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.panelAnswers.AutoScroll = true;

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            _originalRichHeight = richTextBox1.Height;

            _examId = examId;
            _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.xlsx");
            _category = ExamLogic.GetGradeCategory(_filePath, _examId);
            _difficulty = ExamLogic.GetGradeDifficulty(_filePath, _examId);

            _userAnswers = new Dictionary<int, string>();
            _userFeedback = new Dictionary<int, string>();
            _questions = ExamLogic.LoadQuestions(_filePath, _examId);
            _currentIndex = 0;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = _questions.Count;
            InitializeTimer();

            if (ExamLogic.HasExistingGrade(_filePath, _username, _category, _difficulty))
            {
                MessageBox.Show("כבר קיים ציון עבורך במבחן זה.", "נסה מבחן אחר.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            ShowQuestion();
        }

        private void ShowQuestion()
        {
            var q = _questions[_currentIndex];
            textBox1.Text = $"{_currentIndex + 1} / {_questions.Count}";
            textBox2.Text = q.Text;
            progressBar1.Value = _currentIndex + 1;

            // איפוס בחירה
            radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = radioButton4.Checked = false;
            richTextBox1.Clear();

            // קריאה ל־BuildPanelAnswers
            string panelType;
            if (q.Type == "אמריקאית")
                panelType = "MultipleChoice";
            else if (q.Type == "נכון/לא נכון")
                panelType = "TrueFalse";
            else
                panelType = "Open";

            BuildPanelAnswers(panelType);

            // קביעת טקסטים
            if (q.Type == "אמריקאית")
            {
                var rnd = new Random();
                var shuffled = q.Choices.OrderBy(x => rnd.Next()).ToArray();
                radioButton1.Text = shuffled[0];
                radioButton2.Text = shuffled[1];
                radioButton3.Text = shuffled[2];
                radioButton4.Text = shuffled[3];
            }
            else if (q.Type == "נכון/לא נכון")
            {
                radioButton1.Text = "נכון";
                radioButton2.Text = "לא נכון";
            }

            // שיחזור תשובה קיימת
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

            // הפעלת כפתורי Previous/Next
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
            await SubmitExam(isAutoSubmit: false);
        }

        private async Task SubmitExam(bool isAutoSubmit)
        {
            if (_examTimer != null && _timerEnabled)
            {
                _examTimer.Stop();
                _timerEnabled = false;
            }
            SaveCurrentAnswer();

            var unanswered = Enumerable.Range(0, _questions.Count)
                .Where(i => !_userAnswers.ContainsKey(i) || string.IsNullOrWhiteSpace(_userAnswers[i]))
                .Select(i => (i + 1).ToString())
                .ToList();

            if (unanswered.Any() && !isAutoSubmit)
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

            for (int i = 0; i < _questions.Count; i++)
            {
                var q = _questions[i];
                var user = _userAnswers.TryGetValue(i, out var a) ? a : "(לא נענתה)";
                string line;

                if (q.Type == "פתוחה")
                {
                    // משוב GPT קיים ב־_userFeedback[i]
                    var feedback = _userFeedback.TryGetValue(i, out var fb) ? fb : "(אין משוב)";
                    line = $"{i + 1}. פתוחה:\n" +
                           $"   שאלתך: {user}\n" +
                           $"   משוב: {feedback}\n";
                }
                else
                {
                    // שאלה סגורה – בודקים ב־scores
                    bool ok = scores[i] >= 1.0;
                    line = $"{i + 1}. {q.Type}:\n" +
                           $"   תשובתך: {user}\n" +
                           (ok
                               ? "   ✅ נכון\n"
                               : $"   ❌ לא נכון – תשובה נכונה: {q.Correct}\n");
                }

                sb.AppendLine(line);
            }

            MessageBox.Show(sb.ToString(), "סיכום ותובנות");


            ExamLogic.SaveGrade(_filePath, _username, _category, _difficulty, score);
            Close();
        }



        private void InitializeTimer()
        {
            int secondsPerQuestion = 10;
            int totalSeconds = _questions.Count * secondsPerQuestion;
            _remainingTime = TimeSpan.FromSeconds(totalSeconds);

            // הודעה לתלמיד
            MessageBox.Show($"יש לך {secondsPerQuestion} שניות לכל שאלה.\nסה״כ זמן: {totalSeconds / 60} דקות {totalSeconds % 60} שניות.",
                "הנחיות למבחן", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // יוצר את הטיימר
            _examTimer = new Timer();
            _examTimer.Interval = 1000; // 1 שניה
            _examTimer.Tick += ExamTimer_Tick;

            // מפעיל את הטיימר אוטומטית
            _examTimer.Start();
            _timerEnabled = true;
        }

        private void BtnTimerControl_Click(object sender, EventArgs e)
        {
            if (_timerEnabled)
            {
                _examTimer.Stop();
                _timerEnabled = false;
                btnTimerControl.Text = "הפעל טיימר";
            }
            else
            {
                _examTimer.Start();
                _timerEnabled = true;
                btnTimerControl.Text = "עצור טיימר";
            }
        }

        private async void ExamTimer_Tick(object sender, EventArgs e)
        {
            _remainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1));

            // עדכון טקסט בכפתור
            btnTimerControl.Text = _remainingTime.ToString(@"hh\:mm\:ss");

            // אם הזמן נגמר
            if (_remainingTime.TotalSeconds <= 0)
            {
                _examTimer.Stop();
                _timerEnabled = false;
                MessageBox.Show("הזמן נגמר! המבחן יוגש אוטומטית.", "זמן נגמר", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // מפעיל את סיום המבחן במצב אוטומטי
                await SubmitExam(isAutoSubmit: true);
            }
        }


        private void Exam_Load(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton3_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton4_CheckedChanged(object sender, EventArgs e) { }
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }
        private void progressBar1_Click(object sender, EventArgs e) { }

        private void panelQuestion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelAnswers_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BuildPanelAnswers(string questionType)
        {
            // === חישוב רוחב של panelQuestion ===
            int questionPanelWidth = this.panelQuestion.Width;
            int margin = 50;
            int radioButtonWidth = questionPanelWidth - margin * 2;

            // === פנל פנימי לגלילה ===
            Panel panelInnerAnswers = new Panel();
            panelInnerAnswers.Name = "panelInnerAnswers";
            panelInnerAnswers.Location = new System.Drawing.Point(0, 0);
            panelInnerAnswers.BackColor = System.Drawing.Color.Transparent;

            // === פרמטרים ===
            int numAnswers = 4;
            int answerHeight = 90;
            int spacing = 1;
            int startY = 10;
            int totalHeight = 0;

            // === labelAnswersTitle ===
            this.labelAnswersTitle.Location = new System.Drawing.Point(margin, 10);
            panelInnerAnswers.Controls.Add(this.labelAnswersTitle);

            // === אם זו שאלה פתוחה ===
            if (questionType == "Open")
            {
                this.richTextBox1.Visible = true;
                this.richTextBox1.Location = new System.Drawing.Point(margin, startY+30);
                this.richTextBox1.Size = new System.Drawing.Size(questionPanelWidth - margin * 2, 190);
                panelInnerAnswers.Controls.Add(this.richTextBox1);

                this.radioButton1.Visible = false;
                this.radioButton2.Visible = false;
                this.radioButton3.Visible = false;
                this.radioButton4.Visible = false;

                totalHeight = startY + this.richTextBox1.Height + 50;
            }
            else if (questionType == "MultipleChoice")
            {
                this.richTextBox1.Visible = false;

                // === איפוס טקסט ===
                this.radioButton1.Text = "";
                this.radioButton2.Text = "";
                this.radioButton3.Text = "";
                this.radioButton4.Text = "";

                this.radioButton1.Visible = true;
                this.radioButton2.Visible = true;
                this.radioButton3.Visible = true;
                this.radioButton4.Visible = true;

                this.radioButton1.Location = new System.Drawing.Point(margin, startY + 0 * (answerHeight + spacing));
                this.radioButton1.Size = new Size(radioButtonWidth, answerHeight);
                panelInnerAnswers.Controls.Add(this.radioButton1);

                this.radioButton2.Location = new System.Drawing.Point(margin, startY + 1 * (answerHeight + spacing));
                this.radioButton2.Size = new Size(radioButtonWidth, answerHeight);
                panelInnerAnswers.Controls.Add(this.radioButton2);

                this.radioButton3.Location = new System.Drawing.Point(margin, startY + 2 * (answerHeight + spacing));
                this.radioButton3.Size = new Size(radioButtonWidth, answerHeight);
                panelInnerAnswers.Controls.Add(this.radioButton3);

                this.radioButton4.Location = new System.Drawing.Point(margin, startY + 3 * (answerHeight + spacing));
                this.radioButton4.Size = new Size(radioButtonWidth, answerHeight);
                panelInnerAnswers.Controls.Add(this.radioButton4);

                totalHeight = startY + numAnswers * (answerHeight + spacing) + 50;
            }
            else if (questionType == "TrueFalse")
            {
                this.richTextBox1.Visible = false;

                // === איפוס טקסט ===
                this.radioButton1.Text = "";
                this.radioButton2.Text = "";
                this.radioButton3.Text = "";
                this.radioButton4.Text = "";

                this.radioButton1.Visible = true;
                this.radioButton2.Visible = true;
                this.radioButton3.Visible = false;
                this.radioButton4.Visible = false;

                this.radioButton1.Text = "נכון";
                this.radioButton2.Text = "לא נכון";

                this.radioButton1.Location = new System.Drawing.Point(margin, startY + 0 * (answerHeight + spacing));
                this.radioButton1.Size = new Size(radioButtonWidth, answerHeight);
                panelInnerAnswers.Controls.Add(this.radioButton1);

                this.radioButton2.Location = new System.Drawing.Point(margin, startY + 1 * (answerHeight + spacing));
                this.radioButton2.Size = new Size(radioButtonWidth, answerHeight);
                panelInnerAnswers.Controls.Add(this.radioButton2);

                totalHeight = startY + 2 * (answerHeight + spacing) + 50;
            }

            // === הוספת הפאנל הפנימי ל־panelAnswers ===
            this.panelAnswers.Controls.Clear();
            this.panelAnswers.Controls.Add(panelInnerAnswers);

            // === הפעלת גלילה ===
            panelInnerAnswers.Size = new Size(questionPanelWidth, totalHeight);
            this.panelAnswers.AutoScroll = true;
            this.panelAnswers.AutoScrollMinSize = new Size(questionPanelWidth, totalHeight);

            // מתאים גם את panelAnswers
            this.panelAnswers.Size = new Size(questionPanelWidth, this.panelAnswers.Height);
        }


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