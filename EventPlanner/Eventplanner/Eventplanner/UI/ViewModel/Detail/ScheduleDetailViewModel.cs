using Eventplanner.Model;
using Eventplanner.UI.Data;
using Eventplanner.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Eventplanner.UI.ViewModel.Detail
{
    public class ScheduleDetailViewModel : DetailViewModelBase
    {
        private IScheduleRepository _scheduleRepository;
        private IPersonRepository _personRepository;
        private ScheduleWrapper _scheduleWrapper;
        public List<ServiceTask> Appointments;
        public List<Person> Employees;

        public ScheduleWrapper Schedule
        {
            get { return _scheduleWrapper; }
            private set
            {
                _scheduleWrapper = value;
                OnPropertyChanged();
            }
        }

        public ScheduleDetailViewModel(IEventAggregator eventAggregator, IScheduleRepository repository, IPersonRepository personRepository) : base(eventAggregator)
        {
            _scheduleRepository = repository;
            _personRepository = personRepository;
        }

        public override async Task LoadAsync(int Id)
        {
            var thisRoster = Id > 0
              ? await _scheduleRepository.FindAsync(Id)
              : CreateNewSchedule();
            this.Id = Id;
            this.Employees = await _personRepository.GetAllEmpoyeesAsync();
            InitializeScheduleWrapper(thisRoster);

        }

        private void InitializeScheduleWrapper(Schedule schedule)
        {

            Schedule = new ScheduleWrapper(schedule) { };

          
        }

        private Schedule CreateNewSchedule()
        {
            var schedule = new Schedule(){};
            _scheduleRepository.Add(schedule);
            return schedule;
        }

        protected override bool OnSaveCanExecute()
        {
            return Schedule != null && !Schedule.HasErrors && HasChanges;
        }

        protected override async void OnDeleteExecute()
        {
            _scheduleRepository.Remove(Schedule.Model);
            await _scheduleRepository.SaveChangesAsync();
        }

        protected override async void OnSaveExecute()
        {
            await _scheduleRepository.SaveChangesAsync();
            HasChanges = _scheduleRepository.HasChanges();
        }

        //private void CreateResourceAppointments()
        //{
        //    Random random = new Random();
        //    DateTime date;
        //    DateTime dateFrom = DateTime.Now.AddYears(-1).AddDays(-20);
        //    DateTime dateTo = DateTime.Now.AddYears(1).AddDays(20);
        //    foreach (var resource in Schedule.Appointments)
        //    {
        //        for (date = dateFrom; date < dateTo; date = date.AddDays(1))
        //        {
        //            Appointment workDetails = new Appointment();
        //            workDetails.DateTimeFrom = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        //            workDetails.DateTimeUntil = workDetails.DateTimeUntil.AddHours(1);
        //            workDetails.Event = workDetails.Event;
        //            workDetails.Event.Title = workDetails.Event.Title;


        //            Appointments.Add(workDetails);
        //        }
        //    }
        //}
    }
}
