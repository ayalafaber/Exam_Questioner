using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Media;

namespace Exam_Questioner
{
    public partial class CreateQuestion : Form
    {
        private string _selectedCategory = null;
        private readonly string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.xlsx");
        private DataTable questionsTable;
        private string selectedQuestionId = null;
        private SoundPlayer swoosh;

        // UI Components
        private Panel pnlAnswerContainer, pnlMultipleChoice, pnlTrueFalse, pnlCategoriesView, pnlQuestionEditor;
        private TextBox txtOpenAnswer;
        private TextBox[] txtChoices;
        private Button[] btnChoices;
        private int selectedChoice = -1;
        private Button btnTrue, btnFalse;
        private bool? trueFalseAnswer = null;
        private Button btnBackToCategories;
        private Label lblCategoryTitle;
        private FlowLayoutPanel categoriesContainer;

        // הגדרת משתנים לעבודה במצב יעד
        private string _targetDifficulty = null;
        private int _targetQuestionCount = 0;
        private int _currentQuestionCount = 0;
        private Label _progressLabel;

        public CreateQuestion()
        {
            InitializeComponent();
            InitializeExcel();
            swoosh = new SoundPlayer(Properties.Resources.swoosh);
            SetupUI();
            this.Load += (s, e) => { CreateViews(); ShowCategoriesView(); };
        }

        private void SetupUI()
        {
            this.BackColor = Color.FromArgb(240, 242, 247);
            this.Font = new Font("Segoe UI", 10F);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(870, 740);
        }

        private void CreateViews()
        {
            CreateCategoriesView();
            CreateQuestionEditor();
        }

        private void CreateCategoriesView()
        {
            pnlCategoriesView = CreatePanel(true);

            var lblTitle = CreateLabel("בחר קטגוריה", new Font("Segoe UI", 24F, FontStyle.Bold), new Point(0, 30), new Size(870, 60));
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            categoriesContainer = new FlowLayoutPanel
            {
                Location = new Point(50, 120),
                Size = new Size(770, 500),
                AutoScroll = true,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = true,
                BackColor = Color.Transparent
            };

            var btnAdd = CreateButton("+ הוסף קטגוריה חדשה", new Point(335, 640), new Size(200, 50), Color.FromArgb(46, 204, 113));
            btnAdd.Click += BtnAddCategory_Click;

            pnlCategoriesView.Controls.AddRange(new Control[] { lblTitle, categoriesContainer, btnAdd });
            this.Controls.Add(pnlCategoriesView);
            LoadCategoryButtons();
        }

        private void LoadCategoryButtons()
        {
            if (categoriesContainer == null) return;
            categoriesContainer.Controls.Clear();

            try
            {
                var categories = GetCategories();
                var colors = new Color[] { Color.FromArgb(52, 152, 219), Color.FromArgb(46, 204, 113), Color.FromArgb(155, 89, 182), Color.FromArgb(230, 126, 34), Color.FromArgb(231, 76, 60), Color.FromArgb(52, 73, 94), Color.FromArgb(241, 196, 15), Color.FromArgb(26, 188, 156) };

                for (int i = 0; i < categories.Count; i++)
                {
                    var category = categories[i];
                    var questionCount = GetQuestionCountForCategory(category);
                    var categoryPanel = CreateCategoryPanel(category, questionCount, colors[i % colors.Length]);
                    categoriesContainer.Controls.Add(categoryPanel);
                }

                if (!categories.Any())
                {
                    var lblEmpty = CreateLabel("אין קטגוריות\nלחץ על 'הוסף קטגוריה חדשה' כדי להתחיל", new Font("Segoe UI", 14F), Point.Empty, new Size(400, 100));
                    lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
                    categoriesContainer.Controls.Add(lblEmpty);
                }
            }
            catch (Exception ex)
            {
                ShowError($"שגיאה בטעינת הקטגוריות: {ex.Message}");
            }
        }

        private List<string> GetCategories()
        {
            using (var wb = new XLWorkbook(filePath))
            {
                return wb.Worksheet("Categories").Column(1).CellsUsed()
                    .Skip(1)
                    .Select(c => c.GetString().Trim())
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Distinct()
                    .OrderBy(c => c)
                    .ToList();
            }
        }

