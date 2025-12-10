using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using CalculatorToolbox.Models;

namespace CalculatorToolbox.ViewModels
{
    public enum SciOperatorType
    {
        None,
        Add,
        Subtract,
        Multiply,
        Divide,
        Power,
        Mod,
        LeftParen,
        RightParen,
        Negate,
        Square,
        Cube,
        Sqrt,
        CubeRoot,
        Reciprocal,
        Abs,
        Exp,
        Log,
        LogY,
        Ln,
        TenPower,
        TwoPower,
        Sin,
        Cos,
        Tan,
        Sec,
        Csc,
        Cot,
        Factorial,
        Pi,
        E,
        YRoot
    }

    public class ScientificCalculatorViewModel : BaseViewModel
    {
        private bool _isHistoryPanelVisible = false;
        public bool IsHistoryPanelVisible
        {
            get => _isHistoryPanelVisible;
            set => SetProperty(ref _isHistoryPanelVisible, value);
        }

        private bool _requestCloseTrigPanel;
        public bool RequestCloseTrigPanel
        {
            get => _requestCloseTrigPanel;
            set => SetProperty(ref _requestCloseTrigPanel, value);
        }

        private string _mainDisplay = "0";
        public string MainDisplay
        {
            get => _mainDisplay;
            set => SetProperty(ref _mainDisplay, value);
        }

        private string _expressionDisplay = "";
        public string ExpressionDisplay
        {
            get => _expressionDisplay;
            set => SetProperty(ref _expressionDisplay, value);
        }

        private string _currentNumber = "";
        private string _storedNumber = "";
        private SciOperatorType _currentOperator = SciOperatorType.None;
        private bool _isNewNumber = true;

        public ObservableCollection<HistoryRecord> History { get; } = new();
        public ObservableCollection<HistoryRecord> HistoryRecords { get; } = new();

        private HistoryRecord _selectedHistoryItem = new HistoryRecord { Expression = string.Empty, Result = string.Empty };
        public HistoryRecord SelectedHistoryItem
        {
            get => _selectedHistoryItem;
            set
            {
                if (SetProperty(ref _selectedHistoryItem, value) && value != null)
                {
                    UseHistoryRecord(value);
                }
            }
        }

        public ICommand NumberCommand { get; }
        public ICommand OperatorCommand { get; }
        public ICommand EqualsCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand FunctionCommand { get; }
        public ICommand MemoryStoreCommand { get; }
        public ICommand MemoryRecallCommand { get; }
        public ICommand MemoryClearCommand { get; }
        public ICommand MemoryAddCommand { get; }
        public ICommand MemorySubtractCommand { get; }
        public ICommand ToggleModeCommand { get; }
        public ICommand ShowHistoryCommand { get; }
        public ICommand SecondFunctionCommand { get; }
        public ICommand CloseHistoryCommand { get; }
        public ICommand ClearHistoryCommand { get; }

        private double _memory = 0;
        private bool _isRadianMode = false;
        public bool IsRadianMode
        {
            get => _isRadianMode;
            set
            {
                if (SetProperty(ref _isRadianMode, value))
                {
                    OnPropertyChanged(nameof(AngleModeText));
                }
            }
        }
        public string AngleModeText => IsRadianMode ? "RAD" : "DEG";

        private bool _isSecondFunction = false;
        public bool IsSecondFunction
        {
            get => _isSecondFunction;
            set
            {
                if (SetProperty(ref _isSecondFunction, value))
                {
                    OnPropertyChanged(nameof(SquareButtonText));
                    OnPropertyChanged(nameof(SquareButtonParameter));
                    OnPropertyChanged(nameof(SqrtButtonText));
                    OnPropertyChanged(nameof(SqrtButtonParameter));
                    OnPropertyChanged(nameof(PowerButtonText));
                    OnPropertyChanged(nameof(PowerButtonParameter));
                    OnPropertyChanged(nameof(TenPowerButtonText));
                    OnPropertyChanged(nameof(TenPowerButtonParameter));
                    OnPropertyChanged(nameof(LogButtonText));
                    OnPropertyChanged(nameof(LogButtonParameter));
                    OnPropertyChanged(nameof(LnButtonText));
                    OnPropertyChanged(nameof(LnButtonParameter));
                }
            }
        }

