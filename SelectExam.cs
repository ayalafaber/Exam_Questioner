using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Media;

namespace Exam_Questioner
{

    public partial class SelectExam : Form
    {
        // הוספת Label לכותרת המבחן
        private Label lblExamHeader;
        private SoundPlayer swoosh;

        public SelectExam()
        {
            InitializeComponent();
            InitializeExamHeader();
            swoosh = new SoundPlayer(Properties.Resources.swoosh);
        }

        // יצירת Label לכותרת המבחן
        private void InitializeExamHeader()
        {
            lblExamHeader = new Label();
            lblExamHeader.Name = "lblExamHeader";
            lblExamHeader.Font = new Font("Arial", 14F, FontStyle.Bold);
            lblExamHeader.ForeColor = Color.FromArgb(52, 152, 219);
            lblExamHeader.BackColor = Color.White;
            lblExamHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblExamHeader.AutoSize = false;
            lblExamHeader.Visible = false;
            lblExamHeader.BorderStyle = BorderStyle.FixedSingle;
            lblExamHeader.Padding = new Padding(10);

            // הוספת הכותרת לטופס
            this.Controls.Add(lblExamHeader);
            lblExamHeader.BringToFront();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. אתחול ComboBox של רמת הקושי
            comboBox2.Items.AddRange(new object[] { "קל", "בינוני", "קשה" });
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            // 2. קביעת נתיב לקובץ ה-Excel
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "database.xlsx");

            // 3. פתיחת חיבור ל-Excel עם ClosedXML
            using (var wb = new XLWorkbook(filePath))
            {
                // 4. קבלת הגיליון "Categories" (יצירה אוטומטית אם חסר)
                var wsCats = wb.Worksheets
                               .FirstOrDefault(ws => ws.Name == "Categories")
                           ?? wb.Worksheets.Add("Categories");

                // 5. אם הגיליון הזה ריק לגמרי, נייצר שורה ראשונה של כותרת
                if (wsCats.Column(1).CellsUsed().Count() == 0)
                {
                    wsCats.Cell(1, 1).Value = "Category";  // כותרת לתא A1
                    wb.Save();                             // שמירה אם הוספת כותרת
                }

                // 6. קריאת כל הקטגוריות מהעמודה A, החל משורה 2
                var categories = wsCats
                    .Column(1)
                    .CellsUsed()
                    .Skip(1)                             // דילוג על הכותרת
                    .Select(c => c.GetString().Trim())
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Distinct()
                    .ToArray();

                // 7. הוספת הקטגוריות ל-ComboBox1 ואז הוספת האפשרות "אחר" ו"רנדומלי"
                comboBox1.Items.AddRange(categories.Cast<object>().ToArray());
                comboBox1.Items.Add("רנדומלי");
                comboBox1.Items.Add("אחר");
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox1.Items.Insert(0, "רנדומלי");

            }

            // 8. כפתורים כבויים עד לבחירת פריט ב-ListBox
            btnViewExam.Enabled = btnDeleteExam.Enabled = false;
        }

        // אירועי הכפתורים הראשיים
        private void btnRandomExam_Click(object sender, EventArgs e)
        {
            // הסתרת פאנלים אחרים
            panelExamsList.Visible = false;
            dataGridView1.Visible = false;
            btnCloseDataGrid.Visible = false;
            lblExamHeader.Visible = false; // הסתרת הכותרת

            // הצגת פאנל מבחן רנדומלי
            panelRandomExam.Visible = true;
            panelRandomExam.BringToFront();
        }

        private void btnCreateQuestions_Click(object sender, EventArgs e)
        {
            CreateQuestion form = new CreateQuestion();
            form.Show();
        }

        private void btnViewExams_Click(object sender, EventArgs e)
        {
            // הסתרת פאנלים אחרים
            panelRandomExam.Visible = false;
            dataGridView1.Visible = false;
            btnCloseDataGrid.Visible = false;
            lblExamHeader.Visible = false; // הסתרת הכותרת

            // הצגת פאנל רשימת מבחנים
            panelExamsList.Visible = true;
            panelExamsList.BringToFront();

            // טעינת רשימת המבחנים
            LoadExamsList();
        }

