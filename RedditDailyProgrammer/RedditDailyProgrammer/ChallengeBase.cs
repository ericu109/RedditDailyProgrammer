using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditDailyProgrammer
{
   public abstract class ChallengeBase
   {
      protected static string Input;
      public static string ExpectedOutput;
      public bool Passed => string.Join("\r\n", AllOutput) == ExpectedOutput;
      public bool ExpectingResult => ExpectedOutput != "Not Defined";
      protected List<string> AllOutput = new List<string>();

      protected ChallengeBase()
      {
         getInputAndExpectedOutput();
      }

      public abstract void Run();

      protected void Output(string line)
      {
         Console.WriteLine(line);
         AllOutput.Add(line);
      }

      private void getInputAndExpectedOutput()
      {
         var inputOutput = File.ReadAllLines("InputAndExpectedOutput/" + GetType().Name + ".txt");
         var input = new StringBuilder();
         var output = new StringBuilder();

         bool doneWithInput = false;

         foreach (var line in inputOutput.Where(line => !line.StartsWith("//")))
         {
            if (string.IsNullOrEmpty(line))
            {
               doneWithInput = true;
               continue;
            }

            if (!doneWithInput) input.AppendLine(line);
            else output.AppendLine(line);
         }

         Input = input.ToString().Trim();
         ExpectedOutput = output.ToString().Trim();
      }
   }
}
