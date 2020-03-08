using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Operators
{
    public class DecrementOperator : UnaryOperator
    {
        private const string DECREMENT = "--";

        public DecrementOperator() : base(DECREMENT)
        { }

        public override double Calculate(double lValue)
        {
            return --lValue;
        }
    }
}
