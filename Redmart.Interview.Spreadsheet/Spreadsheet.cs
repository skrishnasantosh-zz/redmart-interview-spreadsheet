using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class Spreadsheet
    {
        private const char ROWSTART = 'A';
        private const char ROWEND= 'Z';
                
        public static int Main(string[] args)
        {
            try
            {
                var source = Console.In.ReadToEnd(); // Read all from piped stdin

                var spreadSheet = new Spreadsheet();
                return spreadSheet.Run(source);
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
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR : UNHANDLED EXCEPTION \n");
                Console.WriteLine($"{ex.Message}");
                return -100;
            }
        }

        public WorkSheetGraph CurrentSheet
        {
            get;
            private set;
        }

        public int Run(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new CommandlineInputException("No Input");
            }

            ProcessInput(source);

            Dump();

            return 0;
        }

        public void ProcessInput(string source)
        {
            int columnIndex = 0;
            char rowPrefix = ROWSTART;

            using (var reader = new StringReader(source))
            {
                var line = reader.ReadLine();
                if (line != null)                
                    CreateWorkSheet(line.Trim());

                line = reader.ReadLine();

                while (line != null)
                {
                    line = line.Trim();
                    string cellId = $"{rowPrefix}{columnIndex + 1}";

                    CurrentSheet.SetCellFormula(cellId, line);

                    columnIndex++;
                    if (columnIndex >= CurrentSheet.Width)
                    {
                        rowPrefix++;
                        columnIndex = 0;
                    }
                    
                    line = reader.ReadLine();                    
                }
            }
        }

        private void CreateWorkSheet(string line)
        {
            var bounds = line.Split(' ');

            if (bounds.Length != 2)
                throw new CommandlineInputException($"Bounds could not be resolved from {line}");

            if (bounds[0].StartsWith('-') || bounds[1].StartsWith('-'))
                throw new WorksheetInvalidBoundsException(bounds[0], bounds[1]);

            if (!uint.TryParse(bounds[0], out uint width) ||
                !uint.TryParse(bounds[1], out uint height))
                throw new CommandlineInputException($"Incorrect bound specification (width : {bounds[0]}, height : {bounds[1]})");

            if (height > ((ROWEND - ROWSTART) + 1))
                throw new OverflowException($"There cannot be more than {(ROWEND - ROWSTART)} rows at this time");

            CurrentSheet = new WorkSheetGraph(this, height, width);
        }
      
        public void Dump()
        {
            Console.WriteLine($"{CurrentSheet.Width} {CurrentSheet.Height}");
            // Probably the only time we need to scan the whole matrix 
            for (int h = 0; h < CurrentSheet.Height; h ++)
            {
                for (int w = 0; w < CurrentSheet.Width; w++)
                {
                    var cell = CurrentSheet.Cells[h, w];
                    if (cell != null && cell.Value.HasValue)
                        Console.WriteLine($"{cell.Value:0.00000}");
                    else
                        Console.WriteLine("-");
                }
            }
        }
    }
}