        private string _trigButtonText = "三角函數";
        public string TrigButtonText
        {
            get => _trigButtonText;
            set => SetProperty(ref _trigButtonText, value);
        }

        public string SquareButtonText => IsSecondFunction ? "x³" : "x²";
        public string SquareButtonParameter => IsSecondFunction ? "cube" : "square";
        public string SqrtButtonText => IsSecondFunction ? "³√𝑥" : "√𝑥";
        public string SqrtButtonParameter => IsSecondFunction ? "cuberoot" : "sqrt";
        public string PowerButtonText => IsSecondFunction ? "ʸ√𝑥" : "xʸ";
        public string PowerButtonParameter => IsSecondFunction ? "yroot" : "power";
        public string TenPowerButtonText => IsSecondFunction ? "2ˣ" : "10ˣ";
        public string TenPowerButtonParameter => IsSecondFunction ? "twopower" : "tenpower";
        public string LogButtonText => IsSecondFunction ? "logᵧ𝑥" : "log";
        public string LogButtonParameter => IsSecondFunction ? "logy" : "log";
        public string LnButtonText => IsSecondFunction ? "eˣ" : "ln";
        public string LnButtonParameter => IsSecondFunction ? "exp" : "ln";

        public ScientificCalculatorViewModel()
        {
            NumberCommand = new RelayCommand<string>(NumberPressed);
            OperatorCommand = new RelayCommand<string>(OperatorPressed);
            EqualsCommand = new RelayCommand<object>(_ => EqualsPressed());
            ClearCommand = new RelayCommand<string>(ClearPressed);
            FunctionCommand = new RelayCommand<string>(FunctionPressed);
            MemoryStoreCommand = new RelayCommand<object>(_ => MemoryStore());
            MemoryRecallCommand = new RelayCommand<object>(_ => MemoryRecall());
            MemoryClearCommand = new RelayCommand<object>(_ => MemoryClear());
            MemoryAddCommand = new RelayCommand<object>(_ => MemoryAdd());
            MemorySubtractCommand = new RelayCommand<object>(_ => MemorySubtract());
            ToggleModeCommand = new RelayCommand<object>(_ => IsRadianMode = !IsRadianMode);
            ShowHistoryCommand = new RelayCommand<object>(_ => IsHistoryPanelVisible = true);
            SecondFunctionCommand = new RelayCommand<object>(_ => IsSecondFunction = !IsSecondFunction);
            CloseHistoryCommand = new RelayCommand<object>(_ => IsHistoryPanelVisible = false);
            ClearHistoryCommand = new RelayCommand<object>(_ => ClearHistory());
        }

        public void RefreshLocalizedTexts()
        {
            var loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse();
            TrigButtonText = loader.GetString("TrigButtonText");
        }

        private void NumberPressed(string value)
        {
            string num = value switch
            {
                "zero" => "0",
                "one" => "1",
                "two" => "2",
                "three" => "3",
                "four" => "4",
                "five" => "5",
                "six" => "6",
                "seven" => "7",
                "eight" => "8",
                "nine" => "9",
                "decimal" => ".",
                _ => value
            };

            if (_isNewNumber)
            {
                _currentNumber = num;
                _isNewNumber = false;
            }
            else
            {
                if (num == "." && _currentNumber.Contains("."))
                    return;
                _currentNumber += num;
            }
            MainDisplay = _currentNumber;
        }

        private void OperatorPressed(string op)
        {
            SciOperatorType opType = ParseOperator(op);

            if (opType == SciOperatorType.LeftParen || opType == SciOperatorType.RightParen)
            {
                ExpressionDisplay += opType == SciOperatorType.LeftParen ? "(" : ")";
                return;
            }

            if (opType == SciOperatorType.Negate)
            {
                if (double.TryParse(_currentNumber, out double val))
                {
                    _currentNumber = (-val).ToString();
                    MainDisplay = _currentNumber;
                }
                return;
            }

            if (_currentOperator != SciOperatorType.None && !_isNewNumber)
            {
                PerformCalculation();
            }

            if (!string.IsNullOrEmpty(_currentNumber))
            {
                _storedNumber = _currentNumber;
            }
            else if (string.IsNullOrEmpty(_storedNumber))
            {
                _storedNumber = "0";
            }

            _currentOperator = opType;
            ExpressionDisplay = $"{_storedNumber} {GetOperatorSymbol(opType)}";
            _isNewNumber = true;
        }

