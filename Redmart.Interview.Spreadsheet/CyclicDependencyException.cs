using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class CyclicDependencyException : SpreadSheetException
    {
        public CyclicDependencyException(string cellId) :
            base($"Cyclical Dependencies found when evaluatuing {cellId}", -4)
        {

        }
    }
}
