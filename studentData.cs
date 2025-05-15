using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using ClosedXML.Excel;



namespace Student_data
{
    public partial class studentData : Form
    {
        public studentData()
        {
            InitializeComponent();
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            ShowStudentGrades();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var data = LoadStudentAverageData_ByColumns();
            if (data != null)
            {
                dataGridView2.Visible = true;
                dataGridView1.Visible = false;
                dataGridView3.Visible = false;
                dataGridView2.DataSource = data;
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView3.Visible = true;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            ShowStatisticsToGrid();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ShowStudentGrades()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.xlsx");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("הקובץ database.xlsx לא נמצא על שולחן‑העבודה.",
                                "קובץ לא קיים", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var wb = new XLWorkbook(filePath))
                {
                    if (!wb.Worksheets.Contains("studentData"))
                    {
                        MessageBox.Show("הגיליון studentData לא נמצא בקובץ.", "שגיאה");
                        return;
                    }

                    var ws = wb.Worksheet("studentData");
                    var range = ws.RangeUsed();

                    dataGridView1.Visible = true;
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();

                    // הוספת העמודות לפי השורה הראשונה
                    for (int c = 1; c <= range.ColumnCount(); c++)
                        dataGridView1.Columns.Add("C" + c, range.Cell(1, c).GetString());

                    // הוספת השורות (מהשורה השנייה)
                    foreach (var row in range.RowsUsed().Skip(1))
                    {
                        var vals = row.Cells().Select(cell => (object)cell.Value).ToArray();
                        dataGridView1.Rows.Add(vals);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("אירעה שגיאה בטעינת הגיליון:\n" + ex.Message,
                                "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable LoadStudentAverageData_ByColumns()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.xlsx");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("הקובץ database.xlsx לא נמצא על שולחן‑העבודה.",
                                "קובץ לא קיים", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            try
            {
                using (var wb = new XLWorkbook(filePath))
                {
                    if (!wb.Worksheets.Contains("studentData"))
                    {
                        MessageBox.Show("הגיליון studentData לא נמצא בקובץ.", "שגיאה");
                        return null;
                    }

                    var ws = wb.Worksheet("studentData");
                    var range = ws.RangeUsed();

                    DataTable dt = new DataTable();
                    foreach (var cell in range.Row(1).Cells())
                        dt.Columns.Add(cell.GetString());

                    foreach (var row in range.RowsUsed().Skip(1))
                    {
                        var dataRow = dt.NewRow();
                        for (int i = 0; i < range.ColumnCount(); i++)
                        {
                            dataRow[i] = row.Cell(i + 1).Value;
                        }
                        dt.Rows.Add(dataRow);
                    }

                    // חישוב ממוצע לפי עמודה A (אינדקס 0) ועמודה C (אינדקס 2)
                    var resultQuery = dt.AsEnumerable()
                        .GroupBy(r => r[0].ToString())  // עמודה A - שם תלמיד
                        .Select(g => new
                        {
                            Student = g.Key,
                            Average = g.Average(r => Convert.ToDouble(r[2]))  // עמודה C - ציון
                        })
                        .OrderByDescending(x => x.Average);

                    DataTable result = new DataTable();
                    result.Columns.Add("שם תלמיד");
                    result.Columns.Add("ממוצע ציונים");

                    foreach (var item in resultQuery)
                    {
                        result.Rows.Add(item.Student, item.Average.ToString("F2"));
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("אירעה שגיאה:\n" + ex.Message);
                return null;
            }
        }
        private void ShowStatisticsToGrid()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.xlsx");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("הקובץ database.xlsx לא נמצא על שולחן‑העבודה.",
                                "קובץ לא קיים", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var wb = new XLWorkbook(filePath))
                {
                    var ws = wb.Worksheet("studentData");
                    var range = ws.RangeUsed();

                    List<double> grades = new List<double>();
                    HashSet<string> students = new HashSet<string>();

                    foreach (var row in range.RowsUsed().Skip(1))
                    {
                        string student = row.Cell(1).GetString().Trim();   // עמודה A
                        string gradeText = row.Cell(3).GetString().Trim(); // עמודה C

                        // בדיקה מחמירה - גם ריק, גם טקסט לא חוקי, גם רווחים מוסתרים
                        if (!string.IsNullOrWhiteSpace(gradeText) && double.TryParse(gradeText, out double grade))
                        {
                            grades.Add(grade);
                            students.Add(student);
                        }
                        else
                        {
                            // רישום לוג שקט או הצגת הודעה (אם תרצי)
                            // MessageBox.Show($"ציון לא חוקי לתלמיד {student}. הערך: '{gradeText}'");
                            continue; // מדלג על שורה לא תקינה
                        }
                    }

                    if (grades.Count == 0)
                    {
                        MessageBox.Show("לא נמצאו ציונים חוקיים.", "שגיאה");
                        return;
                    }

                    double avg = grades.Average();
                    double min = grades.Min();
                    double max = grades.Max();
                    double stdDev = Math.Sqrt(grades.Average(v => Math.Pow(v - avg, 2)));
                    int totalGrades = grades.Count;
                    int uniqueStudents = students.Count;
                    double successRate = (grades.Count(g => g >= 60) / (double)grades.Count) * 100;

                    // יצירת DataTable עם סטטיסטיקות
                    DataTable dtStats = new DataTable();
                    dtStats.Columns.Add("סטטיסטיקה");
                    dtStats.Columns.Add("ערך");

                    dtStats.Rows.Add("ממוצע ציונים כללי", avg.ToString("F2"));
                    dtStats.Rows.Add("ציון מקסימלי", max);
                    dtStats.Rows.Add("ציון מינימלי", min);
                    dtStats.Rows.Add("סטיית תקן", stdDev.ToString("F2"));
                    dtStats.Rows.Add("מספר ציונים כולל", totalGrades);
                    dtStats.Rows.Add("מספר תלמידים ייחודיים", uniqueStudents);
                    dtStats.Rows.Add("אחוז הצלחה (60+)", successRate.ToString("F2") + "%");

                    // הצגה ב-DataGridView3
                    dataGridView3.Visible = true;
                    dataGridView3.DataSource = dtStats;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("אירעה שגיאה:\n" + ex.Message);
            }
        }





    }
}