using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesContext.Entities
{
    internal class EmployeesRating
    {
        public int EmployeeId { get; set; }
        public double ResultByDay { get; set; }
        public double ResultByMonth { get; set; }
        public double ResultByAllPeriod { get; set;}
    }
}
