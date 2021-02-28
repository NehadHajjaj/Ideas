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
	public class WorkshopController : ApiController
	{
		private IdeasContext db = new IdeasContext();

		// GET: api/Workshops/ForCurrentUser
		[Authorize]
		[Route("api/Workshops/ForCurrentUser")]
		public IQueryable<Workshop> GetWorkshopsForCurrentUser()
		{
			string userId = User.Identity.GetUserId();

			return db.Workshops.Where(w => w.UserId == userId);
		}

		// GET: api/Workshops
		[Authorize]
		public IQueryable<Workshop> GetWorkshops()
		{
			return db.Workshops;
		}

		// GET: api/Workshops/5
		[Authorize]
		[ResponseType(typeof(Workshop))]
		public IHttpActionResult GetWorkshop(int id)
		{
			Workshop workshop = db.Workshops.Find(id);
			if (workshop == null)
			{
				return NotFound();
			}

			return Ok(workshop);
		}

		// PUT: api/Workshops/5
		[Authorize]
		[ResponseType(typeof(void))]
		public IHttpActionResult PutWorkshop(int id, Workshop workshop)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != workshop.Id)
			{
				return BadRequest();
			}

			var userId = User.Identity.GetUserId();

			if (userId != workshop.UserId)
			{
				return StatusCode(HttpStatusCode.Conflict);
			}

			db.Entry(workshop).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!WorkshopExists(id))
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

		// POST: api/Workshops
		[ResponseType(typeof(Workshop))]
		[Authorize]
		public IHttpActionResult PostWorkshop(Workshop workshop)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			string userId = User.Identity.GetUserId();
			workshop.UserId = userId;

			db.Workshops.Add(workshop);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = workshop.Id }, workshop);
		}

		// DELETE: api/Workshops/5
		[Authorize]
		[ResponseType(typeof(Workshop))]
		public IHttpActionResult DeleteWorkshop(int id)
		{
			Workshop workshop = db.Workshops.Find(id);
			if (workshop == null)
			{
				return NotFound();
			}

			string userId = User.Identity.GetUserId();
			if (userId != workshop.UserId)
			{
				return StatusCode(HttpStatusCode.Conflict);
			}

			db.Workshops.Remove(workshop);
			db.SaveChanges();

			return Ok(workshop);
		}

		// GET: api/Workshops/Search/keyword
		[Authorize]
		[Route("api/Ideas/Search/{keyword}")]
		[HttpGet]
		public IQueryable<Workshop> SearchWorkshops(string keyword)
		{
			return db.Workshops
				.Where(w => w.Title.Contains(keyword));
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool WorkshopExists(int id)
		{
			return db.Workshops.Count(e => e.Id == id) > 0;
		}
	}
}
