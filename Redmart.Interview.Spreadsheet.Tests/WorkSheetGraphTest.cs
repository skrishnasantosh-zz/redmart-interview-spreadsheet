using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Redmart.Interview.Spreadsheet.UnitTests
{
    [TestClass]
    public class WorkSheetGraphTest
    {
        // Worksheet Graph Creation - Bounds test
        [TestMethod]
        public void WhenBoundsAreGiven_BoundAccessShouldBeWorking()
        {
            // arrange
            var workSheet = new WorkSheetGraph(new Spreadsheet(), 3, 3);

            // act            
            var rowTestCell = workSheet.GetCell("C1"); //Should Succeed in getting an empty cell
            var colTestCell = workSheet.GetCell("A3");

            // assert
            Assert.IsNotNull(rowTestCell);
        }

        // Worksheet Graph Creation - Bounds test
        [TestMethod]
        public void WhenBoundsAreBeyondWorksheet_ThrowsCellRangeOutOfBoundsException()
        {
            // arrange
            var workSheet = new WorkSheetGraph(new Spreadsheet(), 3, 3);

            // act and assert
            Assert.ThrowsException<CellRangeOutOfBoundsException>(
                () => workSheet.GetCell("D1")
                );

            Assert.ThrowsException<CellRangeOutOfBoundsException>(
                () => workSheet.GetCell("A4")
                );
        }

        // Graph Edge creation 
        [TestMethod]
        public void WhenSimpleFormulaIsPassed_AdjacacencyEdgesArePopulatedCorrectly()
        {
            // arrange
            var workSheet = new WorkSheetGraph(new Spreadsheet(), 3, 3);

            // act
            workSheet.SetCellFormula("A1", "A2 B2 / 2 +");
            var cell = workSheet.GetCell("A1");

            // assert
            Assert.AreEqual(2, cell.Edges.Count);
            Assert.AreEqual("A2", cell.Edges[0].Name);
            Assert.AreEqual("B2", cell.Edges[1].Name);
        }

        // Graph Edge creation 
        [TestMethod]
        public void WhenNumericFormulaIsPassed_AdjacacencyEdgesShouldNotPopulate()
        {
            // arrange
            var workSheet = new WorkSheetGraph(new Spreadsheet(), 3, 3);

            // act
            workSheet.SetCellFormula("A1", "A2");
            workSheet.SetCellFormula("A2", "4 5 *");
            
            var cellA1 = workSheet.GetCell("A1");
            var cellA2 = workSheet.GetCell("A2");

            // assert
            Assert.AreEqual(1, cellA1.Edges.Count);
            Assert.AreEqual("A2", cellA1.Edges[0].Name);

            Assert.AreEqual(0, cellA2.Edges.Count);
        }

        // Graph Edge creation 
        [TestMethod]
        public void WhenMultiLevelFormulaIsPassed_AdjacacencyEdgesArePopulatedCorrectly()
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

            // assert
            Assert.AreEqual(2, cellA1.Edges.Count);
            Assert.AreEqual(2, cellA2.Edges.Count);
            Assert.AreEqual(2, cellA3.Edges.Count);

            Assert.AreEqual(0, cellA4.Edges.Count);
            Assert.AreEqual(0, cellB2.Edges.Count);

            Assert.AreEqual("A2", cellA1.Edges[0].Name);
            Assert.AreEqual("B2", cellA1.Edges[1].Name);

            Assert.AreEqual("A3", cellA2.Edges[0].Name);
            Assert.AreEqual("B2", cellA2.Edges[1].Name);

            Assert.AreEqual("A4", cellA3.Edges[0].Name);
            Assert.AreEqual("B2", cellA3.Edges[1].Name);
        }

        // Cyclic Dependencies
        [TestMethod]        
        public void WhenFormulaHasCyclicDepedency_ThrowsCyclicDependencyException()
        {            
            // arrange
            var workSheet = new WorkSheetGraph(new Spreadsheet(), 3, 3);

            // act and assert
            workSheet.SetCellFormula("A1", "A2 B2 / 2 +");

            Assert.ThrowsException<CyclicDependencyException>(
                () => workSheet.SetCellFormula("A2", "A1 B2 / 2 +")
                );

        }

        [TestMethod]        
        public void WhenFormulaHasMultiLevelCyclicDepedency_CyclicDependencyExceptionIsThrown()
        {
            // arrange
            var workSheet = new WorkSheetGraph(new Spreadsheet(), 5, 5);

            // act and assert
            workSheet.SetCellFormula("A1", "A2 B2 / 2 +");
            workSheet.SetCellFormula("A2", "A3 B2 / 2 +");

            Assert.ThrowsException<CyclicDependencyException>(
                () => workSheet.SetCellFormula("A3", "A1 B2 / 2 +")
                );

        }


        [TestMethod]
        public void WhenFormulaIsComplexButNoCyclicDepedency_NoExceptionShouldBeThrown()
        {
            // arrange
            var workSheet = new WorkSheetGraph(new Spreadsheet(), 5, 5);

            // act and assert (given input)
            workSheet.SetCellFormula("A1", "A2");
            workSheet.SetCellFormula("A2", "4 5 *");
            workSheet.SetCellFormula("A3", "A1");
            workSheet.SetCellFormula("B1", "A1 B2 / 2 +");
            workSheet.SetCellFormula("B2", "3");
            workSheet.SetCellFormula("B3", "39 B1 B2 * /");

            // no assert needed as the test is to check if exception is not raised.
        }

        // Cyclic Dependency - should not throw
        [TestMethod]
        public void WhenFormulaHasNoCyclicDepedency_NoExceptionShouldBeThrown()
        {
            // arrange
            var workSheet = new WorkSheetGraph(new Spreadsheet(), 5, 5);

            // act 
            workSheet.SetCellFormula("A1", "A2 B2 / 2 +");
            workSheet.SetCellFormula("A2", "A3 B2 / 2 +");
            workSheet.SetCellFormula("A3", "A4 B2 / 2 +");

            // no assert needed as the test is to check if exception is not raised.
        }
    }
}
