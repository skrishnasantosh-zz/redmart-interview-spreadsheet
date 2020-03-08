using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class WorksheetInvalidBoundsException : SpreadSheetException
    {
        public WorksheetInvalidBoundsException(string rows, string cols) :
            base($"Worksheet bounds are invalid ({rows}, {cols})", -3)
        { 
        }

        public WorksheetInvalidBoundsException(uint rows, uint cols) :
            base($"Worksheet bounds are invalid ({rows}, {cols})", -3)
        {
        }
    }
}
