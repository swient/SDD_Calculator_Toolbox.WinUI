using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Windows.Globalization;

namespace CalculatorToolbox.ViewModels
{
    /// <summary>
    /// 全域主題切換事件聚合器
    /// </summary>
    public static class ThemeChangedEventAggregator
    {
        public static event EventHandler<ElementTheme>? ThemeChanged;

        public static void PublishThemeChanged(ElementTheme theme)
        {
            ThemeChanged?.Invoke(null, theme);
        }

        public static void Subscribe(EventHandler<ElementTheme> handler)
        {
            ThemeChanged += handler;
        }

        public static void Unsubscribe(EventHandler<ElementTheme> handler)
        {
            ThemeChanged -= handler;
        }
    }

    public class SettingsViewModel : INotifyPropertyChanged
    {
        private ElementTheme _currentTheme;
        private string _currentLanguage = "zh-TW";

        public ObservableCollection<ThemeOption> ThemeOptions { get; }
        public ObservableCollection<LanguageOption> LanguageOptions { get; }

        public ElementTheme CurrentTheme
        {
            get => _currentTheme;
            set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    OnPropertyChanged();
                    ThemeChanged?.Invoke(this, _currentTheme);
                    ThemeChangedEventAggregator.PublishThemeChanged(_currentTheme);
                    ApplyTheme();
                }
            }
        }

        // 主題切換事件
        public event EventHandler<ElementTheme>? ThemeChanged;

        public string CurrentLanguage
        {
            get => _currentLanguage;
            set
            {
                if (_currentLanguage != value)
                {
                    _currentLanguage = value;
                    OnPropertyChanged();
                    ApplyLanguage();
                }
            }
        }

        public SettingsViewModel()
        {
            // 初始化主題選項
            ThemeOptions = new ObservableCollection<ThemeOption>();
            LoadThemeOptions();

            // 初始化語言選項
            LanguageOptions = new ObservableCollection<LanguageOption>
            {
                new LanguageOption { Name = "繁體中文", Code = "zh-TW" },
                new LanguageOption { Name = "English", Code = "en-us" },
                new LanguageOption { Name = "日本語", Code = "ja-jp" },
                new LanguageOption { Name = "한국어", Code = "ko-kr" }
            };

            // 載入目前設定
            LoadSettings();
        }

        private void LoadSettings()
        {
            // 從本地設定載入主題
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            
            if (localSettings.Values.TryGetValue("Theme", out var themeValue))
            {
                _currentTheme = (ElementTheme)Enum.Parse(typeof(ElementTheme), themeValue.ToString() ?? "Default");
            }
            else
            {
                _currentTheme = ElementTheme.Default;
            }

            // 從本地設定載入語言
            if (localSettings.Values.TryGetValue("Language", out var langValue))
            {
                _currentLanguage = langValue.ToString() ?? "zh-TW";
            }
            else
            {
                _currentLanguage = "zh-TW";
            }

            OnPropertyChanged(nameof(CurrentTheme));
            OnPropertyChanged(nameof(CurrentLanguage));
        }

        private void LoadThemeOptions()
        {
            ThemeOptions.Clear();
            var loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse();
            ThemeOptions.Add(new ThemeOption { Name = loader.GetString("ThemeLight"), Theme = ElementTheme.Light });
            ThemeOptions.Add(new ThemeOption { Name = loader.GetString("ThemeDark"), Theme = ElementTheme.Dark });
            ThemeOptions.Add(new ThemeOption { Name = loader.GetString("ThemeDefault"), Theme = ElementTheme.Default });
            OnPropertyChanged(nameof(ThemeOptions));
        }

        private void ApplyTheme()
        {
            // 儲存設定
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["Theme"] = _currentTheme.ToString();
        }

        private void ApplyLanguage()
        {
            // 儲存設定
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["Language"] = _currentLanguage;

            // 應用語言
            ApplicationLanguages.PrimaryLanguageOverride = _currentLanguage;

            // 重新載入主題選項文字
            ReloadThemeOptionNames();
        }

        // 語言切換時重新載入主題選項文字
        private void ReloadThemeOptionNames()
        {
            var loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse();
            foreach (var option in ThemeOptions)
            {
                switch (option.Theme)
                {
                    case ElementTheme.Light:
                        option.Name = loader.GetString("ThemeLight");
                        break;
                    case ElementTheme.Dark:
                        option.Name = loader.GetString("ThemeDark");
                        break;
                    case ElementTheme.Default:
                        option.Name = loader.GetString("ThemeDefault");
                        break;
                }
            }
            OnPropertyChanged(nameof(ThemeOptions));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class ThemeOption
    {
        public string Name { get; set; } = string.Empty;
        public ElementTheme Theme { get; set; }
    }

    public class LanguageOption
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}