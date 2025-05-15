using System;
using System.Drawing;
using System.Windows.Forms;

namespace Study_Management
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlStudent;
        private Panel pnlLecturer;
        private Label lblStudent;
        private Label lblLecturer;
        private Label labelWelcome;
        private Button btnLogout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelWelcome = new Label();
            this.btnLogout = new Button();
            this.pnlStudent = new Panel();
            this.pnlLecturer = new Panel();
            this.lblStudent = new Label();
            this.lblLecturer = new Label();

            // labelWelcome
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.labelWelcome.ForeColor = Color.FromArgb(111, 78, 55);
            this.labelWelcome.Text = "Welcome!";

            // btnLogout
            this.btnLogout.BackColor = Color.Bisque;
            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.Font = new Font("Segoe UI", 10F);
            this.btnLogout.Text = "Logout";
            this.btnLogout.Size = new Size(80, 30);
            this.btnLogout.Click += new EventHandler(this.btnLogout_Click);

            // pnlStudent
            this.pnlStudent.BackColor = Color.White;
            this.pnlStudent.BorderStyle = BorderStyle.FixedSingle;
            this.pnlStudent.Size = new Size(500, 300);
            this.pnlStudent.Visible = false;
            this.pnlStudent.Controls.Add(this.lblStudent);

            // lblStudent
            this.lblStudent.AutoSize = true;
            this.lblStudent.Font = new Font("Segoe UI", 10F);
            this.lblStudent.ForeColor = Color.FromArgb(111, 78, 55);
            this.lblStudent.Text = "שלום סטודנט";
            this.lblStudent.Location = new Point(380, 10);

            // pnlLecturer
            this.pnlLecturer.BackColor = Color.White;
            this.pnlLecturer.BorderStyle = BorderStyle.FixedSingle;
            this.pnlLecturer.Size = new Size(500, 300);
            this.pnlLecturer.Visible = false;
            this.pnlLecturer.Controls.Add(this.lblLecturer);

            // lblLecturer
            this.lblLecturer.AutoSize = true;
            this.lblLecturer.Font = new Font("Segoe UI", 10F);
            this.lblLecturer.ForeColor = Color.FromArgb(111, 78, 55);
            this.lblLecturer.Text = "שלום מרצה";
            this.lblLecturer.Location = new Point(380, 10);

            // MainForm
            this.BackColor = Color.FromArgb(255, 248, 220);
            this.ClientSize = new Size(800, 500);
            this.Controls.Add(this.labelWelcome);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.pnlStudent);
            this.Controls.Add(this.pnlLecturer);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new EventHandler(this.MainForm_Load);
        }
    }
}
