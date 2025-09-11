using System.Drawing;
using System.Windows.Forms;

namespace Exam_Questioner
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel headerPanel;
        private Label titleLabel;
        private Panel mainPanel;
        private Panel registrationContainer;
        private GroupBox registrationGroupBox;
        private Label lblRoleHeader;
        private Label lblFullName;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblID;
        private Label lblEmail;
        private Panel pnlFullNameContainer;
        private Panel pnlUsernameContainer;
        private Panel pnlPasswordContainer;
        private Panel pnlIDContainer;
        private Panel pnlEmailContainer;
        private TextBox txtFullName;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtID;
        private TextBox txtEmail;
        private Button btnRegister;
        private Button btnBack;
        private Button btnTogglePassword;
        private Panel peoplePanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.registrationContainer = new System.Windows.Forms.Panel();
            this.registrationGroupBox = new System.Windows.Forms.GroupBox();
            this.lblRoleHeader = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.pnlFullNameContainer = new System.Windows.Forms.Panel();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.pnlUsernameContainer = new System.Windows.Forms.Panel();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.pnlPasswordContainer = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnTogglePassword = new System.Windows.Forms.Button();
            this.lblID = new System.Windows.Forms.Label();
            this.pnlIDContainer = new System.Windows.Forms.Panel();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.pnlEmailContainer = new System.Windows.Forms.Panel();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.peoplePanel = new System.Windows.Forms.Panel();
            this.headerPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.registrationContainer.SuspendLayout();
            this.registrationGroupBox.SuspendLayout();
            this.pnlFullNameContainer.SuspendLayout();
            this.pnlUsernameContainer.SuspendLayout();
            this.pnlPasswordContainer.SuspendLayout();
            this.pnlIDContainer.SuspendLayout();
            this.pnlEmailContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.headerPanel.Controls.Add(this.btnBack);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1440, 120);
            this.headerPanel.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(30, 15);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(130, 45);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "🔙 חזרה";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnBack.MouseEnter += new System.EventHandler(this.BtnBack_MouseEnter);
            this.btnBack.MouseLeave += new System.EventHandler(this.BtnBack_MouseLeave);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(673, 25);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(261, 62);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "👥 הרשמה";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.mainPanel.Controls.Add(this.registrationContainer);
            this.mainPanel.Controls.Add(this.peoplePanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 120);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(40, 20, 40, 20);
            this.mainPanel.Size = new System.Drawing.Size(1440, 780);
            this.mainPanel.TabIndex = 1;
            // 
            // registrationContainer
            // 
            this.registrationContainer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.registrationContainer.Controls.Add(this.registrationGroupBox);
            this.registrationContainer.Location = new System.Drawing.Point(220, 40);
            this.registrationContainer.Name = "registrationContainer";
            this.registrationContainer.Size = new System.Drawing.Size(1000, 580);
            this.registrationContainer.TabIndex = 0;
            // 
            // registrationGroupBox
            // 
            this.registrationGroupBox.BackColor = System.Drawing.Color.White;
            this.registrationGroupBox.Controls.Add(this.lblRoleHeader);
            this.registrationGroupBox.Controls.Add(this.lblFullName);
            this.registrationGroupBox.Controls.Add(this.pnlFullNameContainer);
            this.registrationGroupBox.Controls.Add(this.lblUsername);
            this.registrationGroupBox.Controls.Add(this.pnlUsernameContainer);
            this.registrationGroupBox.Controls.Add(this.lblPassword);
            this.registrationGroupBox.Controls.Add(this.pnlPasswordContainer);
            this.registrationGroupBox.Controls.Add(this.lblID);
            this.registrationGroupBox.Controls.Add(this.pnlIDContainer);
            this.registrationGroupBox.Controls.Add(this.lblEmail);
            this.registrationGroupBox.Controls.Add(this.pnlEmailContainer);
            this.registrationGroupBox.Controls.Add(this.btnRegister);
            this.registrationGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.registrationGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registrationGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.registrationGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.registrationGroupBox.Location = new System.Drawing.Point(0, 0);
            this.registrationGroupBox.Name = "registrationGroupBox";
            this.registrationGroupBox.Padding = new System.Windows.Forms.Padding(60, 20, 60, 30);
            this.registrationGroupBox.Size = new System.Drawing.Size(1000, 580);
            this.registrationGroupBox.TabIndex = 0;
            this.registrationGroupBox.TabStop = false;
            this.registrationGroupBox.Text = "📝 פרטי הרשמה";
            // 
            // lblRoleHeader
            // 
            this.lblRoleHeader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRoleHeader.AutoSize = true;
            this.lblRoleHeader.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblRoleHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblRoleHeader.Location = new System.Drawing.Point(404, 34);
            this.lblRoleHeader.Name = "lblRoleHeader";
            this.lblRoleHeader.Size = new System.Drawing.Size(193, 46);
            this.lblRoleHeader.TabIndex = 0;
            this.lblRoleHeader.Text = "הרשמה כ...";
            this.lblRoleHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblFullName.Location = new System.Drawing.Point(434, 102);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(105, 32);
            this.lblFullName.TabIndex = 1;
            this.lblFullName.Text = "שם מלא";
            this.lblFullName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlFullNameContainer
            // 
            this.pnlFullNameContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pnlFullNameContainer.Controls.Add(this.txtFullName);
            this.pnlFullNameContainer.Location = new System.Drawing.Point(93, 137);
            this.pnlFullNameContainer.Name = "pnlFullNameContainer";
            this.pnlFullNameContainer.Size = new System.Drawing.Size(790, 50);
            this.pnlFullNameContainer.TabIndex = 2;
            // 
            // txtFullName
            // 
            this.txtFullName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.txtFullName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.txtFullName.Location = new System.Drawing.Point(15, 12);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtFullName.Size = new System.Drawing.Size(760, 29);
            this.txtFullName.TabIndex = 0;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblUsername.Location = new System.Drawing.Point(404, 212);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(146, 32);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "שם משתמש";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlUsernameContainer
            // 
            this.pnlUsernameContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pnlUsernameContainer.Controls.Add(this.txtUsername);
            this.pnlUsernameContainer.Location = new System.Drawing.Point(100, 247);
            this.pnlUsernameContainer.Name = "pnlUsernameContainer";
            this.pnlUsernameContainer.Size = new System.Drawing.Size(790, 50);
            this.pnlUsernameContainer.TabIndex = 4;
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.txtUsername.Location = new System.Drawing.Point(15, 12);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtUsername.Size = new System.Drawing.Size(760, 29);
            this.txtUsername.TabIndex = 0;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblPassword.Location = new System.Drawing.Point(434, 313);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(89, 32);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "סיסמא";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPasswordContainer
            // 
            this.pnlPasswordContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pnlPasswordContainer.Controls.Add(this.txtPassword);
            this.pnlPasswordContainer.Controls.Add(this.btnTogglePassword);
            this.pnlPasswordContainer.Location = new System.Drawing.Point(100, 348);
            this.pnlPasswordContainer.Name = "pnlPasswordContainer";
            this.pnlPasswordContainer.Size = new System.Drawing.Size(790, 50);
            this.pnlPasswordContainer.TabIndex = 6;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.txtPassword.Location = new System.Drawing.Point(55, 12);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPassword.Size = new System.Drawing.Size(720, 29);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.UseSystemPasswordChar = true;

            // 
            // btnTogglePassword
            // 
            this.btnTogglePassword.BackColor = System.Drawing.Color.Transparent;
            this.btnTogglePassword.FlatAppearance.BorderSize = 0;
            this.btnTogglePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTogglePassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnTogglePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.btnTogglePassword.Location = new System.Drawing.Point(10, 8);
            this.btnTogglePassword.Name = "btnTogglePassword";
            this.btnTogglePassword.Size = new System.Drawing.Size(40, 35);
            this.btnTogglePassword.TabIndex = 1;
            this.btnTogglePassword.Text = "👁";
            this.btnTogglePassword.UseVisualStyleBackColor = false;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblID.Location = new System.Drawing.Point(665, 401);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(136, 32);
            this.lblID.TabIndex = 7;
            this.lblID.Text = "מספר זהות";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlIDContainer
            // 
            this.pnlIDContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pnlIDContainer.Controls.Add(this.txtID);
            this.pnlIDContainer.Location = new System.Drawing.Point(520, 439);
            this.pnlIDContainer.Name = "pnlIDContainer";
            this.pnlIDContainer.Size = new System.Drawing.Size(370, 50);
            this.pnlIDContainer.TabIndex = 8;
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtID.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.txtID.Location = new System.Drawing.Point(15, 12);
            this.txtID.Name = "txtID";
            this.txtID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtID.Size = new System.Drawing.Size(340, 29);
            this.txtID.TabIndex = 0;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblEmail.Location = new System.Drawing.Point(232, 405);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(83, 32);
            this.lblEmail.TabIndex = 9;
            this.lblEmail.Text = "אימייל";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlEmailContainer
            // 
            this.pnlEmailContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pnlEmailContainer.Controls.Add(this.txtEmail);
            this.pnlEmailContainer.Location = new System.Drawing.Point(100, 439);
            this.pnlEmailContainer.Name = "pnlEmailContainer";
            this.pnlEmailContainer.Size = new System.Drawing.Size(370, 50);
            this.pnlEmailContainer.TabIndex = 10;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.txtEmail.Location = new System.Drawing.Point(15, 12);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtEmail.Size = new System.Drawing.Size(340, 29);
            this.txtEmail.TabIndex = 0;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(327, 504);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(320, 50);
            this.btnRegister.TabIndex = 11;
            this.btnRegister.Text = "🚀 השלם הרשמה";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            this.btnRegister.MouseEnter += new System.EventHandler(this.BtnRegister_MouseEnter);
            this.btnRegister.MouseLeave += new System.EventHandler(this.BtnRegister_MouseLeave);
            // 
            // peoplePanel
            // 
            this.peoplePanel.BackColor = System.Drawing.Color.Transparent;
            this.peoplePanel.Location = new System.Drawing.Point(0, 630);
            this.peoplePanel.Name = "peoplePanel";
            this.peoplePanel.Size = new System.Drawing.Size(1440, 150);
            this.peoplePanel.TabIndex = 1;
            this.peoplePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.peoplePanel_Paint);
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = Color.Transparent;
            this.pnlContainer.Location = new Point(0, 0);
            this.pnlContainer.Size = new Size(450, 300);
            this.pnlContainer.Controls.Add(this.lblRole);
            this.pnlContainer.Controls.Add(this.lblUsername);
            this.pnlContainer.Controls.Add(this.txtUsername);
            this.pnlContainer.Controls.Add(this.lblPassword);
            this.pnlContainer.Controls.Add(this.txtPassword);
            this.pnlContainer.Controls.Add(this.lblID);
            this.pnlContainer.Controls.Add(this.txtID);
            this.pnlContainer.Controls.Add(this.lblEmail);
            this.pnlContainer.Controls.Add(this.txtEmail);
            this.pnlContainer.Controls.Add(this.btnRegister);

            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1440, 900);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "RegisterForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "הרשמה - מערכת לימוד חכמה";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.registrationContainer.ResumeLayout(false);
            this.registrationGroupBox.ResumeLayout(false);
            this.registrationGroupBox.PerformLayout();
            this.pnlFullNameContainer.ResumeLayout(false);
            this.pnlFullNameContainer.PerformLayout();
            this.pnlUsernameContainer.ResumeLayout(false);
            this.pnlUsernameContainer.PerformLayout();
            this.pnlPasswordContainer.ResumeLayout(false);
            this.pnlPasswordContainer.PerformLayout();
            this.pnlIDContainer.ResumeLayout(false);
            this.pnlIDContainer.PerformLayout();
            this.pnlEmailContainer.ResumeLayout(false);
            this.pnlEmailContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        // Event handlers for modern hover effects
        private void BtnRegister_MouseEnter(object sender, System.EventArgs e)
        {
            btnRegister.BackColor = System.Drawing.Color.FromArgb(22, 163, 74); // Darker green on hover
        }

        private void BtnRegister_MouseLeave(object sender, System.EventArgs e)
        {
            btnRegister.BackColor = System.Drawing.Color.FromArgb(34, 197, 94); // Original green
        }

        private void BtnBack_MouseEnter(object sender, System.EventArgs e)
        {
            btnBack.BackColor = System.Drawing.Color.FromArgb(220, 38, 38); // Darker red on hover
        }

        private void BtnBack_MouseLeave(object sender, System.EventArgs e)
        {
            btnBack.BackColor = System.Drawing.Color.FromArgb(239, 68, 68); // Original red
        }

        // Paint event for colorful people decorations
        private void peoplePanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int panelWidth = this.peoplePanel.Width;
            int panelHeight = this.peoplePanel.Height;

            // People colors - vibrant and diverse
            System.Drawing.Color[] peopleColors = {
                System.Drawing.Color.FromArgb(239, 68, 68),   // Red
                System.Drawing.Color.FromArgb(34, 197, 94),   // Green
                System.Drawing.Color.FromArgb(59, 130, 246),  // Blue
                System.Drawing.Color.FromArgb(245, 158, 11),  // Yellow
                System.Drawing.Color.FromArgb(168, 85, 247),  // Purple
                System.Drawing.Color.FromArgb(236, 72, 153),  // Pink
                System.Drawing.Color.FromArgb(20, 184, 166),  // Teal
                System.Drawing.Color.FromArgb(251, 146, 60),  // Orange
                System.Drawing.Color.FromArgb(99, 102, 241),  // Indigo
                System.Drawing.Color.FromArgb(16, 185, 129),  // Emerald
                System.Drawing.Color.FromArgb(220, 38, 38),   // Dark Red
                System.Drawing.Color.FromArgb(101, 163, 13),  // Lime
                System.Drawing.Color.FromArgb(37, 99, 235),   // Royal Blue
                System.Drawing.Color.FromArgb(252, 211, 77),  // Amber
                System.Drawing.Color.FromArgb(124, 58, 237),  // Deep Purple
                System.Drawing.Color.FromArgb(219, 39, 119),  // Hot Pink
                System.Drawing.Color.FromArgb(14, 165, 233),  // Sky Blue
                System.Drawing.Color.FromArgb(217, 119, 6),   // Orange 600
                System.Drawing.Color.FromArgb(147, 51, 234),  // Violet
                System.Drawing.Color.FromArgb(190, 18, 60),   // Rose
            };

            System.Random rand = new System.Random(42);

            // Create scattered people figures - more than before due to larger space
            int totalPeople = 45;

            for (int i = 0; i < totalPeople; i++)
            {
                // Random position across the entire panel
                int x = rand.Next(10, panelWidth - 50);
                int y = rand.Next(5, panelHeight - 50);

                // Random color
                System.Drawing.Color personColor = peopleColors[rand.Next(peopleColors.Length)];

                // Random scale for variety
                float scale = 0.5f + (rand.Next(0, 6) * 0.1f); // Scale between 0.5 and 1.0

                // Save graphics state
                var state = g.Save();

                // Apply scaling
                g.TranslateTransform(x, y);
                g.ScaleTransform(scale, scale);

                // Random person type
                int personType = rand.Next(4);

                switch (personType)
                {
                    case 0:
                        DrawStudent(g, 0, 0, personColor);
                        break;
                    case 1:
                        DrawTeacher(g, 0, 0, personColor);
                        break;
                    case 2:
                        DrawProfessional(g, 0, 0, personColor);
                        break;
                    case 3:
                        DrawGraduate(g, 0, 0, personColor);
                        break;
                }

                // Restore graphics state
                g.Restore(state);
            }

            // Add some celebration elements
            for (int i = 0; i < 20; i++)
            {
                int x = rand.Next(0, panelWidth);
                int y = rand.Next(0, panelHeight);
                System.Drawing.Color starColor = peopleColors[rand.Next(peopleColors.Length)];

                DrawStar(g, x, y, starColor);
            }

            // Add some floating books and graduation caps
            for (int i = 0; i < 15; i++)
            {
                int x = rand.Next(50, panelWidth - 50);
                int y = rand.Next(10, panelHeight - 30);
                System.Drawing.Color itemColor = peopleColors[rand.Next(peopleColors.Length)];

                if (rand.Next(2) == 0)
                {
                    DrawFloatingBook(g, x, y, itemColor);
                }
                else
                {
                    DrawFloatingCap(g, x, y, itemColor);
                }
            }
        }

        private void DrawStudent(System.Drawing.Graphics g, int x, int y, System.Drawing.Color color)
        {
            // Head
            using (System.Drawing.SolidBrush headBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 220, 177)))
            {
                g.FillEllipse(headBrush, x + 15, y, 20, 20);
            }

            // Body
            using (System.Drawing.SolidBrush bodyBrush = new System.Drawing.SolidBrush(color))
            {
                System.Drawing.Rectangle bodyRect = new System.Drawing.Rectangle(x + 10, y + 18, 30, 25);
                g.FillRectangle(bodyBrush, bodyRect);
            }

            // Backpack
            using (System.Drawing.SolidBrush backpackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(139, 69, 19)))
            {
                System.Drawing.Rectangle backpackRect = new System.Drawing.Rectangle(x + 35, y + 20, 8, 15);
                g.FillRectangle(backpackBrush, backpackRect);
            }

            // Arms
            using (System.Drawing.SolidBrush armBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 220, 177)))
            {
                g.FillEllipse(armBrush, x + 5, y + 22, 8, 15);
                g.FillEllipse(armBrush, x + 37, y + 22, 8, 15);
            }

            // Legs
            using (System.Drawing.SolidBrush legBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(72, 61, 139)))
            {
                System.Drawing.Rectangle leftLeg = new System.Drawing.Rectangle(x + 15, y + 40, 6, 20);
                System.Drawing.Rectangle rightLeg = new System.Drawing.Rectangle(x + 29, y + 40, 6, 20);
                g.FillRectangle(legBrush, leftLeg);
                g.FillRectangle(legBrush, rightLeg);
            }

            // Book in hand
            using (System.Drawing.SolidBrush bookBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(220, 20, 60)))
            {
                System.Drawing.Rectangle bookRect = new System.Drawing.Rectangle(x + 2, y + 25, 6, 8);
                g.FillRectangle(bookBrush, bookRect);
            }
        }

        private void DrawTeacher(System.Drawing.Graphics g, int x, int y, System.Drawing.Color color)
        {
            // Head
            using (System.Drawing.SolidBrush headBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 220, 177)))
            {
                g.FillEllipse(headBrush, x + 15, y, 20, 20);
            }

            // Glasses
            using (System.Drawing.Pen glassesPen = new System.Drawing.Pen(System.Drawing.Color.Black, 2))
            {
                g.DrawEllipse(glassesPen, x + 17, y + 8, 6, 6);
                g.DrawEllipse(glassesPen, x + 27, y + 8, 6, 6);
                g.DrawLine(glassesPen, x + 23, y + 11, x + 27, y + 11);
            }

            // Body (formal attire)
            using (System.Drawing.SolidBrush bodyBrush = new System.Drawing.SolidBrush(color))
            {
                System.Drawing.Rectangle bodyRect = new System.Drawing.Rectangle(x + 10, y + 18, 30, 25);
                g.FillRectangle(bodyBrush, bodyRect);
            }

            // Tie
            using (System.Drawing.SolidBrush tieBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(139, 0, 0)))
            {
                System.Drawing.Point[] tiePoints = {
                    new System.Drawing.Point(x + 23, y + 20),
                    new System.Drawing.Point(x + 27, y + 20),
                    new System.Drawing.Point(x + 26, y + 35),
                    new System.Drawing.Point(x + 25, y + 38),
                    new System.Drawing.Point(x + 24, y + 35)
                };
                g.FillPolygon(tieBrush, tiePoints);
            }

            // Arms
            using (System.Drawing.SolidBrush armBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 220, 177)))
            {
                g.FillEllipse(armBrush, x + 5, y + 22, 8, 15);
                g.FillEllipse(armBrush, x + 37, y + 22, 8, 15);
            }

            // Legs
            using (System.Drawing.SolidBrush legBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(47, 79, 79)))
            {
                System.Drawing.Rectangle leftLeg = new System.Drawing.Rectangle(x + 15, y + 40, 6, 20);
                System.Drawing.Rectangle rightLeg = new System.Drawing.Rectangle(x + 29, y + 40, 6, 20);
                g.FillRectangle(legBrush, leftLeg);
                g.FillRectangle(legBrush, rightLeg);
            }

            // Pointer/ruler in hand
            using (System.Drawing.Pen pointerPen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(139, 69, 19), 3))
            {
                g.DrawLine(pointerPen, x + 42, y + 25, x + 48, y + 18);
            }
        }

        private void DrawProfessional(System.Drawing.Graphics g, int x, int y, System.Drawing.Color color)
        {
            // Head
            using (System.Drawing.SolidBrush headBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 220, 177)))
            {
                g.FillEllipse(headBrush, x + 15, y, 20, 20);
            }

            // Body (suit)
            using (System.Drawing.SolidBrush bodyBrush = new System.Drawing.SolidBrush(color))
            {
                System.Drawing.Rectangle bodyRect = new System.Drawing.Rectangle(x + 10, y + 18, 30, 25);
                g.FillRectangle(bodyBrush, bodyRect);
            }

            // Briefcase
            using (System.Drawing.SolidBrush briefcaseBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(139, 69, 19)))
            {
                System.Drawing.Rectangle briefcaseRect = new System.Drawing.Rectangle(x + 2, y + 30, 10, 8);
                g.FillRectangle(briefcaseBrush, briefcaseRect);
            }

            // Arms
            using (System.Drawing.SolidBrush armBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 220, 177)))
            {
                g.FillEllipse(armBrush, x + 5, y + 22, 8, 15);
                g.FillEllipse(armBrush, x + 37, y + 22, 8, 15);
            }

            // Legs
            using (System.Drawing.SolidBrush legBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(25, 25, 112)))
            {
                System.Drawing.Rectangle leftLeg = new System.Drawing.Rectangle(x + 15, y + 40, 6, 20);
                System.Drawing.Rectangle rightLeg = new System.Drawing.Rectangle(x + 29, y + 40, 6, 20);
                g.FillRectangle(legBrush, leftLeg);
                g.FillRectangle(legBrush, rightLeg);
            }
        }

        private void DrawGraduate(System.Drawing.Graphics g, int x, int y, System.Drawing.Color color)
        {
            // Head
            using (System.Drawing.SolidBrush headBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 220, 177)))
            {
                g.FillEllipse(headBrush, x + 15, y + 5, 20, 20);
            }

            // Graduation cap
            using (System.Drawing.SolidBrush capBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black))
            {
                // Cap base
                g.FillEllipse(capBrush, x + 13, y, 24, 12);
                // Cap top
                System.Drawing.Rectangle capTop = new System.Drawing.Rectangle(x + 10, y - 2, 30, 4);
                g.FillRectangle(capBrush, capTop);
            }

            // Tassel
            using (System.Drawing.Pen tasselPen = new System.Drawing.Pen(System.Drawing.Color.Gold, 2))
            {
                g.DrawLine(tasselPen, x + 35, y + 2, x + 42, y + 8);
                g.DrawLine(tasselPen, x + 42, y + 8, x + 40, y + 12);
                g.DrawLine(tasselPen, x + 42, y + 8, x + 44, y + 12);
            }

            // Graduation gown
            using (System.Drawing.SolidBrush gownBrush = new System.Drawing.SolidBrush(color))
            {
                System.Drawing.Rectangle gownRect = new System.Drawing.Rectangle(x + 8, y + 23, 34, 30);
                g.FillRectangle(gownBrush, gownRect);
            }

            // Arms
            using (System.Drawing.SolidBrush armBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 220, 177)))
            {
                g.FillEllipse(armBrush, x + 3, y + 27, 8, 15);
                g.FillEllipse(armBrush, x + 39, y + 27, 8, 15);
            }

            // Diploma
            using (System.Drawing.SolidBrush diplomaBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 255, 240)))
            {
                System.Drawing.Rectangle diplomaRect = new System.Drawing.Rectangle(x + 42, y + 28, 8, 6);
                g.FillRectangle(diplomaBrush, diplomaRect);
            }

            // Ribbon on diploma
            using (System.Drawing.Pen ribbonPen = new System.Drawing.Pen(System.Drawing.Color.Red, 2))
            {
                g.DrawLine(ribbonPen, x + 46, y + 28, x + 46, y + 34);
            }

            // Legs
            using (System.Drawing.SolidBrush legBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(25, 25, 112)))
            {
                System.Drawing.Rectangle leftLeg = new System.Drawing.Rectangle(x + 18, y + 50, 6, 15);
                System.Drawing.Rectangle rightLeg = new System.Drawing.Rectangle(x + 26, y + 50, 6, 15);
                g.FillRectangle(legBrush, leftLeg);
                g.FillRectangle(legBrush, rightLeg);
            }
        }

        private void DrawStar(System.Drawing.Graphics g, int x, int y, System.Drawing.Color color)
        {
            using (System.Drawing.SolidBrush starBrush = new System.Drawing.SolidBrush(color))
            {
                System.Drawing.Point[] starPoints = {
                    new System.Drawing.Point(x + 5, y),
                    new System.Drawing.Point(x + 6, y + 3),
                    new System.Drawing.Point(x + 10, y + 3),
                    new System.Drawing.Point(x + 7, y + 5),
                    new System.Drawing.Point(x + 8, y + 8),
                    new System.Drawing.Point(x + 5, y + 6),
                    new System.Drawing.Point(x + 2, y + 8),
                    new System.Drawing.Point(x + 3, y + 5),
                    new System.Drawing.Point(x, y + 3),
                    new System.Drawing.Point(x + 4, y + 3)
                };
                g.FillPolygon(starBrush, starPoints);
            }
        }

        private void DrawFloatingBook(System.Drawing.Graphics g, int x, int y, System.Drawing.Color color)
        {
            // Book cover
            using (System.Drawing.SolidBrush bookBrush = new System.Drawing.SolidBrush(color))
            {
                System.Drawing.Rectangle bookRect = new System.Drawing.Rectangle(x, y, 18, 12);
                g.FillRectangle(bookBrush, bookRect);
            }

            // Book pages
            using (System.Drawing.SolidBrush pagesBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 255, 240)))
            {
                System.Drawing.Rectangle pagesRect = new System.Drawing.Rectangle(x + 15, y + 2, 3, 8);
                g.FillRectangle(pagesBrush, pagesRect);
            }

            // Book spine line
            using (System.Drawing.Pen spinePen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(50, 50, 50), 1))
            {
                g.DrawLine(spinePen, x + 2, y, x + 2, y + 12);
            }
        }

        private void DrawFloatingCap(System.Drawing.Graphics g, int x, int y, System.Drawing.Color color)
        {
            // Cap base
            using (System.Drawing.SolidBrush capBrush = new System.Drawing.SolidBrush(color))
            {
                g.FillEllipse(capBrush, x, y + 5, 16, 8);
                // Cap top
                System.Drawing.Rectangle capTop = new System.Drawing.Rectangle(x - 2, y + 3, 20, 3);
                g.FillRectangle(capBrush, capTop);
            }

            // Tassel
            using (System.Drawing.Pen tasselPen = new System.Drawing.Pen(System.Drawing.Color.Gold, 1))
            {
                g.DrawLine(tasselPen, x + 14, y + 4, x + 18, y + 8);
                g.DrawLine(tasselPen, x + 18, y + 8, x + 17, y + 10);
                g.DrawLine(tasselPen, x + 18, y + 8, x + 19, y + 10);
            }
        }
    }
}