using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace WetherAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // Get api/Values/Clima/
        [HttpGet]
        [AcceptVerbs("Get")]
        [Route("api/Values/Clima/{dia}")]
        public dynamic Clima(int dia)
        {
            Console.WriteLine("clima");
            var result = new WeatherCalculator.dayData
            {
                dia = dia,
                clima = WeatherCalculator.GetWeather(dia)
            };

            return JsonConvert.SerializeObject(result);
        }

        // Get api/Values/Report/
        [HttpGet]
        [AcceptVerbs("Get")]
        [Route("api/Values/Report")]
        public dynamic Report()
        {
            Console.WriteLine("Report");
            return WeatherCalculator.GetNextTenYearsReport();
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
