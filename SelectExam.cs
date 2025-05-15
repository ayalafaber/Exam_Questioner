using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.IO;
using System.Text.RegularExpressions;   
using Microsoft.VisualBasic;

namespace Exam_Questioner
{
    
    public partial class SelectExam : Form
    {
        
        public SelectExam()
        {
            InitializeComponent();
        }


        private void label1_Click(object sender, EventArgs e)
        {

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

                // 7. הוספת הקטגוריות ל-ComboBox1 ואז הוספת האפשרות "אחר"
                comboBox1.Items.AddRange(categories.Cast<object>().ToArray());
                comboBox1.Items.Add("אחר");
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            // 8. כפתורים כבויים עד לבחירת פריט ב-ListBox
            button4.Enabled = button5.Enabled = false;
        }



        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string subject = comboBox1.Text;
            string difficulty = comboBox2.Text;
            string questionCountText = textBox1.Text;

            var result = ExamGeneratorLogic.TryCreateExam(
                subject,
                difficulty,
                questionCountText,
                comboBox1.Items.Cast<string>().ToList(),
                comboBox2.Items.Cast<string>().ToList(),
                out string message,
                out string examId
            );

            if (result)
            {
                listbox.Items.Add($"{examId} - {subject} - {difficulty}");
                MessageBox.Show(message, "הצלחה");
            }
            else
            {
                MessageBox.Show(message, "שגיאה");
            }
        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool has = listbox.SelectedIndex != -1;
            button4.Enabled = button5.Enabled = has;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            // 1. בדיקת בחירה
            var itemText = listbox.SelectedItem as string;
            if (string.IsNullOrEmpty(itemText))
            {
                MessageBox.Show("בחר קודם מבחן מהרשימה.", "שגיאה");
                return;
            }

            // 2. בניית נתיב לקובץ Excel על שולחן-העבודה
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");
            if (!File.Exists(filePath))
            {
                MessageBox.Show("הקובץ לא נמצא.", "שגיאה");
                return;
            }

            // 3. פריסה של המזהה שנבחר (חלק לפני " - ")
            string examId = itemText.Split(new[] { " - " }, StringSplitOptions.None)[0].Trim();

            using (var wb = new XLWorkbook(filePath))
            {
                // 4. אימות קיום הגיליון בשם ExamId
                if (!wb.Worksheets.Contains(examId))
                {
                    MessageBox.Show("המבחן לא קיים במערכת.", "שגיאה");
                    return;
                }

                var ws = wb.Worksheet(examId);

                // 5. חישוב מספר השורות והעמודות בשימוש
                int lastRow = ws.LastRowUsed().RowNumber();         // השורה האחרונה עם נתונים
                int lastCol = ws.LastColumnUsed().ColumnNumber();   // העמודה האחרונה עם נתונים

                // 6. הכנה לתצוגה: הופך גליון לגלוי ומנקה DataGridView
                dataGridView1.Visible = true;
                button6.Visible = true;
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                // 7. הוספת כותרות עמודות מתוך שורה 1 (A עד I)
                for (int col = 1; col <= lastCol; col++)
                {
                    string header = ws.Cell(1, col).GetString();
                    dataGridView1.Columns.Add($"C{col}", header);
                }

                // 8. הוספת שורות נתונים משורה 2 ועד השורה האחרונה
                for (int row = 2; row <= lastRow; row++)
                {
                    object[] values = new object[lastCol];
                    for (int col = 1; col <= lastCol; col++)
                    {
                        values[col - 1] = ws.Cell(row, col).Value;
                    }
                    dataGridView1.Rows.Add(values);
                }
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            var itemText = listbox.SelectedItem as string;
            var examId = itemText.Split(new[] { " - " }, StringSplitOptions.None)[0].Trim();
            if (string.IsNullOrEmpty(examId))
            {
                MessageBox.Show("בחר קודם מבחן למחיקה.", "שגיאה");
                return;
            }

            if (MessageBox.Show(
                    $"אתה בטוח שברצונך למחוק את המבחן {examId}?",
                    "אישור מחיקה",
                    MessageBoxButtons.YesNo
                ) != DialogResult.Yes)
                return;

            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");
            if (!File.Exists(filePath))
            {
                MessageBox.Show("הקובץ לא נמצא.", "שגיאה");
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

            // 3. עדכון ה-ListBox ו-DataGridView
            listbox.Items.Remove(itemText);
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            MessageBox.Show($"המבחן {examId} נמחק בהצלחה.", "הצלחה");
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // 1. נתיב מלא לקובץ בשולחן‑העבודה
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "database.xlsx");   // התאמה לשם הקובץ שלך

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // אם המשתמש בחר באפשרות "אחר"
            if (comboBox1.Text == "אחר")
            {
                // 1. נטרל כפתור יצירת המבחן עד שתשלים את הנושא
                button1.Enabled = false;

                // 2. בקש מהמשתמש להזין נושא חדש
                string input = Interaction.InputBox(
                    "הקלד נושא חדש בעברית בלבד:",
                    "נושא חדש"
                ).Trim();

                // 3. בדיקת תקינות (רק אותיות עבריות ומרווחים)
                if (string.IsNullOrWhiteSpace(input) ||
                    !Regex.IsMatch(input, @"^[\u0590-\u05FF\s]+$"))
                {
                    MessageBox.Show("נושא לא תקין.");
                    comboBox1.SelectedIndex = -1;   // איפוס הבחירה
                    button1.Enabled = true;         // החזרת כפתור לסטנדרט
                    return;
                }

                // 4. הוספת הנושא ל-ComboBox1 אם לא קיים כבר
                if (!comboBox1.Items.Contains(input))
                {
                    // הכנס לפני "אחר" כדי ששמרה של "אחר" תישאר בסוף
                    int idx = comboBox1.Items.IndexOf("אחר");
                    comboBox1.Items.Insert(idx, input);
                }
                comboBox1.SelectedItem = input;

                // 5. שמירת הנושא החדש לגיליון "Categories" ב-Excel
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktop, "database.xlsx");
                using (var wb = new XLWorkbook(filePath))
                {
                    var wsCats = wb.Worksheets
                                   .FirstOrDefault(ws => ws.Name == "Categories")
                               ?? wb.Worksheets.Add("Categories");

                    // מציאת השורה האחרונה המלאה בעמודה A
                    int lastRow = wsCats.LastRowUsed()?.RowNumber() ?? 1;
                    // הוספת ערך חדש בשורה הבאה
                    wsCats.Cell(lastRow + 1, 1).Value = input;
                    wb.Save();
                }

                // 6. החזרת כפתור יצירת המבחן למצב פעיל
                button1.Enabled = true;
            }
            else
            {
                // כל בחירה אחרת – הכפתור מופעל כרגיל
                button1.Enabled = true;
            }
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int count) || count < 4 || count > 12)
            {
                MessageBox.Show("מספר השאלות חייב להיות מספר בין 4 ל־12.", "שגיאת קלט");
                e.Cancel = true;                  // חוסם עזיבת השדה עד לתיקון
            }
        }



        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            listbox.Enabled = true;
            button4.Enabled = listbox.SelectedIndex != -1;
            button5.Enabled = listbox.SelectedIndex != -1;
            button6.Visible = false;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateQuestion form = new CreateQuestion();
            form.Show();

        }
    }

}
