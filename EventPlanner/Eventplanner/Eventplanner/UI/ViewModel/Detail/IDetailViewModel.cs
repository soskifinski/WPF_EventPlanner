using System.Threading.Tasks;

namespace Eventplanner.UI.ViewModel.Detail
{
    public interface IDetailViewModel
    {
        int Id { get;}
        Task LoadAsync(int id);
    }
}