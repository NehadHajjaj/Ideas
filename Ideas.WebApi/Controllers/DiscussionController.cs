using Ideas.WebApi.Models;
using Ideas.WebApi.Models.Entities;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Ideas.WebApi.Controllers
{
	[Authorize]
	public class DiscussionController : ApiController
	{
		private IdeasContext db = new IdeasContext();

		// GET: api/Discussions/ForCurrentUser
		[Authorize]
		[Route("api/Discussions/ForCurrentUser")]
		public IQueryable<Discussion> GetCoursesForCurrentUser()
		{
			string userId = User.Identity.GetUserId();

			return db.Discussions.Where(d => d.UserId == userId);
		}

		// GET: api/Discussions
		[Authorize]
		public IQueryable<Discussion> GetCourses()
		{
			return db.Discussions;
		}

		// GET: api/Discussions/5
		[Authorize]
		[ResponseType(typeof(Discussion))]
		public IHttpActionResult GetCourse(int id)
		{
			Discussion discussion = db.Discussions.Find(id);
			if (discussion == null)
			{
				return NotFound();
			}

			return Ok(discussion);
		}

		// POST: api/Discussions
		[ResponseType(typeof(Discussion))]
		[Authorize]
		public IHttpActionResult PostDiscussion(Discussion discussion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			string userId = User.Identity.GetUserId();
			discussion.UserId = userId;

			db.Discussions.Add(discussion);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = discussion.Id }, discussion);
		}

		// DELETE: api/Discussion/5
		[Authorize]
		[ResponseType(typeof(Discussion))]
		public IHttpActionResult DeleteDiscussion(int id)
		{
			Discussion discussion = db.Discussions.Find(id);
			if (discussion == null)
			{
				return NotFound();
			}

			string userId = User.Identity.GetUserId();
			if (userId != discussion.UserId)
			{
				return StatusCode(HttpStatusCode.Conflict);
			}

			db.Discussions.Remove(discussion);
			db.SaveChanges();

			return Ok(discussion);
		}

		// GET: api/Discussions/Search/keyword
		[Authorize]
		[Route("api/Discussions/Search/{keyword}")]
		[HttpGet]
		public IQueryable<Discussion> SearchDiscussions(string keyword)
		{
			return db.Discussions
				.Where(d => d.DiscussionText.Contains(keyword));
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

	}
}
