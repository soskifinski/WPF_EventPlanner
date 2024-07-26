
using Autofac;
using Prism.Events;
using Eventplanner.DataAccess;
using Eventplanner.UI.Data;
using Eventplanner.UI.ViewModel;
using Eventplanner.UI.ViewModel.Detail;
using Eventplanner.UI.View;
using Eventplanner.UI.ViewModel.List;
using Eventplanner.UI.View.List;

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


            builder.RegisterType<EventRepository>().As<IEventRepository>();
            builder.RegisterType<PersonRepository>().As<IPersonRepository>();
            builder.RegisterType<RoomRepository>().As<IRoomRepository>();
            builder.RegisterType<ScheduleRepository>().As<IScheduleRepository>();
            builder.RegisterType<AddressRepository>().As<IAddressRepository>();

            return builder.Build();
        }
    }
}
