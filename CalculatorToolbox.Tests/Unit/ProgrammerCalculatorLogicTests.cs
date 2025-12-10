using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorToolbox.Logic;
using System;

namespace CalculatorToolbox.Tests.Unit
{
    [TestClass]
    public class ProgrammerCalculatorLogicTests
    {
        private ProgrammerCalculatorLogic _calculator;

        [TestInitialize]
        public void Setup()
        {
            _calculator = new ProgrammerCalculatorLogic();
        }

        [TestMethod]
        public void ConvertBase_DecimalToHex_ReturnsCorrectValue()
        {
            // Arrange
            string value = "255";
            int fromBase = 10;
            int toBase = 16;

            // Act
            int result = _calculator.ConvertBase(value, fromBase, toBase);

            // Assert
            Assert.AreEqual(255, result); // Result is in base 16 representation
        }

        [TestMethod]
        public void ConvertBase_HexToDecimal_ReturnsCorrectValue()
        {
            // Arrange
            string value = "FF";
            int fromBase = 16;
            int toBase = 10;

            // Act
            int result = _calculator.ConvertBase(value, fromBase, toBase);

            // Assert
            Assert.AreEqual(255, result);
        }

        [TestMethod]
        public void ConvertBase_DecimalToBinary_ReturnsCorrectValue()
        {
            // Arrange
            string value = "10";
            int fromBase = 10;
            int toBase = 2;

            // Act
            int result = _calculator.ConvertBase(value, fromBase, toBase);

            // Assert
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void BitwiseAnd_TwoNumbers_ReturnsCorrectResult()
        {
            // Arrange
            int a = 12; // 1100 in binary
            int b = 10; // 1010 in binary
                        // 1000 = 8

            // Act
            int result = _calculator.BitwiseAnd(a, b);

            // Assert
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void BitwiseOr_TwoNumbers_ReturnsCorrectResult()
        {
            // Arrange
            int a = 12; // 1100 in binary
            int b = 10; // 1010 in binary
                        // 1110 = 14

            // Act
            int result = _calculator.BitwiseOr(a, b);

            // Assert
            Assert.AreEqual(14, result);
        }

        [TestMethod]
        public void BitwiseXor_TwoNumbers_ReturnsCorrectResult()
        {
            // Arrange
            int a = 12; // 1100 in binary
            int b = 10; // 1010 in binary
                        // 0110 = 6

            // Act
            int result = _calculator.BitwiseXor(a, b);

            // Assert
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void BitwiseNot_PositiveNumber_ReturnsComplement()
        {
            // Arrange
            int a = 5; // 0101 in binary
                       // NOT = ...11111010 = -6

            // Act
            int result = _calculator.BitwiseNot(a);

            // Assert
            Assert.AreEqual(-6, result);
        }

        [TestMethod]
        public void ShiftLeft_ByOne_DoublesValue()
        {
            // Arrange
            int a = 5; // 0101 in binary
                       // << 1 = 1010 = 10

            // Act
            int result = _calculator.ShiftLeft(a, 1);

            // Assert
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void ShiftRight_ByOne_HalvesValue()
        {
            // Arrange
            int a = 10; // 1010 in binary
                        // >> 1 = 0101 = 5

            // Act
            int result = _calculator.ShiftRight(a, 1);

            // Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void ByteSize_8Bit_TruncatesCorrectly()
        {
            // Arrange
            int value = 300; // Exceeds 8-bit range (0-255)
            int bytes = 1; // 8 bits

            // Act
            int result = _calculator.ByteSize(value, bytes);

            // Assert
            Assert.AreEqual(44, result); // 300 % 256 = 44
        }

        [TestMethod]
        public void ByteSize_16Bit_TruncatesCorrectly()
        {
            // Arrange
            int value = 70000; // Exceeds 16-bit range (0-65535)
            int bytes = 2; // 16 bits

            // Act
            int result = _calculator.ByteSize(value, bytes);

            // Assert
            Assert.AreEqual(4464, result); // 70000 % 65536 = 4464
        }

        [TestMethod]
        public void AddHistory_SingleEntry_StoresCorrectly()
        {
            // Arrange
            string expression = "12 AND 10";
            string result = "8";

            // Act
            _calculator.AddHistory(expression, result);
            var history = _calculator.GetHistory();

            // Assert
            Assert.AreEqual(1, history.Count);
            Assert.AreEqual(expression, history[0].Expression);
            Assert.AreEqual(result, history[0].Result);
        }

        [TestMethod]
        public void GetHistory_MultipleEntries_ReturnsAllEntries()
        {
            // Arrange
            _calculator.AddHistory("12 AND 10", "8");
            _calculator.AddHistory("12 OR 10", "14");
            _calculator.AddHistory("12 XOR 10", "6");

            // Act
            var history = _calculator.GetHistory();

            // Assert
            Assert.AreEqual(3, history.Count);
        }
    }
}