using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class CellNode
    {
        public CellNode(string id, uint row, uint col)
        {
            Edges = new List<CellNode>();

            Name = id;
            Position = (row, col);
        }

        public (uint, uint) Position { get; set; }

        public string Name { get; set; }

        public double? Value { get; set; }

        public string Formula { get; set; }

        public IList<CellNode> Edges { get; }
    }
}
