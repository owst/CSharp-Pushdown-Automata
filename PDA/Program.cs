using System;
using System.Collections.Generic;
using PushdownAutomaton;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputAlphabet = new List<char> { '(', ')' };
            var stackAlphabet = new List<char> { '(' };
            var states = new HashSet<int> { 0 };
            var transitions = new List<PDATransition>
            {
                new PDATransition('(', '_', 0, 0, "("),
                new PDATransition('(', '(', 0, 0, "(("),
                new PDATransition(')', '(', 0, 0, "")
            };

            var pda = new PDA(inputAlphabet, stackAlphabet, states, 0, transitions);

            Console.WriteLine(pda.DoesMatch(""));             // True
            Console.WriteLine(pda.DoesMatch("(())"));         // True
            Console.WriteLine(pda.DoesMatch("(()"));          // False
            Console.WriteLine(pda.DoesMatch("((())()(()))")); // True
        }
    }
}
