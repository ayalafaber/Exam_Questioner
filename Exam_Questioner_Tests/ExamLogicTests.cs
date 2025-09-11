using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Exam_Questioner;

namespace Exam_Questioner_Tests
{
    [TestClass]
    public class ExamLogicTests
    {
        // EvaluateOpenAnswer - תשובה פתוחה

        [TestMethod]
        public void EvaluateOpenAnswer_FullCredit_Returns1()
        {
            // Arrange
            string feedback = "כן, זו תשובה נכונה.";

            // Act
            double score = ExamLogic.EvaluateOpenAnswer(feedback);

            // Assert
            Assert.AreEqual(1.0, score);
        }

        [TestMethod]
        public void EvaluateOpenAnswer_HalfCredit_Returns0_5()
        {
            // Arrange
            string feedback = "חצי נכון, יש עוד להשלים.";

            // Act
            double score = ExamLogic.EvaluateOpenAnswer(feedback);

            // Assert
            Assert.AreEqual(0.5, score);
        }

        [TestMethod]
        public void EvaluateOpenAnswer_WrongOrEmpty_Returns0()
        {
            // Arrange
            string feedback1 = "לא, זו טעות.";
            string feedback2 = "";
            string feedback3 = null;

            // Act & Assert
            Assert.AreEqual(0.0, ExamLogic.EvaluateOpenAnswer(feedback1));
            Assert.AreEqual(0.0, ExamLogic.EvaluateOpenAnswer(feedback2));
            Assert.AreEqual(0.0, ExamLogic.EvaluateOpenAnswer(feedback3));
        }

        // EvaluateClosedAnswer - תשובה סגורה

        [TestMethod]
        public void EvaluateClosedAnswer_CorrectAnswer_Returns1()
        {
            // Arrange
            string userAnswer = "תשובה נכונה";
            string correctAnswer = "תשובה נכונה";

            // Act
            double score = ExamLogic.EvaluateClosedAnswer(userAnswer, correctAnswer);

            // Assert
            Assert.AreEqual(1.0, score);
        }

        [TestMethod]
        public void EvaluateClosedAnswer_IncorrectAnswer_Returns0()
        {
            // Arrange
            string userAnswer = "שגוי";
            string correctAnswer = "נכון";

            // Act
            double score = ExamLogic.EvaluateClosedAnswer(userAnswer, correctAnswer);

            // Assert
            Assert.AreEqual(0.0, score);
        }

        // CalculateScore - חישוב ציון

        [TestMethod]
        public void CalculateScore_WithPartialAndFullAnswers_ReturnsCorrectScore()
        {
            // Arrange
            var questions = new List<Question>
            {
                new Question { Text = "שאלה 1", Type = "אמריקאית" },
                new Question { Text = "שאלה 2", Type = "פתוחה" },
                new Question { Text = "שאלה 3", Type = "אמריקאית" }
            };

            var scores = new Dictionary<int, double>
            {
                { 0, 1.0 },
                { 1, 0.5 },
                { 2, 0.0 }
            };

            // Act
            int finalScore = ExamLogic.CalculateScore(questions, scores);

            // Assert
            Assert.AreEqual(50, finalScore); // (1 + 0.5) / 3 * 100 = 50
        }

        [TestMethod]
        public void CalculateScore_WithEmptyQuestions_ReturnsZero()
        {
            // Arrange
            var questions = new List<Question>();
            var scores = new Dictionary<int, double>();

            // Act
            int finalScore = ExamLogic.CalculateScore(questions, scores);

            // Assert
            Assert.AreEqual(0, finalScore);
        }

        // LoadQuestions - טעינת שאלות (בדיקה מומלצת להוסיף!)

        [TestMethod]
        public void LoadQuestions_WithValidExamId_ReturnsQuestionsList()
        {
            // Arrange
            string filePath = System.IO.Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop),
                "database.xlsx");

            // במקרה שהקובץ לא קיים - נדלג
            if (!System.IO.File.Exists(filePath))
            {
                Assert.Inconclusive("הקובץ database.xlsx לא נמצא.");
                return;
            }

            var examIds = ExamGeneratorLogic.LoadExamIds();
            if (examIds.Count == 0)
            {
                Assert.Inconclusive("אין מבחנים קיימים במערכת.");
                return;
            }

            string examId = examIds[0]; // נבחר את הראשון

            // Act
            List<Question> questions = ExamLogic.LoadQuestions(filePath, examId);

            // Assert
            Assert.IsNotNull(questions);
            Assert.IsTrue(questions.Count > 0, "לא נטענו שאלות למבחן.");
        }
    }
}