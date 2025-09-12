using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Exam_Questioner
{
    public class StudentDataLogic
    {
        private string filePath;
        public string FilePath => filePath;

        public StudentDataLogic(string filePath)
        {
            this.filePath = filePath;
        }

        public DataTable GetAllStudentGrades()
        {
            using (var wb = new XLWorkbook(filePath))
            {
                // --- 1. טוענים את גיליון Grades ומגדירים את ה־DataTable ---
                var wsGrades = wb.Worksheet("Grades");
                var range = wsGrades.RangeUsed();

                DataTable dt = new DataTable();

                // שורה 3 ב־Grades היא שורת ה־Levels (נניח שיש שם ערכים רציפים)
                var levelRow = wsGrades.Row(3);
                int colCount = levelRow.LastCellUsed().Address.ColumnNumber;

                // יוצרים dt.Columns בהתאם למספר העמודות
                for (int i = 0; i < colCount; i++)
                {
                    dt.Columns.Add("Column" + (i + 1));
                }

                // --- 2. ממלאים את ה־DataTable בכל השורות מ־Grades (כולל שורה 2 ושורה 3) ---
                foreach (var row in wsGrades.RowsUsed().Skip(1))
                {
                    var dataRow = dt.NewRow();
                    for (int i = 1; i <= colCount; i++)
                    {
                        dataRow[i - 1] = row.Cell(i).Value;
                    }
                    dt.Rows.Add(dataRow);
                }

                // --- 3. בונים סט (HashSet) של כל השמות שכבר קיימים בעמודה A של Grades ---
                //     נדלג על שורה 1 (Header) וניקח כל שם בעמודה 1 (A)
                var gradeNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var row in wsGrades.RowsUsed().Skip(1))
                {
                    string nameInGrades = row.Cell(1).GetString().Trim();
                    if (!string.IsNullOrEmpty(nameInGrades))
                    {
                        gradeNames.Add(nameInGrades);
                    }
                }

                // --- 4. עולים על גיליון Users ומחפשים שם מלא לכל Row שבה תפקיד == "Student" ---
                var wsUsers = wb.Worksheet("Users");
                var missingNames = new List<string>();

                foreach (var userRow in wsUsers.RowsUsed().Skip(1))
                {
                    // עמודה F = תפקיד
                    string role = userRow.Cell(6).GetString().Trim();

                    // אם זה סטודנט
                    if (role.Equals("Student", StringComparison.OrdinalIgnoreCase))
                    {
                        // עמודה D = שמות מלאים
                        string fullName = userRow.Cell(4).GetString().Trim();
                        if (!string.IsNullOrEmpty(fullName))
                        {
                            // אם השם עדיין לא נמצא ב־Grades, נוסיף לרשימת החסרים
                            if (!gradeNames.Contains(fullName))
                            {
                                missingNames.Add(fullName);
                            }
                        }
                    }
                }

                // --- 5. מוסיפים לכל שם חסר שורה חדשה ב־DataTable (בשורה A שמות, שאר העמודות ריקות) ---
                foreach (var nameToAdd in missingNames)
                {
                    var newRow = dt.NewRow();
                    // שם מלא בעמודה הראשונה
                    newRow[0] = nameToAdd;

                    // את כל יתר העמודות (1..colCount-1) נשאיר ריקות (או null)
                    for (int c = 1; c < colCount; c++)
                    {
                        newRow[c] = ""; // אפשר לשים גם null אם רוצים
                    }

                    dt.Rows.Add(newRow);
                }

                return dt;
            }
        }


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

                DataView dv = result.DefaultView;
                dv.Sort = "ממוצע ציונים DESC";
                return dv.ToTable();
            }
        }

        public DataTable GetStatistics()
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("Grades");
                var range = ws.RangeUsed();

                List<double> allGrades = new List<double>();
                Dictionary<string, List<double>> categoryGrades = new Dictionary<string, List<double>>();

                var subjectRow = ws.Row(2);
                int lastCol = range.LastColumn().ColumnNumber();

                Dictionary<int, string> columnToCategory = new Dictionary<int, string>();
                var foundSubjects = new List<(int col, string name)>();

                for (int col = 2; col <= lastCol; col++)
                {
                    string categoryName = subjectRow.Cell(col).GetString().Trim();
                    if (!string.IsNullOrWhiteSpace(categoryName))
                    {
                        foundSubjects.Add((col, categoryName));
                        if (!categoryGrades.ContainsKey(categoryName))
                        {
                            categoryGrades[categoryName] = new List<double>();
                        }
                    }
                }

                for (int col = 2; col <= lastCol; col++)
                {
                    string relevantSubject = null;
                    for (int i = foundSubjects.Count - 1; i >= 0; i--)
                    {
                        if (col >= foundSubjects[i].col)
                        {
                            relevantSubject = foundSubjects[i].name;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(relevantSubject))
                    {
                        columnToCategory[col] = relevantSubject;
                    }
                }

                foreach (var row in range.RowsUsed().Skip(3))
                {
                    for (int col = 2; col <= lastCol; col++)
                    {
                        string gradeText = row.Cell(col).GetString().Trim();

                        if (!string.IsNullOrWhiteSpace(gradeText) && double.TryParse(gradeText, out double grade))
                        {
                            allGrades.Add(grade);

                            if (columnToCategory.ContainsKey(col))
                            {
                                string category = columnToCategory[col];
                                categoryGrades[category].Add(grade);
                            }
                        }
                    }
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

                var sortedCategories = categoryGrades.Keys.OrderBy(k => k).ToList();
                foreach (var category in sortedCategories)
                {
                    if (categoryGrades[category].Count > 0)
                    {
                        double categoryAvg = categoryGrades[category].Average();
                        dtStats.Rows.Add($"ממוצע ציונים - {category}", categoryAvg.ToString("F2"));
                    }
                    else
                    {
                        dtStats.Rows.Add($"ממוצע ציונים - {category}", "אין ציונים");
                    }
                }

                return dtStats;
            }
        }

        // חיפוש פשוט לפי שם - ישיר בגיליון Grades
        public DataTable SearchStudentByNameViaUsersSheet(string name)
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("Grades");
                DataTable dt = new DataTable();

                // יצירת עמודות בהתאם לכמות העמודות ברמת Levels (Row 3)
                var levelRow = ws.Row(3);
                int colCount = levelRow.LastCellUsed().Address.ColumnNumber;
                for (int i = 0; i < colCount; i++)
                    dt.Columns.Add("Column" + (i + 1));

                // הוספת שורת Subjects (Row 2)
                var subjectsRow = dt.NewRow();
                for (int i = 1; i <= colCount; i++)
                    subjectsRow[i - 1] = ws.Row(2).Cell(i).Value;
                dt.Rows.Add(subjectsRow);

                // הוספת שורת Levels (Row 3)
                var levelsRow = dt.NewRow();
                for (int i = 1; i <= colCount; i++)
                    levelsRow[i - 1] = ws.Row(3).Cell(i).Value;
                dt.Rows.Add(levelsRow);

                // איפוס מחרוזת החיפוש ל־Lowercase וללא רווחים מיותרים
                string searchTerm = name.Trim().ToLower();
                bool foundAny = false;

                // מייצאים את כל השורות שמכילות את השם (Column A בגליון Grades), שורות התלמידים מתחילות ב-Row 4, לכן Skip(3)
                foreach (var row in ws.RowsUsed().Skip(3))
                {
                    string studentFullName = row.Cell(1).GetString().Trim();
                    if (studentFullName.ToLower().Contains(searchTerm))
                    {
                        var dataRow = dt.NewRow();
                        for (int i = 1; i <= colCount; i++)
                            dataRow[i - 1] = row.Cell(i).Value;
                        dt.Rows.Add(dataRow);
                        foundAny = true;
                    }
                }

                if (!foundAny)
                    throw new Exception($"לא נמצאו תלמידים עם השם '{name}' בגליון Grades");

                return dt;
            }
        }


        // חיפוש פשוט לפי ת"ז - ישיר בגיליון Users
        public DataTable SearchStudentByID(string idNumber)
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var wsGrades = wb.Worksheet("Grades");
                var wsUsers = wb.Worksheet("Users");

                DataTable dt = new DataTable();
                // קביעת מספר העמודות על פי שורת הרמות (Row 3 בגליון Grades)
                var levelRow = wsGrades.Row(3);
                int colCount = levelRow.LastCellUsed().Address.ColumnNumber;

                // יצירת עמודות ב-DataTable
                for (int i = 0; i < colCount; i++)
                {
                    dt.Columns.Add("Column" + (i + 1));
                }

                // הוספת שורות הכותרת (מקצועות ושורות רמות) בגליון Grades
                var subjectsRow = dt.NewRow();
                for (int i = 1; i <= colCount; i++)
                {
                    subjectsRow[i - 1] = wsGrades.Row(2).Cell(i).Value;
                }
                dt.Rows.Add(subjectsRow);

                var levelsRow = dt.NewRow();
                for (int i = 1; i <= colCount; i++)
                {
                    levelsRow[i - 1] = wsGrades.Row(3).Cell(i).Value;
                }
                dt.Rows.Add(levelsRow);

                // 1. מציאת כל השמות המלאים שמתאימים לת"ז בגליון Users
                var matchingFullNames = new List<string>();
                string searchTerm = idNumber.Trim();

                foreach (var userRow in wsUsers.RowsUsed().Skip(1)) // (מדלגים על שורת הכותרת בגליון Users)
                {
                    // עמודה C = ת"ז
                    string userId = userRow.Cell(3).GetString().Trim();

                    // עמודה D = שם מלא
                    string userFullName = userRow.Cell(4).GetString().Trim();

                    // עמודה F = Role (אם מעוניינים לסנן רק סטודנטים)
                    string role = userRow.Cell(6).GetString().Trim();

                    // נניח שברצונך לסנן לפי role == "Student"
                    // אם ברצונך לקבל גם בעברית, אפשר להוסיף או לבדוק שני תנאים:
                    // if ((role.Equals("Student", StringComparison.OrdinalIgnoreCase) || role == "סטודנט")
                    //    && userId.Contains(searchTerm))
                    if (role.Equals("Student", StringComparison.OrdinalIgnoreCase)
                        && userId.Contains(searchTerm))
                    {
                        matchingFullNames.Add(userFullName);
                    }
                }

                if (matchingFullNames.Count == 0)
                    throw new Exception($"לא נמצאו תלמידים עם ת\"ז שמכילה '{idNumber}' בגליון Users");

                // 2. הוספת השורות התואמות בגליון Grades (בהתאם לשמות המלאים שמצאנו)
                foreach (var row in wsGrades.RowsUsed().Skip(3)) // (מדלגים על שורות הכותרת: רכזת Subject ו־Levels)
                {
                    string rowFullName = row.Cell(1).GetString().Trim(); // שם מלא בעמודה A
                                                                         // השוואה לא חשה רישיות
                    if (matchingFullNames
                        .Any(fn => fn.Equals(rowFullName, StringComparison.OrdinalIgnoreCase)))
                    {
                        var studentRow = dt.NewRow();
                        for (int i = 1; i <= colCount; i++)
                        {
                            studentRow[i - 1] = row.Cell(i).Value;
                        }
                        dt.Rows.Add(studentRow);
                        // אם רוצים רק תלמיד אחד בלבד, אפשר לעשות break כאן
                    }
                }

                return dt;
            }
        }


        public DataTable GetStudentGradesByUsername(string username)
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var wsGrades = wb.Worksheet("Grades");
                DataTable dt = new DataTable();
                int colCount = wsGrades.Row(3).LastCellUsed().Address.ColumnNumber;

                for (int i = 0; i < colCount; i++)
                    dt.Columns.Add("Column" + (i + 1));

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

        public List<StudentSearchResult> SearchStudentsAdvanced(string searchTerm)
        {
            var results = new List<StudentSearchResult>();
            var allStudents = ExcelHelper.GetAllStudents();

            foreach (var student in allStudents)
            {
                if (student.FullName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    string id = ExcelHelper.GetIDByFullName(student.FullName, "Student");
                    results.Add(new StudentSearchResult
                    {
                        Username = student.Username,
                        FullName = student.FullName,
                        ID = id,
                        MatchType = "שם"
                    });
                }
                else
                {
                    string studentId = ExcelHelper.GetIDByFullName(student.FullName, "Student");
                    if (!string.IsNullOrEmpty(studentId) && studentId.Contains(searchTerm))
                    {
                        results.Add(new StudentSearchResult
                        {
                            Username = student.Username,
                            FullName = student.FullName,
                            ID = studentId,
                            MatchType = "ת\"ז"
                        });
                    }
                }
            }

            return results;
        }

        public DataTable GetAllStudentsWithDetails()
        {
            var allStudents = ExcelHelper.GetAllStudents();
            DataTable dt = new DataTable();
            dt.Columns.Add("שם משתמש");
            dt.Columns.Add("שם מלא");
            dt.Columns.Add("תעודת זהות");

            foreach (var student in allStudents)
            {
                string id = ExcelHelper.GetIDByFullName(student.FullName, "Student");
                dt.Rows.Add(student.Username, student.FullName, id);
            }

            return dt;
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

                var subjectRow = rows[1];
                var levelRow = rows[2];
                var studentRow = rows.FirstOrDefault(r =>
                    r.Cell(1).GetString().Trim().Equals(studentName.Trim(), StringComparison.OrdinalIgnoreCase));

                if (studentRow == null)
                    return scores;

                int lastCol = studentRow.LastCellUsed().Address.ColumnNumber;
                string currentSubject = null;

                for (int col = 2; col <= lastCol; col++)
                {
                    string subjectCell = subjectRow.Cell(col).GetString().Trim();
                    if (!string.IsNullOrWhiteSpace(subjectCell))
                    {
                        currentSubject = subjectCell;
                    }

                    string level = levelRow.Cell(col).GetString().Trim();

                    if (string.IsNullOrWhiteSpace(level) || string.IsNullOrWhiteSpace(currentSubject))
                        continue;

                    string raw = studentRow.Cell(col).GetString().Trim();
                    int? score = null;
                    if (double.TryParse(raw, out double parsedScore))
                        score = (int)Math.Round(parsedScore);

                    if (!subjectToLevels.ContainsKey(currentSubject))
                        subjectToLevels[currentSubject] = new Dictionary<string, int?>();

                    subjectToLevels[currentSubject][level] = score;
                }

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

        public DataTable ReadUsersSheet()
        {
            // קריאת ה־Users sheet מהאקסל
            var table = new DataTable();

            using (var connection = new OleDbConnection(GetConnectionString()))
            {
                connection.Open();
                var command = new OleDbCommand("SELECT * FROM [Users$]", connection);
                var adapter = new OleDbDataAdapter(command);
                adapter.Fill(table);
            }

            return table;
        }

        public string GetEmailByUsername(string username)
        {
            var dataTable = ReadUsersSheet();

            foreach (DataRow row in dataTable.Rows)
            {
                if (row["Username"] != null && row["Username"].ToString() == username)
                {
                    return row["Email"]?.ToString();
                }
            }

            return "";
        }

        private string GetConnectionString()
        {
            return $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';";
        }

    }

    public class StudentSearchResult
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string ID { get; set; }
        public string MatchType { get; set; }
    }
}