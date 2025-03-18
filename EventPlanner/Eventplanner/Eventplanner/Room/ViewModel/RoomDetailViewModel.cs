using Eventplanner.Interfaces;
using Eventplanner.Model;
using Eventplanner.UI.Base;
using Eventplanner.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Eventplanner.UI.Room.ViewModel
{
    public class RoomDetailViewModel : DetailViewModelBase
    {
        #region Properties

        private IRoomRepository _roomRepository;
        private ILocationRepository _locationRepository;
        public ObservableCollection<Location> Locations { get; }
        
        public Model.Room Model { get; }

        public RoomViewModel RoomViewModel { get; set; }
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



        public RoomDetailViewModel(IEventAggregator eventAggregator, IRoomRepository roomRepository, ILocationRepository locationRepository) : base(eventAggregator)
        {
            _roomRepository = roomRepository;
            _locationRepository = locationRepository;

            Locations = new ObservableCollection<Location>();

            Model = Id > 0
             ?  _roomRepository.FirstOrDefault( x=> x.Id == Id)
             : CreateNewRoom();
        }
        public override async Task LoadAsync(int Id)
        {         
            RoomViewModel = new RoomViewModel(Model);
            this.Id = Id;  
            DisplayName = Model.RoomNumber;

            //RegisterEventHandler();

            await LoadLocationsLookupAsync();
        }

        private async Task LoadLocationsLookupAsync()
        {
            Locations.Clear();
            Locations.Add(new Location { Name = " - " });
            var lookup = await _locationRepository.GetAllLocationsAsync();
            foreach (var lookupItem in lookup)
            {
                Locations.Add(lookupItem);
            }
        }

       
        private Model.Room CreateNewRoom()
        {
            var room = new Model.Room
            {
            };
            _roomRepository.Add(room);
            return room;
        }

        protected override async void OnDeleteExecute()
        {
            _roomRepository.Remove(Model);
            await _roomRepository.SaveChangesAsync();
            RaiseDetailDeletedEvent(Model.Id);
        }

        protected override bool OnSaveCanExecute()
        {
            return Model != null
             && !this.HasErrors
             && HasChanges;
        }

        protected override async void OnSaveExecute()
        {
            await _roomRepository.SaveChangesAsync();
            HasChanges = _roomRepository.HasChanges();
            Id = Model.Id;
            var displaymember = Model.RoomNumber;
            RaiseDetailSavedEvent(Model.Id, displaymember);
        }
    }
}
