using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class CellRangeOutOfBoundsException : SpreadSheetException
    {
        public CellRangeOutOfBoundsException(uint rows, uint cols) :
              base($"Cell range ({rows}, {cols}) is beyond the worksheet and is invalid", -3)
        {
        }
    }
}
