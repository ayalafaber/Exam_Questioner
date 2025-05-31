namespace Exam_Questioner
{
    partial class LecturerReviewsForm
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
            this.btnBack = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.reviewGroupBox = new System.Windows.Forms.GroupBox();
            this.lblStudent = new System.Windows.Forms.Label();
            this.cmbStudents = new System.Windows.Forms.ComboBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtReviewMessage = new System.Windows.Forms.TextBox();
            this.ratingGroupBox = new System.Windows.Forms.GroupBox();
            this.rbStar = new System.Windows.Forms.RadioButton();
            this.rbHappy = new System.Windows.Forms.RadioButton();
            this.rbSad = new System.Windows.Forms.RadioButton();
            this.actionsGroupBox = new System.Windows.Forms.GroupBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.reviewGroupBox.SuspendLayout();
            this.ratingGroupBox.SuspendLayout();
            this.actionsGroupBox.SuspendLayout();
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
            this.headerPanel.Size = new System.Drawing.Size(1200, 120);
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
            this.btnBack.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnBack.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(544, 23);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(472, 62);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "📝 ביקורת סטודנטים";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.subtitleLabel.Location = new System.Drawing.Point(581, 85);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(394, 32);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "כתוב ביקורת והערות לסטודנטים שלך";
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.mainPanel.Controls.Add(this.reviewGroupBox);
            this.mainPanel.Controls.Add(this.ratingGroupBox);
            this.mainPanel.Controls.Add(this.actionsGroupBox);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 120);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(80, 30, 30, 30);
            this.mainPanel.Size = new System.Drawing.Size(1200, 580);
            this.mainPanel.TabIndex = 1;
            // 
            // reviewGroupBox
            // 
            this.reviewGroupBox.BackColor = System.Drawing.Color.White;
            this.reviewGroupBox.Controls.Add(this.lblStudent);
            this.reviewGroupBox.Controls.Add(this.cmbStudents);
            this.reviewGroupBox.Controls.Add(this.lblMessage);
            this.reviewGroupBox.Controls.Add(this.txtReviewMessage);
            this.reviewGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reviewGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.reviewGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.reviewGroupBox.Location = new System.Drawing.Point(127, 88);
            this.reviewGroupBox.Name = "reviewGroupBox";
            this.reviewGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.reviewGroupBox.Size = new System.Drawing.Size(950, 339);
            this.reviewGroupBox.TabIndex = 0;
            this.reviewGroupBox.TabStop = false;
            this.reviewGroupBox.Text = "✍️ כתיבת ביקורת";
            // 
            // lblStudent
            // 
            this.lblStudent.AutoSize = true;
            this.lblStudent.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblStudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblStudent.Location = new System.Drawing.Point(730, 50);
            this.lblStudent.Name = "lblStudent";
            this.lblStudent.Size = new System.Drawing.Size(196, 32);
            this.lblStudent.TabIndex = 0;
            this.lblStudent.Text = "👤 בחר סטודנט:";
            // 
            // cmbStudents
            // 
            this.cmbStudents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStudents.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbStudents.FormattingEnabled = true;
            this.cmbStudents.Location = new System.Drawing.Point(30, 85);
            this.cmbStudents.Name = "cmbStudents";
            this.cmbStudents.Size = new System.Drawing.Size(890, 36);
            this.cmbStudents.TabIndex = 1;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblMessage.Location = new System.Drawing.Point(708, 140);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(218, 32);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "💬 הודעת ביקורת:";
            // 
            // txtReviewMessage
            // 
            this.txtReviewMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtReviewMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReviewMessage.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtReviewMessage.Location = new System.Drawing.Point(30, 175);
            this.txtReviewMessage.Multiline = true;
            this.txtReviewMessage.Name = "txtReviewMessage";
            this.txtReviewMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReviewMessage.Size = new System.Drawing.Size(890, 120);
            this.txtReviewMessage.TabIndex = 3;
            // 
            // ratingGroupBox
            // 
            this.ratingGroupBox.BackColor = System.Drawing.Color.White;
            this.ratingGroupBox.Controls.Add(this.rbStar);
            this.ratingGroupBox.Controls.Add(this.rbHappy);
            this.ratingGroupBox.Controls.Add(this.rbSad);
            this.ratingGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ratingGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.ratingGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.ratingGroupBox.Location = new System.Drawing.Point(127, 470);
            this.ratingGroupBox.Name = "ratingGroupBox";
            this.ratingGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.ratingGroupBox.Size = new System.Drawing.Size(1235, 180);
            this.ratingGroupBox.TabIndex = 1;
            this.ratingGroupBox.TabStop = false;
            this.ratingGroupBox.Text = "⭐ בחר דירוג";
            // 
            // rbStar
            // 
            this.rbStar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.rbStar.FlatAppearance.BorderSize = 0;
            this.rbStar.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.rbStar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbStar.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.rbStar.ForeColor = System.Drawing.Color.White;
            this.rbStar.Location = new System.Drawing.Point(232, 90);
            this.rbStar.Name = "rbStar";
            this.rbStar.Size = new System.Drawing.Size(180, 80);
            this.rbStar.TabIndex = 1;
            this.rbStar.Text = "⭐\r\nמצוין";
            this.rbStar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbStar.UseVisualStyleBackColor = false;
            this.rbStar.CheckedChanged += new System.EventHandler(this.RatingButton_CheckedChanged);
            this.rbStar.MouseEnter += new System.EventHandler(this.RatingButton_MouseEnter);
            this.rbStar.MouseLeave += new System.EventHandler(this.RatingButton_MouseLeave);
            // 
            // rbHappy
            // 
            this.rbHappy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.rbHappy.FlatAppearance.BorderSize = 0;
            this.rbHappy.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.rbHappy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbHappy.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.rbHappy.ForeColor = System.Drawing.Color.White;
            this.rbHappy.Location = new System.Drawing.Point(568, 90);
            this.rbHappy.Name = "rbHappy";
            this.rbHappy.Size = new System.Drawing.Size(180, 80);
            this.rbHappy.TabIndex = 2;
            this.rbHappy.Text = "😊\r\nטוב";
            this.rbHappy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbHappy.UseVisualStyleBackColor = false;
            this.rbHappy.CheckedChanged += new System.EventHandler(this.RatingButton_CheckedChanged);
            this.rbHappy.MouseEnter += new System.EventHandler(this.RatingButton_MouseEnter);
            this.rbHappy.MouseLeave += new System.EventHandler(this.RatingButton_MouseLeave);
            // 
            // rbSad
            // 
            this.rbSad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.rbSad.FlatAppearance.BorderSize = 0;
            this.rbSad.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.rbSad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbSad.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.rbSad.ForeColor = System.Drawing.Color.White;
            this.rbSad.Location = new System.Drawing.Point(898, 90);
            this.rbSad.Name = "rbSad";
            this.rbSad.Size = new System.Drawing.Size(180, 80);
            this.rbSad.TabIndex = 3;
            this.rbSad.Text = "😞\r\nצריך שיפור";
            this.rbSad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbSad.UseVisualStyleBackColor = false;
            this.rbSad.CheckedChanged += new System.EventHandler(this.RatingButton_CheckedChanged);
            this.rbSad.MouseEnter += new System.EventHandler(this.RatingButton_MouseEnter);
            this.rbSad.MouseLeave += new System.EventHandler(this.RatingButton_MouseLeave);
            // 
            // actionsGroupBox
            // 
            this.actionsGroupBox.BackColor = System.Drawing.Color.White;
            this.actionsGroupBox.Controls.Add(this.btnSubmit);
            this.actionsGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actionsGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.actionsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.actionsGroupBox.Location = new System.Drawing.Point(1122, 88);
            this.actionsGroupBox.Name = "actionsGroupBox";
            this.actionsGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.actionsGroupBox.Size = new System.Drawing.Size(240, 339);
            this.actionsGroupBox.TabIndex = 2;
            this.actionsGroupBox.TabStop = false;
            this.actionsGroupBox.Text = "🚀 שליחה";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(27, 105);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(180, 120);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "🚀\r\n\r\nשלח ביקורת";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            this.btnSubmit.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnSubmit.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // LecturerReviewsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "LecturerReviewsForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ביקורת סטודנטים - מרצה";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LecturerReviewsForm_Load);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.reviewGroupBox.ResumeLayout(false);
            this.reviewGroupBox.PerformLayout();
            this.ratingGroupBox.ResumeLayout(false);
            this.actionsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox reviewGroupBox;
        private System.Windows.Forms.Label lblStudent;
        private System.Windows.Forms.ComboBox cmbStudents;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtReviewMessage;
        private System.Windows.Forms.GroupBox ratingGroupBox;
        private System.Windows.Forms.RadioButton rbStar;
        private System.Windows.Forms.RadioButton rbHappy;
        private System.Windows.Forms.RadioButton rbSad;
        private System.Windows.Forms.GroupBox actionsGroupBox;
        private System.Windows.Forms.Button btnSubmit;

        // Event handlers with updated colors
        private void btnBack_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Button_MouseEnter(object sender, System.EventArgs e)
        {
            if (sender is System.Windows.Forms.Button btn)
            {
                if (btn == btnBack)
                    btn.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
                else if (btn == btnSubmit)
                    btn.BackColor = System.Drawing.Color.FromArgb(5, 150, 105);
            }
        }

        private void Button_MouseLeave(object sender, System.EventArgs e)
        {
            if (sender is System.Windows.Forms.Button btn)
            {
                if (btn == btnBack)
                    btn.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
                else if (btn == btnSubmit)
                    btn.BackColor = System.Drawing.Color.RoyalBlue;
            }
        }

        private void RatingButton_MouseEnter(object sender, System.EventArgs e)
        {
            if (sender is System.Windows.Forms.RadioButton btn && !btn.Checked)
            {
                if (btn == rbStar)
                    btn.BackColor = System.Drawing.Color.FromArgb(217, 119, 6); // Darker gold
                else if (btn == rbHappy)
                    btn.BackColor = System.Drawing.Color.FromArgb(5, 150, 105); // Darker green
                else if (btn == rbSad)
                    btn.BackColor = System.Drawing.Color.FromArgb(220, 38, 38); // Darker red
            }
        }

        private void RatingButton_MouseLeave(object sender, System.EventArgs e)
        {
            if (sender is System.Windows.Forms.RadioButton btn && !btn.Checked)
            {
                if (btn == rbStar)
                    btn.BackColor = System.Drawing.Color.FromArgb(245, 158, 11); // Gold/Yellow
                else if (btn == rbHappy)
                    btn.BackColor = System.Drawing.Color.FromArgb(16, 185, 129); // Green
                else if (btn == rbSad)
                    btn.BackColor = System.Drawing.Color.FromArgb(239, 68, 68); // Red
            }
        }

        private void RatingButton_CheckedChanged(object sender, System.EventArgs e)
        {
            if (sender is System.Windows.Forms.RadioButton btn)
            {
                // Reset all buttons to normal colors (updated colors)
                rbStar.BackColor = System.Drawing.Color.FromArgb(245, 158, 11);  // Gold/Yellow
                rbHappy.BackColor = System.Drawing.Color.FromArgb(16, 185, 129); // Green
                rbSad.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);    // Red

                // Highlight the checked button with darker shade
                if (btn.Checked)
                {
                    if (btn == rbStar)
                        btn.BackColor = System.Drawing.Color.FromArgb(217, 119, 6); // Darker gold
                    else if (btn == rbHappy)
                        btn.BackColor = System.Drawing.Color.FromArgb(5, 150, 105); // Darker green
                    else if (btn == rbSad)
                        btn.BackColor = System.Drawing.Color.FromArgb(220, 38, 38); // Darker red
                }
            }
        }
    }
}