using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Questioner
{
    public static class LecturerReviewsLogic
    {
        public static bool IsReviewValid(string message, bool isStarChecked, bool isHappyChecked, bool isSadChecked)
        {
            return !string.IsNullOrWhiteSpace(message) && (isStarChecked || isHappyChecked || isSadChecked);
        }

        public static string GetRating(bool isStarChecked, bool isHappyChecked, bool isSadChecked)
        {
            if (isStarChecked) return "⭐ מצוין";
            if (isHappyChecked) return "😊 טוב";
            if (isSadChecked) return "😞 צריך שיפור";
            return string.Empty;
        }
    }
}