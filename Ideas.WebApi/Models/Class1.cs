//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Ideas.WebApi.Models
//{
//	public class Idea
//	{
//		public int Id { get; set; }
//		public string Title { get; set; }
//		public string Description { get; set; }
//		public int IdeaType { get; set; }
//		public int IdeaState { get; set; }
//		public int ScientificClassification { get; set; }
//		public string UserId { get; set; }
//		public string UserName { get; set; }
//	}


//	public class AddIdeaViewModel
//	{
//		ApiService _apiServices = new ApiService();
//		public string Title { get; set; }
//		public string Description { get; set; }
//		public int IdeaType { get; set; }
//		public int IdeaState { get; set; }
//		public int ScientificClassification { get; set; }

//		public ICommand AddCommand
//		{
//			get
//			{
//				return new Command(async () =>
//				{
//					var idea = new Idea
//					{
//						Title = Title,
//						Description = Description,
//						IdeaType = IdeaType,
//						IdeaState = IdeaState,
//						ScientificClassification = ScientificClassification
//					};
//					await _apiServices.PostIdeaAsync(idea, Settings.AccessToken);
//				});
//			}
//		}
//	}
//}