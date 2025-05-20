using Exam_Questioner;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Study_Management
{
    public partial class MainForm : Form
    {
        private readonly string _username;
        private readonly string _role;

        public MainForm(string username, string role)
        {
            InitializeComponent();
            _username = username;
            _role = role;
            labelWelcome.Text = $"Welcome, {_username}!";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartForm startForm = new StartForm();
            startForm.ShowDialog();
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // גודל כמעט מלא
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int width = screenWidth - 100;
            int height = screenHeight - 100;

            this.Size = new Size(width, height);

            // מיקום המסך במרכז
            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - height) / 2
            );

            // הצגת פאנל לפי תפקיד
            if (_role == "Student")
            {
                pnlStudent.Visible = true;
                pnlLecturer.Visible = false;
                AddStudentButtons();
            }



            else if (_role == "Lecturer")
            {
                pnlLecturer.Visible = true;
                pnlStudent.Visible = false;
                AddLecturerButtons();
            }



            CenterLayout();
            this.Resize += (s, args) => CenterLayout();
        }

        private void CenterLayout()
        {
            // מיקום הכפתור והטקסט
            labelWelcome.Top = 20;
            labelWelcome.Left = (this.ClientSize.Width - labelWelcome.Width) / 2;

            btnLogout.Location = new Point(this.ClientSize.Width - btnLogout.Width - 20, 20);

            Panel activePanel = pnlStudent.Visible ? pnlStudent : pnlLecturer;
            activePanel.Location = new Point(
                (this.ClientSize.Width - activePanel.Width) / 2,
                (this.ClientSize.Height - activePanel.Height) / 2
            );
        }

        private void AddStudentButtons()
        {
            // כפתור מבחנים
            Button btnExams = new Button
            {
                Text = "מבחנים",
                Size = new Size(200, 40),
                Location = new Point(150, 100),
                BackColor = Color.LightSteelBlue,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnExams.Click += (s, e) =>
            {
                var examForm = new Exam_or_Practice();  // ← ודאי שזו המחלקה שמראה תרגול או מבחן
                examForm.ShowDialog();
            };
            pnlStudent.Controls.Add(btnExams);

            // כפתור ציונים
            Button btnGrades = new Button
            {
                Text = "ציונים",
                Size = new Size(200, 40),
                Location = new Point(150, 160),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnGrades.Click += (s, e) =>
            {
                // בהמשך נפתח Form שיציג ציונים, לדוגמה:
                MessageBox.Show("מסך ציונים עדיין לא מחובר.");
                // או: new GradesForm().ShowDialog();
            };
            pnlStudent.Controls.Add(btnGrades);
        }


        private void AddLecturerButtons()
        {
            // כפתור יצירת מבחן
            Button btnCreateExam = new Button
            {
                Text = "יצירת מבחן",
                Size = new Size(200, 40),
                Location = new Point(150, 100),
                BackColor = Color.SandyBrown,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnCreateExam.Click += (s, e) =>
            {
                var examForm = new Exam_Questioner.SelectExam();
                examForm.Show();
            };
            pnlLecturer.Controls.Add(btnCreateExam);

            // כפתור ניתוח נתוני סטודנטים
            Button btnStudentStats = new Button
            {
                Text = "נתוני סטודנטים",
                Size = new Size(200, 40),
                Location = new Point(150, 160),
                BackColor = Color.BurlyWood,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnStudentStats.Click += (s, e) =>
            {
                var statsForm = new Exam_Questioner.studentData();
                statsForm.Show();
            };
            pnlLecturer.Controls.Add(btnStudentStats);
        }



    }
}
