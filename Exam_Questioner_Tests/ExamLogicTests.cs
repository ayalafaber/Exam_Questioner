using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Exam_Questioner;

[TestClass]
public class ExamLogicTests
{
    // ✅ בדיקות תשובות פתוחות
    [TestMethod]
    public void EvaluateOpenAnswer_FullCredit_Returns1()
    {
        double score = ExamLogic.EvaluateOpenAnswer("כן, התשובה נכונה.");
        Assert.AreEqual(1.0, score);
    }

    [TestMethod]
    public void EvaluateOpenAnswer_HalfCredit_Returns0_5()
    {
        double score = ExamLogic.EvaluateOpenAnswer("חצי נכון");
        Assert.AreEqual(0.5, score);
    }

    [TestMethod]
    public void EvaluateOpenAnswer_WrongAnswer_Returns0()
    {
        double score = ExamLogic.EvaluateOpenAnswer("לא, זו טעות.");
        Assert.AreEqual(0.0, score);
    }

    [TestMethod]
    public void EvaluateOpenAnswer_NullOrEmpty_Returns0()
    {
        Assert.AreEqual(0.0, ExamLogic.EvaluateOpenAnswer(null));
        Assert.AreEqual(0.0, ExamLogic.EvaluateOpenAnswer(""));
        Assert.AreEqual(0.0, ExamLogic.EvaluateOpenAnswer("   "));
    }

    // ✅ בדיקות תשובות סגורות
    [TestMethod]
    public void EvaluateClosedAnswer_CorrectMatch_Returns1()
    {
        double score = ExamLogic.EvaluateClosedAnswer("תשובה", "תשובה");
        Assert.AreEqual(1.0, score);
    }

    [TestMethod]
    public void EvaluateClosedAnswer_TrimmedMatch_Returns1()
    {
        double score = ExamLogic.EvaluateClosedAnswer("  תשובה  ", "תשובה");
        Assert.AreEqual(1.0, score);
    }

    [TestMethod]
    public void EvaluateClosedAnswer_IncorrectMatch_Returns0()
    {
        double score = ExamLogic.EvaluateClosedAnswer("שגויה", "נכונה");
        Assert.AreEqual(0.0, score);
    }

    // ✅ בדיקות חישוב ציון
    [TestMethod]
    public void CalculateScore_PartialAndFull_ReturnsRoundedScore()
    {
        var questions = new List<Question>
        {
            new Question { Text = "1", Type = "פתוחה" },
            new Question { Text = "2", Type = "אמריקאית" },
            new Question { Text = "3", Type = "אמריקאית" }
        };
        var scores = new Dictionary<int, double>
        {
            { 0, 0.5 }, { 1, 1.0 }, { 2, 0.0 }
        };

        int result = ExamLogic.CalculateScore(questions, scores);
        Assert.AreEqual(50, result);
    }

    [TestMethod]
    public void CalculateScore_EmptyList_Returns0()
    {
        int score = ExamLogic.CalculateScore(new List<Question>(), new Dictionary<int, double>());
        Assert.AreEqual(0, score);
    }

    [TestMethod]
    public void CalculateScore_PartialAnswers_IgnoresMissing()
    {
        var questions = new List<Question>
        {
            new Question { Text = "1" },
            new Question { Text = "2" },
            new Question { Text = "3" }
        };
        var scores = new Dictionary<int, double> { { 0, 1.0 } }; // רק אחת נענתה

        int result = ExamLogic.CalculateScore(questions, scores);
        Assert.AreEqual(33, result); // (1/3)*100 ≈ 33.3 → מעוגל
    }
}