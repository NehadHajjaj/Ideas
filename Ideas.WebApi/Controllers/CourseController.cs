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
	public class CourseController : ApiController
	{
		private IdeasContext db = new IdeasContext();

		// GET: api/Courses/ForCurrentUser
		[Authorize]
		[Route("api/Courses/ForCurrentUser")]
		public IQueryable<Course> GetCoursesForCurrentUser()
		{
			string userId = User.Identity.GetUserId();

			return db.Courses.Where(course => course.UserId == userId);
		}

		// GET: api/Courses
		[Authorize]
		public IQueryable<Course> GetCourses()
		{
			return db.Courses;
		}

		// GET: api/Courses/5
		[Authorize]
		[ResponseType(typeof(Course))]
		public IHttpActionResult GetCourse(int id)
		{
			Course course = db.Courses.Find(id);
			if (course == null)
			{
				return NotFound();
			}

			return Ok(course);
		}

		// PUT: api/Courses/5
		[Authorize]
		[ResponseType(typeof(void))]
		public IHttpActionResult PutCourse(int id, Course course)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != course.Id)
			{
				return BadRequest();
			}

			var userId = User.Identity.GetUserId();

			if (userId != course.UserId)
			{
				return StatusCode(HttpStatusCode.Conflict);
			}

			db.Entry(course).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CourseExists(id))
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

		// POST: api/Courses
		[ResponseType(typeof(Course))]
		[Authorize]
		public IHttpActionResult PostCourse(Course course)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			string userId = User.Identity.GetUserId();
			course.UserId = userId;

			db.Courses.Add(course);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = course.Id }, course);
		}

		// DELETE: api/Course/5
		[Authorize]
		[ResponseType(typeof(Course))]
		public IHttpActionResult DeleteCourse(int id)
		{
			Course course = db.Courses.Find(id);
			if (course == null)
			{
				return NotFound();
			}

			string userId = User.Identity.GetUserId();
			if (userId != course.UserId)
			{
				return StatusCode(HttpStatusCode.Conflict);
			}

			db.Courses.Remove(course);
			db.SaveChanges();

			return Ok(course);
		}

		// GET: api/Courses/Search/keyword
		[Authorize]
		[Route("api/Courses/Search/{keyword}")]
		[HttpGet]
		public IQueryable<Course> SearchCourses(string keyword)
		{
			return db.Courses
				.Where(course => course.Title.Contains(keyword));
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool CourseExists(int id)
		{
			return db.Courses.Count(e => e.Id == id) > 0;
		}
	}
}
