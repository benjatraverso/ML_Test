namespace Scanner.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class DB
    {
        internal enum Kind
        {
            human = 0,
            mutant
        }

        internal static Dictionary<string, Kind> db = new Dictionary<string, Kind>();

        internal static void StoreResult(string dna, Kind result)
        {
            var value = new Dictionary<string, Kind>();
            if (!db.TryGetValue(dna, out _))
            {
                db.TryAdd(dna, result);
            }
        }

        internal static DBStats GetStats()
        {
            var humans = db.Values.Where(x => x == Kind.human).Count();
            var mutants = db.Values.Where(x => x == Kind.mutant).Count();
            var res = new DBStats(mutants, humans);
            return res;
        }
    }

    public class DBStats
    {
        public DBStats(int mut, int hum)
        {
            count_mutant_dna = mut;
            count_human_dna = hum;
            if (hum == 0 && mut == 0)
            {
                ratio = 0;
            }
            if (hum == 0 && mut != 0)
            {
                ratio = 1;
            }
            else
            {
                ratio = hum == 0 ? 0 : mut / hum;
            }
        }

        public int count_mutant_dna { get; set; }
        public int count_human_dna { get; set; }
        public decimal ratio { get; set; }
    }
}
