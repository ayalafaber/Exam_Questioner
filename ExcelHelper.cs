using ClosedXML.Excel;
using System;
using System.IO;
using System.Windows.Forms;

namespace Study_Management
{
    public static class ExcelHelper
    {
        private static readonly string filePath;

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
                string sheetName = role + "s"; // Students או Lecturers

                if (!workbook.Worksheets.Contains(sheetName)) return false;

                var ws = workbook.Worksheet(sheetName);
                var rows = ws.RangeUsed()?.RowsUsed();
                if (rows == null) return false;

                foreach (var row in rows)
                {
                    if (row.RowNumber() == 1) continue;

                    string u = row.Cell(1).GetString();
                    string p = row.Cell(2).GetString();
                    string r = row.Cell(5).GetString();

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
                foreach (var role in new[] { "Students", "Lecturers" })
                {
                    if (!workbook.Worksheets.Contains(role)) continue;

                    var ws = workbook.Worksheet(role);
                    var rows = ws.RangeUsed()?.RowsUsed();
                    if (rows == null) continue;

                    foreach (var row in rows)
                    {
                        if (row.RowNumber() == 1) continue;

                        string u = row.Cell(1).GetString();
                        if (u == username)
                            return true;
                    }
                }
            }

            return false;
        }

        public static bool RegisterUser(string username, string password, string id, string email, string role)
        {
            XLWorkbook workbook;
            IXLWorksheet ws;
            string sheetName = role + "s"; // Students או Lecturers

            if (File.Exists(filePath))
            {
                workbook = new XLWorkbook(filePath);

                // צור את הגיליון אם אינו קיים
                if (!workbook.Worksheets.Contains(sheetName))
                {
                    ws = workbook.AddWorksheet(sheetName);
                    AddHeaders(ws, role);
                }
                else
                {
                    ws = workbook.Worksheet(sheetName);
                }
            }
            else
            {
                workbook = new XLWorkbook();
                ws = workbook.AddWorksheet(sheetName);
                AddHeaders(ws, role);
            }

            // בדיקה אם המשתמש כבר קיים
            var rows = ws.RangeUsed()?.RowsUsed();
            if (rows != null)
            {
                foreach (var row in rows)
                {
                    if (row.RowNumber() == 1) continue;
                    if (row.Cell(1).GetString() == username)
                        return false;
                }
            }

            int nextRow = ws.LastRowUsed()?.RowNumber() + 1 ?? 2;
            ws.Cell(nextRow, 1).Value = username;
            ws.Cell(nextRow, 2).Value = password;
            ws.Cell(nextRow, 3).Value = id;
            ws.Cell(nextRow, 4).Value = email;
            ws.Cell(nextRow, 5).Value = role;

            workbook.SaveAs(filePath);
            return true;
        }

        // הוספת כותרות מתאימות לפי תפקיד
        private static void AddHeaders(IXLWorksheet ws, string role)
        {
            ws.Cell(1, 1).Value = "Username";
            ws.Cell(1, 2).Value = "Password";
            ws.Cell(1, 3).Value = "ID";
            ws.Cell(1, 4).Value = "Email";
            ws.Cell(1, 5).Value = "Role";

            if (role == "Student")
            {
                ws.Cell(1, 6).Value = "Course1";
                ws.Cell(1, 7).Value = "Grade1";
                ws.Cell(1, 8).Value = "Course2";
                ws.Cell(1, 9).Value = "Grade2";
            }
            else if (role == "Lecturer")
            {
                ws.Cell(1, 6).Value = "CoursesTaught";
                ws.Cell(1, 7).Value = "StudentsLinked";
            }
        }
    }
}
