using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.UnitTests
{
    [TestClass]
    public class GraphNodeObserverTest
    {
        [TestMethod]
        public void WhenGraphNodeIsDependentOnOther_ObserverIsSetCorrectly()
        {
            // arrange
            var workSheet = new WorkSheetGraph(new Spreadsheet(), 2, 4);

            // act
            workSheet.SetCellFormula("A1", "A2 B2 / 2 +");
            workSheet.SetCellFormula("A2", "A3 B2 / 2 +");
            workSheet.SetCellFormula("A3", "A4 B2 / 2 +");

            var cellA1 = workSheet.GetCell("A1");
            var cellA2 = workSheet.GetCell("A2");
            var cellA3 = workSheet.GetCell("A3");
            var cellA4 = workSheet.GetCell("A4");
            var cellB2 = workSheet.GetCell("B2");
                        
            var observersB2 = cellB2.Observers.GetAll();
            var observersA4 = cellA4.Observers.GetAll();
            var observersA3 = cellA3.Observers.GetAll();
            var observersA2 = cellA2.Observers.GetAll();
            var observersA1 = cellA1.Observers.GetAll();

            // assert
            Assert.AreEqual(observersB2.Count, 3);
            Assert.AreEqual(observersA1.Count, 0);
            Assert.AreEqual(observersA2.Count, 1);
            Assert.AreEqual(observersA3.Count, 1);
            Assert.AreEqual(observersA4.Count, 1);

        }

        [TestMethod]
        public void WhenGraphNodeIsDependentOnOther_ObserverIsEvaluatedCorrectly()
        {
            // arrange
            var workSheet = new WorkSheetGraph(new Spreadsheet(), 2, 4);

            // act
            workSheet.SetCellFormula("A1", "A2");
            workSheet.SetCellFormula("A2", "10");
            
            var cellA1 = workSheet.GetCell("A1");
            var cellA2 = workSheet.GetCell("A2");
            
            var observersA2 = cellA2.Observers.GetAll();
            var observersA1 = cellA1.Observers.GetAll();


            // assert
            Assert.AreEqual(0, observersA1.Count);
            Assert.AreEqual(1, observersA2.Count);

            Assert.AreEqual(10, cellA2.Value);
            Assert.AreEqual(10, cellA1.Value);
        }
    }
}
