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
            // במקום לרשום מה־Login Form, פשוט פותחים את מסך ההרשמה
            RegisterForm registerForm = new RegisterForm(selectedRole);
            registerForm.Show();
            this.Hide();
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
            // עדכון הטקסט של הכותרת
            lblRole.Text = $"התחברות כ{(selectedRole == "Student" ? "סטודנט" : "מרצה")}";

        }


        private void CenterPanel()
        {
            pnlContainer.Location = new Point(
                (this.ClientSize.Width - pnlContainer.Width) / 2,
                (this.ClientSize.Height - pnlContainer.Height) / 2
            );

            // מיקום של הטקסט בתוך הפאנל
            lblUsername.Top = 20;
            lblUsername.Left = 30;
            txtUsername.Top = lblUsername.Top;
            txtUsername.Left = lblUsername.Right + 10;

            lblPassword.Top = lblUsername.Bottom + 20;
            lblPassword.Left = lblUsername.Left;
            txtPassword.Top = lblPassword.Top;
            txtPassword.Left = lblPassword.Right + 10;

            // מיקום כפתור התחברות
            btnLogin.Top = txtPassword.Bottom + 30;
            btnLogin.Left = (pnlContainer.Width - btnLogin.Width) / 2;

            // כפתור חזור
            btnBack.Top = 10;
            btnBack.Left = 10;
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