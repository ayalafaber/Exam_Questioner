using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using ClosedXML.Excel;

namespace Exam_Questioner
{
    public partial class GradesTracker : Form
    {
        private List<GradeRecord> scores;
        private string loggedInUsername;

        public GradesTracker(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
        }

        // טעינת הטופס: קריאת קובץ הציונים, הצגת הטבלה והגרף
        private void GradesTracker_Load(object sender, EventArgs e)
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.xlsx");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("קובץ הנתונים לא נמצא!", "שגיאה");
                labelAverage.Text = "אין נתונים זמינים";
                return;
            }

            scores = LoadScoresFromExcel(filePath);
            var validScores = GradesLogic.FilterValidScores(scores);

            if (validScores.Any())
            {
                // הצגת ציונים בטבלה
                dataGridScores.DataSource = validScores.Select(s => new
                {
                    תאריך = s.Date.ToShortDateString(),
                    מקצוע = s.Subject,
                    רמה = s.Level,
                    ציון = s.Score.Value.ToString()
                }).ToList();

                StyleDataGrid();

                var average = GradesLogic.CalculateAverage(validScores);
                labelAverage.Text = average.HasValue ? $"ממוצע ציונים: {average.Value:F1}" : "אין ציונים זמינים";

                DrawChart();
            }
            else
            {
                labelAverage.Text = "אין ציונים זמינים";
                dataGridScores.DataSource = new List<object>();
                DrawEmptyChart();
            }
        }

        // טעינת ציונים מקובץ אקסל
        private List<GradeRecord> LoadScoresFromExcel(string filePath)
        {
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    IXLWorksheet gradesWorksheet = workbook.Worksheets.FirstOrDefault(w =>
                        w.Name.ToLower() == "grades");

                    if (gradesWorksheet == null)
                    {
                        foreach (var worksheet in workbook.Worksheets)
                        {
                            if (worksheet.Name == "Questions" || worksheet.Name == "Categories" ||
                                worksheet.Name == "ExamID" || worksheet.Name == "Users")
                                continue;

                            if (IsGradesWorksheet(worksheet))
                            {
                                gradesWorksheet = worksheet;
                                break;
                            }
                        }
                    }

                    // אם מצאנו גיליון מתאים, נטען ממנו את הציונים
                    if (gradesWorksheet != null)
                    {
                        var rawScores = LoadRawScoresFromWorksheet(gradesWorksheet);
                        return GradesLogic.ConvertRawScores(loggedInUsername, rawScores);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בקריאת הציונים מהקובץ: {ex.Message}", "שגיאה",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return new List<GradeRecord>();
        }

        private List<(string Subject, string Level, int? Score)> LoadRawScoresFromWorksheet(IXLWorksheet worksheet)
        {
            var rawScores = new List<(string Subject, string Level, int? Score)>();

            try
            {
                if (worksheet.RangeUsed() == null || worksheet.RangeUsed().RowCount() <= 3)
                    return rawScores;

                int nameColumn = 1;
                bool foundNameColumn = false;

                for (int row = 1; row <= 4; row++)
                {
                    for (int col = 1; col <= 5; col++)
                    {
                        if (worksheet.Cell(row, col).GetString().Contains("Student's Name"))
                        {
                            nameColumn = col;
                            foundNameColumn = true;
                            break;
                        }
                    }
                    if (foundNameColumn) break;
                }

                IXLRow studentRow = null;
                for (int rowNum = 4; rowNum <= worksheet.RangeUsed().RowCount(); rowNum++)
                {
                    var name = worksheet.Cell(rowNum, nameColumn).GetString().Trim();
                    if (name.Equals(loggedInUsername, StringComparison.OrdinalIgnoreCase))
                    {
                        studentRow = worksheet.Row(rowNum);
                        break;
                    }
                }

                if (studentRow == null) return rawScores;

                var subjectHeaderRow = worksheet.Row(2);
                var levelRow = worksheet.Row(3);
                var subjects = new List<(int startCol, string name)>();

                for (int col = nameColumn + 1; col <= worksheet.RangeUsed().ColumnCount(); col++)
                {
                    var subjectText = subjectHeaderRow.Cell(col).GetString().Trim();
                    if (!string.IsNullOrEmpty(subjectText))
                        subjects.Add((col, subjectText));
                }

                for (int col = nameColumn + 1; col <= worksheet.RangeUsed().ColumnCount(); col++)
                {
                    string levelText = levelRow.Cell(col).GetString().Trim();
                    string scoreText = studentRow.Cell(col).GetString().Trim();

                    string currentSubject = "";
                    for (int i = subjects.Count - 1; i >= 0; i--)
                    {
                        if (col >= subjects[i].startCol)
                        {
                            currentSubject = subjects[i].name;
                            break;
                        }
                    }

                    if (string.IsNullOrEmpty(scoreText)) continue;

                    if (int.TryParse(scoreText, out int score))
                    {
                        if (string.IsNullOrEmpty(levelText))
                            levelText = "רגיל";

                        rawScores.Add((currentSubject, levelText, score));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה בטעינת ציונים: {ex.Message}");
            }

            return rawScores;
        }

        // בדיקה אם גיליון נראה כמו גיליון ציונים לפי שם עמודה
        private bool IsGradesWorksheet(IXLWorksheet worksheet)
        {
            try
            {
                if (worksheet.RangeUsed() == null || worksheet.RangeUsed().RowCount() <= 1)
                    return false;

                for (int row = 1; row <= 3; row++)
                {
                    for (int col = 1; col <= 5; col++)
                    {
                        if (worksheet.Cell(row, col).GetString().Contains("Student's Name"))
                            return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        // עיצוב טבלת הציונים
        private void StyleDataGrid()
        {
            dataGridScores.AllowUserToAddRows = false;
            dataGridScores.AllowUserToDeleteRows = false;
            dataGridScores.AllowUserToResizeRows = false;
            dataGridScores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridScores.RightToLeft = RightToLeft.Yes;

            if (dataGridScores.Columns.Count > 0)
            {
                dataGridScores.Columns[0].FillWeight = 20;
                dataGridScores.Columns[1].FillWeight = 30;
                dataGridScores.Columns[2].FillWeight = 25;
                dataGridScores.Columns[3].FillWeight = 25;
            }

            foreach (DataGridViewRow row in dataGridScores.Rows)
            {
                if (row.Cells[3].Value != null)
                {
                    var gradeText = row.Cells[3].Value.ToString();
                    if (int.TryParse(gradeText, out int grade))
                    {
                        if (grade >= 85)
                        {
                            row.Cells[3].Style.BackColor = Color.FromArgb(220, 252, 231);
                            row.Cells[3].Style.ForeColor = Color.FromArgb(5, 150, 105);
                        }
                        else if (grade > 56)
                        {
                            row.Cells[3].Style.BackColor = Color.FromArgb(254, 249, 195);
                            row.Cells[3].Style.ForeColor = Color.FromArgb(180, 83, 9);
                        }
                        else
                        {
                            row.Cells[3].Style.BackColor = Color.FromArgb(254, 226, 226);
                            row.Cells[3].Style.ForeColor = Color.FromArgb(220, 38, 38);
                        }
                    }
                }
            }
        }
        // ציור גרף התקדמות ציונים
        private void DrawChart()
        {
            chartProgress.Series.Clear();
            chartProgress.ChartAreas.Clear();
            chartProgress.Legends.Clear();

            var chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea("MainArea");
            chartArea.BackColor = Color.White;
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.Interval = 1;
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Maximum = 100;
            chartProgress.ChartAreas.Add(chartArea);

            var legend = new System.Windows.Forms.DataVisualization.Charting.Legend("Legend1");
            chartProgress.Legends.Add(legend);

            var series = new System.Windows.Forms.DataVisualization.Charting.Series("ציונים")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                BorderWidth = 3,
                MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle,
                MarkerSize = 8
            };

            var validScores = GradesLogic.FilterValidScores(scores);
            for (int i = 0; i < validScores.Count; i++)
            {
                var score = validScores[i];
                string label = $"{score.Subject}\n{score.Level}";
                int pointIndex = series.Points.AddXY(i + 1, score.Score.Value);
                series.Points[pointIndex].AxisLabel = label;

                if (score.Score.Value >= 85)
                    series.Points[pointIndex].Color = Color.Green;
                else if (score.Score.Value >= 70)
                    series.Points[pointIndex].Color = Color.Orange;
                else
                    series.Points[pointIndex].Color = Color.Red;
            }

            chartProgress.Series.Add(series);
        }
        // גרף ריק כשאין נתונים
        private void DrawEmptyChart()
        {
            chartProgress.Series.Clear();
            chartProgress.ChartAreas.Clear();
            chartProgress.Legends.Clear();

            var chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea("MainArea");
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Maximum = 100;
            chartProgress.ChartAreas.Add(chartArea);

            var series = new System.Windows.Forms.DataVisualization.Charting.Series("אין נתונים")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
            };

            series.Points.AddXY(1, 0);
            series.Points[0].AxisLabel = "אין נתונים";
            chartProgress.Series.Add(series);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GradesTracker_Load(sender, e);
            MessageBox.Show("הנתונים עודכנו בהצלחה! 🎉", "רענון הושלם", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // אירועים ריקים
        private void chartProgress_Click(object sender, EventArgs e) { }
        private void labelAverage_Click(object sender, EventArgs e) { }
        private void dataGridScores_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void averagePanel_Paint(object sender, PaintEventArgs e) { }
        private void titleLabel_Click(object sender, EventArgs e) { }

        private void headerPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}