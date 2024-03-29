﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class SpreadSheetException : ApplicationException
    {
        public SpreadSheetException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;  
        }

        public int ErrorCode { get; }
    }
}
