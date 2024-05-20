using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class DepartmentLocation
    {
        [ForeignKey("Department")]
        [DisplayName("Department")]
        public int? DNumber { get; set; }
        public virtual Department? Department { get; set; }
        [RegularExpression("^[^0-9]*$", ErrorMessage = "Location shouldn't contain numbers")]

        public string Location { get; set; }

    }
}
