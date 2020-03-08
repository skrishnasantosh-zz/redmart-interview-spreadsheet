using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class FormulaEvaluator
    {
        private string[] formula;

        public FormulaEvaluator(IEnumerable<string> formula)
        {
            this.formula = formula?.ToArray() ?? new string[] { };
        }

        public bool HasCellReference()
        {
            foreach (var token in formula)
            {
                if (token.Length > 0 && char.IsLetter(token[0]))
                    return true;
            }

            return false;
        }

        public double? Evaluate()
        {
            if (formula.Count() == 0)
                return null;

            var stack = new Stack<double>();

            foreach (var token in formula)
            {
                var op = OperatorStrategy.Instance.GetOperator(token);
                if (op == null)
                    throw new OperatorNotFoundException($"Unrecognized operator {token}");

                op.Operate(token, stack);
            }

            if (stack.Count != 1)
                throw new FormulaEvaluatorException("The formula cannot be evaulated");

            return stack.Pop();
        }
    }
}
