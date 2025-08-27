using System.ComponentModel.DataAnnotations;

namespace AluGo.ModelViews.Utils
{   
    public class EnumValidation : ValidationAttribute
    {
        public char NullValue { get; set; } = '0';

        public override bool IsValid(object value)
        {
            if (!value.GetType().IsEnum)
                return false;

            var nullEnum = Enum.ToObject(value.GetType(), NullValue);

            return nullEnum.ToString() != value.ToString();
        }
    }
}
