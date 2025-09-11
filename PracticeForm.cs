
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
using System.Media;


namespace Exam_Questioner
{
    public partial class PracticeForm : Form
    {
        private readonly List<Question> _questions;
        private int _currentIndex;
        private readonly string _subject;
        private readonly string _difficulty;
        private readonly int _originalRichHeight;
        private SoundPlayer correct;
        private SoundPlayer wrong;
        private int _correctCount = 0;
        private List<bool> _results;
        private Timer _practiceTimer;      // טיימר של התרגול
        private TimeSpan _elapsedTime;     // כמה זמן עבר
        private DateTime _sessionStartTime; // מתי התחיל

        public PracticeForm(string subject, string difficulty)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.panelAnswers.AutoScroll = true;
            BuildPanelAnswers("Open");
            this.panelAnswers.Size = new System.Drawing.Size(1327, 800);
            
            this.panelAnswers.Margin = new Padding(0);





            correct = new SoundPlayer(Properties.Resources.correct);
            wrong = new SoundPlayer(Properties.Resources.wrong);

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
            // 3. אתחל רשימת תוצאות
            _results = new List<bool>(new bool[_questions.Count]);

            // 4. אתחל את התצוגה
            progressBar1.Minimum = 0;
            progressBar1.Maximum = _questions.Count;
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.ForeColor = Color.SeaGreen;
            labelInsideBar.Text = $"0/{_questions.Count}";

            InitializePracticeTimer();


            ShowCurrentQuestion();
        }


        private void ShowCurrentQuestion()
        {
            var q = _questions[_currentIndex];

            // אינדקס ושאלה
            textBox1.Text = $"{_currentIndex + 1}/{_questions.Count}";
            textBox2.Text = q.Text;
            progressBar1.Value = _currentIndex + 1;

            // איפוס כל הבקרים (טוב להשאיר)
            foreach (var rb in new[] { radioButton1, radioButton2, radioButton3, radioButton4 })
            {
                rb.Visible = rb.Checked = false;
            }
            richTextBox1.Visible = false;
            richTextBox1.Clear();
            richTextBox1.Height = _originalRichHeight;

            // כאן - במקום כל ה־if..else שיש לך עכשיו
            if (q.Type == "אמריקאית")
                BuildPanelAnswers("MultipleChoice");
            else if (q.Type == "נכון/לא נכון")
                BuildPanelAnswers("TrueFalse");
            else // פתוחה
                BuildPanelAnswers("Open");

            // אם זו אמריקאית — אפשר להשאיר את ה־Shuffle (רק הטקסטים):
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
                this.Enabled = false;
                Cursor = Cursors.WaitCursor;
                feedback = await GptAnswerChecker.CheckAnswerAsync(q.Text, q.Correct, ans);
                Cursor = Cursors.Default;
                this.Enabled = true;
                var f = feedback.Trim().ToLower();
                if (f.StartsWith("כן"))
                    isCorrect = true;
                else
                    isCorrect = false;
            }
            else
            {
                isCorrect = ans == q.Correct;
                feedback = isCorrect
                    ? "נכון! כל הכבוד!"
                    : $"לא נכון. התשובה הנכונה היא: {q.Correct}";
            }

            // נגן צליל בהתאם
            if (isCorrect)
                correct.Play();
            else
                wrong.Play();

            // שמירת התוצאה ברשימת התשובות
            _results[_currentIndex] = isCorrect;

            // אם ענו נכון – נגדיל את _correctCount
            if (isCorrect)
                _correctCount++;

            // הצגת פידבק מיידי (לא משנה למהלך ה־ProgressBar)
            MessageBox.Show(feedback, isCorrect ? "נכון!" : "הערכה");

            // ===== עדכון ה־ProgressBar לפי כמות התשובות הנכונות =====
            progressBar1.Value = _correctCount;
            labelInsideBar.Text = $"{_correctCount}/{_questions.Count}";

