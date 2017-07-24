using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Core.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class EnsureMinimumElementsAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly int _minElements;
        public EnsureMinimumElementsAttribute(int minElements, string errorMessage)
            : base(errorMessage)
        {
            _minElements = minElements;
        }

        public override bool IsValid(object value)
        {
            var list = value as IList<Answer>;
            var filteredList = list?.Where(s => !string.IsNullOrWhiteSpace(s.Content)).DistinctBy(a => a.Content).ToList();
            return filteredList?.Count >= _minElements;
        }

        //The metadata expressed for this rule will be used by the runtime to emit the HTML5 data-val custom attributes.
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string errorMessage = ErrorMessageString;

            // Wartości te wykorzystywane są przez adapter jQuery
            ModelClientValidationRule ensureMinimumElementsRule = new ModelClientValidationRule();
            ensureMinimumElementsRule.ErrorMessage = errorMessage;
            ensureMinimumElementsRule.ValidationType = "ensureminimumelements"; // nazwa której będzie używał jQuery adapter

            //"minElements" jest nazwą parametru jQuery dla adaptera i musi składać się z małych liter.
            ensureMinimumElementsRule.ValidationParameters.Add("minelements", _minElements);

            yield return ensureMinimumElementsRule;
        }
    }
}