using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CalculatorToolbox.Models;
using CalculatorToolbox.Logic;

namespace CalculatorToolbox.ViewModels
{
    public class ProgrammerCalculatorViewModel : BaseViewModel
    {
        private readonly ProgrammerCalculatorLogic _logic = new();

        private string _hexDisplay = "0";
        public string HexDisplay
        {
            get => _hexDisplay;
            set => SetProperty(ref _hexDisplay, value);
        }

        private string _decDisplay = "0";
        public string DecDisplay
        {
            get => _decDisplay;
            set => SetProperty(ref _decDisplay, value);
        }

        private string _octDisplay = "0";
        public string OctDisplay
        {
            get => _octDisplay;
            set => SetProperty(ref _octDisplay, value);
        }

        private string _binDisplay = "0";
        public string BinDisplay
        {
            get => _binDisplay;
            set => SetProperty(ref _binDisplay, value);
        }

        private int _currentBase = 10; // Dec
        private long _currentValue = 0;
        private int _byteSize = 8; // 默認 8-bit

        public int ByteSize
        {
            get => _byteSize;
            set
            {
                if (SetProperty(ref _byteSize, value))
                {
                    ApplyByteSize();
                }
            }
        }

        public ObservableCollection<HistoryRecord> History { get; } = new();

        public ICommand NumberCommand { get; }
        public ICommand BitwiseCommand { get; }
        public ICommand BaseCommand { get; }
        public ICommand ByteSizeCommand { get; }
        public ICommand ClearCommand { get; }

        public ProgrammerCalculatorViewModel()
        {
            NumberCommand = new RelayCommand<string>(NumberPressed);
            BitwiseCommand = new RelayCommand<string>(BitwisePressed);
            BaseCommand = new RelayCommand<string>(BaseChanged);
            ByteSizeCommand = new RelayCommand<int>(size => ByteSize = size);
            ClearCommand = new RelayCommand<object>(_ => ClearPressed());
        }

        private void NumberPressed(string value)
        {
            try
            {
                string currentDisplay = GetCurrentBaseDisplay();
                if (currentDisplay == "0")
                    currentDisplay = "";
                
                currentDisplay += value;
                _currentValue = Convert.ToInt64(currentDisplay, _currentBase);
                ApplyByteSize();
                UpdateAllDisplays();
            }
            catch
            {
                // 忽略非法輸入
            }
        }

        private void BitwisePressed(string operation)
        {
            try
            {
                long result = 0;
                string expression = "";

                switch (operation)
                {
                    case "AND":
                        // 需要兩個操作數，這裡簡化為示範
                        result = _logic.BitwiseAnd((int)_currentValue, (int)_currentValue);
                        expression = $"{_currentValue} AND {_currentValue}";
                        break;
                    case "OR":
                        result = _logic.BitwiseOr((int)_currentValue, (int)_currentValue);
                        expression = $"{_currentValue} OR {_currentValue}";
                        break;
                    case "XOR":
                        result = _logic.BitwiseXor((int)_currentValue, (int)_currentValue);
                        expression = $"{_currentValue} XOR {_currentValue}";
                        break;
                    case "NOT":
                        result = _logic.BitwiseNot((int)_currentValue);
                        expression = $"NOT {_currentValue}";
                        break;
                    case "LSH":
                        result = _logic.ShiftLeft((int)_currentValue, 1);
                        expression = $"{_currentValue} << 1";
                        break;
                    case "RSH":
                        result = _logic.ShiftRight((int)_currentValue, 1);
                        expression = $"{_currentValue} >> 1";
                        break;
                }

                _currentValue = result;
                ApplyByteSize();
                UpdateAllDisplays();

                _logic.AddHistory(expression, result.ToString());
                History.Add(new HistoryRecord
                {
                    Expression = expression,
                    Result = result.ToString()
                });
            }
            catch
            {
                DecDisplay = "錯誤";
            }
        }

        private void BaseChanged(string baseType)
        {
            switch (baseType)
            {
                case "HEX":
                    _currentBase = 16;
                    break;
                case "DEC":
                    _currentBase = 10;
                    break;
                case "OCT":
                    _currentBase = 8;
                    break;
                case "BIN":
                    _currentBase = 2;
                    break;
            }
        }

        private void ClearPressed()
        {
            _currentValue = 0;
            UpdateAllDisplays();
        }

        private void ApplyByteSize()
        {
            int maxBits = _byteSize;
            long mask = (1L << maxBits) - 1;
            _currentValue = _currentValue & mask;
        }

        private void UpdateAllDisplays()
        {
            try
            {
                HexDisplay = Convert.ToString(_currentValue, 16).ToUpper();
                DecDisplay = _currentValue.ToString();
                OctDisplay = Convert.ToString(_currentValue, 8);
                BinDisplay = Convert.ToString(_currentValue, 2);
            }
            catch
            {
                HexDisplay = DecDisplay = OctDisplay = BinDisplay = "錯誤";
            }
        }

        private string GetCurrentBaseDisplay()
        {
            return _currentBase switch
            {
                16 => HexDisplay,
                10 => DecDisplay,
                8 => OctDisplay,
                2 => BinDisplay,
                _ => DecDisplay
            };
        }
    }
}