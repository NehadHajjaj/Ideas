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
	public class EventController : ApiController
	{
		private IdeasContext db = new IdeasContext();

		// GET: api/Events/ForCurrentUser
		[Authorize]
		[Route("api/Events/ForCurrentUser")]
		public IQueryable<Event> GetEventsForCurrentUser()
		{
			string userId = User.Identity.GetUserId();

			return db.Events.Where(ev => ev.UserId == userId);
		}

		// GET: api/Events
		[Authorize]
		public IQueryable<Event> GetEvents()
		{
			return db.Events;
		}

		// GET: api/Events/5
		[Authorize]
		[ResponseType(typeof(Event))]
		public IHttpActionResult GetEvent(int id)
		{
			Event ev = db.Events.Find(id);
			if (ev == null)
			{
				return NotFound();
			}

			return Ok(ev);
		}

		// PUT: api/Events/5
		[Authorize]
		[ResponseType(typeof(void))]
		public IHttpActionResult PutEvent(int id, Event ev)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != ev.Id)
			{
				return BadRequest();
			}

			var userId = User.Identity.GetUserId();

			if (userId != ev.UserId)
			{
				return StatusCode(HttpStatusCode.Conflict);
			}

			db.Entry(ev).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!EventExists(id))
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

		// POST: api/Events
		[ResponseType(typeof(Event))]
		[Authorize]
		public IHttpActionResult PostEvent(Event ev)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			string userId = User.Identity.GetUserId();
			ev.UserId = userId;

			db.Events.Add(ev);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = ev.Id }, ev);
		}

		// DELETE: api/Events/5
		[Authorize]
		[ResponseType(typeof(Event))]
		public IHttpActionResult DeleteEvent(int id)
		{
			Event ev = db.Events.Find(id);
			if (ev == null)
			{
				return NotFound();
			}

			string userId = User.Identity.GetUserId();
			if (userId != ev.UserId)
			{
				return StatusCode(HttpStatusCode.Conflict);
			}

			db.Events.Remove(ev);
			db.SaveChanges();

			return Ok(ev);
		}

		// GET: api/Events/Search/keyword
		[Authorize]
		[Route("api/Events/Search/{keyword}")]
		[HttpGet]
		public IQueryable<Event> SearchEvents(string keyword)
		{
			return db.Events
				.Where(ev => ev.Title.Contains(keyword));
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool EventExists(int id)
		{
			return db.Events.Count(e => e.Id == id) > 0;
		}
	}
}
