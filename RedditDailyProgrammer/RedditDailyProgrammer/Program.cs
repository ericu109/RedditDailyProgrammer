using System;
using System.Collections.Generic;
using System.Diagnostics;
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
         try
         {
            ChallengeBase challengeToExecute = getChallenge();
            DateTime startTime;
            DateTime stopTime;

            startTime = DateTime.Now;
            challengeToExecute.Run(); //run the challenge
            stopTime = DateTime.Now;
            if (challengeToExecute.ExpectingResult)
            {
               Console.WriteLine(challengeToExecute.Passed ? "Challenge Passed" : "Challenge Failed");
            }
            else
            {
               Console.WriteLine("Not expecting any particular result.");
            }

            //Output some stats:
            Console.WriteLine($"{Environment.NewLine}Here are some stats!");
            Console.WriteLine($"Start Time      : {startTime}");
            Console.WriteLine($"Stop Time       : {stopTime}");
            Console.WriteLine($"Run Duration    : {(stopTime - startTime).ToString(@"mm\:ss\.fff")}");
            Console.WriteLine($"Peak Memory     : {Process.GetCurrentProcess().PeakWorkingSet64/1024f/1024f}MB");
            Console.WriteLine($"Total CPU Time  : {Process.GetCurrentProcess().TotalProcessorTime.ToString(@"mm\:ss\.fff")}");
         }
         catch (Exception e)
         {
            Console.WriteLine(e.Message);
         }
         finally
         {
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
         }
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
