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
        public int EmployeeId { get; set; }
        public string OperationName { get; set; } = null!;
        public string? Description { get; set; }
        public Difficult Difficult { get; set; }
        public DateTime CreationDate { get; set; }

        private int expectationTime = 100;
        public DateTime Deadline { get; set; }
        public bool IsAccessed { get => CreationDate.ToUniversalTime() < Deadline.ToUniversalTime(); }
    }
    public enum Difficult
    {
        Easy = 10,
        MiddleEasy = 20,
        Middle = 30,
        HardMiddle = 40,
        Hard = 50
    }
}
