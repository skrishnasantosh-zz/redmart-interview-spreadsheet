using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Operators
{
    public class DivisionOperator : BinaryOperator
    {
        private const string DIVISION = "/";

        public DivisionOperator() : base(DIVISION) { }

        public override double Calculate(double lValue, double rValue)
        {
            return lValue / rValue;
        }
    }
}
