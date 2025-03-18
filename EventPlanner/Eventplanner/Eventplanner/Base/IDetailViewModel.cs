using System.Threading.Tasks;

namespace Eventplanner.UI.Base
{
    public interface IDetailViewModel
    {
        int Id { get;}
        Task LoadAsync(int id);
    }
}