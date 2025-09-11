namespace Exam_Questioner
{
    partial class GradesTracker
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.gradesGroupBox = new System.Windows.Forms.GroupBox();
            this.dataGridScores = new System.Windows.Forms.DataGridView();
            this.chartGroupBox = new System.Windows.Forms.GroupBox();
            this.chartProgress = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.summaryGroupBox = new System.Windows.Forms.GroupBox();
            this.averagePanel = new System.Windows.Forms.Panel();
            this.averageIcon = new System.Windows.Forms.Label();
            this.labelAverage = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.gradesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridScores)).BeginInit();
            this.chartGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartProgress)).BeginInit();
            this.summaryGroupBox.SuspendLayout();
            this.averagePanel.SuspendLayout();
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
            this.titleLabel.Location = new System.Drawing.Point(636, 23);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(362, 62);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "📊 מעקב ציונים";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.subtitleLabel.Location = new System.Drawing.Point(665, 85);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(290, 32);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "עקוב אחר ההתקדמות שלך";
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.mainPanel.Controls.Add(this.gradesGroupBox);
            this.mainPanel.Controls.Add(this.chartGroupBox);
            this.mainPanel.Controls.Add(this.summaryGroupBox);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 120);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(80, 30, 30, 30);
            this.mainPanel.Size = new System.Drawing.Size(1440, 780);
            this.mainPanel.TabIndex = 1;
            // 
            // gradesGroupBox
            // 
            this.gradesGroupBox.BackColor = System.Drawing.Color.White;
            this.gradesGroupBox.Controls.Add(this.dataGridScores);
            this.gradesGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gradesGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.gradesGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.gradesGroupBox.Location = new System.Drawing.Point(110, 33);
            this.gradesGroupBox.Name = "gradesGroupBox";
            this.gradesGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.gradesGroupBox.Size = new System.Drawing.Size(1339, 320);
            this.gradesGroupBox.TabIndex = 0;
            this.gradesGroupBox.TabStop = false;
            this.gradesGroupBox.Text = "📋 היסטוריית ציונים";
            // 
            // dataGridScores
            // 
            this.dataGridScores.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.dataGridScores.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridScores.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridScores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridScores.ColumnHeadersHeight = 50;
            this.dataGridScores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridScores.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridScores.EnableHeadersVisualStyles = false;
            this.dataGridScores.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.dataGridScores.Location = new System.Drawing.Point(33, 44);
            this.dataGridScores.Name = "dataGridScores";
            this.dataGridScores.ReadOnly = true;
            this.dataGridScores.RowHeadersVisible = false;
            this.dataGridScores.RowHeadersWidth = 51;
            this.dataGridScores.RowTemplate.Height = 40;
            this.dataGridScores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridScores.Size = new System.Drawing.Size(1270, 260);
            this.dataGridScores.TabIndex = 0;
            this.dataGridScores.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridScores_CellContentClick);
            // 
            // chartGroupBox
            // 
            this.chartGroupBox.BackColor = System.Drawing.Color.White;
            this.chartGroupBox.Controls.Add(this.chartProgress);
            this.chartGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chartGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.chartGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.chartGroupBox.Location = new System.Drawing.Point(110, 370);
            this.chartGroupBox.Name = "chartGroupBox";
            this.chartGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.chartGroupBox.Size = new System.Drawing.Size(950, 380);
            this.chartGroupBox.TabIndex = 1;
            this.chartGroupBox.TabStop = false;
            this.chartGroupBox.Text = "📈 גרף התקדמות";
            // 
            // chartProgress
            // 
            this.chartProgress.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.BorderColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chartProgress.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.BorderColor = System.Drawing.Color.Transparent;
            legend1.Font = new System.Drawing.Font("Segoe UI", 12F);
            legend1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.chartProgress.Legends.Add(legend1);
            this.chartProgress.Location = new System.Drawing.Point(17, 70);
            this.chartProgress.Name = "chartProgress";
            series1.BorderWidth = 4;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            series1.Legend = "Legend1";
            series1.Name = "ציונים";
            this.chartProgress.Series.Add(series1);
            this.chartProgress.Size = new System.Drawing.Size(890, 290);
            this.chartProgress.TabIndex = 0;
            // 
            // summaryGroupBox
            // 
            this.summaryGroupBox.BackColor = System.Drawing.Color.White;
            this.summaryGroupBox.Controls.Add(this.averagePanel);
            this.summaryGroupBox.Controls.Add(this.btnRefresh);
            this.summaryGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.summaryGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.summaryGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.summaryGroupBox.Location = new System.Drawing.Point(1089, 370);
            this.summaryGroupBox.Name = "summaryGroupBox";
            this.summaryGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.summaryGroupBox.Size = new System.Drawing.Size(360, 380);
            this.summaryGroupBox.TabIndex = 2;
            this.summaryGroupBox.TabStop = false;
            this.summaryGroupBox.Text = "📊 סיכום";
            // 
            // averagePanel
            // 
            this.averagePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.averagePanel.Controls.Add(this.averageIcon);
            this.averagePanel.Controls.Add(this.labelAverage);
            this.averagePanel.Location = new System.Drawing.Point(33, 70);
            this.averagePanel.Name = "averagePanel";
            this.averagePanel.Size = new System.Drawing.Size(300, 150);
            this.averagePanel.TabIndex = 0;
            this.averagePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.averagePanel_Paint);
            // 
            // averageIcon
            // 
            this.averageIcon.AutoSize = true;
            this.averageIcon.Font = new System.Drawing.Font("Segoe UI", 32F);
            this.averageIcon.ForeColor = System.Drawing.Color.White;
            this.averageIcon.Location = new System.Drawing.Point(115, 20);
            this.averageIcon.Name = "averageIcon";
            this.averageIcon.Size = new System.Drawing.Size(104, 72);
            this.averageIcon.TabIndex = 0;
            this.averageIcon.Text = "🎯";
            this.averageIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAverage
            // 
            this.labelAverage.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelAverage.ForeColor = System.Drawing.Color.White;
            this.labelAverage.Location = new System.Drawing.Point(15, 100);
            this.labelAverage.Name = "labelAverage";
            this.labelAverage.Size = new System.Drawing.Size(270, 40);
            this.labelAverage.TabIndex = 1;
            this.labelAverage.Text = "ממוצע ציונים: --";
            this.labelAverage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Magenta;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(33, 244);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(300, 55);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "🔄 רענן נתונים";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // GradesTracker
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1440, 900);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "GradesTracker";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "מערכת מעקב ציונים חכמה";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.GradesTracker_Load);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.gradesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridScores)).EndInit();
            this.chartGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartProgress)).EndInit();
            this.summaryGroupBox.ResumeLayout(false);
            this.averagePanel.ResumeLayout(false);
            this.averagePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox gradesGroupBox;
        private System.Windows.Forms.DataGridView dataGridScores;
        private System.Windows.Forms.GroupBox chartGroupBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartProgress;
        private System.Windows.Forms.GroupBox summaryGroupBox;
        private System.Windows.Forms.Panel averagePanel;
        private System.Windows.Forms.Label averageIcon;
        private System.Windows.Forms.Label labelAverage;
        private System.Windows.Forms.Button btnRefresh;

        // Event handlers that need to be implemented
        private void btnBack_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Button_MouseEnter(object sender, System.EventArgs e)
        {
            if (sender is System.Windows.Forms.Button btn)
            {
                btn.BackColor = System.Drawing.Color.FromArgb(220, 38, 38); // Darker red on hover
            }
        }

        private void Button_MouseLeave(object sender, System.EventArgs e)
        {
            if (sender is System.Windows.Forms.Button btn)
            {
                btn.BackColor = System.Drawing.Color.FromArgb(239, 68, 68); // Original red color
            }
        }
    }
}