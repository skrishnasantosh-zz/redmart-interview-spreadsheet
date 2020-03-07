using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class InvalidCellReferenceException : ApplicationException
    {
        public InvalidCellReferenceException(string message) : base(message) 
        { 
        }
    }
}
