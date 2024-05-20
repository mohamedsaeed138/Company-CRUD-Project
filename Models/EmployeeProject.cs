using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class EmployeeProject
    {
        [ForeignKey("Employee")]
        [DisplayName("Employee")]
        public int? ESSN { get; set; }
        public virtual Employee? Employee { get; set; }

        [ForeignKey("Project")]
        public int? PNo { get; set; }
        public virtual Project? Project { get; set; }

        [Range(6, 12)]
        [DisplayName("Working Hours")]
        public int Hours { get; set; }
    }
}
