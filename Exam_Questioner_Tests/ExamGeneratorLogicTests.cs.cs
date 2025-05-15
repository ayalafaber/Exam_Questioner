using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using Exam_Questioner;

namespace Exam_Questioner_Tests
{
    [TestClass]
    public class ExamGeneratorLogicTests
    {
        [TestMethod]
        public void TryCreateExam_EmptySubject_ReturnsError()
        {
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "",
                difficulty: "קל",
                questionCountText: "6",
                allSubjects: new List<string> { "תכנות", "היסטוריה" },
                allDifficulties: new List<string> { "קל", "בינוני", "קשה" },
                out string message,
                out string examId
            );

            Assert.IsFalse(result);
            Assert.AreEqual("בחר נושא.", message);
        }

        [TestMethod]
        public void TryCreateExam_EmptyQuestionCount_ReturnsError()
        {
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "תכנות",
                difficulty: "קל",
                questionCountText: "",
                allSubjects: new List<string> { "תכנות", "היסטוריה" },
                allDifficulties: new List<string> { "קל", "בינוני", "קשה" },
                out string message,
                out string examId
            );

            Assert.IsFalse(result);
            Assert.AreEqual("מספר השאלות חייב להיות בין 4 ל‑12.", message);
        }

        [TestMethod]
        public void TryCreateExam_NonNumericQuestionCount_ReturnsError()
        {
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
                allSubjects: new List<string> { "תכנות", "היסטוריה" },
                allDifficulties: new List<string> { "קל", "בינוני", "קשה" },
                out string message,
                out string examId
            );

            Assert.IsFalse(result);
            Assert.AreEqual("מספר השאלות חייב להיות בין 4 ל‑12.", message);
        }

        [TestMethod]
        public void TryCreateExam_TooManyQuestions_ReturnsError()
        {
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "היסטוריה",
                difficulty: "בינוני",
                questionCountText: "20",
                allSubjects: new List<string> { "היסטוריה" },
                allDifficulties: new List<string> { "בינוני" },
                out string message,
                out string examId
            );

            Assert.IsFalse(result);
            Assert.AreEqual("מספר השאלות חייב להיות בין 4 ל‑12.", message);
        }

        [TestMethod]
        public void TryCreateExam_EmptyDifficulty_ReturnsError()
        {
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "תכנות",
                difficulty: "",
                questionCountText: "5",
                allSubjects: new List<string> { "תכנות", "היסטוריה" },
                allDifficulties: new List<string> { "קל", "בינוני", "קשה" },
                out string message,
                out string examId
            );

            Assert.IsFalse(result);
            Assert.AreEqual("בחר רמת קושי.", message);
        }

        [TestMethod]
        public void TryCreateExam_SubjectNotInQuestions_ReturnsError()
        {
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "פיזיקה",
                difficulty: "קשה",
                questionCountText: "5",
                allSubjects: new List<string> { "פיזיקה", "תכנות", "היסטוריה" },
                allDifficulties: new List<string> { "קל", "בינוני", "קשה" },
                out string message,
                out string examId
            );

            Assert.IsFalse(result);
            Assert.AreEqual("אין מספיק שאלות במאגר לתנאים המבוקשים.", message);
        }

        [TestMethod]
        public void TryCreateExam_ValidInput_CreatesExam()
        {
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "תכנות",
                difficulty: "בינוני",
                questionCountText: "1",
                allSubjects: new List<string> { "תכנות", "היסטוריה", "מתמטיקה" },
                allDifficulties: new List<string> { "קל", "בינוני", "קשה" },
                out string message,
                out string examId
            );

            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.xlsx")))
            {
                Assert.IsTrue(result);
                Assert.IsNotNull(examId);
                Assert.IsTrue(message.Contains("נוצר מבחן חדש"));
            }
            else
            {
                Assert.IsFalse(result);
                Assert.AreEqual("קובץ database.xlsx לא נמצא על שולחן‑העבודה.", message);
            }
        }
    }
}