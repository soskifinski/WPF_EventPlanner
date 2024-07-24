using Eventplanner.Model;
using Eventplanner.UI.Data;
using Eventplanner.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;

namespace Eventplanner.UI.ViewModel.Detail
{
    public class PersonDetailViewModel : DetailViewModelBase, IPersonDetailViewModel
    {
        private IPersonRepository _personRepository;
        private PersonWrapper _person;

        public PersonWrapper Person
        {
            get { return _person; }
            private set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        public PersonDetailViewModel(IEventAggregator eventAggregator, IPersonRepository personRepository) : base(eventAggregator)
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
            var addressWrapper = new AddressWrapper(person.Address ?? new Address());


            Person = new PersonWrapper(person)
            {
                Address = addressWrapper
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
            var Address = new Address();
            var Person = new Person()
            {
                IsEmployee = false,
                Address = Address
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
            return Person != null
              && !Person.HasErrors
              && HasChanges;
        }

        protected override async void OnDeleteExecute()
        {
            _personRepository.Remove(Person.Model);
            await _personRepository.SaveAsync();
            RaiseDetailDeletedEvent(Person.Id);
        }
        protected override async void OnSaveExecute()
        {
            await _personRepository.SaveAsync();
            HasChanges = _personRepository.HasChanges();
            Id = Person.Id;
            var displaymember = Person.FirstName;
            RaiseDetailSavedEvent(Person.Id, displaymember);
        }
    }
}
