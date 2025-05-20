using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ClosedXML.Excel;

namespace Exam_Questioner
{
    public class StudentDataLogic
    {
        private string filePath;

        public StudentDataLogic(string filePath)
        {
            this.filePath = filePath;
        }

        // פונקציה 1 - החזרת כל הנתונים מהגיליון Grades
        public DataTable GetAllStudentGrades()
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("Grades");
                var range = ws.RangeUsed();

                DataTable dt = new DataTable();
                foreach (var cell in range.Row(1).Cells())
                    dt.Columns.Add(cell.GetString());

                foreach (var row in range.RowsUsed().Skip(1))
                {
                    var dataRow = dt.NewRow();
                    for (int i = 0; i < range.ColumnCount(); i++)
                        dataRow[i] = row.Cell(i + 1).Value;
                    dt.Rows.Add(dataRow);
                }

                return dt;
            }
        }

        // פונקציה 2 - ממוצע ציונים לפי שם תלמיד מהעמודות ציונים (עמודה 2 ואילך)
        public DataTable GetStudentAveragePerName()
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("Grades");
                var range = ws.RangeUsed();

                DataTable result = new DataTable();
                result.Columns.Add("שם תלמיד");
                result.Columns.Add("ממוצע ציונים");

                foreach (var row in range.RowsUsed().Skip(1))
                {
                    string student = row.Cell(1).GetString().Trim();
                    var grades = row.Cells(2, range.ColumnCount())
                                   .Select(c => c.GetString())
                                   .Where(s => !string.IsNullOrWhiteSpace(s) && double.TryParse(s, out _))
                                   .Select(double.Parse)
                                   .ToList();

                    if (grades.Count > 0)
                    {
                        double average = grades.Average();
                        result.Rows.Add(student, average.ToString("F2"));
                    }
                }

                // מיון מהגבוה לנמוך
                DataView dv = result.DefaultView;
                dv.Sort = "ממוצע ציונים DESC";
                return dv.ToTable();
            }
        }

        // פונקציה 3 - סטטיסטיקות כלליות על כל הציונים
        public DataTable GetStatistics()
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("Grades");
                var range = ws.RangeUsed();

                List<double> allGrades = new List<double>();
                List<double> programmingGrades = new List<double>();
                List<double> dataStructGrades = new List<double>();
                List<double> principlesGrades = new List<double>();
                List<double> testingGrades = new List<double>();

                foreach (var row in range.RowsUsed().Skip(1))
                {
                    // כל הציונים הכלליים (עמודות 2 והלאה)
                    var grades = row.Cells(2, range.ColumnCount())
                                    .Select(c => c.GetString())
                                    .Where(s => !string.IsNullOrWhiteSpace(s) && double.TryParse(s, out _))
                                    .Select(double.Parse)
                                    .ToList();
                    allGrades.AddRange(grades);

                    // ציוני תכנות - B C D = עמודות 2, 3, 4
                    var progGrades = row.Cells(2, 4)
                                        .Select(c => c.GetString())
                                        .Where(s => !string.IsNullOrWhiteSpace(s) && double.TryParse(s, out _))
                                        .Select(double.Parse);
                    programmingGrades.AddRange(progGrades);

                    // מבנה נתונים - E F G = עמודות 5, 6, 7
                    var dsGrades = row.Cells(5, 7)
                                      .Select(c => c.GetString())
                                      .Where(s => !string.IsNullOrWhiteSpace(s) && double.TryParse(s, out _))
                                      .Select(double.Parse);
                    dataStructGrades.AddRange(dsGrades);

                    // עקרונות - H I J = עמודות 8, 9, 10
                    var prinGrades = row.Cells(8, 10)
                                        .Select(c => c.GetString())
                                        .Where(s => !string.IsNullOrWhiteSpace(s) && double.TryParse(s, out _))
                                        .Select(double.Parse);
                    principlesGrades.AddRange(prinGrades);

                    // בדיקות - K L M = עמודות 11, 12, 13
                    var testGrades = row.Cells(11, 13)
                                        .Select(c => c.GetString())
                                        .Where(s => !string.IsNullOrWhiteSpace(s) && double.TryParse(s, out _))
                                        .Select(double.Parse);
                    testingGrades.AddRange(testGrades);
                }

                if (allGrades.Count == 0)
                    throw new Exception("לא נמצאו ציונים חוקיים.");

                double avg = allGrades.Average();
                double min = allGrades.Min();
                double max = allGrades.Max();
                double stdDev = Math.Sqrt(allGrades.Average(v => Math.Pow(v - avg, 2)));
                double successRate = (allGrades.Count(g => g >= 60) / (double)allGrades.Count) * 100;

                DataTable dtStats = new DataTable();
                dtStats.Columns.Add("סטטיסטיקה");
                dtStats.Columns.Add("ערך");

                dtStats.Rows.Add("ממוצע ציונים כללי", avg.ToString("F2"));
                dtStats.Rows.Add("ציון מקסימלי", max);
                dtStats.Rows.Add("ציון מינימלי", min);
                dtStats.Rows.Add("סטיית תקן", stdDev.ToString("F2"));
                dtStats.Rows.Add("מספר ציונים כולל", allGrades.Count);
                dtStats.Rows.Add("אחוז הצלחה (60+)", successRate.ToString("F2") + "%");

                // הוספת ממוצעים לפי תחומים
                if (programmingGrades.Count > 0)
                    dtStats.Rows.Add("ממוצע ציונים - תכנות", programmingGrades.Average().ToString("F2"));

                if (dataStructGrades.Count > 0)
                    dtStats.Rows.Add("ממוצע ציונים - מבנה נתונים", dataStructGrades.Average().ToString("F2"));

                if (principlesGrades.Count > 0)
                    dtStats.Rows.Add("ממוצע ציונים - עקרונות", principlesGrades.Average().ToString("F2"));

                if (testingGrades.Count > 0)
                    dtStats.Rows.Add("ממוצע ציונים - בדיקות", testingGrades.Average().ToString("F2"));

                return dtStats;
            }
        }
        // פונקציה 4 - חיפוש סטודנט לפי שם
        public DataTable SearchStudentByName(string name)
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("Grades");

                DataTable dt = new DataTable();

                // שלב 1: כותרות העמודות — נקבע ידנית
                int colCount = ws.Row(3).LastCellUsed().Address.ColumnNumber;
                // מספר עמודות לפי שורת המקצועות
                for (int i = 0; i < colCount; i++)
                    dt.Columns.Add("Column" + (i + 1)); // או תני שמות יפים יותר אם את רוצה

                // שלב 2: שורת המקצועות (שורה 2 באקסל)
                var rowSubjects = dt.NewRow();
                for (int i = 0; i < colCount; i++)
                    rowSubjects[i] = ws.Row(2).Cell(i + 1).Value;
                dt.Rows.Add(rowSubjects);

                // שלב 3: שורת הרמות (שורה 3 באקסל)
                var rowLevels = dt.NewRow();
                for (int i = 0; i < colCount; i++)
                    rowLevels[i] = ws.Row(3).Cell(i + 1).Value;
                dt.Rows.Add(rowLevels);

                // שלב 4: חיפוש התלמיד (שורה 4 והלאה)
                foreach (var row in ws.RowsUsed().Skip(3))
                {
                    string studentName = row.Cell(1).GetString().Trim();
                    if (studentName.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        var studentRow = dt.NewRow();
                        for (int i = 0; i < colCount; i++)
                            studentRow[i] = row.Cell(i + 1).Value;
                        dt.Rows.Add(studentRow);
                        break;
                    }
                }

                return dt;
            }
        }


    }

}



    