        private SciOperatorType ParseOperator(string symbol)
        {
            return symbol switch
            {
                "add" => SciOperatorType.Add,
                "subtract" => SciOperatorType.Subtract,
                "multiply" => SciOperatorType.Multiply,
                "divide" => SciOperatorType.Divide,
                "power" => SciOperatorType.Power,
                "mod" => SciOperatorType.Mod,
                "leftparen" => SciOperatorType.LeftParen,
                "rightparen" => SciOperatorType.RightParen,
                "negate" => SciOperatorType.Negate,
                _ => SciOperatorType.None
            };
        }

        private string GetOperatorSymbol(SciOperatorType opType)
        {
            return opType switch
            {
                SciOperatorType.Add => "+",
                SciOperatorType.Subtract => "−",
                SciOperatorType.Multiply => "×",
                SciOperatorType.Divide => "÷",
                SciOperatorType.Power => "^",
                SciOperatorType.Mod => "mod",
                SciOperatorType.LeftParen => "(",
                SciOperatorType.RightParen => ")",
                SciOperatorType.Negate => "⁺/₋",
                _ => ""
            };
        }

        private void FunctionPressed(string param)
        {
            double value;
            if (!double.TryParse(_currentNumber, out value))
                value = 0;

            switch (param)
            {
                case "sin":
                    _currentNumber = Math.Sin(value).ToString();
                    ExpressionDisplay = $"sin({value})";
                    break;
                case "cos":
                    _currentNumber = Math.Cos(value).ToString();
                    ExpressionDisplay = $"cos({value})";
                    break;
                case "tan":
                    _currentNumber = Math.Tan(value).ToString();
                    ExpressionDisplay = $"tan({value})";
                    break;
                case "sqrt":
                    _currentNumber = Math.Sqrt(value).ToString();
                    ExpressionDisplay = $"√({value})";
                    break;
                case "square":
                    _currentNumber = Math.Pow(value, 2).ToString();
                    ExpressionDisplay = $"{value}²";
                    break;
                case "cube":
                    _currentNumber = Math.Pow(value, 3).ToString();
                    ExpressionDisplay = $"{value}³";
                    break;
                case "reciprocal":
                    if (value == 0)
                    {
                        MainDisplay = "無法除以零";
                        _currentNumber = "0";
                        _isNewNumber = true;
                        return;
                    }
                    _currentNumber = (1 / value).ToString();
                    ExpressionDisplay = $"1/({value})";
                    break;
                case "abs":
                    _currentNumber = Math.Abs(value).ToString();
                    ExpressionDisplay = $"abs({value})";
                    break;
                case "exp":
                    _currentNumber = Math.Exp(value).ToString();
                    ExpressionDisplay = $"exp({value})";
                    break;
                case "log":
                    _currentNumber = Math.Log10(value).ToString();
                    ExpressionDisplay = $"log({value})";
                    break;
                case "ln":
                    _currentNumber = Math.Log(value).ToString();
                    ExpressionDisplay = $"ln({value})";
                    break;
                case "factorial":
                    _currentNumber = Factorial((int)value).ToString();
                    ExpressionDisplay = $"{(int)value}!";
                    break;
                case "tenpower":
                    _currentNumber = Math.Pow(10, value).ToString();
                    ExpressionDisplay = $"10^{value}";
                    break;
                case "twopower":
                    _currentNumber = Math.Pow(2, value).ToString();
                    ExpressionDisplay = $"2^{value}";
                    break;
                case "pi":
                    _currentNumber = Math.PI.ToString();
                    break;
                case "euler":
                    _currentNumber = Math.E.ToString();
                    break;
                default:
                    MainDisplay = "錯誤";
                    _currentNumber = "0";
                    _isNewNumber = true;
                    return;
            }

            MainDisplay = _currentNumber;
            _isNewNumber = true;

            var record = new HistoryRecord
            {
                Expression = ExpressionDisplay,
                Result = MainDisplay
            };
            History.Add(record);
            HistoryRecords.Add(record);
        }

        private double Factorial(int n)
        {
            if (n < 0) return double.NaN;
            double result = 1;
            for (int i = 2; i <= n; i++)
                result *= i;
            return result;
        }

