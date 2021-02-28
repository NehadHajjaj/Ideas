using System.ComponentModel.DataAnnotations.Schema;

namespace Ideas.WebApi.Models.Entities
{
	public class Idea
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int IdeaType { get; set; }
            public int IdeaState { get; set; }
            public int ScientificClassification { get; set; }
        
            public string UserId { get; set; }
            [NotMapped]
            public string UserName { get; set; }
        }
    
}