using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Tokens
{
    public interface IOperator
    {
        bool IsOpcodeMatch(string token);
        double Reduce(WorkSheetGraph graph, CellNode node, Stack<double> stack, string token);
    }
}
