using System.Threading.Tasks;

namespace Eventplanner.UI.ViewModel.Detail
{
    public interface IDetailViewModel
    {
        bool HasChanges { get; set; }
        int Id { get; }
        Task LoadAsync(int id);
    }
}