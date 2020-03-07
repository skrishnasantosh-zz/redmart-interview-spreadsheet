using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Tokens
{
    public class TokenMap
    {
        private readonly IEnumerable<IOperator> operators;

        public TokenMap(IEnumerable<IOperator> operators)
        {
            this.operators = operators;
        }

        public IOperator GetMappedOperator(string token)
        {
            var op = operators.FirstOrDefault(op => op.IsOpcodeMatch(token));
            return op;
        }
    }
}
