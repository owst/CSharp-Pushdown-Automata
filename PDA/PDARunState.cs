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

namespace PushdownAutomaton
{
    public class PDARunState
    {
        public int MatchedSoFar { get; private set; }
        // Use a string for efficiency.
        public string Stack { get; private set; }
        public int State { get; private set; }
        public string Input { get; private set; }
        // Not private, as a state may be recognised as a failure, later.
        public bool Failure { get; set; }

        public PDARunState(string input, int matchedSoFar, string stack,
                           int state)
        {
            Input = input;
            MatchedSoFar = matchedSoFar;
            Stack = stack;
            State = state;
        }

        public override string ToString()
        {
            return String.Format("Input: '{0}', MatchLen: {1}, Stack: {2}, State: {3}", 
                Input, MatchedSoFar, Stack, State);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var state = (PDARunState)obj;

            return Input == state.Input &&
                   MatchedSoFar == state.MatchedSoFar &&
                   Stack == state.Stack &&
                   State == state.State;
        }

        public override int GetHashCode()
        {
            return Input.GetHashCode() ^
                   MatchedSoFar.GetHashCode() ^
                   Stack.GetHashCode() ^
                   State.GetHashCode();
        }
    }
}
