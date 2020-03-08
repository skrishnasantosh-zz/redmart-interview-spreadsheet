using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Operators
{
    public class CellReferenceOperator : IOperator
    {            
        public bool IsOpcodeMatch(string token)
        {
            if (token.Length >= 1 && char.IsLetter(token[0]))
                return true;

            return false;
        }

        public void Operate(string token, Stack<double> stack)
        {
            // If it reaches here, it means the cell value referred in the formula 
            // isn't assigned any value. So set to zero

            stack.Push(0);
        }
    }
}
