using Eventplanner.Model;
using Eventplanner.UI.Events;
using FriendOrganizer.UI.ViewModel;
using Prism.Commands;
using Prism.Events;
using System.Globalization;
using System.Windows.Input;

namespace Eventplanner.UI.ViewModel
{
    public class RoomListItemViewModel : ViewModelBase
    {
        private string _roomNumber;
        private string _Institution;
        private IEventAggregator _eventAggregator;
        private string _detailViewModelName;
        public ICommand OpenDetailViewCommand { get; }
        public int Id { get; }

        public string RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                _roomNumber = value;
                OnPropertyChanged();
            }
        }


        public string Institution
        {
            get { return _Institution; }
            set
            {
                _Institution = value;
                OnPropertyChanged();
            }
        }

        public RoomListItemViewModel(Room _room,
          string detailViewModelName,
          IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = _room.Id;
            RoomNumber = _room.RoomNumber;
            Institution = _room.Institution.Name;
            _detailViewModelName = detailViewModelName;
            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailViewExecute);
        }
        private void OnOpenDetailViewExecute()
        {
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                  .Publish(
              new OpenDetailViewEventArgs
              {
                  Id = Id,
                  ViewModelName = _detailViewModelName
              });
        }
    }
}

