using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Operators
{
    public class IncrementOperator : UnaryOperator
    {
        private const string INCREMENT = "++";
        public IncrementOperator(): base(INCREMENT)
        { }

        public override double Calculate(double lValue)
        {
            return ++lValue;
        }
    }
}
