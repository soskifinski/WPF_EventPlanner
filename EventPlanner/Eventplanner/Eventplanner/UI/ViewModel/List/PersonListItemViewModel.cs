using Eventplanner.Model;
using Eventplanner.UI.Events;
using FriendOrganizer.UI.ViewModel;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Eventplanner.UI.ViewModel.List
{
    public class PersonListItemViewModel : ViewModelBase
    {
        private string _firstName;
        private string _lastName;       
        private IEventAggregator _eventAggregator;
        private string _detailViewModelName; 

        public ICommand OpenDetailViewCommand { get; }
        public int Id { get; }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public PersonListItemViewModel(Person _person, string detailViewModelName, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = _person.Id;
            FirstName = _person.FirstName;
            LastName = _person.LastName;
            _detailViewModelName = detailViewModelName;
            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailViewExecute);
        }

        private void OnOpenDetailViewExecute()
        {
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                  .Publish(
              new OpenDetailViewEventArgs
              {
                  Id = Id,
                  ViewModelName = _detailViewModelName
              });
        }


    }
}
