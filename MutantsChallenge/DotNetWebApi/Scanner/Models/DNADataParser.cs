namespace Scanner.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public static class DNADataParser
    {
        public static List<string> ParseDNAData(Dictionary<string, object> data)
        {
            return JsonConvert.DeserializeObject<List<string>>(data["dna"].ToString());
        }
    }
}
