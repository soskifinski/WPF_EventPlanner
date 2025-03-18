using Eventplanner.Interfaces;
using Eventplanner.Model;
using Eventplanner.UI.Base;
using Prism.Events;
using System.Threading.Tasks;

namespace Eventplanner.UI.Person.ViewModel
{
    public class PersonDetailViewModel : DetailViewModelBase
    {
        #region Properties
        #endregion Properties
        #region Constructor
        #endregion Constructor
        #region Methods
        #endregion Methods
        #region Events
        #endregion
        #region Commands
        #endregion
        #region Validation
        #endregion Validation

        private IPersonRepository _personRepository;
        private IAddressRepository _addressRepository;
        private Model.Person _person;
        private Address _address;

        public int PersonId { get { return _person.Id; } }

        public string FirstName
        {
            get { return _person.FirstName; }
            set
            {

                if (_person.FirstName != value)
                {
                    _person.FirstName = value;
                    this.OnPropertyChanged(nameof(FirstName));
                }
            }
        }


        public string LastName
        {
            get { return _person.LastName; }
            set
            {

                if (_person.LastName != value)
                {
                    _person.LastName = value;
                    this.OnPropertyChanged(nameof(LastName));
                }
            }
        }

        public string TelephoneNumber
        {
            get { return _person.TelephoneNumber; }
            set
            {

                if (_person.TelephoneNumber != value)
                {
                    _person.TelephoneNumber = value;
                    this.OnPropertyChanged(nameof(TelephoneNumber));
                }
            }
        }

        public string Email
        {
            get { return _person.Email; }
            set
            {

                if (_person.Email != value)
                {
                    _person.Email = value;
                    this.OnPropertyChanged(nameof(Email));
                }
            }
        }


        public bool IsEmployee
        {
            get { return _person.IsEmployee; }
            set
            {

                if (_person.IsEmployee != value)
                {
                    _person.IsEmployee = value;
                    this.OnPropertyChanged(nameof(IsEmployee));
                }
            }
        }

        public Gender Gender
        {
            get { return _person.Gender; }
            set
            {

                if (_person.Gender != value)
                {
                    _person.Gender = value;
                    this.OnPropertyChanged(nameof(Gender));
                }
            }
        }
        #region Adress

        public int AdressId { get { return _address.Id; } }


        public string StreetHouseNr
        {
            get { return _address.StreetHouseNr; }
            set
            {

                if (_address.StreetHouseNr != value)
                {
                    _address.StreetHouseNr = value;
                    this.OnPropertyChanged(nameof(StreetHouseNr));
                }
            }
        }


        public string PostalCode
        {
            get { return _address.PostalCode; }
            set
            {

                if (_address.PostalCode != value)
                {
                    _address.PostalCode = value;
                    this.OnPropertyChanged(nameof(PostalCode));
                }
            }
        }

        public string City
        {
            get { return _address.City; }
            set
            {

                if (_address.City != value)
                {
                    _address.City = value;
                    this.OnPropertyChanged(nameof(City));
                }
            }
        }

        public string Country
        {
            get { return _address.Country; }
            set
            {

                if (_address.Country != value)
                {
                    _address.Country = value;
                    this.OnPropertyChanged(nameof(Country));
                }
            }
        }

        #endregion


        #region constructor
        public PersonDetailViewModel(IEventAggregator eventAggregator, IPersonRepository personRepository, IAddressRepository addressRepository) : base(eventAggregator)
        {
            _personRepository = personRepository;
            _addressRepository = addressRepository;
        }
        #endregion
        public override async Task LoadAsync(int personId)
        {
            var person = personId > 0
              ? await _personRepository.FindAsync(personId)
              : CreateNewPerson();
            Id = personId;
            InitializePerson(person);
        }

        private void InitializePerson(Model.Person person)
        {
            Address address = new Address();

            if (person.Address != null)
            {
                address = _addressRepository.FindById(person.AddressId);
            }
            if (address == null)
            {
                _addressRepository.Add(new Address());
                _addressRepository.SaveChangesAsync();
                address = new Address();
            }
 
        }

        private Model.Person CreateNewPerson()
        {
            var Person = new Model.Person()
            {
                IsEmployee = false
            };
            _person = Person;
            _personRepository.Add(Person);
            return Person;
        }

        private void SetTitle()
        {
            DisplayName = $"{_person.FirstName} {_person.LastName}";
        }

        protected override bool OnSaveCanExecute()
        {
            return _person != null && _address != null;
              
        }

        protected override async void OnDeleteExecute()
        {
            _addressRepository.Remove(_address);
            _personRepository.Remove(_person);
            await _personRepository.SaveChangesAsync();
            RaiseDetailDeletedEvent(_person.Id);
        }
        protected override async void OnSaveExecute()
        {
            await _personRepository.SaveChangesAsync();
            HasChanges = _personRepository.HasChanges() && _addressRepository.HasChanges();
            Id = _person.Id;
            var displayMember = $"{_person.FirstName} {_person.LastName}";
            RaiseDetailSavedEvent(_person.Id, displayMember);
        }
    }
}
