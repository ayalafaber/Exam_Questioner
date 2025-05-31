using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Exam_Questioner
{
    public partial class GradesTracker : Form
    {
        private List<(string Username, DateTime Date, string Subject, string Level, int? Score)> scores;
        private string loggedInUsername;

        public GradesTracker(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
        }

        private void GradesTracker_Load(object sender, EventArgs e)
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.xlsx");
            var logic = new StudentDataLogic(filePath);

            var rawScores = logic.GetStudentScoresDetailed(loggedInUsername);

            scores = rawScores.Select(s => (loggedInUsername, DateTime.Now, s.Subject, s.Level, s.Score)).ToList();

            // סינון רק ציונים שהושלמו (יש להם ערך)
            var completedScores = scores.Where(s => s.Score.HasValue).ToList();

            // הצגת הנתונים בטבלה - רק ציונים שהושלמו
            dataGridScores.DataSource = completedScores.Select(s => new
            {
                תאריך = s.Date.ToShortDateString(),
                מקצוע = s.Subject,
                רמה = s.Level,
                ציון = s.Score.Value.ToString()
            }).ToList();

            // עיצוב הטבלה
            StyleDataGrid();

            // מחשב ממוצע רק אם יש ציונים עם ערך
            if (completedScores.Any())
            {
                double average = completedScores.Average(s => s.Score.Value);
                labelAverage.Text = $"ממוצע ציונים: {average:F1}";
            }
            else
            {
                labelAverage.Text = "אין ציונים זמינים";
            }

            DrawChart();
        }

        private void StyleDataGrid()
        {
            // הגדרות כלליות לטבלה
            dataGridScores.AllowUserToAddRows = false;
            dataGridScores.AllowUserToDeleteRows = false;
            dataGridScores.AllowUserToResizeRows = false;
            dataGridScores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // הגדרת רוחב עמודות
            if (dataGridScores.Columns.Count > 0)
            {
                dataGridScores.Columns[0].FillWeight = 20; // תאריך
                dataGridScores.Columns[1].FillWeight = 30; // מקצוע
                dataGridScores.Columns[2].FillWeight = 25; // רמה
                dataGridScores.Columns[3].FillWeight = 25; // ציון
            }

            // עיצוב מותאם לציונים - כעת כל הציונים יהיו עם ערך
            foreach (DataGridViewRow row in dataGridScores.Rows)
            {
                if (row.Cells[3].Value != null)
                {
                    var gradeText = row.Cells[3].Value.ToString();
                    if (int.TryParse(gradeText, out int grade))
                    {
                        // צביעת הציון לפי הערך
                        if (grade >= 85)
                        {
                            row.Cells[3].Style.BackColor = Color.FromArgb(220, 252, 231); // ירוק בהיר
                            row.Cells[3].Style.ForeColor = Color.FromArgb(5, 150, 105); // ירוק כהה
                        }
                        else if (grade >= 70)
                        {
                            row.Cells[3].Style.BackColor = Color.FromArgb(254, 249, 195); // צהוב בהיר
                            row.Cells[3].Style.ForeColor = Color.FromArgb(180, 83, 9); // כתום כהה
                        }
                        else if (grade >= 55)
                        {
                            row.Cells[3].Style.BackColor = Color.FromArgb(254, 226, 226); // אדום בהיר
                            row.Cells[3].Style.ForeColor = Color.FromArgb(220, 38, 38); // אדום כהה
                        }
                    }
                }
            }
        }

        private void DrawChart()
        {
            chartProgress.Series.Clear();
            chartProgress.ChartAreas.Clear();
            chartProgress.Legends.Clear();

            // הגדרת אזור הגרף
            var chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea("MainArea");
            chartArea.BackColor = Color.White;
            chartArea.AxisX.MajorGrid.Enabled = true;
            chartArea.AxisY.MajorGrid.Enabled = true;
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(243, 244, 246);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(243, 244, 246);
            chartArea.AxisX.LineColor = Color.FromArgb(229, 231, 235);
            chartArea.AxisY.LineColor = Color.FromArgb(229, 231, 235);
            chartArea.AxisX.LabelStyle.ForeColor = Color.FromArgb(107, 114, 128);
            chartArea.AxisY.LabelStyle.ForeColor = Color.FromArgb(107, 114, 128);
            chartArea.AxisX.LabelStyle.Angle = -45;
            chartArea.AxisX.Interval = 1;
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Maximum = 100;

            chartProgress.ChartAreas.Add(chartArea);

            // הגדרת מקרא
            var legend = new System.Windows.Forms.DataVisualization.Charting.Legend("Legend1");
            legend.BackColor = Color.Transparent;
            legend.BorderColor = Color.Transparent;
            legend.Font = new Font("Segoe UI", 9F);
            legend.ForeColor = Color.FromArgb(75, 85, 99);
            chartProgress.Legends.Add(legend);

            // יצירת הסדרה
            var series = new System.Windows.Forms.DataVisualization.Charting.Series("ציונים");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series.Color = Color.FromArgb(16, 185, 129);
            series.BorderWidth = 3;
            series.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series.MarkerSize = 8;
            series.MarkerColor = Color.FromArgb(16, 185, 129);

            // הוספת נקודות לגרף - רק ציונים שהושלמו
            var validScores = scores.Where(s => s.Score.HasValue).ToList();
            if (validScores.Any())
            {
                for (int i = 0; i < validScores.Count; i++)
                {
                    var score = validScores[i];
                    string label = $"{score.Subject}\n{score.Level}";
                    var point = series.Points.AddXY(i + 1, score.Score.Value);
                    series.Points[point].AxisLabel = label;

                    // צביעת נקודות לפי ציון
                    if (score.Score.Value >= 85)
                        series.Points[point].Color = Color.FromArgb(34, 197, 94); // ירוק
                    else if (score.Score.Value >= 70)
                        series.Points[point].Color = Color.FromArgb(245, 158, 11); // צהוב/כתום
                    else
                        series.Points[point].Color = Color.FromArgb(239, 68, 68); // אדום
                }
            }
            else
            {
                // אם אין ציונים, הוסף נקודה דמה
                series.Points.AddXY(1, 0);
                series.Points[0].AxisLabel = "אין נתונים";
                series.Points[0].Color = Color.FromArgb(156, 163, 175);
            }

            chartProgress.Series.Add(series);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // רענון הנתונים
            GradesTracker_Load(sender, e);

            // הודעת הצלחה
            MessageBox.Show("הנתונים עודכנו בהצלחה! 🎉", "רענון הושלם",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // אירועים ריקים
        private void chartProgress_Click(object sender, EventArgs e) { }
        private void labelAverage_Click(object sender, EventArgs e) { }
        private void dataGridScores_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}