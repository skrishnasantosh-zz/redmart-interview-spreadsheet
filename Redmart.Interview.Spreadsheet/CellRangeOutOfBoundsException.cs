using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class CellRangeOutOfBoundsException : ApplicationException
    {
        public CellRangeOutOfBoundsException(uint rows, uint cols) :
              base($"Cell range ({rows}, {cols}) is beyond the worksheet and is invalid")
        {
        }
    }
}
