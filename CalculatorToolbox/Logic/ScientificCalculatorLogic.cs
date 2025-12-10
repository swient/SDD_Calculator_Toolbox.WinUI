using System;
using System.Collections.Generic;
using CalculatorToolbox.Logic.Models;

namespace CalculatorToolbox.Logic
{
    public class ScientificCalculatorLogic
    {
        public enum AngleMode { Degree, Radian }
        public AngleMode Mode { get; set; } = AngleMode.Degree;
        private readonly List<HistoryRecord> history = new();

        public double Sin(double value) => Math.Sin(ToRadian(value));
        public double Cos(double value) => Math.Cos(ToRadian(value));
        public double Tan(double value) => Math.Tan(ToRadian(value));
        public double Sec(double value) => 1 / Math.Cos(ToRadian(value));
        public double Csc(double value) => 1 / Math.Sin(ToRadian(value));
        public double Cot(double value) => 1 / Math.Tan(ToRadian(value));
        public double Log(double value) => Math.Log10(value);
        public double Ln(double value) => Math.Log(value);
        public double Exp(double value) => Math.Exp(value);
        public double Power(double x, double y) => Math.Pow(x, y);
        public double TenPower(double value) => Math.Pow(10, value);
        public double Abs(double value) => Math.Abs(value);
        public double Sqrt(double value) => Math.Sqrt(value);
        public double Square(double value) => value * value;
        public double Reciprocal(double value) => 1 / value;
        public double Mod(double a, double b) => a % b;
        public double Negate(double value) => -value;
        public double Factorial(int n) => n < 0 ? 0 : n == 0 ? 1 : n * Factorial(n - 1);

        private double ToRadian(double value) => Mode == AngleMode.Degree ? value * Math.PI / 180 : value;

        public void SetMode(AngleMode mode) => Mode = mode;

        public void AddHistory(string expr, string result)
        {
            history.Add(new HistoryRecord
            {
                Expression = expr,
                Result = result
            });
        }

        public IReadOnlyList<HistoryRecord> GetHistory() => history.AsReadOnly();
        
        public void ClearHistory()
        {
            history.Clear();
        }
    }
}