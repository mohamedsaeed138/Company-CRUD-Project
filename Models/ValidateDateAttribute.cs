using System.ComponentModel.DataAnnotations;

namespace Company.Models
{
    public class ValidateDate : ValidationAttribute
    {
        private readonly DateTime _minValue;
        private readonly DateTime _maxValue;

        public ValidateDate(string minimmum, string maximmum)
        {
            _minValue = DateTime.Parse(minimmum);
            _maxValue = maximmum.ToLower() == "now" ? DateTime.Now : DateTime.Parse(maximmum);
        }
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            DateTime val = (DateTime)value;
            return val >= _minValue && val <= _maxValue;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, _minValue.ToString("dd-MM-yyyy"), _maxValue.ToString("dd-MM-yyyy"));
        }
    }
}
