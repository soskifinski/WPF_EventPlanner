using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Eventplanner.UI.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public virtual string DisplayName { get; protected set; }

        private bool _hasData = false;
        public bool HasData
        {
            get { return _hasData; }
            set
            {
                if (_hasData != value)
                {
                    _hasData = value;
                    this.OnPropertyChanged(nameof(HasData));
                }
            }
        }

        private bool _hasChanges;
        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    this.OnPropertyChanged(nameof(HasChanges));
                }
            }
        }

        // the properties in this list are ignored when checking for HasChanges
        protected List<string> untrackedProperties = new List<string> { nameof(HasChanges) };

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property on this object has a new value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this ViewModels PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property that has a new value</param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            this.VerifyPropertyName(name);
            this.OnPropertyChanged(new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Raises this ViewModels PropertyChanged event
        /// </summary>
        /// <typeparam name="T">The Typ</typeparam>
        /// <param name="action">Lambda Expression for the property</param>
        protected void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Returns the Propertyname of the member in Type from the Lambda Expression
        /// </summary>
        /// <typeparam name="T">The Type</typeparam>
        /// <param name="action">Lambda Expression for the property</param>
        /// <returns>Propertyname</returns>
        protected static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        /// <summary>
        /// Raises this ViewModels PropertyChanged event
        /// </summary>
        /// <param name="e">Arguments detailing the change</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion INotifyPropertyChanged Members

        #region IDisposable

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            OnDispose();
        }

        /// <summary>
        /// Internal dispose method.
        /// </summary>
        protected virtual void OnDispose()
        {
        }

#if DEBUG
        /// <summary>
        /// Useful for ensuring that ViewModel objects are properly garbage collected.
        /// </summary>
        ~ViewModelBase()
        {
            string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.DisplayName, this.GetHashCode());
            System.Diagnostics.Debug.WriteLine(msg);
        }
#endif

        #endregion IDisposable

        #region Debugging Aides

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This 
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This 
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void VerifyPropertyName(Type type, string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(type)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;
                Debug.Fail(msg);
            }
        }

        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might 
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion Debugging Aides

        #region Selection
        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private ICommand _itemClickedCommand;
        public ICommand ItemClickedCommand
        {
            get
            {
                if (_itemClickedCommand == null)
                {
                    _itemClickedCommand = new DelegateCommand(OnItemClickedCommand);
                }

                return _itemClickedCommand;
            }
        }

        private void OnItemClickedCommand()
        {
            OnItemClicked();
        }

        public event EventHandler ItemClicked;
        private void OnItemClicked()
        {
            ItemClicked?.Invoke(this, EventArgs.Empty);
        }
        #endregion Selection
    }
}


