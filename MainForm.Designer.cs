namespace Exam_Questioner
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel educationalPanel; // Panel for educational decorations

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.welcomeGroupBox = new System.Windows.Forms.GroupBox();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.pnlStudent = new System.Windows.Forms.Panel();
            this.studentGroupBox = new System.Windows.Forms.GroupBox();
            this.btnExams = new System.Windows.Forms.Button();
            this.btnGrades = new System.Windows.Forms.Button();
            this.btnStudentReviews = new System.Windows.Forms.Button(); // כפתור חדש לסטודנט
            this.pnlLecturer = new System.Windows.Forms.Panel();
            this.lecturerGroupBox = new System.Windows.Forms.GroupBox();
            this.btnCreateExam = new System.Windows.Forms.Button();
            this.btnStudentStats = new System.Windows.Forms.Button();
            this.btnLecturerReviews = new System.Windows.Forms.Button(); // כפתור חדש למרצה
            this.educationalPanel = new System.Windows.Forms.Panel();
            this.headerPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.welcomeGroupBox.SuspendLayout();
            this.pnlStudent.SuspendLayout();
            this.studentGroupBox.SuspendLayout();
            this.pnlLecturer.SuspendLayout();
            this.lecturerGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.headerPanel.Controls.Add(this.lblConnectionStatus);
            this.headerPanel.Controls.Add(this.btnLogout);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.subtitleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1440, 120);
            this.headerPanel.TabIndex = 0;
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblConnectionStatus.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.White;
            this.lblConnectionStatus.Location = new System.Drawing.Point(1280, 15);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            this.lblConnectionStatus.Size = new System.Drawing.Size(124, 41);
            this.lblConnectionStatus.TabIndex = 0;
            this.lblConnectionStatus.Text = "🟢 מחובר";
            this.lblConnectionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblConnectionStatus.Click += new System.EventHandler(this.lblConnectionStatus_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(30, 15);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(130, 45);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "🚪 התנתק";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            this.btnLogout.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnLogout.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(617, 25);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(548, 62);
            this.titleLabel.TabIndex = 2;
            this.titleLabel.Text = "🎓 מערכת בחינות חכמה";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.subtitleLabel.Location = new System.Drawing.Point(675, 87);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(418, 30);
            this.subtitleLabel.TabIndex = 3;
            this.subtitleLabel.Text = "פלטפורמה מתקדמת לניהול ויצירת מבחנים";
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.mainPanel.Controls.Add(this.welcomeGroupBox);
            this.mainPanel.Controls.Add(this.pnlStudent);
            this.mainPanel.Controls.Add(this.pnlLecturer);
            this.mainPanel.Controls.Add(this.educationalPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 120);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(80, 40, 40, 40);
            this.mainPanel.Size = new System.Drawing.Size(1440, 780);
            this.mainPanel.TabIndex = 1;
            // 
            // welcomeGroupBox
            // 
            this.welcomeGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.welcomeGroupBox.BackColor = System.Drawing.Color.White;
            this.welcomeGroupBox.Controls.Add(this.lblWelcome);
            this.welcomeGroupBox.Controls.Add(this.lblUserRole);
            this.welcomeGroupBox.Controls.Add(this.lblDescription);
            this.welcomeGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.welcomeGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.welcomeGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.welcomeGroupBox.Location = new System.Drawing.Point(320, 43);
            this.welcomeGroupBox.Name = "welcomeGroupBox";
            this.welcomeGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.welcomeGroupBox.Size = new System.Drawing.Size(800, 185);
            this.welcomeGroupBox.TabIndex = 0;
            this.welcomeGroupBox.TabStop = false;
            this.welcomeGroupBox.Text = "💫 ברוכים הבאים";
            this.welcomeGroupBox.Enter += new System.EventHandler(this.welcomeGroupBox_Enter);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblWelcome.Location = new System.Drawing.Point(323, 42);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(154, 62);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "שלום!";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUserRole
            // 
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblUserRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.lblUserRole.Location = new System.Drawing.Point(308, 106);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(183, 37);
            this.lblUserRole.TabIndex = 1;
            this.lblUserRole.Text = "מחובר בתור...";
            this.lblUserRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblDescription.Location = new System.Drawing.Point(196, 143);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(475, 28);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "בחרו את הפעולה הרצויה מהאפשרויות המוצגות למטה";
            // 
            // pnlStudent
            // 
            this.pnlStudent.Controls.Add(this.studentGroupBox);
            this.pnlStudent.Location = new System.Drawing.Point(220, 250);
            this.pnlStudent.Name = "pnlStudent";
            this.pnlStudent.Size = new System.Drawing.Size(1000, 340);
            this.pnlStudent.TabIndex = 1;
            this.pnlStudent.Visible = false;
            // 
            // studentGroupBox
            // 
            this.studentGroupBox.BackColor = System.Drawing.Color.White;
            this.studentGroupBox.Controls.Add(this.btnExams);
            this.studentGroupBox.Controls.Add(this.btnGrades);
            this.studentGroupBox.Controls.Add(this.btnStudentReviews);
            this.studentGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.studentGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.studentGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.studentGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.studentGroupBox.Location = new System.Drawing.Point(0, 0);
            this.studentGroupBox.Name = "studentGroupBox";
            this.studentGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.studentGroupBox.Size = new System.Drawing.Size(1000, 340);
            this.studentGroupBox.TabIndex = 0;
            this.studentGroupBox.TabStop = false;
            this.studentGroupBox.Text = "🎯 לוח הבקרה - סטודנט";
            this.studentGroupBox.Enter += new System.EventHandler(this.studentGroupBox_Enter);
            // 
            // btnExams
            // 
            this.btnExams.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnExams.FlatAppearance.BorderSize = 0;
            this.btnExams.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExams.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnExams.ForeColor = System.Drawing.Color.White;
            this.btnExams.Location = new System.Drawing.Point(50, 80);
            this.btnExams.Name = "btnExams";
            this.btnExams.Size = new System.Drawing.Size(280, 180);
            this.btnExams.TabIndex = 0;
            this.btnExams.Text = "🎯 מבחנים\r\n\r\nתרגול שאלות\r\nוביצוע מבחנים חדשים";
            this.btnExams.UseVisualStyleBackColor = false;
            this.btnExams.Click += new System.EventHandler(this.btnExams_Click);
            this.btnExams.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnExams.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // btnGrades
            // 
            this.btnGrades.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnGrades.FlatAppearance.BorderSize = 0;
            this.btnGrades.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrades.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnGrades.ForeColor = System.Drawing.Color.White;
            this.btnGrades.Location = new System.Drawing.Point(360, 80);
            this.btnGrades.Name = "btnGrades";
            this.btnGrades.Size = new System.Drawing.Size(280, 180);
            this.btnGrades.TabIndex = 1;
            this.btnGrades.Text = "📊 ציונים\r\n\r\nצפייה בציונים אישיים\r\nומעקב אחר התקדמות";
            this.btnGrades.UseVisualStyleBackColor = false;
            this.btnGrades.Click += new System.EventHandler(this.btnGrades_Click);
            this.btnGrades.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnGrades.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // btnStudentReviews
            // 
            this.btnStudentReviews.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(72)))), ((int)(((byte)(153)))));
            this.btnStudentReviews.FlatAppearance.BorderSize = 0;
            this.btnStudentReviews.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudentReviews.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnStudentReviews.ForeColor = System.Drawing.Color.White;
            this.btnStudentReviews.Location = new System.Drawing.Point(670, 80);
            this.btnStudentReviews.Name = "btnStudentReviews";
            this.btnStudentReviews.Size = new System.Drawing.Size(280, 180);
            this.btnStudentReviews.TabIndex = 2;
            this.btnStudentReviews.Text = "⭐ הביקורות שלי\r\n\r\nצפייה בביקורות\r\nמהמרצים";
            this.btnStudentReviews.UseVisualStyleBackColor = false;
            this.btnStudentReviews.Click += new System.EventHandler(this.btnStudentReviews_Click);
            this.btnStudentReviews.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnStudentReviews.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // pnlLecturer
            // 
            this.pnlLecturer.Controls.Add(this.lecturerGroupBox);
            this.pnlLecturer.Location = new System.Drawing.Point(220, 250);
            this.pnlLecturer.Name = "pnlLecturer";
            this.pnlLecturer.Size = new System.Drawing.Size(1000, 340);
            this.pnlLecturer.TabIndex = 2;
            this.pnlLecturer.Visible = false;
            // 
            // lecturerGroupBox
            // 
            this.lecturerGroupBox.BackColor = System.Drawing.Color.White;
            this.lecturerGroupBox.Controls.Add(this.btnCreateExam);
            this.lecturerGroupBox.Controls.Add(this.btnStudentStats);
            this.lecturerGroupBox.Controls.Add(this.btnLecturerReviews);
            this.lecturerGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lecturerGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lecturerGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lecturerGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lecturerGroupBox.Location = new System.Drawing.Point(0, 0);
            this.lecturerGroupBox.Name = "lecturerGroupBox";
            this.lecturerGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.lecturerGroupBox.Size = new System.Drawing.Size(1000, 340);
            this.lecturerGroupBox.TabIndex = 0;
            this.lecturerGroupBox.TabStop = false;
            this.lecturerGroupBox.Text = "👨‍🏫 לוח הבקרה - מרצה";
            // 
            // btnCreateExam
            // 
            this.btnCreateExam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(115)))), ((int)(((byte)(22)))));
            this.btnCreateExam.FlatAppearance.BorderSize = 0;
            this.btnCreateExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateExam.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnCreateExam.ForeColor = System.Drawing.Color.White;
            this.btnCreateExam.Location = new System.Drawing.Point(50, 80);
            this.btnCreateExam.Name = "btnCreateExam";
            this.btnCreateExam.Size = new System.Drawing.Size(280, 180);
            this.btnCreateExam.TabIndex = 0;
            this.btnCreateExam.Text = "📝 ניהול מבחנים\r\n\r\nיצירה, עריכה וניהול\r\nמבחנים ושאלות";
            this.btnCreateExam.UseVisualStyleBackColor = false;
            this.btnCreateExam.Click += new System.EventHandler(this.btnCreateExam_Click);
            this.btnCreateExam.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnCreateExam.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // btnStudentStats
            // 
            this.btnStudentStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(85)))), ((int)(((byte)(247)))));
            this.btnStudentStats.FlatAppearance.BorderSize = 0;
            this.btnStudentStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudentStats.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnStudentStats.ForeColor = System.Drawing.Color.White;
            this.btnStudentStats.Location = new System.Drawing.Point(360, 80);
            this.btnStudentStats.Name = "btnStudentStats";
            this.btnStudentStats.Size = new System.Drawing.Size(280, 180);
            this.btnStudentStats.TabIndex = 1;
            this.btnStudentStats.Text = "👥 מעקב ציונים\r\n\r\nצפייה וניתוח נתוני\r\nביצועי הסטודנטים";
            this.btnStudentStats.UseVisualStyleBackColor = false;
            this.btnStudentStats.Click += new System.EventHandler(this.btnStudentStats_Click);
            this.btnStudentStats.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnStudentStats.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // btnLecturerReviews
            // 
            this.btnLecturerReviews.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(127)))));
            this.btnLecturerReviews.FlatAppearance.BorderSize = 0;
            this.btnLecturerReviews.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLecturerReviews.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnLecturerReviews.ForeColor = System.Drawing.Color.White;
            this.btnLecturerReviews.Location = new System.Drawing.Point(670, 80);
            this.btnLecturerReviews.Name = "btnLecturerReviews";
            this.btnLecturerReviews.Size = new System.Drawing.Size(280, 180);
            this.btnLecturerReviews.TabIndex = 2;
            this.btnLecturerReviews.Text = "📝 ביקורת סטודנטים\r\n\r\nכתיבת ביקורות\r\nועדכון הערות";
            this.btnLecturerReviews.UseVisualStyleBackColor = false;
            this.btnLecturerReviews.Click += new System.EventHandler(this.btnLecturerReviews_Click);
            this.btnLecturerReviews.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnLecturerReviews.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // educationalPanel
            // 
            this.educationalPanel.BackColor = System.Drawing.Color.Transparent;
            this.educationalPanel.Location = new System.Drawing.Point(0, 629);
            this.educationalPanel.Name = "educationalPanel";
            this.educationalPanel.Size = new System.Drawing.Size(1478, 111);
            this.educationalPanel.TabIndex = 3;
            this.educationalPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.educationalPanel_Paint);
            // 
            // MainForm
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
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "מערכת בחינות חכמה - דף ראשי";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.welcomeGroupBox.ResumeLayout(false);
            this.welcomeGroupBox.PerformLayout();
            this.pnlStudent.ResumeLayout(false);
            this.studentGroupBox.ResumeLayout(false);
            this.pnlLecturer.ResumeLayout(false);
            this.lecturerGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #region Component Declarations

        // Header components
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.Button btnLogout;

        // Main panel and welcome section
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox welcomeGroupBox;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.Label lblDescription;

        // Student panel
        private System.Windows.Forms.Panel pnlStudent;
        private System.Windows.Forms.GroupBox studentGroupBox;
        private System.Windows.Forms.Button btnExams;
        private System.Windows.Forms.Button btnGrades;
        private System.Windows.Forms.Button btnStudentReviews; // כפתור חדש

        // Lecturer panel
        private System.Windows.Forms.Panel pnlLecturer;
        private System.Windows.Forms.GroupBox lecturerGroupBox;
        private System.Windows.Forms.Button btnCreateExam;
        private System.Windows.Forms.Button btnStudentStats;
        private System.Windows.Forms.Button btnLecturerReviews; // כפתור חדש

        #endregion

        // Paint event for educational decorations
        private void educationalPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int panelWidth = this.educationalPanel.Width;
            int panelHeight = this.educationalPanel.Height;

            // Educational item colors - vibrant and diverse
            System.Drawing.Color[] itemColors = {
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
            };

            System.Random rand = new System.Random(42);

            // Create scattered confetti-like arrangement
            int totalItems = 45; // Total number of educational items

            for (int i = 0; i < totalItems; i++)
            {
                // Random position across the entire panel
                int x = rand.Next(10, panelWidth - 60);
                int y = rand.Next(5, panelHeight - 45);

                // Random color
                System.Drawing.Color itemColor = itemColors[rand.Next(itemColors.Length)];

                // Random rotation angle for variety
                float angle = rand.Next(0, 360);

                // Save graphics state
                var state = g.Save();

                // Apply rotation around the item center
                g.TranslateTransform(x + 25, y + 25);
                g.RotateTransform(angle);
                g.TranslateTransform(-25, -25);

                // Random item type
                int itemType = rand.Next(6);

                switch (itemType)
                {
                    case 0:
                        DrawCalculator(g, 0, 0, itemColor);
                        break;
                    case 1:
                        DrawGlobe(g, 0, 0, itemColor);
                        break;
                    case 2:
                        DrawMicroscope(g, 0, 0, itemColor);
                        break;
                    case 3:
                        DrawTestTube(g, 0, 0, itemColor);
                        break;
                    case 4:
                        DrawPencil(g, 0, 0, itemColor);
                        break;
                    case 5:
                        DrawRuler(g, 0, 0, itemColor);
                        break;
                }

                // Restore graphics state
                g.Restore(state);
            }

            // Add some extra small scattered elements for more confetti effect
            for (int i = 0; i < 20; i++)
            {
                int x = rand.Next(0, panelWidth);
                int y = rand.Next(0, panelHeight);
                System.Drawing.Color dotColor = itemColors[rand.Next(itemColors.Length)];

                using (System.Drawing.SolidBrush dotBrush = new System.Drawing.SolidBrush(dotColor))
                {
                    // Small dots and shapes
                    int shapeType = rand.Next(3);
                    switch (shapeType)
                    {
                        case 0: // Circle
                            g.FillEllipse(dotBrush, x, y, 4, 4);
                            break;
                        case 1: // Square
                            g.FillRectangle(dotBrush, x, y, 4, 4);
                            break;
                        case 2: // Triangle
                            System.Drawing.Point[] triangle = {
                                new System.Drawing.Point(x + 2, y),
                                new System.Drawing.Point(x, y + 4),
                                new System.Drawing.Point(x + 4, y + 4)
                            };
                            g.FillPolygon(dotBrush, triangle);
                            break;
                    }
                }
            }
        }

        private void DrawCalculator(System.Drawing.Graphics g, int x, int y, System.Drawing.Color color)
        {
            // Calculator body
            using (System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(color))
            {
                System.Drawing.Rectangle calcRect = new System.Drawing.Rectangle(x, y, 35, 45);
                g.FillRectangle(brush, calcRect);
            }

            // Screen
            using (System.Drawing.SolidBrush screenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(40, 40, 40)))
            {
                System.Drawing.Rectangle screenRect = new System.Drawing.Rectangle(x + 3, y + 3, 29, 12);
                g.FillRectangle(screenBrush, screenRect);
            }

            // Buttons (small squares)
            using (System.Drawing.SolidBrush buttonBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(220, 220, 220)))
            {
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        System.Drawing.Rectangle buttonRect = new System.Drawing.Rectangle(x + 5 + (col * 8), y + 18 + (row * 8), 6, 6);
                        g.FillRectangle(buttonBrush, buttonRect);
                    }
                }
            }

            // Outline
            using (System.Drawing.Pen outlinePen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(80, System.Drawing.Color.Black), 1))
            {
                System.Drawing.Rectangle outlineRect = new System.Drawing.Rectangle(x, y, 35, 45);
                g.DrawRectangle(outlinePen, outlineRect);
            }
        }

        private void DrawGlobe(System.Drawing.Graphics g, int x, int y, System.Drawing.Color color)
        {
            // Globe base
            using (System.Drawing.SolidBrush baseBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(139, 69, 19)))
            {
                System.Drawing.Rectangle baseRect = new System.Drawing.Rectangle(x, y + 25, 20, 8);
                g.FillRectangle(baseBrush, baseRect);
            }

            // Globe sphere
            using (System.Drawing.SolidBrush globeBrush = new System.Drawing.SolidBrush(color))
            {
                g.FillEllipse(globeBrush, x + 2, y, 16, 16);
            }

            // Continents (simple shapes)
            using (System.Drawing.SolidBrush continentBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(34, 139, 34)))
            {
                g.FillEllipse(continentBrush, x + 5, y + 3, 4, 3);
                g.FillEllipse(continentBrush, x + 11, y + 6, 3, 4);
                g.FillEllipse(continentBrush, x + 6, y + 10, 5, 2);
            }

            // Globe outline
            using (System.Drawing.Pen outlinePen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(80, System.Drawing.Color.Black), 1))
            {
                g.DrawEllipse(outlinePen, x + 2, y, 16, 16);
            }
        }

        private void DrawMicroscope(System.Drawing.Graphics g, int x, int y, System.Drawing.Color color)
        {
            // Base
            using (System.Drawing.SolidBrush baseBrush = new System.Drawing.SolidBrush(color))
            {
                System.Drawing.Rectangle baseRect = new System.Drawing.Rectangle(x, y + 35, 25, 8);
                g.FillRectangle(baseBrush, baseRect);
            }

            // Body
            using (System.Drawing.SolidBrush bodyBrush = new System.Drawing.SolidBrush(color))
            {
                System.Drawing.Rectangle bodyRect = new System.Drawing.Rectangle(x + 8, y + 15, 8, 20);
                g.FillRectangle(bodyBrush, bodyRect);
            }

            // Eyepiece
            using (System.Drawing.SolidBrush eyepieceBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(60, 60, 60)))
            {
                System.Drawing.Rectangle eyepieceRect = new System.Drawing.Rectangle(x + 10, y, 4, 15);
                g.FillRectangle(eyepieceBrush, eyepieceRect);
            }

            // Objective lens
            using (System.Drawing.SolidBrush objectiveBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(180, 180, 180)))
            {
                g.FillEllipse(objectiveBrush, x + 9, y + 30, 6, 4);
            }
        }

        private void DrawTestTube(System.Drawing.Graphics g, int x, int y, System.Drawing.Color color)
        {
            // Test tube body
            using (System.Drawing.SolidBrush tubeBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(240, 240, 240)))
            {
                System.Drawing.Rectangle tubeRect = new System.Drawing.Rectangle(x, y, 8, 30);
                g.FillRectangle(tubeBrush, tubeRect);
            }

            // Liquid inside
            using (System.Drawing.SolidBrush liquidBrush = new System.Drawing.SolidBrush(color))
            {
                System.Drawing.Rectangle liquidRect = new System.Drawing.Rectangle(x + 1, y + 15, 6, 14);
                g.FillRectangle(liquidBrush, liquidRect);
            }

            // Cork/stopper
            using (System.Drawing.SolidBrush corkBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(139, 69, 19)))
            {
                System.Drawing.Rectangle corkRect = new System.Drawing.Rectangle(x - 1, y - 3, 10, 5);
                g.FillRectangle(corkBrush, corkRect);
            }

            // Outline
            using (System.Drawing.Pen outlinePen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(120, System.Drawing.Color.Black), 1))
            {
                System.Drawing.Rectangle outlineRect = new System.Drawing.Rectangle(x, y, 8, 30);
                g.DrawRectangle(outlinePen, outlineRect);
            }
        }

        private void DrawPencil(System.Drawing.Graphics g, int x, int y, System.Drawing.Color color)
        {
            // Pencil body
            using (System.Drawing.SolidBrush pencilBrush = new System.Drawing.SolidBrush(color))
            {
                System.Drawing.Rectangle pencilRect = new System.Drawing.Rectangle(x, y, 40, 6);
                g.FillRectangle(pencilBrush, pencilRect);
            }

            // Pencil tip
            using (System.Drawing.SolidBrush tipBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(139, 69, 19)))
            {
                System.Drawing.Point[] tipPoints = {
                    new System.Drawing.Point(x + 40, y),
                    new System.Drawing.Point(x + 40, y + 6),
                    new System.Drawing.Point(x + 45, y + 3)
                };
                g.FillPolygon(tipBrush, tipPoints);
            }

            // Eraser
            using (System.Drawing.SolidBrush eraserBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 192, 203)))
            {
                System.Drawing.Rectangle eraserRect = new System.Drawing.Rectangle(x - 4, y + 1, 5, 4);
                g.FillRectangle(eraserBrush, eraserRect);
            }

            // Metal band
            using (System.Drawing.SolidBrush metalBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(192, 192, 192)))
            {
                System.Drawing.Rectangle metalRect = new System.Drawing.Rectangle(x - 2, y, 3, 6);
                g.FillRectangle(metalBrush, metalRect);
            }
        }

        private void DrawRuler(System.Drawing.Graphics g, int x, int y, System.Drawing.Color color)
        {
            // Ruler body
            using (System.Drawing.SolidBrush rulerBrush = new System.Drawing.SolidBrush(color))
            {
                System.Drawing.Rectangle rulerRect = new System.Drawing.Rectangle(x, y, 50, 4);
                g.FillRectangle(rulerBrush, rulerRect);
            }

            // Measurement marks
            using (System.Drawing.Pen markPen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(80, System.Drawing.Color.Black), 1))
            {
                for (int i = 0; i < 10; i++)
                {
                    int markX = x + (i * 5);
                    int markHeight = (i % 5 == 0) ? 3 : 1;
                    g.DrawLine(markPen, markX, y, markX, y + markHeight);
                }
            }

            // Outline
            using (System.Drawing.Pen outlinePen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(100, System.Drawing.Color.Black), 1))
            {
                System.Drawing.Rectangle outlineRect = new System.Drawing.Rectangle(x, y, 50, 4);
                g.DrawRectangle(outlinePen, outlineRect);
            }
        }

        private void pnlStudent_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Custom paint method preserved for compatibility
        }
    }
}