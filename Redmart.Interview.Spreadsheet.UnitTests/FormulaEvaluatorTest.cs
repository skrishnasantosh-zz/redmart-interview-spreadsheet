using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.UnitTests
{
    [TestClass]
    public class FormulaEvaluatorTest
    {
        [TestInitialize]
        public void Initialize()
        {
            var operatorFactory = OperatorFactory.Instance;
        }

        [TestMethod]
        public void SimpleFormula_ShouldEvaluateCorrectly()
        {
            var formula = "1 2 +";
            var tokens = formula.Split(' ');

            var eval = new FormulaEvaluator(tokens);
            var result = eval.Evaluate();

            Assert.AreEqual(result, 3);
        }

        [TestMethod]
        public void SimpleFormulaForNegativeResult_ShouldEvaluateCorrectly()
        {
            var formula = "1 2 -";
            var tokens = formula.Split(' ');

            var eval = new FormulaEvaluator(tokens);
            var result = eval.Evaluate();

            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void ComplexFormula_ShouldEvaluateCorrectly()
        {
            var formula = "15 7 1 1 + - / 3 * 2 1 1 + + -";
            var tokens = formula.Split(' ');

            var eval = new FormulaEvaluator(tokens);
            var result = eval.Evaluate();

            Assert.AreEqual(result, 5);
        }

        [TestMethod]
        public void SimpleFormulaWithCellReference_ShouldEvaluateCorrectlyBySubstitutingWithZero()
        {
            var formula = "1 A1 + ";
            var tokens = formula.Split(' ');

            var eval = new FormulaEvaluator(tokens);
            var result = eval.Evaluate();

            Assert.AreEqual(result, 1);
        }
    }
}