        private Panel CreateCategoryPanel(string category, int questionCount, Color color)
        {
            var panel = new Panel { Size = new Size(220, 160), BackColor = color, Margin = new Padding(15), Cursor = Cursors.Hand, Tag = category };

            var lblName = CreateLabel(category, new Font("Segoe UI", 14F, FontStyle.Bold), new Point(10, 50), new Size(200, 40), Color.White);
            lblName.TextAlign = ContentAlignment.MiddleCenter;

            var lblCount = CreateLabel(questionCount == 0 ? "אין שאלות" : $"{questionCount} שאלות", new Font("Segoe UI", 11F), new Point(10, 95), new Size(200, 30), Color.White);
            lblCount.TextAlign = ContentAlignment.MiddleCenter;

            var btnDelete = CreateButton("✖", new Point(185, 10), new Size(25, 25), Color.FromArgb(192, 57, 43), Color.White);
            btnDelete.Click += (s, e) => DeleteCategory(category);

            // Click events
            Action openEditor = () => OpenCategoryEditor(category);
            panel.Click += (s, e) => openEditor();
            lblName.Click += (s, e) => openEditor();
            lblCount.Click += (s, e) => openEditor();

            panel.Controls.AddRange(new Control[] { lblName, lblCount, btnDelete });
            return panel;
        }

        private void CreateQuestionEditor()
        {
            pnlQuestionEditor = CreatePanel(false);

            btnBackToCategories = CreateButton("← חזרה לקטגוריות", new Point(30, 20), new Size(150, 35), Color.FromArgb(52, 73, 94));
            btnBackToCategories.Click += (s, e) => {
                // במצב יעד, לא לאפשר חזרה לקטגוריות
                if (_targetQuestionCount > 0 && !string.IsNullOrEmpty(_targetDifficulty))
                {
                    var result = MessageBox.Show(
                        $"אתה במצב יצירת שאלות יעד.\nעדיין נותרו {_targetQuestionCount - _currentQuestionCount} שאלות ליצור.\n\nהאם אתה בטוח שברצונך לצאת?",
                        "אישור יציאה",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        this.Close(); // סגירת הטופס במקום חזרה לקטגוריות
                    }
                }
                else
                {
                    ShowCategoriesView(); // חזרה רגילה במצב רגיל
                }
            };

            lblCategoryTitle = CreateLabel("", new Font("Segoe UI", 20F, FontStyle.Bold), new Point(220, 20), new Size(400, 40));
            lblCategoryTitle.TextAlign = ContentAlignment.MiddleCenter;

            var btnNew = CreateButton("+ שאלה חדשה", new Point(650, 20), new Size(140, 35), Color.FromArgb(46, 204, 113));
            btnNew.Click += (s, e) => ClearForm();

            // Move existing controls
            MoveControlsToEditor();

            pnlQuestionEditor.Controls.AddRange(new Control[] { btnBackToCategories, lblCategoryTitle, btnNew });
            this.Controls.Add(pnlQuestionEditor);
        }

        private void MoveControlsToEditor()
        {
            // Position existing controls
            lblQuestion.Location = new Point(74, 125);
            txtQuestion.Location = new Point(180, 115);
            lblType.Location = new Point(74, 182);
            comboBoxType.Location = new Point(180, 180);
            lblDifficulty.Location = new Point(56, 220);
            comboBoxDifficulty.Location = new Point(180, 220);
            lblAnswer.Location = new Point(74, 270);

            // Position buttons below the answer area with better spacing
            btnSave.Location = new Point(220, 440);
            btnEdit.Location = new Point(340, 440);
            btnDelete.Location = new Point(460, 440);

            // Hide category controls
            comboBoxCategory.Visible = false;
            lblCategory.Visible = false;

            // Add to editor panel
            var controls = new Control[] { lblQuestion, txtQuestion, lblType, comboBoxType, lblDifficulty, comboBoxDifficulty, lblAnswer, btnSave, btnEdit, btnDelete, questionsGrid };
            pnlQuestionEditor.Controls.AddRange(controls);

            SetupAnswerControls();
            SetupComboBoxes();
            StyleDataGrid();
        }

        private void SetupAnswerControls()
        {
            pnlAnswerContainer = new Panel
            {
                Location = new Point(180, 270),
                Size = new Size(476, 150),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                RightToLeft = RightToLeft.Yes
            };
            pnlQuestionEditor.Controls.Add(pnlAnswerContainer);

            CreateAnswerControls();
            comboBoxType.SelectedIndexChanged += ComboBoxType_SelectedIndexChanged;
        }

        private void CreateAnswerControls()
        {
            // Open answer
            txtOpenAnswer = new TextBox
            {
                Location = new Point(15, 15),
                Size = new Size(446, 120),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 11F),
                BorderStyle = BorderStyle.None,
                RightToLeft = RightToLeft.Yes,
                TextAlign = HorizontalAlignment.Right,
                Visible = false
            };
            pnlAnswerContainer.Controls.Add(txtOpenAnswer);

            CreateMultipleChoiceControls();
            CreateTrueFalseControls();
        }

