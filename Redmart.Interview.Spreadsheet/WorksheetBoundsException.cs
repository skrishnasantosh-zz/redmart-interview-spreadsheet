using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class WorksheetInvalidBoundsException : ApplicationException
    {
        public WorksheetInvalidBoundsException(uint rows, uint cols) :
            base($"Worksheet bounds are invalid ({rows}, {cols})")
        {
        }
    }
}
