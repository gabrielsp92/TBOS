using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TropicalBears.Model.DataBase;
using TropicalBears.Model.DataBase.Model;

namespace TropicalBears.API.Controllers
{
    public class UserController : ApiController
    {
        public IList<User> Users { get; set; }

        public UserController()
        {
            Users = DbConfig.Instance.UserRepository.FindAll();
        }
        
        // GET: api/User
        public IList<User> Get()
        {
            return this.Users;
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }


        // POST: api/User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
