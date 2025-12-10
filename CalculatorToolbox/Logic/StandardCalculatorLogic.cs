using System;
using System.Collections.Generic;
using CalculatorToolbox.Logic.Models;

namespace CalculatorToolbox.Logic
{
    public class StandardCalculatorLogic
    {
        private readonly List<HistoryRecord> history = new();

        public string Calculate(string expression)
        {
            double result = 0;
            try
            {
                var tokens = expression.Split(' ');
                double a = double.Parse(tokens[0]);
                string op = tokens[1];
                double b = double.Parse(tokens[2]);
                switch (op)
                {
                    case "+": result = a + b; break;
                    case "-": result = a - b; break;
                    case "*": result = a * b; break;
                    case "/": result = b != 0 ? a / b : 0; break;
                }
                var record = new HistoryRecord
                {
                    Expression = expression,
                    Result = result.ToString()
                };
                history.Add(record);
                return result.ToString();
            }
            catch
            {
                return "Error";
            }
        }

        public IReadOnlyList<HistoryRecord> GetHistory()
        {
            return history.AsReadOnly();
        }
    }
}