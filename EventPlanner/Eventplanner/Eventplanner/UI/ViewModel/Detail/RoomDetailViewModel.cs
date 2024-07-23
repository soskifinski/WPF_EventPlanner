using Eventplanner.Model;
using Eventplanner.UI.Data;
using Eventplanner.UI.Events;
using Eventplanner.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Eventplanner.UI.ViewModel.Detail
{
    public class RoomDetailViewModel : DetailViewModelBase, IRoomDetailViewModel
    {
        private RoomRepository _roomRepository;
        private PersonWrapper _room;
        private bool _hasChanges;
        public int Id { get; }
        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }


        //public RoomWrapper Room
        //{
        //    get { return _room; }
        //    private set
        //    {
        //        _room = value;
        //        OnPropertyChanged();
        //    }
        //}

        public RoomDetailViewModel(IEventAggregator eventAggregator,
         RoomRepository meetingRepository) : base(eventAggregator)
        {
            _roomRepository = meetingRepository;  
        }
        public override async Task LoadAsync(int Id)
        {
            //_allPersons = await _roomRepository.GetAllPersonsAsync();
            //foreach (var availablePerson in _allPersons)
            //{
            //    var person = new Person();
            //    _roomRepository.Add(person);
            //}
            //SetTitle();
        }

        protected override void OnDeleteExecute()
        {
            throw new System.NotImplementedException();
        }

        protected override bool OnSaveCanExecute()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnSaveExecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
