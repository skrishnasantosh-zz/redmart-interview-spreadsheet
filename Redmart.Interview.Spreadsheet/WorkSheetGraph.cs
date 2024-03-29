﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    // Model this graph as Adjacency matrix. 
    // This class only manages the graph (including setting values and detecting cyclic dependency)
    public class WorkSheetGraph
    {
        private readonly GraphNode[,] cells;
        private readonly Spreadsheet spreadSheet;

        public WorkSheetGraph(Spreadsheet parent, uint height, uint width)
        {
            if (height == 0 || width == 0)
                throw new WorksheetInvalidBoundsException(height, width);

            spreadSheet = parent;

            // Create a null matrix of rows * columns
            cells = new GraphNode[height, width];

            Height = height;
            Width = width;
        }

        public uint Height
        {
            get;
        }

        public uint Width
        {
            get;
        }

        public GraphNode[,] Cells => cells;

        // Gets the cell node based on cell identifier
        public GraphNode GetCell(string id)
        {
            var (row, col) = GetPositionFromId(id);

            var cell = cells[row, col];

            if (cell == null)
                cell = CreateCell(id, row, col);

            return cell;
        }

        public GraphNode CreateCell(string id, uint row, uint col)
        {
            return new GraphNode(this, id, row, col);
        }

        // Identifies if cell value is a formula or a constant and assigns accordingly
        public void SetCellFormula(string id, string formula)
        {
            if (string.IsNullOrWhiteSpace(formula))
                throw new ArgumentException("Value or Formula is missing");

            var (row, col) = GetPositionFromId(id);

            if (cells[row, col] == null)
                cells[row, col] = CreateCell(id, row, col);

            var cell = cells[row, col];
            var tokens = formula.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Could be a simple value too - check for it
            FormulaEvaluator evaluator = new FormulaEvaluator(tokens);

            if (!evaluator.HasCellReference())
            {
                var value = evaluator.Evaluate();

                SetCellValue(cell, value);
                cell.Formula = new[] { formula };
            }
            else
            {
                cells[row, col].Value = null;
                SetCellFormula(row, col, tokens);
            }
        }

        public void SetCellValue(GraphNode cell, double? value)
        {
            if (!value.HasValue)
            {
                cell.Value = null;
                return;
            }

            cell.Value = value;
            cell.Observers.Notify();
        }

        // Gets the position (x,y) when cell identifier is specified
        // Validates the input
        public (uint, uint) GetPositionFromId(string id)
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
            colId--;

            if (rowId >= Height || colId >= Width)
                throw new CellRangeOutOfBoundsException(rowId + 1, colId + 1);

            return (rowId, colId);
        }

        // Privates

        // In case the value provided is a formula, 
        // extend the edges of the graph and check for 
        // cyclic dependencies
        private void SetCellFormula(uint row, uint col, string[] tokens)
        {
            var thisCell = cells[row, col];

            cells[row, col].Formula = tokens;

            foreach (var token in tokens)
            {
                // Quickly process cyclic dependency during insertion itself
                // so that formula evaluation happens faster
                if (char.IsLetter(token[0]))
                {
                    var (formulaRow, formulaCol) = GetPositionFromId(token);
                    var connectionCell = cells[formulaRow, formulaCol];

                    if (connectionCell == null)
                    {
                        connectionCell = CreateCell(token, formulaRow, formulaCol);
                        cells[formulaRow, formulaCol] = connectionCell;
                    }

                    AddEdge(thisCell, connectionCell);
                }
            }
        }

        // Adds a directed edge to the cell
        // Tradeoff decision - speed vs space - prefer speed as this space is shortlived and temporary
        private void AddEdge(GraphNode from, GraphNode to)
        {
            if (from.Name == to.Name)
                throw new CyclicDependencyException($"{from.Name}");

            from.Edges.Add(to);            
            to.Observers.Add(from);

            CheckForCyclicDependencies(from, null);

            to.Observers.NotifyNode(from); //Notify the newly added observer inorder to force a recalculate
        }

        // Walk through dependent nodes and check each dependent node for dependencies        
        private void CheckForCyclicDependencies(GraphNode sourceCell, GraphNode recurseCell)
        {
            if (recurseCell == null)
                recurseCell = sourceCell;

            foreach (var node in recurseCell.Edges)
            {
                if (node.Name == sourceCell.Name)
                    throw new CyclicDependencyException($"{sourceCell.Name}");

                CheckForCyclicDependencies(sourceCell, node);
            }
        }
    }
}
