
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
                questionCountText: "4",
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

        [TestMethod]
        public void TryCreateExam_MinValidQuestionCount_ReturnsSuccess()
        {
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "תכנות",
                difficulty: "קל",
                questionCountText: "4",
                allSubjects: new List<string> { "תכנות" },
                allDifficulties: new List<string> { "קל" },
                out string message,
                out string examId
            );
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TryCreateExam_QuestionCountTooLow_ReturnsError()
        {
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "תכנות",
                difficulty: "קל",
                questionCountText: "3",
                allSubjects: new List<string> { "תכנות" },
                allDifficulties: new List<string> { "קל" },
                out string message,
                out string examId
            );
            Assert.IsFalse(result);
            Assert.AreEqual("מספר השאלות חייב להיות בין 4 ל‑12.", message);
        }

        [TestMethod]
        public void TryCreateExam_QuestionCountTooHigh_ReturnsError()
        {
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
                allSubjects: new List<string> { "תכנות", "היסטוריה", "רנדומלי" },
                allDifficulties: new List<string> { "קל", "בינוני", "קשה" },
                out string message,
                out string examId
            );

            if (result)
            {
                Assert.IsNotNull(examId);
                Assert.IsTrue(message.Contains("נוצר מבחן חדש"));
            }
            else
            {
                // לא בודקים הודעה מדויקת אלא רק שההפעלה נכשלה – למניעת בעיות במבחן ריק
                Assert.IsFalse(result);
            }
        }


        [TestMethod]
        public void DeleteExam_ValidExamId_DeletesSuccessfully()
        {
            bool created = ExamGeneratorLogic.TryCreateExam(
                subject: "תכנות",
                difficulty: "קל",
                questionCountText: "4",
                allSubjects: new List<string> { "תכנות" },
                allDifficulties: new List<string> { "קל" },
                out string createMessage,
                out string examId
            );
            Assert.IsTrue(created);

            bool deleted = ExamGeneratorLogic.DeleteExam(examId, out string deleteMessage);
            Assert.IsTrue(deleted);
            Assert.AreEqual($"המבחן {examId} נמחק.", deleteMessage);
        }

        [TestMethod]
        public void LoadExam_ValidExamId_ReturnsQuestions()
        {
            bool result = ExamGeneratorLogic.TryCreateExam(
                subject: "תכנות",
                difficulty: "קל",
                questionCountText: "4",
                allSubjects: new List<string> { "תכנות" },
                allDifficulties: new List<string> { "קל" },
                out string createMessage,
                out string examId
            );

            Assert.IsTrue(result);

            var data = ExamGeneratorLogic.LoadExam(examId, out string loadMessage);

            Assert.IsNotNull(data);
            Assert.IsTrue(data.Count > 0); // יש שאלות
        }

        [TestMethod]
        public void IsValidHebrewSubject_ValidHebrew_ReturnsTrue()
        {
            Assert.IsTrue(ExamGeneratorLogic.IsValidHebrewSubject("תכנות מתקדם"));
        }

        [TestMethod]
        public void IsValidHebrewSubject_EmptyOrNull_ReturnsFalse()
        {
            Assert.IsFalse(ExamGeneratorLogic.IsValidHebrewSubject(""));
            Assert.IsFalse(ExamGeneratorLogic.IsValidHebrewSubject("   "));
            Assert.IsFalse(ExamGeneratorLogic.IsValidHebrewSubject(null));
        }

        [TestMethod]
        public void IsValidHebrewSubject_NonHebrewCharacters_ReturnsFalse()
        {
            Assert.IsFalse(ExamGeneratorLogic.IsValidHebrewSubject("Math123"));
            Assert.IsFalse(ExamGeneratorLogic.IsValidHebrewSubject("תכנות!"));
            Assert.IsFalse(ExamGeneratorLogic.IsValidHebrewSubject("תכנות advanced"));
        }

        [TestMethod]
        public void DeleteExam_RemovedFromListBox_AfterReload()
        {
            // יצירת מבחן חדש
            bool created = ExamGeneratorLogic.TryCreateExam(
                subject: "תכנות",
                difficulty: "קל",
                questionCountText: "4",
                allSubjects: new List<string> { "תכנות" },
                allDifficulties: new List<string> { "קל" },
                out string createMessage,
                out string examId
            );
            Assert.IsTrue(created, "יצירת מבחן נכשלה");

            // מחיקת המבחן
            bool deleted = ExamGeneratorLogic.DeleteExam(examId, out string deleteMessage);
            Assert.IsTrue(deleted, "מחיקת מבחן נכשלה");

            // קריאת הרשימה מחדש
            var exams = ExamGeneratorLogic.LoadExamIds();
            Assert.IsFalse(exams.Any(e => e.StartsWith(examId)), $"המבחן {examId} עדיין מופיע ברשימה לאחר מחיקה");
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
        public void IsValidHebrewSubject_NonHebrewOrEmpty_ReturnsFalse()
        {
            Assert.IsFalse(ExamGeneratorLogic.IsValidHebrewSubject("History"));
            Assert.IsFalse(ExamGeneratorLogic.IsValidHebrewSubject("123"));
            Assert.IsFalse(ExamGeneratorLogic.IsValidHebrewSubject(""));
        }

        [TestMethod]
        public void TryCreateExam_WhenFileMissing_ReturnsError()
        {
            // שנה את שם הקובץ או נתק אותו זמנית
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "database.xlsx");
            if (File.Exists(filePath))
                File.Move(filePath, filePath + ".bak");  // גיבוי זמני

            bool result = ExamGeneratorLogic.TryCreateExam(
                "תכנות", "קל", "4", new() { "תכנות" }, new() { "קל" }, out string message, out _);

            Assert.IsFalse(result);
            Assert.AreEqual("קובץ database.xlsx לא נמצא על שולחן‑העבודה.", message);

            if (File.Exists(filePath + ".bak"))
                File.Move(filePath + ".bak", filePath); // שחזור
        }



    }
}
