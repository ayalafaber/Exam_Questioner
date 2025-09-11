namespace Exam_Questioner
{
    partial class StartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.welcomeGroupBox = new System.Windows.Forms.GroupBox();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.actionsGroupBox = new System.Windows.Forms.GroupBox();
            this.btnExistingUser = new System.Windows.Forms.Button();
            this.btnNewUser = new System.Windows.Forms.Button();
            this.pnlLeftDecoration = new System.Windows.Forms.Panel();
            this.pnlRightDecoration = new System.Windows.Forms.Panel();
            this.pnlRoleSelect = new System.Windows.Forms.Panel();
            this.roleGroupBox = new System.Windows.Forms.GroupBox();
            this.lblSelectRole = new System.Windows.Forms.Label();
            this.btnStudent = new System.Windows.Forms.Button();
            this.btnLecturer = new System.Windows.Forms.Button();
            this.btnCloseRolePanel = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.welcomeGroupBox.SuspendLayout();
            this.actionsGroupBox.SuspendLayout();
            this.pnlRoleSelect.SuspendLayout();
            this.roleGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.headerPanel.Controls.Add(this.btnClose);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.subtitleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1440, 120);
            this.headerPanel.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(30, 35);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 50);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "✕ יציאה";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(501, 23);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(515, 62);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "📚 מערכת לימוד חכמה";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.subtitleLabel.Location = new System.Drawing.Point(558, 85);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(339, 30);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "ברוכים הבאים למערכת המתקדמת";
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.mainPanel.Controls.Add(this.pnlContainer);
            this.mainPanel.Controls.Add(this.pnlLeftDecoration);
            this.mainPanel.Controls.Add(this.pnlRightDecoration);
            this.mainPanel.Controls.Add(this.pnlRoleSelect);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 120);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(80, 40, 80, 40);
            this.mainPanel.Size = new System.Drawing.Size(1440, 780);
            this.mainPanel.TabIndex = 1;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlContainer.Controls.Add(this.welcomeGroupBox);
            this.pnlContainer.Controls.Add(this.actionsGroupBox);
            this.pnlContainer.Location = new System.Drawing.Point(320, 80);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(800, 600);
            this.pnlContainer.TabIndex = 0;
            // 
            // welcomeGroupBox
            // 
            this.welcomeGroupBox.BackColor = System.Drawing.Color.White;
            this.welcomeGroupBox.Controls.Add(this.lblWelcome);
            this.welcomeGroupBox.Controls.Add(this.lblSubtitle);
            this.welcomeGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.welcomeGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.welcomeGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.welcomeGroupBox.Location = new System.Drawing.Point(0, 0);
            this.welcomeGroupBox.Name = "welcomeGroupBox";
            this.welcomeGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.welcomeGroupBox.Size = new System.Drawing.Size(800, 280);
            this.welcomeGroupBox.TabIndex = 0;
            this.welcomeGroupBox.TabStop = false;
            this.welcomeGroupBox.Text = "ברוכים הבאים";
            // 
            // lblWelcome
            // 
            this.lblWelcome.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.lblWelcome.Location = new System.Drawing.Point(30, 56);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(740, 100);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "שלום";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblSubtitle.Location = new System.Drawing.Point(30, 213);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(740, 37);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "מערכת בחינות מתקדמת";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // actionsGroupBox
            // 
            this.actionsGroupBox.BackColor = System.Drawing.Color.White;
            this.actionsGroupBox.Controls.Add(this.btnExistingUser);
            this.actionsGroupBox.Controls.Add(this.btnNewUser);
            this.actionsGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actionsGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.actionsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.actionsGroupBox.Location = new System.Drawing.Point(0, 320);
            this.actionsGroupBox.Name = "actionsGroupBox";
            this.actionsGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.actionsGroupBox.Size = new System.Drawing.Size(800, 280);
            this.actionsGroupBox.TabIndex = 1;
            this.actionsGroupBox.TabStop = false;
            this.actionsGroupBox.Text = "בחר פעולה";
            // 
            // btnExistingUser
            // 
            this.btnExistingUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExistingUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnExistingUser.FlatAppearance.BorderSize = 0;
            this.btnExistingUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExistingUser.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnExistingUser.ForeColor = System.Drawing.Color.White;
            this.btnExistingUser.Location = new System.Drawing.Point(200, 60);
            this.btnExistingUser.Name = "btnExistingUser";
            this.btnExistingUser.Size = new System.Drawing.Size(400, 80);
            this.btnExistingUser.TabIndex = 0;
            this.btnExistingUser.Text = "🔐 משתמש קיים";
            this.btnExistingUser.UseVisualStyleBackColor = false;
            this.btnExistingUser.Click += new System.EventHandler(this.btnExistingUser_Click);
            // 
            // btnNewUser
            // 
            this.btnNewUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNewUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnNewUser.FlatAppearance.BorderSize = 0;
            this.btnNewUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewUser.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnNewUser.ForeColor = System.Drawing.Color.White;
            this.btnNewUser.Location = new System.Drawing.Point(200, 160);
            this.btnNewUser.Name = "btnNewUser";
            this.btnNewUser.Size = new System.Drawing.Size(400, 80);
            this.btnNewUser.TabIndex = 1;
            this.btnNewUser.Text = "✨ משתמש חדש";
            this.btnNewUser.UseVisualStyleBackColor = false;
            this.btnNewUser.Click += new System.EventHandler(this.btnNewUser_Click);
            // 
            // pnlLeftDecoration
            // 
            this.pnlLeftDecoration.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeftDecoration.Location = new System.Drawing.Point(50, 100);
            this.pnlLeftDecoration.Name = "pnlLeftDecoration";
            this.pnlLeftDecoration.Size = new System.Drawing.Size(200, 400);
            this.pnlLeftDecoration.TabIndex = 2;
            this.pnlLeftDecoration.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlLeftDecoration_Paint);
            // 
            // pnlRightDecoration
            // 
            this.pnlRightDecoration.BackColor = System.Drawing.Color.Transparent;
            this.pnlRightDecoration.Location = new System.Drawing.Point(1252, 90);
            this.pnlRightDecoration.Name = "pnlRightDecoration";
            this.pnlRightDecoration.Size = new System.Drawing.Size(214, 400);
            this.pnlRightDecoration.TabIndex = 3;
            this.pnlRightDecoration.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlRightDecoration_Paint);
            // 
            // pnlRoleSelect
            // 
            this.pnlRoleSelect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlRoleSelect.BackColor = System.Drawing.Color.Transparent;
            this.pnlRoleSelect.Controls.Add(this.roleGroupBox);
            this.pnlRoleSelect.Location = new System.Drawing.Point(320, 80);
            this.pnlRoleSelect.Name = "pnlRoleSelect";
            this.pnlRoleSelect.Size = new System.Drawing.Size(800, 600);
            this.pnlRoleSelect.TabIndex = 1;
            this.pnlRoleSelect.Visible = false;
            // 
            // roleGroupBox
            // 
            this.roleGroupBox.BackColor = System.Drawing.Color.White;
            this.roleGroupBox.Controls.Add(this.lblSelectRole);
            this.roleGroupBox.Controls.Add(this.btnStudent);
            this.roleGroupBox.Controls.Add(this.btnLecturer);
            this.roleGroupBox.Controls.Add(this.btnCloseRolePanel);
            this.roleGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roleGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.roleGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.roleGroupBox.Location = new System.Drawing.Point(0, 0);
            this.roleGroupBox.Name = "roleGroupBox";
            this.roleGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.roleGroupBox.Size = new System.Drawing.Size(800, 600);
            this.roleGroupBox.TabIndex = 0;
            this.roleGroupBox.TabStop = false;
            this.roleGroupBox.Text = "בחירת תפקיד";
            // 
            // lblSelectRole
            // 
            this.lblSelectRole.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSelectRole.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblSelectRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.lblSelectRole.Location = new System.Drawing.Point(30, 56);
            this.lblSelectRole.Name = "lblSelectRole";
            this.lblSelectRole.Size = new System.Drawing.Size(740, 80);
            this.lblSelectRole.TabIndex = 0;
            this.lblSelectRole.Text = "בחר את התפקיד";
            this.lblSelectRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStudent
            // 
            this.btnStudent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStudent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnStudent.FlatAppearance.BorderSize = 0;
            this.btnStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudent.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btnStudent.ForeColor = System.Drawing.Color.White;
            this.btnStudent.Location = new System.Drawing.Point(200, 180);
            this.btnStudent.Name = "btnStudent";
            this.btnStudent.Size = new System.Drawing.Size(400, 100);
            this.btnStudent.TabIndex = 1;
            this.btnStudent.Text = "🎓 סטודנט";
            this.btnStudent.UseVisualStyleBackColor = false;
            this.btnStudent.Click += new System.EventHandler(this.btnStudent_Click);
            // 
            // btnLecturer
            // 
            this.btnLecturer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLecturer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnLecturer.FlatAppearance.BorderSize = 0;
            this.btnLecturer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLecturer.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btnLecturer.ForeColor = System.Drawing.Color.White;
            this.btnLecturer.Location = new System.Drawing.Point(200, 300);
            this.btnLecturer.Name = "btnLecturer";
            this.btnLecturer.Size = new System.Drawing.Size(400, 100);
            this.btnLecturer.TabIndex = 2;
            this.btnLecturer.Text = "👨‍🏫 מרצה";
            this.btnLecturer.UseVisualStyleBackColor = false;
            this.btnLecturer.Click += new System.EventHandler(this.btnLecturer_Click);
            // 
            // btnCloseRolePanel
            // 
            this.btnCloseRolePanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCloseRolePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnCloseRolePanel.FlatAppearance.BorderSize = 0;
            this.btnCloseRolePanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseRolePanel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnCloseRolePanel.ForeColor = System.Drawing.Color.White;
            this.btnCloseRolePanel.Location = new System.Drawing.Point(300, 450);
            this.btnCloseRolePanel.Name = "btnCloseRolePanel";
            this.btnCloseRolePanel.Size = new System.Drawing.Size(200, 60);
            this.btnCloseRolePanel.TabIndex = 3;
            this.btnCloseRolePanel.Text = "🔙 חזרה";
            this.btnCloseRolePanel.UseVisualStyleBackColor = false;
            this.btnCloseRolePanel.Click += new System.EventHandler(this.btnCloseRolePanel_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1440, 900);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StartForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "מערכת בחינות חכמה";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.welcomeGroupBox.ResumeLayout(false);
            this.actionsGroupBox.ResumeLayout(false);
            this.pnlRoleSelect.ResumeLayout(false);
            this.roleGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlLeftDecoration;
        private System.Windows.Forms.Panel pnlRightDecoration;
        private System.Windows.Forms.GroupBox welcomeGroupBox;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.GroupBox actionsGroupBox;
        private System.Windows.Forms.Button btnExistingUser;
        private System.Windows.Forms.Button btnNewUser;
        private System.Windows.Forms.Panel pnlRoleSelect;
        private System.Windows.Forms.GroupBox roleGroupBox;
        private System.Windows.Forms.Label lblSelectRole;
        private System.Windows.Forms.Button btnStudent;
        private System.Windows.Forms.Button btnLecturer;
        private System.Windows.Forms.Button btnCloseRolePanel;

        // Modern geometric decoration paint events
        private void pnlLeftDecoration_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create modern geometric pattern with education theme
            // Gradient background circles
            System.Drawing.Drawing2D.LinearGradientBrush gradientBrush1 =
                new System.Drawing.Drawing2D.LinearGradientBrush(
                    new System.Drawing.Point(0, 0),
                    new System.Drawing.Point(200, 200),
                    System.Drawing.Color.FromArgb(80, 99, 102, 241),
                    System.Drawing.Color.FromArgb(40, 16, 185, 129));

            System.Drawing.Drawing2D.LinearGradientBrush gradientBrush2 =
                new System.Drawing.Drawing2D.LinearGradientBrush(
                    new System.Drawing.Point(0, 100),
                    new System.Drawing.Point(150, 300),
                    System.Drawing.Color.FromArgb(60, 245, 158, 11),
                    System.Drawing.Color.FromArgb(30, 99, 102, 241));

            // Large decorative circles
            g.FillEllipse(gradientBrush1, 20, 50, 120, 120);
            g.FillEllipse(gradientBrush2, 80, 200, 100, 100);

            // Smaller accent circles
            System.Drawing.SolidBrush accentBrush1 = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(100, 16, 185, 129));
            System.Drawing.SolidBrush accentBrush2 = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(80, 245, 158, 11));

            g.FillEllipse(accentBrush1, 150, 80, 40, 40);
            g.FillEllipse(accentBrush2, 10, 220, 35, 35);
            g.FillEllipse(accentBrush1, 130, 280, 25, 25);

            // Modern book/knowledge symbols
            System.Drawing.SolidBrush symbolBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(150, 51, 51, 76));
            System.Drawing.Font symbolFont = new System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold);

            g.DrawString("📚", symbolFont, symbolBrush, 40, 70);
            g.DrawString("🎓", symbolFont, symbolBrush, 110, 240);
            g.DrawString("✏️", symbolFont, symbolBrush, 160, 150);

            // Abstract geometric lines
            System.Drawing.Pen linePen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(60, 99, 102, 241), 3);
            linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            g.DrawArc(linePen, 30, 120, 80, 80, 45, 90);
            g.DrawArc(linePen, 100, 180, 60, 60, 180, 120);

            // Dispose resources
            gradientBrush1.Dispose();
            gradientBrush2.Dispose();
            accentBrush1.Dispose();
            accentBrush2.Dispose();
            symbolBrush.Dispose();
            symbolFont.Dispose();
            linePen.Dispose();
        }

        private void pnlRightDecoration_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create complementary geometric pattern for right side
            // Gradient background shapes
            System.Drawing.Drawing2D.LinearGradientBrush gradientBrush1 =
                new System.Drawing.Drawing2D.LinearGradientBrush(
                    new System.Drawing.Point(200, 0),
                    new System.Drawing.Point(0, 200),
                    System.Drawing.Color.FromArgb(80, 245, 158, 11),
                    System.Drawing.Color.FromArgb(40, 99, 102, 241));

            System.Drawing.Drawing2D.LinearGradientBrush gradientBrush2 =
                new System.Drawing.Drawing2D.LinearGradientBrush(
                    new System.Drawing.Point(150, 100),
                    new System.Drawing.Point(50, 300),
                    System.Drawing.Color.FromArgb(60, 16, 185, 129),
                    System.Drawing.Color.FromArgb(30, 245, 158, 11));

            // Large decorative shapes
            g.FillEllipse(gradientBrush1, 60, 60, 110, 110);
            g.FillEllipse(gradientBrush2, 20, 220, 90, 90);

            // Hexagonal academic shapes
            System.Drawing.SolidBrush hexBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(100, 99, 102, 241));
            System.Drawing.Point[] hexPoints = {
                new System.Drawing.Point(50, 150),
                new System.Drawing.Point(70, 140),
                new System.Drawing.Point(90, 150),
                new System.Drawing.Point(90, 170),
                new System.Drawing.Point(70, 180),
                new System.Drawing.Point(50, 170)
            };
            g.FillPolygon(hexBrush, hexPoints);

            // Academic achievement symbols
            System.Drawing.SolidBrush symbolBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(150, 51, 51, 76));
            System.Drawing.Font symbolFont = new System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold);

            g.DrawString("🏆", symbolFont, symbolBrush, 80, 90);
            g.DrawString("📖", symbolFont, symbolBrush, 30, 250);
            g.DrawString("💡", symbolFont, symbolBrush, 140, 180);

            // Mathematical symbols in elegant style
            System.Drawing.Font mathFont = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold);
            System.Drawing.SolidBrush mathBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(120, 245, 158, 11));

            g.DrawString("∑", mathFont, mathBrush, 160, 50);
            g.DrawString("π", mathFont, mathBrush, 20, 120);
            g.DrawString("∞", mathFont, mathBrush, 150, 300);

            // Elegant connecting lines
            System.Drawing.Pen elegantPen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(80, 16, 185, 129), 2);
            elegantPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

            g.DrawCurve(elegantPen, new System.Drawing.Point[] {
                new System.Drawing.Point(100, 50),
                new System.Drawing.Point(80, 100),
                new System.Drawing.Point(120, 150),
                new System.Drawing.Point(90, 200)
            });

            // Small decorative dots pattern
            System.Drawing.SolidBrush dotBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(100, 99, 102, 241));
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    g.FillEllipse(dotBrush, 10 + i * 15, 320 + j * 15, 4, 4);
                }
            }

            // Dispose resources
            gradientBrush1.Dispose();
            gradientBrush2.Dispose();
            hexBrush.Dispose();
            symbolBrush.Dispose();
            symbolFont.Dispose();
            mathFont.Dispose();
            mathBrush.Dispose();
            elegantPen.Dispose();
            dotBrush.Dispose();
        }

        // Background decoration for entire main panel
        private void mainPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create scattered geometric elements across the background
            // Using fixed values instead of Random for consistency

            // Large background circles with very low opacity
            System.Drawing.SolidBrush bgCircleBrush1 = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(15, 99, 102, 241));
            System.Drawing.SolidBrush bgCircleBrush2 = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(15, 16, 185, 129));
            System.Drawing.SolidBrush bgCircleBrush3 = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(15, 245, 158, 11));

            // Top area circles
            g.FillEllipse(bgCircleBrush1, 200, 50, 180, 180);
            g.FillEllipse(bgCircleBrush2, 800, 80, 150, 150);
            g.FillEllipse(bgCircleBrush3, 1100, 40, 200, 200);

            // Middle area circles
            g.FillEllipse(bgCircleBrush2, 150, 300, 120, 120);
            g.FillEllipse(bgCircleBrush1, 900, 350, 160, 160);
            g.FillEllipse(bgCircleBrush3, 1200, 320, 140, 140);

            // Bottom area circles
            g.FillEllipse(bgCircleBrush3, 100, 550, 100, 100);
            g.FillEllipse(bgCircleBrush1, 600, 580, 130, 130);
            g.FillEllipse(bgCircleBrush2, 1000, 600, 110, 110);

            // Small accent circles scattered around (predefined positions)
            int[] xPositions = { 120, 340, 560, 780, 950, 1150, 250, 470, 690, 910, 1130, 180, 400, 620, 840, 1060, 300, 520, 740, 960, 1180, 160, 380, 600, 820 };
            int[] yPositions = { 80, 150, 220, 290, 360, 430, 500, 570, 640, 120, 190, 260, 330, 400, 470, 540, 610, 140, 210, 280, 350, 420, 490, 560, 630 };
            int[] sizes = { 25, 35, 45, 30, 40, 50, 25, 35, 45, 30, 40, 25, 35, 45, 30, 40, 50, 25, 35, 45, 30, 40, 25, 35, 45 };
            int[] colors = { 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0 };

            for (int i = 0; i < 25; i++)
            {
                System.Drawing.SolidBrush accentBrush;
                if (colors[i] == 0)
                    accentBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(8, 99, 102, 241));
                else if (colors[i] == 1)
                    accentBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(8, 16, 185, 129));
                else
                    accentBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(8, 245, 158, 11));

                g.FillEllipse(accentBrush, xPositions[i], yPositions[i], sizes[i], sizes[i]);
                accentBrush.Dispose();
            }

            // Geometric lines connecting elements
            System.Drawing.Pen connectionPen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(20, 99, 102, 241), 1);
            connectionPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            // Diagonal connection lines
            g.DrawLine(connectionPen, 250, 100, 350, 200);
            g.DrawLine(connectionPen, 850, 150, 950, 250);
            g.DrawLine(connectionPen, 200, 350, 300, 450);
            g.DrawLine(connectionPen, 1100, 400, 1200, 500);

            // Curved connection lines
            g.DrawArc(connectionPen, 500, 200, 200, 100, 0, 180);
            g.DrawArc(connectionPen, 700, 450, 150, 80, 180, 180);

            // Mathematical and educational symbols scattered very subtly
            System.Drawing.Font subtleFont = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            System.Drawing.SolidBrush subtleBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(25, 51, 51, 76));

            // Top area symbols
            g.DrawString("📚", subtleFont, subtleBrush, 300, 120);
            g.DrawString("∑", subtleFont, subtleBrush, 500, 80);
            g.DrawString("🎓", subtleFont, subtleBrush, 750, 140);
            g.DrawString("π", subtleFont, subtleBrush, 950, 100);
            g.DrawString("💡", subtleFont, subtleBrush, 1150, 120);

            // Middle area symbols
            g.DrawString("📖", subtleFont, subtleBrush, 180, 380);
            g.DrawString("∫", subtleFont, subtleBrush, 400, 420);
            g.DrawString("✏️", subtleFont, subtleBrush, 650, 360);
            g.DrawString("∞", subtleFont, subtleBrush, 850, 440);
            g.DrawString("🏆", subtleFont, subtleBrush, 1100, 380);

            // Bottom area symbols
            g.DrawString("📐", subtleFont, subtleBrush, 200, 600);
            g.DrawString("α", subtleFont, subtleBrush, 450, 640);
            g.DrawString("🔬", subtleFont, subtleBrush, 700, 620);
            g.DrawString("β", subtleFont, subtleBrush, 900, 650);
            g.DrawString("🧮", subtleFont, subtleBrush, 1150, 610);

            // Hexagonal patterns in corners
            System.Drawing.SolidBrush hexBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(12, 99, 102, 241));

            // Top-left hexagon
            System.Drawing.Point[] hexPoints1 = {
                new System.Drawing.Point(80, 100),
                new System.Drawing.Point(100, 90),
                new System.Drawing.Point(120, 100),
                new System.Drawing.Point(120, 120),
                new System.Drawing.Point(100, 130),
                new System.Drawing.Point(80, 120)
            };
            g.FillPolygon(hexBrush, hexPoints1);

            // Top-right hexagon
            System.Drawing.Point[] hexPoints2 = {
                new System.Drawing.Point(1300, 80),
                new System.Drawing.Point(1320, 70),
                new System.Drawing.Point(1340, 80),
                new System.Drawing.Point(1340, 100),
                new System.Drawing.Point(1320, 110),
                new System.Drawing.Point(1300, 100)
            };
            g.FillPolygon(hexBrush, hexPoints2);

            // Bottom hexagons
            System.Drawing.Point[] hexPoints3 = {
                new System.Drawing.Point(100, 650),
                new System.Drawing.Point(120, 640),
                new System.Drawing.Point(140, 650),
                new System.Drawing.Point(140, 670),
                new System.Drawing.Point(120, 680),
                new System.Drawing.Point(100, 670)
            };
            g.FillPolygon(hexBrush, hexPoints3);

            System.Drawing.Point[] hexPoints4 = {
                new System.Drawing.Point(1280, 630),
                new System.Drawing.Point(1300, 620),
                new System.Drawing.Point(1320, 630),
                new System.Drawing.Point(1320, 650),
                new System.Drawing.Point(1300, 660),
                new System.Drawing.Point(1280, 650)
            };
            g.FillPolygon(hexBrush, hexPoints4);

            // Dispose all brushes and pens
            bgCircleBrush1.Dispose();
            bgCircleBrush2.Dispose();
            bgCircleBrush3.Dispose();
            connectionPen.Dispose();
            subtleFont.Dispose();
            subtleBrush.Dispose();
            hexBrush.Dispose();
        }
    }
}