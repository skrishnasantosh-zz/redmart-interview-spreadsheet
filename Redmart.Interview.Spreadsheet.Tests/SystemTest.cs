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
    }
}
