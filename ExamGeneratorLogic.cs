using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Exam_Questioner
{
    public static class ExamGeneratorLogic
    {
        public static bool TryCreateExam(string subject, string difficulty, string questionCountText,
                                 List<string> allSubjects, List<string> allDifficulties,
                                 out string message, out string examId)
        {
            examId = null;

            if (string.IsNullOrWhiteSpace(subject))
            {
                message = "בחר נושא.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(difficulty))
            {
                message = "בחר רמת קושי.";
                return false;
            }

            if (!int.TryParse(questionCountText, out int needed) || needed < 4 || needed > 12)
            {
                message = "מספר השאלות חייב להיות בין 4 ל‑12.";
                return false;
            }

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "database.xlsx");
            if (!File.Exists(filePath))
            {
                message = "קובץ database.xlsx לא נמצא על שולחן‑העבודה.";
                return false;
            }

            var rnd = new Random();

            using (var wb = new XLWorkbook(filePath))
            {
                var wsQ = wb.Worksheet("Questions");
                if (wsQ == null)
                {
                    message = "הגיליון 'Questions' לא קיים.";
                    return false;
                }

                var rows = wsQ.RangeUsed()?.RowsUsed()?.ToList();
                if (rows == null || rows.Count == 0)
                {
                    message = "הגיליון 'Questions' ריק.";
                    return false;
                }

                var pool = rows
                    .Where(r =>
                        string.Equals(r.Cell(4).GetString().Trim(), subject.Trim(), StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(r.Cell(5).GetString().Trim(), difficulty.Trim(), StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (pool.Count < needed)
                {
                    message = $"אין מספיק שאלות במאגר ({pool.Count}) לנושא '{subject}' ורמה '{difficulty}'.";
                    return false;
                }

                var wsIds = wb.Worksheet("ExamID");
                if (wsIds == null)
                {
                    message = "הגיליון 'ExamID' לא נמצא.";
                    return false;
                }

                // 🟢 שלב 1: מלא שורות ריקות קיימות במזהה אקראי (לניקוי ישנים)
                foreach (var row in wsIds.RowsUsed().Skip(1))
                {
                    var cell = row.Cell(1);
                    if (string.IsNullOrWhiteSpace(cell.GetString()))
                    {
                        var tempGuid = Guid.NewGuid().ToString("N");
                        cell.Value = tempGuid.Substring(0, Math.Min(31, tempGuid.Length));
                    }
                }

                // 🟢 שלב 2: הפקת מזהה חדש ייחודי (עד 31 תווים, ללא כפילות בגיליונות)
                string tempId;
                do
                {
                    var rawGuid = Guid.NewGuid().ToString("N");
                    tempId = rawGuid.Substring(0, Math.Min(31, rawGuid.Length));
                }
                while (wb.Worksheets.Any(ws => ws.Name == tempId));

                examId = tempId;

                // יצירת גיליון חדש עם מזהה זה
                var selected = pool.OrderBy(_ => rnd.Next()).Take(needed).ToList();
                var wsTest = wb.Worksheets.Add(examId);

                var header = wsQ.Row(1);
                int lastCol = wsQ.LastColumnUsed().ColumnNumber();
                for (int col = 2; col <= lastCol; col++)
                {
                    wsTest.Cell(1, col - 1).Value = header.Cell(col).Value;
                }

                int rowIdx = 2;
                foreach (var qRow in selected)
                {
                    for (int col = 2; col <= lastCol; col++)
                    {
                        wsTest.Cell(rowIdx, col - 1).Value = qRow.Cell(col).Value;
                    }
                    rowIdx++;
                }

                int newIdRow = (wsIds.LastRowUsed()?.RowNumber() ?? 1) + 1;
                wsIds.Cell(newIdRow, 1).Value = examId;
                wsIds.Cell(newIdRow, 2).Value = subject;
                wsIds.Cell(newIdRow, 3).Value = difficulty;

                wb.Save();
            }

            message = $"נוצר מבחן חדש: {examId}";
            return true;
        }

        public static bool DeleteExam(string examId, out string message)
        {
            message = "";
            if (string.IsNullOrWhiteSpace(examId))
            {
                message = "מזהה מבחן לא חוקי.";
                return false;
            }

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "database.xlsx");
            if (!File.Exists(filePath))
            {
                message = "קובץ database.xlsx לא נמצא על שולחן‑העבודה.";
                return false;
            }

            using (var wb = new XLWorkbook(filePath))
            {
                if (wb.Worksheets.Contains(examId))
                    wb.Worksheets.Delete(examId);

                var wsIds = wb.Worksheet("ExamID");
                var cell = wsIds
                    .Column(1)
                    .CellsUsed()
                    .FirstOrDefault(c => c.GetString() == examId);

                if (cell != null)
                    cell.WorksheetRow().Delete();

                wb.Save();
            }

            message = $"המבחן {examId} נמחק.";
            return true;
        }

        public static List<string[]> LoadExam(string examId, out string message)
        {
            message = "";
            List<string[]> result = new List<string[]>();

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "database.xlsx");
            if (!File.Exists(filePath))
            {
                message = "הקובץ database.xlsx לא נמצא על שולחן‑העבודה.";
                return null;
            }

            using (var wb = new XLWorkbook(filePath))
            {
                if (!wb.Worksheets.Contains(examId))
                {
                    message = "המבחן לא קיים במערכת.";
                    return null;
                }

                var ws = wb.Worksheet(examId);
                foreach (var row in ws.RangeUsed().RowsUsed().Skip(1))
                {
                    result.Add(row.Cells().Select(c => c.GetString()).ToArray());
                }
            }

            return result;
        }

        public static bool IsValidHebrewSubject(string input)
        {
            return !string.IsNullOrWhiteSpace(input) &&
                   Regex.IsMatch(input, @"^[֐-׿\s]+$");
        }

        public static List<string> LoadExamIds()
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "database.xlsx");

            var result = new List<string>();
            if (!File.Exists(filePath))
                return result;

            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("ExamID");
                result = ws.Column(1)
                           .CellsUsed()
                           .Skip(1)
                           .Select(c => c.GetString())
                           .ToList();
            }

            return result;
        }
    }
}
