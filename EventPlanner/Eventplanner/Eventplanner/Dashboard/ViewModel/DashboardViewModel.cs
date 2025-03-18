using Autofac;
using Eventplanner.Interfaces;
using Eventplanner.Model;
using Eventplanner.ServiceProvider;
using Eventplanner.UI.Base;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Eventplanner.UI.Dashboard.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        private IEventRepository _eventRepository;
        #region Constructor
        public DashboardViewModel(ILifetimeScope scope = null)
        {
            SetScopeRepository(scope);
            Initialize();
        }
        #endregion
        #region Properties
        public List<Model.Event> FutureEvents { get; set; }
        public List<Model.Event> TodayEvents { get; set; }
        public List<Model.Event> PassedEvents { get; set; }

        public int Id;
        #endregion

        #region Methods
        private void SetScopeRepository(ILifetimeScope scope = null)
        {
            var _scope = scope ?? ServiceProvider.ServiceProvider.Instance.BeginLifetimeScope();
            _eventRepository = _eventRepository.Resolve<IEventRepository>();
           
        }

        public void Initialize()
        {
            List<Model.Event> events = _eventRepository.ToList();
            events.Sort((x, y) =>
            {
                return DateTime.Compare(x.DateTimeFrom, y.DateTimeFrom);
            });

            TodayEvents = events.Where(x => x.DateTimeFrom == DateTime.Today).ToList();
            FutureEvents = events.Where(x => x.DateTimeFrom > DateTime.Today).ToList();
            PassedEvents = events.Where(x => x.DateTimeFrom < DateTime.Today).ToList();
        }

        #endregion Methods
        #region Events
        #endregion
        #region Commands
        #endregion
        #region Validation
        #endregion Validation



    }
}
