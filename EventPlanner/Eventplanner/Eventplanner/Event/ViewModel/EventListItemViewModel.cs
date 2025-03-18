using Eventplanner.Model;
using Eventplanner.UI.Base;
using Eventplanner.UI.Events;
using Prism.Commands;
using Prism.Events;
using System.Windows.Input;

namespace Eventplanner.UI.Event.ViewModel
{
    public class EventListItemViewModel : ViewModelBase
    {
        private string _title;
        private string _subtitle;
        private string _status;
        private string _date;
        private string _dateTimeTo;
        private string _dateTimeFrom;

        private IEventAggregator _eventAggregator;
        private string _detailViewModelName;
        public ICommand OpenDetailViewCommand { get; }
        public int Id { get; }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }


        public string SubTitle
        {
            get { return _subtitle; }
            set
            {
                _subtitle = value;
                OnPropertyChanged();
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public string DateTimeTo
        {
            get { return _dateTimeTo; }
            set
            {
                _dateTimeTo = value;
                OnPropertyChanged();
            }
        }

        public string DateTimeFrom
        {
            get { return _dateTimeFrom; }
            set
            {
                _dateTimeFrom = value;
                OnPropertyChanged();
            }
        }

        public EventListItemViewModel(Model.Event _event,
          string detailViewModelName,
          IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = _event.Id;
            Title = _event.Title;
            SubTitle = _event.SubTitle;
            Status = _event.Status.ToString();
            Date = _event.DateTimeFrom.ToString("dd.MM.yyyy") + "-" + _event.DateTimeTo.ToString("dd.MM.yyyy");
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

