using BcsExamApp.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcsExamApp.Model
{
    public class ValidatableObject<T> : BindableBase
    {
        private T value;
        public T Value
        {
            get { return value; }
            set { SetProperty(ref this.value, value); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { SetProperty(ref _error, value); }
        }

        private bool _isValid;
        public bool IsValid
        {
            get { return _isValid; }
            set { SetProperty(ref _isValid, value); }
        }

        public ValidatableObject()
        {
            Rules = new List<IValidationRule<T>>();
            _isValid = true;
            _error = "";
        }

        public List<IValidationRule<T>> Rules { get; set; }
        public void Validate()
        {
            Error = "";
            var _errors = Rules.Where(x => !x.Check(Value)).Select(x => x.ValidationMessage.Replace(".", "") + ".").ToList();

            int _count = 1;
            foreach (var x in _errors)
            {
                Error += string.Format("{0}{1}", x, _count >= _errors.Count ? "" : "\n");
                _count++;
            }
            IsValid = !_errors.Any();
        }
    }
}
