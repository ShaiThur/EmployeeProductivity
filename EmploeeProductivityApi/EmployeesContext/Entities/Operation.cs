using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProductivity.Application.Entities
{
    public class Operation
    {
        public int Id { get; set; }
        public int DirectorId { get; set; }
        public string OperationName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DayOfWeek Validity { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsAccessed { get; set; }
    }
}
