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
        string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");
        private DataTable questionsTable;
        private string selectedQuestionId = null;
        private SoundPlayer swoosh;

        // רכיבי UI לתשובות
        private Panel pnlAnswerContainer;
        private TextBox txtOpenAnswer;
        private Panel pnlMultipleChoice;
        private TextBox[] txtChoices;
        private Button[] btnChoices;
        private int selectedChoice = -1;
        private Panel pnlTrueFalse;
        private Button btnTrue, btnFalse;
        private bool? trueFalseAnswer = null;

        // רכיבי ממשק קטגוריות
        private Panel pnlCategoriesView;
        private Panel pnlQuestionEditor;
        private Button btnBackToCategories;
        private Label lblCategoryTitle;
        private FlowLayoutPanel categoriesContainer;

        public CreateQuestion()
        {
            InitializeComponent();
            InitializeExcel();
            SetupModernUI();
            this.Load += CreateQuestion_Load;
            swoosh = new SoundPlayer(Properties.Resources.swoosh);
        }

        private void SetupModernUI()
        {
            this.BackColor = Color.FromArgb(240, 242, 247);
            this.Font = new Font("Segoe UI", 10F);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(870, 740);
        }

        private void CreateQuestion_Load(object sender, EventArgs e)
        {
            CreateCategoriesView();
            CreateQuestionEditor();
            ShowCategoriesView();
        }

        private void CreateCategoriesView()
        {
            // פאנל ראשי לקטגוריות
            pnlCategoriesView = new Panel
            {
                Location = new Point(0, 0),
                Size = this.ClientSize,
                BackColor = Color.FromArgb(240, 242, 247),
                Visible = true
            };

            // כותרת
            Label lblMainTitle = new Label
            {
                Text = "בחר קטגוריה",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(0, 30),
                Size = new Size(870, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // כונטיינר לריבועי הקטגוריות
            categoriesContainer = new FlowLayoutPanel
            {
                Location = new Point(50, 120),
                Size = new Size(770, 500),
                AutoScroll = true,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = true,
                BackColor = Color.Transparent
            };

            // כפתור הוספת קטגוריה חדשה
            Button btnAddCategory = new Button
            {
                Text = "+ הוסף קטגוריה חדשה",
                Size = new Size(200, 50),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(335, 640),
                Cursor = Cursors.Hand
            };
            btnAddCategory.FlatAppearance.BorderSize = 0;
            btnAddCategory.Click += BtnAddCategory_Click;

            pnlCategoriesView.Controls.Add(lblMainTitle);
            pnlCategoriesView.Controls.Add(categoriesContainer);
            pnlCategoriesView.Controls.Add(btnAddCategory);
            this.Controls.Add(pnlCategoriesView);

            LoadCategoryButtons();
        }

        private void LoadCategoryButtons()
        {
            categoriesContainer.Controls.Clear();

            try
            {
                // טעינת קטגוריות מגיליון Categories בלבד (לא מהשאלות)
                using (var wb = new XLWorkbook(filePath))
                {
                    var wsCats = wb.Worksheet("Categories");
                    var categories = wsCats.Column(1).CellsUsed()
                        .Skip(1) // דילוג על הכותרת
                        .Select(c => c.GetString().Trim())
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Distinct()
                        .OrderBy(c => c)
                        .ToList();

                    var colors = new Color[]
                    {
                        Color.FromArgb(52, 152, 219),  // כחול
                        Color.FromArgb(46, 204, 113),  // ירוק
                        Color.FromArgb(155, 89, 182),  // סגול
                        Color.FromArgb(230, 126, 34),  // כתום
                        Color.FromArgb(231, 76, 60),   // אדום
                        Color.FromArgb(52, 73, 94),    // אפור כהה
                        Color.FromArgb(241, 196, 15),  // צהוב
                        Color.FromArgb(26, 188, 156)   // טורקיז
                    };

                    for (int i = 0; i < categories.Count; i++)
                    {
                        string category = categories[i];
                        int questionCount = GetQuestionCountForCategory(category);

                        Panel categoryPanel = new Panel
                        {
                            Size = new Size(220, 160),
                            BackColor = colors[i % colors.Length],
                            Margin = new Padding(15),
                            Cursor = Cursors.Hand,
                            Tag = category
                        };

                        Label lblCategoryName = new Label
                        {
                            Text = category,
                            Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                            ForeColor = Color.White,
                            Location = new Point(10, 50),
                            Size = new Size(200, 40),
                            TextAlign = ContentAlignment.MiddleCenter
                        };

                        Label lblQuestionCount = new Label
                        {
                            Text = questionCount == 0 ? "אין שאלות" : $"{questionCount} שאלות",
                            Font = new Font("Segoe UI", 11F),
                            ForeColor = Color.White,
                            Location = new Point(10, 95),
                            Size = new Size(200, 30),
                            TextAlign = ContentAlignment.MiddleCenter
                        };

                        // כפתור מחיקת קטגוריה
                        Button btnDeleteCategory = new Button
                        {
                            Text = "✖",
                            Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                            ForeColor = Color.White,
                            BackColor = Color.FromArgb(192, 57, 43),
                            Size = new Size(25, 25),
                            Location = new Point(185, 10),
                            FlatStyle = FlatStyle.Flat,
                            Cursor = Cursors.Hand,
                            Tag = category
                        };
                        btnDeleteCategory.FlatAppearance.BorderSize = 0;
                        btnDeleteCategory.Click += (s, e) => DeleteCategory(category);

                        categoryPanel.Controls.Add(lblCategoryName);
                        categoryPanel.Controls.Add(lblQuestionCount);
                        categoryPanel.Controls.Add(btnDeleteCategory);

                        // הוספת אירועי לחיצה לכל הרכיבים
                        categoryPanel.Click += (s, e) => OpenCategoryEditor(category);
                        lblCategoryName.Click += (s, e) => OpenCategoryEditor(category);
                        lblQuestionCount.Click += (s, e) => OpenCategoryEditor(category);

                        categoriesContainer.Controls.Add(categoryPanel);
                    }

                    // אם אין קטגוריות - הצגת הודעה
                    if (categories.Count == 0)
                    {
                        Label lblNoCategories = new Label
                        {
                            Text = "אין קטגוריות\nלחץ על 'הוסף קטגוריה חדשה' כדי להתחיל",
                            Font = new Font("Segoe UI", 14F),
                            ForeColor = Color.FromArgb(52, 73, 94),
                            Size = new Size(400, 100),
                            TextAlign = ContentAlignment.MiddleCenter
                        };
                        categoriesContainer.Controls.Add(lblNoCategories);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בטעינת הקטגוריות: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteCategory(string category)
        {
            // בדיקה אם יש שאלות בקטגוריה
            int questionCount = GetQuestionCountForCategory(category);

            string message = questionCount > 0
                ? $"הקטגוריה '{category}' מכילה {questionCount} שאלות.\nמחיקת הקטגוריה תגרום גם למחיקת כל השאלות שלה.\n\nהאם אתה בטוח שברצונך למחוק?"
                : $"האם אתה בטוח שברצונך למחוק את הקטגוריה '{category}'?";

            var result = MessageBox.Show(message, "אישור מחיקה", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var wb = new XLWorkbook(filePath))
                    {
                        // מחיקת הקטגוריה מגיליון Categories
                        var wsCats = wb.Worksheet("Categories");
                        var rowToDelete = wsCats.RowsUsed().FirstOrDefault(r => r.Cell(1).GetString().Trim() == category);
                        if (rowToDelete != null)
                        {
                            rowToDelete.Delete();
                        }

                        // מחיקת כל השאלות של הקטגוריה מגיליון Questions
                        var wsQuestions = wb.Worksheet("Questions");
                        var questionsToDelete = wsQuestions.RowsUsed().Skip(1)
                            .Where(r => r.Cell(4).GetString().Trim() == category)
                            .OrderByDescending(r => r.RowNumber()) // מחיקה מלמטה למעלה
                            .ToList();

                        foreach (var questionRow in questionsToDelete)
                        {
                            questionRow.Delete();
                        }
                        wb.Save();
                    }

                    MessageBox.Show($"הקטגוריה '{category}' נמחקה בהצלחה יחד עם {questionCount} שאלות.", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoryButtons(); // רענון התצוגה
                    swoosh.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"שגיאה במחיקת הקטגוריה: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int GetQuestionCountForCategory(string category)
        {
            try
            {
                using (var wb = new XLWorkbook(filePath))
                {
                    var ws = wb.Worksheet("Questions");
                    var rowsUsed = ws.RowsUsed();

                    if (rowsUsed.Count() <= 1) // רק הכותרת או אין שורות
                        return 0;

                    int count = 0;
                    foreach (var row in rowsUsed.Skip(1)) // דילוג על שורת הכותרת
                    {
                        string questionCategory = row.Cell(4).GetString().Trim(); // עמודה D - קטגוריה
                        if (!string.IsNullOrEmpty(questionCategory) && questionCategory.Equals(category, StringComparison.OrdinalIgnoreCase))
                        {
                            count++;
                        }
                    }

                    return count;
                }
            }
            catch (Exception ex)
            {
                // במקרה של שגיאה, נחזיר 0 במקום לזרוק exception
                MessageBox.Show($"שגיאה בספירת שאלות עבור קטגוריה {category}: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            using (var inputForm = new Form())
            {
                inputForm.Text = "קטגוריה חדשה";
                inputForm.Size = new Size(350, 150);
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.MaximizeBox = false;
                inputForm.MinimizeBox = false;
                inputForm.RightToLeft = RightToLeft.Yes;

                var lblPrompt = new Label()
                {
                    Text = "הכנס את שם הקטגוריה החדשה:",
                    Location = new Point(20, 20),
                    Size = new Size(300, 20),
                    TextAlign = ContentAlignment.MiddleRight
                };

                var txtInput = new TextBox()
                {
                    Location = new Point(20, 50),
                    Size = new Size(300, 25),
                    RightToLeft = RightToLeft.Yes,
                    TextAlign = HorizontalAlignment.Right
                };

                var btnOK = new Button()
                {
                    Text = "אישור",
                    Location = new Point(180, 80),
                    Size = new Size(75, 25),
                    DialogResult = DialogResult.OK
                };

                var btnCancel = new Button()
                {
                    Text = "ביטול",
                    Location = new Point(260, 80),
                    Size = new Size(75, 25),
                    DialogResult = DialogResult.Cancel
                };

                inputForm.Controls.AddRange(new Control[] { lblPrompt, txtInput, btnOK, btnCancel });
                inputForm.AcceptButton = btnOK;
                inputForm.CancelButton = btnCancel;

                if (inputForm.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(txtInput.Text))
                {
                    string newCategory = txtInput.Text.Trim();

                    try
                    {
                        using (var wb = new XLWorkbook(filePath))
                        {
                            var wsCats = wb.Worksheet("Categories");
                            var lastRow = wsCats.LastRowUsed()?.RowNumber() ?? 1;
                            wsCats.Cell(lastRow + 1, 1).Value = newCategory;
                            wb.Save();
                        }

                        LoadCategoryButtons();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"שגיאה בהוספת הקטגוריה: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void OpenCategoryEditor(string category)
        {
            _selectedCategory = category;
            ShowQuestionEditor();
            LoadQuestionsForCategory();
        }

        private void CreateQuestionEditor()
        {
            // פאנל עריכת שאלות (מוסתר בהתחלה)
            pnlQuestionEditor = new Panel
            {
                Location = new Point(0, 0),
                Size = this.ClientSize,
                BackColor = Color.FromArgb(240, 242, 247),
                Visible = false
            };

            // כפתור חזרה
            btnBackToCategories = new Button
            {
                Text = "← חזרה לקטגוריות",
                Location = new Point(30, 20),
                Size = new Size(150, 35),
                Font = new Font("Segoe UI", 11F),
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBackToCategories.FlatAppearance.BorderSize = 0;
            btnBackToCategories.Click += (s, e) => ShowCategoriesView();

            // כותרת קטגוריה
            lblCategoryTitle = new Label
            {
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(220, 20),
                Size = new Size(400, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // כפתור הוספת שאלה חדשה
            Button btnNewQuestion = new Button
            {
                Text = "+ שאלה חדשה",
                Location = new Point(650, 20),
                Size = new Size(140, 35),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnNewQuestion.FlatAppearance.BorderSize = 0;
            btnNewQuestion.Click += (s, e) => ClearForm(); // ניקוי הטופס לשאלה חדשה

            pnlQuestionEditor.Controls.Add(btnBackToCategories);
            pnlQuestionEditor.Controls.Add(lblCategoryTitle);
            pnlQuestionEditor.Controls.Add(btnNewQuestion);

            // העברת כל הקומפוננטים הקיימים לפאנל עריכת השאלות
            MoveExistingControlsToEditor();

            this.Controls.Add(pnlQuestionEditor);
        }

        private void MoveExistingControlsToEditor()
        {
            // הזזת הרכיבים הקיימים לפאנל עריכת השאלות
            lblQuestion.Location = new Point(74, 125);
            txtQuestion.Location = new Point(180, 115);
            lblType.Location = new Point(74, 182);
            comboBoxType.Location = new Point(180, 180);
            comboBoxCategory.Visible = false; // מסתירים את הקומבו של הקטגוריות
            lblCategory.Visible = false;
            lblDifficulty.Location = new Point(56, 220);
            comboBoxDifficulty.Location = new Point(180, 220);
            lblAnswer.Location = new Point(74, 270);

            pnlQuestionEditor.Controls.Add(lblQuestion);
            pnlQuestionEditor.Controls.Add(txtQuestion);
            pnlQuestionEditor.Controls.Add(lblType);
            pnlQuestionEditor.Controls.Add(comboBoxType);
            pnlQuestionEditor.Controls.Add(lblDifficulty);
            pnlQuestionEditor.Controls.Add(comboBoxDifficulty);
            pnlQuestionEditor.Controls.Add(lblAnswer);
            pnlQuestionEditor.Controls.Add(btnSave);
            pnlQuestionEditor.Controls.Add(btnEdit);
            pnlQuestionEditor.Controls.Add(btnDelete);
            pnlQuestionEditor.Controls.Add(questionsGrid);

            SetupComboBoxes();
            CreateAnswerContainer();
            StyleDataGrid();
            AdjustButtonPositions();

            comboBoxType.SelectedIndexChanged += ComboBoxType_SelectedIndexChanged;
        }

        private void ShowCategoriesView()
        {
            pnlCategoriesView.Visible = true;
            pnlQuestionEditor.Visible = false;
            LoadCategoryButtons(); // רענון הריבועים
        }

        private void ShowQuestionEditor()
        {
            pnlCategoriesView.Visible = false;
            pnlQuestionEditor.Visible = true;
            lblCategoryTitle.Text = $"עריכת שאלות - {_selectedCategory}";
            ClearForm();
        }

        private void LoadQuestionsForCategory()
        {
            questionsTable = new DataTable();
            questionsTable.Columns.Add("ID");
            questionsTable.Columns.Add("שאלה");
            questionsTable.Columns.Add("סוג");
            questionsTable.Columns.Add("קטגוריה");
            questionsTable.Columns.Add("רמת קושי");
            questionsTable.Columns.Add("תשובה נכונה");

            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var ws = workbook.Worksheet("Questions");
                    var rowsUsed = ws.RowsUsed();

                    if (rowsUsed.Count() > 1)
                    {
                        foreach (var row in rowsUsed.Skip(1))
                        {
                            if (row.Cell(4).GetString().Trim() == _selectedCategory.Trim())
                            {
                                string answerDisplay = row.Cell(6).GetString();

                                questionsTable.Rows.Add(
                                    row.Cell(1).GetString(),
                                    row.Cell(2).GetString(),
                                    row.Cell(3).GetString(),
                                    row.Cell(4).GetString(),
                                    row.Cell(5).GetString(),
                                    answerDisplay
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בטעינת השאלות: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (questionsGrid != null)
            {
                questionsGrid.DataSource = questionsTable;
                if (questionsGrid.Columns.Count > 0)
                    questionsGrid.Columns[0].Visible = false;
            }
        }

        private void SaveQuestionToExcel()
        {
            string questionId = Guid.NewGuid().ToString();

            using (var workbook = new XLWorkbook(filePath))
            {
                var ws = workbook.Worksheet("Questions");
                var lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;
                lastRow++;

                ws.Cell(lastRow, 1).Value = questionId;
                ws.Cell(lastRow, 2).Value = txtQuestion.Text;
                ws.Cell(lastRow, 3).Value = comboBoxType.Text;
                ws.Cell(lastRow, 4).Value = _selectedCategory; // השתמשות בקטגוריה הנבחרת
                ws.Cell(lastRow, 5).Value = comboBoxDifficulty.Text;

                switch (comboBoxType.SelectedIndex)
                {
                    case 0: // שאלה פתוחה
                        ws.Cell(lastRow, 6).Value = txtOpenAnswer.Text;
                        break;

                    case 1: // שאלה אמריקאית
                        if (selectedChoice >= 0 && selectedChoice < 4)
                        {
                            var correctChoiceText = txtChoices[selectedChoice].Text;
                            if (!string.IsNullOrEmpty(correctChoiceText) && !correctChoiceText.StartsWith("אפשרות"))
                            {
                                ws.Cell(lastRow, 6).Value = correctChoiceText;
                            }
                        }

                        int wrongCol = 7;
                        for (int j = 0; j < 4; j++)
                        {
                            if (j != selectedChoice)
                            {
                                var choiceText = txtChoices[j].Text;
                                if (!string.IsNullOrEmpty(choiceText) && !choiceText.StartsWith("אפשרות"))
                                {
                                    ws.Cell(lastRow, wrongCol).Value = choiceText;
                                    wrongCol++;
                                }
                            }
                        }
                        break;

                    case 2: // שאלת נכון/לא נכון
                        ws.Cell(lastRow, 6).Value = trueFalseAnswer == true ? "נכון" : "לא נכון";
                        break;
                }

                workbook.Save();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsFormIncomplete())
                {
                    MessageBox.Show("אנא מלאו את כל השדות", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveQuestionToExcel();
                MessageBox.Show("השאלה נשמרה בהצלחה!", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadQuestionsForCategory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בשמירת השאלה: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // המשך כל המתודות הקיימות עם שינויים מינימליים...
        // (כל הקוד הנוסף נשאר זהה - CreateAnswerContainer, SetupComboBoxes, וכו')

        private void CreateAnswerContainer()
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
        }

        private void SetupComboBoxes()
        {
            var comboBoxes = new[] { comboBoxType, comboBoxDifficulty };

            foreach (var combo in comboBoxes)
            {
                combo.FlatStyle = FlatStyle.Flat;
                combo.BackColor = Color.White;
                combo.Font = new Font("Segoe UI", 11F);
                combo.ForeColor = Color.FromArgb(52, 73, 94);
            }

            comboBoxType.Items.AddRange(new[] { "פתוחה", "אמריקאית", "נכון/לא נכון" });
            comboBoxDifficulty.Items.AddRange(new[] { "קל", "בינוני", "קשה" });
            comboBoxType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDifficulty.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CreateAnswerControls()
        {
            // שאלה פתוחה
            txtOpenAnswer = new TextBox
            {
                Location = new Point(15, 15),
                Size = new Size(446, 120),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 11F),
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                RightToLeft = RightToLeft.Yes,
                TextAlign = HorizontalAlignment.Right,
                Visible = false
            };
            pnlAnswerContainer.Controls.Add(txtOpenAnswer);

            CreateMultipleChoice();
            CreateTrueFalse();
        }

        private void CreateMultipleChoice()
        {
            pnlMultipleChoice = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(476, 150),
                BackColor = Color.Transparent,
                RightToLeft = RightToLeft.Yes,
                Visible = false
            };

            txtChoices = new TextBox[4];
            btnChoices = new Button[4];

            var colors = new Color[]
            {
                Color.FromArgb(52, 152, 219),
                Color.FromArgb(46, 204, 113),
                Color.FromArgb(155, 89, 182),
                Color.FromArgb(230, 126, 34)
            };

            for (int i = 0; i < 4; i++)
            {
                txtChoices[i] = new TextBox
                {
                    Location = new Point(15, 10 + (i * 32)),
                    Size = new Size(400, 25),
                    Font = new Font("Segoe UI", 10F),
                    BorderStyle = BorderStyle.None,
                    BackColor = Color.White,
                    ForeColor = Color.Gray,
                    Text = $"אפשרות {i + 1}",
                    RightToLeft = RightToLeft.Yes,
                    TextAlign = HorizontalAlignment.Right
                };

                btnChoices[i] = new Button
                {
                    Location = new Point(420, 10 + (i * 32)),
                    Size = new Size(30, 25),
                    Text = Convert.ToChar(65 + i).ToString(),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = colors[i],
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Tag = i
                };

                btnChoices[i].FlatAppearance.BorderSize = 0;

                int index = i;
                btnChoices[i].Click += (s, e) => SelectChoice(index);

                txtChoices[i].Enter += (s, e) =>
                {
                    var tb = s as TextBox;
                    if (tb.Text == $"אפשרות {index + 1}")
                    {
                        tb.Text = "";
                        tb.ForeColor = Color.Black;
                    }
                };

                txtChoices[i].Leave += (s, e) =>
                {
                    var tb = s as TextBox;
                    if (string.IsNullOrWhiteSpace(tb.Text))
                    {
                        tb.Text = $"אפשרות {index + 1}";
                        tb.ForeColor = Color.Gray;
                    }
                };

                pnlMultipleChoice.Controls.Add(txtChoices[i]);
                pnlMultipleChoice.Controls.Add(btnChoices[i]);
            }

            pnlAnswerContainer.Controls.Add(pnlMultipleChoice);
        }

        private void SelectChoice(int choice)
        {
            var colors = new Color[]
            {
                Color.FromArgb(52, 152, 219),
                Color.FromArgb(46, 204, 113),
                Color.FromArgb(155, 89, 182),
                Color.FromArgb(230, 126, 34)
            };

            for (int i = 0; i < 4; i++)
            {
                btnChoices[i].Text = Convert.ToChar(65 + i).ToString();
                btnChoices[i].BackColor = colors[i];
            }

            selectedChoice = choice;
            btnChoices[choice].Text = "✓";
            btnChoices[choice].BackColor = Color.FromArgb(41, 128, 185);
        }

        private void CreateTrueFalse()
        {
            pnlTrueFalse = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(476, 150),
                BackColor = Color.Transparent,
                Visible = false
            };

            Label lblQuestion = new Label
            {
                Text = "בחר את התשובה הנכונה:",
                Location = new Point(250, 20),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                TextAlign = ContentAlignment.MiddleRight
            };

            btnTrue = new Button
            {
                Text = "✓ נכון",
                Location = new Point(250, 60),
                Size = new Size(150, 50),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(46, 204, 113),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            btnTrue.FlatAppearance.BorderSize = 0;
            btnTrue.Click += (s, e) => SelectTrueFalse(true);

            btnFalse = new Button
            {
                Text = "✗ לא נכון",
                Location = new Point(50, 60),
                Size = new Size(150, 50),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(231, 76, 60),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            btnFalse.FlatAppearance.BorderSize = 0;
            btnFalse.Click += (s, e) => SelectTrueFalse(false);

            pnlTrueFalse.Controls.AddRange(new Control[] { lblQuestion, btnTrue, btnFalse });
            pnlAnswerContainer.Controls.Add(pnlTrueFalse);
        }

        private void SelectTrueFalse(bool isTrue)
        {
            trueFalseAnswer = isTrue;
            btnTrue.BackColor = Color.FromArgb(46, 204, 113);
            btnFalse.BackColor = Color.FromArgb(231, 76, 60);

            if (isTrue)
            {
                btnTrue.BackColor = Color.FromArgb(39, 174, 96);
            }
            else
            {
                btnFalse.BackColor = Color.FromArgb(192, 57, 43);
            }
        }

        private void ComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pnlAnswerContainer == null) return;

            if (txtOpenAnswer != null) txtOpenAnswer.Visible = false;
            if (pnlMultipleChoice != null) pnlMultipleChoice.Visible = false;
            if (pnlTrueFalse != null) pnlTrueFalse.Visible = false;

            switch (comboBoxType.SelectedIndex)
            {
                case 0:
                    if (txtOpenAnswer != null)
                    {
                        txtOpenAnswer.Visible = true;
                        pnlAnswerContainer.Size = new Size(476, 150);
                    }
                    break;
                case 1:
                    if (pnlMultipleChoice != null)
                    {
                        pnlMultipleChoice.Visible = true;
                        pnlAnswerContainer.Size = new Size(476, 160);
                    }
                    break;
                case 2:
                    if (pnlTrueFalse != null)
                    {
                        pnlTrueFalse.Visible = true;
                        pnlAnswerContainer.Size = new Size(476, 130);
                    }
                    break;
            }

            AdjustButtonPositions();
        }

        private void AdjustButtonPositions()
        {
            if (pnlAnswerContainer == null || btnSave == null) return;

            int baseY = pnlAnswerContainer.Location.Y + pnlAnswerContainer.Size.Height + 10;

            btnSave.Location = new Point(200, baseY);
            if (btnEdit != null) btnEdit.Location = new Point(360, baseY);
            if (btnDelete != null) btnDelete.Location = new Point(520, baseY);

            int gridY = baseY + 50;
            if (questionsGrid != null) questionsGrid.Location = new Point(30, gridY);
        }

        private void StyleDataGrid()
        {
            if (questionsGrid == null) return;

            questionsGrid.EnableHeadersVisualStyles = false;
            questionsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            questionsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            questionsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            questionsGrid.ColumnHeadersHeight = 45;

            questionsGrid.DefaultCellStyle.BackColor = Color.White;
            questionsGrid.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            questionsGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            questionsGrid.DefaultCellStyle.SelectionForeColor = Color.White;
            questionsGrid.DefaultCellStyle.Font = new Font("Segoe UI", 11F);

            questionsGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 255);
            questionsGrid.GridColor = Color.FromArgb(189, 195, 199);

            questionsGrid.RowTemplate.Height = 50;
            questionsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            questionsGrid.BorderStyle = BorderStyle.None;
            questionsGrid.Size = new Size(760, 250);

            questionsGrid.RightToLeft = RightToLeft.Yes;
            questionsGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            questionsGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void SetAnswerFromExcelData(DataGridViewRow row)
        {
            string questionType = row.Cells[2].Value?.ToString();

            switch (questionType)
            {
                case "פתוחה":
                    if (txtOpenAnswer != null)
                        txtOpenAnswer.Text = row.Cells[5].Value?.ToString();
                    break;
                case "אמריקאית":
                    using (var workbook = new XLWorkbook(filePath))
                    {
                        var ws = workbook.Worksheet("Questions");
                        string selectedId = row.Cells[0].Value?.ToString();
                        var excelRow = ws.RowsUsed().Skip(1).FirstOrDefault(r => r.Cell(1).GetString() == selectedId);

                        if (excelRow != null && txtChoices != null)
                        {
                            string correctAnswer = excelRow.Cell(6).GetString();
                            string[] wrongAnswers = new string[3];
                            for (int i = 0; i < 3; i++)
                            {
                                wrongAnswers[i] = excelRow.Cell(7 + i).GetString();
                            }

                            var allAnswers = new List<string>();
                            if (!string.IsNullOrEmpty(correctAnswer))
                                allAnswers.Add(correctAnswer);

                            for (int j = 0; j < wrongAnswers.Length; j++)
                            {
                                if (!string.IsNullOrEmpty(wrongAnswers[j]))
                                    allAnswers.Add(wrongAnswers[j]);
                            }

                            for (int k = 0; k < 4; k++)
                            {
                                if (k < allAnswers.Count)
                                {
                                    txtChoices[k].Text = allAnswers[k];
                                    txtChoices[k].ForeColor = Color.Black;
                                }
                                else
                                {
                                    txtChoices[k].Text = $"אפשרות {k + 1}";
                                    txtChoices[k].ForeColor = Color.Gray;
                                }
                            }

                            if (!string.IsNullOrEmpty(correctAnswer))
                            {
                                for (int m = 0; m < 4; m++)
                                {
                                    if (txtChoices[m].Text == correctAnswer)
                                    {
                                        SelectChoice(m);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "נכון/לא נכון":
                    string tfAnswer = row.Cells[5].Value?.ToString();
                    if (tfAnswer == "נכון")
                        SelectTrueFalse(true);
                    else if (tfAnswer == "לא נכון")
                        SelectTrueFalse(false);
                    break;
            }
        }

        private void UpdateQuestionInExcel()
        {
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var ws = workbook.Worksheet("Questions");
                    var row = ws.RowsUsed().Skip(1).FirstOrDefault(r => r.Cell(1).GetString() == selectedQuestionId);

                    if (row != null)
                    {
                        row.Cell(2).Value = txtQuestion.Text;
                        row.Cell(3).Value = comboBoxType.Text;
                        row.Cell(4).Value = _selectedCategory;
                        row.Cell(5).Value = comboBoxDifficulty.Text;

                        for (int i = 6; i <= 9; i++)
                        {
                            row.Cell(i).Clear();
                        }

                        switch (comboBoxType.SelectedIndex)
                        {
                            case 0:
                                row.Cell(6).Value = txtOpenAnswer.Text;
                                break;

                            case 1:
                                if (selectedChoice >= 0 && selectedChoice < 4)
                                {
                                    var correctChoiceText = txtChoices[selectedChoice].Text;
                                    if (!string.IsNullOrEmpty(correctChoiceText) && !correctChoiceText.StartsWith("אפשרות"))
                                    {
                                        row.Cell(6).Value = correctChoiceText;
                                    }
                                }

                                int wrongCol = 7;
                                for (int j = 0; j < 4; j++)
                                {
                                    if (j != selectedChoice)
                                    {
                                        var choiceText = txtChoices[j].Text;
                                        if (!string.IsNullOrEmpty(choiceText) && !choiceText.StartsWith("אפשרות"))
                                        {
                                            row.Cell(wrongCol).Value = choiceText;
                                            wrongCol++;
                                        }
                                    }
                                }
                                break;

                            case 2:
                                row.Cell(6).Value = trueFalseAnswer == true ? "נכון" : "לא נכון";
                                break;
                        }
                    }
                    workbook.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בעדכון השאלה: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeExcel()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    using (var wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Questions");
                        ws.Cell("A1").Value = "ID";
                        ws.Cell("B1").Value = "שאלה";
                        ws.Cell("C1").Value = "סוג";
                        ws.Cell("D1").Value = "קטגוריה";
                        ws.Cell("E1").Value = "רמת קושי";
                        ws.Cell("F1").Value = "תשובה נכונה";
                        ws.Cell("G1").Value = "תשובה שגויה1";
                        ws.Cell("H1").Value = "תשובה שגויה2";
                        ws.Cell("I1").Value = "תשובה שגויה3";

                        wb.Worksheets.Add("Categories");
                        var catWs = wb.Worksheet("Categories");
                        catWs.Cell("A1").Value = "Category";
                        wb.SaveAs(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה ביצירת קובץ האקסל: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void questionsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && questionsGrid.Rows.Count > e.RowIndex)
                {
                    var row = questionsGrid.Rows[e.RowIndex];
                    selectedQuestionId = row.Cells[0].Value?.ToString();
                    txtQuestion.Text = row.Cells[1].Value?.ToString();
                    comboBoxType.Text = row.Cells[2].Value?.ToString();
                    comboBoxDifficulty.Text = row.Cells[4].Value?.ToString();

                    SetAnswerFromExcelData(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בטעינת השאלה: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(selectedQuestionId))
                {
                    MessageBox.Show("לא נבחרה שאלה לעריכה.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (IsFormIncomplete())
                {
                    MessageBox.Show("אנא מלאו את כל השדות", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UpdateQuestionInExcel();
                MessageBox.Show("השאלה עודכנה בהצלחה.", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadQuestionsForCategory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בעדכון השאלה: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(selectedQuestionId))
                {
                    MessageBox.Show("לא נבחרה שאלה למחיקה.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show("האם אתה בטוח שברצונך למחוק את השאלה?", "אישור מחיקה",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (var workbook = new XLWorkbook(filePath))
                    {
                        var ws = workbook.Worksheet("Questions");
                        var row = ws.RowsUsed().Skip(1).FirstOrDefault(r => r.Cell(1).GetString() == selectedQuestionId);
                        if (row != null)
                        {
                            row.Delete();
                            workbook.Save();
                        }
                    }

                    MessageBox.Show("השאלה נמחקה בהצלחה.", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    swoosh.Play();
                    ClearForm();
                    LoadQuestionsForCategory();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה במחיקת השאלה: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsFormIncomplete()
        {
            if (string.IsNullOrWhiteSpace(txtQuestion.Text) ||
                comboBoxType.SelectedIndex == -1 ||
                comboBoxDifficulty.SelectedIndex == -1)
                return true;

            switch (comboBoxType.SelectedIndex)
            {
                case 0:
                    return string.IsNullOrWhiteSpace(txtOpenAnswer?.Text);

                case 1:
                    if (txtChoices == null) return true;
                    return txtChoices.Any(txt => string.IsNullOrWhiteSpace(txt.Text) ||
                                                txt.Text.StartsWith("אפשרות")) ||
                           selectedChoice == -1;

                case 2:
                    return trueFalseAnswer == null;

                default:
                    return true;
            }
        }

        private void ClearForm()
        {
            txtQuestion.Clear();
            if (txtOpenAnswer != null) txtOpenAnswer.Clear();

            if (txtChoices != null && btnChoices != null)
            {
                var colors = new Color[]
                {
                    Color.FromArgb(52, 152, 219),
                    Color.FromArgb(46, 204, 113),
                    Color.FromArgb(155, 89, 182),
                    Color.FromArgb(230, 126, 34)
                };

                for (int idx = 0; idx < txtChoices.Length; idx++)
                {
                    txtChoices[idx].Text = $"אפשרות {idx + 1}";
                    txtChoices[idx].ForeColor = Color.Gray;
                    btnChoices[idx].Text = Convert.ToChar(65 + idx).ToString();
                    btnChoices[idx].BackColor = colors[idx];
                }
            }
            selectedChoice = -1;

            if (btnTrue != null && btnFalse != null)
            {
                trueFalseAnswer = null;
                btnTrue.BackColor = Color.FromArgb(46, 204, 113);
                btnFalse.BackColor = Color.FromArgb(231, 76, 60);
            }

            comboBoxType.SelectedIndex = -1;
            comboBoxDifficulty.SelectedIndex = -1;
            selectedQuestionId = null;

            if (txtOpenAnswer != null) txtOpenAnswer.Visible = false;
            if (pnlMultipleChoice != null) pnlMultipleChoice.Visible = false;
            if (pnlTrueFalse != null) pnlTrueFalse.Visible = false;
        }

        public void PreselectCategory(string category)
        {
            // במקום להציג את ממשק הקטגוריות, עובר ישירות לעריכת הקטגוריה המבוקשת
            _selectedCategory = category;
            ShowQuestionEditor();
            LoadQuestionsForCategory();
        }

        // Event handlers (נדרשים עבור designer)
        private void txtQuestion_TextChanged(object sender, EventArgs e) { }
        private void comboBoxDifficulty_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtAnswer_TextChanged(object sender, EventArgs e) { }
        private void questionsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void CreateQuestion_Load_1(object sender, EventArgs e) { }
    }
}