        private void CreateMultipleChoiceControls()
        {
            pnlMultipleChoice = new Panel { Size = new Size(476, 150), BackColor = Color.Transparent, RightToLeft = RightToLeft.Yes, Visible = false };
            txtChoices = new TextBox[4];
            btnChoices = new Button[4];
            var colors = new Color[] { Color.FromArgb(52, 152, 219), Color.FromArgb(46, 204, 113), Color.FromArgb(155, 89, 182), Color.FromArgb(230, 126, 34) };

            for (int i = 0; i < 4; i++)
            {
                int index = i;
                txtChoices[i] = new TextBox
                {
                    Location = new Point(15, 10 + (i * 32)),
                    Size = new Size(400, 25),
                    Font = new Font("Segoe UI", 10F),
                    BorderStyle = BorderStyle.None,
                    ForeColor = Color.Gray,
                    Text = $"אפשרות {i + 1}",
                    RightToLeft = RightToLeft.Yes,
                    TextAlign = HorizontalAlignment.Right
                };

                btnChoices[i] = CreateButton(Convert.ToChar(65 + i).ToString(), new Point(420, 10 + (i * 32)), new Size(30, 25), colors[i]);
                btnChoices[i].Click += (s, e) => SelectChoice(index);

                txtChoices[i].Enter += (s, e) => { if (txtChoices[index].Text == $"אפשרות {index + 1}") { txtChoices[index].Text = ""; txtChoices[index].ForeColor = Color.Black; } };
                txtChoices[i].Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtChoices[index].Text)) { txtChoices[index].Text = $"אפשרות {index + 1}"; txtChoices[index].ForeColor = Color.Gray; } };

                pnlMultipleChoice.Controls.AddRange(new Control[] { txtChoices[i], btnChoices[i] });
            }
            pnlAnswerContainer.Controls.Add(pnlMultipleChoice);
        }

        private void CreateTrueFalseControls()
        {
            pnlTrueFalse = new Panel { Size = new Size(476, 150), BackColor = Color.Transparent, Visible = false };

            var lblPrompt = CreateLabel("בחר את התשובה הנכונה:", new Font("Segoe UI", 12F, FontStyle.Bold), new Point(250, 20), new Size(200, 25));
            lblPrompt.TextAlign = ContentAlignment.MiddleRight;

            btnTrue = CreateButton("✓ נכון", new Point(250, 60), new Size(150, 50), Color.FromArgb(46, 204, 113));
            btnTrue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnTrue.Click += (s, e) => SelectTrueFalse(true);

            btnFalse = CreateButton("✗ לא נכון", new Point(50, 60), new Size(150, 50), Color.FromArgb(231, 76, 60));
            btnFalse.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnFalse.Click += (s, e) => SelectTrueFalse(false);

            pnlTrueFalse.Controls.AddRange(new Control[] { lblPrompt, btnTrue, btnFalse });
            pnlAnswerContainer.Controls.Add(pnlTrueFalse);
        }

        // Helper methods
        private Panel CreatePanel(bool visible) => new Panel { Location = Point.Empty, Size = this.ClientSize, BackColor = Color.FromArgb(240, 242, 247), Visible = visible };
        private Label CreateLabel(string text, Font font, Point location, Size size, Color? foreColor = null) => new Label { Text = text, Font = font, Location = location, Size = size, ForeColor = foreColor ?? Color.FromArgb(52, 73, 94) };
        private Button CreateButton(string text, Point location, Size size, Color backColor, Color? foreColor = null) => new Button { Text = text, Location = location, Size = size, BackColor = backColor, ForeColor = foreColor ?? Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };

        private void SetupComboBoxes()
        {
            var combos = new[] { comboBoxType, comboBoxDifficulty };
            foreach (var combo in combos)
            {
                combo.FlatStyle = FlatStyle.Flat;
                combo.BackColor = Color.White;
                combo.Font = new Font("Segoe UI", 11F);
                combo.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            comboBoxType.Items.AddRange(new[] { "פתוחה", "אמריקאית", "נכון/לא נכון" });
            comboBoxDifficulty.Items.AddRange(new[] { "קל", "בינוני", "קשה" });
        }

        private void StyleDataGrid()
        {
            questionsGrid.EnableHeadersVisualStyles = false;
            questionsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            questionsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            questionsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            questionsGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            questionsGrid.ColumnHeadersHeight = 45;

            questionsGrid.DefaultCellStyle.BackColor = Color.White;
            questionsGrid.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            questionsGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            questionsGrid.DefaultCellStyle.SelectionForeColor = Color.White;
            questionsGrid.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            questionsGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            questionsGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 255);
            questionsGrid.GridColor = Color.FromArgb(189, 195, 199);
            questionsGrid.RowTemplate.Height = 50;
            questionsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            questionsGrid.BorderStyle = BorderStyle.None;
            questionsGrid.RightToLeft = RightToLeft.Yes;

            // Position the grid below buttons with proper spacing
            questionsGrid.Location = new Point(30, 490);
            questionsGrid.Size = new Size(810, 200);
        }

        // Event handlers
        private void SelectChoice(int choice)
        {
            var colors = new Color[] { Color.FromArgb(52, 152, 219), Color.FromArgb(46, 204, 113), Color.FromArgb(155, 89, 182), Color.FromArgb(230, 126, 34) };
            for (int i = 0; i < 4; i++)
            {
                btnChoices[i].Text = Convert.ToChar(65 + i).ToString();
                btnChoices[i].BackColor = colors[i];
            }
            selectedChoice = choice;
            btnChoices[choice].Text = "✓";
            btnChoices[choice].BackColor = Color.FromArgb(41, 128, 185);
        }

        private void SelectTrueFalse(bool isTrue)
        {
            trueFalseAnswer = isTrue;
            btnTrue.BackColor = isTrue ? Color.FromArgb(39, 174, 96) : Color.FromArgb(46, 204, 113);
            btnFalse.BackColor = isTrue ? Color.FromArgb(231, 76, 60) : Color.FromArgb(192, 57, 43);
        }

        private void ComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pnlAnswerContainer == null) return;
            txtOpenAnswer.Visible = pnlMultipleChoice.Visible = pnlTrueFalse.Visible = false;

            switch (comboBoxType.SelectedIndex)
            {
                case 0:
                    txtOpenAnswer.Visible = true;
                    pnlAnswerContainer.Size = new Size(476, 150);
                    break;
                case 1:
                    pnlMultipleChoice.Visible = true;
                    pnlAnswerContainer.Size = new Size(476, 160);
                    break;
                case 2:
                    pnlTrueFalse.Visible = true;
                    pnlAnswerContainer.Size = new Size(476, 130);
                    break;
            }

            // Update button positions based on answer container size
            int buttonY = pnlAnswerContainer.Location.Y + pnlAnswerContainer.Size.Height + 20;
            btnSave.Location = new Point(260, buttonY);
            btnEdit.Location = new Point(400, buttonY);
            btnDelete.Location = new Point(520, buttonY);

            // Update grid position
            questionsGrid.Location = new Point(30, buttonY + 50);
        }

        // Navigation
        private void ShowCategoriesView()
        {
            if (pnlCategoriesView != null) pnlCategoriesView.Visible = true;
            if (pnlQuestionEditor != null) pnlQuestionEditor.Visible = false;
            LoadCategoryButtons();
        }

        private void ShowQuestionEditor()
        {
            if (pnlCategoriesView != null) pnlCategoriesView.Visible = false;
            if (pnlQuestionEditor != null) pnlQuestionEditor.Visible = true;

            // עדכון הכותרת לפי המצב
            if (lblCategoryTitle != null)
            {
                if (!string.IsNullOrEmpty(_targetDifficulty) && _targetQuestionCount > 0)
                {
                    lblCategoryTitle.Text = $"יצירת {_targetQuestionCount} שאלות - {_selectedCategory} ({_targetDifficulty})";
                }
                else
                {
                    lblCategoryTitle.Text = $"עריכת שאלות - {_selectedCategory}";
                }
            }

            ClearForm();
        }

        private void OpenCategoryEditor(string category)
        {
            _selectedCategory = category;

            // איפוס מצב יעד כשפותחים בדרך רגילה
            ResetTargetMode();

            ShowQuestionEditor();
            LoadQuestionsForCategory();
        }

        // איפוס מצב יעד
        private void ResetTargetMode()
        {
            _targetDifficulty = null;
            _targetQuestionCount = 0;
            _currentQuestionCount = 0;

            // הסרת תצוגת התקדמות
            if (_progressLabel != null)
            {
                pnlQuestionEditor?.Controls.Remove(_progressLabel);
                _progressLabel = null;
            }

            // החזרת רמת הקושי לזמינה
            if (comboBoxDifficulty != null)
            {
                comboBoxDifficulty.Enabled = true;
            }

            // החזרת טקסט הכפתור
            if (btnBackToCategories != null)
            {
                btnBackToCategories.Text = "← חזרה לקטגוריות";
            }
        }

        // Category management
        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            var newCategory = ShowInputDialog("קטגוריה חדשה", "הכנס את שם הקטגוריה החדשה:");
            if (!string.IsNullOrWhiteSpace(newCategory))
            {
                try
                {
                    EnsureCategoryExists(newCategory);
                    LoadCategoryButtons();
                }
                catch (Exception ex) { ShowError($"שגיאה בהוספת הקטגוריה: {ex.Message}"); }
            }
        }

        private void DeleteCategory(string category)
        {
            int questionCount = GetQuestionCountForCategory(category);
            string message = questionCount > 0 ? $"הקטגוריה '{category}' מכילה {questionCount} שאלות.\nמחיקת הקטגוריה תמחק גם את כל השאלות.\n\nהאם אתה בטוח?" : $"האם אתה בטוח שברצונך למחוק את הקטגוריה '{category}'?";

            if (MessageBox.Show(message, "אישור מחיקה", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var wb = new XLWorkbook(filePath))
                    {
                        // Delete from Categories
                        var wsCats = wb.Worksheet("Categories");
                        wsCats.RowsUsed().FirstOrDefault(r => r.Cell(1).GetString().Trim() == category)?.Delete();

                        // Delete questions
                        var wsQuestions = wb.Worksheet("Questions");
                        var questionsToDelete = wsQuestions.RowsUsed().Skip(1).Where(r => r.Cell(4).GetString().Trim() == category).OrderByDescending(r => r.RowNumber()).ToList();
                        questionsToDelete.ForEach(q => q.Delete());

                        wb.Save();
                    }
                    ShowSuccess($"הקטגוריה '{category}' נמחקה בהצלחה יחד עם {questionCount} שאלות.");
                    LoadCategoryButtons();
                }
                catch (Exception ex) { ShowError($"שגיאה במחיקת הקטגוריה: {ex.Message}"); }
            }
        }

        private int GetQuestionCountForCategory(string category)
        {
            try
            {
                using (var wb = new XLWorkbook(filePath))
                {
                    return wb.Worksheet("Questions").RowsUsed().Skip(1).Count(row => row.Cell(4).GetString().Trim().Equals(category, StringComparison.OrdinalIgnoreCase));
                }
            }
            catch { return 0; }
        }

        // Question management
        private void LoadQuestionsForCategory()
        {
            questionsTable = new DataTable();
            questionsTable.Columns.AddRange(new[] { "ID", "שאלה", "סוג", "קטגוריה", "רמת קושי", "תשובה נכונה" }.Select(col => new DataColumn(col)).ToArray());

            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var rows = workbook.Worksheet("Questions").RowsUsed().Skip(1).Where(row => row.Cell(4).GetString().Trim() == _selectedCategory.Trim());
                    foreach (var row in rows)
                    {
                        questionsTable.Rows.Add(row.Cell(1).GetString(), row.Cell(2).GetString(), row.Cell(3).GetString(), row.Cell(4).GetString(), row.Cell(5).GetString(), row.Cell(6).GetString());
                    }
                }
            }
            catch (Exception ex) { ShowError($"שגיאה בטעינת השאלות: {ex.Message}"); }

            if (questionsGrid != null)
            {
                questionsGrid.DataSource = questionsTable;
                if (questionsGrid.Columns.Count > 0) questionsGrid.Columns[0].Visible = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsFormIncomplete()) { ShowError("אנא מלאו את כל השדות"); return; }
            try
            {
                SaveQuestionToExcel();

                // עדכון המונה במצב יעד
                if (_targetQuestionCount > 0)
                {
                    _currentQuestionCount++;
                    UpdateProgressDisplay();
                }

                ShowSuccess("השאלה נשמרה בהצלחה!");
                ClearForm();
                LoadQuestionsForCategory();
            }
            catch (Exception ex) { ShowError($"שגיאה בשמירת השאלה: {ex.Message}"); }
        }

        private void SaveQuestionToExcel()
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                var ws = workbook.Worksheet("Questions");
                var lastRow = (ws.LastRowUsed()?.RowNumber() ?? 1) + 1;

                ws.Cell(lastRow, 1).Value = Guid.NewGuid().ToString();
                ws.Cell(lastRow, 2).Value = txtQuestion.Text;
                ws.Cell(lastRow, 3).Value = comboBoxType.Text;
                ws.Cell(lastRow, 4).Value = _selectedCategory;
                ws.Cell(lastRow, 5).Value = comboBoxDifficulty.Text;

                switch (comboBoxType.SelectedIndex)
                {
                    case 0:
                        ws.Cell(lastRow, 6).Value = txtOpenAnswer.Text;
                        break;
                    case 1:
                        if (selectedChoice >= 0 && selectedChoice < 4)
                        {
                            ws.Cell(lastRow, 6).Value = txtChoices[selectedChoice].Text;
                            int wrongCol = 7;
                            for (int j = 0; j < 4; j++)
                            {
                                if (j != selectedChoice && !txtChoices[j].Text.StartsWith("אפשרות"))
                                {
                                    ws.Cell(lastRow, wrongCol).Value = txtChoices[j].Text;
                                    wrongCol++;
                                }
                            }
                        }
                        break;
                    case 2:
                        ws.Cell(lastRow, 6).Value = trueFalseAnswer == true ? "נכון" : "לא נכון";
                        break;
                }
                workbook.Save();
            }
        }

        private bool IsFormIncomplete()
        {
            if (string.IsNullOrWhiteSpace(txtQuestion?.Text) || comboBoxType?.SelectedIndex == -1 || comboBoxDifficulty?.SelectedIndex == -1) return true;

            switch (comboBoxType.SelectedIndex)
            {
                case 0: return string.IsNullOrWhiteSpace(txtOpenAnswer?.Text);
                case 1: return txtChoices?.Any(txt => string.IsNullOrWhiteSpace(txt.Text) || txt.Text.StartsWith("אפשרות")) == true || selectedChoice == -1;
                case 2: return trueFalseAnswer == null;
                default: return true;
            }
        }

        private void ClearForm()
        {
            txtQuestion?.Clear();
            txtOpenAnswer?.Clear();
            if (txtChoices != null)
            {
                var colors = new Color[] { Color.FromArgb(52, 152, 219), Color.FromArgb(46, 204, 113), Color.FromArgb(155, 89, 182), Color.FromArgb(230, 126, 34) };
                for (int i = 0; i < txtChoices.Length; i++)
                {
                    txtChoices[i].Text = $"אפשרות {i + 1}";
                    txtChoices[i].ForeColor = Color.Gray;
                    btnChoices[i].Text = Convert.ToChar(65 + i).ToString();
                    btnChoices[i].BackColor = colors[i];
                }
            }
            selectedChoice = -1;
            trueFalseAnswer = null;
            if (btnTrue != null) btnTrue.BackColor = Color.FromArgb(46, 204, 113);
            if (btnFalse != null) btnFalse.BackColor = Color.FromArgb(231, 76, 60);
            if (comboBoxType != null) comboBoxType.SelectedIndex = -1;

            // במצב יעד - לא לנקות את רמת הקושי
            if (comboBoxDifficulty != null && string.IsNullOrEmpty(_targetDifficulty))
                comboBoxDifficulty.SelectedIndex = -1;
            else if (comboBoxDifficulty != null && !string.IsNullOrEmpty(_targetDifficulty))
                comboBoxDifficulty.Text = _targetDifficulty; // שמירה על רמת הקושי היעד

            selectedQuestionId = null;
            if (txtOpenAnswer != null) txtOpenAnswer.Visible = false;
            if (pnlMultipleChoice != null) pnlMultipleChoice.Visible = false;
            if (pnlTrueFalse != null) pnlTrueFalse.Visible = false;
        }

        // Public methods
        public void PreselectCategory(string category)
        {
            _selectedCategory = category;
            EnsureCategoryExists(category);

            // איפוס מצב יעד כשפותחים בדרך רגילה
            ResetTargetMode();

            if (pnlCategoriesView == null || pnlQuestionEditor == null)
                this.Load += (s, e) => { ShowQuestionEditor(); LoadQuestionsForCategory(); };
            else
            { ShowQuestionEditor(); LoadQuestionsForCategory(); }
        }

        // פונקציה להגדרת יעד שאלות ספציפי
        public void SetTargetQuestions(string category, string difficulty, int targetCount)
        {
            _selectedCategory = category;
            _targetDifficulty = difficulty;
            _targetQuestionCount = targetCount;
            _currentQuestionCount = 0;

            EnsureCategoryExists(category);

            if (pnlCategoriesView == null || pnlQuestionEditor == null)
                this.Load += (s, e) => {
                    ShowQuestionEditor();
                    LoadQuestionsForCategory();
                    SetupTargetMode();
                };
            else
            {
                ShowQuestionEditor();
                LoadQuestionsForCategory();
                SetupTargetMode();
            }
        }

        // הגדרת המסך לעבודה במצב יעד
        private void SetupTargetMode()
        {
            if (!string.IsNullOrEmpty(_targetDifficulty) && _targetQuestionCount > 0)
            {
                // קביעת רמת הקושי מראש
                if (comboBoxDifficulty != null)
                {
                    comboBoxDifficulty.Text = _targetDifficulty;
                    comboBoxDifficulty.Enabled = false; // לא ניתן לשנות
                }

                // עדכון הכותרת
                if (lblCategoryTitle != null)
                {
                    lblCategoryTitle.Text = $"יצירת {_targetQuestionCount} שאלות - {_selectedCategory} ({_targetDifficulty})";
                }

                // שינוי טקסט כפתור החזרה
                if (btnBackToCategories != null)
                {
                    btnBackToCategories.Text = "← יציאה";
                }

                // הוספת תצוגת התקדמות
                AddProgressDisplay();
            }
        }

        // הוספת תצוגת התקדמות
        private void AddProgressDisplay()
        {
            if (_progressLabel == null)
            {
                _progressLabel = CreateLabel("", new Font("Segoe UI", 12F, FontStyle.Bold), new Point(30, 80), new Size(400, 25));
                _progressLabel.ForeColor = Color.FromArgb(41, 128, 185);
                pnlQuestionEditor?.Controls.Add(_progressLabel);
            }
            UpdateProgressDisplay();
        }

        // עדכון תצוגת התקדמות
        private void UpdateProgressDisplay()
        {
            if (_progressLabel != null && _targetQuestionCount > 0)
            {
                _progressLabel.Text = $"התקדמות: {_currentQuestionCount}/{_targetQuestionCount} שאלות נוצרו";

                if (_currentQuestionCount >= _targetQuestionCount)
                {
                    _progressLabel.Text += " ✓ הושלם!";
                    _progressLabel.ForeColor = Color.FromArgb(39, 174, 96);

                    // הצגת הודעת השלמה
                    MessageBox.Show($"מעולה! יצרת בהצלחה {_targetQuestionCount} שאלות בקטגוריה '{_selectedCategory}' ברמת קושי '{_targetDifficulty}'.",
                                  "יצירת שאלות הושלמה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void EnsureCategoryExists(string category)
        {
            try
            {
                using (var wb = new XLWorkbook(filePath))
                {
                    var wsCats = wb.Worksheet("Categories");
                    bool exists = wsCats.Column(1).CellsUsed().Skip(1).Any(c => c.GetString().Trim().Equals(category, StringComparison.OrdinalIgnoreCase));
                    if (!exists)
                    {
                        wsCats.Cell((wsCats.LastRowUsed()?.RowNumber() ?? 1) + 1, 1).Value = category;
                        wb.Save();
                    }
                }
            }
            catch (Exception ex) { ShowError($"שגיאה בהוספת הקטגוריה: {ex.Message}"); }
        }

        private void InitializeExcel()
        {
            if (File.Exists(filePath)) return;
            try
            {
                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Questions");
                    var headers = new[] { "ID", "שאלה", "סוג", "קטגוריה", "רמת קושי", "תשובה נכונה", "תשובה שגויה1", "תשובה שגויה2", "תשובה שגויה3" };
                    for (int i = 0; i < headers.Length; i++) ws.Cell(1, i + 1).Value = headers[i];

                    wb.Worksheets.Add("Categories").Cell("A1").Value = "Category";
                    wb.SaveAs(filePath);
                }
            }
            catch (Exception ex) { ShowError($"שגיאה ביצירת קובץ האקסל: {ex.Message}"); }
        }

        // Utility methods
        private string ShowInputDialog(string title, string prompt)
        {
            using (var form = new Form { Text = title, Size = new Size(350, 150), StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false, RightToLeft = RightToLeft.Yes })
            {
                var lblPrompt = CreateLabel(prompt, this.Font, new Point(20, 20), new Size(300, 20));
                lblPrompt.TextAlign = ContentAlignment.MiddleRight;

                var txtInput = new TextBox { Location = new Point(20, 50), Size = new Size(300, 25), RightToLeft = RightToLeft.Yes, TextAlign = HorizontalAlignment.Right };
                var btnOK = new Button { Text = "אישור", Location = new Point(180, 80), Size = new Size(75, 25), DialogResult = DialogResult.OK };
                var btnCancel = new Button { Text = "ביטול", Location = new Point(260, 80), Size = new Size(75, 25), DialogResult = DialogResult.Cancel };

                form.Controls.AddRange(new Control[] { lblPrompt, txtInput, btnOK, btnCancel });
                form.AcceptButton = btnOK;
                form.CancelButton = btnCancel;

                return form.ShowDialog() == DialogResult.OK ? txtInput.Text.Trim() : null;
            }
        }

        private void ShowError(string message) => MessageBox.Show(message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
        private void ShowSuccess(string message) => MessageBox.Show(message, "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // Grid events
        private void questionsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || questionsGrid.Rows.Count <= e.RowIndex) return;
            try
            {
                var row = questionsGrid.Rows[e.RowIndex];
                selectedQuestionId = row.Cells[0].Value?.ToString();
                txtQuestion.Text = row.Cells[1].Value?.ToString();
                comboBoxType.Text = row.Cells[2].Value?.ToString();
                comboBoxDifficulty.Text = row.Cells[4].Value?.ToString();
                LoadAnswerFromGrid(row);
            }
            catch (Exception ex) { ShowError($"שגיאה בטעינת השאלה: {ex.Message}"); }
        }

        private void LoadAnswerFromGrid(DataGridViewRow row)
        {
            string questionType = row.Cells[2].Value?.ToString();
            switch (questionType)
            {
                case "פתוחה":
                    if (txtOpenAnswer != null) txtOpenAnswer.Text = row.Cells[5].Value?.ToString();
                    break;
                case "אמריקאית":
                    LoadMultipleChoiceFromExcel(row.Cells[0].Value?.ToString());
                    break;
                case "נכון/לא נכון":
                    string tfAnswer = row.Cells[5].Value?.ToString();
                    if (tfAnswer == "נכון") SelectTrueFalse(true);
                    else if (tfAnswer == "לא נכון") SelectTrueFalse(false);
                    break;
            }
        }

        private void LoadMultipleChoiceFromExcel(string questionId)
        {
            if (txtChoices == null) return;
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var excelRow = workbook.Worksheet("Questions").RowsUsed().Skip(1).FirstOrDefault(r => r.Cell(1).GetString() == questionId);
                    if (excelRow == null) return;

                    string correctAnswer = excelRow.Cell(6).GetString();
                    var allAnswers = new[] { correctAnswer, excelRow.Cell(7).GetString(), excelRow.Cell(8).GetString(), excelRow.Cell(9).GetString() }
                        .Where(a => !string.IsNullOrEmpty(a)).ToList();

                    for (int i = 0; i < 4; i++)
                    {
                        if (i < allAnswers.Count)
                        {
                            txtChoices[i].Text = allAnswers[i];
                            txtChoices[i].ForeColor = Color.Black;
                            if (allAnswers[i] == correctAnswer) SelectChoice(i);
                        }
                        else
                        {
                            txtChoices[i].Text = $"אפשרות {i + 1}";
                            txtChoices[i].ForeColor = Color.Gray;
                        }
                    }
                }
            }
            catch { }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedQuestionId)) { ShowError("לא נבחרה שאלה לעריכה."); return; }
            if (IsFormIncomplete()) { ShowError("אנא מלאו את כל השדות"); return; }

            try
            {
                UpdateQuestionInExcel();
                ShowSuccess("השאלה עודכנה בהצלחה.");
                ClearForm();
                LoadQuestionsForCategory();
            }
            catch (Exception ex) { ShowError($"שגיאה בעדכון השאלה: {ex.Message}"); }
        }

        private void UpdateQuestionInExcel()
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                var row = workbook.Worksheet("Questions").RowsUsed().Skip(1).FirstOrDefault(r => r.Cell(1).GetString() == selectedQuestionId);
                if (row == null) return;

                row.Cell(2).Value = txtQuestion.Text;
                row.Cell(3).Value = comboBoxType.Text;
                row.Cell(4).Value = _selectedCategory;
                row.Cell(5).Value = comboBoxDifficulty.Text;

                // Clear old answers
                for (int i = 6; i <= 9; i++) row.Cell(i).Clear();

                switch (comboBoxType.SelectedIndex)
                {
                    case 0:
                        row.Cell(6).Value = txtOpenAnswer.Text;
                        break;
                    case 1:
                        if (selectedChoice >= 0 && selectedChoice < 4)
                        {
                            row.Cell(6).Value = txtChoices[selectedChoice].Text;
                            int wrongCol = 7;
                            for (int j = 0; j < 4; j++)
                            {
                                if (j != selectedChoice && !txtChoices[j].Text.StartsWith("אפשרות"))
                                {
                                    row.Cell(wrongCol).Value = txtChoices[j].Text;
                                    wrongCol++;
                                }
                            }
                        }
                        break;
                    case 2:
                        row.Cell(6).Value = trueFalseAnswer == true ? "נכון" : "לא נכון";
                        break;
                }
                workbook.Save();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedQuestionId)) { ShowError("לא נבחרה שאלה למחיקה."); return; }
            if (MessageBox.Show("האם אתה בטוח שברצונך למחוק את השאלה?", "אישור מחיקה", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    workbook.Worksheet("Questions").RowsUsed().Skip(1).FirstOrDefault(r => r.Cell(1).GetString() == selectedQuestionId)?.Delete();
                    workbook.Save();
                    swoosh.Play();
                }
                ShowSuccess("השאלה נמחקה בהצלחה.");
                ClearForm();
                LoadQuestionsForCategory();
            }
            catch (Exception ex) { ShowError($"שגיאה במחיקת השאלה: {ex.Message}"); }
        }

        private void txtQuestion_TextChanged_1(object sender, EventArgs e)
        {

        }

        // Designer event handlers (required)
        private void txtQuestion_TextChanged(object sender, EventArgs e) { }
        private void comboBoxDifficulty_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtAnswer_TextChanged(object sender, EventArgs e) { }
        private void questionsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void CreateQuestion_Load_1(object sender, EventArgs e) { }
    }
}