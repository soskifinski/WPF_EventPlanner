using Autofac.Features.Indexed;
using Eventplanner.UI.Base;
using Eventplanner.UI.Dashboard.ViewModel;
using Eventplanner.UI.Events;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Eventplanner.UI.Main
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;

        #region Constructor

        public MainWindowViewModel(IIndex<string, IListViewModel> listViewModelCreator, IIndex<string, IDetailViewModel> detailViewModelCreator, IEventAggregator eventAggregator)
        {

            _eventAggregator = eventAggregator;
            _listViewModelCreator = listViewModelCreator;
            _detailViewModelCreator = detailViewModelCreator;

            Dashboard = new DashboardViewModel();
           

            DetailViewModels = new ObservableCollection<IDetailViewModel>();
            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);
            OpenSingleDetailViewCommand = new DelegateCommand<Type>(OnOpenSingleDetailViewExecute);

            ListViewModels = new ObservableCollection<IListViewModel>();
            NavigateToCommand = new DelegateCommand<Type>(Navigate);

            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
              .Subscribe(AfterDetailDeleted);
            _eventAggregator.GetEvent<AfterDetailClosedEvent>()
              .Subscribe(AfterDetailClosed);
        }

        #endregion
        #region Properties
        public DashboardViewModel Dashboard {  get; set; }


        private IDetailViewModel _selectedDetailViewModel;
        private IListViewModel _selectedListViewModel;
        private IIndex<string, IDetailViewModel> _detailViewModelCreator;
        private IIndex<string, IListViewModel> _listViewModelCreator;
        private int nextNewItemId = 0;

        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }
        public ObservableCollection<IListViewModel> ListViewModels { get; }

        public IDetailViewModel SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set
            {
                _selectedDetailViewModel = value;
                OnPropertyChanged();
            }
        }


        public IListViewModel SelectedListViewModel
        {
            get => _selectedListViewModel;
            set
            {
                _selectedListViewModel = value;
                OnPropertyChanged();
            }
        }

        private object _selectedMenuItem;
        public object SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                _selectedMenuItem = value;
                OnPropertyChanged(nameof(SelectedMenuItem));
            }
        }

        #endregion

        #region Commands

        public ICommand CreateNewDetailCommand { get; }
        public ICommand NavigateToCommand { get; }
        public ICommand OpenSingleDetailViewCommand { get; }


        private void OnOpenSingleDetailViewExecute(Type viewModelType)
        {
            SelectedListViewModel = null;
            OnOpenDetailView(
              new OpenDetailViewEventArgs
              {
                  Id = -1,
                  ViewModelName = viewModelType.Name
              });
        }

        private void Navigate(Type viewModelType)
        {
            OnOpenListView(
                new OpenListViewEventArgs
                {
                    Id = -1,
                    ViewModelName = viewModelType.Name
                });

        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(
              new OpenDetailViewEventArgs
              {
                  Id = nextNewItemId--,
                  ViewModelName = viewModelType.Name
              });
        }

        public async Task LoadAsync()
        {
            SelectedListViewModel = null;
           
        }

        public ICommand OpenDashboardViewCommand { get; }


        private void OpenDashboardViewExecute(Type viewModelType)
        {
            SelectedListViewModel = null;
            OnOpenDetailView(
              new OpenDetailViewEventArgs
              {
                  Id = -1,
                  ViewModelName = viewModelType.Name
              });
        }
        #endregion

        #region Events
        #endregion










        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            var detailViewModel = DetailViewModels
              .SingleOrDefault(vm => vm.Id == args.Id
              && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel == null)
            {
                detailViewModel = _detailViewModelCreator[args.ViewModelName];
                await detailViewModel.LoadAsync(args.Id);
                DetailViewModels.Add(detailViewModel);
            }

            SelectedDetailViewModel = detailViewModel;
        }

        private async void OnOpenListView(OpenListViewEventArgs args)
        {
            var listViewModel = ListViewModels
               .SingleOrDefault(vm => vm.Id == args.Id
              && vm.GetType().Name == args.ViewModelName);

            if (listViewModel == null)
            {
                listViewModel = _listViewModelCreator[args.ViewModelName];
                await listViewModel.LoadAsync();
                ListViewModels.Add(listViewModel);
            }
            SelectedListViewModel = listViewModel;

        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void AfterDetailClosed(AfterDetailClosedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void RemoveDetailViewModel(int id, string viewModelName)
        {
            var detailViewModel = DetailViewModels
                   .SingleOrDefault(vm => vm.Id == id
                   && vm.GetType().Name == viewModelName);
            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }
        }
    }
}
