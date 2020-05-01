namespace Scanner.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
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
            var cadenas = DNADataParser.ParseDNAData(dna);
            var response = new HttpResponseMessage();
            if (Scan.IsMutant(cadenas))
            {
                response.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.StatusCode = HttpStatusCode.Forbidden;
            }

            return response;
        }
    }
}
