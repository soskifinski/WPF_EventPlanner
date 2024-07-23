namespace Eventplanner.Model
{
    public class RoomPlan
    {
        public int Id { get; set; }
        public Room Room { get; set; }
        public Event Event { get; set; }
    }
}
