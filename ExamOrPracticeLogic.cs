using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;

namespace Exam_Questioner
{
    public static class ExamOrPracticeLogic
    {
        /// <summary>
        /// טוען את מזהי המבחנים המתאימים לפי נושא ורמת קושי
        /// </summary>
        public static List<string> LoadMatchingExamIds(string subject, string difficulty, string filePath)
        {
            var results = new List<string>();

            if (!File.Exists(filePath))
                return results;

            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheet("ExamID");
                foreach (var row in ws.RangeUsed().RowsUsed().Skip(1))
                {
                    var id = row.Cell(1).GetString();
                    var category = row.Cell(2).GetString();
                    var level = row.Cell(3).GetString();

                    if (string.Equals(category, subject, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(level, difficulty, StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add($"{id} - {category} - {level}");
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// מחלץ את מזהה המבחן מהשורה שנבחרה ב־ListBox
        /// </summary>
        public static string ExtractExamIdFromListItem(string itemText)
        {
            if (string.IsNullOrWhiteSpace(itemText))
                return null;

            var parts = itemText.Split(new[] { " - " }, StringSplitOptions.None);
            return parts.Length > 0 ? parts[0].Trim() : null;
        }

        /// <summary>
        /// בודק אם גם combobox1 וגם combobox2 נבחרו
        /// </summary>
        public static bool IsSubjectAndDifficultySelected(int subjectIndex, int difficultyIndex)
        {
            return subjectIndex >= 0 && difficultyIndex >= 0;
        }
    }
}
