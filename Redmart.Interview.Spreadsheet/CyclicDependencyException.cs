using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class CyclicDependencyException : ApplicationException
    {
        public CyclicDependencyException(string cellId) :
            base($"Cyclical Dependencies found when evaluatuing {cellId}")
        {

        }
    }
}
