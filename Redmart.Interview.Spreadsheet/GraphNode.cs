using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class GraphNode : IObservable
    {        
        private double? doubleVal;
        private List<string> formula;
        
        public GraphNode(WorkSheetGraph root, string id, uint row, uint col)
        {
            Edges = new List<GraphNode>();

            Name = id;
            Position = (row, col);

            formula = new List<string>();

            Observers = new GraphNodeObserver(this);
            Parent = root;
        }

        public (uint, uint) Position { get; set; }

        public string Name { get; set; }

        public WorkSheetGraph Parent { get;  }

        public IList<string> ResolvedFormula { get; set; }

        public IList<GraphNode> Edges { get; }

        public GraphNodeObserver Observers { get; }

        public IEnumerable<string> Formula
        {
            get
            {
                return formula;
            }

            set
            {
                formula = value.ToList();
                ResolvedFormula = value.ToArray();
            }
        }

        public double? Value
        {
            get
            {
                return doubleVal;
            }
            set
            {
                doubleVal = value;
                Observers.Notify();
            }
        }

        private void Evaluate()
        {
            var evaluator = new FormulaEvaluator(ResolvedFormula);

            if (evaluator.HasCellReference())
                return;

            var result = evaluator.Evaluate();

            Value = result;
        }

        public void OnNotify(GraphNode updatedNode)
        {
            var formula = Formula.ToArray();

            // update only the affected node
            for (var i = 0; i < formula.Count(); i++)
            {
                if (formula[i].ToLower() == updatedNode.Name.ToLower() &&
                    updatedNode.Value != null)                
                    ResolvedFormula[i] = Convert.ToString(updatedNode.Value);
            }

            Evaluate();
        }        
    }
}
