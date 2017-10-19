using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Models
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required, MaxLength(255), MinLength(2)]
        //[StringLength(255, MinimumLength = 2)]
        public string Name { get; set; }
        public string Description { get; set; }

        //Relationship
        public IList<Employee> Employees { get; set; }
    }
}