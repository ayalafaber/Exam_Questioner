using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;

namespace Exam_Questioner
{
    public static class ExamLogic
    {
        /// טוען את כל השאלות של מבחן לפי מזהה גיליון
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

        /// בודק תשובה פתוחה על בסיס פידבק מ־GPT ומחזיר ציון
        public static double EvaluateOpenAnswer(string feedback)
        {
            if (string.IsNullOrWhiteSpace(feedback)) return 0.0;
            if (feedback.StartsWith("כן", StringComparison.OrdinalIgnoreCase)) return 1.0;
            if (feedback.Contains("חצי")) return 0.5;
            return 0.0;
        }

        /// בודק תשובה סגורה על בסיס התאמה מדויקת
        public static double EvaluateClosedAnswer(string userAnswer, string correctAnswer)
        {
            return userAnswer?.Trim() == correctAnswer?.Trim() ? 1.0 : 0.0;
        }

        /// מחשב את הציון הכללי על בסיס טבלת ציונים לשאלות
        public static int CalculateScore(List<Question> questions, Dictionary<int, double> questionScores)
        {
            if (questions == null || questions.Count == 0) return 0;
            double total = questionScores.Sum(kv => kv.Value);
            return (int)Math.Round(100.0 * total / questions.Count);
        }

        /// מחלץ את הקטגוריה של מבחן מתוך גיליון ExamID לפי מזהה
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

        /// שומר את הציון בגליון Grades לפי תלמיד וקטגוריה
        public static void SaveGrade(string filePath, string student, string category, string difficulty, int score)
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("Grades");

                // 1. מוצאים את העמודה שבה מופיעה הקטגוריה בשורה הראשונה (Row 1)
                int colCategory = ws
                    .Row(2)
                    .CellsUsed()
                    .FirstOrDefault(c => c.GetString().Trim() == category.Trim())
                    ?.Address.ColumnNumber
                    ?? -1;

                if (colCategory < 1)
                {
                    Console.WriteLine("קטגוריה לא נמצאה בשורה הראשונה של גיליון Grades.");
                    return;
                }

                // 2. בתוך העמודות של אותה קטגוריה (colCategory, colCategory+1, colCategory+2),
                //    מוצאים איזו עמודה מתאימה ל־difficulty (Row 2)
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

                // 3. מוצאים את השורה של התלמיד (עמודה A). משורות 2 ומטה (מספר שורה >= 3)
                var rowCell = ws
            .Column(1)
            .CellsUsed()
            .FirstOrDefault(c =>
                c.GetString().Trim() == student.Trim()
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
