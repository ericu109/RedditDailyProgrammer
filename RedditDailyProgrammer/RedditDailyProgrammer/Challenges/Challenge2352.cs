using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditDailyProgrammer;

namespace ChallengeRunner.Challenges
{
   public class Challenge2352 : ChallengeBase
   {
      public override void Run()
      {
         Input
            .Split(new[] {"\r\n"}, StringSplitOptions.None)
            .Select(m => m.Split(' '))
            .ToList()
            .ForEach(m => Output(scoreGame(m).ToString()));
      }

      private int scoreGame(string[] scoreCard)
      {
         int total = 0;
         for (int i = 0; i < scoreCard.Length; i++)
         {
            
         }

         return total;
      }
   }
}
