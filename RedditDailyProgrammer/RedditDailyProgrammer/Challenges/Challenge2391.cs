using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditDailyProgrammer;

namespace ChallengeRunner.Challenges
{
   class Challenge2391 : ChallengeBase
   {
      public override void Run()
      {
         var num = Convert.ToInt32(Input);

         while (num != 1)
         {
            if (num%3 == 0)
            {
               Output($"{num} 0");
               num = num/3;
            }
            if ((num - 1)%3 == 0)
            {
               Output($"{num} -1");
               num = (num - 1)/3;
            }
            if ((num + 1)%3 == 0)
            {
               Output($"{num} 1");
               num = (num + 1)/3;
            }
         }
         Output("1");
      }
   }
}

