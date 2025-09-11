using System;
using System.Drawing;
using System.Windows.Forms;
namespace Exam_Questioner
{
    public partial class StartForm : Form
    {
        private string action = ""; // "login" or "register"
        public StartForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void StartForm_Load(object sender, EventArgs e)
        {
            // Initially hide role selection panel
            pnlRoleSelect.Visible = false;
            pnlContainer.Visible = true;

        }
        private void btnExistingUser_Click(object sender, EventArgs e)
        {
            action = "login";
            // הודעה ידידותית
            subtitleLabel.Text = "🎯 בחר את התפקיד שלך ובואו נתחיל ללמוד יחד!";
            pnlContainer.Visible = false;
            pnlRoleSelect.Visible = true;
        }
        private void btnNewUser_Click(object sender, EventArgs e)
        {
            action = "register";
            // הודעה ידידותית
            subtitleLabel.Text = "🌟 נהדר! עוד חבר חדש במשפחת הלמידה!";
            pnlContainer.Visible = false;
            pnlRoleSelect.Visible = true;
        }
        private void btnCloseRolePanel_Click(object sender, EventArgs e)
        {
            // החזרת הודעה מקורית
            subtitleLabel.Text = "🌟 ברוכים הבאים למחר הלמידה!";
            pnlRoleSelect.Visible = false;
            pnlContainer.Visible = true;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                " בטוח שרוצה לעזוב? עוד כל כך הרבה ללמוד! 📚✨",
                "יציאה מהמערכת",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void btnStudent_Click(object sender, EventArgs e)
        {
            OpenNextForm("Student");
        }
        private void btnLecturer_Click(object sender, EventArgs e)
        {
            OpenNextForm("Lecturer");
        }
        public void ShowRoleSelectionOnly(string sourceAction)
        {
            this.action = sourceAction; // "login" או "register"
            pnlContainer.Visible = false;
            pnlRoleSelect.Visible = true;
        }
        private void OpenNextForm(string role)
        {
            try
            {
                if (action == "login")
                {
                    LogInForm loginForm = new LogInForm(role);
                    loginForm.Show();
                }
                else if (action == "register")
                {
                    RegisterForm registerForm = new RegisterForm(role);
                    registerForm.Show();
                }
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בפתיחת הטופס: " + ex.Message, "שגיאה",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}