using System;
using System.Collections.Generic;
using System.Linq;

namespace PushdownAutomaton
{
    class PDA
    {
        public IEnumerable<PDATransition> Transitions { get; private set; }
        public int StartState { get; private set; }
        public ISet<int> States { get; private set; }
        public IEnumerable<char> StackAlphabet { get; private set; }
        public IEnumerable<char> InputAlphabet { get; private set; }

        public PDA(IEnumerable<Char> inputAlphabet,
                   IEnumerable<Char> stackAlphabet,
                   ISet<int> states, int startState,
                   IEnumerable<PDATransition> transitions)
        {
            InputAlphabet = inputAlphabet;
            StackAlphabet = stackAlphabet;
            States = states;
            StartState = startState;
            Transitions = transitions;
        }

        /// <summary>
        /// Does this PDA match the given input?
        /// </summary>
        public bool DoesMatch(string input)
        {
            return MatchResults(input).Any(r => r.Success);
        }

        /// <summary>
        /// Return an IEnumerable of PDAResults after applying this PDA to the
        /// given input.
        /// </summary>
        public IEnumerable<PDAResult> MatchResults(string input)
        {
            // Catch empty initial string, which trivially matches an empty
            // stack acceptance PDA.
            if (input == "")
            {
                return new List<PDAResult> { new PDAResult(0, 0, true) };
            }

            // If there are no transitions that can move on the initial
            // configuration, or can remove items from the stack, fail quickly.
            if (!Transitions.Any(t => t.StackHead == '_' &&
                                       t.OldState == StartState &&
                                       t.InputChar == input[0]) || 
                !Transitions.Any(t => t.StackReplace.Length == 0))
            {
                return new List<PDAResult> { new PDAResult(0, 0, false) };
            }

            // HashSet to remove duplicate results.
            var resultList = new HashSet<PDAResult>();

            var stateList = new List<PDARunState> { new PDARunState(input, 0, 
                "", StartState) };

            while (stateList.Count() > 0)
            {
                // HashSet to remove duplicate states.
                var newStateList = new HashSet<PDARunState>();

                foreach (var state in stateList)
                {
                    // Obtain the list of states reachable from this state.
                    var nextStates = FindNextStates(state).ToList();

                    // No further states, so fail.
                    if (nextStates.Count == 0)
                    {
                        resultList.Add(new PDAResult(state.MatchedSoFar,
                                                     state.Stack.Length,
                                                     false));
                        continue;
                    }

                    foreach (var nextState in nextStates)
                    {
                        // Check for accept/reject state with acceptance by
                        // empty stack.
                        if (nextState.Failure || nextState.Input == "" &&
                                                 nextState.Stack.Length == 0)
                        {
                            resultList.Add(new PDAResult(nextState.MatchedSoFar,
                                nextState.Stack.Count(), !nextState.Failure));
                        }
                        else
                        {
                            newStateList.Add(nextState);
                        }
                    }
                }

                stateList = newStateList.ToList();
            }

            return resultList.AsEnumerable();
        }

        /// <summary>
        /// Return a list of all next possible states, having followed any
        /// applicable transitions, given the current state.
        /// </summary>
        private IEnumerable<PDARunState> FindNextStates(PDARunState state)
        {
            // Follow each possible transition available.
            return from transition in Transitions
                   where transition.MatchesConfiguration(state)
                   select ApplyTransition(transition, state);
        }

        /// <summary>
        /// Applies the given transition, to the given run state.
        /// </summary>
        /// <returns>
        /// An IEnumerable of the states resulting from applying the transition.
        /// </returns>
        private static PDARunState ApplyTransition(PDATransition transition,
                                                   PDARunState state)
        {
            var newState = transition.NewState;
            var newStack = state.Stack;
            var newInput = state.Input.Substring(1);
            var newMatchedSoFar = state.MatchedSoFar + 1;
            var isFailure = false;

            // If we're not matching against the empty stack, try pop.
            if (transition.StackHead != '_')
            {
                if (newStack.Length > 0)
                {
                    newStack = newStack.Substring(1);
                }
                else
                {
                    // Cannot pop from the stack, so bail out here.
                    isFailure = true;
                }
            }
            else if (newStack != "")
            {
                // Catch illegal use of empty stack transition.
                isFailure = true;
            }

            if (!isFailure)
            {
                newStack = transition.StackReplace + newStack;
            }

            var returnState = new PDARunState(newInput, newMatchedSoFar, newStack, newState);

            // Check for end-of-input without empty stack - failure.
            // We won't be able to empty the stack if there are fewer input
            // chars than stack elems, so fail to prevent unnecessary work.
            if (isFailure || newInput == "" && newStack.Length > 0 || newInput.Length < newStack.Length)
            {
                returnState.Failure = true;
            }

            return returnState;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var pda = (PDA)obj;

            return InputAlphabet.SequenceEqual(pda.InputAlphabet) &&
                   StackAlphabet.SequenceEqual(pda.StackAlphabet) &&
                   States.SequenceEqual(pda.States) &&
                   StartState == pda.StartState &&
                   Transitions.SequenceEqual(pda.Transitions);
        }

        public override int GetHashCode()
        {
            return InputAlphabet.GetHashCode() ^
                   StackAlphabet.GetHashCode() ^
                   States.GetHashCode() ^
                   StartState.GetHashCode() ^
                   Transitions.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("PDA: StackAlpha:<{0}> StartState:<{1}>" + 
                "States:<{2}> InputAlpha:<{3}> Transitions:{4}", 
                String.Join(",", StackAlphabet.Select(s => s.ToString())), 
                StartState, String.Join(",", States.Select(s => s.ToString())), 
                String.Join(",", InputAlphabet.Select(s => s.ToString())), 
                String.Join(", ", Transitions.Select(t => t.ToString())));
        }
    }
}

