using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Document_API.DAL;
using Document_API.Filters;
using Document_API.Models;
using Document_API.Utilities;

namespace Document_API.Controllers
{
    public class CategoriesController : ApiController
    {
        private DocumentContext db = new DocumentContext();

        // GET: api/Categories
        [JwtAuthentication(EnumRole.User,EnumRole.Contributor)]
        public IHttpActionResult GetCategory()
        {
            List<Category> category = db.Categorys.ToList();
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Category))]
        [JwtAuthentication(EnumRole.User, EnumRole.Contributor)]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        [JwtAuthentication(EnumRole.Admin)]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.ID)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        [JwtAuthentication( EnumRole.Admin)]
        public IHttpActionResult PostCategory(Category category)
        {
            var httpRequest = HttpContext.Current.Request;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categorys.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.ID }, category);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Category))]
        [JwtAuthentication(EnumRole.Admin)]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categorys.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categorys.Count(e => e.ID == id) > 0;
        }
    }
}