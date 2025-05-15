using System.Drawing;
using System.Windows.Forms;

namespace Study_Management
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblRole;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblID;
        private Label lblEmail;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtID;
        private TextBox txtEmail;
        private Button btnRegister;
        private Button btnBack;
        private Panel pnlContainer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblRole = new Label();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.lblID = new Label();
            this.lblEmail = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.txtID = new TextBox();
            this.txtEmail = new TextBox();
            this.btnRegister = new Button();
            this.btnBack = new Button();
            this.pnlContainer = new Panel();

            // 
            // lblRole
            // 
            this.lblRole.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblRole.ForeColor = Color.FromArgb(111, 78, 55);
            ///this.lblRole.Text = "הרשמה כ...";
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new Point(260, 10); // מוצמד לימין
            this.lblRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lblRole.TextAlign = ContentAlignment.TopRight;

            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new Font("Segoe UI", 10F);
            this.lblUsername.ForeColor = Color.FromArgb(111, 78, 55);
            this.lblUsername.Location = new Point(39, 50);
            this.lblUsername.Text = "Username:";

            // 
            // txtUsername
            // 
            this.txtUsername.Font = new Font("Segoe UI", 10F);
            this.txtUsername.Location = new Point(159, 47);
            this.txtUsername.Size = new Size(250, 34);

            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new Font("Segoe UI", 10F);
            this.lblPassword.ForeColor = Color.FromArgb(111, 78, 55);
            this.lblPassword.Location = new Point(39, 95);
            this.lblPassword.Text = "Password:";

            // 
            // txtPassword
            // 
            this.txtPassword.Font = new Font("Segoe UI", 10F);
            this.txtPassword.Location = new Point(159, 92);
            this.txtPassword.Size = new Size(250, 34);
            this.txtPassword.UseSystemPasswordChar = true;

            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new Font("Segoe UI", 10F);
            this.lblID.ForeColor = Color.FromArgb(111, 78, 55);
            this.lblID.Location = new Point(39, 140);
            this.lblID.Text = "ID Number:";

            // 
            // txtID
            // 
            this.txtID.Font = new Font("Segoe UI", 10F);
            this.txtID.Location = new Point(159, 137);
            this.txtID.Size = new Size(250, 34);

            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new Font("Segoe UI", 10F);
            this.lblEmail.ForeColor = Color.FromArgb(111, 78, 55);
            this.lblEmail.Location = new Point(39, 185);
            this.lblEmail.Text = "Email:";

            // 
            // txtEmail
            // 
            this.txtEmail.Font = new Font("Segoe UI", 10F);
            this.txtEmail.Location = new Point(159, 182);
            this.txtEmail.Size = new Size(250, 34);

            // 
            // btnRegister
            // 
            this.btnRegister.Font = new Font("Segoe UI", 10F);
            this.btnRegister.Text = "Register";
            this.btnRegister.Size = new Size(110, 40);
            this.btnRegister.Location = new Point(159, 235);
            this.btnRegister.BackColor = Color.FromArgb(111, 78, 55);
            this.btnRegister.ForeColor = Color.White;
            this.btnRegister.FlatStyle = FlatStyle.Flat;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            // 
            // btnBack
            // 
            this.btnBack.Text = "← Back";
            this.btnBack.Font = new Font("Segoe UI", 9F);
            this.btnBack.Location = new Point(12, 12);
            this.btnBack.Size = new Size(75, 30);
            this.btnBack.BackColor = Color.Bisque;
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

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
            this.ClientSize = new Size(700, 450);
            this.BackColor = Color.FromArgb(255, 248, 220);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.pnlContainer);
            this.Load += new System.EventHandler(this.RegisterForm_Load);
        }
    }
}
