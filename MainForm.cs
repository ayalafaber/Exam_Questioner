using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Exam_Questioner
{
    public partial class MainForm : Form
    {
        private readonly string _username;
        private readonly string _fullName;
        private readonly string _role;
        private readonly Dictionary<Button, Color> _originalColors = new Dictionary<Button, Color>();

        public MainForm(string username, string fullName, string role)
        {
            InitializeComponent();

            _username = username;
            _fullName = fullName;
            _role = role;

            SetupUI();
        }

        private void SetupUI()
        {
            // Set greeting
            string firstName = _fullName.Split(' ')[0];
            lblWelcome.Text = $"שלום {firstName}";
            lblUserRole.Text = _role == "Student" ? "מחובר בתור סטודנט" : "מחובר בתור מרצה";

            // Show appropriate panel based on role
            if (_role == "Student")
            {
                pnlStudent.Visible = true;
                pnlLecturer.Visible = false;
            }
            else if (_role == "Lecturer")
            {
                pnlLecturer.Visible = true;
                pnlStudent.Visible = false;
            }

            // Store original button colors
            _originalColors[btnExams] = Color.FromArgb(59, 130, 246);
            _originalColors[btnGrades] = Color.FromArgb(34, 197, 94);
            _originalColors[btnStudentReviews] = Color.FromArgb(236, 72, 153); // צבע ורוד לכפתור הביקורות של הסטודנט
            _originalColors[btnCreateExam] = Color.FromArgb(249, 115, 22);
            _originalColors[btnStudentStats] = Color.FromArgb(168, 85, 247);
            _originalColors[btnLecturerReviews] = Color.FromArgb(220, 38, 127); // צבע ורוד כהה לכפתור הביקורות של המרצה
            _originalColors[btnLogout] = Color.FromArgb(239, 68, 68);

            // Set button colors to original colors
            btnExams.BackColor = _originalColors[btnExams];
            btnGrades.BackColor = _originalColors[btnGrades];
            btnStudentReviews.BackColor = _originalColors[btnStudentReviews];
            btnCreateExam.BackColor = _originalColors[btnCreateExam];
            btnStudentStats.BackColor = _originalColors[btnStudentStats];
            btnLecturerReviews.BackColor = _originalColors[btnLecturerReviews];
            btnLogout.BackColor = _originalColors[btnLogout];

            // מרכז את הרכיבים בטעינה הראשונית
            CenterComponents();
        }

        private void CenterComponents()
        {
            // קבל את רוחב הפאנל הראשי (לא הטופס כולו)
            int mainPanelWidth = mainPanel.Width;

            // מרכז את ריבוע הברכות
            welcomeGroupBox.Location = new Point((mainPanelWidth - welcomeGroupBox.Width) / 2, 20);

            // מרכז את הפאנלים (Student/Lecturer)
            int panelX = (mainPanelWidth - pnlStudent.Width) / 2;
            pnlStudent.Location = new Point(panelX, 220);
            pnlLecturer.Location = new Point(panelX, 220);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // מרכז שוב אחרי שהטופס נטען לגמרי
            CenterComponents();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // מרכז מחדש כשגודל החלון משתנה
            CenterComponents();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartForm startForm = new StartForm();
            startForm.ShowDialog();
            this.Close();
        }

        // Student button events
        private void btnExams_Click(object sender, EventArgs e)
        {
            var examForm = new Exam_or_Practice(_fullName);
            examForm.ShowDialog();
        }

        private void btnGrades_Click(object sender, EventArgs e)
        {
            GradesTracker gradesForm = new GradesTracker(_username);
            gradesForm.Show();
        }

        private void btnStudentReviews_Click(object sender, EventArgs e)
        {
            // פתח את טופס הביקורות של הסטודנט
            var reviewsForm = new StudentReviewsForm(_username, _fullName);
            reviewsForm.Show();
        }

        // Lecturer button events
        private void btnCreateExam_Click(object sender, EventArgs e)
        {
            var examForm = new SelectExam();
            examForm.Show();
        }

        private void btnStudentStats_Click(object sender, EventArgs e)
        {
            var statsForm = new studentData();
            statsForm.Show();
        }

        private void btnLecturerReviews_Click(object sender, EventArgs e)
        {
            // פתח את טופס הביקורות של המרצה
            var reviewsForm = new LecturerReviewsForm(_username, _fullName);
            reviewsForm.Show();
        }

        // Button hover effects - improved version
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (_originalColors.ContainsKey(btn))
            {
                var originalColor = _originalColors[btn];
                // Create darker shade for hover effect
                int newR = Math.Max(0, originalColor.R - 30);
                int newG = Math.Max(0, originalColor.G - 30);
                int newB = Math.Max(0, originalColor.B - 30);
                btn.BackColor = Color.FromArgb(newR, newG, newB);
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (_originalColors.ContainsKey(btn))
            {
                btn.BackColor = _originalColors[btn];
            }
        }

        private void lblConnectionStatus_Click(object sender, EventArgs e)
        {
            // Connection status click handler - can be expanded if needed
        }

        private void headerPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void studentGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void welcomeGroupBox_Enter(object sender, EventArgs e)
        {

        }
    }
}