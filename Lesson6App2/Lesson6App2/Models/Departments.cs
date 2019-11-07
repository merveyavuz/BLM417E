using System;
using System.Collections.Generic;

namespace Lesson6App2.Models
{
    public partial class Departments
    {
        public Departments()
        {
            Employees = new HashSet<Employees>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentTitle { get; set; }

        public ICollection<Employees> Employees { get; set; }
    }
}
