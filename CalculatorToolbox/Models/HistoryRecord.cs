using System;

namespace CalculatorToolbox.Models
{
    public class HistoryRecord
    {
        public required string Expression { get; set; }
        public required string Result { get; set; }
    }
}