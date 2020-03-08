using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Operators
{
    public class MultiplicationOperator : BinaryOperator
    {
        private const string MULTIPLICATION = "*";

        public MultiplicationOperator() : base(MULTIPLICATION) { }

        public override double Calculate(double lValue, double rValue)
        {
            return lValue * rValue;
        }
    }
}
