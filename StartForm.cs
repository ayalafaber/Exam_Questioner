using System;
using System.Drawing;
using System.Windows.Forms;

namespace Study_Management
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
            pnlContainer.Location = new Point(
                (this.ClientSize.Width - pnlContainer.Width) / 2,
                (this.ClientSize.Height - pnlContainer.Height) / 2
            );

            pnlRoleSelect.Visible = false; // הסתרת פאנל בחירת תפקיד בהתחלה


        }

        private void btnCloseRolePanel_Click(object sender, EventArgs e)
        {
            pnlRoleSelect.Visible = false;
            pnlContainer.Visible = true;
        }


        private void btnExistingUser_Click(object sender, EventArgs e)
        {
            action = "login";
            pnlContainer.Visible = false;
            pnlRoleSelect.Visible = true;
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            action = "register";
            pnlContainer.Visible = false;
            pnlRoleSelect.Visible = true;
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
            if (action == "login")
            {
                LogInForm loginForm = new LogInForm(role); // צריך בנאי עם string role
                loginForm.Show();
            }
            else if (action == "register")
            {
                RegisterForm registerForm = new RegisterForm(role); // צריך בנאי עם string role
                registerForm.Show();
            }

            this.Hide();
        }

        private void pnlContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
