using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CalculatorToolbox.Models;

namespace CalculatorToolbox.ViewModels
{
    public enum OperatorType
    {
        None,
        Add,
        Subtract,
        Multiply,
        Divide,
        Power,
        Percent,
        Reciprocal,
        SquareRoot,
        Negate
    }

    public class StandardCalculatorViewModel : BaseViewModel
    {
        private bool _isHistoryPanelVisible = false;
        public bool IsHistoryPanelVisible
        {
            get => _isHistoryPanelVisible;
            set => SetProperty(ref _isHistoryPanelVisible, value);
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
        private OperatorType _currentOperator = OperatorType.None;
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
        public ICommand ShowHistoryCommand { get; }
        public ICommand CloseHistoryCommand { get; }
        public ICommand ClearHistoryCommand { get; }

        public StandardCalculatorViewModel()
        {
            NumberCommand = new RelayCommand<string>(NumberPressed);
            OperatorCommand = new RelayCommand<string>(OperatorPressed);
            EqualsCommand = new RelayCommand<object>(_ => EqualsPressed());
            ClearCommand = new RelayCommand<string>(ClearPressed);
            ShowHistoryCommand = new RelayCommand<object>(_ => IsHistoryPanelVisible = true);
            CloseHistoryCommand = new RelayCommand<object>(_ => IsHistoryPanelVisible = false);
            ClearHistoryCommand = new RelayCommand<object>(_ => ClearHistory());
        }

        private void NumberPressed(string value)
        {
            // 英文參數轉換為數字字元
            string mappedValue = value switch
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

            // 防止重複輸入小數點
            if (mappedValue == "." && _currentNumber.Contains("."))
                return;

            if (_isNewNumber)
            {
                // 如果是新數字且輸入小數點，前面加 0
                _currentNumber = mappedValue == "." ? "0." : mappedValue;
                _isNewNumber = false;
            }
            else
            {
                // 防止前導零（除非是小數）
                if (_currentNumber == "0" && mappedValue != ".")
                {
                    _currentNumber = mappedValue;  // 替換掉前導的 0
                }
                else
                {
                    _currentNumber += mappedValue;
                }
            }
            MainDisplay = _currentNumber;
        }

        private void OperatorPressed(string operatorSymbol)
        {
            OperatorType opType = ParseOperator(operatorSymbol);

            // 單元運算符（立即運算）
            if (IsUnaryOperator(opType))
            {
                PerformUnaryOperation(opType);
                return;
            }

            // 二元運算符
            // 如果已經有運算符，先計算前一個運算
            if (_currentOperator != OperatorType.None && !_isNewNumber)
            {
                PerformCalculation();
            }

            // 如果當前有數字，儲存它
            if (!string.IsNullOrEmpty(_currentNumber))
            {
                _storedNumber = _currentNumber;
            }
            else if (string.IsNullOrEmpty(_storedNumber))
            {
                _storedNumber = "0";
            }

            // 設定新的運算符
            _currentOperator = opType;

            // 更新表達式顯示（次方在表達式中顯示為 ^）
            string displaySymbol = opType == OperatorType.Power ? "^" : MapOperatorParameter(operatorSymbol);
            ExpressionDisplay = $"{_storedNumber} {displaySymbol}";

            // 準備輸入下一個數字
            _isNewNumber = true;
        }

        private OperatorType ParseOperator(string symbol)
        {
            return symbol switch
            {
                "add" => OperatorType.Add,
                "subtract" => OperatorType.Subtract,
                "multiply" => OperatorType.Multiply,
                "divide" => OperatorType.Divide,
                "power" => OperatorType.Power,
                "percent" => OperatorType.Percent,
                "reciprocal" => OperatorType.Reciprocal,
                "sqrt" => OperatorType.SquareRoot,
                "negate" => OperatorType.Negate,
                _ => OperatorType.None
            };
        }

        private string GetOperatorSymbol(OperatorType opType)
        {
            return opType switch
            {
                OperatorType.Add => "+",
                OperatorType.Subtract => "−",
                OperatorType.Multiply => "×",
                OperatorType.Divide => "÷",
                OperatorType.Power => "𝑥ʸ",
                OperatorType.Percent => "%",
                OperatorType.Reciprocal => "¹/𝑥",
                OperatorType.SquareRoot => "√𝑥",
                OperatorType.Negate => "⁺/₋",
                _ => ""
            };
        }

        // 英文參數轉運算符號
        private string MapOperatorParameter(string param)
        {
            return param switch
            {
                "add" => "+",
                "subtract" => "−",
                "multiply" => "×",
                "divide" => "÷",
                "power" => "𝑥ʸ",
                "percent" => "%",
                "reciprocal" => "¹/𝑥",
                "sqrt" => "√𝑥",
                "negate" => "⁺/₋",
                _ => param
            };
        }

        private bool IsUnaryOperator(OperatorType opType)
        {
            return opType == OperatorType.Percent ||
                   opType == OperatorType.Reciprocal ||
                   opType == OperatorType.SquareRoot ||
                   opType == OperatorType.Negate;
        }

        private void PerformUnaryOperation(OperatorType opType)
        {
            if (string.IsNullOrEmpty(_currentNumber))
                _currentNumber = "0";

            try
            {
                double value = double.Parse(_currentNumber);
                double result = 0;
                string operation = "";

                switch (opType)
                {
                    case OperatorType.Percent:
                        // 百分比：如果有儲存的數字，計算百分比；否則直接除以 100
                        if (!string.IsNullOrEmpty(_storedNumber))
                        {
                            double stored = double.Parse(_storedNumber);
                            result = stored * (value / 100);
                            operation = $"{_storedNumber} × {value}%";
                        }
                        else
                        {
                            result = value / 100;
                            operation = $"{value}%";
                        }
                        break;

                    case OperatorType.Reciprocal:
                        if (value == 0)
                        {
                            MainDisplay = "無法除以零";
                            return;
                        }
                        result = 1 / value;
                        operation = $"1/({value})";
                        break;

                    case OperatorType.SquareRoot:
                        if (value < 0)
                        {
                            MainDisplay = "無效輸入";
                            return;
                        }
                        result = Math.Sqrt(value);
                        operation = $"√({value})";
                        break;

                    case OperatorType.Negate:
                        result = -value;
                        // 正負號切換不顯示在表達式中，直接更新當前數字
                        _currentNumber = result.ToString();
                        MainDisplay = _currentNumber;
                        return;
                }

                _currentNumber = result.ToString();
                MainDisplay = _currentNumber;

                // 單元運算後更新表達式顯示
                if (_currentOperator != OperatorType.None)
                {
                    ExpressionDisplay = $"{_storedNumber} {GetOperatorSymbol(_currentOperator)} {operation}";
                }
                else
                {
                    ExpressionDisplay = operation;
                }

                _isNewNumber = true;
            }
            catch
            {
                MainDisplay = "錯誤";
                _currentNumber = "0";
            }
        }

        private void EqualsPressed()
        {
            if (_currentOperator == OperatorType.None)
                return;

            // 保存原始的兩個運算元和運算符（在計算前）
            string originalFirstOperand = _storedNumber;
            string originalSecondOperand = _currentNumber;
            OperatorType originalOperator = _currentOperator;

            bool success = PerformCalculation();

            // 如果計算失敗（例如除零），不顯示等號，保持表達式顯示
            if (!success)
            {
                // 表達式保持原樣，不添加 "="
                return;
            }

            // 組成完整表達式（次方使用 ^ 符號）
            string operatorSymbol = originalOperator == OperatorType.Power ? "^" : GetOperatorSymbol(originalOperator);
            string fullExpression = $"{originalFirstOperand} {operatorSymbol} {originalSecondOperand} =";
            ExpressionDisplay = fullExpression;

            var record = new HistoryRecord
            {
                Expression = ExpressionDisplay,
                Result = MainDisplay
            };
            History.Add(record);
            HistoryRecords.Add(record);

            // 重置狀態
            _currentOperator = OperatorType.None;
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
                    case OperatorType.Add:
                        result = a + b;
                        break;
                    case OperatorType.Subtract:
                        result = a - b;
                        break;
                    case OperatorType.Multiply:
                        result = a * b;
                        break;
                    case OperatorType.Divide:
                        if (b == 0)
                        {
                            MainDisplay = "無法除以零";
                            _currentNumber = "0";
                            _isNewNumber = true;
                            // 不重置 _storedNumber 和 _currentOperator，保持表達式顯示
                            return false;  // 返回失敗
                        }
                        result = a / b;
                        break;
                    case OperatorType.Power:
                        result = Math.Pow(a, b);
                        break;
                }

                _currentNumber = result.ToString();
                MainDisplay = _currentNumber;
                _storedNumber = _currentNumber;
                return true;  // 返回成功
            }
            catch
            {
                MainDisplay = "錯誤";
                _currentNumber = "0";
                _isNewNumber = true;
                return false;  // 返回失敗
            }
        }

        private void ClearPressed(string type)
        {
            switch (type)
            {
                case "clear_entry":
                    _currentNumber = "";
                    MainDisplay = "0";
                    _isNewNumber = true;
                    break;

                case "clear":
                    _currentNumber = "";
                    _storedNumber = "";
                    _currentOperator = OperatorType.None;
                    _isNewNumber = true;
                    MainDisplay = "0";
                    ExpressionDisplay = "";
                    break;

                case "backspace":
                    if (!string.IsNullOrEmpty(_currentNumber) && _currentNumber.Length > 0)
                    {
                        _currentNumber = _currentNumber.Substring(0, _currentNumber.Length - 1);
                        MainDisplay = string.IsNullOrEmpty(_currentNumber) ? "0" : _currentNumber;
                    }
                    break;
            }
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
            _isNewNumber = true;

            _currentNumber = record.Result;
            MainDisplay = record.Result;
            ExpressionDisplay = record.Expression;
            IsHistoryPanelVisible = false;
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool>? _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute((T)parameter!);
        }

        public void Execute(object? parameter)
        {
            _execute((T)parameter!);
        }

        public event EventHandler? CanExecuteChanged
        {
            add { }
            remove { }
        }
    }
}