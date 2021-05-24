using System.Collections.Generic;
using FluentValidation.Validators;

namespace VideoPlatform.Api.Models.Validators
{
    internal class UniqueValidator<T> : PropertyValidator where T : class
    {
        private readonly IEnumerable<T> _items;

        public UniqueValidator(IEnumerable<T> items) //: base("{PropertyName} must be unique")
        {
            _items = items;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            //var editedItem = context.Instance as T;
            //var newValue = context.PropertyValue as string;
            //var propName = context.PropertyName;
            //if (propName.Contains('.'))
            //    propName = propName.Substring(propName.LastIndexOf('.') + 1);
            //var property = typeof(T).GetTypeInfo().GetDeclaredProperty(propName);
            //return _items.All(item => item.Equals(editedItem) || property.GetValue(item).ToString() != newValue);

            return true;
        }
    }
}