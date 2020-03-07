using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    // Model this graph as Adjacency matrix

    // Worksheet handles insertion of values into the sheet.
    // It does not process formula and handles all stuffs related to entry 
    // and detection of cyclic dependencies on such entry
    public class WorkSheetGraph 
    {
        private readonly CellNode[,] cells;
        private uint maxRows;
        private uint maxColumns;

        public WorkSheetGraph(uint rows, uint columns)
        {
            cells = new CellNode[rows, columns];

            if (rows == 0 || columns == 0)
                throw new WorksheetInvalidBoundsException(rows, columns);

            maxRows = rows - 1;
            maxColumns = columns - 1;
        }

        // Gets the cell node based on cell identifier
        public CellNode GetCell(string id)
        {
            var (row, col) = GetPositionFromId(id);

            var cell = cells[row, col];

            if (cell == null)            
                cell = new CellNode(id, row, col);            

            return cell;
        }

        // Identifies if cell value is a formula or a constant and assigns accordingly
        public void SetCellValueOrFormula(string  id, string valueOrFormula)
        {
            if (string.IsNullOrWhiteSpace(valueOrFormula))
                throw new ArgumentException("Value or Formula is missing");

            var (row, col) = GetPositionFromId(id);

            if (cells[row, col] == null)
                cells[row, col] = new CellNode(id, row, col);

            var cell = cells[row, col];

            if (double.TryParse(valueOrFormula, out double value))
            {
                cell.Value = value;
                cell.Formula = valueOrFormula;
            }
            else
            {
                cells[row, col].Value = null;
                SetCellFormula(row, col, valueOrFormula);
            }
        }

        public void Evaluate()
        {
            //Walk through the graph and evaluate formula
            var root = GetCell("A0");


        }

        // Privates

        // In case the value provided is a formula, 
        // extend the edges of the graph and check for 
        // cyclic dependencies
        private void SetCellFormula(uint row, uint col, string formula)
        {
            var tokens = formula.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var thisCell = cells[row, col];

            foreach (var token in tokens)
            {
                // Quickly process cyclic dependency during insertion itself
                // so that formula evaluation happens faster
                if (char.IsLetter(token[0]))
                {
                    var (formulaRow, formulaCol) = GetPositionFromId(token);                    
                    var connectionCell = cells[formulaRow, formulaCol];

                    if (connectionCell == null)                    
                        connectionCell = new CellNode(token, formulaRow, formulaCol);                    

                    AddEdge(thisCell, connectionCell);
                }
            }

            cells[row, col].Formula = formula;
        }

        // Adds a directed edge to the cell
        // Tradeoff decision - speed vs space - prefer speed as this space is shortlived and temporary
        private void AddEdge(CellNode from, CellNode to)
        {
            from.Edges.Add(to);
            var bitmap = new bool[cells.GetLength(0), cells.GetLength(1)];

            CheckForCyclicDependencies(from, bitmap);
        }

        // Walk through dependent nodes and check each dependent node for dependencies        
        private void CheckForCyclicDependencies(CellNode cell, bool[,] bitmap)
        {
            if (cell == null)
                return;

            var (row, col) = cell.Position;

            if (bitmap[row, col] == true)
                throw new CyclicDependencyException(cell.Name);

            bitmap[row, col] = true; //mark current node in bitmap

            for (var i = 0; i < cell.Edges.Count; i++)
            {
                CheckForCyclicDependencies(cell.Edges[i], bitmap);
            }            
        }

        // Gets the position (x,y) when cell identifier is specified
        // Validates the input
        private (uint, uint) GetPositionFromId(string id)
        {
            // Decision: Used single exception with multiple messages as
            // these are raised when id is invalid
            // So that the code is terse
            if (string.IsNullOrWhiteSpace(id))
                throw new InvalidCellReferenceException($"Cell name is missing");

            if (!char.IsLetter(id[0]) || id.Length == 1)
                throw new InvalidCellReferenceException($"Cell reference is not well formed");

            var colRef = id.Substring(1);
            if (!uint.TryParse(colRef, out uint colId) || colId == 0)
                throw new InvalidCellReferenceException($"Column reference zero is not allowed");

            uint rowId = (uint)((id.ToLower())[0] - 'a');
            colId--  ;

            if (maxRows < rowId || maxColumns < colId)
                throw new CellRangeOutOfBoundsException(rowId + 1, colId + 1);

            return (rowId, colId);
        }        
    }
}
