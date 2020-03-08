using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public class GraphNodeObserver 
    {
        private readonly List<GraphNode> observers = new List<GraphNode>();
        private readonly GraphNode parent;

        public GraphNodeObserver(GraphNode parent)
        {
            this.parent = parent;
        }

        public void Add(GraphNode node)
        {
            observers.Add(node);
        }

        public IReadOnlyCollection<GraphNode> GetAll()
        {
            return observers;
        }

        public void Notify()
        {
            foreach (var observer in observers)
            {
                var observable = observer as IObservable;
                observable.OnNotify(parent);
            }
        }
    }
}
