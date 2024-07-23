using Eventplanner.Model;
using Eventplanner.UI.Data;
using Eventplanner.UI.ViewModel.Detail;
using FriendOrganizer.UI.ViewModel;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Eventplanner.UI.ViewModel.List
{
    public class RoomListViewModel : ViewModelBase, IListViewModel
    {
        public int Id { get; }
        private IRoomRepository _repo;
        private IEventAggregator _eventAggregator;
        private int nextNewItemId = 0;
        public ObservableCollection<RoomListItemViewModel> Rooms { get; }

        public RoomListViewModel(IRoomRepository repo,
          IEventAggregator eventAggregator)
        {
            _repo = repo;
            _eventAggregator = eventAggregator;
            Rooms = new ObservableCollection<RoomListItemViewModel>();
        }

        public async Task LoadAsync()
        {
            var rooms = await _repo.GetAllRoomsAsync();
            Rooms.Clear();
            rooms.Sort();
            foreach (var item in rooms)
            {
                Rooms.Add(new RoomListItemViewModel(item, nameof(RoomDetailViewModel),_eventAggregator));
            }
        }
    }
}
