using System.Drawing;
using System.Windows.Forms;

namespace Exam_Questioner
{
    partial class LogInForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel headerPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private Panel mainPanel;
        private Panel loginContainer;
        private GroupBox loginGroupBox;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblRole;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnBack;
        private Button btnTogglePassword;
        private Panel pnlUsernameContainer;
        private Panel pnlPasswordContainer;
        private Panel booksPanel;

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
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.loginContainer = new System.Windows.Forms.Panel();
            this.loginGroupBox = new System.Windows.Forms.GroupBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.pnlUsernameContainer = new System.Windows.Forms.Panel();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.pnlPasswordContainer = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnTogglePassword = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.booksPanel = new System.Windows.Forms.Panel();
            this.headerPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.loginContainer.SuspendLayout();
            this.loginGroupBox.SuspendLayout();
            this.pnlUsernameContainer.SuspendLayout();
            this.pnlPasswordContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.headerPanel.Controls.Add(this.btnBack);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.subtitleLabel);
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
            this.titleLabel.Location = new System.Drawing.Point(656, 25);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(305, 62);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "🔐 התחברות";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.subtitleLabel.Location = new System.Drawing.Point(675, 87);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(241, 30);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "כניסה למערכת הבחינות";
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.mainPanel.Controls.Add(this.loginContainer);
            this.mainPanel.Controls.Add(this.booksPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 120);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(80, 40, 40, 40);
            this.mainPanel.Size = new System.Drawing.Size(1440, 780);
            this.mainPanel.TabIndex = 1;
            // 
            // loginContainer
            // 
            this.loginContainer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loginContainer.Controls.Add(this.loginGroupBox);
            this.loginContainer.Location = new System.Drawing.Point(320, 140);
            this.loginContainer.Name = "loginContainer";
            this.loginContainer.Size = new System.Drawing.Size(800, 500);
            this.loginContainer.TabIndex = 0;
            // 
            // loginGroupBox
            // 
            this.loginGroupBox.BackColor = System.Drawing.Color.White;
            this.loginGroupBox.Controls.Add(this.lblRole);
            this.loginGroupBox.Controls.Add(this.lblUsername);
            this.loginGroupBox.Controls.Add(this.pnlUsernameContainer);
            this.loginGroupBox.Controls.Add(this.lblPassword);
            this.loginGroupBox.Controls.Add(this.pnlPasswordContainer);
            this.loginGroupBox.Controls.Add(this.btnLogin);
            this.loginGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.loginGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.loginGroupBox.Location = new System.Drawing.Point(0, 0);
            this.loginGroupBox.Name = "loginGroupBox";
            this.loginGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.loginGroupBox.Size = new System.Drawing.Size(800, 500);
            this.loginGroupBox.TabIndex = 0;
            this.loginGroupBox.TabStop = false;
            this.loginGroupBox.Text = "🔑 פרטי התחברות";
            // 
            // lblRole
            // 
            this.lblRole.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblRole.Location = new System.Drawing.Point(287, 82);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(224, 46);
            this.lblRole.TabIndex = 0;
            this.lblRole.Text = "התחברות כ...";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblUsername.Location = new System.Drawing.Point(614, 175);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(146, 32);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "שם משתמש";
            // 
            // pnlUsernameContainer
            // 
            this.pnlUsernameContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pnlUsernameContainer.Controls.Add(this.txtUsername);
            this.pnlUsernameContainer.Location = new System.Drawing.Point(120, 175);
            this.pnlUsernameContainer.Name = "pnlUsernameContainer";
            this.pnlUsernameContainer.Size = new System.Drawing.Size(482, 50);
            this.pnlUsernameContainer.TabIndex = 2;
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
            this.txtUsername.Size = new System.Drawing.Size(456, 29);
            this.txtUsername.TabIndex = 0;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblPassword.Location = new System.Drawing.Point(659, 288);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(89, 32);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "סיסמא";
            // 
            // pnlPasswordContainer
            // 
            this.pnlPasswordContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pnlPasswordContainer.Controls.Add(this.txtPassword);
            this.pnlPasswordContainer.Controls.Add(this.btnTogglePassword);
            this.pnlPasswordContainer.Location = new System.Drawing.Point(120, 280);
            this.pnlPasswordContainer.Name = "pnlPasswordContainer";
            this.pnlPasswordContainer.Size = new System.Drawing.Size(482, 50);
            this.pnlPasswordContainer.TabIndex = 4;
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
            this.txtPassword.Size = new System.Drawing.Size(401, 29);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
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
            this.btnTogglePassword.Click += new System.EventHandler(this.BtnTogglePassword_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(234, 378);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(320, 80);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "🚀 התחבר למערכת";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnLogin.MouseEnter += new System.EventHandler(this.BtnLogin_MouseEnter);
            this.btnLogin.MouseLeave += new System.EventHandler(this.BtnLogin_MouseLeave);
            // 
            // booksPanel
            // 
            this.booksPanel.BackColor = System.Drawing.Color.Transparent;
            this.booksPanel.Location = new System.Drawing.Point(0, 636);
            this.booksPanel.Name = "booksPanel";
            this.booksPanel.Size = new System.Drawing.Size(1555, 110);
            this.booksPanel.TabIndex = 1;
            this.booksPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.booksPanel_Paint);
            // 
            // LogInForm
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
            this.Name = "LogInForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "התחברות - מערכת לימוד חכמה";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LogInForm_Load);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.loginContainer.ResumeLayout(false);
            this.loginGroupBox.ResumeLayout(false);
            this.loginGroupBox.PerformLayout();
            this.pnlUsernameContainer.ResumeLayout(false);
            this.pnlUsernameContainer.PerformLayout();
            this.pnlPasswordContainer.ResumeLayout(false);
            this.pnlPasswordContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        // Event handlers for modern hover effects
        private void BtnLogin_MouseEnter(object sender, System.EventArgs e)
        {
            btnLogin.BackColor = System.Drawing.Color.FromArgb(37, 99, 235); // Darker blue on hover
        }

        private void BtnLogin_MouseLeave(object sender, System.EventArgs e)
        {
            btnLogin.BackColor = System.Drawing.Color.FromArgb(59, 130, 246); // Original blue
        }

        private void BtnBack_MouseEnter(object sender, System.EventArgs e)
        {
            btnBack.BackColor = System.Drawing.Color.FromArgb(220, 38, 38); // Darker red on hover
        }

        private void BtnBack_MouseLeave(object sender, System.EventArgs e)
        {
            btnBack.BackColor = System.Drawing.Color.FromArgb(239, 68, 68); // Original red
        }

        // Paint event for books decoration
        private void booksPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Book colors - vibrant and diverse
            System.Drawing.Color[] bookColors = {
                System.Drawing.Color.FromArgb(239, 68, 68),   // Red
                System.Drawing.Color.FromArgb(34, 197, 94),   // Green
                System.Drawing.Color.FromArgb(59, 130, 246),  // Blue
                System.Drawing.Color.FromArgb(245, 158, 11),  // Yellow
                System.Drawing.Color.FromArgb(168, 85, 247),  // Purple
                System.Drawing.Color.FromArgb(236, 72, 153),  // Pink
                System.Drawing.Color.FromArgb(20, 184, 166),  // Teal
                System.Drawing.Color.FromArgb(251, 146, 60),  // Orange
                System.Drawing.Color.FromArgb(139, 69, 19),   // Brown
                System.Drawing.Color.FromArgb(99, 102, 241),  // Indigo
                System.Drawing.Color.FromArgb(16, 185, 129),  // Emerald
                System.Drawing.Color.FromArgb(217, 70, 239),  // Fuchsia
                System.Drawing.Color.FromArgb(234, 88, 12),   // Orange-red
                System.Drawing.Color.FromArgb(5, 150, 105),   // Green-teal
                System.Drawing.Color.FromArgb(147, 51, 234),  // Violet
                System.Drawing.Color.FromArgb(220, 38, 38),   // Dark Red
                System.Drawing.Color.FromArgb(101, 163, 13),  // Lime
                System.Drawing.Color.FromArgb(37, 99, 235),   // Royal Blue
                System.Drawing.Color.FromArgb(252, 211, 77),  // Amber
                System.Drawing.Color.FromArgb(124, 58, 237),  // Deep Purple
                System.Drawing.Color.FromArgb(219, 39, 119),  // Hot Pink
                System.Drawing.Color.FromArgb(6, 182, 212),   // Cyan
                System.Drawing.Color.FromArgb(249, 115, 22),  // Deep Orange
                System.Drawing.Color.FromArgb(120, 53, 15),   // Dark Brown
                System.Drawing.Color.FromArgb(67, 56, 202),   // Indigo Blue
                System.Drawing.Color.FromArgb(4, 120, 87),    // Dark Green
                System.Drawing.Color.FromArgb(190, 24, 93),   // Deep Pink
                System.Drawing.Color.FromArgb(180, 83, 9),    // Rust
                System.Drawing.Color.FromArgb(30, 58, 138),   // Navy
                System.Drawing.Color.FromArgb(91, 33, 182)    // Purple Deep
            };

            int bookWidth = 35;
            int bookHeight = 70;
            int spacing = 2;
            int startX = 0;
            int baseY = 30;
            int panelWidth = this.booksPanel.Width;

            // Calculate how many books can fit across the entire width
            int totalBooksNeeded = panelWidth / (bookWidth + spacing) + 5; // Add extra to ensure full coverage

            // Draw books with slight height variations across the entire width
            System.Random rand = new System.Random(42); // Fixed seed for consistent layout

            for (int i = 0; i < totalBooksNeeded; i++)
            {
                int x = startX + (i * (bookWidth + spacing));
                if (x > panelWidth) break; // Stop if we exceed panel width

                int heightVariation = rand.Next(-15, 20);
                int y = baseY + heightVariation;
                int currentHeight = bookHeight + System.Math.Abs(heightVariation);

                // Use color cycling to ensure we have enough colors
                System.Drawing.Color bookColor = bookColors[i % bookColors.Length];

                // Main book body
                using (System.Drawing.SolidBrush bookBrush = new System.Drawing.SolidBrush(bookColor))
                {
                    System.Drawing.Rectangle bookRect = new System.Drawing.Rectangle(x, y, bookWidth, currentHeight);
                    g.FillRectangle(bookBrush, bookRect);
                }

                // Book spine highlight
                using (System.Drawing.SolidBrush highlightBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(100, System.Drawing.Color.White)))
                {
                    System.Drawing.Rectangle highlightRect = new System.Drawing.Rectangle(x + 2, y + 5, 6, currentHeight - 10);
                    g.FillRectangle(highlightBrush, highlightRect);
                }

                // Book outline
                using (System.Drawing.Pen outlinePen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(50, System.Drawing.Color.Black), 1))
                {
                    System.Drawing.Rectangle outlineRect = new System.Drawing.Rectangle(x, y, bookWidth, currentHeight);
                    g.DrawRectangle(outlinePen, outlineRect);
                }

                // Book title lines (decorative) - smaller for thinner books
                using (System.Drawing.Pen titlePen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(80, System.Drawing.Color.White), 1))
                {
                    int titleY = y + currentHeight / 3;
                    g.DrawLine(titlePen, x + 8, titleY, x + bookWidth - 4, titleY);
                    if (bookWidth > 25) // Only add second line if book is wide enough
                    {
                        g.DrawLine(titlePen, x + 8, titleY + 6, x + bookWidth - 6, titleY + 6);
                    }
                }
            }

            // Add second row of smaller books for extra fullness
            int secondRowY = baseY + 60;
            int smallBookWidth = 25;
            int smallBookHeight = 40;
            int smallSpacing = 3;
            int totalSmallBooksNeeded = panelWidth / (smallBookWidth + smallSpacing) + 5;

            for (int i = 0; i < totalSmallBooksNeeded; i++)
            {
                int x = (i * (smallBookWidth + smallSpacing)) + 15; // Offset for staggered look
                if (x > panelWidth) break;

                int heightVariation = rand.Next(-8, 12);
                int y = secondRowY + heightVariation;
                int currentHeight = smallBookHeight + System.Math.Abs(heightVariation / 2);

                // Use different color pattern for variety
                System.Drawing.Color bookColor = bookColors[(i * 3 + 7) % bookColors.Length];

                using (System.Drawing.SolidBrush smallBookBrush = new System.Drawing.SolidBrush(bookColor))
                {
                    System.Drawing.Rectangle smallBookRect = new System.Drawing.Rectangle(x, y, smallBookWidth, currentHeight);
                    g.FillRectangle(smallBookBrush, smallBookRect);
                }

                using (System.Drawing.Pen smallOutlinePen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(50, System.Drawing.Color.Black), 1))
                {
                    System.Drawing.Rectangle smallOutlineRect = new System.Drawing.Rectangle(x, y, smallBookWidth, currentHeight);
                    g.DrawRectangle(smallOutlinePen, smallOutlineRect);
                }

                // Small highlight
                using (System.Drawing.SolidBrush highlightBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(80, System.Drawing.Color.White)))
                {
                    System.Drawing.Rectangle highlightRect = new System.Drawing.Rectangle(x + 1, y + 3, 4, currentHeight - 6);
                    g.FillRectangle(highlightBrush, highlightRect);
                }
            }
        }
    }
}