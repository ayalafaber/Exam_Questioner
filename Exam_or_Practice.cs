using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Exam_Questioner
{
    public partial class Exam_or_Practice : Form
    {
        private PracticeForm _practiceForm;

        public Exam_or_Practice()
        {
            InitializeComponent();
            // וודאו שבאירוע הזה מחובר ב–Designer:
            this.Load += examORexercise_Load;
        }

        private void examORexercise_Load(object sender, EventArgs e)
        {
            // --------------------------------------------------------------------
            // 1. טענת ערכי ComboBox
            // --------------------------------------------------------------------
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new object[] { "בדיקות", "תכנות", "עקרונות", "מבנה נתונים" });
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(new object[] { "קל", "בינוני", "קשה" });
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            // בברירת־מחדל הכפתורים כבויים
            button1.Enabled = false; // practice
            button2.Enabled = true;  // toggle list
            button3.Enabled = false; // start exam

            listbox.Visible = false;
            listbox.SelectedIndexChanged += listbox_SelectedIndexChanged;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
        }

        // --------------------------------------------------------------------
        // 2. Practice window toggle
        // --------------------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            // אם החלון לא קיים או נסגר – פותחים
            if (_practiceForm == null || _practiceForm.IsDisposed)
            {
                _practiceForm = new PracticeForm();
                _practiceForm.Show();
                button1.Text = "סגור תרגול";
            }
            else // נסגר
            {
                _practiceForm.Close();
                button1.Text = "פתח תרגול";
            }
        }

        // --------------------------------------------------------------------
        // 3. Toggle listbox visibility & load exams
        // --------------------------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            // 0. ודא שנבחר נושא וקושי
            if (comboBox1.SelectedIndex < 0 || comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("בחר נושא ורמת קושי לפני טעינת המבחנים.", "שגיאה");
                return;
            }

            // 1. קרא את הבחירות
            string selectedSubject = comboBox1.Text;
            string selectedDifficulty = comboBox2.Text;

            // 2. נקה ולטעון
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
                        // 3. רק אם מתאים
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

            // 4. הצג/עדכן כפתור התחלת מבחן
            listbox.Visible = true;
            listbox.Enabled = true;
            button3.Visible = true;
            button3.Enabled = listbox.Items.Count > 0;
        }


        // --------------------------------------------------------------------
        // 4. Enable “Start Exam” when user picks from list
        // --------------------------------------------------------------------
        private void listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = listbox.SelectedIndex != -1;
        }

        // --------------------------------------------------------------------
        // 5. Start the selected exam
        // --------------------------------------------------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            var itemText = listbox.SelectedItem as string;
            if (string.IsNullOrEmpty(itemText))
            {
                MessageBox.Show("בחר מבחן מהרשימה.", "שגיאה");
                return;
            }
            // מפצלים את המזהה לפני ה־" - "
            var examId = itemText.Split(new[] { " - " }, StringSplitOptions.None)[0].Trim();
            var examForm = new Exam(examId);
            examForm.ShowDialog();
        }

        // --------------------------------------------------------------------
        // 6. Enable Practice btn only when both comboBoxes מלאים
        // --------------------------------------------------------------------
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = comboBox1.SelectedIndex != -1
                           && comboBox2.SelectedIndex != -1;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = comboBox1.SelectedIndex != -1
                           && comboBox2.SelectedIndex != -1;
        }

        // שאר האירועים נשארים ריקים או יוסרו אם לא דרושים
        private void label4_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
    }
}
