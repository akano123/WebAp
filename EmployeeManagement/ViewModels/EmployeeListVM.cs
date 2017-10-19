using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagement.ViewModels
{
    public partial class EmployeeListVM
    {
        public int EmployeeId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Birthdate")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Is Married")]
        public bool IsMarried { get; set; }

        [MaxLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public string Skills { get; set; }

        [Display(Name = "Department")]
        public string DepartmentName { get; set; }
       

    }

    public partial class EmployeeListVM
    { 
        [Display(Name = "Full Name")]
        //[DisplayName("Full Name")]
        public string DisplayFullName
        {
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }
        }

        [Display(Name = "Status")]
        public string DisplayMarriedStatus
        {
            get
            {
                return this.IsMarried ? "Yes" : "No";
            }
        }

        [Display(Name = "Skills"), DisplayFormat(NullDisplayText = "No Skills")]
        public string DisplaySkills
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Skills))
                {
                    //(DB)1,2,3,4 => (Split) [] ={"1","2","3","4"} => [] =>(Select) {"C#","java", "php", "jquery"} => C#,java,php,jquery
                    return string.Join(",", Skills.Split(',').Select(x => (Skill)int.Parse(x)));
                }
                return null;
            }
        }
    }
}