using System;
using System.Drawing;
using System.Windows.Forms;

namespace Study_Management
{
    public partial class MainForm : Form
    {
        private readonly string _username;
        private readonly string _role;

        public MainForm(string username, string role)
        {
            InitializeComponent();
            _username = username;
            _role = role;
            labelWelcome.Text = $"Welcome, {_username}!";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartForm startForm = new StartForm();
            startForm.ShowDialog();
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // גודל כמעט מלא
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int width = screenWidth - 100;
            int height = screenHeight - 100;

            this.Size = new Size(width, height);

            // מיקום המסך במרכז
            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - height) / 2
            );

            // הצגת פאנל לפי תפקיד
            if (_role == "Student")
            {
                pnlStudent.Visible = true;
                pnlLecturer.Visible = false;
            }
            else if (_role == "Lecturer")
            {
                pnlLecturer.Visible = true;
                pnlStudent.Visible = false;
            }

            CenterLayout();
            this.Resize += (s, args) => CenterLayout();
        }

        private void CenterLayout()
        {
            // מיקום הכפתור והטקסט
            labelWelcome.Top = 20;
            labelWelcome.Left = (this.ClientSize.Width - labelWelcome.Width) / 2;

            btnLogout.Location = new Point(this.ClientSize.Width - btnLogout.Width - 20, 20);

            Panel activePanel = pnlStudent.Visible ? pnlStudent : pnlLecturer;
            activePanel.Location = new Point(
                (this.ClientSize.Width - activePanel.Width) / 2,
                (this.ClientSize.Height - activePanel.Height) / 2
            );
        }
    }
}
