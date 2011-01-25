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

