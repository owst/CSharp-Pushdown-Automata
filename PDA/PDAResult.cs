using System;

namespace PushdownAutomaton
{
    class PDAResult
    {
        public int ConsumedLength { get; private set; }
        public int StackCount { get; private set; }
        public bool Success { get; private set; }

        public PDAResult(int consumedLength, int stackCount, bool success)
        {
            ConsumedLength = consumedLength;
            StackCount = stackCount;
            Success = success;
        }

        public override string ToString()
        {
            return String.Format("Success: {0}, Consumed: {1}, Stack size: {2}", 
                                 Success, ConsumedLength, StackCount);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var result = (PDAResult)obj;

            return ConsumedLength == result.ConsumedLength &&
                StackCount == result.StackCount &&
                Success == result.Success;
        }

        public override int GetHashCode()
        {
            return ConsumedLength.GetHashCode() ^
                StackCount.GetHashCode() ^
                Success.GetHashCode();
        }
    }
}
