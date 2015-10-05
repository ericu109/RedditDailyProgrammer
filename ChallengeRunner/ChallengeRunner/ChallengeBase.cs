using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeRunner
{
   public abstract class ChallengeBase
   {
      protected ChallengeBase()
      {
         init();
      }

      protected static string[] input;
      protected static string[] expectedOutput;

      public bool passed => AllOutput.SequenceEqual(expectedOutput);

      public abstract void run();
      protected abstract void init();
      protected List<string> AllOutput = new List<string>();
      protected void Output(string line)
      {
         Console.WriteLine(line);
         AllOutput.Add(line);
      }
   }
}
