using System.ComponentModel.DataAnnotations;

namespace cimob.Attributes
{
    public class SameValueAttribute : ValidationAttribute
    {
        private object propertyToCompare;

        public SameValueAttribute(object propertyToCompare)
        {
            this.propertyToCompare = propertyToCompare;
        }

        public override bool IsValid(object value)
        {
            return this.propertyToCompare != value;
        }
    }
}
