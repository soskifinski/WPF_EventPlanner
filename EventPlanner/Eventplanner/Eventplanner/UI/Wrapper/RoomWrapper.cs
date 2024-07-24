using Eventplanner.Model;

namespace Eventplanner.UI.Wrapper
{
    public class RoomWrapper : ModelWrapper<Room>
    {
        public RoomWrapper(Room model) : base(model)
        {
        }
        public int Id { get { return Model.Id; } }

        public string RoomNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public int SeatsCapacity
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public string Description
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
