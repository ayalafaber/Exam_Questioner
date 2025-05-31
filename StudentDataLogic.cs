using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using System.Windows.Forms;


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

                // קביעת מספר העמודות לפי השורה השלישית
                var levelRow = ws.Row(3);
                int colCount = levelRow.LastCellUsed().Address.ColumnNumber;

                // יצירת עמודות גנריות – Column1, Column2 וכו'
                for (int i = 0; i < colCount; i++)
                {
                    dt.Columns.Add("Column" + (i + 1));
                }

                // הוספת כל השורות כולל מקצועות, רמות, תלמידים
                foreach (var row in ws.RowsUsed().Skip(1)) // דילוג רק על שורת Subject
                {
                    var dataRow = dt.NewRow();
                    for (int i = 1; i <= colCount; i++)
                    {
                        dataRow[i - 1] = row.Cell(i).Value;
                    }
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
        public DataTable SearchStudentByNameViaUsersSheet(string name)
        {
            using (var wb = new XLWorkbook(filePath))
            {
                // 1. מציאת שם המשתמש לפי שם מהגיליון Users
                var wsUsers = wb.Worksheet("Users");
                string username = null;

                foreach (var row in wsUsers.RowsUsed().Skip(1)) // נניח ששורה 1 היא כותרות
                {
                    string fullName = row.Cell(4).GetString().Trim(); // עמודה D
                    if (fullName.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        username = row.Cell(1).GetString().Trim(); // עמודה A
                        break;
                    }
                }

                if (string.IsNullOrEmpty(username))
                    throw new Exception("לא נמצא סטודנט עם השם שהזנת");

                // 2. מציאת הציונים לפי שם המשתמש בגיליון Grades
                var wsGrades = wb.Worksheet("Grades");

                DataTable dt = new DataTable();
                int colCount = wsGrades.Row(3).LastCellUsed().Address.ColumnNumber;

                // הוספת כותרות
                for (int i = 0; i < colCount; i++)
                    dt.Columns.Add("Column" + (i + 1));

                // הוספת שורת נושאים ורמות
                var subjects = dt.NewRow();
                for (int i = 0; i < colCount; i++)
                    subjects[i] = wsGrades.Row(2).Cell(i + 1).Value;
                dt.Rows.Add(subjects);

                var levels = dt.NewRow();
                for (int i = 0; i < colCount; i++)
                    levels[i] = wsGrades.Row(3).Cell(i + 1).Value;
                dt.Rows.Add(levels);

                foreach (var row in wsGrades.RowsUsed().Skip(3))
                {
                    string rowUsername = row.Cell(1).GetString().Trim(); // עמודה A
                    if (rowUsername.Equals(username, StringComparison.OrdinalIgnoreCase))
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

        public DataTable SearchStudentByID(string idNumber)
        {
            using (var wb = new XLWorkbook(filePath))
            {
                // 1. חיפוש שם משתמש לפי תז בגליון Users
                var wsUsers = wb.Worksheet("Users");
                string username = null;

                foreach (var row in wsUsers.RowsUsed().Skip(1)) // שורה 1 היא כותרת
                {
                    string id = row.Cell(3).GetString().Trim(); // עמודה C = ת"ז
                    if (id.Equals(idNumber.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        username = row.Cell(1).GetString().Trim(); // עמודה A = שם משתמש
                        break;
                    }
                }

                if (string.IsNullOrEmpty(username))
                    throw new Exception("לא נמצא סטודנט עם מספר תעודת הזהות שהזנת.");

                // 2. שליפת הציונים מגיליון Grades לפי שם משתמש
                var wsGrades = wb.Worksheet("Grades");

                DataTable dt = new DataTable();
                int colCount = wsGrades.Row(3).LastCellUsed().Address.ColumnNumber;

                // שורת נושאים
                var subjects = dt.NewRow();
                for (int i = 0; i < colCount; i++)
                    dt.Columns.Add("Column" + (i + 1));
                for (int i = 0; i < colCount; i++)
                    subjects[i] = wsGrades.Row(2).Cell(i + 1).Value;
                dt.Rows.Add(subjects);

                // שורת רמות
                var levels = dt.NewRow();
                for (int i = 0; i < colCount; i++)
                    levels[i] = wsGrades.Row(3).Cell(i + 1).Value;
                dt.Rows.Add(levels);

                // מציאת שורת הציונים לפי שם משתמש
                foreach (var row in wsGrades.RowsUsed().Skip(3))
                {
                    string rowUsername = row.Cell(1).GetString().Trim();
                    if (rowUsername.Equals(username, StringComparison.OrdinalIgnoreCase))
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


        public List<(string Subject, string Level, int? Score)> GetStudentScoresDetailed(string studentName)
        {
            var scores = new List<(string Subject, string Level, int? Score)>();
            var allLevels = new[] { "קל", "בינוני", "קשה" };
            var subjectToLevels = new Dictionary<string, Dictionary<string, int?>>();

            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("Grades");
                var rows = ws.RowsUsed().ToList();

                var subjectRow = rows[1]; // שורת שמות מקצועות
                var levelRow = rows[2];   // שורת רמות
                var studentRow = rows.FirstOrDefault(r =>
                    r.Cell(1).GetString().Trim().Equals(studentName.Trim(), StringComparison.OrdinalIgnoreCase));

                if (studentRow == null)
                    return scores;

                int lastCol = studentRow.LastCellUsed().Address.ColumnNumber;
                string currentSubject = null;

                for (int col = 2; col <= lastCol; col++)
                {
                    // שמירת שם מקצוע נוכחי אם כתוב
                    string subjectCell = subjectRow.Cell(col).GetString().Trim();
                    if (!string.IsNullOrWhiteSpace(subjectCell))
                    {
                        currentSubject = subjectCell;
                    }

                    string level = levelRow.Cell(col).GetString().Trim();

                    // אם אין רמה או אין מקצוע עד כה, ממשיכים
                    if (string.IsNullOrWhiteSpace(level) || string.IsNullOrWhiteSpace(currentSubject))
                        continue;

                    // קבלת הציון מהתא
                    string raw = studentRow.Cell(col).GetString().Trim();
                    int? score = null;
                    if (double.TryParse(raw, out double parsedScore))
                        score = (int)Math.Round(parsedScore);

                    if (!subjectToLevels.ContainsKey(currentSubject))
                        subjectToLevels[currentSubject] = new Dictionary<string, int?>();

                    subjectToLevels[currentSubject][level] = score;
                }

                // שלב סופי: מוודאים 3 רמות לכל מקצוע
                foreach (var subject in subjectToLevels.Keys)
                {
                    foreach (var level in allLevels)
                    {
                        int? score = subjectToLevels[subject].ContainsKey(level)
                            ? subjectToLevels[subject][level]
                            : null;

                        scores.Add((subject, level, score));
                    }
                }
            }

            return scores;
        }




    }


}