        // אירועי פאנל מבחן רנדומלי
        private void btnClosePanel_Click(object sender, EventArgs e)
        {
            panelRandomExam.Visible = false;
        }

        private void btnCreateRandomExam_Click(object sender, EventArgs e)
        {
            // 1. קלט בסיסי
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("בחר נושא.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("בחר רמת קושי.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string subject = comboBox1.Text;
            string difficulty = comboBox2.Text;
            string countText = textBox1.Text;

            // בדיקת תקינות מספר השאלות
            if (!int.TryParse(countText, out int questionCount) || questionCount < 4 || questionCount > 12)
            {
                MessageBox.Show("מספר השאלות חייב להיות בין 4 ל‑12.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // אם בחרנו ברנדומלי – בחר נושא אקראי
            if (subject == "רנדומלי")
            {
                var candidates = comboBox1.Items
                    .Cast<string>()
                    .Where(s => s != "רנדומלי" && s != "אחר")
                    .ToList();
                var rnd = new Random();
                subject = candidates[rnd.Next(candidates.Count)];
            }

            // 2. "אחר" → בקש נושא חדש, הודע, פתח CreateQuestion נעול עם יעד שאלות
            if (subject == "אחר")
            {
                string input = Interaction.InputBox(
                    "הקלד נושא חדש בעברית בלבד:",
                    "נושא חדש"
                ).Trim();

                if (string.IsNullOrWhiteSpace(input) ||
                    !Regex.IsMatch(input, @"^[\u0590-\u05FF\s]+$"))
                {
                    MessageBox.Show("נושא לא תקין.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // הוספת הנושא לפני "אחר" אם לא קיים
                if (!comboBox1.Items.Contains(input))
                {
                    int idx = comboBox1.Items.IndexOf("אחר");
                    comboBox1.Items.Insert(idx, input);

                    // שמירת הנושא לגיליון "Categories"
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = Path.Combine(desktop, "database.xlsx");
                    using (var wb = new XLWorkbook(filePath))
                    {
                        var wsCats = wb.Worksheets
                                       .FirstOrDefault(ws => ws.Name == "Categories")
                                   ?? wb.Worksheets.Add("Categories");
                        int lastRow = wsCats.LastRowUsed()?.RowNumber() ?? 1;
                        wsCats.Cell(lastRow + 1, 1).Value = input;
                        wb.Save();
                    }
                }

                MessageBox.Show(
                    $"נושא חדש '{input}' נוצר!\nכעת תצטרך ליצור {questionCount} שאלות ברמת קושי '{difficulty}' כדי ליצור את המבחן.",
                    "נושא חדש",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // פתח CreateQuestion עם יעד שאלות ספציפי
                var cq = new CreateQuestion();
                cq.SetTargetQuestions(input, difficulty, questionCount);
                cq.ShowDialog();
                return;
            }

                // לאחר סגירת CreateQuestion, נסה ליצור את המבחן
                bool ok = ExamGeneratorLogic.TryCreateExam(
                    input,
                    difficulty,
                    countText,
                    comboBox1.Items.Cast<string>().ToList(),
                    comboBox2.Items.Cast<string>().ToList(),
                    out string message,
                    out string examId
                );

            // 5. תוצאה
                if (ok)
                {
                    MessageBox.Show($"מעולה! המבחן נוצר בהצלחה!\n{message}", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    panelRandomExam.Visible = false;

                    // רענון רשימת המבחנים אם הפאנל פתוח
                    if (panelExamsList.Visible)
                    {
                        LoadExamsList();
                    }
                }
                else
                {
                    MessageBox.Show($"לא ניתן ליצור את המבחן:\n{message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            // 4. קריאה ללוגיקה למקרה רגיל (לא "אחר")
            bool success = ExamGeneratorLogic.TryCreateExam(
                subject,
                difficulty,
                countText,
                comboBox1.Items.Cast<string>().ToList(),
                comboBox2.Items.Cast<string>().ToList(),
                out string resultMessage,
                out string resultExamId
            );

            // 5. תוצאה
            if (success)
            {
                MessageBox.Show(resultMessage, "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panelRandomExam.Visible = false;

                // רענון רשימת המבחנים אם הפאנל פתוח
                if (panelExamsList.Visible)
                {
                    LoadExamsList();
                }
            }
            else
            {
                MessageBox.Show(resultMessage, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // אירועי פאנל רשימת מבחנים
        private void btnCloseExamsList_Click(object sender, EventArgs e)
        {
            panelExamsList.Visible = false;
        }

        private void listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool hasSelection = listbox.SelectedIndex != -1;
            btnViewExam.Enabled = hasSelection;
            btnDeleteExam.Enabled = hasSelection;
        }

        private void btnViewExam_Click(object sender, EventArgs e)
        {
            // 1. בדיקת בחירה
            var itemText = listbox.SelectedItem as string;
            if (string.IsNullOrEmpty(itemText))
            {
                MessageBox.Show("בחר קודם מבחן מהרשימה.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. בניית נתיב לקובץ Excel על שולחן-העבודה
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");
            if (!File.Exists(filePath))
            {
                MessageBox.Show("הקובץ לא נמצא.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. פריסה של המזהה שנבחר (חלק לפני " - ")
            string[] parts = itemText.Split(new[] { " - " }, StringSplitOptions.None);
            string examId = parts[0].Trim();
            string category = parts.Length > 1 ? parts[1].Trim() : "";
            string difficulty = parts.Length > 2 ? parts[2].Trim() : "";

            using (var wb = new XLWorkbook(filePath))
            {
                // 4. אימות קיום הגיליון בשם ExamId
                if (!wb.Worksheets.Contains(examId))
                {
                    MessageBox.Show("המבחן לא קיים במערכת.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var ws = wb.Worksheet(examId);

                // 5. חישוב מספר השורות והעמודות בשימוש
                int lastRow = ws.LastRowUsed().RowNumber();         // השורה האחרונה עם נתונים
                int lastCol = ws.LastColumnUsed().ColumnNumber();   // העמודה האחרונה עם נתונים

                // 6. הכנה לתצוגה: הופך גליון לגלוי ומנקה DataGridView
                panelExamsList.Visible = false;

                // הגדרת הכותרת והצגתה
                lblExamHeader.Text = $"קטגוריה: {category} | רמת קושי: {difficulty}";
                lblExamHeader.Location = new Point(dataGridView1.Location.X + 65, dataGridView1.Location.Y + 140);
                lblExamHeader.Size = new Size(dataGridView1.Width, 50);
                lblExamHeader.Visible = true;
                lblExamHeader.BringToFront();

                dataGridView1.Visible = true;
                btnCloseDataGrid.Visible = true;
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                // עיצוב הטבלה
                StyleDataGridView();

                for (int col = 1; col <= lastCol - 3; col++)
                {
                    if (col == 4 || col == 3)
                        continue;
                    string header = ws.Cell(1, col).GetString();
                    var column = new DataGridViewTextBoxColumn();
                    column.Name = $"C{col}";
                    column.HeaderText = header;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns.Add(column);
                }

                // 8. הוספת שורות נתונים משורה 2 ועד השורה האחרונה
                for (int row = 2; row <= lastRow; row++)
                {
                    List<object> values = new List<object>();

                    for (int col = 1; col <= lastCol - 3; col++)
                    {
                        if (col == 4 || col == 3)
                            continue;

                        values.Add(ws.Cell(row, col).Value);
                    }

                    int rowIndex = dataGridView1.Rows.Add(values.ToArray());

                    // צביעת שורות לסירוגין
                    if (rowIndex % 2 == 0)
                    {
                        dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(249, 249, 249);
                    }
                    else
                    {
                        dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
                    }
                    dataGridView1.Rows.Add(values);
                }
            }
        }

        private void btnDeleteExam_Click(object sender, EventArgs e)
        {
            var itemText = listbox.SelectedItem as string;
            var examId = itemText.Split(new[] { " - " }, StringSplitOptions.None)[0].Trim();
            if (string.IsNullOrEmpty(examId))
            {
                MessageBox.Show("בחר קודם מבחן למחיקה.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                    $"אתה בטוח שברצונך למחוק את המבחן {examId}?",
                    "אישור מחיקה",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                ) != DialogResult.Yes)
                return;

            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");
            if (!File.Exists(filePath))
            {
                MessageBox.Show("הקובץ לא נמצא.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var wb = new XLWorkbook(filePath))
            {
                // 1. מחיקת הגיליון עצמו, אם קיים
                if (wb.Worksheets.Contains(examId))
                    wb.Worksheets.Delete(examId);

                // 2. מחיקת השורה מגליון "ExamID"
                var wsIds = wb.Worksheet("ExamID");
                var cell = wsIds
                    .Column(1)
                    .CellsUsed()
                    .FirstOrDefault(c => c.GetString() == examId);
                cell?.WorksheetRow().Delete();

                wb.Save();
            }

            // 3. עדכון ה-ListBox
            listbox.Items.Remove(itemText);
            MessageBox.Show($"המבחן {examId} נמחק בהצלחה.", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
            swoosh.Play();
        }

        // אירוע סגירת DataGrid
        private void btnCloseDataGrid_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            btnCloseDataGrid.Visible = false;
            lblExamHeader.Visible = false; // הסתרת הכותרת
            panelExamsList.Visible = true;
        }

        // פונקציה לטעינת רשימת מבחנים
        private void LoadExamsList()
        {
            // 1. נתיב מלא לקובץ בשולחן‑העבודה
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "database.xlsx");

            // 2. בדיקה שהקובץ אכן קיים
            if (!File.Exists(filePath))
            {
                MessageBox.Show("הקובץ database.xlsx לא נמצא על שולחן‑העבודה.",
                                "קובץ לא קיים", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. קריאה וטעינה ל‑ListBox
            try
            {
                listbox.Items.Clear();
                listbox.Enabled = true;
                listbox.Visible = true;
                button4.Visible = true;
                button5.Visible = true;

                using (var wb = new XLWorkbook(filePath))
                {
                    var ws = wb.Worksheet("ExamID");
                    foreach (var row in ws.RangeUsed().RowsUsed().Skip(1))  // דילוג על כותרת
                    {
                        var id = row.Cell(1).GetString();  // עמודה A
                        var category = row.Cell(2).GetString();  // עמודה B
                        var difficulty = row.Cell(3).GetString();  // עמודה C
                        listbox.Items.Add($"{id} - {category} - {difficulty}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("אירעה שגיאה בטעינת הקובץ:\n" + ex.Message,
                                "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // אירועי ComboBox
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // רק כשבוחרים "אחר" ישירות מה-ComboBox (לא דרך כפתור יצירת מבחן):
            if (comboBox1.Text == "אחר")
            {
                // 1. בקש נושא חדש
                string input = Interaction.InputBox(
                    "הקלד נושא חדש בעברית בלבד:",
                    "נושא חדש"
                ).Trim();

                // 2. בדיקת תקינות
                if (string.IsNullOrWhiteSpace(input) ||
                    !Regex.IsMatch(input, @"^[\u0590-\u05FF\s]+$"))
                {
                    MessageBox.Show("נושא לא תקין.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBox1.SelectedIndex = -1;
                    return;
                }

                // 3. הוספת הנושא ל-ComboBox1 לפני הפריט "אחר" (אם לא קיים)
                if (!comboBox1.Items.Contains(input))
                {
                    int idx = comboBox1.Items.IndexOf("אחר");
                    comboBox1.Items.Insert(idx, input);
                }

                // 4. בחירה של הנושא החדש
                comboBox1.SelectedItem = input;

                // 5. שמירת הנושא לגיליון "Categories"
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktop, "database.xlsx");
                using (var wb = new XLWorkbook(filePath))
                {
                    var wsCats = wb.Worksheets
                                   .FirstOrDefault(ws => ws.Name == "Categories")
                               ?? wb.Worksheets.Add("Categories");
                    int lastRow = wsCats.LastRowUsed()?.RowNumber() ?? 1;
                    wsCats.Cell(lastRow + 1, 1).Value = input;
                    wb.Save();
                }

                // 6. בדיקה אם יש רמת קושי ומספר שאלות - אם כן פתח עם יעד
                if (comboBox2.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                    int.TryParse(textBox1.Text, out int count) && count >= 4 && count <= 12)
                {
                    // יש פרטי יעד - פתח עם יעד שאלות
                    MessageBox.Show(
                        $"נושא חדש '{input}' נוצר!\nכעת תצטרך ליצור {count} שאלות ברמת קושי '{comboBox2.Text}'.",
                        "נושא חדש",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    var cq = new CreateQuestion();
                    cq.SetTargetQuestions(input, comboBox2.Text, count);
                    cq.ShowDialog();
                }
                else
                {
                    // אין פרטי יעד מלאים - פתח רגיל
                    MessageBox.Show(
                        "נושא חדש נוצר, אנא צור לפחות 4 שאלות בנושא זה על מנת ליצור מבחן חדש",
                        "נושא חדש",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                // 7. מעבר לטופס יצירת שאלות עם קטגוריה נעולה
                    var cq = new CreateQuestion();
                    cq.PreselectCategory(input);
                    cq.ShowDialog();
                }

                // 8. איפוס הבחירה בחזרה (רק אם לא נבחרו פרטי יעד)
                if (comboBox2.SelectedIndex == -1 || string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    comboBox1.SelectedIndex = -1;
                }
            }
        }





        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ללא פעולה מיוחדת
        }

        // אימות קלט TextBox
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int count) || count < 4 || count > 12)
            {
                MessageBox.Show("מספר השאלות חייב להיות מספר בין 4 ל־12.", "שגיאת קלט", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;                  // חוסם עזיבת השדה עד לתיקון
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // ללא פעולה מיוחדת
        }

        // פונקציה לעיצוב הטבלה
        private void StyleDataGridView()
        {
            // עיצוב כללי של הטבלה
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.GridColor = Color.FromArgb(230, 230, 230);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Font = new Font("Arial", 10F, FontStyle.Regular);

            // עיצוב הכותרות
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11F, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.EnableHeadersVisualStyles = false;

            // עיצוב התאים
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 51);
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10F, FontStyle.Regular);
            dataGridView1.DefaultCellStyle.Padding = new Padding(5);
            dataGridView1.RowTemplate.Height = 35;

            // עיצוב עמודות אלטרנטיביות
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 249, 249);
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 51);

            // הוספת צללים עדינים
            dataGridView1.CellPainting += DataGridView1_CellPainting;
        }

        // אירוע לצביעת תאים מותאמת אישית
        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // צביעת כותרות עם גרדיאנט
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Background);

                using (LinearGradientBrush brush = new LinearGradientBrush(
                    e.CellBounds,
                    Color.FromArgb(52, 152, 219),
                    Color.FromArgb(41, 128, 185),
                    LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                }

                // כתיבת הטקסט
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    e.Graphics.DrawString(
                        e.FormattedValue.ToString(),
                        new Font("Arial", 11F, FontStyle.Bold),
                        textBrush,
                        e.CellBounds,
                        sf);
                }

                e.Handled = true;
            }
        }

        // אירועים ישנים שנשארו לתאימות
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void panelBackground_Paint(object sender, PaintEventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void button1_Click(object sender, EventArgs e) => btnCreateRandomExam_Click(sender, e);
        private void button2_Click(object sender, EventArgs e) => btnCreateQuestions_Click(sender, e);
        private void button3_Click(object sender, EventArgs e) => btnViewExams_Click(sender, e);
        private void button4_Click(object sender, EventArgs e) => btnViewExam_Click(sender, e);
        private void button5_Click(object sender, EventArgs e) => btnDeleteExam_Click(sender, e);
        private void button6_Click(object sender, EventArgs e) => btnCloseDataGrid_Click(sender, e);
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) => listbox_SelectedIndexChanged(sender, e);

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void randomExamGroupBox_Enter(object sender, EventArgs e)
        {
            CreateQuestion form = new CreateQuestion();
            form.Show();

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }

}