using System.Threading.Tasks;

namespace Eventplanner.UI.Base
{
    public interface IListViewModel
    {
        int Id { get; }
        Task LoadAsync();
    }
}