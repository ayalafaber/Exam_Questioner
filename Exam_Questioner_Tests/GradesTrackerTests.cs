using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Exam_Questioner;

namespace Exam_Questioner_Tests
{
    [TestClass]
    public class GradesTrackerTests
    {
        [TestMethod]
        //בדיקה על חישוב ממוצע ציונים
        public void CalculateAverage_ReturnsCorrectAverage()
        {
            var grades = new List<GradeRecord>
            {
                new GradeRecord { Score = 80 },
                new GradeRecord { Score = 90 },
                new GradeRecord { Score = 100 }
            };

            var result = GradesLogic.CalculateAverage(grades);
            Assert.AreEqual(90.0, result);
        }

        [TestMethod]
        //האם הפונקציה מחזירה NULL כאשר אין ציונים ברשימה
        public void CalculateAverage_ReturnsNullWhenNoScores()
        {
            var grades = new List<GradeRecord>(); // Empty list
            var result = GradesLogic.CalculateAverage(grades);
            Assert.IsNull(result);
        }

        [TestMethod]
        //האם הפונקציה מתעלמת מציונים שהם NULL
        public void CalculateAverage_IgnoresNullScores()
        {
            var grades = new List<GradeRecord>
            {
                new GradeRecord { Score = null },
                new GradeRecord { Score = 80 },
                new GradeRecord { Score = null },
                new GradeRecord { Score = 100 }
            };

            var result = GradesLogic.CalculateAverage(grades);
            Assert.AreEqual(90.0, result);
        }

        [TestMethod]
        public void FilterValidScores_ReturnsOnlyValidScores()
        //האם הפונקציה מסננת רק ציונים תקפים
        {
            var grades = new List<GradeRecord>
            {
                new GradeRecord { Score = null },
                new GradeRecord { Score = 75 },
                new GradeRecord { Score = 88 },
                new GradeRecord { Score = null }
            };

            var result = GradesLogic.FilterValidScores(grades);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(g => g.Score.HasValue));
        }

        [TestMethod]
        //האם שם המשתמש שומר על הערך שהוזן בכל רשימה
        public void Username_IsPreservedInRecords()
        {
            var grades = new List<GradeRecord>
            {
                new GradeRecord { Username = "Dana", Score = 100 },
                new GradeRecord { Username = "Dana", Score = 95 }
            };

            Assert.IsTrue(grades.All(g => g.Username == "Dana"));
        }
    }
}