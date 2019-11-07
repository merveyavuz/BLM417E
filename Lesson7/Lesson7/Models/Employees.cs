using System;
using System.Collections.Generic;

namespace Lesson7.Models
{
    public partial class Employees
    {
        public int PersonelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Salary { get; set; }
        public int? JobId { get; set; }
        public int? DeparmentId { get; set; }

        public Departments Deparment { get; set; }
        public Jobs Job { get; set; }
    }
}
