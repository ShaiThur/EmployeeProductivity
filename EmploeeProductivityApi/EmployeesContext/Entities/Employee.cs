using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProductivity.Application.Entities
{
    public class Employee : User
    {
        public int DirectorId { get; set; }
        public string Role { get; } = "Employee";
        public double Score { get; set; }
    }
}
