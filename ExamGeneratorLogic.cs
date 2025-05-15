using ClosedXML.Excel;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace Exam_Questioner
{
    public static class ExamGeneratorLogic
    {
        public static bool TryCreateExam(
    string subject,
    string difficulty,
    string questionCountText,
    List<string> allSubjects,
    List<string> allDifficulties,
    out string message,
    out string examId)
        {
            examId = null;

            // 1. בדיקת קלט ראשונית
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
                message = "מספר השאלות חייב להיות בין 4 ל-12.";
                return false;
            }

            // 2. הגדרת נתיב לקובץ ה-Excel על שולחן-העבודה
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "database.xlsx");
            if (!File.Exists(filePath))
            {
                message = "קובץ database.xlsx לא נמצא על שולחן-העבודה.";
                return false;
            }

            using (var wb = new XLWorkbook(filePath))
            {
                // 3. קבלת גיליונות השאלות והמזהים
                var wsQ = wb.Worksheet("Questions");  // מכיל את כל השאלות עם העמודות A–I
                var wsIds = wb.Worksheet("ExamID");     // מכיל רשימת מבחנים ושמותיהם

                // 4. לכל שורה ב-ExamID שאין לה ID – צור GUID קצר ושמור
                foreach (var idRow in wsIds.RowsUsed().Skip(1))
                {
                    var existing = idRow.Cell(1).GetString().Trim();
                    if (string.IsNullOrEmpty(existing))
                    {
                        var newGuid = Guid.NewGuid().ToString("N");
                        idRow.Cell(1).Value = newGuid.Substring(0, Math.Min(31, newGuid.Length));
                    }
                }

                // 5. בנה GUID ייחודי חדש לשם הגיליון של המבחן
                var rawGuid = Guid.NewGuid().ToString("N");
                examId = rawGuid.Substring(0, Math.Min(31, rawGuid.Length));

                // 6. אסוף את כל השורות בגיליון Questions (שורה 2 ואילך)
                var allRows = wsQ.RangeUsed().RowsUsed().Skip(1);

                // 7. סינון השאלות לפי נושא ורמת קושי (עם Trim להסרת רווחים סביב)
                var pool = allRows
                    .Where(r => string.Equals(r.Cell(4).GetString().Trim(), subject, StringComparison.OrdinalIgnoreCase)
                             && string.Equals(r.Cell(5).GetString().Trim(), difficulty, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // 8. בדיקת מספר השאלות הזמינות מול הנדרש
                if (pool.Count < needed)
                {
                    message = "אין מספיק שאלות במאגר לתנאים המבוקשים.";
                    return false;
                }

                // 9. יצירת גיליון חדש בשם ה-GUID ושכפול שורת הכותרת (עמודות B–I)
                var wsTest = wb.Worksheets.Add(examId);
                var header = wsQ.Row(1);
                int lastCol = wsQ.LastColumnUsed().ColumnNumber();
                for (int col = 2; col <= lastCol; col++)
                {
                    wsTest.Cell(1, col - 1).Value = header.Cell(col).Value;
                }

                // 10. בחירת שאלות אקראיות והעתקתן אל הגיליון החדש
                var rnd = new Random();
                var selected = pool.OrderBy(_ => rnd.Next()).Take(needed).ToList();
                int rowIdx = 2;
                foreach (var qRow in selected)
                {
                    for (int col = 2; col <= lastCol; col++)
                    {
                        wsTest.Cell(rowIdx, col - 1).Value = qRow.Cell(col).Value;
                    }
                    rowIdx++;
                }

                // 11. הוספת רשומה חדשה בגיליון ExamID: GUID, נושא ורמת קושי
                int newIdRow = (wsIds.LastRowUsed()?.RowNumber() ?? 1) + 1;
                wsIds.Cell(newIdRow, 1).Value = examId;
                wsIds.Cell(newIdRow, 2).Value = subject;
                wsIds.Cell(newIdRow, 3).Value = difficulty;

                // 12. שמירת כל השינויים חזרה ל-Excel
                wb.Save();
            }

            message = $"נוצר מבחן חדש: {examId}";
            return true;
        }


    }
}