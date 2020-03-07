using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Tokens
{
    public class CellReferenceOperator : IOperator
    {
        public bool IsOpcodeMatch(string token)
        {
            if (char.IsLetter(token[0]))
                return true;

            return false;
        }

        public double Reduce(WorkSheetGraph graph, CellNode node, Stack<double> stack, string token)
        {
            //if (graph.TryGetValue(token, out CellNode cell))
            //{

            //}

            throw new NotImplementedException();
        }
    }
}
