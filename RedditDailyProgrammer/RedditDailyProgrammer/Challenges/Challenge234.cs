using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditDailyProgrammer;

namespace ChallengeRunner.Challenges
{
   class Challenge234 : ChallengeBase
   {
      public override void run()
      {
         var wordList = File.ReadAllLines("ContentFiles/enable1.txt");

         var inputWords = input
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
                     Output(string.Format($"{inputWord.Substring(0, i)}<{inputWord.Substring(i, inputWord.Length-i)}"));
                     break;
                  }
               }
            }
         }
      }

      protected override void init()
      {
         input = string.Join(Environment.NewLine,
            "accomodate",
            "acknowlegement",
            "arguemint",
            "comitmment",
            "deductabel",
            "depindant",
            "existanse",
            "forworde",
            "herrass",
            "inadvartent",
            "judgemant",
            "ocurrance",
            "parogative",
            "suparseed"
            );

         expectedOutput = string.Join(Environment.NewLine,
            "accomo<date",
            "acknowleg<ement",
            "arguem<int",
            "comitm<ment",
            "deducta<bel",
            "depin<dant",
            "exista<nse",
            "forword<e",
            "herra<ss",
            "inadva<rtent",
            "judgema<nt",
            "ocur<rance",
            "parog<ative",
            "supa<rseed"
            );
      }
   }
}
