using ClosedXML.Excel;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace Exam_Questioner
{
    public static class ExamGeneratorLogic
    {
        public static bool TryCreateExam(string subject, string difficulty, string questionCountText,
                                         List<string> allSubjects, List<string> allDifficulties,
                                         out string message, out string examId)
        {
            examId = null;

            // בדיקת קלט בסיסית
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

            var rnd = new Random();

            // אם המשתמש בחר "רנדומלי", נבחר ערכים מתוך הרשימות
            if (subject == "רנדומלי")
            {
                var validSubjects = allSubjects.Where(s => s != "רנדומלי" && s != "אחר").ToList();
                if (!validSubjects.Any())
                {
                    message = "אין נושאים חוקיים ברשימה.";
                    return false;
                }
                subject = validSubjects[rnd.Next(validSubjects.Count)];

                var validDifficulties = allDifficulties;
                difficulty = validDifficulties[rnd.Next(validDifficulties.Count)];
            }

            // נתיב לקובץ
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "database.xlsx");
            if (!File.Exists(filePath))
            {
                message = "קובץ database.xlsx לא נמצא על שולחן‑העבודה.";
                return false;
            }

            using (var wb = new XLWorkbook(filePath))
            {
                var wsQ = wb.Worksheet("Questions");
                var rows = wsQ.RangeUsed().RowsUsed();

                var pool = rows.Where(r =>
                             string.Equals(r.Cell(4).GetString(), subject, StringComparison.OrdinalIgnoreCase) &&
                             string.Equals(r.Cell(5).GetString(), difficulty, StringComparison.OrdinalIgnoreCase))
                             .ToList();

                if (pool.Count < needed)
                {
                    message = "אין מספיק שאלות במאגר לתנאים המבוקשים.";
                    return false;
                }

                var selected = pool.OrderBy(_ => rnd.Next()).Take(needed).ToList();
                var wsIds = wb.Worksheet("ExamID");

                var existingNums = wsIds
                    .Column(1)
                    .CellsUsed()
                    .Skip(1)
                    .Select(c => int.Parse(c.GetString()))
                    .DefaultIfEmpty(0);
                int nextSeq = existingNums.Max() + 1;
                examId = nextSeq.ToString("D2");

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

                var lastRowUsed = wsIds.LastRowUsed();
                int newIdRow = (lastRowUsed != null ? lastRowUsed.RowNumber() : 0) + 1;
                wsIds.Cell(newIdRow, 1).Value = examId;
                wsIds.Cell(newIdRow, 2).Value = subject;
                wsIds.Cell(newIdRow, 3).Value = difficulty;

                wb.Save();
            }

            message = $"נוצר מבחן חדש: {examId}";
            return true;
        }
    }
}