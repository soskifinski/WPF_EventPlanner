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
    }
}
