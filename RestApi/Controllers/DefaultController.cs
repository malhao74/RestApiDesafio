using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestApi.Controllers
{
    public class DefaultController : ApiController
    {
        // GET: api/Default
        public string Get([FromUri] int[] id)
        {
            if (id.Count() == 0)
            {
                return DataLayerXML.Get();
            }
            else
            {
                return DataLayerXML.Get(id);
            }
        }

        //// POST: api/Default
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Default/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Default/5
        //public void Delete(int id)
        //{
        //}
    }
}
