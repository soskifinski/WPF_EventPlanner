using Eventplanner.Model;
using Eventplanner.UI.Data;
using Eventplanner.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Eventplanner.UI.ViewModel.Detail
{
    public class RoomDetailViewModel : DetailViewModelBase, IRoomDetailViewModel
    {
        private IRoomRepository _roomRepository;
        private ILocationRepository _locationRepository;
        private RoomWrapper _room;
        public ObservableCollection<Location> Locations { get; }


        public RoomWrapper Room
        {
            get { return _room; }
            private set
            {
                _room = value;
                OnPropertyChanged();
            }
        }

        public RoomDetailViewModel(IEventAggregator eventAggregator, IRoomRepository roomRepository, ILocationRepository locationRepository) : base(eventAggregator)
        {
            _roomRepository = roomRepository;
            _locationRepository = locationRepository;

            Locations = new ObservableCollection<Location>();
        }
        public override async Task LoadAsync(int Id)
        {
            var room = Id > 0
              ? await _roomRepository.GetByIdAsync(Id)
              : CreateNewRoom();

            this.Id = Id;

            InitializeRoomWrapper(room);
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

        private void InitializeRoomWrapper(Room room)
        {
            Room = new RoomWrapper(room);

            Room.PropertyChanged += (sender, args) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _roomRepository.HasChanges();
                }

                if (args.PropertyName == nameof(Room.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

                    if (args.PropertyName == nameof(Room.RoomNumber))
                    {
                        SetTitle();
                    }
                };
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

                if (Room.Id == 0)
                {
                    // Little trick to trigger the validation
                    Room.RoomNumber = "";
                }
                SetTitle();
            };

        }

        private Room CreateNewRoom()
        {
            var room = new Room
            {
            };
            _roomRepository.Add(room);
            return room;
        }

        protected override async void OnDeleteExecute()
        {
            _roomRepository.Remove(Room.Model);
            await _roomRepository.SaveAsync();
            RaiseDetailDeletedEvent(Room.Id);
        }

        protected override bool OnSaveCanExecute()
        {
            return Room != null
             && !Room.HasErrors
             && HasChanges;
        }

        protected override async void OnSaveExecute()
        {
            await _roomRepository.SaveAsync();
            HasChanges = _roomRepository.HasChanges();
            Id = Room.Id;
            var displaymember = Room.RoomNumber;
            RaiseDetailSavedEvent(Room.Id, displaymember);
        }
        private void SetTitle()
        {
            Title = Room.RoomNumber;
        }
    }
}
