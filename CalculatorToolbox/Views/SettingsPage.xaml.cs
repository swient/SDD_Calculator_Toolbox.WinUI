using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using CalculatorToolbox.ViewModels;
using System.Linq;
using Windows.ApplicationModel.Resources;

namespace CalculatorToolbox.Views
{
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel ViewModel => (SettingsViewModel)Resources["ViewModel"];
        private ResourceLoader _resourceLoader;

        public SettingsPage()
        {
            InitializeComponent();
            _resourceLoader = ResourceLoader.GetForViewIndependentUse();
            UpdateLocalization();
            InitializeSettings();
        }

        private void UpdateLocalization()
        {
            SettingsTitle.Text = _resourceLoader.GetString("Settings");
            ThemeLabel.Text = _resourceLoader.GetString("Theme");
            ThemeDescription.Text = _resourceLoader.GetString("ThemeDescription");
            LanguageLabel.Text = _resourceLoader.GetString("Language");
            LanguageDescription.Text = _resourceLoader.GetString("LanguageDescription");
            AboutLabel.Text = _resourceLoader.GetString("About");
            AppNameText.Text = _resourceLoader.GetString("AppName");
            VersionText.Text = _resourceLoader.GetString("VersionLabel") + " 1.0.0";
            AppDescriptionText.Text = _resourceLoader.GetString("AppDescription");
            RestartInfoBar.Title = _resourceLoader.GetString("RestartTitle");
            RestartInfoBar.Message = _resourceLoader.GetString("RestartMessage");
        }

        private void InitializeSettings()
        {
            // 設定主題選項
            var currentTheme = ViewModel.CurrentTheme;
            var themeIndex = ViewModel.ThemeOptions.ToList().FindIndex(t => t.Theme == currentTheme);
            if (themeIndex >= 0)
            {
                ThemeRadioButtons.SelectedIndex = themeIndex;
            }

            // 設定語言選項
            var currentLang = ViewModel.CurrentLanguage;
            var langOption = ViewModel.LanguageOptions.FirstOrDefault(l => l.Code == currentLang);
            if (langOption != null)
            {
                LanguageComboBox.SelectedItem = langOption;
            }

            // 連接事件
            ThemeRadioButtons.SelectionChanged += ThemeRadioButtons_SelectionChanged;
            LanguageComboBox.SelectionChanged += LanguageComboBox_SelectionChanged;
        }

        private void ThemeRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemeRadioButtons.SelectedItem is ThemeOption selectedTheme)
            {
                // 儲存設定
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["Theme"] = selectedTheme.Theme.ToString();

                // 立即套用主題到當前視窗
                if (Window.Current?.Content is FrameworkElement root)
                {
                    root.RequestedTheme = selectedTheme.Theme;
                }

                // 更新 ViewModel 狀態
                ViewModel.CurrentTheme = selectedTheme.Theme;
            }
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageComboBox.SelectedItem is LanguageOption selectedLang)
            {
                if (selectedLang.Code != ViewModel.CurrentLanguage)
                {
                    ViewModel.CurrentLanguage = selectedLang.Code;
                    
                    // 顯示重啟提示
                    RestartInfoBar.IsOpen = true;
                }
            }
        }
    }
}