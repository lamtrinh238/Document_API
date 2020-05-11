using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Document_API.DAL;
using Document_API.Filters;
using Document_API.Models;
using Document_API.Utilities;

namespace Document_API.Controllers
{
    public class DocumentsController : ApiController
    {
        private DocumentContext db = new DocumentContext();

        // GET: api/Documents
        [JwtAuthentication(EnumRole.User, EnumRole.Contributor)]
        public IHttpActionResult GetDocuments()
        {
            List<Document> document = db.Documents.ToList();
            if (document == null)
            {
                return NotFound();
            }

            return Ok(document);
        }

        // GET: api/Documents/5
        [ResponseType(typeof(Document))]
        [JwtAuthentication(EnumRole.User, EnumRole.Contributor)]
        public IHttpActionResult GetDocument(int id)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return NotFound();
            }

            return Ok(document);
        }

        // PUT: api/Documents/5
        [ResponseType(typeof(void))]
        [JwtAuthentication(EnumRole.Contributor)]
        public IHttpActionResult PutDocument(int id, Document document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != document.ID)
            {
                return BadRequest();
            }

            db.Entry(document).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(id))
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

        // POST: api/Documents
        [ResponseType(typeof(Document))]
        [JwtAuthentication(EnumRole.Contributor)]
        public IHttpActionResult PostDocument(Document document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Documents.Add(document);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = document.ID }, document);
        }

        // DELETE: api/Documents/5
        [ResponseType(typeof(Document))]
        [JwtAuthentication(EnumRole.Contributor)]
        public IHttpActionResult DeleteDocument(int id)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return NotFound();
            }

            db.Documents.Remove(document);
            db.SaveChanges();

            return Ok(document);
        }

        [HttpGet]
        [ActionName("DownloadCover")]
        public HttpResponseMessage DownloadCover(int id)
        {
            HttpResponseMessage result = null;
            try
            {
                var document = db.Documents.Find(id);

                if (document == null)
                {
                    result = Request.CreateResponse(HttpStatusCode.Gone);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.OK);
                    result.Content = new ByteArrayContent(document.Cover);
                    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = document.Title + "_Cover." + document.CoverFileExtension;
                }

                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.Gone);
            }
        }

        [HttpGet]
        [ActionName("DownloadContent")]
        public HttpResponseMessage DownloadContent(int id)
        {
            HttpResponseMessage result = null;
            try
            {
                var document = db.Documents.Find(id);

                if (document == null)
                {
                    result = Request.CreateResponse(HttpStatusCode.Gone);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.OK);
                    result.Content = new ByteArrayContent(document.Cover);
                    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = document.Title + "_Content." + document.ContentFileExtension;
                }

                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.Gone);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DocumentExists(int id)
        {
            return db.Documents.Count(e => e.ID == id) > 0;
        }
    }
}