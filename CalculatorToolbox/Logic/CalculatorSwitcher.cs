namespace CalculatorToolbox.Logic
{
    public class CalculatorSwitcher
    {
        public string? CurrentType { get; private set; }

        public void ShowCalculator(string type)
        {
            CurrentType = type;
        }
    }
}