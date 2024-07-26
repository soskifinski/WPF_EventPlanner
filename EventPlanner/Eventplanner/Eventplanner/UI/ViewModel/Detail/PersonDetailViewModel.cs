using Eventplanner.DataAccess;
using Eventplanner.Model;
using Eventplanner.UI.Data;
using Eventplanner.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Eventplanner.UI.ViewModel.Detail
{
    public class PersonDetailViewModel : DetailViewModelBase, IPersonDetailViewModel
    {
        private IPersonRepository _personRepository;
        private IAddressRepository _addressRepository;
        private PersonWrapper _person;
        private AddressWrapper _address;

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
            get { return _address; }
            private set
            {
                _address = value;
                if (value != null && Person != null)
                {
                    Person.Address = _address.Address;
                }

                OnPropertyChanged();
            }
        }

        public PersonDetailViewModel(IEventAggregator eventAggregator, IPersonRepository personRepository, IAddressRepository addressRepository) : base(eventAggregator)
        {
            _personRepository = personRepository;
            _addressRepository = addressRepository;
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
            Address address = new Address();

            if (person.Address != null)
            {
                address = _addressRepository.FindById(person.AddressId);
            }
            if (address == null)
            {
                _addressRepository.Add(new Address());
                _addressRepository.SaveAsync();
                address = new Address();
            }

            person.Address = address;
            Person = new PersonWrapper(person);
            Address = new AddressWrapper(address);

            Address.PropertyChanged += (sender, args) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _addressRepository.HasChanges();
                }

                if (args.PropertyName == nameof(Address.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

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
            var Person = new Person()
            {
                IsEmployee = false
            };

            _personRepository.Add(Person);
            return Person;
        }

        private void SetTitle()
        {
            Title = $"{Person.FirstName} {Person.LastName}";
        }

        protected override bool OnSaveCanExecute()
        {
            return Person != null && Address != null
              && !Person.HasErrors && !Address.HasErrors
              && HasChanges;
        }

        protected override async void OnDeleteExecute()
        {
            _addressRepository.Remove(Address.Model);
            await _addressRepository.SaveAsync();
            _personRepository.Remove(Person.Model);            
            await _personRepository.SaveAsync();
            RaiseDetailDeletedEvent(Person.Id);
        }
        protected override async void OnSaveExecute()
        {
            await _addressRepository.SaveAsync();
            await _personRepository.SaveAsync();           
            HasChanges = _personRepository.HasChanges() && _addressRepository.HasChanges();
            Id = Person.Id;
            var displayMember = $"{Person.FirstName} {Person.LastName}";
            RaiseDetailSavedEvent(Person.Id, displayMember);
        }
    }
}
