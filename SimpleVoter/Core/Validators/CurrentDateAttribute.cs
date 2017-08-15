using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleVoter.Core.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CurrentDateAttribute : ValidationAttribute, IClientValidatable
    {
        public CurrentDateAttribute() : base("Date must be greater than current date!")
        { }
        public CurrentDateAttribute(string errorMessage) : base(errorMessage)
        { }

        public override bool IsValid(object value)
        {
            var date = value as DateTime?;
            return date == null || date.Value > DateTime.Now;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var errorMessage = ErrorMessageString;

            var currentdateRule = new ModelClientValidationRule
            {
                ErrorMessage = errorMessage,
                ValidationType = "currentdate"
            };

            yield return currentdateRule;
        }
    }
}