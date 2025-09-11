using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Exam_Questioner;
using ClosedXML.Excel;
using System;

namespace Exam_Questioner_Tests
{
    [TestClass]
    public class StudentReviewsLogicTests
    {
        [TestMethod]
        public void CalculateAverageScore_ShouldReturnCorrectAverage()
        {
            var reviews = new List<ReviewInfo>
            {
                new ReviewInfo { Rating = "⭐ מצוין" },
                new ReviewInfo { Rating = "😊 טוב" },
                new ReviewInfo { Rating = "💪 צריך שיפור" },
            };

            var average = StudentReviewsLogic.CalculateAverageScore(reviews);
            Assert.AreEqual(2.0, average);
        }

        [TestMethod]
        public void CalculateAverageScore_ShouldReturnNull_WhenNoValidRatings()
        {
            var reviews = new List<ReviewInfo>
            {
                new ReviewInfo { Rating = null },
                new ReviewInfo { Rating = "" }
            };

            var average = StudentReviewsLogic.CalculateAverageScore(reviews);
            Assert.IsNull(average);
        }

        [TestMethod]
        public void GetAverageText_ShouldReturnCorrectText()
        {
            Assert.AreEqual("דירוג ממוצע: מצוין", StudentReviewsLogic.GetAverageText(2.7));
            Assert.AreEqual("דירוג ממוצע: טוב", StudentReviewsLogic.GetAverageText(1.7));
            Assert.AreEqual("דירוג ממוצע: צריך שיפור", StudentReviewsLogic.GetAverageText(1.2));
            Assert.AreEqual("דירוג ממוצע: --", StudentReviewsLogic.GetAverageText(null));
        }

        [TestMethod]
        public void GetAverageIcon_ShouldReturnCorrectEmoji()
        {
            Assert.AreEqual("⭐", StudentReviewsLogic.GetAverageIcon(3));
            Assert.AreEqual("😊", StudentReviewsLogic.GetAverageIcon(2));
            Assert.AreEqual("💪", StudentReviewsLogic.GetAverageIcon(1));
            Assert.AreEqual("📈", StudentReviewsLogic.GetAverageIcon(null));
        }

        [TestMethod]
        public void GetLatestRatingIcon_ShouldReturnCorrectIcon()
        {
            Assert.AreEqual("⭐", StudentReviewsLogic.GetLatestRatingIcon("⭐ מצוין"));
            Assert.AreEqual("😊", StudentReviewsLogic.GetLatestRatingIcon("😊 טוב"));
            Assert.AreEqual("💪", StudentReviewsLogic.GetLatestRatingIcon("💪 צריך שיפור"));
            Assert.AreEqual("📝", StudentReviewsLogic.GetLatestRatingIcon(null));
            Assert.AreEqual("📝", StudentReviewsLogic.GetLatestRatingIcon("לא מזוהה"));
        }
    }
}