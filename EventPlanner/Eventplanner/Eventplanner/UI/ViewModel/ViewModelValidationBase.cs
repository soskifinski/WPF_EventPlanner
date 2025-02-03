using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventplanner.UI.ViewModel
{

    public abstract class ViewModelValidationBase : ViewModelBase, INotifyDataErrorInfo
    {
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName != nameof(HasErrors))
            {
                ValidateAsync();
            }
        }

        #region INotifyDataErrorInfo

        public virtual bool HasErrors
        {
            get
            {
                return _errors.Any(kv => kv.Value != null && kv.Value.Count > 0);
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public event EventHandler Validated;

        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName == null)
                return null;

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return null;
                var messages = _errors.Where(kv => kv.Value.Count > 0).SelectMany(kv => kv.Value).ToList();
                var message = string.Join(Environment.NewLine, messages);
                return new List<string>() { message };
            }
            else
            {
                List<string> errors = null;
                List<string> errorsForName;
                if (_errors.TryGetValue(propertyName, out errorsForName))
                {
                    var message = string.Join(Environment.NewLine, errorsForName);
                    errors = new List<string>() { message };
                }
                return errors;
            }
        }

        public void DoValidate()
        {
            Validate();
        }

        private ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();
        private object _lockValidate = new object();

        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void OnValidated()
        {
            Validated?.Invoke(this, System.EventArgs.Empty);
        }

        protected void AddError(string propertyName, string message)
        {
            lock (_lockValidate)
            {
                List<string> messages;

                if (_errors.TryGetValue(propertyName, out messages))
                {
                    if (!messages.Contains(message))
                        messages.Add(message);
                }
                else
                {
                    _errors.TryAdd(propertyName, new List<string>() { message });
                }

                OnErrorsChanged(propertyName);
            }
        }

        protected void RemoveError(string propertyName, string message)
        {
            lock (_lockValidate)
            {
                List<string> messages;

                if (_errors.TryGetValue(propertyName, out messages))
                {
                    if (messages.Contains(message))
                        messages.Remove(message);

                    if (messages.Count == 0)
                        _errors.TryRemove(propertyName, out messages);
                }

                OnErrorsChanged(propertyName);
            }
        }

        protected void RemoveError(string propertyName)
        {
            lock (_lockValidate)
            {
                List<string> messages;

                if (_errors.TryGetValue(propertyName, out messages))
                {
                    messages.Clear();

                    if (messages.Count == 0)
                        _errors.TryRemove(propertyName, out messages);
                }

                OnErrorsChanged(propertyName);
            }
        }

        protected Task ValidateAsync()
        {
            if (IsTesting)
            {
                Validate();
                return Task.CompletedTask;
            }
            return Task.Run(() => Validate());
        }

        protected virtual void Validate()
        {
            lock (_lockValidate)
            {
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);

                foreach (var kv in _errors.ToList())
                {
                    if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                    {
                        List<string> outLi;
                        _errors.TryRemove(kv.Key, out outLi);
                        OnErrorsChanged(kv.Key);
                    }
                }

                var q = from r in validationResults
                        from m in r.MemberNames
                        group r by m into g
                        select g;

                foreach (var prop in q)
                {
                    var messages = prop.Select(r => r.ErrorMessage).ToList();

                    if (_errors.ContainsKey(prop.Key))
                    {
                        List<string> outLi;
                        _errors.TryRemove(prop.Key, out outLi);
                    }
                    _errors.TryAdd(prop.Key, messages);
                    OnErrorsChanged(prop.Key);
                }

                OnValidated();
            }
        }
        #endregion INotifyDataErrorInfo

        #region TestingProperties
        public bool IsTesting = false;
        #endregion TestingProperties
    }
}

