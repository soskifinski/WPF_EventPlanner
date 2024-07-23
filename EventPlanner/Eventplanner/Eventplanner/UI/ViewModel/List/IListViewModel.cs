using System.Threading.Tasks;

namespace Eventplanner.UI.ViewModel.List
{
    public interface IListViewModel
    {
        int Id { get; }
        Task LoadAsync();
    }
}