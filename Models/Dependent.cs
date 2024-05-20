using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Dependent
    {
        [ForeignKey("Employee")]
        [DisplayName("Employee")]
        public int? ESSN { get; set; }
        public virtual Employee? Employee { get; set; }
        public string Name { get; set; }
        [RegularExpression("^[MFmf]{1}$", ErrorMessage = "Sex should be M or F only !")]
        public char Sex { get; set; }
        [ValidateDate("2004-01-01", "now",
       ErrorMessage = "Birth date should be between {0} and {1}.")]
        [DisplayName("Birth Date")]
        public DateTime BDate { get; set; }
        [RegularExpression("^[^0-9]*$", ErrorMessage = "Relationship shouldn't contain numbers")]
        public string Relationship { get; set; }
    }
}
