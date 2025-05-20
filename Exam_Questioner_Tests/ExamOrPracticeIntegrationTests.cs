using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using Exam_Questioner;

namespace Exam_Questioner_Tests
{
    [TestClass]
    public class ExamOrPracticeIntegrationTests
    {
        private string testFilePath = string.Empty;


        [TestInitialize]
        public void SetUp()
        {
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

                wb.SaveAs(testFilePath);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);
        }

        [TestMethod]
        public void LoadMatchingExamIds_ReturnsCorrectItems()
        {
            var results = ExamOrPracticeLogic.LoadMatchingExamIds("תכנות", "קל", testFilePath);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("01 - תכנות - קל", results[0]);
        }

        [TestMethod]
        public void LoadMatchingExamIds_NoMatches_ReturnsEmpty()
        {
            var results = ExamOrPracticeLogic.LoadMatchingExamIds("היסטוריה", "קשה", testFilePath);
            Assert.AreEqual(0, results.Count);
        }
    }
}
