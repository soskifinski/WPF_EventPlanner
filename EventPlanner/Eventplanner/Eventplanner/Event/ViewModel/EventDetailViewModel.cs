using Eventplanner.Interfaces;
using Eventplanner.Model;
using Eventplanner.UI.Base;
using Eventplanner.UI.Events;
using Eventplanner.UI.Room.ViewModel;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace Eventplanner.UI.Event.ViewModel
{
    public class EventDetailViewModel : DetailViewModelBase
    {
        private IEventRepository _eventRepository;
        private IRoomRepository _roomRepo;
        private IScheduleRepository _scheduleRepo;

        #region Constructor
        public EventDetailViewModel(IEventAggregator eventAggregator, IEventRepository eventRepository, IRoomRepository roomRepository, IScheduleRepository scheduleRepository) : base(eventAggregator)
        {
            _eventRepository = eventRepository;
            _roomRepo = roomRepository;
            _scheduleRepo = scheduleRepository;

            Rooms = new ObservableCollection<Model.Room>();
            Appointments = new ObservableCollection<ServiceTask>();
            Stati = new ObservableCollection<Status>();
            DisplayName = Event.Title;
        }

        #endregion Constructor

        #region Properties
        public Model.Event Event;
        public ObservableCollection<Model.Room> Rooms { get; }
        public ObservableCollection<ServiceTask> Appointments { get; }

        private Status _selectedStatus;
        public ObservableCollection<Status> Stati { get; set; }

        public int Id { get { return Event.Id; } }

        private Status _status;
        public Status Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                };
            }
        }
        public string Title
        {
            get { return Event.Title; }
            set
            {
                Event.Title = value;
                OnPropertyChanged();
            }
        }

        public string SubTitle
        {
            get { return Event.SubTitle; }
            set
            {
                Event.SubTitle = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateTimeFrom
        {
            get { return Event.DateTimeFrom; }
            set
            {
                Event.DateTimeFrom = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateTimeTo
        {
            get { return Event.DateTimeTo; }
            set
            {
                Event.DateTimeTo = value;
                OnPropertyChanged();
            }
        }

        public int StatusId
        {
            get { return ((int)_selectedStatus); }
            set
            {
                Event.StatusId = value;
                OnPropertyChanged();
            }
        }

        public int TotalTickets
        {
            get { return Event.TotalTickets; }
            set
            {
                Event.TotalTickets = value;
                OnPropertyChanged();
            }
        }

        public int BookedTickets
        {
            get { return Event.BookedTickets; }
            set
            {
                Event.BookedTickets = value;
                OnPropertyChanged();
            }
        }

        public int? RoomId
        {
            get { return Event.RoomId; }
            set
            {
                Event.RoomId = value;
                OnPropertyChanged();
            }
        }


        public Status SelectedStatus
        {
            get { return _selectedStatus; }
            set
            {
                _selectedStatus = value;
                OnPropertyChanged();
            }
        }
        #endregion Properties
        
        #region Methods
        #endregion Methods
        #region Events
        #endregion
        #region Commands
        #endregion
        #region Validation
        #endregion Validation





        public override async Task LoadAsync(int eventId)
        {
            var thisEvent = eventId > 0
              ? await _eventRepository.FindAsync(eventId)
              : CreateNewEvent();

            InitializeEvent(thisEvent);
            await GetRoomsAsync();
            await GetServiceTasksAsync();
            //UpdateStatus von Published,Gebucht,inPreparation  wenn DatumUntil < AktuellesDatum  auf Stattgefunden fehlt
            InitializeStatus();
        }

        private void InitializeEvent(Model.Event thisEvent)
        {
            throw new NotImplementedException();
        }

        private void InitializeStatus()
        {   
            Stati.Clear();
            var statiArray = Enum.GetValues(typeof(Status));
            foreach (var status in statiArray)
            {
                Stati.Add((Status)status);
            }
        }

        private async void AfterCollectionSaved(AfterCollectionSavedEventArgs args)
        {
            if (args.ViewModelName == nameof(RoomDetailViewModel))
            {
                await GetRoomsAsync();
            }
        }

        public Model.Event CreateNewEvent()
        {
            var Event = new Model.Event
            {
                DateTimeFrom = DateTime.Now.Date,
                DateTimeTo = DateTime.Now.Date
            };
            _eventRepository.Add(Event);
            return Event;
        }



        protected override bool OnSaveCanExecute()
        {
            return Event != null;
        }

        protected override async void OnSaveExecute()
        {
            await _eventRepository.SaveChangesAsync();
            HasChanges = _eventRepository.HasChanges();
            //Id = Event.Id;
            //RaiseDetailSavedEvent(Event.Id, Event.Title);
        }

        protected override async void OnDeleteExecute()
        {
            ////erst schauen ob ein Kontakt Dienst hat bei Event
            //if (await _eventRepository.HasMeetingsAsync(Friend.Id))
            //{
            //    MessageDialogService.ShowInfoDialog($"{Friend.FirstName} {Friend.LastName} can't be deleted, as this friend is part of at least one meeting");
            //    return;
            //}

            //var result = MessageDialogService.ShowOkCancelDialog($"Do you really want to delete the friend {Friend.FirstName} {Friend.LastName}?",
            //  "Question");
            //if (result == MessageDialogResult.OK)
            //{
            _eventRepository.Remove(Event);
            await _eventRepository.SaveChangesAsync();
            //}
        }

        private async Task GetRoomsAsync()
        {
            Rooms.Clear();
            Rooms.Add(new NullRoom { RoomNumber = " - " });
            var rooms = await _roomRepo.GetAllRoomsAsync();
            foreach (var room in rooms)
            {
                Rooms.Add(room);
            }
        }

        private async Task GetServiceTasksAsync()
        {
            //Alle Personen laden die Employees sind 
            //Und eigentlich pro Rolle schauen ob eine Person für dieses Event das zugeschrieben bekommen aht

            //Appointments.Clear();
            //var appointments = await _scheduleRepo.GetAllAppointmentsOfEventAsync();
            //foreach ()
            //{
            //    Appointments.Add(employee);
            //}
        }

    }
}
