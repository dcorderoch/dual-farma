using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dual_farma.Controllers
{
    public class CompleteController : ApiController
    {
        [HttpGet]
        [Route("api/complete")]
        public string Get()
        {
            return "complete";
        }

        [HttpGet]
        [Route("api/complete/{arg}")]
        public String GetArgAfter([FromUri] string arg)
        {
            String tmp = "your argument was: ";
            tmp = String.Concat(tmp, arg);
            return tmp;
        }

        [HttpGet]
        [Route("api/complete/post/{pArg}")]
        public int GetNumber([FromUri] int pArg)
        {
            return 200 + pArg;
        }
    }
}
