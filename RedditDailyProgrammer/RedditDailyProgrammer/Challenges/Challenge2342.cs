using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditDailyProgrammer;

namespace ChallengeRunner.Challenges
{
   class Challenge2342 : ChallengeBase
   {
      public override void Run()
      {
         var wordList = File.ReadAllLines("ContentFiles/enable1.txt");

         var inputWords = Input
                           .Split(new[] {"\r\n"}, StringSplitOptions.None)
                           .ToList();

         foreach (var inputWord in inputWords)
         {
            if (wordList.Contains(inputWord))
            {
               Output(inputWord);
            }
            else
            {
               for (var i = 0; i < inputWord.Length; i++)
               {
                  if (!wordList.Any(m => m.StartsWith(inputWord.Substring(0, i))))
                  {
                     Output(string.Format($"{inputWord.Substring(0, i)}<{inputWord.Substring(i)}"));
                     break;
                  }
               }
            }
         }
      }
   }
}
