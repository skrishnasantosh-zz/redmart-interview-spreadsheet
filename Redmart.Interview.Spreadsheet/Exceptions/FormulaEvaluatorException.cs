using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class FormulaEvaluatorException : SpreadSheetException
    {
        public FormulaEvaluatorException(string message) : base(message, -7) { }
    }
}
