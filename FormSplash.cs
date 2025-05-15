using Study_Management;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam_Questioner
{
    public partial class FormSplash : Form
    {
        public FormSplash()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(700, 500);
            this.DoubleBuffered = true;
            InitUI();
        }

        private void InitUI()
        {
            Label title = new Label
            {
                Text = "QuizVerse",
                Font = new Font("Segoe UI", 36, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                BackColor = Color.Transparent,
                Location = new Point(230, 100)
            };
            this.Controls.Add(title);

            Label subtitle = new Label
            {
                Text = "Smart Learning & Exams Platform",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = Color.WhiteSmoke,
                AutoSize = true,
                BackColor = Color.Transparent,
                Location = new Point(180, 170)
            };
            this.Controls.Add(subtitle);

            Button btnSignIn = new Button
            {
                Text = "התחברות",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                Size = new Size(180, 45),
                Location = new Point(260, 280),
                FlatStyle = FlatStyle.Flat
            };
            btnSignIn.Click += (s, e) =>
            {
                this.Hide();
                StartForm start = new StartForm();
                start.ShowDialog();
                
            };
            this.Controls.Add(btnSignIn);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                Color.FromArgb(30, 30, 60), Color.FromArgb(70, 20, 80), 45f))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }


    }
}
