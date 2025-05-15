using System.Drawing;
using System.Windows.Forms;

namespace Study_Management
{
    partial class LogInForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblUsername;
        private Label lblPassword;
        private Label lblRole; // הצגת התפקיד שנבחר
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnBack;
        private Panel pnlContainer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.lblRole = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.btnBack = new Button();
            this.pnlContainer = new Panel();

            // pnlContainer
            this.pnlContainer.Size = new Size(450, 300);
            this.pnlContainer.BackColor = Color.Transparent;

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new Font("Segoe UI", 10F);
            this.lblUsername.ForeColor = Color.FromArgb(111, 78, 55);
            this.lblUsername.Location = new Point(30, 60);
            this.lblUsername.Text = "Username:";

            // txtUsername
            this.txtUsername.Font = new Font("Segoe UI", 10F);
            this.txtUsername.Location = new Point(150, 58);
            this.txtUsername.Size = new Size(200, 34);

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new Font("Segoe UI", 10F);
            this.lblPassword.ForeColor = Color.FromArgb(111, 78, 55);
            this.lblPassword.Location = new Point(30, 110);
            this.lblPassword.Text = "Password:";

            // txtPassword
            this.txtPassword.Font = new Font("Segoe UI", 10F);
            this.txtPassword.Location = new Point(150, 108);
            this.txtPassword.Size = new Size(200, 34);
            this.txtPassword.UseSystemPasswordChar = true;

            // lblRole
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblRole.ForeColor = Color.FromArgb(111, 78, 55);
            this.lblRole.Location = new Point(150, 0);
            this.lblRole.Text = ""; // מתעדכן בפונקציית Load לפי selectedRole

            // btnLogin
            this.btnLogin.BackColor = Color.FromArgb(111, 78, 55);
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Font = new Font("Segoe UI", 10F);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new Point(150, 160);
            this.btnLogin.Size = new Size(100, 40);
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // btnBack
            this.btnBack.BackColor = Color.Bisque;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.Font = new Font("Segoe UI", 9F);
            this.btnBack.Location = new Point(12, 12);
            this.btnBack.Size = new Size(75, 30);
            this.btnBack.Text = "← Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // Add to Panel
            this.pnlContainer.Controls.Add(this.lblRole);
            this.pnlContainer.Controls.Add(this.lblUsername);
            this.pnlContainer.Controls.Add(this.txtUsername);
            this.pnlContainer.Controls.Add(this.lblPassword);
            this.pnlContainer.Controls.Add(this.txtPassword);
            this.pnlContainer.Controls.Add(this.btnLogin);

            // LogInForm
            this.BackColor = Color.FromArgb(255, 248, 220);
            this.ClientSize = new Size(700, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Name = "LogInForm";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.LogInForm_Load);
            this.ResumeLayout(false);
        }
    }
}
