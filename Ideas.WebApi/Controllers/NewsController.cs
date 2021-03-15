using Ideas.WebApi.Models;
using Ideas.WebApi.Models.Entities;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Ideas.WebApi.Controllers
{
	[Authorize]
	public class NewsController : ApiController
	{
		private IdeasContext db = new IdeasContext();

		// GET: api/News/ForCurrentUser
		[Authorize]
		[Route("api/News/ForCurrentUser")]
		public IQueryable<NewsModel> GetNewsForCurrentUser()
		{
			string userId = User.Identity.GetUserId();

			return db.News.Where(n => n.UserId == userId).Select(x => new NewsModel
			{
				Title = x.Title
			});
		}

		// GET: api/News
		[Authorize]
		public IQueryable<NewsModel> GetNews()
		{
			return db.News.Select(x => new NewsModel
			{
				Title = x.Title
			});
		}

		// GET: api/News/5
		[Authorize]
		[ResponseType(typeof(News))]
		public IHttpActionResult GetNews(int id)
		{
			News news = db.News.Find(id);
			if (news == null)
			{
				return NotFound();
			}

			return Ok(news);
		}

		// PUT: api/News/5
		[Authorize]
		[ResponseType(typeof(void))]
		public IHttpActionResult PutCourse(int id, News news)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != news.Id)
			{
				return BadRequest();
			}

			var userId = User.Identity.GetUserId();

			if (userId != news.UserId)
			{
				return StatusCode(HttpStatusCode.Conflict);
			}

			db.Entry(news).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!NewsExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/News
		[ResponseType(typeof(News))]
		[Authorize]
		public IHttpActionResult PostCourse(News news)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			string userId = User.Identity.GetUserId();
			news.UserId = userId;

			db.News.Add(news);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = news.Id }, news);
		}

		// DELETE: api/News/5
		[Authorize]
		[ResponseType(typeof(News))]
		public IHttpActionResult DeleteNews(int id)
		{
			News news = db.News.Find(id);
			if (news == null)
			{
				return NotFound();
			}

			string userId = User.Identity.GetUserId();
			if (userId != news.UserId)
			{
				return StatusCode(HttpStatusCode.Conflict);
			}

			db.News.Remove(news);
			db.SaveChanges();

			return Ok(news);
		}

		// GET: api/News/Search/keyword
		[Authorize]
		[Route("api/News/Search/{keyword}")]
		[HttpGet]
		public IQueryable<News> SearchNews(string keyword)
		{
			return db.News
				.Where(n => n.Title.Contains(keyword));
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool NewsExists(int id)
		{
			return db.News.Count(e => e.Id == id) > 0;
		}
	}
}
