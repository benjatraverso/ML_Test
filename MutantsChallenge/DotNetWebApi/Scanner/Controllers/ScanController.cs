namespace Scanner.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Scanner.Models;
    using System.Collections.Generic;

    public class ScanController : Controller
    {
        [HttpPost]
        [AcceptVerbs("Post")]
        [Route("api/Scan/Mutant")]
        public dynamic Mutant([FromBody] Dictionary<string, object> dna)
        {
            var cadenas = DNADataParser.ParseDNAData(dna);
            return Scan.IsMutant(cadenas);
        }
    }
}
