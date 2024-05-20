using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Department
    {
        [Key]
        [DisplayName("Number")]
        public int Number { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [DisplayName("Manager")]
        [ForeignKey("Employee")]
        public int? ManagerSSN { get; set; }
        public virtual Employee? Manager { get; set; }

        [ValidateDate("1990-01-01", "now",
        ErrorMessage = "Manager Start Date should be between {0} and {1}.")]
        [DisplayName("Manager Start Date")]
        public DateTime? MGRStartDate { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
        public virtual ICollection<DepartmentLocation> Locations { get; set; } = new HashSet<DepartmentLocation>();
    }

}
