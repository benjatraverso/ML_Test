using System;
using System.Collections.Generic;

namespace Scanner.Tests
{
    internal class Test
    {
        internal bool RunAll()
        {
            var succ = true;
            succ = TestScanner();
            return succ;
        }

        private bool TestScanner()
        {
            var bValid = true;
            //test algorithm Nivel1
            //this mutant has matches in the first element of the matrix so the eval ends quikly
            var fastMutantEval = new List<string>() { "AAAAGA", "AAGTGC", "ATATTT", "AGACGG", "GCGTCA", "TCACTG" };
            //this is the mutant's given example and the CCCC sequence is never reached for the eval ends on the second match
            var mutant = new List<string>() { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            //this mutant has a positive and negative angle diagonals matches
            var diagonalMutant = new List<string>() { "ATGCGA", "CAGTGC", "TTATTT", "AGTCGG", "GTGTCA", "TCACTG" };
            //this one is the "not mutant" given example
            var notMutant = new List<string>() { "ATGCGA", "CAGTGC", "TTATTT", "AGACGG", "GCGTCA", "TCACTG" };
            bValid = bValid && Helpers.Helper.Scan(diagonalMutant);
            bValid = bValid && Helpers.Helper.Scan(fastMutantEval);
            bValid = bValid && Helpers.Helper.Scan(mutant);
            bValid = bValid && !Helpers.Helper.Scan(notMutant);
            return bValid;
        }
    }
}