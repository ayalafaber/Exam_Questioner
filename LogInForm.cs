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

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("נא להזין שם משתמש וסיסמה");
                return;
            }

            // האם שם המשתמש קיים?
            if (!ExcelHelper.UsernameExists(username))
            {
                MessageBox.Show("שם המשתמש לא קיים במערכת.");
                return;
            }

            // נבדוק אם קיימת התאמה לשם משתמש וסיסמה בתפקיד המבוקש
            bool exactMatch = ExcelHelper.UserExists(username, password, role);

            if (exactMatch)
            {
                string fullName = ExcelHelper.GetFullName(username, role);
                MessageBox.Show($"התחברת כ{(role == "Student" ? "סטודנט" : "מרצה")}.");

                MainForm mainForm = new MainForm(username, fullName, role);
                mainForm.StartPosition = FormStartPosition.Manual;
                mainForm.Location = this.Location;

                this.Hide();
                mainForm.ShowDialog();
                this.Close();
                return;
            }

            // אם אין התאמה לתפקיד הנוכחי – נבדוק אם שם משתמש וסיסמה נכונים אבל עם תפקיד אחר
            foreach (var altRole in new[] { "Student", "Lecturer" })
            {
                if (altRole != role && ExcelHelper.UserExists(username, password, altRole))
                {
                    MessageBox.Show("שם משתמש ו/או סיסמה נכונים, אך התפקיד שגוי.");
                    return;
                }
            }

            // אחרת – הסיסמה שגויה
            MessageBox.Show("הסיסמה שגויה עבור שם המשתמש הזה.");
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
            startForm.ShowDialog();
            this.Close();
        }
        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }

        private void lblRole_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}