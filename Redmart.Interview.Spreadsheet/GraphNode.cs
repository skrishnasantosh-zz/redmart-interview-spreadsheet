using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class GraphNode : IObservable
    {        
        private double? doubleVal;
        private List<string> resolvedFormula;
        
        public GraphNode(WorkSheetGraph root, string id, uint row, uint col)
        {
            Edges = new List<GraphNode>();

            Name = id;
            Position = (row, col);

            resolvedFormula = new List<string>();

            Observers = new GraphNodeObserver(this);
            Parent = root;
        }

        public (uint, uint) Position { get; set; }

        public string Name { get; set; }

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

        public IEnumerable<string> Formula { get; set; }

        public WorkSheetGraph Parent { get;  }

        public IEnumerable<string> ResolvedFormula => resolvedFormula;

        public IList<GraphNode> Edges { get; }

        public GraphNodeObserver Observers { get; }

        void IObservable.OnNotify(GraphNode updatedNode)
        {
            resolvedFormula = new List<string>(Formula);
            var formula = Formula.ToArray();

            for (var i = 0; i < formula.Count(); i++)
            {
                if (formula[i].ToLower() == updatedNode.Name.ToLower() &&
                    updatedNode.Value != null)                
                    resolvedFormula[i] = Convert.ToString(updatedNode.Value);                
                else
                    resolvedFormula[i] = formula[i];
            }

            EvaluateCell();
        }

        private void EvaluateCell()
        {
            var evaluator = new FormulaEvaluator(ResolvedFormula);
            var result = evaluator.Evaluate();

            Value = result;
        }
    }
}
