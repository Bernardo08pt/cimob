using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimob.Models
{
    public class BirthDateValidationAttribute : ValidationAttribute, IClientModelValidator
    {

        public override bool IsValid(object value)
        {
            DateTime dataLimiteMaxima = (DateTime.Now.AddYears(-17));
            DateTime dataLimiteMinima = (DateTime.Now.AddYears(-100));

            DateTime valor = ((DateTime)value);

            return valor.Date < dataLimiteMaxima.Date && valor.Date > dataLimiteMinima.Date;
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            MergeAttribute(context.Attributes, "data-val-dataoffparameters", errorMessage);
        }

        private bool MergeAttribute(
            IDictionary<string, string> attributes,
            string key,
            string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
    
}
