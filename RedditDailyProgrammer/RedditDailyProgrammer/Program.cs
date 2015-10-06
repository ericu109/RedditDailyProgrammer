using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RedditDailyProgrammer.Challenges;


namespace RedditDailyProgrammer
{
   class Program
   {
      static void Main(string[] args)
      {
         ChallengeBase challengeToExecute = getChallenge("Challenge234");
         challengeToExecute.run();//run the challenge
         Console.WriteLine(challengeToExecute.passed? "Challenge Passed" : "Challenge Failed");//Output weather or not the challenge was passed.
         Console.ReadLine();//Pause
      }

      private static ChallengeBase getChallenge(string challenge = "")
      {
         if (string.IsNullOrEmpty(challenge))
         {
            return mostRecentChallenge();
         }
         else
         {
            return getChallengeWithName(challenge);
         }
      }

      private static ChallengeBase getChallengeWithName(string challengeName)
      {
         if (string.IsNullOrEmpty(challengeName))
         {
            throw new ArgumentNullException(nameof(challengeName));
         }

         var challenge = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(m => m.Name == challengeName);

         if (challenge != null)
         {
            return (ChallengeBase) Activator.CreateInstance(challenge);
         }
         throw new Exception($"Could not find challenge {challengeName} to run");
      }

      private static ChallengeBase mostRecentChallenge()
      {
         var challenges = Assembly.GetExecutingAssembly().GetTypes().Where(m => m.Name.Contains("Challenge") && m.Name != "ChallengeBase");

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
