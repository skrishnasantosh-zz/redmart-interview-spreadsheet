using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class InvalidCellReferenceException : SpreadSheetException
    {
        public InvalidCellReferenceException(string message) : base(message, -6) 
        { 
        }
    }
}
