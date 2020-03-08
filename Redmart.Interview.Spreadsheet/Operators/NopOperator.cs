using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Operators
{
    //To tolerate minor leading / trailing spacing mistakes if any
    public class NopOperator : IOperator
    {
        public bool IsOpcodeMatch(string token)
        {
            return string.IsNullOrEmpty(token);
        }

        public void Operate(string token, Stack<double> stack)
        {
            // Do Nothing
        }
    }
}
