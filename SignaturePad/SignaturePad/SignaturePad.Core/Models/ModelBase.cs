using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Intersoft.Crosslight;

namespace SignaturePad.Models
{
    public class ModelBase : INotifyPropertyChanged, INotifyDataErrorInfo, IDataValidation
    {
        private List<ValidationResult> _validationResultList = new List<ValidationResult>();

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public virtual bool HasErrors
        {
            get
            {
                return _validationResultList.Count(o => !o.IsSuccess) > 0;
            }
        }

        public void ClearErrors()
        {
        }
        public void SetError(string errorMessage, string propertyName)
        {
            _validationResultList.Add(new ValidationResult(errorMessage, propertyName));

            this.OnErrorsChanged(propertyName);
            this.OnPropertyChanged(propertyName);
        }

        public void ClearError(string propertyName)
        {
            var emptyValidationResult = new List<ValidationResult>();

            foreach (var result in _validationResultList)
            {
                if (result.MemberNames.Contains(propertyName))
                {
                    ((IList)result.MemberNames).Remove(propertyName);

                    if (result.MemberNames.Count() == 0)
                        emptyValidationResult.Add(result);
                }
            }

            foreach (var result in emptyValidationResult)
                _validationResultList.Remove(result);

            this.OnErrorsChanged(propertyName);
        }

        public void ClearAllErrors()
        {
            List<string> properties = new List<string>();

            foreach (var result in this._validationResultList)
            {
                foreach (var member in result.MemberNames)
                {
                    if (!properties.Contains(member))
                        properties.Add(member);
                }
            }

            _validationResultList.Clear();
            this.OnErrorsChanged("");

            foreach (string property in properties)
                this.OnPropertyChanged(property);
        }

        public IEnumerable GetErrors(string propertyName)
        {
            var validationResult = new List<ValidationResult>();

            foreach (var result in _validationResultList)
            {
                if ((string.IsNullOrEmpty(propertyName) || result.MemberNames.Contains(propertyName)) && !result.IsSuccess)
                    validationResult.Add(result);
            }

            return validationResult;
        }

        public IEnumerable<ValidationResult> GetAllErrors()
        {
            return _validationResultList.Where(o => !o.IsSuccess).ToList();
        }

        protected virtual void OnErrorsChanged(string propertyName)
        {
            this.OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            var handler = this.ErrorsChanged;

            if (handler != null)
                handler(this, e);
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var propertyChanged = this.PropertyChanged;

            if (propertyChanged != null)
                propertyChanged(this, e);
        }

        public virtual void Validate()
        {
        }

        public void RaisePropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(propertyName);
        }
    }
}