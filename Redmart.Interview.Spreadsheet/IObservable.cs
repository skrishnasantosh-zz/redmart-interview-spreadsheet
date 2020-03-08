using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet
{
    public interface IObservable
    {
        void OnNotify(GraphNode updatedNode);
    }
}
