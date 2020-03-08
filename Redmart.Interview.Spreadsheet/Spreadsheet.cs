using Redmart.Interview.Spreadsheet.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public static class Spreadsheet
    {
        private const char ROWSTART = 'A';
        private const char ROWEND= 'Z';

        public static int Main(string[] args)
        {
            try
            {
                return Run(args);
            }
            catch(CommandlineInputException ex) 
            {
                Console.WriteLine($"ERROR : {ex.Message}");
                return -1;
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"ERROR : {ex.Message}");
                return -2;
            }
            catch (SpreadSheetException ex)
            {
                Console.WriteLine($"ERROR : {ex.Message}");
                return ex.ErrorCode;
            }
        }

        public static int Run(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("ERROR: No commandline arguments");
                return -1;
            }

            var source = args[0];

            Console.WriteLine(source);

            ProcessInput(source);

            return 0;
        }

        public static void ProcessInput(string source)
        {
            int columnIndex = 1;
            char rowPrefix = ROWSTART;

            using (var reader = new StringReader(source))
            {
                var line = reader.ReadLine();
                if (line != null)                
                    CreateWorkSheet(line);

                while (line != null)
                {
                    line = reader.ReadLine();
                    string cellId = $"{rowPrefix}{columnIndex}";

                    CurrentSheet.SetCellFormula(cellId, line);

                    if (columnIndex >= CurrentSheet.Width)
                    {
                        rowPrefix++;
                        columnIndex = 0;
                    }
                }
            }
        }

        private static void CreateWorkSheet(string line)
        {
            var bounds = line.Split(' ');

            if (bounds.Length != 2)
                throw new CommandlineInputException($"Bounds could not be resolved from {line}");

            if (bounds[0].StartsWith('-') || bounds[1].StartsWith('-'))
                throw new WorksheetInvalidBoundsException(bounds[0], bounds[1]);

            if (!uint.TryParse(bounds[0], out uint width) ||
                !uint.TryParse(bounds[1], out uint height))
                throw new CommandlineInputException($"Incorrect bound specification (width : {bounds[0]}, height : {bounds[1]})");

            if (height > (ROWEND - ROWSTART))
                throw new OverflowException($"There cannot be more than {(ROWEND - ROWSTART)} rows at this time");

            CurrentSheet = new WorkSheetGraph(height, width);
        }
            
        public static WorkSheetGraph CurrentSheet
        {
            get;
            private set;
        }      
    }
}
