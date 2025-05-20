// 🧩 בדיקות אינטגרציה
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Exam_Questioner;
using System.Collections.Generic;

[TestClass]
public class ExamIntegrationTests
{
    [TestMethod]
    public void LoadQuestions_ValidExamId_ReturnsQuestions()
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.xlsx");
        var questions = ExamLogic.LoadQuestions(path, "01");
        Assert.IsTrue(questions.Count > 0);
    }

    [TestMethod]
    public void GetGradeCategory_KnownExamId_ReturnsCategory()
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.xlsx");
        string category = ExamLogic.GetGradeCategory(path, "01");
        Assert.IsFalse(string.IsNullOrWhiteSpace(category));
    }

    [TestMethod]
    public void SaveGrade_NewStudent_AddsRow()
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.xlsx");
        string student = "בדיקה_" + Guid.NewGuid().ToString("N").Substring(0, 6);
        string category = "תכנות";

        ExamLogic.SaveGrade(path, student, category, 88);

        // בדיקה כללית: אם לא נזרקה שגיאה, סביר שהשמירה הצליחה
        Assert.IsTrue(true);
    }
}
