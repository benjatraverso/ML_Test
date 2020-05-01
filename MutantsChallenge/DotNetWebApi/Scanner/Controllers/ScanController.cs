namespace Scanner.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Scanner.Models;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;

    public class ScanController : Controller
    {
        [HttpPost]
        [AcceptVerbs("Post")]
        [Route("api/Scan/Mutant")]
        public HttpResponseMessage Mutant([FromBody] Dictionary<string, object> dna)
        {
            var cadenas = JsonConvert.DeserializeObject<List<string>>(dna["dna"].ToString());
            var response = new HttpResponseMessage();
            DB.Kind type;
            if (Scan.IsMutant(cadenas))
            {
                response.StatusCode = HttpStatusCode.OK;
                type = DB.Kind.mutant;
            }
            else
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                type = DB.Kind.human;
            }

            DB.StoreResult(string.Join(",", cadenas), type);
            return response;
        }

        [HttpPost]
        [AcceptVerbs("Get")]
        [Route("api/Scan/Stats")]
        public dynamic Stats()
        {
            return JsonConvert.SerializeObject(DB.GetStats());
        }
    }
}
