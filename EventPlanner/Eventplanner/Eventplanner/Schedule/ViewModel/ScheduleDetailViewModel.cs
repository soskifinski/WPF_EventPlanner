using Eventplanner.Interfaces;
using Eventplanner.Model;
using Eventplanner.UI.Base;
using Eventplanner.UI.Wrapper;
using Prism.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventplanner.UI.Schedule.ViewModel
{
    public class ScheduleDetailViewModel : DetailViewModelBase
    {
        //nochmal überarbeiten
        //Unterscheidung zw. Kalender, Dienst bzw auch einzelne Aufgabe und Rolle sowie unterscheidung zw. Event und Person 
        //ein Dienst hängt an einem Event und an einer oder mehreren Personen
        //was ist der Schedule ist das der Dienstplan eines Events? oder einer Person?

        #region Properties
        private IScheduleRepository _scheduleRepository;
        private IPersonRepository _personRepository;
        private Model.Schedule Schedule;
        public List<ServiceTask> Appointments;
        public List<Model.Person> Employees;

        #endregion Properties

        #region Constructor
        public ScheduleDetailViewModel(IEventAggregator eventAggregator, IScheduleRepository repository, IPersonRepository personRepository) : base(eventAggregator)
        {
            _scheduleRepository = repository;
            _personRepository = personRepository;
        }
        #endregion Constructor

        #region Methods

        public override async Task LoadAsync(int Id)
        {
            var schedule = Id > 0
              ? await _scheduleRepository.FindAsync(Id)
              : CreateNewSchedule();
            this.Id = Id;
            this.Employees = await _personRepository.GetAllEmpoyeesAsync();

        }
        #endregion Methods


        #region Events
        #endregion

        #region Commands
        private Model.Schedule CreateNewSchedule()
        {
            var schedule = new Model.Schedule() { };
            _scheduleRepository.Add(schedule);
            return schedule;
        }

        protected override bool OnSaveCanExecute()
        {
            return Schedule != null && !this.HasErrors && HasChanges;
        }

        protected override async void OnDeleteExecute()
        {
            _scheduleRepository.Remove(this.Schedule);
            await _scheduleRepository.SaveChangesAsync();
        }

        protected override async void OnSaveExecute()
        {
            await _scheduleRepository.SaveChangesAsync();
            HasChanges = _scheduleRepository.HasChanges();
        }
        #endregion

        #region Validation
        #endregion Validation










    }
}
