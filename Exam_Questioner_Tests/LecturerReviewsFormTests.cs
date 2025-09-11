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
        public class LecturerReviewsLogicTests
        {
            [TestMethod]
            public void IsReviewValid_ShouldReturnFalse_WhenMessageIsEmpty()
            {
                var result = LecturerReviewsLogic.IsReviewValid("", true, false, false);
                Assert.IsFalse(result, "אסור לאשר ביקורת בלי תוכן");
            }

            [TestMethod]
            public void IsReviewValid_ShouldReturnFalse_WhenNoRatingSelected()
            {
                var result = LecturerReviewsLogic.IsReviewValid("הביקורת טובה", false, false, false);
                Assert.IsFalse(result, "אסור לאשר ביקורת בלי דירוג");
            }

            [TestMethod]
            public void IsReviewValid_ShouldReturnTrue_WhenValidMessageAndRating()
            {
                var result = LecturerReviewsLogic.IsReviewValid("כל הכבוד", false, true, false);
                Assert.IsTrue(result, "ביקורת תקפה צריכה לעבור");
            }

            [TestMethod]
            public void GetRating_ShouldReturnStar_WhenStarChecked()
            {
                var rating = LecturerReviewsLogic.GetRating(true, false, false);
                Assert.AreEqual("⭐ מצוין", rating);
            }

            [TestMethod]
            public void GetRating_ShouldReturnHappy_WhenHappyChecked()
            {
                var rating = LecturerReviewsLogic.GetRating(false, true, false);
                Assert.AreEqual("😊 טוב", rating);
            }

            [TestMethod]
            public void GetRating_ShouldReturnSad_WhenSadChecked()
            {
                var rating = LecturerReviewsLogic.GetRating(false, false, true);
                Assert.AreEqual("😞 צריך שיפור", rating);
            }

            [TestMethod]
            public void GetRating_ShouldReturnEmpty_WhenNoneChecked()
            {
                var rating = LecturerReviewsLogic.GetRating(false, false, false);
                Assert.AreEqual(string.Empty, rating);
            }
        }
    }
