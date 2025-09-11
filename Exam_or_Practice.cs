using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Text;

namespace Exam_Questioner
{
    public partial class Exam_or_Practice : Form
    {
        private string _username;
        private PracticeForm _practiceForm;
        private bool _noQuestionsMessageShown = false;
        private string _lastCheckedCategory = "";
        private string _lastCheckedDifficulty = "";

        public Exam_or_Practice(string username)
        {
            InitializeComponent();
            _username = username;
            this.Load += examORexercise_Load;
        }

        private void examORexercise_Load(object sender, EventArgs e)
        {
            // טעינת ערכי ComboBox
            LoadCategoriesFromExcel();

            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(new object[] { "קל", "בינוני", "קשה" });
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            // בברירת־מחדל הכפתורים כבויים
            button1.Enabled = false; // practice
            button2.Enabled = false; // toggle list
            button3.Enabled = false; // start exam

            listbox.Visible = false;
            listbox.SelectedIndexChanged += listbox_SelectedIndexChanged;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
        }

        // טעינת קטגוריות מהאקסל
        private void LoadCategoriesFromExcel()
        {
            comboBox1.Items.Clear();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");

            if (!File.Exists(path))
            {
                MessageBox.Show("לא נמצא קובץ database.xlsx", "שגיאה");
                return;
            }

            try
            {
                using (var wb = new XLWorkbook(path))
                {
                    // בדיקה שהלשונית Categories קיימת
                    if (!wb.Worksheets.Contains("Categories"))
                    {
                        MessageBox.Show("לא נמצאה לשונית 'Categories' בקובץ database.xlsx", "שגיאה");
                        return;
                    }

                    // טעינת קטגוריות מלשונית Categories
                    var categories = wb.Worksheet("Categories").Column(1).CellsUsed()
                        .Skip(1)
                        .Select(c => c.GetString().Trim())
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Distinct()
                        .OrderBy(c => c)
                        .ToList();

                    // הוספת הקטגוריות ל-ComboBox
                    foreach (var category in categories)
                    {
                        comboBox1.Items.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בטעינת הקטגוריות מהאקסל: {ex.Message}", "שגיאה");
            }
        }

        // בדיקה אם צריך להציג הודעה על אין שאלות
        private bool ShouldShowNoQuestionsMessage(string category, string difficulty)
        {
            // אם זה קטגוריה או רמת קושי חדשה, אפשר להציג הודעה שוב
            if (category != _lastCheckedCategory || difficulty != _lastCheckedDifficulty)
            {
                _noQuestionsMessageShown = false;
                _lastCheckedCategory = category;
                _lastCheckedDifficulty = difficulty;
            }

            return !_noQuestionsMessageShown;
        }

        // הצגת הודעה על אין שאלות
        private void ShowNoQuestionsMessage(string category, string difficulty)
        {
            if (ShouldShowNoQuestionsMessage(category, difficulty))
            {
                MessageBox.Show($"אין שאלות זמינות בנושא '{category}' ברמת קושי '{difficulty}'.",
                               "אין שאלות זמינות",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information);
                _noQuestionsMessageShown = true;
            }
        }
        private int GetExistingQuestionCount(string category, string difficulty)
        {
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");

            if (!File.Exists(path))
                return 0;

            try
            {
                using (var wb = new XLWorkbook(path))
                {
                    if (!wb.Worksheets.Contains("Questions"))
                        return 0;

                    var ws = wb.Worksheet("Questions");
                    return ws.RowsUsed().Skip(1).Count(row =>
                        row.Cell(4).GetString().Trim().Equals(category, StringComparison.OrdinalIgnoreCase) &&
                        row.Cell(5).GetString().Trim().Equals(difficulty, StringComparison.OrdinalIgnoreCase));
                }
            }
            catch
            {
                return 0;
            }
        }

        // כפתור התרגול - Practice window
        private void button1_Click(object sender, EventArgs e)
        {
            // בדיקה שיש שאלות זמינות לפני פתיחת התרגול
            string selectedCategory = comboBox1.Text;
            string selectedDifficulty = comboBox2.Text;

            int availableQuestions = GetExistingQuestionCount(selectedCategory, selectedDifficulty);

            if (availableQuestions == 0)
            {
                ShowNoQuestionsMessage(selectedCategory, selectedDifficulty);
                return;
            }

            if (availableQuestions == 0)
            {
                ShowNoQuestionsMessage(selectedCategory, selectedDifficulty);
                return;
            }
            // אם יש שאלות, פתח את חלון התרגול
            if (_practiceForm == null || _practiceForm.IsDisposed)
            {
                _practiceForm = new PracticeForm(comboBox1.Text, comboBox2.Text);
                _practiceForm.Show();
            }
        }

        // כפתור טעינת מבחנים - Toggle listbox visibility & load exams
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0 || comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("בחר נושא ורמת קושי לפני טעינת המבחנים.", "שגיאה");
                return;
            }

            string selectedSubject = comboBox1.Text;
            string selectedDifficulty = comboBox2.Text;

            // בדיקה שיש שאלות זמינות
            int availableQuestions = GetExistingQuestionCount(selectedSubject, selectedDifficulty);

            if (availableQuestions == 0)
            {
                ShowNoQuestionsMessage(selectedSubject, selectedDifficulty);
                return;
            }

            resultsGroupBox.Visible = true;

            listbox.Items.Clear();
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");

            if (File.Exists(path))
            {
                using (var wb = new XLWorkbook(path))
                {
                    var ws = wb.Worksheet("ExamID");
                    foreach (var row in ws.RangeUsed().RowsUsed().Skip(1))
                    {
                        var id = row.Cell(1).GetString();
                        var category = row.Cell(2).GetString();
                        var difficulty = row.Cell(3).GetString();

                        if (string.Equals(category, selectedSubject, StringComparison.OrdinalIgnoreCase)
                         && string.Equals(difficulty, selectedDifficulty, StringComparison.OrdinalIgnoreCase))
                        {
                            listbox.Items.Add($"{id} - {category} - {difficulty}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("לא נמצא קובץ database.xlsx", "שגיאה");
                return;
            }

            listbox.Visible = true;
            button3.Enabled = listbox.Items.Count > 0;
        }

        private void listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = listbox.SelectedIndex != -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var itemText = listbox.SelectedItem as string;
            if (string.IsNullOrEmpty(itemText))
            {
                MessageBox.Show("בחר מבחן מהרשימה.", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            // חילוץ examId, category ו־difficulty מהטקסט
            var parts = itemText.Split(new[] { " - " }, StringSplitOptions.None);
            string examId = parts[0].Trim();
            string category = parts.Length > 1 ? parts[1].Trim() : "";
            string difficulty = parts.Length > 2 ? parts[2].Trim() : "";

            // נתיב קובץ ה־Excel
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");

            // בדיקה אם כבר קיימת תוצאה
            bool hasGrade = ExamLogic.HasExistingGrade(
                filePath,
                _username,
                category,
                difficulty);

            if (hasGrade)
            {
                MessageBox.Show(
                    "כבר קיימת תוצאה עבורך במבחן זה (נושא: "
                    + category + ", קושי: " + difficulty + ").",
                    "אין גישה",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // רק עכשיו יוצרים ומציגים את החלון של המבחן
            using (var examForm = new Exam(examId, _username))
            {
                examForm.ShowDialog();
            }
        }


        // עדכון מצב הכפתורים
        private void UpdateButtonStates()
        {
            bool allSelected = comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1;

            button1.Enabled = allSelected; // practice
            button2.Enabled = allSelected; // toggle list
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        // אירועים ריקים
        private void label4_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void Exam_or_Practice_Load(object sender, EventArgs e) { }
        private void panelSelectors_Paint(object sender, PaintEventArgs e) { }
        private void panelMain_Paint(object sender, PaintEventArgs e) { }
        private void mainPanel_Paint(object sender, PaintEventArgs e) { }
    }
}