Copyright (c) 2011, Owen Stephens
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.
    * Neither the name of Owen Stephens nor the
      names of its contributors may be used to endorse or promote products
      derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL Owen Stephens BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

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
