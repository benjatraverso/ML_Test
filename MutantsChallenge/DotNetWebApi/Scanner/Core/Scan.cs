namespace Scanner
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Scan
    {
        internal static bool IsMutant(List<string> dna)
        {
            var match = 0;
            var row = 0;
            foreach (var frag in dna)
            {
                for (var i = 0; i < frag.Length; i++)
                {
                    match += Evaluate(frag.ToCharArray()[i], dna.ToArray(), i, row);
                    if (match >= 2)
                    {
                        return true;
                    }
                }

                row++;
            }

            return false;
        }

        private static int Evaluate(char c, string[] dna, int x, int y)
        {
            var match = 0;

            // check if we can have an horizontal secuence
            if (dna[0].Length - x > 3)
            {
                if (IsSec(dna, c, x, y, -1, 0, 1))
                {
                    match++;
                }
            }

            // check if we can have a perpendicular secuence
            if (dna.Length - y > 3)
            {
                if (IsSec(dna, c, x, y, 0, -1, 1))
                {
                    match++;
                }
            }

            // check if we can have an negative angle diagonal secuence
            if (dna[0].Length - x > 3 && dna.Length - y > 3)
            {
                if (IsSec(dna, c, x, y, -1, -1, 1))
                {
                    match++;
                }
            }


            if (match < 2 && y > 3 && dna[0].Length - x > 3)
            {
                // we might have a positive angle diagonal secuence
                if (IsSec(dna, c, x, y, -1, 1, 1))
                {
                    match++;
                }
            }

            return match;
        }

        private static bool IsSec(string[] dna, char c, int x, int y, int dirX, int dirY, int rep)
        {
            var nextChar = GetChar(dna, x - dirX, y - dirY);
            if (c != nextChar)
            {
                // if current is not equal to next position we don't even bother
                return false;
            }
            else
            {
                // they are equal so there might be a secuence, check next and so on upt to 4 times
                if (rep >= 3 || IsSec(dna, nextChar, x - dirX, y - dirY, dirX, dirY, ++rep))
                {
                    return true;
                }
                else
                {
                    // we checked less than 4 times and the secuence returned false so, there is no secuence.
                    return false;
                }
            }
        }

        private static char GetChar(string[] dna, int x, int y)
        {
            var arrDna = dna.ToArray();
            var word = arrDna[y];
            var arrWord = word.ToArray();
            return arrWord[x];
        }
    }
}
