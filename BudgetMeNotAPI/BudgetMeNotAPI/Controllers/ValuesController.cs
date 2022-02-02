using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BudgetMeNotAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            var retvalue = new { Key1 = "value1", Key2 = "value2" };
            HttpResponseMessage httpReturnResponse = Request.CreateResponse(HttpStatusCode.OK, retvalue);
            return httpReturnResponse;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
