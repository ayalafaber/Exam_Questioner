
namespace Exam_Questioner
{
    partial class SelectExam
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.btnCloseDataGrid = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.actionsGroupBox = new System.Windows.Forms.GroupBox();
            this.btnRandomExam = new System.Windows.Forms.Button();
            this.btnCreateQuestions = new System.Windows.Forms.Button();
            this.btnViewExams = new System.Windows.Forms.Button();
            this.panelRandomExam = new System.Windows.Forms.Panel();
            this.randomExamGroupBox = new System.Windows.Forms.GroupBox();
            this.btnClosePanel = new System.Windows.Forms.Button();
            this.createExamPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnCreateRandomExam = new System.Windows.Forms.Button();
            this.panelExamsList = new System.Windows.Forms.Panel();
            this.examsListGroupBox = new System.Windows.Forms.GroupBox();
            this.btnCloseExamsList = new System.Windows.Forms.Button();
            this.listbox = new System.Windows.Forms.ListBox();
            this.examsButtonsPanel = new System.Windows.Forms.Panel();
            this.btnViewExam = new System.Windows.Forms.Button();
            this.btnDeleteExam = new System.Windows.Forms.Button();
            this.dataGridGroupBox = new System.Windows.Forms.GroupBox();
            this.headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.actionsGroupBox.SuspendLayout();
            this.panelRandomExam.SuspendLayout();
            this.randomExamGroupBox.SuspendLayout();
            this.createExamPanel.SuspendLayout();
            this.panelExamsList.SuspendLayout();
            this.examsListGroupBox.SuspendLayout();
            this.examsButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.headerPanel.Controls.Add(this.btnBack);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.subtitleLabel);
            this.headerPanel.Controls.Add(this.btnCloseDataGrid);
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
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "🔙 חזרה";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnBack.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnBack.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(512, 25);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(559, 62);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "📝 מערכת יצירת מבחנים";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.subtitleLabel.Location = new System.Drawing.Point(598, 85);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(395, 32);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "יצירה, עריכה וניהול מבחנים דיגיטליים";
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCloseDataGrid
            // 
            this.btnCloseDataGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnCloseDataGrid.FlatAppearance.BorderSize = 0;
            this.btnCloseDataGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseDataGrid.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCloseDataGrid.ForeColor = System.Drawing.Color.White;
            this.btnCloseDataGrid.Location = new System.Drawing.Point(1408, 54);
            this.btnCloseDataGrid.Name = "btnCloseDataGrid";
            this.btnCloseDataGrid.Size = new System.Drawing.Size(59, 43);
            this.btnCloseDataGrid.TabIndex = 1;
            this.btnCloseDataGrid.Text = "✕ ";
            this.btnCloseDataGrid.UseVisualStyleBackColor = false;
            this.btnCloseDataGrid.Visible = false;
            this.btnCloseDataGrid.Click += new System.EventHandler(this.btnCloseDataGrid_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 45;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, -45);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 40;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1395, 745);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.mainPanel.Controls.Add(this.actionsGroupBox);
            this.mainPanel.Controls.Add(this.panelRandomExam);
            this.mainPanel.Controls.Add(this.panelExamsList);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 120);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(80, 30, 30, 30);
            this.mainPanel.Size = new System.Drawing.Size(1440, 780);
            this.mainPanel.TabIndex = 1;
            // 
            // actionsGroupBox
            // 
            this.actionsGroupBox.BackColor = System.Drawing.Color.White;
            this.actionsGroupBox.Controls.Add(this.dataGridView1);
            this.actionsGroupBox.Controls.Add(this.btnRandomExam);
            this.actionsGroupBox.Controls.Add(this.btnCreateQuestions);
            this.actionsGroupBox.Controls.Add(this.btnViewExams);
            this.actionsGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actionsGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.actionsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.actionsGroupBox.Location = new System.Drawing.Point(80, 30);
            this.actionsGroupBox.Name = "actionsGroupBox";
            this.actionsGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.actionsGroupBox.Size = new System.Drawing.Size(1380, 320);
            this.actionsGroupBox.TabIndex = 0;
            this.actionsGroupBox.TabStop = false;
            this.actionsGroupBox.Text = "🎯 פעולות ראשיות";
            // 
            // btnRandomExam
            // 
            this.btnRandomExam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnRandomExam.FlatAppearance.BorderSize = 0;
            this.btnRandomExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRandomExam.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnRandomExam.ForeColor = System.Drawing.Color.White;
            this.btnRandomExam.Location = new System.Drawing.Point(63, 80);
            this.btnRandomExam.Name = "btnRandomExam";
            this.btnRandomExam.Size = new System.Drawing.Size(380, 180);
            this.btnRandomExam.TabIndex = 0;
            this.btnRandomExam.Text = "➕ צור מבחן חדש\r\n\r\nיצירת מבחן אוטומטי\r\nמשאלות קיימות";
            this.btnRandomExam.UseVisualStyleBackColor = false;
            this.btnRandomExam.Click += new System.EventHandler(this.btnRandomExam_Click);
            // 
            // btnCreateQuestions
            // 
            this.btnCreateQuestions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnCreateQuestions.FlatAppearance.BorderSize = 0;
            this.btnCreateQuestions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateQuestions.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnCreateQuestions.ForeColor = System.Drawing.Color.White;
            this.btnCreateQuestions.Location = new System.Drawing.Point(510, 80);
            this.btnCreateQuestions.Name = "btnCreateQuestions";
            this.btnCreateQuestions.Size = new System.Drawing.Size(380, 180);
            this.btnCreateQuestions.TabIndex = 1;
            this.btnCreateQuestions.Text = "📝 יצירת ועריכת שאלות\r\n\r\nהוספה, עריכה ומחיקה\r\nשל שאלות במאגר";
            this.btnCreateQuestions.UseVisualStyleBackColor = false;
            this.btnCreateQuestions.Click += new System.EventHandler(this.btnCreateQuestions_Click);
            // 
            // btnViewExams
            // 
            this.btnViewExams.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnViewExams.FlatAppearance.BorderSize = 0;
            this.btnViewExams.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewExams.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnViewExams.ForeColor = System.Drawing.Color.White;
            this.btnViewExams.Location = new System.Drawing.Point(967, 80);
            this.btnViewExams.Name = "btnViewExams";
            this.btnViewExams.Size = new System.Drawing.Size(380, 180);
            this.btnViewExams.TabIndex = 2;
            this.btnViewExams.Text = "🔍 צפה במבחנים קיימים\r\n\r\nצפייה, עריכה ומחיקה\r\nשל מבחנים קיימים";
            this.btnViewExams.UseVisualStyleBackColor = false;
            this.btnViewExams.Click += new System.EventHandler(this.btnViewExams_Click);
            // 
            // panelRandomExam
            // 
            this.panelRandomExam.Controls.Add(this.randomExamGroupBox);
            this.panelRandomExam.Location = new System.Drawing.Point(370, 370);
            this.panelRandomExam.Name = "panelRandomExam";
            this.panelRandomExam.Size = new System.Drawing.Size(800, 380);
            this.panelRandomExam.TabIndex = 1;
            this.panelRandomExam.Visible = false;
            // 
            // randomExamGroupBox
            // 
            this.randomExamGroupBox.BackColor = System.Drawing.Color.White;
            this.randomExamGroupBox.Controls.Add(this.btnClosePanel);
            this.randomExamGroupBox.Controls.Add(this.createExamPanel);
            this.randomExamGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.randomExamGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.randomExamGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.randomExamGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.randomExamGroupBox.Location = new System.Drawing.Point(0, 0);
            this.randomExamGroupBox.Name = "randomExamGroupBox";
            this.randomExamGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.randomExamGroupBox.Size = new System.Drawing.Size(800, 380);
            this.randomExamGroupBox.TabIndex = 0;
            this.randomExamGroupBox.TabStop = false;
            this.randomExamGroupBox.Text = "➕ יצירת מבחן חדש";
            this.randomExamGroupBox.Enter += new System.EventHandler(this.randomExamGroupBox_Enter);
            // 
            // btnClosePanel
            // 
            this.btnClosePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnClosePanel.FlatAppearance.BorderSize = 0;
            this.btnClosePanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClosePanel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnClosePanel.ForeColor = System.Drawing.Color.White;
            this.btnClosePanel.Location = new System.Drawing.Point(3, 25);
            this.btnClosePanel.Name = "btnClosePanel";
            this.btnClosePanel.Size = new System.Drawing.Size(40, 40);
            this.btnClosePanel.TabIndex = 1;
            this.btnClosePanel.Text = "✕";
            this.btnClosePanel.UseVisualStyleBackColor = false;
            this.btnClosePanel.Click += new System.EventHandler(this.btnClosePanel_Click);
            // 
            // createExamPanel
            // 
            this.createExamPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.createExamPanel.Controls.Add(this.label2);
            this.createExamPanel.Controls.Add(this.label3);
            this.createExamPanel.Controls.Add(this.label4);
            this.createExamPanel.Controls.Add(this.comboBox1);
            this.createExamPanel.Controls.Add(this.comboBox2);
            this.createExamPanel.Controls.Add(this.textBox1);
            this.createExamPanel.Controls.Add(this.btnCreateRandomExam);
            this.createExamPanel.Location = new System.Drawing.Point(49, 59);
            this.createExamPanel.Name = "createExamPanel";
            this.createExamPanel.Size = new System.Drawing.Size(699, 260);
            this.createExamPanel.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.label2.Location = new System.Drawing.Point(479, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 37);
            this.label2.TabIndex = 0;
            this.label2.Text = "מספר שאלות:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.label3.Location = new System.Drawing.Point(579, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 37);
            this.label3.TabIndex = 1;
            this.label3.Text = "נושא:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.label4.Location = new System.Drawing.Point(529, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 37);
            this.label4.TabIndex = 2;
            this.label4.Text = "רמת קושי:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(179, 29);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(350, 43);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(179, 74);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(350, 43);
            this.comboBox2.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.textBox1.Location = new System.Drawing.Point(179, 119);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(300, 41);
            this.textBox1.TabIndex = 5;
            // 
            // btnCreateRandomExam
            // 
            this.btnCreateRandomExam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnCreateRandomExam.FlatAppearance.BorderSize = 0;
            this.btnCreateRandomExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateRandomExam.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnCreateRandomExam.ForeColor = System.Drawing.Color.White;
            this.btnCreateRandomExam.Location = new System.Drawing.Point(179, 166);
            this.btnCreateRandomExam.Name = "btnCreateRandomExam";
            this.btnCreateRandomExam.Size = new System.Drawing.Size(220, 60);
            this.btnCreateRandomExam.TabIndex = 6;
            this.btnCreateRandomExam.Text = "🚀 צור מבחן";
            this.btnCreateRandomExam.UseVisualStyleBackColor = false;
            this.btnCreateRandomExam.Click += new System.EventHandler(this.btnCreateRandomExam_Click);
            // 
            // panelExamsList
            // 
            this.panelExamsList.Controls.Add(this.examsListGroupBox);
            this.panelExamsList.Location = new System.Drawing.Point(370, 370);
            this.panelExamsList.Name = "panelExamsList";
            this.panelExamsList.Size = new System.Drawing.Size(800, 380);
            this.panelExamsList.TabIndex = 2;
            this.panelExamsList.Visible = false;
            // 
            // examsListGroupBox
            // 
            this.examsListGroupBox.BackColor = System.Drawing.Color.White;
            this.examsListGroupBox.Controls.Add(this.btnCloseExamsList);
            this.examsListGroupBox.Controls.Add(this.listbox);
            this.examsListGroupBox.Controls.Add(this.examsButtonsPanel);
            this.examsListGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.examsListGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.examsListGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.examsListGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.examsListGroupBox.Location = new System.Drawing.Point(0, 0);
            this.examsListGroupBox.Name = "examsListGroupBox";
            this.examsListGroupBox.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.examsListGroupBox.Size = new System.Drawing.Size(800, 380);
            this.examsListGroupBox.TabIndex = 0;
            this.examsListGroupBox.TabStop = false;
            this.examsListGroupBox.Text = "🔍 מבחנים קיימים";
            // 
            // btnCloseExamsList
            // 
            this.btnCloseExamsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnCloseExamsList.FlatAppearance.BorderSize = 0;
            this.btnCloseExamsList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseExamsList.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnCloseExamsList.ForeColor = System.Drawing.Color.White;
            this.btnCloseExamsList.Location = new System.Drawing.Point(830, 25);
            this.btnCloseExamsList.Name = "btnCloseExamsList";
            this.btnCloseExamsList.Size = new System.Drawing.Size(40, 40);
            this.btnCloseExamsList.TabIndex = 2;
            this.btnCloseExamsList.Text = "✕";
            this.btnCloseExamsList.UseVisualStyleBackColor = false;
            this.btnCloseExamsList.Click += new System.EventHandler(this.btnCloseExamsList_Click);
            // 
            // listbox
            // 
            this.listbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.listbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listbox.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.listbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.listbox.FormattingEnabled = true;
            this.listbox.ItemHeight = 35;
            this.listbox.Location = new System.Drawing.Point(30, 75);
            this.listbox.Name = "listbox";
            this.listbox.Size = new System.Drawing.Size(740, 210);
            this.listbox.TabIndex = 0;
            this.listbox.SelectedIndexChanged += new System.EventHandler(this.listbox_SelectedIndexChanged);
            // 
            // examsButtonsPanel
            // 
            this.examsButtonsPanel.Controls.Add(this.btnViewExam);
            this.examsButtonsPanel.Controls.Add(this.btnDeleteExam);
            this.examsButtonsPanel.Location = new System.Drawing.Point(300, 300);
            this.examsButtonsPanel.Name = "examsButtonsPanel";
            this.examsButtonsPanel.Size = new System.Drawing.Size(400, 70);
            this.examsButtonsPanel.TabIndex = 1;
            // 
            // btnViewExam
            // 
            this.btnViewExam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnViewExam.Enabled = false;
            this.btnViewExam.FlatAppearance.BorderSize = 0;
            this.btnViewExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewExam.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnViewExam.ForeColor = System.Drawing.Color.White;
            this.btnViewExam.Location = new System.Drawing.Point(210, 10);
            this.btnViewExam.Name = "btnViewExam";
            this.btnViewExam.Size = new System.Drawing.Size(180, 50);
            this.btnViewExam.TabIndex = 1;
            this.btnViewExam.Text = "👁️ צפה במבחן";
            this.btnViewExam.UseVisualStyleBackColor = false;
            this.btnViewExam.Click += new System.EventHandler(this.btnViewExam_Click);
            // 
            // btnDeleteExam
            // 
            this.btnDeleteExam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnDeleteExam.Enabled = false;
            this.btnDeleteExam.FlatAppearance.BorderSize = 0;
            this.btnDeleteExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteExam.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnDeleteExam.ForeColor = System.Drawing.Color.White;
            this.btnDeleteExam.Location = new System.Drawing.Point(10, 10);
            this.btnDeleteExam.Name = "btnDeleteExam";
            this.btnDeleteExam.Size = new System.Drawing.Size(180, 50);
            this.btnDeleteExam.TabIndex = 0;
            this.btnDeleteExam.Text = "🗑️ מחק מבחן";
            this.btnDeleteExam.UseVisualStyleBackColor = false;
            this.btnDeleteExam.Click += new System.EventHandler(this.btnDeleteExam_Click);
            // 
            // dataGridGroupBox
            // 
            this.dataGridGroupBox.Location = new System.Drawing.Point(0, 0);
            this.dataGridGroupBox.Name = "dataGridGroupBox";
            this.dataGridGroupBox.Size = new System.Drawing.Size(200, 100);
            this.dataGridGroupBox.TabIndex = 0;
            this.dataGridGroupBox.TabStop = false;
            // 
            // SelectExam
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
            this.Name = "SelectExam";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "מערכת יצירת מבחנים חכמה";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.actionsGroupBox.ResumeLayout(false);
            this.panelRandomExam.ResumeLayout(false);
            this.randomExamGroupBox.ResumeLayout(false);
            this.createExamPanel.ResumeLayout(false);
            this.createExamPanel.PerformLayout();
            this.panelExamsList.ResumeLayout(false);
            this.examsListGroupBox.ResumeLayout(false);
            this.examsButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #region Component Declarations

        // Header components
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Button btnBack;

        // Main panel
        private System.Windows.Forms.Panel mainPanel;

        // Actions group
        private System.Windows.Forms.GroupBox actionsGroupBox;
        private System.Windows.Forms.Button btnRandomExam;
        private System.Windows.Forms.Button btnCreateQuestions;
        private System.Windows.Forms.Button btnViewExams;

        // Random exam panel
        private System.Windows.Forms.Panel panelRandomExam;
        private System.Windows.Forms.GroupBox randomExamGroupBox;
        private System.Windows.Forms.Button btnClosePanel;
        private System.Windows.Forms.Panel createExamPanel;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.ComboBox comboBox2;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnCreateRandomExam;

        // Exams list panel
        private System.Windows.Forms.Panel panelExamsList;
        private System.Windows.Forms.GroupBox examsListGroupBox;
        private System.Windows.Forms.Button btnCloseExamsList;
        public System.Windows.Forms.ListBox listbox;
        private System.Windows.Forms.Panel examsButtonsPanel;
        private System.Windows.Forms.Button btnViewExam;
        private System.Windows.Forms.Button btnDeleteExam;

        // DataGrid group
        private System.Windows.Forms.GroupBox dataGridGroupBox;
        private System.Windows.Forms.Button btnCloseDataGrid;
        public System.Windows.Forms.DataGridView dataGridView1;

        #endregion

        // Event handlers that need to be implemented in the code-behind
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