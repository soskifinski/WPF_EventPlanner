using Eventplanner.Model;
using Eventplanner.UI.Base;

namespace Eventplanner.UI.Wrapper
{
    public class RoomViewModel : ViewModelValidationBase
    {
        public Model.Room Model { get; }
        public RoomViewModel(Model.Room model)
        {
            model = Model;
        }
        public int Id { get { return Model.Id; } }

        public string RoomNumber
        {
            get { return Model.RoomNumber; }
            set
            {
                if (this.Model.RoomNumber != value)
                {
                    this.Model.RoomNumber = value;
                    this.OnPropertyChanged(nameof(RoomNumber));
                }
            }
        }
        public int SeatsCapacity
        {
            get { return Model.SeatsCapacity; }
            set
            {
                if (this.Model.SeatsCapacity != value)
                {
                    this.Model.SeatsCapacity = value;
                    this.OnPropertyChanged(nameof(SeatsCapacity));
                }
            }
        }

        public string Description
        {
            get { return Model.Description; }
            set
            {
                if (this.Model.Description != value)
                {
                    this.Model.Description = value;
                    this.OnPropertyChanged(nameof(Description));
                }
            }
        }

        public Location Location
        {
            get { return Model.Location; }
            set
            {
                if (this.Model.Location != value)
                {
                    this.Model.Location = value;
                    this.OnPropertyChanged(nameof(Location));
                }
            }
        }
    }
}

