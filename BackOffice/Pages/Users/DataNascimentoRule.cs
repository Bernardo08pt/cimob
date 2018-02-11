using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BackOffice.Pages.Users
{
    class DataNascimentoRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                DateTime input = Convert.ToDateTime(value);

                if (input < DateTime.Now.AddYears(-100) || input > DateTime.Now.AddYears(-16))
                {
                    return new ValidationResult(false, string.Format("O utilizador deve ter entre 17 a 100 anos."));
                }
            } catch(Exception e)
            {
                return new ValidationResult(false, string.Format("A data é inválida"));
            }

            return new ValidationResult(true,"");
        }
    }
}
