using System;
using System.Collections.Generic;
using CalculatorToolbox.Logic.Models;

namespace CalculatorToolbox.Logic
{
    public class ProgrammerCalculatorLogic
    {
        private readonly List<HistoryRecord> history = new();

        public int ConvertBase(string value, int fromBase, int toBase)
        {
            int number = Convert.ToInt32(value, fromBase);
            return Convert.ToInt32(Convert.ToString(number, toBase), toBase);
        }

        public int BitwiseAnd(int a, int b) => a & b;
        public int BitwiseOr(int a, int b) => a | b;
        public int BitwiseXor(int a, int b) => a ^ b;
        public int BitwiseNot(int a) => ~a;
        public int ShiftLeft(int a, int n) => a << n;
        public int ShiftRight(int a, int n) => a >> n;

        public int ByteSize(int value, int bytes)
        {
            int mask = (1 << (bytes * 8)) - 1;
            return value & mask;
        }

        public void AddHistory(string expr, string result)
        {
            history.Add(new HistoryRecord
            {
                Expression = expr,
                Result = result
            });
        }

        public IReadOnlyList<HistoryRecord> GetHistory() => history.AsReadOnly();
    }
}