using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class OperatorNotFoundException : FormulaEvaluatorException
    {
        // use the same error code
        public OperatorNotFoundException(string message): base(message) { }
    }
}
