using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Project
    {
        [Key]
        public int Number { get; set; }

        [RegularExpression("^[^0-9]*$", ErrorMessage = "Name shouldn't contain numbers")]
        public string Name { get; set; }

        [RegularExpression("^[^0-9]*$", ErrorMessage = "Location shouldn't contain numbers")]
        public string Location { get; set; }

        [ForeignKey("Department")]
        public int? DNum { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<EmployeeProject> Employees { get; set; } = new HashSet<EmployeeProject>();

    }
}
