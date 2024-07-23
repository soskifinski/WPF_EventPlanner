using Eventplanner.Model;
using Eventplanner.UI.Data;
using Eventplanner.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Eventplanner.UI.ViewModel.Detail
{
    public class PersonDetailViewModel : DetailViewModelBase, IPersonDetailViewModel
    {
        private IPersonRepository _personRepository;
        private PersonWrapper _person;
        private AddressWrapper _adress;

        public PersonWrapper Person
        {
            get { return _person; }
            private set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        public AddressWrapper Address
        {
            get { return _adress; }
            private set
            {
                _adress = value;
                OnPropertyChanged();
            }
        }

        public PersonDetailViewModel(IEventAggregator eventAggregator,IPersonRepository personRepository) : base(eventAggregator)
        {
            _personRepository = personRepository;
        }

        public override async Task LoadAsync(int personId)
        {
            var person = personId > 0
              ? await _personRepository.GetByIdAsync(personId)
              : CreateNewPerson();

            Id = personId;

            InitializePerson(person);

        }

        private void InitializePerson(Person person)
        {
            Person = new PersonWrapper(person);
            Person.PropertyChanged += (sender, args) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _personRepository.HasChanges();
                }

                if (args.PropertyName == nameof(Person.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                if (args.PropertyName == nameof(Person.FirstName))
                {
                    SetTitle();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (Person.Id == 0)
            {
                // Little trick to trigger the validation
                Person.FirstName = "";
                Person.LastName = "";
            }
            SetTitle();
        }

        private Person CreateNewPerson()
        {
            var Person = new Person();
            _personRepository.Add(Person);
            return Person;
        }

        private void SetTitle()
        {
            Title = $"{Person.FirstName} {Person.LastName}";
        }
        protected override bool OnSaveCanExecute()
        {
            return Person != null
              && !Person.HasErrors
              && HasChanges;
        }

        protected override async void OnDeleteExecute()
        {
            
        }
        protected override async void OnSaveExecute()
        {

        }
        private void Option_Checked(object sender, RoutedEventArgs e)
        {
            // Code, der ausgeführt wird, wenn der ToggleButton im Checked-Zustand ist
        }

        private void Option_Unchecked(object sender, RoutedEventArgs e)
        {
            // Code, der ausgeführt wird, wenn der ToggleButton im Unchecked-Zustand ist
        }

        private void Option_Indeterminate(object sender, RoutedEventArgs e)
        {
            // Code, der ausgeführt wird, wenn der ToggleButton im Indeterminate-Zustand ist
        }

    }
}
