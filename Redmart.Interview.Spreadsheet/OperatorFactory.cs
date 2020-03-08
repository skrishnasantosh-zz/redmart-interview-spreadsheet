using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class OperatorFactory
    {        
        private static OperatorFactory instance;
        private readonly List<IOperator> operators;

        private OperatorFactory()
        {
            // Reflection may be bad in performance, but this is done one time 
            // at start of the application. So it is better to do it for this use case
            // as we have not referenced any DI libraries (as per requirement)

            operators = new List<IOperator>();

            IEnumerable<Type> matchedTypes = Assembly
                                                .GetExecutingAssembly()
                                                .GetTypes()
                                                .Where(t =>
                                                    t.GetInterfaces().Contains(typeof(IOperator)) &&
                                                    !t.IsAbstract);
            foreach (var t in matchedTypes)
            {
                operators.Add(Activator.CreateInstance(t) as IOperator);
            }
        }
              
        public IOperator CreateOperatorForToken(string token)
        {
            var op = operators.FirstOrDefault(op => op.IsOpcodeMatch(token.Trim()));
            return op;
        }

        public static OperatorFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new OperatorFactory();

                return instance;
            }
        }
    }
}
