using Ideas.WebApi.Models.Entities;
using System.Data.Entity;

namespace Ideas.WebApi.Models
{
	public class IdeasContext : DbContext
	{
		// You can add custom code to this file. Changes will not be overwritten.
		// 
		// If you want Entity Framework to drop and regenerate your database
		// automatically whenever you change your model schema, please use data migrations.
		// For more information refer to the documentation:
		// http://msdn.microsoft.com/en-us/data/jj591621.aspx

		public IdeasContext() : base("name=IdeasDatabaseConnectionNew")
		{
			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<IdeasContext>());
		}

		public DbSet<Idea> Ideas { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Workshop> Workshops { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<News> News { get; set; }

	}
}