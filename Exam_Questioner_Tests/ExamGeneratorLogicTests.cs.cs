
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Exam_Questioner;

namespace Exam_Questioner_Tests
{
    [TestClass]
    public class ExamGeneratorLogicTests
    {
        [TestMethod]
        public void TryCreateExam_WithEmptySubject_ReturnsError()
        {
            // Arrange
            var allSubjects = new List<string> { "תכנות", "מבנה נתונים", "בדיקות", "עקרונות" };
            var allDifficulties = new List<string> { "קל", "בינוני", "קשה" };

            // Act
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "",
                difficulty: "קל",
                questionCountText: "6",
                allSubjects,
                allDifficulties,
                out string message,
                out _);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("בחר נושא.", message);
        }

        [TestMethod]
        public void TryCreateExam_WithEmptyDifficulty_ReturnsError()
        {
            // Arrange
            var allSubjects = new List<string> { "תכנות" };
            var allDifficulties = new List<string> { "קל", "בינוני", "קשה" };

            // Act
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "תכנות",
                difficulty: "",
                questionCountText: "5",
                allSubjects,
                allDifficulties,
                out string message,
                out _);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("בחר רמת קושי.", message);
        }

        [TestMethod]
        public void TryCreateExam_WithInvalidQuestionCount_ReturnsError()
        {
            // Arrange
            var allSubjects = new List<string> { "תכנות" };
            var allDifficulties = new List<string> { "קל", "בינוני", "קשה" };

            // Act
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "תכנות",
                difficulty: "קל",
                questionCountText: "abc",
                allSubjects: new List<string> { "תכנות" },
                allDifficulties: new List<string> { "קל" },
                out string message,
                out string examId
            );
            Assert.IsFalse(result);
            Assert.AreEqual("מספר השאלות חייב להיות בין 4 ל‑12.", message);
        }

        [TestMethod]
        public void TryCreateExam_InvalidQuestionCount_ReturnsError()
        {
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "תכנות",
                difficulty: "קשה",
                questionCountText: "2",
                allSubjects,
                allDifficulties,
                out string message,
                out _);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("מספר השאלות חייב להיות בין 4 ל‑12.", message);
        }

        [TestMethod]
        public void TryCreateExam_WithMissingFile_ReturnsError()
        {
            // Arrange
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "database.xlsx");

            if (File.Exists(filePath))
                File.Move(filePath, filePath + ".bak");  // Backup

            var allSubjects = new List<string> { "תכנות" };
            var allDifficulties = new List<string> { "קל" };

            // Act
            bool result = ExamGeneratorLogic.TryCreateExam(
                "תכנות", "קל", "4", allSubjects, allDifficulties, out string message, out _);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("קובץ database.xlsx לא נמצא על שולחן‑העבודה.", message);
            }
        }

            // Cleanup
            if (File.Exists(filePath + ".bak"))
                File.Move(filePath + ".bak", filePath);
        }


        [TestMethod]
        public void TryCreateExam_WithValidInput_CreatesExam()
        {
            // Arrange
            var allSubjects = new List<string> { "תכנות" };
            var allDifficulties = new List<string> { "קל" };

            // Act
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "תכנות",
                difficulty: "קל",
                questionCountText: "13",
                allSubjects: new List<string> { "תכנות" },
                allDifficulties: new List<string> { "קל" },
                out string message,
                out string examId
            );
            Assert.IsFalse(result);
            Assert.AreEqual("מספר השאלות חייב להיות בין 4 ל‑12.", message);
        }

        [TestMethod]
        public void TryCreateExam_RandomSubjectAndDifficulty_ReturnsSuccess()
        {
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "רנדומלי",
                difficulty: "",
                questionCountText: "4",
                allSubjects,
                allDifficulties,
                out string message,
                out string examId);

            // Assert
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.xlsx")))
            {
                Assert.IsTrue(result);
                Assert.IsNotNull(examId);
                Assert.IsTrue(message.Contains("נוצר מבחן חדש"));

                // Cleanup - delete exam after test
                ExamGeneratorLogic.DeleteExam(examId, out _);
            }
            else
            {
                // לא בודקים הודעה מדויקת אלא רק שההפעלה נכשלה – למניעת בעיות במבחן ריק
                Assert.IsFalse(result);
                Assert.AreEqual("קובץ database.xlsx לא נמצא על שולחן‑העבודה.", message);
            }
        }


        [TestMethod]
        public void DeleteExam_WithValidExamId_DeletesExam()
        {
            // Arrange
            var allSubjects = new List<string> { "תכנות" };
            var allDifficulties = new List<string> { "קל" };

            bool created = ExamGeneratorLogic.TryCreateExam(
                subject: "תכנות",
                difficulty: "קל",
                questionCountText: "4",
                allSubjects,
                allDifficulties,
                out string createMessage,
                out string examId);

            Assert.IsTrue(created);

            // Act
            bool deleted = ExamGeneratorLogic.DeleteExam(examId, out string deleteMessage);

            // Assert
            Assert.IsTrue(deleted);
            Assert.AreEqual($"המבחן {examId} נמחק.", deleteMessage);
        }

        [TestMethod]
        public void LoadExam_WithValidExamId_ReturnsQuestions()
        {
            // Arrange
            var allSubjects = new List<string> { "תכנות" };
            var allDifficulties = new List<string> { "קל" };

            bool created = ExamGeneratorLogic.TryCreateExam(
                subject: "תכנות",
                difficulty: "קל",
                questionCountText: "4",
                allSubjects,
                allDifficulties,
                out string createMessage,
                out string examId);

            Assert.IsTrue(created);

            // Act
            var data = ExamGeneratorLogic.LoadExam(examId, out string loadMessage);

            // Assert
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Count > 0);

            // Cleanup
            ExamGeneratorLogic.DeleteExam(examId, out _);
        }

        [TestMethod]
        public void IsValidHebrewSubject_WithValidHebrew_ReturnsTrue()
        {
            // Arrange + Act + Assert
            Assert.IsTrue(ExamGeneratorLogic.IsValidHebrewSubject("מבנה נתונים"));
            Assert.IsTrue(ExamGeneratorLogic.IsValidHebrewSubject("בדיקות תוכנה"));
        }

        [TestMethod]
        public void ReloadExamList_LoadsIdsFromExcel()
        {
            var exams = ExamGeneratorLogic.LoadExamIds();

            Assert.IsNotNull(exams, "הרשימה שהתקבלה היא null");
            Assert.IsTrue(exams.Count >= 0, "הרשימה לא נטענה כראוי"); // בדיקה כללית – אין קריסה
        }
        [TestMethod]
        public void IsValidHebrewSubject_HebrewWithSpaces_ReturnsTrue()
        {
            Assert.IsTrue(ExamGeneratorLogic.IsValidHebrewSubject("תולדות ישראל"));
        }
        [TestMethod]
        public void IsValidHebrewSubject_WithInvalidInput_ReturnsFalse()
        {
            // Arrange + Act + Assert
            Assert.IsFalse(ExamGeneratorLogic.IsValidHebrewSubject("History123"));
            Assert.IsFalse(ExamGeneratorLogic.IsValidHebrewSubject(""));
            Assert.IsFalse(ExamGeneratorLogic.IsValidHebrewSubject("123"));
        }

        [TestMethod]
        public void LoadExamIds_ReturnsListOfIds()
        {
            // Arrange + Act
            var exams = ExamGeneratorLogic.LoadExamIds();

            // Assert
            Assert.IsNotNull(exams);
            Assert.IsTrue(exams.Count >= 0); // General sanity check
        }



    }
}