        private void EqualsPressed()
        {
            if (_currentOperator == SciOperatorType.None)
                return;

            string originalFirstOperand = _storedNumber;
            string originalSecondOperand = _currentNumber;
            SciOperatorType originalOperator = _currentOperator;

            bool success = PerformCalculation();

            if (!success)
                return;

            string operatorSymbol = GetOperatorSymbol(originalOperator);
            string fullExpression = $"{originalFirstOperand} {operatorSymbol} {originalSecondOperand} =";
            ExpressionDisplay = fullExpression;

            var record = new HistoryRecord
            {
                Expression = fullExpression,
                Result = MainDisplay
            };
            History.Add(record);
            HistoryRecords.Add(record);

            _currentOperator = SciOperatorType.None;
            _storedNumber = "";
            _isNewNumber = true;
        }

        private bool PerformCalculation()
        {
            if (string.IsNullOrEmpty(_storedNumber) || string.IsNullOrEmpty(_currentNumber))
                return false;

            try
            {
                double a = double.Parse(_storedNumber);
                double b = double.Parse(_currentNumber);
                double result = 0;

                switch (_currentOperator)
                {
                    case SciOperatorType.Add: result = a + b; break;
                    case SciOperatorType.Subtract: result = a - b; break;
                    case SciOperatorType.Multiply: result = a * b; break;
                    case SciOperatorType.Divide:
                        if (b == 0)
                        {
                            MainDisplay = "無法除以零";
                            _currentNumber = "0";
                            _isNewNumber = true;
                            return false;
                        }
                        result = a / b;
                        break;
                    case SciOperatorType.Power: result = Math.Pow(a, b); break;
                    case SciOperatorType.Mod: result = a % b; break;
                    case SciOperatorType.YRoot:
                        // yroot: a^(1/b)
                        if (b == 0)
                        {
                            MainDisplay = "根號不能為零";
                            _currentNumber = "0";
                            _isNewNumber = true;
                            return false;
                        }
                        result = Math.Pow(a, 1.0 / b);
                        break;
                    case SciOperatorType.LogY:
                        // logy: log_b(a)
                        if (a <= 0 || b <= 0 || b == 1)
                        {
                            MainDisplay = "無效對數";
                            _currentNumber = "0";
                            _isNewNumber = true;
                            return false;
                        }
                        result = Math.Log(a, b);
                        break;
                }

                _currentNumber = result.ToString();
                MainDisplay = _currentNumber;
                _storedNumber = _currentNumber;
                return true;
            }
            catch
            {
                MainDisplay = "錯誤";
                _currentNumber = "0";
                _isNewNumber = true;
                return false;
            }
        }

        private void ClearPressed(string type)
        {
            switch (type)
            {
                case "clear":
                    _currentNumber = "";
                    _storedNumber = "";
                    _currentOperator = SciOperatorType.None;
                    _isNewNumber = true;
                    MainDisplay = "0";
                    ExpressionDisplay = "";
                    break;
                case "backspace":
                    // 如果目前顯示的是 e，則不執行 backspace
                    if (_currentNumber == Math.E.ToString("G15"))
                        break;
                    if (!string.IsNullOrEmpty(_currentNumber) && _currentNumber.Length > 0)
                    {
                        _currentNumber = _currentNumber.Substring(0, _currentNumber.Length - 1);
                        MainDisplay = string.IsNullOrEmpty(_currentNumber) ? "0" : _currentNumber;
                    }
                    break;
            }
        }

        private void MemoryStore()
        {
            if (double.TryParse(_currentNumber, out double value))
                _memory = value;
        }

        private void MemoryRecall()
        {
            _currentNumber = _memory.ToString();
            MainDisplay = _currentNumber;
        }

        private void MemoryClear()
        {
            _memory = 0;
        }

        private void MemoryAdd()
        {
            if (double.TryParse(_currentNumber, out double value))
                _memory += value;
        }

        private void MemorySubtract()
        {
            if (double.TryParse(_currentNumber, out double value))
                _memory -= value;
        }

        public void ClearHistory()
        {
            History.Clear();
            HistoryRecords.Clear();
        }

        public void UseHistoryRecord(HistoryRecord record)
        {
            _currentNumber = "";
            _storedNumber = "";
            _currentOperator = SciOperatorType.None;
            _isNewNumber = true;

            _currentNumber = record.Result;
            MainDisplay = record.Result;
            ExpressionDisplay = record.Expression;
            IsHistoryPanelVisible = false;
        }
    }
}