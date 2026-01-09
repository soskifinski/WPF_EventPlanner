using Eventplanner.Model;
using Eventplanner.UI.Data;
using Eventplanner.UI.Events;
using Eventplanner.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace Eventplanner.UI.ViewModel.Detail
{
    public class EventDetailViewModel : DetailViewModelBase
    {
        private IEventRepository _eventRepository;
        private IRoomRepository _roomRepo;
        private IScheduleRepository _scheduleRepo;
        private EventWrapper _event;

        public ObservableCollection<Room> Rooms { get; }
        public ObservableCollection<ServiceTask> Appointments { get; }

        private Status _selectedStatus;
        public ObservableCollection<Status> Stati { get; set; }

        public EventWrapper Event
        {
            get { return _event; }
            private set
            {
                _event = value;
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

        public EventDetailViewModel(IEventAggregator eventAggregator, IEventRepository eventRepository, IRoomRepository roomRepository, IScheduleRepository scheduleRepository) : base(eventAggregator)
        {
            _eventRepository = eventRepository;
            _roomRepo = roomRepository;
            _scheduleRepo = scheduleRepository;

            eventAggregator.GetEvent<AfterCollectionSavedEvent>()
               .Subscribe(AfterCollectionSaved);
            Rooms = new ObservableCollection<Room>();
            Appointments = new ObservableCollection<ServiceTask>();
            Stati = new ObservableCollection<Status>();
        }

        public override async Task LoadAsync(int eventId)
        {
            var thisEvent = eventId > 0
              ? await _eventRepository.FindAsync(eventId)
              : CreateNewEvent();

            Id = eventId;
            InitializeEvent(thisEvent);
            await GetRoomsAsync();
            await GetServiceTasksAsync();
            //UpdateStatus von Published,Gebucht,inPreparation  wenn DatumUntil < AktuellesDatum  auf Stattgefunden fehlt
            InitializeStatus();
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

        public Event CreateNewEvent()
        {
            var Event = new Event
            {
                DateTimeFrom = DateTime.Now.Date,
                DateTimeTo = DateTime.Now.Date
            };
            _eventRepository.Add(Event);
            return Event;
        }

        private void InitializeEvent(Event newEvent)
        {
            Event = new EventWrapper(newEvent);
        }


        private void SetTitle()
        {
            DisplayName = Event.Title;
        }

        protected override bool OnSaveCanExecute()
        {
            return Event != null && !Event.HasErrors && HasChanges;
        }

        protected override async void OnSaveExecute()
        {
            await _eventRepository.SaveChangesAsync();
            HasChanges = _eventRepository.HasChanges();
            Id = Event.Id;
            RaiseDetailSavedEvent(Event.Id, Event.Title);
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
            _eventRepository.Remove(Event.Model);
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
