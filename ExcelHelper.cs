using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Exam_Questioner
{
    public static class ExcelHelper
    {
        private static readonly string filePath;
        private const string SheetName = "Users";
        private const string ReviewsSheetName = "Reviews";

        static ExcelHelper()
        {
            try
            {
                filePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "database.xlsx");
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה באתחול ExcelHelper:\n" + ex.Message);
                throw;
            }
        }

        public static bool UserExists(string username, string password, string role)
        {
            if (!File.Exists(filePath)) return false;

            using (var workbook = new XLWorkbook(filePath))
            {
                if (!workbook.Worksheets.Contains(SheetName)) return false;

                var ws = workbook.Worksheet(SheetName);
                var rows = ws.RangeUsed()?.RowsUsed();
                if (rows == null) return false;

                foreach (var row in rows)
                {
                    if (row.RowNumber() == 1) continue;

                    string u = row.Cell(1).GetString();
                    string p = row.Cell(2).GetString();
                    string r = row.Cell(6).GetString();

                    if (u == username && p == password && r == role)
                        return true;
                }
            }

            return false;
        }

        public static bool UsernameExists(string username)
        {
            if (!File.Exists(filePath)) return false;

            using (var workbook = new XLWorkbook(filePath))
            {
                if (!workbook.Worksheets.Contains(SheetName)) return false;

                var ws = workbook.Worksheet(SheetName);
                var rows = ws.RangeUsed()?.RowsUsed();
                if (rows == null) return false;

                foreach (var row in rows)
                {
                    if (row.RowNumber() == 1) continue;

                    string u = row.Cell(1).GetString();
                    if (u == username)
                        return true;
                }
            }

            return false;
        }

        public static bool RegisterUser(string username, string password, string id, string fullName, string email, string role)
        {
            using (var workbook = File.Exists(filePath) ? new XLWorkbook(filePath) : new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Contains(SheetName)
                    ? workbook.Worksheet(SheetName)
                    : workbook.AddWorksheet(SheetName);

                if (worksheet.Cell(1, 1).IsEmpty())
                {
                    worksheet.Cell(1, 1).Value = "Username";
                    worksheet.Cell(1, 2).Value = "Password";
                    worksheet.Cell(1, 3).Value = "ID";
                    worksheet.Cell(1, 4).Value = "Full Name";
                    worksheet.Cell(1, 5).Value = "Email";
                    worksheet.Cell(1, 6).Value = "Role";
                }

                int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 1;
                int newRow = lastRow + 1;

                worksheet.Cell(newRow, 1).Value = username;
                worksheet.Cell(newRow, 2).Value = password;
                worksheet.Cell(newRow, 3).Value = id;
                worksheet.Cell(newRow, 4).Value = fullName;
                worksheet.Cell(newRow, 5).Value = email;
                worksheet.Cell(newRow, 6).Value = role;

                workbook.SaveAs(filePath);
                return true;
            }
        }

        public static string GetFullName(string username, string role)
        {
            if (!File.Exists(filePath)) return "";

            using (var workbook = new XLWorkbook(filePath))
            {
                if (!workbook.Worksheets.Contains(SheetName)) return "";

                var ws = workbook.Worksheet(SheetName);
                var rows = ws.RangeUsed()?.RowsUsed();
                if (rows == null) return "";

                foreach (var row in rows)
                {
                    if (row.RowNumber() == 1) continue;

                    string u = row.Cell(1).GetString();
                    string r = row.Cell(6).GetString();

                    if (u == username && r == role)
                        return row.Cell(4).GetString(); // Full Name
                }
            }

            return "";
        }

        public static string GetFullNameByID(string id, string role = "Student")
        {
            if (!File.Exists(filePath)) return "";

            using (var workbook = new XLWorkbook(filePath))
            {
                if (!workbook.Worksheets.Contains(SheetName)) return "";

                var ws = workbook.Worksheet(SheetName);
                var rows = ws.RangeUsed()?.RowsUsed();
                if (rows == null) return "";

                foreach (var row in rows)
                {
                    if (row.RowNumber() == 1) continue;

                    string rowID = row.Cell(3).GetString();
                    string rowRole = row.Cell(6).GetString();

                    if (rowID == id && rowRole == role)
                        return row.Cell(4).GetString(); // Full Name
                }
            }

            return "";
        }

        public static string GetIDByFullName(string fullName, string role = "Student")
        {
            if (!File.Exists(filePath)) return "";

            using (var workbook = new XLWorkbook(filePath))
            {
                if (!workbook.Worksheets.Contains(SheetName)) return "";

                var ws = workbook.Worksheet(SheetName);
                var rows = ws.RangeUsed()?.RowsUsed();
                if (rows == null) return "";

                foreach (var row in rows)
                {
                    if (row.RowNumber() == 1) continue;

                    string name = row.Cell(4).GetString();
                    string rowRole = row.Cell(6).GetString();

                    if (name == fullName && rowRole == role)
                        return row.Cell(3).GetString(); // ID
                }
            }

            return "";
        }

        public static List<string> GetAllStudentNames()
        {
            List<string> names = new List<string>();

            if (!File.Exists(filePath)) return names;

            using (var workbook = new XLWorkbook(filePath))
            {
                if (!workbook.Worksheets.Contains(SheetName)) return names;

                var ws = workbook.Worksheet(SheetName);
                var rows = ws.RangeUsed()?.RowsUsed();
                if (rows == null) return names;

                foreach (var row in rows)
                {
                    if (row.RowNumber() == 1) continue;

                    string role = row.Cell(6).GetString();
                    if (role == "Student")
                    {
                        string name = row.Cell(4).GetString();
                        names.Add(name);
                    }
                }
            }

            return names;
        }

        public static bool IsUserAndPasswordCorrect(string username, string password)
        {
            if (!File.Exists(filePath)) return false;

            using (var workbook = new XLWorkbook(filePath))
            {
                if (!workbook.Worksheets.Contains(SheetName)) return false;

                var ws = workbook.Worksheet(SheetName);
                var rows = ws.RangeUsed()?.RowsUsed();
                if (rows == null) return false;

                foreach (var row in rows)
                {
                    if (row.RowNumber() == 1) continue;

                    string u = row.Cell(1).GetString();
                    string p = row.Cell(2).GetString();

                    if (u == username && p == password)
                        return true;
                }
            }

            return false;
        }

        public static bool IDExists(string id)
        {
            if (!File.Exists(filePath)) return false;

            using (var workbook = new XLWorkbook(filePath))
            {
                if (!workbook.Worksheets.Contains(SheetName)) return false;

                var ws = workbook.Worksheet(SheetName);
                var rows = ws.RangeUsed()?.RowsUsed();
                if (rows == null) return false;

                foreach (var row in rows)
                {
                    if (row.RowNumber() == 1) continue;

                    string existingId = row.Cell(3).GetString(); // עמודת ת"ז
                    if (existingId == id)
                        return true;
                }
            }

            return false;
        }

        // ===== פונקציות ביקורות חדשות =====

        public static List<StudentInfo> GetAllStudents()
        {
            List<StudentInfo> students = new List<StudentInfo>();

            if (!File.Exists(filePath)) return students;

            using (var workbook = new XLWorkbook(filePath))
            {
                if (!workbook.Worksheets.Contains(SheetName)) return students;

                var ws = workbook.Worksheet(SheetName);
                var rows = ws.RangeUsed()?.RowsUsed();
                if (rows == null) return students;

                foreach (var row in rows)
                {
                    if (row.RowNumber() == 1) continue;

                    string role = row.Cell(6).GetString();
                    if (role == "Student")
                    {
                        string username = row.Cell(1).GetString();
                        string fullName = row.Cell(4).GetString();

                        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(fullName))
                        {
                            students.Add(new StudentInfo { Username = username, FullName = fullName });
                        }
                    }
                }
            }

            return students;
        }

        public static bool SaveReview(string lecturerFullName, string studentUsername, string studentFullName,
                                     string message, string rating)
        {
            try
            {
                using (var workbook = File.Exists(filePath) ? new XLWorkbook(filePath) : new XLWorkbook())
                {
                    // בדוק אם גיליון הביקורות קיים, אם לא - צור אותו
                    IXLWorksheet reviewsWorksheet;
                    if (workbook.Worksheets.Contains(ReviewsSheetName))
                    {
                        reviewsWorksheet = workbook.Worksheet(ReviewsSheetName);
                    }
                    else
                    {
                        reviewsWorksheet = workbook.AddWorksheet(ReviewsSheetName);

                        // הוסף כותרות
                        reviewsWorksheet.Cell(1, 1).Value = "תאריך";
                        reviewsWorksheet.Cell(1, 2).Value = "מרצה";
                        reviewsWorksheet.Cell(1, 3).Value = "סטודנט (שם משתמש)";
                        reviewsWorksheet.Cell(1, 4).Value = "סטודנט (שם מלא)";
                        reviewsWorksheet.Cell(1, 5).Value = "הודעה";
                        reviewsWorksheet.Cell(1, 6).Value = "דירוג";

                        // עיצוב כותרות
                        var headerRange = reviewsWorksheet.Range("A1:F1");
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    }

                    // מצא את השורה הריקה הבאה
                    int lastRow = reviewsWorksheet.LastRowUsed()?.RowNumber() ?? 1;
                    int newRow = lastRow + 1;

                    // הוסף את הנתונים
                    reviewsWorksheet.Cell(newRow, 1).Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                    reviewsWorksheet.Cell(newRow, 2).Value = lecturerFullName;
                    reviewsWorksheet.Cell(newRow, 3).Value = studentUsername;
                    reviewsWorksheet.Cell(newRow, 4).Value = studentFullName;
                    reviewsWorksheet.Cell(newRow, 5).Value = message;
                    reviewsWorksheet.Cell(newRow, 6).Value = rating;

                    // שמור את הקובץ
                    workbook.SaveAs(filePath);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בשמירת הביקורת: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static List<ReviewInfo> GetReviewsForStudent(string studentUsername)
        {
            List<ReviewInfo> reviews = new List<ReviewInfo>();

            if (!File.Exists(filePath)) return reviews;

            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    if (!workbook.Worksheets.Contains(ReviewsSheetName)) return reviews;

                    var ws = workbook.Worksheet(ReviewsSheetName);
                    var rows = ws.RangeUsed()?.RowsUsed();
                    if (rows == null) return reviews;

                    foreach (var row in rows)
                    {
                        if (row.RowNumber() == 1) continue; // דלג על כותרות

                        string studentUsernameInRow = row.Cell(3).GetString(); // עמודה C - Student Username

                        if (studentUsernameInRow == studentUsername)
                        {
                            reviews.Add(new ReviewInfo
                            {
                                Date = row.Cell(1).GetString(),
                                LecturerName = row.Cell(2).GetString(),
                                Message = row.Cell(5).GetString(),
                                Rating = row.Cell(6).GetString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בטעינת הביקורות: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return reviews;
        }
    }

    // מחלקות עזר לביקורות
    public class StudentInfo
    {
        public string Username { get; set; }
        public string FullName { get; set; }
    }

    public class ReviewInfo
    {
        public string Date { get; set; }
        public string LecturerName { get; set; }
        public string Message { get; set; }
        public string Rating { get; set; }
    }
}