using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Questioner
{
    public static class StudentReviewsLogic
    {
        public static double? CalculateAverageScore(List<ReviewInfo> reviews)
        {
            if (reviews == null || reviews.Count == 0) return null;

            int totalScore = 0;
            int validRatings = 0;

            foreach (var review in reviews)
            {
                if (string.IsNullOrWhiteSpace(review.Rating))
                    continue;

                if (review.Rating.Contains("מצוין"))
                {
                    totalScore += 3;
                    validRatings++;
                }
                else if (review.Rating.Contains("טוב"))
                {
                    totalScore += 2;
                    validRatings++;
                }
                else if (review.Rating.Contains("שיפור"))
                {
                    totalScore += 1;
                    validRatings++;
                }
            }

            if (validRatings == 0) return null;

            return (double)totalScore / validRatings;
        }

        public static string GetAverageText(double? avg)
        {
            if (!avg.HasValue) return "דירוג ממוצע: --";

            if (avg >= 2.5) return "דירוג ממוצע: מצוין";
            if (avg >= 1.5) return "דירוג ממוצע: טוב";
            return "דירוג ממוצע: צריך שיפור";
        }

        public static string GetAverageIcon(double? avg)
        {
            if (!avg.HasValue) return "📈";

            if (avg >= 2.5) return "⭐";
            if (avg >= 1.5) return "😊";
            return "💪";
        }

        public static string GetLatestRatingIcon(string rating)
        {
            if (string.IsNullOrWhiteSpace(rating)) return "📝";

            if (rating.Contains("מצוין")) return "⭐";
            if (rating.Contains("טוב")) return "😊";
            if (rating.Contains("שיפור")) return "💪";

            return "📝";
        }
    }
}