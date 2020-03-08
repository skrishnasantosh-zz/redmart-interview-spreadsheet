using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Operators
{
    public abstract class UnaryOperator : IOperator
    {
        private string opCode;

        public UnaryOperator(string opCode)
        {
            this.opCode = opCode;
        }

        public bool IsOpcodeMatch(string token)
        {
            return (token == opCode);
        }

        public void Operate(string token, Stack<double> stack)
        {
            if (stack.Count < 1)
                throw new FormulaEvaluatorException("Incorrect operands for this operator");

            var value = Calculate(stack.Pop());

            stack.Push(value);
        }

        public abstract double Calculate(double lValue);
    }
}
