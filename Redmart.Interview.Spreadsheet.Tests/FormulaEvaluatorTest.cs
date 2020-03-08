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
            var operatorFactory = OperatorStrategy.Instance;
        }

        [TestMethod]
        public void WhenSimpleFormula_ShouldEvaluateCorrectly()
        {
            // arrange
            var formula = "1 2 +";

            // act
            var tokens = formula.Split(' ');
            var eval = new FormulaEvaluator(tokens);
            var result = eval.Evaluate();

            // assert

            Assert.AreEqual(result, 3);
        }

        [TestMethod]
        public void WhenFormulaHasNegativeNumbers_ShouldEvaluateCorrectly()
        {
            // arrange
            var formula = "-1 2 +";

            // act
            var tokens = formula.Split(' ');
            var eval = new FormulaEvaluator(tokens);
            var result = eval.Evaluate();

            // assert

            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void WhenSimpleFormulaForNegativeResult_ShouldEvaluateCorrectly()
        {
            // arrange 
            var formula = "1 2 -";

            // act
            var tokens = formula.Split(' ');
            var eval = new FormulaEvaluator(tokens);
            var result = eval.Evaluate();

            // assert
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void WhenComplexFormula_ShouldEvaluateCorrectly()
        {
            // arrange
            var formula = "15 7 1 1 + - / 3 * 2 1 1 + + -";

            // act
            var tokens = formula.Split(' ');

            var eval = new FormulaEvaluator(tokens);
            var result = eval.Evaluate();

            // assert
            Assert.AreEqual(result, 5);
        }

        [TestMethod]
        public void WhenUnaryOperatorsAreSpecified_ShouldEvaluateCorrectly()
        {
            // arrange
            var formula = "15 ++ 7 + --";

            // act
            var tokens = formula.Split(' ');

            var eval = new FormulaEvaluator(tokens);
            var result = eval.Evaluate();

            // assert
            Assert.AreEqual(result, 22);
        }

        [TestMethod]
        public void WhenSimpleFormulaWithCellReference_ShouldEvaluateCorrectlyBySubstitutingWithZero()
        {
            // arrange
            var formula = "1 A1 + ";

            // act
            var tokens = formula.Split(' ');

            var eval = new FormulaEvaluator(tokens);
            var result = eval.Evaluate();

            // assert
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void WhenInvalidOperatorIsProvided_ShouldThrowOperatorNotFoundException()
        {
            // arrange
            var formula = "1 2 & ";

            // act
            var tokens = formula.Split(' ');
            var eval = new FormulaEvaluator(tokens);

            // assert
            Assert.ThrowsException<OperatorNotFoundException>(() => eval.Evaluate());
        }

        [TestMethod]
        public void WhenInvalidOperandsAreProvided_ShouldThrowFormulaEvaluatorException()
        {
            // arrange
            var formula = "1 +";

            // act
            var tokens = formula.Split(' ');
            var eval = new FormulaEvaluator(tokens);

            // assert
            Assert.ThrowsException<FormulaEvaluatorException>(() => eval.Evaluate());
        }
    }
}
