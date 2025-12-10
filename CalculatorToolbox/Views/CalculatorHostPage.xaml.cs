using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CalculatorToolbox.Views
{
    public sealed partial class CalculatorHostPage : Page
    {
        public CalculatorHostPage()
        {
            InitializeComponent();
        }

        public void SwitchCalculator(string calculatorType)
        {
            // 隱藏所有計算器
            StandardCalculatorView.Visibility = Visibility.Collapsed;
            ScientificCalculatorView.Visibility = Visibility.Collapsed;
            ProgrammerCalculatorView.Visibility = Visibility.Collapsed;
            
            // 顯示選中的計算器
            switch (calculatorType)
            {
                case "Standard":
                case "標準":
                    StandardCalculatorView.Visibility = Visibility.Visible;
                    break;
                case "Scientific":
                case "科學":
                    ScientificCalculatorView.Visibility = Visibility.Visible;
                    break;
                case "Programmer":
                case "程式設計人員":
                    ProgrammerCalculatorView.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}