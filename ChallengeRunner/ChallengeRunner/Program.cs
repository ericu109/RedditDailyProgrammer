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
         ChallengeBase c = mostRecentChallenge();//Get the most recent challenge file
         c.run();//run the challenge
         Console.WriteLine(c.passed? "Challenge Passed" : "Challenge Failed");//Output weather or not the challenge was passed.
         Console.ReadLine();//Pause
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
         throw new Exception("Could not find a challenge to run.");
      }
   }
}
