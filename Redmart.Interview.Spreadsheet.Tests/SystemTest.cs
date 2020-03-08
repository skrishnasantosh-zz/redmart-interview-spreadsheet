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
        public void GivenSampleInputTest()
        {
            var source = @" 3 2
                            A2
                            4 5 *
                            A1
                            A1 B2 / 2 +
                            3
                            39 B1 B2 * /";

            var spreadSheet = new Spreadsheet();
            spreadSheet.Run(source);
        }
    }
}
