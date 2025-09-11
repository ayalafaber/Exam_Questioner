using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System.Net;

namespace Exam_Questioner
{
    public partial class RegisterForm : Form
    {
        private string selectedRole;
        private ProgressBar progressBar;
        private Label progressLabel;
        private Label motivationalLabel; 
        private Label usernameValidationLabel;
        private Label passwordValidationLabel;
        private Label idValidationLabel;
        private Label emailValidationLabel;
        private Label fullNameValidationLabel;


        public RegisterForm(string role)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.selectedRole = role;
            SetupProgressBar();
            SetupValidationLabels();
            AttachValidationEvents();
            SetupPasswordToggle();
            ReorganizeLayout();
        }

        private void SetupPasswordToggle()
        {
            btnTogglePassword.Click += BtnTogglePassword_Click;
            btnTogglePassword.MouseEnter += (s, e) => btnTogglePassword.BackColor = Color.FromArgb(229, 231, 235);
            btnTogglePassword.MouseLeave += (s, e) => btnTogglePassword.BackColor = Color.Transparent;
        }

        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar)
            {
                txtPassword.UseSystemPasswordChar = false;
                btnTogglePassword.Text = "🙈";
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                btnTogglePassword.Text = "👁";
            }
        }

        private void ReorganizeLayout()
        {
            this.mainPanel.Location = new Point(0, 160);
            this.mainPanel.Size = new Size(1440, 740);

            this.registrationContainer.Location = new Point(220, 50);
            this.registrationContainer.Size = new Size(1000, 600);

            this.registrationGroupBox.Size = new Size(1000, 600);
            this.registrationGroupBox.Padding = new Padding(60, 30, 60, 40);
        }

        private void SetupProgressBar()
        {
            // Progress container panel
            Panel progressPanel = new Panel();
            progressPanel.BackColor = Color.FromArgb(24, 28, 33);
            progressPanel.Location = new Point(0, 80);
            progressPanel.Size = new Size(1550, 50); 
            progressPanel.Dock = DockStyle.None;
            this.Controls.Add(progressPanel);

            // Progress bar
            progressBar = new ProgressBar();
            progressBar.Location = new Point(300, 10);
            progressBar.Size = new Size(800, 8);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.ForeColor = Color.FromArgb(34, 197, 94);
            progressBar.BackColor = Color.FromArgb(75, 85, 99);
            progressBar.Maximum = 100;
            progressBar.Value = 0;
            progressPanel.Controls.Add(progressBar);

            // Progress label
            progressLabel = new Label();
            progressLabel.Location = new Point(1000, 5);
            progressLabel.Size = new Size(200, 20);
            progressLabel.Text = "התקדמות: 0%";
            progressLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            progressLabel.ForeColor = Color.White;
            progressLabel.TextAlign = ContentAlignment.MiddleLeft;
            progressPanel.Controls.Add(progressLabel);

            // הוספת הודעה מעודדת
            motivationalLabel = new Label();
            motivationalLabel.Location = new Point(320, 25);
            motivationalLabel.Size = new Size(800, 25);
            motivationalLabel.Text = "בואו נתחיל! מלאו את הפרטים הבסיסיים";
            motivationalLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            motivationalLabel.ForeColor = Color.FromArgb(156, 163, 175); // צבע אפור בהיר
            motivationalLabel.TextAlign = ContentAlignment.MiddleCenter;
            progressPanel.Controls.Add(motivationalLabel);

            progressPanel.BringToFront();
        }

        private void SetupValidationLabels()
        {
            fullNameValidationLabel = CreateValidationLabel();
            fullNameValidationLabel.Location = new Point(93, 200);
            fullNameValidationLabel.Size = new Size(790, 20);

            usernameValidationLabel = CreateValidationLabel();
            usernameValidationLabel.Location = new Point(100, 290);
            usernameValidationLabel.Size = new Size(790, 20);

            passwordValidationLabel = CreateValidationLabel();
            passwordValidationLabel.Location = new Point(100, 380);
            passwordValidationLabel.Size = new Size(790, 20);

            idValidationLabel = CreateValidationLabel();
            idValidationLabel.Location = new Point(520, 470);
            idValidationLabel.Size = new Size(370, 20);

            emailValidationLabel = CreateValidationLabel();
            emailValidationLabel.Location = new Point(100, 470);
            emailValidationLabel.Size = new Size(370, 20);

            AdjustFieldPositions();

            this.registrationGroupBox.Controls.Add(fullNameValidationLabel);
            this.registrationGroupBox.Controls.Add(usernameValidationLabel);
            this.registrationGroupBox.Controls.Add(passwordValidationLabel);
            this.registrationGroupBox.Controls.Add(idValidationLabel);
            this.registrationGroupBox.Controls.Add(emailValidationLabel);
        }

        private void AdjustFieldPositions()
        {
            int spacing = 100;
            int topMargin = 110;

            this.lblFullName.Location = new Point(434, topMargin);
            this.pnlFullNameContainer.Location = new Point(93, topMargin + 35);
            fullNameValidationLabel.Location = new Point(93, topMargin + 90);

            this.lblUsername.Location = new Point(404, topMargin + spacing);
            this.pnlUsernameContainer.Location = new Point(100, topMargin + spacing + 35);
            usernameValidationLabel.Location = new Point(100, topMargin + spacing + 90);

            this.lblPassword.Location = new Point(434, topMargin + (spacing * 2));
            this.pnlPasswordContainer.Location = new Point(100, topMargin + (spacing * 2) + 35);
            passwordValidationLabel.Location = new Point(100, topMargin + (spacing * 2) + 90);

            int lastRowY = topMargin + (spacing * 3);
            this.lblID.Location = new Point(665, lastRowY);
            this.pnlIDContainer.Location = new Point(520, lastRowY + 35);
            idValidationLabel.Location = new Point(520, lastRowY + 90);

            this.lblEmail.Location = new Point(232, lastRowY);
            this.pnlEmailContainer.Location = new Point(100, lastRowY + 35);
            emailValidationLabel.Location = new Point(100, lastRowY + 90);

            this.btnRegister.Location = new Point(327, lastRowY + 120);
        }

        private Label CreateValidationLabel()
        {
            Label label = new Label();
            label.Font = new Font("Segoe UI", 9F);
            label.ForeColor = Color.FromArgb(239, 68, 68);
            label.TextAlign = ContentAlignment.MiddleRight;
            label.Text = "";
            label.Visible = false;
            label.AutoSize = false;
            return label;
        }

        private void AttachValidationEvents()
        {
            txtFullName.TextChanged += (s, e) => ValidateFullName();
            txtUsername.TextChanged += (s, e) => ValidateUsername();
            txtPassword.TextChanged += (s, e) => ValidatePassword();
            txtID.TextChanged += (s, e) => ValidateID();
            txtEmail.TextChanged += (s, e) => ValidateEmail();

            txtFullName.Enter += (s, e) => ShowFieldRequirements("fullname");
            txtUsername.Enter += (s, e) => ShowFieldRequirements("username");
            txtPassword.Enter += (s, e) => ShowFieldRequirements("password");
            txtID.Enter += (s, e) => ShowFieldRequirements("id");
            txtEmail.Enter += (s, e) => ShowFieldRequirements("email");

            txtFullName.Leave += (s, e) => HideFieldRequirements(fullNameValidationLabel);
            txtUsername.Leave += (s, e) => HideFieldRequirements(usernameValidationLabel);
            txtPassword.Leave += (s, e) => HideFieldRequirements(passwordValidationLabel);
            txtID.Leave += (s, e) => HideFieldRequirements(idValidationLabel);
            txtEmail.Leave += (s, e) => HideFieldRequirements(emailValidationLabel);
        }

        private void HideFieldRequirements(Label validationLabel)
        {
            if (validationLabel.ForeColor == Color.FromArgb(59, 130, 246))
            {
                validationLabel.Visible = false;
            }
        }

        private void ShowFieldRequirements(string field)
        {
            string requirements = "";
            switch (field)
            {
                case "fullname":
                    requirements = "💡 הזן את שמך המלא";
                    ShowValidationMessage(fullNameValidationLabel, requirements, Color.FromArgb(59, 130, 246));
                    break;
                case "username":
                    requirements = "💡 6-8 תווים, מקסימום 2 ספרות, שאר האותיות באנגלית";
                    ShowValidationMessage(usernameValidationLabel, requirements, Color.FromArgb(59, 130, 246));
                    break;
                case "password":
                    requirements = "💡 8-10 תווים, לפחות אות אחת, ספרה אחת ותו מיוחד (!@#$%&*)";
                    ShowValidationMessage(passwordValidationLabel, requirements, Color.FromArgb(59, 130, 246));
                    break;
                case "id":
                    requirements = "💡 מספר זהות בן 9 ספרות בדיוק";
                    ShowValidationMessage(idValidationLabel, requirements, Color.FromArgb(59, 130, 246));
                    break;
                case "email":
                    requirements = "💡 כתובת אימייל תקינה (example@domain.com)";
                    ShowValidationMessage(emailValidationLabel, requirements, Color.FromArgb(59, 130, 246));
                    break;
            }
        }

        private void ValidateFullName()
        {
            string fullName = txtFullName.Text.Trim();
            if (string.IsNullOrWhiteSpace(fullName))
            {
                ShowValidationMessage(fullNameValidationLabel, "❌ שדה חובה - הזן שם מלא", Color.FromArgb(239, 68, 68));
            }
            else if (fullName.Length < 2)
            {
                ShowValidationMessage(fullNameValidationLabel, "❌ שם מלא חייב להכיל לפחות 2 תווים", Color.FromArgb(239, 68, 68));
            }
            else
            {
                ShowValidationMessage(fullNameValidationLabel, "✅ שם מלא תקין", Color.FromArgb(34, 197, 94));
            }
            UpdateProgress();
        }

        private void ValidateUsername()
        {
            string username = txtUsername.Text.Trim();
            if (string.IsNullOrWhiteSpace(username))
            {
                ShowValidationMessage(usernameValidationLabel, "❌ שדה חובה - הזן שם משתמש", Color.FromArgb(239, 68, 68));
            }
            else if (username.Length < 6)
            {
                ShowValidationMessage(usernameValidationLabel, $"❌ שם משתמש קצר מדי ({username.Length}/6)", Color.FromArgb(239, 68, 68));
            }
            else if (username.Length > 8)
            {
                ShowValidationMessage(usernameValidationLabel, $"❌ שם משתמש ארוך מדי ({username.Length}/8)", Color.FromArgb(239, 68, 68));
            }
            else
            {
                int digitCount = 0;
                bool hasInvalidChars = false;
                foreach (char c in username)
                {
                    if (char.IsDigit(c)) digitCount++;
                    else if (!char.IsLetter(c)) hasInvalidChars = true;
                }

                if (hasInvalidChars)
                {
                    ShowValidationMessage(usernameValidationLabel, "❌ רק אותיות וספרות מותרים", Color.FromArgb(239, 68, 68));
                }
                else if (digitCount > 2)
                {
                    ShowValidationMessage(usernameValidationLabel, $"❌ יותר מדי ספרות ({digitCount}/2)", Color.FromArgb(239, 68, 68));
                }
                else
                {
                    ShowValidationMessage(usernameValidationLabel, "✅ שם משתמש תקין", Color.FromArgb(34, 197, 94));
                }
            }
            UpdateProgress();
        }

        private void ValidatePassword()
        {
            string password = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(password))
            {
                ShowValidationMessage(passwordValidationLabel, "❌ שדה חובה - הזן סיסמא", Color.FromArgb(239, 68, 68));
            }
            else if (password.Length < 8)
            {
                ShowValidationMessage(passwordValidationLabel, $"❌ סיסמא קצרה מדי ({password.Length}/8)", Color.FromArgb(239, 68, 68));
            }
            else if (password.Length > 10)
            {
                ShowValidationMessage(passwordValidationLabel, $"❌ סיסמא ארוכה מדי ({password.Length}/10)", Color.FromArgb(239, 68, 68));
            }
            else
            {
                bool hasLetter = false, hasDigit = false, hasSpecial = false;
                foreach (char c in password)
                {
                    if (char.IsLetter(c)) hasLetter = true;
                    else if (char.IsDigit(c)) hasDigit = true;
                    else if ("!@#$%&*()_+-=".IndexOf(c) >= 0) hasSpecial = true;
                }

                if (!hasLetter)
                {
                    ShowValidationMessage(passwordValidationLabel, "❌ חסרה אות", Color.FromArgb(239, 68, 68));
                }
                else if (!hasDigit)
                {
                    ShowValidationMessage(passwordValidationLabel, "❌ חסרה ספרה", Color.FromArgb(239, 68, 68));
                }
                else if (!hasSpecial)
                {
                    ShowValidationMessage(passwordValidationLabel, "❌ חסר תו מיוחד (!@#$%&*)", Color.FromArgb(239, 68, 68));
                }
                else
                {
                    ShowValidationMessage(passwordValidationLabel, "✅ סיסמא חזקה", Color.FromArgb(34, 197, 94));
                }
            }
            UpdateProgress();
        }

        private void ValidateID()
        {
            string id = txtID.Text.Trim();
            if (string.IsNullOrWhiteSpace(id))
            {
                ShowValidationMessage(idValidationLabel, "❌ שדה חובה - הזן מספר זהות", Color.FromArgb(239, 68, 68));
            }
            else if (id.Length != 9)
            {
                ShowValidationMessage(idValidationLabel, $"❌ מספר זהות חייב להכיל 9 ספרות ({id.Length}/9)", Color.FromArgb(239, 68, 68));
            }
            else if (!long.TryParse(id, out _))
            {
                ShowValidationMessage(idValidationLabel, "❌ מספר זהות חייב להכיל ספרות בלבד", Color.FromArgb(239, 68, 68));
            }
            else
            {
                ShowValidationMessage(idValidationLabel, "✅ מספר זהות תקין", Color.FromArgb(34, 197, 94));
            }
            UpdateProgress();
        }

        private void ValidateEmail()
        {
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                ShowValidationMessage(emailValidationLabel, "❌ שדה חובה - הזן כתובת אימייל", Color.FromArgb(239, 68, 68));
            }
            else if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                ShowValidationMessage(emailValidationLabel, "❌ כתובת אימייל לא תקינה", Color.FromArgb(239, 68, 68));
            }
            else
            {
                ShowValidationMessage(emailValidationLabel, "✅ אימייל תקין", Color.FromArgb(34, 197, 94));
            }
            UpdateProgress();
        }

        private void ShowValidationMessage(Label label, string message, Color color)
        {
            label.Text = message;
            label.ForeColor = color;
            label.Visible = true;
        }

        // פונקציה מעודכנת עם הודעות מעודדות
        private void UpdateProgress()
        {
            int validFields = 0;
            int totalFields = 5;

            if (IsValidFullName(txtFullName.Text.Trim())) validFields++;
            if (IsValidUsername(txtUsername.Text.Trim())) validFields++;
            if (IsValidPassword(txtPassword.Text)) validFields++;
            if (IsValidID(txtID.Text.Trim())) validFields++;
            if (IsValidEmail(txtEmail.Text.Trim())) validFields++;

            int progressPercentage = (validFields * 100) / totalFields;
            progressBar.Value = progressPercentage;

            string progressText = $"התקדמות: {progressPercentage}%";
            string motivationalText = GetMotivationalMessage(progressPercentage);

            if (progressPercentage == 100)
            {
                progressText += " 🎉";
            }

            progressLabel.Text = progressText;
            motivationalLabel.Text = motivationalText;
        }

        // פונקציה חדשה להודעות מעודדות
        private string GetMotivationalMessage(int progressPercentage)
        {
            switch (progressPercentage)
            {
                case 0:
                    return "בואו נתחיל! מלאו את הפרטים הבסיסיים 🚀";
                case 20:
                    return "התחלה מעולה! המשיכו כך 💪";
                case 40:
                    return "אתם באמצע הדרך - עוד קצת ואתם שם! 🌟";
                case 60:
                    return "מתקדמים יפה! כמה צעדים פשוטים ואתם בפנים 🎯";
                case 80:
                    return "כמעט סיימתם! עוד שדה אחד ואתם חלק מהמערכת 🏁";
                case 100:
                    return "מושלם! הצטרפו למערכת ולמדו דברים חדשים 🎓✨";
                default:
                    return "המשיכו כך - אתם עושים עבודה נהדרת! 👍";
            }
        }

        private bool IsValidFullName(string fullName)
        {
            return !string.IsNullOrWhiteSpace(fullName) && fullName.Length >= 2;
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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string id = txtID.Text.Trim();
            string email = txtEmail.Text.Trim();
            string fullName = txtFullName.Text.Trim();

            if (!IsValidFullName(fullName))
            {
                MessageBox.Show("❌ שם מלא לא תקין", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            if (!IsValidUsername(username))
            {
                MessageBox.Show("❌ שם משתמש לא תקין", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (!IsValidPassword(password))
            {
                MessageBox.Show("❌ סיסמא לא תקינה", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (!IsValidID(id))
            {
                MessageBox.Show("❌ מספר זהות לא תקין", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtID.Focus();
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("❌ כתובת אימייל לא תקינה", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (ExcelHelper.IDExists(id))
            {
                MessageBox.Show("❌ מספר זהות כבר קיים במערכת", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtID.Focus();
                txtID.SelectAll();
                return;
            }

            if (ExcelHelper.UsernameExists(username))
            {
                MessageBox.Show("❌ שם משתמש כבר קיים במערכת", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                txtUsername.SelectAll();
                return;
            }

            bool saved = ExcelHelper.RegisterUser(username, password, id, fullName, email, selectedRole);

            if (saved)
            {
                MessageBox.Show("🎉 ההרשמה הושלמה בהצלחה!\nכעת תוכל להתחבר למערכת.", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                LogInForm loginForm = new LogInForm(selectedRole);
                loginForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("❌ אירעה שגיאה בשמירת הנתונים\nאנא נסה שוב.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartForm startForm = new StartForm();
            startForm.Load += (s, ev) =>
            {
                startForm.ShowRoleSelectionOnly("register");
            };
            startForm.ShowDialog();
            this.Close();
        }


        private void RegisterForm_Load(object sender, EventArgs e)
        {
            lblRoleHeader.Text = $"הרשמה כ{(selectedRole == "Student" ? "סטודנט" : "מרצה")}";
            UpdateProgress();
        }

        private void lblFullName_Click(object sender, EventArgs e)
        {
            // Empty event handler
        }

        private void subtitleLabel_Click(object sender, EventArgs e)
        {

            pnlContainer.Controls.Add(lblRoleHeader);
        }
    }
}