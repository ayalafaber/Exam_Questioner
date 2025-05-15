using System;
using System.Windows.Forms;
using System.Drawing;



namespace Study_Management
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

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("נא להזין שם משתמש וסיסמה");
                return;
            }

            bool exists = ExcelHelper.UserExists(username, password, selectedRole);
            if (exists)
            {
                // התחברות הצליחה – תציג גם את ההתחברות לפי התפקיד
                MessageBox.Show($"התחברת כ{(selectedRole == "Student" ? "סטודנט" : "מרצה")}.");

                MainForm mainForm = new MainForm(username, selectedRole);
                mainForm.StartPosition = FormStartPosition.Manual;
                mainForm.Location = this.Location;

                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                // התחברות נכשלה – אל תציג שום תפקיד
                MessageBox.Show("שם משתמש, סיסמה או תפקיד שגויים.");

                // change to seperate messages!!!!
            }


        }


        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // במקום לרשום מה־Login Form, פשוט פותחים את מסך ההרשמה
            RegisterForm registerForm = new RegisterForm(selectedRole);
            registerForm.Show();
            this.Hide(); // לא חובה, רק אם את רוצה להסתיר את מסך ההתחברות
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
            CenterPanel();

            this.Resize += (s, eArgs) => CenterPanel();
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
            startForm.ShowDialog(); // ⬅️ בלי ShowRoleSelectionOnly
            this.Close();
        }



    }
}
