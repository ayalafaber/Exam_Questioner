using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;

namespace Exam_Questioner
{
    public static class ExamLogic
    {
        /// <summary>
        /// טוען את כל השאלות של מבחן לפי מזהה גיליון
        /// </summary>
        public static List<Question> LoadQuestions(string filePath, string examId)
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet(examId);
                return ws
                    .RangeUsed()
                    .RowsUsed()
                    .Skip(1)
                    .Select(r => new Question
                    {
                        Text = r.Cell(1).GetString().Trim(),
                        Type = r.Cell(2).GetString().Trim(),
                        Difficulty = r.Cell(3).GetString().Trim(),
                        Correct = r.Cell(5).GetString().Trim(),
                        Choices = new List<string>
                        {
                            r.Cell(5).GetString().Trim(),
                            r.Cell(6).GetString().Trim(),
                            r.Cell(7).GetString().Trim(),
                            r.Cell(8).GetString().Trim()
                        }
                    })
                    .ToList();
            }
        }
        public static bool HasExistingGrade(string filePath, string student, string category, string difficulty)
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("Grades");

                // 1. מוצאים את עמודת הקטגוריה בשורה 2
                int colCategory = ws
                    .Row(2)
                    .CellsUsed()
                    .FirstOrDefault(c =>
                        c.GetString().Trim().Equals(category.Trim(), StringComparison.OrdinalIgnoreCase))
                    ?.Address.ColumnNumber ?? -1;
                if (colCategory < 1)
                    return false;

                // 2. מוצאים בתוך שלוש העמודות של הקטגוריה את העמודה המתאימה לקושי
                int targetCol = -1;
                for (int offset = 0; offset < 3; offset++)
                {
                    if (ws.Cell(3, colCategory + offset)
                          .GetString()
                          .Trim()
                          .Equals(difficulty.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        targetCol = colCategory + offset;
                        break;
                    }
                }
                if (targetCol < 1)
                    return false;

                // 3. מוצאים את השורה של התלמיד בעמודה A (רשומות משורה 4 ומטה)
                var rowCell = ws
                    .Column(1)
                    .CellsUsed()
                    .FirstOrDefault(c =>
                        c.GetString().Trim().Equals(student.Trim(), StringComparison.OrdinalIgnoreCase)
                        && c.Address.RowNumber >= 4);
                if (rowCell == null)
                    return false;

                // 4. קוראים את התוכן של התא בעמודה targetCol באותה שורה כ־string
                string raw = ws.Cell(rowCell.Address.RowNumber, targetCol)
                               .GetString()
                               .Trim();
                // 5. מנסים לפרסר ל־int – אם הצליחו ויש ערך גדול מאפס, יש ציון קיים
                if (int.TryParse(raw, out int score) && score > 0)
                    return true;

                return false;
            }
        }


        /// בודק תשובה פתוחה על בסיס פידבק מ־GPT ומחזיר ציון
        /// </summary>
        public static double EvaluateOpenAnswer(string feedback)
        {
            if (string.IsNullOrWhiteSpace(feedback)) return 0.0;
            if (feedback.StartsWith("כן", StringComparison.OrdinalIgnoreCase)) return 1.0;
            if (feedback.Contains("חצי")) return 0.5;
            return 0.0;
        }

        /// <summary>
        /// בודק תשובה סגורה על בסיס התאמה מדויקת
        /// </summary>
        public static double EvaluateClosedAnswer(string userAnswer, string correctAnswer)
        {
            return userAnswer?.Trim() == correctAnswer?.Trim() ? 1.0 : 0.0;
        }

        /// <summary>
        /// מחשב את הציון הכללי על בסיס טבלת ציונים לשאלות
        /// </summary>
        public static int CalculateScore(List<Question> questions, Dictionary<int, double> questionScores)
        {
            if (questions == null || questions.Count == 0) return 0;
            double total = questionScores.Sum(kv => kv.Value);
            return (int)Math.Round(100.0 * total / questions.Count);
        }

        /// <summary>
        /// מחלץ את הקטגוריה של מבחן מתוך גיליון ExamID לפי מזהה
        /// </summary>
        public static string GetGradeCategory(string filePath, string examId)
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("ExamID");
                var cell = ws
                    .Column(1)
                    .CellsUsed()
                    .FirstOrDefault(c => c.GetString() == examId);
                return cell?.WorksheetRow().Cell(2).GetString() ?? "Unknown";
            }
        }

        public static string GetGradeDifficulty(string filePath, string examId)
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("ExamID");
                // מוצאים את התא בעמודה A שבו יש את examId
                var cell = ws
                    .Column(1)
                    .CellsUsed()
                    .FirstOrDefault(c => c.GetString() == examId);

                // אם מצאנו, נחזיר את הערך של העמודה השלישית (C) משורה זו
                return cell?.WorksheetRow().Cell(3).GetString() ?? "קל";
            }
        }

        /// מוסיף קטגוריה חדשה לגיליון Grades עם 3 עמודות (קל, בינוני, קשה)
        private static void AddNewCategoryToGrades(IXLWorksheet ws, string category)
        {
            // מוצאים את העמודה הפנויה הבאה (אחרי "Student's Name" ואחרי כל הקטגוריות הקיימות)
            int nextCol = 2; // מתחילים מעמודה B (אחרי "Student's Name")

            // מוצאים את העמודה הפנויה הבאה
            while (!string.IsNullOrWhiteSpace(ws.Cell(2, nextCol).GetString()))
            {
                nextCol += 3; // כל קטגוריה תופסת 3 עמודות
            }

            // מוסיפים את הקטגוריה החדשה
            // שורה 2: שם הקטגוריה (מתפרסת על 3 עמודות)
            ws.Cell(2, nextCol).Value = category;
            ws.Range(2, nextCol, 2, nextCol + 2).Merge();
            ws.Cell(2, nextCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(2, nextCol).Style.Font.Bold = true;

            // שורה 3: רמות הקושי
            ws.Cell(3, nextCol).Value = "קל";
            ws.Cell(3, nextCol + 1).Value = "בינוני";
            ws.Cell(3, nextCol + 2).Value = "קשה";

            // עיצוב הכותרות
            for (int i = 0; i < 3; i++)
            {
                var cell = ws.Cell(3, nextCol + i);
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Font.Bold = true;
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }

            Console.WriteLine($"קטגוריה חדשה '{category}' נוספה לגיליון Grades בעמודות {nextCol}-{nextCol + 2}");
        }

        /// בדיקה אם קטגוריה קיימת בגיליון Grades
        private static bool CategoryExists(IXLWorksheet ws, string category)
        {
            return ws.Row(2)
                     .CellsUsed()
                     .Any(c => c.GetString().Trim().Equals(category.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// שומר את הציון בגליון Grades לפי תלמיד וקטגוריה
        public static void SaveGrade(string filePath, string student, string category, string difficulty, int score)
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("Grades");

                // בדיקה אם הקטגוריה קיימת, ואם לא - הוספתה
                if (!CategoryExists(ws, category))
                {
                    AddNewCategoryToGrades(ws, category);
                }

                // 1. מוצאים את העמודה שבה מופיעה הקטגוריה בשורה השנייה (Row 2)
                int colCategory = ws
                    .Row(2)
                    .CellsUsed()
                    .FirstOrDefault(c => c.GetString().Trim().Equals(category.Trim(), StringComparison.OrdinalIgnoreCase))
                    ?.Address.ColumnNumber
                    ?? -1;

                if (colCategory < 1)
                {
                    Console.WriteLine("שגיאה: קטגוריה לא נמצאה גם לאחר הניסיון להוסיף אותה.");
                    return;
                }

                // 2. בתוך העמודות של אותה קטגוריה (colCategory, colCategory+1, colCategory+2),
                //    מוצאים איזו עמודה מתאימה ל־difficulty (Row 3)
                int targetCol = -1;
                for (int offset = 0; offset < 3; offset++)
                {
                    string diffVal = ws.Cell(3, colCategory + offset)
                                        .GetString()
                                        .Trim();
                    if (string.Equals(diffVal, difficulty.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        targetCol = colCategory + offset;
                        break;
                    }
                }

                if (targetCol < 1)
                {
                    Console.WriteLine(
                        $"לא נמצאה עמודה עבור רמת הקושי \"{difficulty}\" ב-Row 3 תחת קטגוריה \"{category}\".");
                    return;
                }

                // 3. מוצאים את השורה של התלמיד (עמודה A). משורות 4 ומטה (מספר שורה >= 4)
                var rowCell = ws
                    .Column(1)
                    .CellsUsed()
                    .FirstOrDefault(c =>
                        c.GetString().Trim().Equals(student.Trim(), StringComparison.OrdinalIgnoreCase)
                        && c.Address.RowNumber >= 4);

                if (rowCell == null)
                {
                    // אם התלמיד לא קיים – מוסיפים שורה חדשה בתחתית
                    int newRow = ws.LastRowUsed().RowNumber() + 1;
                    ws.Cell(newRow, 1).Value = student;          // שם התלמיד בעמודה A
                    ws.Cell(newRow, targetCol).Value = score;    // ושם הציון בעמודה המתאימה
                }
                else
                {
                    // אם התלמיד כבר קיים – מעדכנים את העמודה המתאימה
                    rowCell.WorksheetRow().Cell(targetCol).Value = score;
                }

                wb.Save();
                Console.WriteLine(
                    $"הציון של \"{student}\" (קטגוריה={category}, קושי={difficulty}) נשמר בהצלחה.");
            }
        }
    }
}