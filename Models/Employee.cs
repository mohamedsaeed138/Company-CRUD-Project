using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SSN { get; set; }
        [StringLength(20, MinimumLength = 3,
        ErrorMessage = "First Name length should be between 3,20 characters")]
        [DisplayName("First Name")]
        public string FName { get; set; }

        [StringLength(20, MinimumLength = 3,
       ErrorMessage = "Middle Name length should be between 3,20 characters")]
        [DisplayName("Middle Name")]
        public string MName { get; set; }

        [StringLength(20, MinimumLength = 3,
       ErrorMessage = "Last Name length should be between 3,20 characters")]
        [DisplayName("Last Name")]
        public string LName { get; set; }

        [DisplayName("Birth Date")]
        public DateTime? BDate { get; set; }
        [RegularExpression("^[^0-9]*$", ErrorMessage = "Address shouldn't contain numbers")]
        public string? Address { get; set; }

        [Range(10000, 25000, ErrorMessage = "Salary should between 10000 and 25000")]
        public double Salary { get; set; }

        [RegularExpression("^[MFmf]{1}$", ErrorMessage = "Sex should be M or F only !")]
        public char Sex { get; set; }

        [ForeignKey("Department")]
        [DisplayName("Department")]
        public int? DNo { get; set; }
        public virtual Department? Department { get; set; }

        [ForeignKey("Employee")]
        [DisplayName("Supervisor")]
        public int? SupervisorSSN { get; set; }
        public virtual Employee? Supervisor { get; set; }

        public virtual ICollection<EmployeeProject> Projects { get; set; } = new HashSet<EmployeeProject>();
    }
}
