using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Tests
{
    [TestClass]
    public class SystemTest
    {
        [TestMethod]
        public void GivenSampleInput_ShouldExecuteCorrectly()
        {
            // arrange
            var source = @" 3 2
                            A2
                            4 5 *
                            A1
                            A1 B2 / 2 +
                            3
                            39 B1 B2 * /";

            // act
            var spreadSheet = new Spreadsheet();
            spreadSheet.Run(source);

            // assert
            Assert.AreEqual(3u, spreadSheet.CurrentSheet.Width);
            Assert.AreEqual(2u, spreadSheet.CurrentSheet.Height);

            Assert.AreEqual(20, Math.Round((double)spreadSheet.CurrentSheet.GetCell("A1").Value, 5));
            Assert.AreEqual(20, Math.Round((double)spreadSheet.CurrentSheet.GetCell("A2").Value, 5));
            Assert.AreEqual(20, Math.Round((double)spreadSheet.CurrentSheet.GetCell("A3").Value, 5));

            Assert.AreEqual(8.66667, Math.Round((double)spreadSheet.CurrentSheet.GetCell("B1").Value, 5));
            Assert.AreEqual(3, Math.Round((double)spreadSheet.CurrentSheet.GetCell("B2").Value, 5));
            Assert.AreEqual(1.5, Math.Round((double)spreadSheet.CurrentSheet.GetCell("B3").Value, 5));
        }

        [TestMethod]
        public void SampleInputHasNegativeNumbers_ShouldExecuteCorrectly()
        {
            // arrange
            var source = @" 3 2
                            A2
                            -4 5 *
                            A1
                            A1 B2 / 2 +
                            3
                            39 B1 B2 * /";

            // act
            var spreadSheet = new Spreadsheet();
            spreadSheet.Run(source);

            // assert
            Assert.AreEqual(3u, spreadSheet.CurrentSheet.Width);
            Assert.AreEqual(2u, spreadSheet.CurrentSheet.Height);

            Assert.AreEqual(-20, Math.Round((double)spreadSheet.CurrentSheet.GetCell("A1").Value, 5));
            Assert.AreEqual(-20, Math.Round((double)spreadSheet.CurrentSheet.GetCell("A2").Value, 5));
            Assert.AreEqual(-20, Math.Round((double)spreadSheet.CurrentSheet.GetCell("A3").Value, 5));

            Assert.AreEqual(-4.66667, Math.Round((double)spreadSheet.CurrentSheet.GetCell("B1").Value, 5));
            Assert.AreEqual(3, Math.Round((double)spreadSheet.CurrentSheet.GetCell("B2").Value, 5));
            Assert.AreEqual(-2.78571, Math.Round((double)spreadSheet.CurrentSheet.GetCell("B3").Value, 5));
        }

        [TestMethod]
        public void SampleInputHasUnaryOperators_ShouldExecuteCorrectly()
        {
            // arrange
            var source = @" 3 1
                            A2 ++
                            4 5 *
                            A1 --";

            // act
            var spreadSheet = new Spreadsheet();
            spreadSheet.Run(source);

            // assert
            Assert.AreEqual(3u, spreadSheet.CurrentSheet.Width);
            Assert.AreEqual(1u, spreadSheet.CurrentSheet.Height);

            Assert.AreEqual(21, Math.Round((double)spreadSheet.CurrentSheet.GetCell("A1").Value, 5));
            Assert.AreEqual(20, Math.Round((double)spreadSheet.CurrentSheet.GetCell("A2").Value, 5));
            Assert.AreEqual(20, Math.Round((double)spreadSheet.CurrentSheet.GetCell("A3").Value, 5));
        }

        [TestMethod]
        public void SampleInputHasInsufficientEntries_ShouldSetMissingValuesAsNull()
        {
            // arrange
            var source = @" 3 1
                            4 ++";

            // act
            var spreadSheet = new Spreadsheet();
            spreadSheet.Run(source);

            // assert
            Assert.AreEqual(3u, spreadSheet.CurrentSheet.Width);
            Assert.AreEqual(1u, spreadSheet.CurrentSheet.Height);

            Assert.AreEqual(5, Math.Round((double)spreadSheet.CurrentSheet.GetCell("A1").Value, 5));
            Assert.AreEqual(null, spreadSheet.CurrentSheet.GetCell("A2").Value);
            Assert.AreEqual(null, spreadSheet.CurrentSheet.GetCell("A3").Value);
        }


        [TestMethod]
        public void SampleInputHasCyclicDependency_ShouldExecuteCorrectly()
        {
            // arrange
            var source = @" 3 2
                            A3
                            4 5 *
                            A1
                            A1 B2 / 2 +
                            3
                            39 B1 B2 * /";

            // act and assert
            var spreadSheet = new Spreadsheet();
            
            Assert.ThrowsException<CyclicDependencyException>(() => spreadSheet.Run(source));
        }

        [TestMethod]
        public void SampleInputRefersToMissingCells_ShouldSetThatReferenceCellAsWellAsSourceCellToNull()
        {
            // arrange
            var source = @" 3 1
                            A2";

            // act
            var spreadSheet = new Spreadsheet();
            spreadSheet.Run(source);

            // assert
            Assert.AreEqual(3u, spreadSheet.CurrentSheet.Width);
            Assert.AreEqual(1u, spreadSheet.CurrentSheet.Height);

            Assert.AreEqual(null, spreadSheet.CurrentSheet.GetCell("A1").Value);
            Assert.AreEqual(null, spreadSheet.CurrentSheet.GetCell("A2").Value);
        }

    }
}
