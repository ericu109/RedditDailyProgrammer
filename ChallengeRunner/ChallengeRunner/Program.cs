using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ChallengeRunner.Challenges;


namespace ChallengeRunner
{
   class Program
   {
      static void Main(string[] args)
      {
         ChallengeBase c = mostRecentChallenge();
         c.run();
         Console.WriteLine(c.passed? "Challenge Passed" : "Challenge Failed");
         Console.ReadLine();
      }

      private static ChallengeBase mostRecentChallenge()
      {
         var assembly = Assembly.GetExecutingAssembly();

         var challenges = assembly.GetTypes().Where(m => m.Name.Contains("Challenge") && m.Name != "ChallengeBase");

         Type mostRecentChallenge = null;
         int highestChallengeNumber = 0;

         foreach (var challenge in challenges)
         {
            var challengeNumber = Convert.ToInt32(challenge.Name.Replace("Challenge", string.Empty));
            if (challengeNumber > highestChallengeNumber)
            {
               highestChallengeNumber = challengeNumber;
               mostRecentChallenge = challenge;
            }
         }

         if (mostRecentChallenge != null)
         {
            return (ChallengeBase) Activator.CreateInstance(mostRecentChallenge);
         }
         throw new Exception("Could not find a challenge to run");
      }
   }
}
