using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Operators
{
    public class SubtractionOperator : BinaryOperator
    {
        private const string SUBTRACTION = "-";

        public SubtractionOperator() : base(SUBTRACTION) { }

        public override double Calculate(double lValue, double rValue)
        {
            return lValue - rValue;
        }
    }
}
