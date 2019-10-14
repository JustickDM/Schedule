using System;

using Schedule.Models.Entities;

namespace Schedule.Models
{
	public class EventVM
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Object { get; set; }
        public int GroupId { get; set; }
        public DateTime Date { get; set; }

        public static implicit operator EventVM(Event dbEvent)
        {
            return new EventVM()
            {
                Id = dbEvent.Id,
                Type = dbEvent.Type,
                GroupId = dbEvent.GroupId,
                Object = dbEvent.Object,
                Date = new DateTime(dbEvent.Date)
            };
        }
    }
}