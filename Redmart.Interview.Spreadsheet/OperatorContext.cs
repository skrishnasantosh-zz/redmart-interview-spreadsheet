using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class OperatorContext
    {
        public OperatorContext(IOperator op, string token)
        {
            Operator = op;
            Token = token;
        }

        public OperatorContext(double value)
        {            
            Value = value;
        }

        public IOperator Operator { get; }
        public string Token { get; }

        public double? Value { get; set; }
    }
}
