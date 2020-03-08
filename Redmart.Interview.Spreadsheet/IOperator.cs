using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public interface IOperator
    {
        bool IsOpcodeMatch(string token);
        
        void Operate(string token, Stack<double> stack);
    }
}
