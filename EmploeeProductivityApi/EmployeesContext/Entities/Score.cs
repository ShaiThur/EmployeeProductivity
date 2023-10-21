using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesContext.Entities
{
    public class Score
    {
        public int ScoreId { get; set; }
        public  int DayScore { get; set; }
        public int WeekScore { get; set; }
        public int MonthScore { get; set; }
        public int TotalScore { get; set; }
        public int EmployeeId { get; set; }

        public override string ToString()
        //Это я для удобства
        {
            return $"id = {ScoreId}, DayScore = {DayScore}, WeekScore = {WeekScore}, MonthScore = {MonthScore}, TotalScore = {TotalScore}";
        }
    }
}
