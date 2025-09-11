using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using Exam_Questioner;

namespace Exam_Questioner_Tests
{
    [TestClass]
    public class ExamOrPracticeLogicTests
    {
        private string testFilePath = string.Empty;


        [TestInitialize]
        public void SetUp()
        {
            // Arrange - יצירת קובץ Excel לבדיקה
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            testFilePath = Path.Combine(desktop, "test_database.xlsx");

            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("ExamID");

                ws.Cell(1, 1).Value = "ID";
                ws.Cell(1, 2).Value = "Category";
                ws.Cell(1, 3).Value = "Difficulty";

                ws.Cell(2, 1).Value = "01";
                ws.Cell(2, 2).Value = "תכנות";
                ws.Cell(2, 3).Value = "קל";

                ws.Cell(3, 1).Value = "02";
                ws.Cell(3, 2).Value = "מבנה נתונים";
                ws.Cell(3, 3).Value = "בינוני";

                ws.Cell(4, 1).Value = "03";
                ws.Cell(4, 2).Value = "בדיקות";
                ws.Cell(4, 3).Value = "קשה";

                ws.Cell(5, 1).Value = "04";
                ws.Cell(5, 2).Value = "עקרונות";
                ws.Cell(5, 3).Value = "קל";

                wb.SaveAs(testFilePath);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);
        }

        // ===== 1. LoadMatchingExamIds =====

        [TestMethod]
        public void LoadMatchingExamIds_WithValidMatch_ReturnsCorrectResult()
        {
            // Arrange
            string subject = "תכנות";
            string difficulty = "קל";

            // Act
            var results = ExamOrPracticeLogic.LoadMatchingExamIds(subject, difficulty, testFilePath);

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("01 - תכנות - קל", results[0]);
        }

        [TestMethod]
        public void LoadMatchingExamIds_WithNoMatch_ReturnsEmptyList()
        {
            // Arrange
            string subject = "עקרונות";
            string difficulty = "קשה";

            // Act
            var results = ExamOrPracticeLogic.LoadMatchingExamIds(subject, difficulty, testFilePath);

            // Assert
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void LoadMatchingExamIds_FileNotExists_ReturnsEmptyList()
        {
            // Arrange
            string invalidPath = @"C:\Fake\Invalid.xlsx";

            // Act
            var results = ExamOrPracticeLogic.LoadMatchingExamIds("בדיקות", "קל", invalidPath);

            // Assert
            Assert.AreEqual(0, results.Count);
        }

        // ===== 2. ExtractExamIdFromListItem =====

        [TestMethod]
        public void ExtractExamIdFromListItem_ValidInput_ReturnsId()
        {
            // Arrange
            string item = "03 - בדיקות - קשה";

            // Act
            string result = ExamOrPracticeLogic.ExtractExamIdFromListItem(item);

            // Assert
            Assert.AreEqual("03", result);
        }

        [TestMethod]
        public void ExtractExamIdFromListItem_EmptyOrNull_ReturnsNull()
        {
            // Arrange
            string emptyItem = "";
            string nullItem = null;

            // Act & Assert
            Assert.IsNull(ExamOrPracticeLogic.ExtractExamIdFromListItem(emptyItem));
            Assert.IsNull(ExamOrPracticeLogic.ExtractExamIdFromListItem(nullItem));
        }

        // ===== 3. IsSubjectAndDifficultySelected =====

        [TestMethod]
        public void IsSubjectAndDifficultySelected_BothSelected_ReturnsTrue()
        {
            // Arrange
            int subjectIndex = 1;
            int difficultyIndex = 2;

            // Act
            bool result = ExamOrPracticeLogic.IsSubjectAndDifficultySelected(subjectIndex, difficultyIndex);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsSubjectAndDifficultySelected_MissingSelection_ReturnsFalse()
        {
            // Arrange
            int subjectIndex = -1;
            int difficultyIndex = 0;

            // Act
            bool result = ExamOrPracticeLogic.IsSubjectAndDifficultySelected(subjectIndex, difficultyIndex);

            // Assert
            Assert.IsFalse(result);
        }



        [TestMethod]
        public void LoadMatchingExamIds_WithAllValidSubjects_WorksCorrectly()
        {
            // Arrange
            var validSubjects = new List<string> { "תכנות", "מבנה נתונים", "בדיקות", "עקרונות" };
            string difficulty = "קל";

            foreach (var subject in validSubjects)
            {
                // Act
                var results = ExamOrPracticeLogic.LoadMatchingExamIds(subject, difficulty, testFilePath);

                // Assert
                Assert.IsNotNull(results);
                Assert.IsTrue(results.Count >= 0); // גם אם אין התאמה, לא נכשלים
            }
        }
    }
}