using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Document_API.DAL;

namespace Document_API.Controllers
{
    public class AuthenController : ApiController
    {
        private DocumentContext db = new DocumentContext();

        [AllowAnonymous]
        [HttpPost]
        public string Login([FromBody]Models.User user)
        {
            if (CheckUser(user.Username, user.Password, out Models.User loginUser))
            {
                return JwtManager.GenerateToken(loginUser);
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        private bool CheckUser(string username, string password, out Models.User loginUser)
        {
            loginUser = db.Users.Where(c=>c.Username == username && c.Password == password).FirstOrDefault();
            if (loginUser != null && loginUser.Username != null)
            {
                return true;
            }
            return false;
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
