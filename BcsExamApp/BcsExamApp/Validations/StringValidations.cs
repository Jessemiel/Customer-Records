using BcsExamApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BcsExamApp.Validations
{
    class StringValidations
    {
    }

    class EmptyOrNullValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null) 
                return false;

            if (value.GetType() != typeof(string))
                return true;

            var _content = value as string;
            return !string.IsNullOrWhiteSpace(_content);
        }
    }

    class DateFormatValidation<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            var _content = Convert.ToDateTime(value).ToShortDateString();

            if (string.IsNullOrEmpty(_content))
                return true;

            return _content.IsDateFormatValid();
        }
    }

    class EmailValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            var _content = value as string;

            if (string.IsNullOrEmpty(_content)) 
                return true;

            return _content.IsValidEmail();
        }
    }
}
