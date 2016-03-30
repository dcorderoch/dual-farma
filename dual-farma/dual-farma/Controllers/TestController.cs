using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using dual_farma.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dual_farma.Controllers
{
    public class TestController : ApiController
    {
        // GET api/test
        public string Get()
        {
            return "ok, this is interesting";
        }
        // POST api/test/post
        public string Post([FromBody] string test)
        {
            //body is id=something&ok=something
            var parts = test.Split('&');
            if (parts.Length != 2)
            {
                return "0";
            }
            string id = parts[0].Remove(0, 3);
            string ok = parts[1].Remove(0, 3);
            return "ok, haha!: " + id + " nor can: " + ok;
        }
        // POST api/test/json
        public string Json([FromBody] string json)
        {
            //body is '{\"id\":\"vida\",\"ok\":\"muerte\"} (\ used to escape character, to really send it from CURL)
            JToken theJson = JToken.Parse(json);

            return theJson.Value<string>("id") + theJson.Value<string>("ok");
        }
    }
}
