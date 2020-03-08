using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Operators
{
    public abstract class BinaryOperator : IOperator
    {
        private string opCode;

        public BinaryOperator(string opCode)
        {
            this.opCode = opCode;
        }

        public bool IsOpcodeMatch(string token)
        {
            return (token == opCode);
        }

        public void Operate(string token, Stack<double> stack)
        {
            if (stack.Count < 2)
                throw new InvalidOperationException();

            var rValue = stack.Pop();
            var lValue = stack.Pop();

            var value = Calculate(lValue, rValue);

            stack.Push(value);
        }
        public abstract double Calculate(double lValue, double rValue);
    }
}
