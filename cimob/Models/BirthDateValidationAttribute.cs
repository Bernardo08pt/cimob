using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
            DateTime dataLimiteMaxima = (DateTime.Now.AddYears(-17));
            DateTime dataLimiteMinima = (DateTime.Now.AddYears(-100));

            DateTime valor = ((DateTime)value);

            return valor.Date < dataLimiteMaxima.Date && valor.Date > dataLimiteMinima.Date;
        }

    }
}
