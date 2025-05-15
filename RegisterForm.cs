using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace Study_Management
{
    public partial class RegisterForm : Form
    {
        private string selectedRole;
        private Label lblRoleHeader;


        public RegisterForm(string role)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.selectedRole = role;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string id = txtID.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (!IsValidUsername(username))
            {
                MessageBox.Show("Username must be 6–8 characters, max 2 digits, and the rest letters (English only).");
                return;
            }

            if (!IsValidPassword(password))
            {
                MessageBox.Show("Password must be 8–10 characters, with at least one letter, one digit, and one special character (!, $, #, etc).");
                return;
            }

            if (!IsValidID(id))
            {
                MessageBox.Show("ID number must be exactly 9 digits.");
                return;
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Please enter a valid ID number.");
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            if (ExcelHelper.UsernameExists(username))
            {
                MessageBox.Show("Username already exists. Please choose a different one.");
                txtUsername.Focus();
                txtUsername.SelectAll();
                return;
            }

            bool saved = ExcelHelper.RegisterUser(username, password, id, email, selectedRole);
            if (saved)
            {
                MessageBox.Show("Registration successful! You can now log in.");

                this.Hide();
                LogInForm loginForm = new LogInForm(selectedRole); // עובר עם role
                loginForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("An error occurred while saving your data.");
            }
        }

        private bool IsValidUsername(string username)
        {
            if (username.Length < 6 || username.Length > 8)
                return false;

            int digitCount = 0;
            foreach (char c in username)
            {
                if (char.IsDigit(c)) digitCount++;
                else if (!char.IsLetter(c)) return false;
            }

            return digitCount <= 2;
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length < 8 || password.Length > 10) return false;

            bool hasLetter = false, hasDigit = false, hasSpecial = false;
            foreach (char c in password)
            {
                if (char.IsLetter(c)) hasLetter = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else if ("!@#$%&*()_+-=".IndexOf(c) >= 0) hasSpecial = true;
            }

            return hasLetter && hasDigit && hasSpecial;
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }

        private bool IsValidID(string id)
        {
            return id.Length == 9 && long.TryParse(id, out _);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartForm startForm = new StartForm();
            startForm.Load += (s, ev) =>
            {
                startForm.ShowRoleSelectionOnly("register"); // מחזיר למסך בחירת רול מתוך הרשמה
            };
            startForm.ShowDialog();
            this.Close();
        }


        private void RegisterForm_Load(object sender, EventArgs e)
        {
            pnlContainer.Location = new Point(
        (this.ClientSize.Width - pnlContainer.Width) / 2,
        (this.ClientSize.Height - pnlContainer.Height) / 2
    );

            // יצירת תווית עם טקסט של התפקיד
            lblRoleHeader = new Label();
            lblRoleHeader.AutoSize = true;
            lblRoleHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRoleHeader.ForeColor = Color.FromArgb(111, 78, 55);
            lblRoleHeader.Text = $"הרשמה כ{(selectedRole == "Student" ? "סטודנט" : "מרצה")}";
            lblRoleHeader.Location = new Point((pnlContainer.Width - 200) / 2, 10);

            pnlContainer.Controls.Add(lblRoleHeader);
        }
    }
}
