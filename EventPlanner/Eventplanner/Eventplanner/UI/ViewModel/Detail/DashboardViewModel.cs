using Eventplanner.DataAccess;
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
    public class DashboardViewModel: DetailViewModelBase, IDashboardViewModel
    {

        //Ticketsübersicht
        //Events vll. bevorstehende, vergangene und mit Statusübersicht?
        private IEventAggregator _eventAggregator;
        private IEventRepository _eventRepository;


        public ObservableCollection<Event> Events { get; }

        //von EventListViewModel oder ähnliches für
        //DashboardEventStatusListItem
        
        public int Id;

        public DashboardViewModel(IEventAggregator eventAggregator, IEventRepository eventRepository, IRoomRepository roomRepository, IScheduleRepository scheduleRepository) 
            : base(eventAggregator)
        {
            _eventRepository = eventRepository;
        }

   
        public override Task LoadAsync(int id)
        {
            throw new NotImplementedException();
        }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }

        protected override bool OnSaveCanExecute()
        {
            throw new NotImplementedException();
        }

        protected override void OnSaveExecute()
        {
            throw new NotImplementedException();
        }
    }
}
