using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using CalculatorToolbox.Views;
using Windows.ApplicationModel.Resources;

namespace CalculatorToolbox
{
    public sealed partial class MainWindow : Window
    {
        private CalculatorHostPage _calculatorHostPage;
        private ResourceLoader _resourceLoader;

        public MainWindow()
        {
            InitializeComponent();

            // 初始化資源載入器
            _resourceLoader = ResourceLoader.GetForViewIndependentUse();

            // 初始化計算器宿主頁面
            _calculatorHostPage = new CalculatorHostPage();

            // 連接 NavigationView 事件
            MainNavView.SelectionChanged += NavigationView_SelectionChanged;
            MainNavView.BackRequested += NavigationView_BackRequested;

            // 訂閱全域主題切換事件
            CalculatorToolbox.ViewModels.ThemeChangedEventAggregator.Subscribe((s, theme) =>
            {
                RootGrid.RequestedTheme = theme;
            });

            // 載入並應用已儲存的主題
            LoadTheme();

            // 更新本地化文字
            UpdateLocalization();

            // 設定預設頁面為標準計算器
            ContentFrame.Navigate(typeof(CalculatorHostPage));
            if (MainNavView.MenuItems.Count > 0)
            {
                MainNavView.SelectedItem = MainNavView.MenuItems[0];
            }
        }

        private void LoadTheme()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue("Theme", out var themeValue))
            {
                var theme = (ElementTheme)Enum.Parse(typeof(ElementTheme), themeValue.ToString() ?? "Default");
                RootGrid.RequestedTheme = theme;
            }
        }

        public void SetTheme(ElementTheme theme)
        {
            RootGrid.RequestedTheme = theme;
        }

        private void UpdateLocalization()
        {
            // 更新視窗標題
            Title = _resourceLoader.GetString("AppTitle");
            
            // 更新導航選單項目
            if (MainNavView.MenuItems.Count >= 3)
            {
                ((NavigationViewItem)MainNavView.MenuItems[0]).Content = _resourceLoader.GetString("Standard");
                ((NavigationViewItem)MainNavView.MenuItems[1]).Content = _resourceLoader.GetString("Scientific");
                ((NavigationViewItem)MainNavView.MenuItems[2]).Content = _resourceLoader.GetString("Programmer");
            }
        }

        public void RefreshLocalization()
        {
            _resourceLoader = ResourceLoader.GetForViewIndependentUse();
            UpdateLocalization();
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                // 導航到設定頁面
                ContentFrame.Navigate(typeof(SettingsPage));
            }
            else if (args.SelectedItem is NavigationViewItem item)
            {
                var tag = item.Tag?.ToString();
                
                // 導航到計算器頁面
                if (ContentFrame.Content is not CalculatorHostPage)
                {
                    ContentFrame.Navigate(typeof(CalculatorHostPage));
                }
                
                // 切換計算器類型
                if (ContentFrame.Content is CalculatorHostPage hostPage)
                {
                    hostPage.SwitchCalculator(tag ?? "Standard");
                }
            }
        }

        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
                
                // 如果返回到計算器頁面，確保選中計算器選單項
                if (ContentFrame.Content is CalculatorHostPage && MainNavView.MenuItems.Count > 0)
                {
                    MainNavView.SelectedItem = MainNavView.MenuItems[0];
                }
            }
        }
    }
}