            // מעבר לשאלה הבאה או הצגת סיכום בסיום
            if (_currentIndex < _questions.Count - 1)
            {
                _currentIndex++;
                ShowCurrentQuestion();
            }
            else
            {
                // התרגול הסתיים – בונים מחרוזת סיכום
                var sb = new StringBuilder();
                sb.AppendLine("סיכום התרגול:");
                sb.AppendLine($"ענית נכון על {_correctCount} מתוך {_questions.Count} שאלות.");
                sb.AppendLine();
                for (int i = 0; i < _questions.Count; i++)
                {
                    string status = _results[i] ? "✅ נכון" : "❌ טעות";
                    sb.AppendLine($"שאלה {i + 1}: {status}");
                }

                // עוצרים את הטיימר
                if (_practiceTimer != null)
                {
                    _practiceTimer.Stop();
                }

                // מוסיפים לסיכום — כמה זמן לקח
                sb.AppendLine();
                sb.AppendLine($"⏱️ זמן כולל: {(int)_elapsedTime.TotalHours:D2}:{_elapsedTime.Minutes:D2}:{_elapsedTime.Seconds:D2}");

                // חישוב זמן ממוצע לשאלה
                double avgSecondsPerQuestion = _elapsedTime.TotalSeconds / _questions.Count;
                TimeSpan avgTime = TimeSpan.FromSeconds(avgSecondsPerQuestion);

                sb.AppendLine($"🕑 ממוצע לשאלה: {(int)avgTime.TotalMinutes:D2}:{avgTime.Seconds:D2} דקות");


                // מציגים את הסיכום
                MessageBox.Show(sb.ToString(), "סיכום התרגול");

                // סוגרים את הטופס (או מתאפשר לשוב למסך קודם)
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

        private void InitializePracticeTimer()
        {
            _elapsedTime = TimeSpan.Zero;
            _sessionStartTime = DateTime.Now;

            _practiceTimer = new Timer();
            _practiceTimer.Interval = 1000; // כל שניה
            _practiceTimer.Tick += PracticeTimer_Tick;
            _practiceTimer.Start();

            // אם יש לך כפתור timepractice — אפשר להציג עליו את הזמן:
            timepractice.Text = "00:00:00";
        }

        private void PracticeTimer_Tick(object sender, EventArgs e)
        {
            _elapsedTime = DateTime.Now - _sessionStartTime;

            // מעדכנים את הכפתור timepractice:
            timepractice.Text = $"{(int)_elapsedTime.TotalHours:D2}:{_elapsedTime.Minutes:D2}:{_elapsedTime.Seconds:D2}";
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

        private void labelTitle_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelAnswers_Paint(object sender, PaintEventArgs e)
        {

        }


        private void BuildPanelAnswers(string questionType)
        {
            // === פנל פנימי לגלילה ===
            Panel panelInnerAnswers = new Panel();
            panelInnerAnswers.Name = "panelInnerAnswers";
            panelInnerAnswers.Location = new System.Drawing.Point(0, 0);
            panelInnerAnswers.BackColor = System.Drawing.Color.Transparent;

            // === פרמטרים ===
            int numAnswers = 4;
            int answerHeight = 80;
            int spacing = 2;
            int startY = 30;
            int totalHeight = 0;

            // === labelAnswersTitle ===
            this.labelAnswersTitle.Location = new System.Drawing.Point(1200, 10);
            panelInnerAnswers.Controls.Add(this.labelAnswersTitle);

            // === אם זו שאלה פתוחה ===
            if (questionType == "Open")
            {
                this.richTextBox1.Visible = true;
                this.richTextBox1.Location = new System.Drawing.Point(50, startY);
                this.richTextBox1.Size = new System.Drawing.Size(1163, 190);
                panelInnerAnswers.Controls.Add(this.richTextBox1);

                // מסתירים את ה־radioButtons
                this.radioButton1.Visible = false;
                this.radioButton2.Visible = false;
                this.radioButton3.Visible = false;
                this.radioButton4.Visible = false;

                totalHeight = startY + this.richTextBox1.Height + 50;
            }
            // === אם זו שאלה אמריקאית ===
            else if (questionType == "MultipleChoice")
            {
                this.richTextBox1.Visible = false;

                this.radioButton1.Visible = true;
                this.radioButton2.Visible = true;
                this.radioButton3.Visible = true;
                this.radioButton4.Visible = true;

                this.radioButton1.Location = new System.Drawing.Point(50, startY + 0 * (answerHeight + spacing));
                panelInnerAnswers.Controls.Add(this.radioButton1);

                this.radioButton2.Location = new System.Drawing.Point(50, startY + 1 * (answerHeight + spacing));
                panelInnerAnswers.Controls.Add(this.radioButton2);

                this.radioButton3.Location = new System.Drawing.Point(50, startY + 2 * (answerHeight + spacing));
                panelInnerAnswers.Controls.Add(this.radioButton3);

                this.radioButton4.Location = new System.Drawing.Point(50, startY + 3 * (answerHeight + spacing));
                panelInnerAnswers.Controls.Add(this.radioButton4);

                totalHeight = startY + numAnswers * (answerHeight + spacing) + 50;
            }
            // === אם זו שאלה נכון/לא נכון ===
            else if (questionType == "TrueFalse")
            {
                this.richTextBox1.Visible = false;

                this.radioButton1.Visible = true;
                this.radioButton2.Visible = true;

                this.radioButton3.Visible = false;
                this.radioButton4.Visible = false;

                this.radioButton1.Text = "נכון";
                this.radioButton2.Text = "לא נכון";

                this.radioButton1.Location = new System.Drawing.Point(50, startY + 0 * (answerHeight + spacing));
                panelInnerAnswers.Controls.Add(this.radioButton1);

                this.radioButton2.Location = new System.Drawing.Point(50, startY + 1 * (answerHeight + spacing));
                panelInnerAnswers.Controls.Add(this.radioButton2);

                totalHeight = startY + 2 * (answerHeight + spacing) + 50;
            }

            // === הוספת הפאנל הפנימי ל־panelAnswers ===
            this.panelAnswers.Controls.Clear();
            this.panelAnswers.Controls.Add(panelInnerAnswers);

            // === הפעלת גלילה ===
            panelInnerAnswers.Size = new Size(1347, totalHeight);
            this.panelAnswers.AutoScroll = true;
            this.panelAnswers.AutoScrollMinSize = new Size(1347, totalHeight);
        }



        private void panelQuestion_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}