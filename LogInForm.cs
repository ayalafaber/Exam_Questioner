using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;

namespace Exam_Questioner
{
    public partial class LogInForm : Form
    {
        private string selectedRole;

        public LogInForm(string role)
        {
            InitializeComponent();
            selectedRole = role;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string role = selectedRole;

            // שימוש בלוגיקה החדשה לאימות המשתמש
            LoginResult result = LoginLogic.AuthenticateUser(username, password, role);

            if (result.IsSuccess)
            {
                // התחברות מוצלחת
                MessageBox.Show(result.Message);

                MainForm mainForm = new MainForm(result.Username, result.FullName, result.Role, result.Email);
                mainForm.StartPosition = FormStartPosition.Manual;
                mainForm.Location = this.Location;

                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                // התחברות נכשלה
                MessageBox.Show(result.Message);
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm(selectedRole);
            registerForm.Show();
            this.Hide();
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
            // עדכון הטקסט של הכותרת
            lblRole.Text = $"התחברות כ{(selectedRole == "Student" ? "סטודנט" : "מרצה")}";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartForm startForm = new StartForm();
            startForm.ShowRoleSelectionOnly("login"); // העבר את הפעולה הנוכחית
            startForm.Show();
            this.Close();
        }

        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }

        private void lblRole_Click(object sender, EventArgs e) { }
        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void lblUsername_Click(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
        private void mainPanel_Paint(object sender, PaintEventArgs e) { }
    }
}