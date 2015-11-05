/*
   https://www.reddit.com/r/dailyprogrammer/comments/3rhzdj/20151104_challenge_239_intermediate_a_zerosum/

   Doesn't really work with 18446744073709551614 as it's impossible, but this program continues searching each path.
   Can probably change it so that it doesn't do that, and return impossible much earlier.

   Looks like there are a couple of other solutions having this issue if you look at the replies in the thread.

   Finding one solution for 18446744073709551615 happens quickly though.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using RedditDailyProgrammer;

namespace ChallengeRunner.Challenges
{
   public class Challenge2392 : ChallengeBase
   {
      public override void Run()
      {
         var startingNumber = Convert.ToUInt64(Input);

         var solutions = new Node(startingNumber).getSoulutions();

         foreach (var solution in solutions)
         {
            ulong num = startingNumber;
            foreach (var step in solution)
            {
               Output($"{num} {step}");
               num = (num + (ulong)step)/3;
            }
            Output("1");
            Output(string.Empty);
         }

         if (solutions.Count == 0)
         {
            Output("Impossible");
         }
      }

      public class Node
      {
         private Node parent { get; set; }
         private Actions Action { get; set; }
         private readonly ulong num;
         private static List<Node> solutionLeafs;
         private static bool findOne;

         /// <summary>
         /// 
         /// </summary>
         /// <param name="num">Starting number</param>
         /// <param name="findOne">Find only one solution.  Passing false will get all solutions for this number, and could cause the program to run out of memory.</param>
         public Node(ulong num, bool findOne = true)
         {
            this.num = num;
            solutionLeafs = new List<Node>();
            Node.findOne = findOne;
            buildSolutions();
         }

         private Node(ulong num, Node parent, Actions action)
         {
            //We're only finding one solution, so just return and call it a day.
            if (solutionLeafs.Count > 0 && findOne)
               return;
            Action = action;
            this.parent = parent;
            this.num = num;
            buildSolutions();
         }

         private void buildSolutions()
         {
            if (num > 1)
            {
               if (num % 3 == 0) new Node(num / 3, this, Actions.None);
               if (num - 1 != 0 && (num - 1) % 3 == 0) new Node((num - 1) / 3, this, Actions.MinusOne);
               if (num - 2 != 0 && (num - 2) % 3 == 0) new Node((num - 2) / 3, this, Actions.MinusTwo);
               if (num + 1 != 0 && (num + 1) % 3 == 0) new Node((num + 1) / 3, this, Actions.AddOne);
               if (num + 2 != 0 && (num + 2) % 3 == 0) new Node((num + 2) / 3, this, Actions.AddTwo);
            }

            if ((num == 1 && sumOfActions() == 0))
            {
               solutionLeafs.Add(this);
            }
         }

         private int sumOfActions()
         {
            Node node = this;
            int sum = 0;

            while (node.parent != null)
            {
               sum += (int)node.Action;
               node = node.parent;
            }

            return sum;
         }

         public List<List<int>> getSoulutions()
         {
            return solutionLeafs.Select(getSolution).ToList();
         }

         private List<int> getSolution(Node node)
         {
            var rval = new List<int>();

            var currNode = node;

            while (currNode.parent != null)
            {
               rval.Add((int) currNode.Action);
               currNode = currNode.parent;
            }
            rval.Reverse();

            return rval;
         }

         enum Actions
         {
            MinusOne = -1,
            MinusTwo = -2,
            None = 0,
            AddOne = 1,
            AddTwo = 2
         }
      }
   }
}
