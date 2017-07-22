using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SimpleVoter.Core.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class EnsureMinimumElementsAttribute : ValidationAttribute
    {
        private readonly int _minElements;
        public EnsureMinimumElementsAttribute(int minElements, string errorMessage)
            : base(errorMessage)
        {
            _minElements = minElements;
        }

        public override bool IsValid(object value)
        {
            var list = value as IList;
            return list?.Count >= _minElements;
        }

        //The metadata expressed for this rule will be used by the runtime to emit the HTML5 data-val custom attributes.
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            //string errorMessage = this.FormatErrorMessage(metadata.DisplayName);
            string errorMessage = ErrorMessageString;

            // The value we set here are needed by the jQuery adapter
            ModelClientValidationRule ensureMinimumElementsRule = new ModelClientValidationRule();
            ensureMinimumElementsRule.ErrorMessage = errorMessage;
            ensureMinimumElementsRule.ValidationType = "ensureminimumelements"; // This is the name the jQuery adapter will use
            //"minElements" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
            ensureMinimumElementsRule.ValidationParameters.Add("minelements", _minElements);

            yield return ensureMinimumElementsRule;
        }
    }
}