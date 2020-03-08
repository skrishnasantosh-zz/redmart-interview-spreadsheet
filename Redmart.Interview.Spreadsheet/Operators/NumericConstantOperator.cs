using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Operators
{
    public class NumericConstantOperator : IOperator
    {
        public bool IsOpcodeMatch(string token)
        {
            if (token.Length >= 1 && char.IsDigit(token[0]))
                return true;

            return false;
        }

        public void Operate(string token, Stack<double> stack)
        {
            if (double.TryParse(token, out double value))
            {
                stack.Push(value);
                return;
            }
            
            throw new InvalidOperationException($"Value {token} is not recognized as number");
        }
    }
}
