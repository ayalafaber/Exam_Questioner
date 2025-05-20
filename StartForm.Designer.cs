using System;
using System.Drawing;
using System.Windows.Forms;

namespace Study_Management
{
    partial class StartForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlContainer;
        private Button btnExistingUser;
        private Button btnNewUser;
        private Label lblWelcome;

        private Panel pnlRoleSelect;
        private Button btnStudent;
        private Button btnLecturer;
        private Button btnCloseRolePanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnExistingUser = new System.Windows.Forms.Button();
            this.btnNewUser = new System.Windows.Forms.Button();
            this.pnlRoleSelect = new System.Windows.Forms.Panel();
            this.btnStudent = new System.Windows.Forms.Button();
            this.btnLecturer = new System.Windows.Forms.Button();
            this.btnCloseRolePanel = new System.Windows.Forms.Button();
            this.pnlContainer.SuspendLayout();
            this.pnlRoleSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlContainer.Controls.Add(this.lblWelcome);
            this.pnlContainer.Controls.Add(this.btnExistingUser);
            this.pnlContainer.Controls.Add(this.btnNewUser);
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(700, 450);
            this.pnlContainer.TabIndex = 0;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = Color.SaddleBrown;
            this.lblWelcome.Location = new System.Drawing.Point(100, 60);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(500, 60);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome!\r\nAre you an existing user or a new user?";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExistingUser
            // 
            this.btnExistingUser.BackColor = Color.SaddleBrown;
            this.btnExistingUser.FlatStyle = FlatStyle.Flat;
            this.btnExistingUser.FlatAppearance.BorderSize = 0;
            this.btnExistingUser.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnExistingUser.ForeColor = Color.White;
            this.btnExistingUser.Location = new Point(175, 140);
            this.btnExistingUser.Name = "btnExistingUser";
            this.btnExistingUser.Size = new Size(150, 50);
            this.btnExistingUser.TabIndex = 1;
            this.btnExistingUser.Text = "👤 Existing User";
            this.btnExistingUser.UseVisualStyleBackColor = false;
            this.btnExistingUser.Click += new EventHandler(this.btnExistingUser_Click);
            // 
            // btnNewUser
            // 
            this.btnNewUser.BackColor = Color.SaddleBrown;
            this.btnNewUser.FlatStyle = FlatStyle.Flat;
            this.btnNewUser.FlatAppearance.BorderSize = 0;
            this.btnNewUser.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnNewUser.ForeColor = Color.White;
            this.btnNewUser.Location = new Point(375, 140);
            this.btnNewUser.Name = "btnNewUser";
            this.btnNewUser.Size = new Size(150, 50);
            this.btnNewUser.TabIndex = 2;
            this.btnNewUser.Text = "➕ New User";
            this.btnNewUser.UseVisualStyleBackColor = false;
            this.btnNewUser.Click += new EventHandler(this.btnNewUser_Click);
            // 
            // pnlRoleSelect
            // 
            this.pnlRoleSelect.BackColor = Color.White;
            this.pnlRoleSelect.BorderStyle = BorderStyle.FixedSingle;
            this.pnlRoleSelect.Controls.Add(this.btnStudent);
            this.pnlRoleSelect.Controls.Add(this.btnLecturer);
            this.pnlRoleSelect.Controls.Add(this.btnCloseRolePanel);
            this.pnlRoleSelect.Location = new Point(200, 125);
            this.pnlRoleSelect.Name = "pnlRoleSelect";
            this.pnlRoleSelect.Size = new Size(300, 200);
            this.pnlRoleSelect.TabIndex = 1;
            this.pnlRoleSelect.Visible = false;
            // 
            // btnStudent
            // 
            this.btnStudent.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            this.btnStudent.Location = new Point(75, 60);
            this.btnStudent.Name = "btnStudent";
            this.btnStudent.Size = new Size(150, 40);
            this.btnStudent.TabIndex = 0;
            this.btnStudent.Text = "סטודנט";
            this.btnStudent.UseVisualStyleBackColor = true;
            this.btnStudent.Click += new EventHandler(this.btnStudent_Click);
            // 
            // btnLecturer
            // 
            this.btnLecturer.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            this.btnLecturer.Location = new Point(75, 110);
            this.btnLecturer.Name = "btnLecturer";
            this.btnLecturer.Size = new Size(150, 40);
            this.btnLecturer.TabIndex = 1;
            this.btnLecturer.Text = "מרצה";
            this.btnLecturer.UseVisualStyleBackColor = true;
            this.btnLecturer.Click += new EventHandler(this.btnLecturer_Click);
            // 
            // btnCloseRolePanel
            // 
            this.btnCloseRolePanel.BackColor = Color.IndianRed;
            this.btnCloseRolePanel.FlatStyle = FlatStyle.Flat;
            this.btnCloseRolePanel.FlatAppearance.BorderSize = 0;
            this.btnCloseRolePanel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCloseRolePanel.ForeColor = Color.White;
            this.btnCloseRolePanel.Location = new Point(260, 10);
            this.btnCloseRolePanel.Name = "btnCloseRolePanel";
            this.btnCloseRolePanel.Size = new Size(30, 30);
            this.btnCloseRolePanel.TabIndex = 2;
            this.btnCloseRolePanel.Text = "X";
            this.btnCloseRolePanel.UseVisualStyleBackColor = false;
            this.btnCloseRolePanel.Click += new EventHandler(this.btnCloseRolePanel_Click);
            // 
            // StartForm
            // 
            this.BackColor = Color.FromArgb(255, 248, 220);
            this.ClientSize = new Size(700, 450);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.pnlRoleSelect);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "StartForm";
            this.Text = "Welcome";
            this.Load += new EventHandler(this.StartForm_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlRoleSelect.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
