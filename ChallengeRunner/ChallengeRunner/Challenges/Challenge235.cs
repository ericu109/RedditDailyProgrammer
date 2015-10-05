using System;
using System.Collections.Generic;
using System.Linq;

namespace ChallengeRunner.Challenges
{
   /// <summary>
   /// https://www.reddit.com/r/dailyprogrammer/comments/3nkanm/20151005_challenge_235_easy_ruthaaron_pairs/
   /// </summary>
   class Challenge235 : ChallengeBase
   {
      protected override void init()
      {
         input = string.Join(Environment.NewLine,
            "4",
            "(5, 6)",
            "(2107, 2108)",
            "(492, 493)",
            "(128, 129)");


         expectedOutput =
            string.Join(Environment.NewLine,
            "(5,6) VALID",
            "(2107,2108) VALID",
            "(492,493) VALID",
            "(128,129) NOT VALID");
      }

      public override void run()
      {
         input
            .Split(new [] {"\r\n"}, StringSplitOptions.None)//sprint the string into each of it's lines
            .Skip(1)//skip the first input base it's the number of pairs we're inputing.  This will work for any number of pairs
            .Select(m => new Tuple<int, int>(Convert.ToInt32(m.Trim('(', ')').Split(',')[0]), Convert.ToInt32(m.Trim('(', ')').Split(',')[1])))//Parse the line into a tuple of two numbers
            .ToList()
            .ForEach(m => Output($"({m.Item1},{m.Item2}) {(sumOfPrimes(m.Item1) == sumOfPrimes(m.Item2) ? "VALID" : "NOT VALID")}"));//Check if the sum of primes is equal, if so output VALID otherwise output NOT VALID
      }

      private int sumOfPrimes(int num)
      {
         return getFactorsOf(num).Where(isPrime).Sum();
      }

      public static IEnumerable<int> getFactorsOf(int num)
      {
         int max = (int)Math.Ceiling(Math.Sqrt(num));
         for (int factor = 1; factor < max; factor++)
         {
            if (num % factor == 0)
            {
               yield return factor;
               if (factor != max)
                  yield return num / factor;
            }
         }
      }

      public static bool isPrime(int number)
      {
         if (number == 1) return false;
         if (number == 2) return true;

         for (var i = 2; i <= (int)Math.Floor(Math.Sqrt(number)); ++i)
         {
            if (number % i == 0)
               return false;
         }

         return true;
      }
   }
}
