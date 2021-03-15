namespace Ideas.WebApi.Models.Entities
{
	public class Discussion
	{
		public int Id { get; set; }
		public string DiscussionText { get; set; }
		public int Type { get; set; }
		public string UserId { get; set; }
	}

}