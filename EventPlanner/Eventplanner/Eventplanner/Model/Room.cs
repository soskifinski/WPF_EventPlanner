namespace Eventplanner.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public Institution Institution { get; set; }
        public int SeatsCapacity { get; set; }
        public string Description { get; set; }

        public Room()
        {
        }
    }

    public class NullRoom : Room
    {
        public new int? Id { get { return null; } }
    }
}
