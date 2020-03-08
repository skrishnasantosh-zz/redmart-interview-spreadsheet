using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Redmart.Interview.Spreadsheet.Operators
{
    public class NumericConstantOperator : IOperator
    {
        public bool IsOpcodeMatch(string token)
        {
            return (double.TryParse(token, out double value));
        }

        public void Operate(string token, Stack<double> stack)
        {
            if (double.TryParse(token, out double value))
            {
                stack.Push(value);
                return;
            }
            
            throw new FormulaEvaluatorException($"Value {token} is not recognized as number");
        }
    }
}
