using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scanner.Models
{
    public static class DNADataParser
    {
        public static List<string> ParseDNAData(Dictionary<string, object> data)
        {
            var fragments = new List<string>();
            var arrayFragments = JsonConvert.DeserializeObject<Dictionary<string, object>>(data["dna"].ToString()).ToList();
            return fragments;
        }
    }
}
