using System.Drawing;
using System.Windows.Forms;


namespace Exam_Questioner
{
    partial class CreateQuestion
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblDifficulty;
        private System.Windows.Forms.Label lblAnswer;

        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.ComboBox comboBoxDifficulty;
        private System.Windows.Forms.TextBox txtAnswer;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;

        private System.Windows.Forms.DataGridView questionsGrid;

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblDifficulty = new System.Windows.Forms.Label();
            this.lblAnswer = new System.Windows.Forms.Label();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.comboBoxDifficulty = new System.Windows.Forms.ComboBox();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.questionsGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.questionsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(51)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(300, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(222, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "יצירת שאלה חדשה";
            // 
            // lblQuestion
            // 
            this.lblQuestion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblQuestion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblQuestion.Location = new System.Drawing.Point(74, 95);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(100, 25);
            this.lblQuestion.TabIndex = 1;
            this.lblQuestion.Text = "שאלה";
            this.lblQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblType
            // 
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblType.Location = new System.Drawing.Point(74, 152);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(100, 25);
            this.lblType.TabIndex = 3;
            this.lblType.Text = "סוג";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCategory
            // 
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblCategory.Location = new System.Drawing.Point(74, 190);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(100, 25);
            this.lblCategory.TabIndex = 5;
            this.lblCategory.Text = "קטגוריה";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCategory.Visible = false;
            // 
            // lblDifficulty
            // 
            this.lblDifficulty.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDifficulty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblDifficulty.Location = new System.Drawing.Point(56, 230);
            this.lblDifficulty.Name = "lblDifficulty";
            this.lblDifficulty.Size = new System.Drawing.Size(118, 33);
            this.lblDifficulty.TabIndex = 7;
            this.lblDifficulty.Text = "רמת קושי";
            this.lblDifficulty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAnswer
            // 
            this.lblAnswer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAnswer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblAnswer.Location = new System.Drawing.Point(74, 290);
            this.lblAnswer.Name = "lblAnswer";
            this.lblAnswer.Size = new System.Drawing.Size(100, 25);
            this.lblAnswer.TabIndex = 9;
            this.lblAnswer.Text = "תשובה";
            this.lblAnswer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtQuestion
            // 
            this.txtQuestion.BackColor = System.Drawing.Color.White;
            this.txtQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuestion.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtQuestion.Location = new System.Drawing.Point(180, 85);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtQuestion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtQuestion.Size = new System.Drawing.Size(476, 50);
            this.txtQuestion.TabIndex = 2;
            this.txtQuestion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuestion.TextChanged += new System.EventHandler(this.txtQuestion_TextChanged_1);
            // 
            // comboBoxType
            // 
            this.comboBoxType.BackColor = System.Drawing.Color.White;
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxType.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.comboBoxType.Location = new System.Drawing.Point(180, 150);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comboBoxType.Size = new System.Drawing.Size(476, 33);
            this.comboBoxType.TabIndex = 4;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.BackColor = System.Drawing.Color.White;
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxCategory.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.comboBoxCategory.Location = new System.Drawing.Point(180, 190);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comboBoxCategory.Size = new System.Drawing.Size(476, 33);
            this.comboBoxCategory.TabIndex = 6;
            this.comboBoxCategory.Visible = false;
            // 
            // comboBoxDifficulty
            // 
            this.comboBoxDifficulty.BackColor = System.Drawing.Color.White;
            this.comboBoxDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDifficulty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxDifficulty.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.comboBoxDifficulty.Location = new System.Drawing.Point(180, 230);
            this.comboBoxDifficulty.Name = "comboBoxDifficulty";
            this.comboBoxDifficulty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comboBoxDifficulty.Size = new System.Drawing.Size(476, 33);
            this.comboBoxDifficulty.TabIndex = 8;
            this.comboBoxDifficulty.SelectedIndexChanged += new System.EventHandler(this.comboBoxDifficulty_SelectedIndexChanged);
            // 
            // txtAnswer
            // 
            this.txtAnswer.Location = new System.Drawing.Point(180, 225);
            this.txtAnswer.Multiline = true;
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.Size = new System.Drawing.Size(476, 60);
            this.txtAnswer.TabIndex = 10;
            this.txtAnswer.TextChanged += new System.EventHandler(this.txtAnswer_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(200, 370);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "שמור";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(360, 370);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 40);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.Text = "ערוך";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(520, 370);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 40);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "מחק";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // questionsGrid
            // 
            this.questionsGrid.AllowUserToAddRows = false;
            this.questionsGrid.AllowUserToDeleteRows = false;
            this.questionsGrid.BackgroundColor = System.Drawing.Color.White;
            this.questionsGrid.ColumnHeadersHeight = 35;
            this.questionsGrid.Location = new System.Drawing.Point(30, 430);
            this.questionsGrid.MultiSelect = false;
            this.questionsGrid.Name = "questionsGrid";
            this.questionsGrid.ReadOnly = true;
            this.questionsGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.questionsGrid.RowHeadersVisible = false;
            this.questionsGrid.RowHeadersWidth = 51;
            this.questionsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.questionsGrid.Size = new System.Drawing.Size(760, 250);
            this.questionsGrid.TabIndex = 14;
            this.questionsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.questionsGrid_CellClick);
            this.questionsGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.questionsGrid_CellContentClick);
            // 
            // CreateQuestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(870, 740);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.txtQuestion);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.comboBoxCategory);
            this.Controls.Add(this.lblDifficulty);
            this.Controls.Add(this.comboBoxDifficulty);
            this.Controls.Add(this.lblAnswer);
            this.Controls.Add(this.txtAnswer);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.questionsGrid);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "CreateQuestion";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "מערכת ניהול שאלות";
            this.Load += new System.EventHandler(this.CreateQuestion_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.questionsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}