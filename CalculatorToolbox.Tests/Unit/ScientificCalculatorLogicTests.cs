using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorToolbox.Logic;
using System;

namespace CalculatorToolbox.Tests.Unit
{
    [TestClass]
    public class ScientificCalculatorLogicTests
    {
        private ScientificCalculatorLogic _calculator;
        private const double Tolerance = 0.0001;

        [TestInitialize]
        public void Setup()
        {
            _calculator = new ScientificCalculatorLogic();
        }

        [TestMethod]
        public void Sin_InDegreeMode_ReturnsCorrectValue()
        {
            // Arrange
            _calculator.SetMode(ScientificCalculatorLogic.AngleMode.Degree);
            double angle = 30; // sin(30°) = 0.5

            // Act
            double result = _calculator.Sin(angle);

            // Assert
            Assert.AreEqual(0.5, result, Tolerance);
        }

        [TestMethod]
        public void Sin_InRadianMode_ReturnsCorrectValue()
        {
            // Arrange
            _calculator.SetMode(ScientificCalculatorLogic.AngleMode.Radian);
            double angle = Math.PI / 6; // sin(π/6) = 0.5

            // Act
            double result = _calculator.Sin(angle);

            // Assert
            Assert.AreEqual(0.5, result, Tolerance);
        }

        [TestMethod]
        public void Cos_InDegreeMode_ReturnsCorrectValue()
        {
            // Arrange
            _calculator.SetMode(ScientificCalculatorLogic.AngleMode.Degree);
            double angle = 60; // cos(60°) = 0.5

            // Act
            double result = _calculator.Cos(angle);

            // Assert
            Assert.AreEqual(0.5, result, Tolerance);
        }

        [TestMethod]
        public void Tan_InDegreeMode_ReturnsCorrectValue()
        {
            // Arrange
            _calculator.SetMode(ScientificCalculatorLogic.AngleMode.Degree);
            double angle = 45; // tan(45°) = 1

            // Act
            double result = _calculator.Tan(angle);

            // Assert
            Assert.AreEqual(1.0, result, Tolerance);
        }

        [TestMethod]
        public void Log_ValidInput_ReturnsCorrectValue()
        {
            // Arrange
            double value = 100; // log10(100) = 2

            // Act
            double result = _calculator.Log(value);

            // Assert
            Assert.AreEqual(2.0, result, Tolerance);
        }

        [TestMethod]
        public void Exp_ValidInput_ReturnsCorrectValue()
        {
            // Arrange
            double value = 1; // e^1 = e

            // Act
            double result = _calculator.Exp(value);

            // Assert
            Assert.AreEqual(Math.E, result, Tolerance);
        }

        [TestMethod]
        public void Factorial_Zero_ReturnsOne()
        {
            // Act
            double result = _calculator.Factorial(0);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Factorial_PositiveNumber_ReturnsCorrectValue()
        {
            // Act
            double result = _calculator.Factorial(5); // 5! = 120

            // Assert
            Assert.AreEqual(120, result);
        }

        [TestMethod]
        public void Factorial_NegativeNumber_ReturnsZero()
        {
            // Act
            double result = _calculator.Factorial(-5);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void AddHistory_SingleEntry_StoresCorrectly()
        {
            // Arrange
            string expression = "sin(30)";
            string result = "0.5";

            // Act
            _calculator.AddHistory(expression, result);
            var history = _calculator.GetHistory();

            // Assert
            Assert.AreEqual(1, history.Count);
            Assert.AreEqual(expression, history[0].Expression);
            Assert.AreEqual(result, history[0].Result);
        }

        [TestMethod]
        public void SetMode_ChangesToRadian_ModeChanges()
        {
            // Act
            _calculator.SetMode(ScientificCalculatorLogic.AngleMode.Radian);

            // Assert
            Assert.AreEqual(ScientificCalculatorLogic.AngleMode.Radian, _calculator.Mode);
        }

        [TestMethod]
        public void SetMode_ChangesToDegree_ModeChanges()
        {
            // Arrange
            _calculator.SetMode(ScientificCalculatorLogic.AngleMode.Radian);

            // Act
            _calculator.SetMode(ScientificCalculatorLogic.AngleMode.Degree);

            // Assert
            Assert.AreEqual(ScientificCalculatorLogic.AngleMode.Degree, _calculator.Mode);
        }
    }
}