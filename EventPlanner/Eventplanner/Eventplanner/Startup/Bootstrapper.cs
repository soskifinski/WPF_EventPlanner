
using Autofac;
using Eventplanner.DataAccess;
using Eventplanner.Interfaces;
using Eventplanner.Repository;
using Eventplanner.UI.Base;
using Eventplanner.UI.Dashboard.ViewModel;
using Eventplanner.UI.Event.ViewModel;
using Eventplanner.UI.Main;
using Eventplanner.UI.Person.ViewModel;
using Eventplanner.UI.Person.ViewModel.List;
using Eventplanner.UI.Room.ViewModel;
using Eventplanner.UI.Schedule.ViewModel;
using Prism.Events;

namespace Eventplanner.UI.Startup
{
    public class Bootstrapper
    {
        public Autofac.IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<EventPlannerDbContext>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainWindowViewModel>().AsSelf();

            builder.RegisterType<EventListViewModel>()
             .Keyed<IListViewModel>(nameof(EventListViewModel));

            builder.RegisterType<PersonListViewModel>()
            .Keyed<IListViewModel>(nameof(PersonListViewModel));

            builder.RegisterType<RoomListViewModel>()
           .Keyed<IListViewModel>(nameof(RoomListViewModel));

            builder.RegisterType<EventDetailViewModel>()
             .Keyed<IDetailViewModel>(nameof(EventDetailViewModel));

            builder.RegisterType<PersonDetailViewModel>()
             .Keyed<IDetailViewModel>(nameof(PersonDetailViewModel));

            builder.RegisterType<RoomDetailViewModel>()
             .Keyed<IDetailViewModel>(nameof(RoomDetailViewModel));

            builder.RegisterType<ScheduleDetailViewModel>()
            .Keyed<IDetailViewModel>(nameof(ScheduleDetailViewModel));

            builder.RegisterType<DashboardViewModel>()
            .Keyed<ViewModelBase>(nameof(DashboardViewModel));


            builder.RegisterType<EventRepository>().As<IEventRepository>();
            builder.RegisterType<PersonRepository>().As<IPersonRepository>();
            builder.RegisterType<RoomRepository>().As<IRoomRepository>();
            builder.RegisterType<LocationRepository>().As<ILocationRepository>();
            builder.RegisterType<ScheduleRepository>().As<IScheduleRepository>();
            builder.RegisterType<ServiceTaskRepository>().As<IServiceTaskRepository>();
            builder.RegisterType<AddressRepository>().As<IAddressRepository>();

            return builder.Build();
        }
    }
}
