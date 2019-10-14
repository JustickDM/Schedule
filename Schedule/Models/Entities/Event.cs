namespace Schedule.Models.Entities
{
	public class Event
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Object { get; set; }
        public int GroupId { get; set; }
        public long Date { get; set; }
    }
}
