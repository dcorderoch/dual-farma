using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dual_farma.BLL;

namespace dual_farma.Controllers
{
    public class UserMgmController : ApiController
    {
        // PUT api/usermgm/update
        public string Update([FromBody]string userId, [FromBody]string password, [FromBody]string name, [FromBody]string lastName1, [FromBody]string lastName2, [FromBody]string email, [FromBody]string company, [FromBody]string roleId)
        {
            var ACM = new Account_Manager();
            if (ACM.UpdateUser(userId, password, name, lastName1, lastName2, email, company, roleId) ==
                Constants.USER_NOT_UPDATED)
            {
                return "{User:notupdated}";
            }
            return "{User:updated}";

        }
        // DELETE api/usermgm/delete
        public string Delete([FromBody]string uID)
        {
            var ACM = new Account_Manager();
            if (ACM.DeleteUser(uID) == Constants.USER_DELETED)
            {
                return "{user:deleted}";
            }
            return "{user:still-undeleted}";
        }
    }
}
