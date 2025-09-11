namespace Exam_Questioner
{
    partial class StudentReviewsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.reviewsGroupBox = new System.Windows.Forms.GroupBox();
            this.dataGridReviews = new System.Windows.Forms.DataGridView();
            this.lblNoReviews = new System.Windows.Forms.Label();
            this.actionsGroupBox = new System.Windows.Forms.GroupBox();
            this.summaryPanel = new System.Windows.Forms.Panel();
            this.summaryIcon = new System.Windows.Forms.Label();
            this.labelReviewCount = new System.Windows.Forms.Label();
            this.averageRatingPanel = new System.Windows.Forms.Panel();
            this.avgRatingIcon = new System.Windows.Forms.Label();
            this.labelAverageRating = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.reviewsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReviews)).BeginInit();
            this.actionsGroupBox.SuspendLayout();
            this.summaryPanel.SuspendLayout();
            this.averageRatingPanel.SuspendLayout();
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
            this.btnBack.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnBack.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(622, 23);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(371, 62);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "⭐ הביקורות שלי";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.subtitleLabel.Location = new System.Drawing.Point(644, 85);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(344, 32);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "צפה בביקורות והערות מהמרצים";
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.mainPanel.Controls.Add(this.reviewsGroupBox);
            this.mainPanel.Controls.Add(this.actionsGroupBox);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 120);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(80, 30, 30, 30);
            this.mainPanel.Size = new System.Drawing.Size(1440, 780);
            this.mainPanel.TabIndex = 1;
            // 
            // reviewsGroupBox
            // 
            this.reviewsGroupBox.BackColor = System.Drawing.Color.White;
            this.reviewsGroupBox.Controls.Add(this.dataGridReviews);
            this.reviewsGroupBox.Controls.Add(this.lblNoReviews);
            this.reviewsGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reviewsGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.reviewsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.reviewsGroupBox.Location = new System.Drawing.Point(12, 33);
            this.reviewsGroupBox.Name = "reviewsGroupBox";
            this.reviewsGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.reviewsGroupBox.Size = new System.Drawing.Size(1108, 690);
            this.reviewsGroupBox.TabIndex = 0;
            this.reviewsGroupBox.TabStop = false;
            this.reviewsGroupBox.Text = "📋 ביקורות המרצים שלי";
            // 
            // dataGridReviews
            // 
            this.dataGridReviews.AllowUserToAddRows = false;
            this.dataGridReviews.AllowUserToDeleteRows = false;
            this.dataGridReviews.AllowUserToResizeRows = false;
            this.dataGridReviews.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.dataGridReviews.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridReviews.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridReviews.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridReviews.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridReviews.ColumnHeadersHeight = 60;
            this.dataGridReviews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(72)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridReviews.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridReviews.EnableHeadersVisualStyles = false;
            this.dataGridReviews.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.dataGridReviews.Location = new System.Drawing.Point(18, 59);
            this.dataGridReviews.MultiSelect = false;
            this.dataGridReviews.Name = "dataGridReviews";
            this.dataGridReviews.ReadOnly = true;
            this.dataGridReviews.RowHeadersVisible = false;
            this.dataGridReviews.RowHeadersWidth = 51;
            this.dataGridReviews.RowTemplate.Height = 80;
            this.dataGridReviews.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridReviews.Size = new System.Drawing.Size(1076, 613);
            this.dataGridReviews.TabIndex = 0;
            // 
            // lblNoReviews
            // 
            this.lblNoReviews.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.lblNoReviews.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold); // הגדלת הפונט מ-20 ל-28
            this.lblNoReviews.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99))))); // צבע כהה יותר
            this.lblNoReviews.Location = new System.Drawing.Point(50, 150); // מיקום יותר גבוה
            this.lblNoReviews.Name = "lblNoReviews";
            this.lblNoReviews.Size = new System.Drawing.Size(1000, 400); // הגדלת הגודל
            this.lblNoReviews.TabIndex = 1;
            this.lblNoReviews.Text = "📝\r\n\r\nאין ביקורות זמינות כרגע\r\n\r\nכשהמרצים יכתבו לך ביקורות,\r\nהן יופיעו כאן ברשימה מסודרת";
            this.lblNoReviews.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoReviews.Visible = false;
            // 
            // actionsGroupBox
            // 
            this.actionsGroupBox.BackColor = System.Drawing.Color.White;
            this.actionsGroupBox.Controls.Add(this.summaryPanel);
            this.actionsGroupBox.Controls.Add(this.averageRatingPanel);
            this.actionsGroupBox.Controls.Add(this.btnRefresh);
            this.actionsGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actionsGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.actionsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.actionsGroupBox.Location = new System.Drawing.Point(1150, 33);
            this.actionsGroupBox.Name = "actionsGroupBox";
            this.actionsGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.actionsGroupBox.Size = new System.Drawing.Size(360, 690);
            this.actionsGroupBox.TabIndex = 1;
            this.actionsGroupBox.TabStop = false;
            this.actionsGroupBox.Text = "📊 הסיכום שלי";
            // 
            // summaryPanel
            // 
            this.summaryPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.summaryPanel.Controls.Add(this.summaryIcon);
            this.summaryPanel.Controls.Add(this.labelReviewCount);
            this.summaryPanel.Location = new System.Drawing.Point(33, 70);
            this.summaryPanel.Name = "summaryPanel";
            this.summaryPanel.Size = new System.Drawing.Size(300, 150);
            this.summaryPanel.TabIndex = 0;
            // 
            // summaryIcon
            // 
            this.summaryIcon.AutoSize = true;
            this.summaryIcon.Font = new System.Drawing.Font("Segoe UI", 36F);
            this.summaryIcon.ForeColor = System.Drawing.Color.White;
            this.summaryIcon.Location = new System.Drawing.Point(115, 15);
            this.summaryIcon.Name = "summaryIcon";
            this.summaryIcon.Size = new System.Drawing.Size(87, 81);
            this.summaryIcon.TabIndex = 0;
            this.summaryIcon.Text = "⭐";
            this.summaryIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelReviewCount
            // 
            this.labelReviewCount.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelReviewCount.ForeColor = System.Drawing.Color.White;
            this.labelReviewCount.Location = new System.Drawing.Point(15, 100);
            this.labelReviewCount.Name = "labelReviewCount";
            this.labelReviewCount.Size = new System.Drawing.Size(270, 40);
            this.labelReviewCount.TabIndex = 1;
            this.labelReviewCount.Text = "סה\"כ ביקורות: --";
            this.labelReviewCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // averageRatingPanel
            // 
            this.averageRatingPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.averageRatingPanel.Controls.Add(this.avgRatingIcon);
            this.averageRatingPanel.Controls.Add(this.labelAverageRating);
            this.averageRatingPanel.Location = new System.Drawing.Point(33, 240);
            this.averageRatingPanel.Name = "averageRatingPanel";
            this.averageRatingPanel.Size = new System.Drawing.Size(300, 150);
            this.averageRatingPanel.TabIndex = 1;
            // 
            // avgRatingIcon
            // 
            this.avgRatingIcon.AutoSize = true;
            this.avgRatingIcon.Font = new System.Drawing.Font("Segoe UI", 36F);
            this.avgRatingIcon.ForeColor = System.Drawing.Color.White;
            this.avgRatingIcon.Location = new System.Drawing.Point(115, 15);
            this.avgRatingIcon.Name = "avgRatingIcon";
            this.avgRatingIcon.Size = new System.Drawing.Size(117, 81);
            this.avgRatingIcon.TabIndex = 0;
            this.avgRatingIcon.Text = "😊";
            this.avgRatingIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAverageRating
            // 
            this.labelAverageRating.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelAverageRating.ForeColor = System.Drawing.Color.White;
            this.labelAverageRating.Location = new System.Drawing.Point(15, 100);
            this.labelAverageRating.Name = "labelAverageRating";
            this.labelAverageRating.Size = new System.Drawing.Size(270, 40);
            this.labelAverageRating.TabIndex = 1;
            this.labelAverageRating.Text = "דירוג ממוצע: --";
            this.labelAverageRating.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(33, 420);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(300, 80);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "🔄\r\nרענן ביקורות";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnRefresh.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnRefresh.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // StudentReviewsForm
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
            this.Name = "StudentReviewsForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "הביקורות שלי - סטודנט";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.StudentReviewsForm_Load);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.reviewsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReviews)).EndInit();
            this.actionsGroupBox.ResumeLayout(false);
            this.summaryPanel.ResumeLayout(false);
            this.summaryPanel.PerformLayout();
            this.averageRatingPanel.ResumeLayout(false);
            this.averageRatingPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox reviewsGroupBox;
        private System.Windows.Forms.DataGridView dataGridReviews;
        private System.Windows.Forms.Label lblNoReviews;
        private System.Windows.Forms.GroupBox actionsGroupBox;
        private System.Windows.Forms.Panel summaryPanel;
        private System.Windows.Forms.Label summaryIcon;
        private System.Windows.Forms.Label labelReviewCount;
        private System.Windows.Forms.Panel averageRatingPanel;
        private System.Windows.Forms.Label avgRatingIcon;
        private System.Windows.Forms.Label labelAverageRating;
        private System.Windows.Forms.Button btnRefresh;

        // Event handlers
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
                else if (btn == btnRefresh)
                    btn.BackColor = System.Drawing.Color.FromArgb(217, 119, 6);
            }
        }

        private void Button_MouseLeave(object sender, System.EventArgs e)
        {
            if (sender is System.Windows.Forms.Button btn)
            {
                if (btn == btnBack)
                    btn.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
                else if (btn == btnRefresh)
                    btn.BackColor = System.Drawing.Color.FromArgb(245, 158, 11);
            }
        }
    }
}