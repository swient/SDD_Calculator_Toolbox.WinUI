using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CalculatorToolbox.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private bool _isStandardVisible = true;
        private bool _isScientificVisible = false;
        private bool _isProgrammerVisible = false;

        public bool IsStandardVisible
        {
            get => _isStandardVisible;
            set { _isStandardVisible = value; OnPropertyChanged(); }
        }
        public bool IsScientificVisible
        {
            get => _isScientificVisible;
            set { _isScientificVisible = value; OnPropertyChanged(); }
        }
        public bool IsProgrammerVisible
        {
            get => _isProgrammerVisible;
            set { _isProgrammerVisible = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel() { }

        public void SwitchCalculator(string? type)
        {
            IsStandardVisible = false;
            IsScientificVisible = false;
            IsProgrammerVisible = false;

            if (type == "標準")
                IsStandardVisible = true;
            else if (type == "科學")
                IsScientificVisible = true;
            else if (type == "程式設計人員")
                IsProgrammerVisible = true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name ?? ""));
        }
    }
}