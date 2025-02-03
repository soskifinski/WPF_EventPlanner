using Eventplanner.Model;
using Eventplanner.UI.Data;
using Eventplanner.UI.Events;
using Eventplanner.UI.ViewModel.Detail;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Eventplanner.UI.ViewModel.List
{
    public class PersonListViewModel : ViewModelBase, IListViewModel
    {
        public int Id { get; }
        private IPersonRepository _personRepo;
        private IEventAggregator _eventAggregator;
        public ObservableCollection<PersonListItemViewModel> Persons { get; }

        public PersonListViewModel(IPersonRepository personRepo,
         IEventAggregator eventAggregator)
        {
            _personRepo = personRepo;
            _eventAggregator = eventAggregator;
            Persons = new ObservableCollection<PersonListItemViewModel>();
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
        }

        public async Task LoadAsync()
        {
            var persons = await _personRepo.GetAllPersonsAsync();
            Persons.Clear();


            foreach (var item in persons)
            {
                Persons.Add(new PersonListItemViewModel(item, nameof(PersonDetailViewModel), _eventAggregator));
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            var Person = Persons.SingleOrDefault(l => l.Id == args.Id);
            Person thisPerson = _personRepo.FindById(args.Id);
            if (Person == null)
            {
                Person = new PersonListItemViewModel(thisPerson,
                  args.ViewModelName,
                  _eventAggregator);
            }
            else
            {
                Person.FirstName = args.DisplayMember;
            }
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            AfterDetailDeleted(Persons, args);
        }
        private void AfterDetailDeleted(ObservableCollection<PersonListItemViewModel> items,
          AfterDetailDeletedEventArgs args)
        {
            var item = items.SingleOrDefault(f => f.Id == args.Id);
            if (item != null)
            {
                items.Remove(item);
            }
        }
    }
}
