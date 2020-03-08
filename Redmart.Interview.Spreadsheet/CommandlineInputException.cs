using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class CommandlineInputException : ApplicationException
    {
        public CommandlineInputException(string message):base(message)
        { }
    }
}
