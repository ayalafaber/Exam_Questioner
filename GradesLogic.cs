using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam_Questioner
{
    // מייצג רשומת ציון אחת של תלמיד
    public class GradeRecord
    {
        public string Username { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public string Level { get; set; }
        public int? Score { get; set; }
    }

    public static class GradesLogic
    {
        //ממיר רשימה גולמית של ציונים לרשימה תקנית עם פרטי משתמש ותאריך

        public static List<GradeRecord> ConvertRawScores(
            string username,
            List<(string Subject, string Level, int? Score)> rawScores)
        {
            return rawScores.Select(s => new GradeRecord
            {
                Username = username,
                Date = DateTime.Now,
                Subject = s.Subject,
                Level = s.Level,
                Score = s.Score
            }).ToList();
        }


        // מחזיר רק ציונים תקפים 

        public static List<GradeRecord> FilterValidScores(List<GradeRecord> grades)
        {
            return grades.Where(g => g.Score.HasValue).ToList();
        }


        // מחשב ממוצע לציונים תקפים. מחזיר null אם אין ציונים

        public static double? CalculateAverage(List<GradeRecord> grades)
        {
            var valid = grades.Where(g => g.Score.HasValue).ToList();
            if (!valid.Any()) return null;
            return valid.Average(g => g.Score.Value);
        }
    }
}