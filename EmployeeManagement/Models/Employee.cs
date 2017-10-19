using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Models
{
    [Table("Employees")]
    public partial class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        
        [Remote("IsIdCardAvailable", "Employees", AdditionalFields ="OriginIdCard" , ErrorMessage ="The IdCard is already in use.")]
        public string IdCard { get; set; }

        [Required, StringLength(255, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required, StringLength(255, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Birthdate")]
        public DateTime DateOfBirth { get; set; }

        public bool IsMarried { get; set; }

        [MaxLength(255)]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "The email is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string PhoneNumber { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [Range(1000,10000)]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public string Skills { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        //Relationship
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }

    public partial class Employee
    {
        [NotMapped]
        [Display(Name = "Full Name")]
        //[DisplayName("Full Name")]
        public string DisplayFullName
        {
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }
        }

        [NotMapped]
        [Display(Name = "Status")]
        public string DisplayMarriedStatus
        {
            get
            {
                return this.IsMarried ? "Yes" : "No";
            }
        }

        [NotMapped]
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

        [NotMapped]
        public IEnumerable<int> EditSkills {
            get
            {
                if (!string.IsNullOrEmpty(this.Skills))
                {
                    return this.Skills.Split(',').Select(x => int.Parse(x));
                }
                return null;
            }
            set
            {
                if(value != null)
                {
                    this.Skills = string.Join(",", value);
                }
            }
        }

        [NotMapped]
        [System.ComponentModel.DataAnnotations.Compare("ExpiryDate")]
        public DateTime? ConfirmExpiryDate { get; set; }
    }
}