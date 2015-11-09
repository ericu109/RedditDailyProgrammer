using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditDailyProgrammer;

namespace ChallengeRunner.Challenges
{
   public class Challenge2401 : ChallengeBase
   {
      public override void Run()
      {
         Input
            .Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries)
            .Select(typoglycemiaify)
            .ToList()
            .ForEach(Output);
      }

      private string typoglycemiaify(string sentance)
      {
         var words = sentance.Split(' ').Where(m => m != string.Empty).ToList();
         var puncuation = new[] { '.', ',' };
         var sb = new StringBuilder();

         foreach (var word in words)
         {
            var last = word[word.Length - 1];
            bool puncuationPresent = false;
            if (puncuation.Contains(last))
            {
               last = word[word.Length - 2];
               puncuationPresent = true;
            }
            sb.Append(word.Length > 2
               ? $"{word[0]}{ScrambleWord(word.Substring(1, (word.Length - (puncuationPresent ? 3 : 2))))}{last} "
               : $"{word} ");
         }

         return sb.ToString();
      }
      private string ScrambleWord(string word)
      {
         var chars = new char[word.Length];
         var rand = new Random(10000);
         var index = 0;
         while (word.Length > 0)
         { 
            var next = rand.Next(0, word.Length - 1); 
                                                      
            chars[index] = word[next];
            word = word.Substring(0, next) + word.Substring(next + 1);
            ++index;
         }
         return new string(chars);
      }
   }
}
