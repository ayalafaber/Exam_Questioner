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
            // יצירת קובץ Excel לדוגמה על שולחן העבודה
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
                ws.Cell(3, 2).Value = "תכנות";
                ws.Cell(3, 3).Value = "בינוני";

                ws.Cell(4, 1).Value = "03";
                ws.Cell(4, 2).Value = "בדיקות";
                ws.Cell(4, 3).Value = "קשה";

                wb.SaveAs(testFilePath);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);
        }

        // ====== 1. בדיקות לפונקציה LoadMatchingExamIds ======
        [TestMethod]
        public void LoadMatchingExamIds_ReturnsCorrectMatch()
        {
            var results = ExamOrPracticeLogic.LoadMatchingExamIds("תכנות", "קל", testFilePath);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("01 - תכנות - קל", results[0]);
        }

        [TestMethod]
        public void LoadMatchingExamIds_NoMatches_ReturnsEmptyList()
        {
            var results = ExamOrPracticeLogic.LoadMatchingExamIds("מתמטיקה", "בינוני", testFilePath);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void LoadMatchingExamIds_FileDoesNotExist_ReturnsEmpty()
        {
            var results = ExamOrPracticeLogic.LoadMatchingExamIds("תכנות", "קל", @"C:\Does\NotExist.xlsx");
            Assert.AreEqual(0, results.Count);
        }

        // ====== 2. בדיקות לפונקציה ExtractExamIdFromListItem ======
        [TestMethod]
        public void ExtractExamIdFromListItem_ValidString_ReturnsId()
        {
            string result = ExamOrPracticeLogic.ExtractExamIdFromListItem("07 - עקרונות - בינוני");
            Assert.AreEqual("07", result);
        }

        [TestMethod]
        public void ExtractExamIdFromListItem_EmptyString_ReturnsNull()
        {
            string result = ExamOrPracticeLogic.ExtractExamIdFromListItem("");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ExtractExamIdFromListItem_NullString_ReturnsNull()
        {
            string result = ExamOrPracticeLogic.ExtractExamIdFromListItem(null);
            Assert.IsNull(result);
        }

        // ====== 3. בדיקות לפונקציה IsSubjectAndDifficultySelected ======
        [TestMethod]
        public void IsSubjectAndDifficultySelected_ValidIndices_ReturnsTrue()
        {
            bool result = ExamOrPracticeLogic.IsSubjectAndDifficultySelected(0, 1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsSubjectAndDifficultySelected_NegativeSubjectIndex_ReturnsFalse()
        {
            bool result = ExamOrPracticeLogic.IsSubjectAndDifficultySelected(-1, 1);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsSubjectAndDifficultySelected_NegativeDifficultyIndex_ReturnsFalse()
        {
            bool result = ExamOrPracticeLogic.IsSubjectAndDifficultySelected(1, -1);
            Assert.IsFalse(result);
        }
    }
}
