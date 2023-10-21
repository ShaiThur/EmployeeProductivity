using EmployeesContext.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProductivity.Infrustructure.RatingCreators
{
    public class ScoreRequest
    {
        Score[] scores;
        public ScoreRequest(Score[] scores)
        {
            this.scores = scores;
        }

        #region Если надо найти только некоторых работников из всего массива

        public enum TimeInterval
        {
            Day,
            Week,
            Month
        }
        public int GetScoreOfEmployees(int[] EmployeeId, TimeInterval interval)
        //Это один метод для всего но хз, мб он долгий, поэтому сделал и отдельные.
        {
            int day = 0, month = 0, week = 0;
            switch (interval)
            {
                case TimeInterval.Day:
                    {
                        day = 1;
                        break;
                    }
                case TimeInterval.Week:
                    {
                        week = 1;
                        break;
                    }
                case TimeInterval.Month:
                    {
                        month = 1;
                        break;
                    }
            }

            int result = 0;
            foreach (Score score in scores)
            {
                if (EmployeeId.Contains(score.EmployeeId)) result += score.DayScore * day + score.WeekScore * week + score.MonthScore * month;
            }
            return result;
        }

        public int GetScoreOfEmployeesDay(int[] EmployeeId)
        {
            int result = 0;
            foreach (Score score in scores)
            {
                if (EmployeeId.Contains(score.EmployeeId)) result += score.DayScore;
            }
            return result;
        }
        public int GetScoreOfEmployeesWeek(int[] EmployeeId)
        {
            int result = 0;
            foreach (Score score in scores)
            {
                if (EmployeeId.Contains(score.EmployeeId)) result += score.WeekScore;
            }
            return result;
        }
        public int GetScoreOfEmployeesMonth(int[] EmployeeId)
        {
            int result = 0;
            foreach (Score score in scores)
            {
                if (EmployeeId.Contains(score.EmployeeId)) result += score.MonthScore;
            }
            return result;
        }
        public int GetScoreOfEmployeesTotal(int[] EmployeeId)
        {
            int result = 0;
            foreach (Score score in scores)
            {
                if (EmployeeId.Contains(score.EmployeeId)) result += score.TotalScore;
            }
            return result;
        }
        #endregion
        #region Если надо найти среди всего массива
        public int GetScorePerDay()
        {
            int result = 0;
            foreach (Score score in scores)
            {
                result += score.DayScore;
            }
            return result;
        }
        public int GetScorePerWeek()
        {
            int result = 0;
            foreach (Score score in scores)
            {
                result += score.WeekScore;
            }
            return result;
        }
        public int GetScorePerMonth()
        {
            int result = 0;
            foreach (Score score in scores)
            {
                result += score.MonthScore;
            }
            return result;
        }
        public int GetScorePerTotal()
        {
            int result = 0;
            foreach (Score score in scores)
            {
                result += score.TotalScore;
            }
            return result;
        }
        #endregion

        public float GetEmployeesRating(int EmployeeId)
        //Рэйтинг продуктивности рабочего
        {
            int minRating = 1;
            int maxRating = 5;
            int maxScore = scores[0].TotalScore;
            int minScore = scores[0].TotalScore;
            for (int i = 1; i < scores.Length; i++)
            {
                if (scores[i].TotalScore > maxScore) maxScore = scores[i].TotalScore;
                if (scores[i].TotalScore < minScore) minScore = scores[i].TotalScore;
            }

            float result;
            foreach (Score score in scores)
            {
                if (EmployeeId == score.EmployeeId)
                {
                    result = minRating + (float)(score.TotalScore - minScore) / (maxScore - minScore) * (maxRating - minRating);
                    return result;
                }
            }
            throw new Exception("Не нашёл работника");
        }

        public int SeedScore()
        {
            for (int i = 0; i < scores.Length; i++)
            {
                scores[i] = new Score();
                scores[i].ScoreId = i;
                scores[i].EmployeeId = i;
                scores[i].TotalScore = new Random().Next(100);
                scores[i].DayScore = Convert.ToInt32(scores[i].TotalScore * 0.15);
                scores[i].WeekScore = Convert.ToInt32(scores[i].TotalScore * 0.35);
                scores[i].MonthScore = Convert.ToInt32(scores[i].TotalScore * 0.50);
            }
            ScoreRequest sr = new ScoreRequest(scores);
            return sr.GetScorePerMonth();
        }
    }
}
