using Eventplanner.Model;
using Eventplanner.UI.Base;

namespace Eventplanner.UI.Wrapper
{
    public class LocationViewModel : ViewModelValidationBase
    {
        private Location Model { get; }

        public LocationViewModel(Location model)
        {
            Model = model;
        }

        public int Id { get { return Model.Id; } }


        public string Name
        {
            get => Model.Name;

            set
            {
                if (Model.Name != value)
                {
                    Model.Name = value;
                    this.OnPropertyChanged(nameof(Name));
                }
            }
        }
    }
}
