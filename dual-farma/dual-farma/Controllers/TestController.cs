using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
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
        // GET api/test/cheat
        public string Cheat([FromBody]string json)
        {
            JToken theJson = JToken.Parse(json);

            return theJson.Value<string>("id") + theJson.Value<string>("ok");
        }
        // POST api/test/post
        public string Post([FromBody] string test)
        {
            // CURL command is:
            // curl -H "Content-Type: application/json" -d "'id=algo&ok=cosa'" -X POST http://localhost:7506/api/test/post
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
        //[HttpGet]
        //[Route("api/test/json")]
        //public string Json([FromBody] int json)
        //{
        //    // CURL command is:
        //    // curl -H "Content-Type: application/json" -d "'{\"id\":\"vida\",\"ok\":\"muerte\"}'" -X POST http://localhost:7506/api/test/json
        //    //body is '{\"id\":\"vida\",\"ok\":\"muerte\"} (\ used to escape character, to really send it from CURL)
        //    //JToken theJson = JToken.Parse(json);

        //    //return theJson.Value<string>("id") + theJson.Value<string>("ok");
        //    return json.ToString();
        //}
        //[HttpPost]
        //[Route("api/test/json")]
        [HttpPost]
        public string Json([FromBody] string json)
        {
            // CURL command is:
            // curl -H "Content-Type: application/json" -d "'{\"id\":\"vida\",\"ok\":\"muerte\"}'" -X POST http://localhost:7506/api/test/json
            //body is '{\"id\":\"vida\",\"ok\":\"muerte\"} (\ used to escape character, to really send it from CURL)
            JToken theJson = JToken.Parse(json);

            return theJson.Value<string>("id") + theJson.Value<string>("ok");
        }
        // POST api/test/res
        public string Res([FromBody] string jsonstr)
        {
            return jsonstr;
        }
    }
}
