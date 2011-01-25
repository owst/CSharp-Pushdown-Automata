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
