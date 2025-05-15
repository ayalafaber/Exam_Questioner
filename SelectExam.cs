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
            // --- אתחול תיבות הבחירה ------------------------------------------
            comboBox1.Items.AddRange(new object[]
            { "אנגלית", "מתמטיקה", "תכנות", "היסטוריה", "רנדומלי", "אחר" });
            comboBox2.Items.AddRange(new object[] { "קל", "בינוני", "קשה" });
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            // --- טעינת מזהי המבחנים ל־ListBox1 -------------------------------
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = @"C:\Users\nevoi\OneDrive\שולחן העבודה\database.xlsx";
            if (File.Exists(filePath))
            {
                using (var wb = new XLWorkbook(filePath))
                {
                    var wsIds = wb.Worksheets.FirstOrDefault(ws => ws.Name == "ExamID");
                    if (wsIds != null)
                    {
                        var ids = wsIds
                            .Column(1)
                            .CellsUsed()
                            .Select(c => c.GetString())
                            .ToArray();
                        listbox.Items.AddRange(ids);
                    }
                }
            }
            else
            {
                MessageBox.Show("לא נמצא הקובץ database.xlsx על שולחן העבודה.", "שגיאה");
            }

            // --- כפתורים כבויים עד לבחירת מבחן -------------------------------
            button4.Enabled = false;   // הצג מבחן
            button5.Enabled = false;   // מחק מבחן
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
            var itemText = listbox.SelectedItem as string;
            if (string.IsNullOrEmpty(itemText))
            {
                MessageBox.Show("בחר קודם מבחן מהרשימה.", "שגיאה");
                return;
            }

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"database.xlsx");
            if (!File.Exists(filePath))
            {
                MessageBox.Show("הקובץ לא נמצא.", "שגיאה");
                return;
            }

            var examId = itemText.Split(new[] { " - " }, StringSplitOptions.None)[0].Trim();
            using (var wb = new XLWorkbook(filePath))
            {
                if (!wb.Worksheets.Contains(examId))
                {
                    MessageBox.Show("המבחן לא קיים במערכת.", "שגיאה");
                    return;
                }

                var ws = wb.Worksheet(examId);
                var range = ws.RangeUsed();
                dataGridView1.Visible = true;
                button6.Visible = true;
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                // הוספת העמודות לפי שורה 1
                for (int c = 1; c <= range.ColumnCount(); c++)
                    dataGridView1.Columns.Add("C" + c, range.Cell(1, c).GetString());

                // הוספת שורות (מהשורה השנייה ואילך)
                foreach (var row in range.RowsUsed().Skip(1))
                {
                    var vals = row.Cells().Select(cell => (object)cell.Value).ToArray();
                    dataGridView1.Rows.Add(vals);
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
            if (comboBox1.Text == "אחר")
            {
                button1.Enabled = false;                 // מבטל יצירה רנדומלית
                string input = Interaction.InputBox("הקלד נושא חדש בעברית בלבד:", "נושא חדש");
                if (string.IsNullOrWhiteSpace(input) ||
                    !Regex.IsMatch(input, @"^[\u0590-\u05FF\s]+$"))
                {
                    MessageBox.Show("נושא לא תקין.");   // שחזור המצב
                    comboBox1.SelectedIndex = -1;
                    button1.Enabled = true;
                    return;
                }
                if (!comboBox1.Items.Contains(input))
                    comboBox1.Items.Insert(0, input);    // מוסיף לרשימה
                comboBox1.SelectedItem = input;
            }
            else
            {
                button1.Enabled = true;                  // מאפשר חזרה
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
