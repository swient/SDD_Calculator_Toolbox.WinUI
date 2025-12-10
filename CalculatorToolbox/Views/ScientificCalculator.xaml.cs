using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.ApplicationModel.Resources;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CalculatorToolbox
{
    public sealed partial class ScientificCalculator : UserControl
    {
        private ResourceLoader _resourceLoader;
        private ViewModels.ScientificCalculatorViewModel? ViewModel => DataContext as ViewModels.ScientificCalculatorViewModel;

        public ScientificCalculator()
        {
            InitializeComponent();
            var vm = new ViewModels.ScientificCalculatorViewModel();
            DataContext = vm;
            _resourceLoader = ResourceLoader.GetForViewIndependentUse();
            UpdateLocalization();
            vm.RefreshLocalizedTexts();

            // 監聽 RequestCloseTrigPanel 屬性
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModels.ScientificCalculatorViewModel.RequestCloseTrigPanel)
                && ViewModel?.RequestCloseTrigPanel == true)
            {
                if (TrigButton?.Flyout is Flyout flyout)
                {
                    flyout.Hide();
                }
                ViewModel.RequestCloseTrigPanel = false;
            }
        }

        private void UpdateLocalization()
        {
            // 更新標題文字
            ScientificTitle.Text = _resourceLoader.GetString("Scientific");
            ToolTipService.SetToolTip(HistoryButton, _resourceLoader.GetString("History"));

            // 更新歷史記錄面板的文字
            HistoryTitleText.Text = _resourceLoader.GetString("History");
            ToolTipService.SetToolTip(CloseHistoryButton, _resourceLoader.GetString("Close"));
            ClearHistoryButtonElement.Content = _resourceLoader.GetString("ClearHistory");
        }
    }
}
