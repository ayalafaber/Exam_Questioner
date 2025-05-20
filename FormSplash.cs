using Study_Management;
using System;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam_Questioner
{
    public partial class FormSplash : Form
    {
        private PictureBox gifBox;
        private Button btnSignIn;
        private Button btnClose;
        private Timer glowTimer = new Timer();
        private bool glowState = false;

        public FormSplash()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(800, 600);
            this.DoubleBuffered = true;
            this.Opacity = 0;

            InitUI();
            PlaySound();
            FadeIn();
        }

        private void InitUI()
        {
            // === רקע GIF ===
            gifBox = new PictureBox
            {
                Image = Image.FromFile("animated_background.gif"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Dock = DockStyle.Fill
            };
            this.Controls.Add(gifBox); // קודם מוסיפים רקע

            // === כפתור התחברות ===
            btnSignIn = new Button
            {
                Text = "🔒 התחברות",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                BackColor = Color.FromArgb(180, 0, 120, 215),
                ForeColor = Color.White,
                Size = new Size(200, 50),
                FlatStyle = FlatStyle.Flat
            };
            btnSignIn.FlatAppearance.BorderSize = 0;
            btnSignIn.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 0, 150, 255);
            btnSignIn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSignIn.Width, btnSignIn.Height, 30, 30));
            btnSignIn.Click += async (s, e) =>
            {
                glowTimer.Stop();
                await FadeOut();
                this.Hide();
                new StartForm().ShowDialog();
                this.Show(); // חזרה אם סגרו את StartForm
            };
            this.Controls.Add(btnSignIn);

            // === כפתור סגירה X ===
            btnClose = new Button
            {
                Text = "✖",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(100, Color.Black),
                Size = new Size(30, 30),
                FlatStyle = FlatStyle.Flat
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.DarkRed;
            btnClose.Click += (s, e) => this.Hide(); // רק מחביא
            this.Controls.Add(btnClose);

            // === אפקט הבהוב לכפתור התחברות ===
            glowTimer.Interval = 500;
            glowTimer.Tick += (s, e) =>
            {
                glowState = !glowState;
                btnSignIn.BackColor = glowState
                    ? Color.FromArgb(200, 0, 180, 255)
                    : Color.FromArgb(180, 0, 120, 215);
            };
            glowTimer.Start();

            // מיקום
            this.Resize += (s, e) => CenterLayout();
            CenterLayout();

            // ודא שהכפתורים מעל הכל
            btnSignIn.BringToFront();
            btnClose.BringToFront();
        }

        private void CenterLayout()
        {
            // מרכז כפתור התחברות בתחתית
            btnSignIn.Left = (this.ClientSize.Width - btnSignIn.Width) / 2;
            btnSignIn.Top = this.ClientSize.Height - btnSignIn.Height - 40;

            // מיקום כפתור סגירה
            btnClose.Top = 10;
            btnClose.Left = this.ClientSize.Width - btnClose.Width - 10;
        }

        private async Task FadeOut()
        {
            Timer fadeOutTimer = new Timer { Interval = 30 };
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            fadeOutTimer.Tick += (s, e) =>
            {
                this.Opacity -= 0.03;
                if (this.Opacity <= 0)
                {
                    fadeOutTimer.Stop();
                    tcs.SetResult(true);
                }
            };
            fadeOutTimer.Start();
            await tcs.Task;
        }

        private void FadeIn()
        {
            Timer fadeTimer = new Timer { Interval = 30 };
            fadeTimer.Tick += (s, e) =>
            {
                if (this.Opacity < 1)
                    this.Opacity += 0.03;
                else
                    fadeTimer.Stop();
            };
            fadeTimer.Start();
        }

        private void PlaySound()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("intro.wav");
                player.Play();
            }
            catch { /* אם אין קובץ – לא לעשות כלום */ }
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);
    }
}
