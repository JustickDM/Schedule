using System.Data.Entity;

namespace Schedule.Models.Entities
{
	public class DatabaseContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public DatabaseContext() : base("VkApiConnection") { }
    }
}
