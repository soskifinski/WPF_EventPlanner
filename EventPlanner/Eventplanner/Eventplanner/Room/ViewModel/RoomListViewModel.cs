using Eventplanner.Interfaces;
using Eventplanner.UI.Base;
using Eventplanner.UI.Events;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Eventplanner.UI.Room.ViewModel
{
    public class RoomListViewModel : ViewModelBase, IListViewModel
    {
        public int Id { get; }
        private IRoomRepository _repo;
        private IEventAggregator _eventAggregator;
        public ObservableCollection<RoomListItemViewModel> Rooms { get; }

        public RoomListViewModel(IRoomRepository repo,
          IEventAggregator eventAggregator)
        {
            _repo = repo;
            _eventAggregator = eventAggregator;
            Rooms = new ObservableCollection<RoomListItemViewModel>();
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
        }

        public async Task LoadAsync()
        {
            var rooms = await _repo.GetAllRoomsAsync();
            Rooms.Clear();
            foreach (var item in rooms)
            {
                Rooms.Add(new RoomListItemViewModel(item, nameof(RoomDetailViewModel),_eventAggregator));
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            var room = Rooms.SingleOrDefault(l => l.Id == args.Id);
            Model.Room thisRoom = _repo.FindById(args.Id);
            if (room == null)
            {
                room = new RoomListItemViewModel(thisRoom,
                  args.ViewModelName,
                  _eventAggregator);
            }
            else
            {
                room.RoomNumber = args.DisplayMember;
            }
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            AfterDetailDeleted(Rooms, args);
        }
        private void AfterDetailDeleted(ObservableCollection<RoomListItemViewModel> items,
          AfterDetailDeletedEventArgs args)
        {
            var item = items.SingleOrDefault(r => r.Id == args.Id);
            if (item != null)
            {
                items.Remove(item);
            }
        }
    }
}
