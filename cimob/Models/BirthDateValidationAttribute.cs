using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimob.Models
{
    public class BirthDateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dataLimite = (DateTime.Now.AddYears(-16));

            if (((DateTime)value).Date > dataLimite.Date)
                return false;
            else
                return true;
        }
    }
}
