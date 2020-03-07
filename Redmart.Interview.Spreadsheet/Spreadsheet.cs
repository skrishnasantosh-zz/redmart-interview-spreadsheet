using Microsoft.Extensions.DependencyInjection;
using Redmart.Interview.Spreadsheet.Configuration;
using Redmart.Interview.Spreadsheet.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class Spreadsheet
    {
        //private readonly IApplicationConfiguration configuration;
        private readonly TokenMap tokenMap;
        
        public Spreadsheet(WorkSheetGraph sheet, TokenMap map)
        {
            Sheet = sheet;

            //configuration = config;
            tokenMap = map;
        }

        static int Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddTransient(typeof(WorkSheetGraph));
            services.AddTransient(typeof(TokenMap));
                
            services.AddSingleton(typeof(Spreadsheet));

            var container = services.BuildServiceProvider();

            var spreadSheet = container.GetRequiredService<Spreadsheet>();

            if (args.Length != 2)
            {
                Console.WriteLine("Error : No input");
                
            }

            Console.WriteLine(args[1]);

            //spreadSheet.ProcessInput(argv[1]);

            return 0;
            
        }

        public WorkSheetGraph Sheet
        {
            get;
        }

        public void ProcessInput(string formulae)
        {

        }

        //private double ReduceWorksheet()
        //{
        //    Stack<double> stack = new Stack<double>();
        //    Stack<CellNode> visited = new Stack<CellNode>();
            
        //    foreach (var sh)
        //    //quick exit if it is a single value
        //    if (double.TryParse(formula, out double value) && tokens.Count() == 1)            
        //        return value; 
            
        //    foreach (var token in tokens)
        //    {
        //        var op = tokenMap.GetMappedOperator(token);
        //        var reduced = op.Reduce(stack, token);

        //        stack.Push(reduced);
        //    }

        //    if (stack.Count() != 1)
        //        throw new InvalidOperationException($"Invalid Formula - {formula}");

        //    return stack.Pop();
        //}
    }
}
