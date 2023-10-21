using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProductivity.Application.Entities
{
    public class Director : User
    {
        public string Role { get; } = "Director";
    }
}
