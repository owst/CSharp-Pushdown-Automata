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
using System.Linq;

namespace PushdownAutomaton
{
    class PDATransition
    {
        public char InputChar { get; private set; }
        public char StackHead { get; private set; }
        public string StackReplace { get; private set; }
        public int OldState { get; private set; }
        public int NewState { get; private set; }

        public PDATransition(char inputChar, char stackHead, int oldState,
                             int newState, string stackReplace)
        {
            InputChar = inputChar;
            StackHead = stackHead;
            StackReplace = stackReplace;
            OldState = oldState;
            NewState = newState;
        }

        public bool MatchesConfiguration(PDARunState state)
        {
            return InputChar == state.Input[0] &&
                   OldState == state.State &&
                   ((StackHead == '_' && state.Stack.Count() == 0) ||
                    (state.Stack.Count() > 0 && StackHead == state.Stack[0]));
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            PDATransition pdat = (PDATransition)obj;

            return InputChar == pdat.InputChar &&
                   StackHead == pdat.StackHead &&
                   StackReplace.SequenceEqual(pdat.StackReplace) &&
                   NewState == pdat.NewState &&
                   OldState == pdat.OldState;
        }

        public override int GetHashCode()
        {
            return InputChar.GetHashCode() ^
                   StackHead.GetHashCode() ^
                   StackReplace.GetHashCode() ^
                   NewState.GetHashCode() ^
                   OldState.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("<{0},{1},{2}>-<{3},{4}>", InputChar, OldState,
                StackHead, NewState,
                String.Join("", StackReplace.Select(s => s.ToString())));
        }
    }
}

