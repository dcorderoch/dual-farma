using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.UI;
using Newtonsoft.Json.Linq;

using dual_farma.BLL;

namespace dual_farma.Controllers
{
    public class AccountsController : ApiController
    {
        // POST api/accounts/createuser
        public string CreateUser([FromBody] string json)
        {
            string retVal = "";
            JToken theJson = JToken.Parse(json);
            string uID= theJson.Value<string>("uID");
            string uPass = theJson.Value<string>("uPass");
            string uName = theJson.Value<string>("uName");
            string uLname = theJson.Value<string>("uLname");
            string uLname2 = theJson.Value<string>("uLname2");
            string uEmail = theJson.Value<string>("uEmail");
            string uCompany = theJson.Value<string>("uCompany");
            string uRole = theJson.Value<string>("uRole");

            var AM = new Account_Manager();
            int retStatus = AM.CreateUser(uID,uPass,uName,uLname,uLname2,uEmail,uCompany,uRole);

            switch (retStatus)
            {
                case Constants.USER_CREATED:
                    retVal = "successfully registered";
                    break;
                case Constants.ALREADY_REGISTERED:
                    retVal = "there is already a user registered with that ID";
                    break;
            }
            JavaScriptSerializer responseMaker = new JavaScriptSerializer();
            return responseMaker.Serialize(retVal);

        }
    }
}
