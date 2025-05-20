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

        /// <summary>
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

        /// <summary>
        /// שומר את הציון בגליון Grades לפי תלמיד וקטגוריה
        /// </summary>
        public static void SaveGrade(string filePath, string student, string category, int score)
        {
            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("Grades");

                int col = ws
                    .Row(2)
                    .CellsUsed()
                    .FirstOrDefault(c => c.GetString() == category)?
                    .Address.ColumnNumber ?? -1;
                if (col < 2) return;

                var rowCell = ws
                    .Column(1)
                    .CellsUsed()
                    .Skip(1)
                    .FirstOrDefault(c => c.GetString() == student);

                if (rowCell == null)
                {
                    int newRow = ws.LastRowUsed().RowNumber() + 1;
                    ws.Cell(newRow, 1).Value = student;
                    ws.Cell(newRow, col).Value = score;
                }
                else
                {
                    rowCell.WorksheetRow().Cell(col).Value = score;
                }

                wb.Save();
            }
        }
    }
}
