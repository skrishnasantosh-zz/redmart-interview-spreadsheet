using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Operators
{
    public class AdditionOperator : BinaryOperator
    {
        private const string ADDITION = "+";

        public AdditionOperator() : base(ADDITION) { }
        
        public override double Calculate(double lValue, double rValue)
        {
            return lValue + rValue;   
        }
    }
}
