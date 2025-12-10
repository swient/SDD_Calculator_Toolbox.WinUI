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
using CalculatorToolbox.Models;
using CalculatorToolbox.ViewModels;
using Windows.ApplicationModel.Resources;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CalculatorToolbox
{
    public sealed partial class StandardCalculator : UserControl
    {
        private StandardCalculatorViewModel? ViewModel => DataContext as StandardCalculatorViewModel;
        private ResourceLoader _resourceLoader;

        public StandardCalculator()
        {
            InitializeComponent();
            DataContext = new ViewModels.StandardCalculatorViewModel();
            _resourceLoader = ResourceLoader.GetForViewIndependentUse();
            UpdateLocalization();
        }

        private void UpdateLocalization()
        {
            // 更新標題文字
            StandardTitle.Text = _resourceLoader.GetString("Standard");
            ToolTipService.SetToolTip(HistoryButton, _resourceLoader.GetString("History"));

            // 更新歷史記錄面板的文字
            HistoryTitleText.Text = _resourceLoader.GetString("History");
            ToolTipService.SetToolTip(CloseHistoryButton, _resourceLoader.GetString("Close"));
            ClearHistoryButtonElement.Content = _resourceLoader.GetString("ClearHistory");
        }
    }
}
