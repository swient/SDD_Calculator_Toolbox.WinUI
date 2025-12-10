using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorToolbox.Logic;
using System;

namespace CalculatorToolbox.Tests.Unit
{
    [TestClass]
    public class StandardCalculatorLogicTests
    {
        private StandardCalculatorLogic _calculator;

        [TestInitialize]
        public void Setup()
        {
            _calculator = new StandardCalculatorLogic();
        }

        [TestMethod]
        public void Calculate_Addition_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "5 + 3";

            // Act
            string result = _calculator.Calculate(expression);

            // Assert
            Assert.AreEqual("8", result);
        }

        [TestMethod]
        public void Calculate_Subtraction_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "10 - 4";

            // Act
            string result = _calculator.Calculate(expression);

            // Assert
            Assert.AreEqual("6", result);
        }

        [TestMethod]
        public void Calculate_Multiplication_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "7 * 6";

            // Act
            string result = _calculator.Calculate(expression);

            // Assert
            Assert.AreEqual("42", result);
        }

        [TestMethod]
        public void Calculate_Division_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "20 / 4";

            // Act
            string result = _calculator.Calculate(expression);

            // Assert
            Assert.AreEqual("5", result);
        }

        [TestMethod]
        public void Calculate_DivisionByZero_ReturnsZero()
        {
            // Arrange
            string expression = "10 / 0";

            // Act
            string result = _calculator.Calculate(expression);

            // Assert
            Assert.AreEqual("0", result);
        }

        [TestMethod]
        public void Calculate_InvalidExpression_ReturnsError()
        {
            // Arrange
            string expression = "invalid";

            // Act
            string result = _calculator.Calculate(expression);

            // Assert
            Assert.AreEqual("Error", result);
        }

        [TestMethod]
        public void GetHistory_AfterCalculation_ContainsRecord()
        {
            // Arrange
            string expression = "5 + 3";
            _calculator.Calculate(expression);

            // Act
            var history = _calculator.GetHistory();

            // Assert
            Assert.AreEqual(1, history.Count);
            Assert.AreEqual(expression, history[0].Expression);
            Assert.AreEqual("8", history[0].Result);
        }

        [TestMethod]
        public void GetHistory_MultipleCalculations_ContainsAllRecords()
        {
            // Arrange
            _calculator.Calculate("5 + 3");
            _calculator.Calculate("10 - 2");
            _calculator.Calculate("4 * 5");

            // Act
            var history = _calculator.GetHistory();

            // Assert
            Assert.AreEqual(3, history.Count);
        }
    }
}