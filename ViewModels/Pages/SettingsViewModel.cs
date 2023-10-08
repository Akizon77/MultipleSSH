// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using MultipleSSH.Resources;
using Wpf.Ui.Controls;

namespace MultipleSSH.ViewModels.Pages
{
    public partial class SettingsViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _appVersion = String.Empty;

        [ObservableProperty]
        private Wpf.Ui.Appearance.ThemeType _currentTheme = Wpf.Ui.Appearance.ThemeType.Unknown;
        [ObservableProperty]
        private WindowBackdropType _currentBackdrop = WindowBackdropType.None;

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        public void OnNavigatedFrom() { }

        private void InitializeViewModel()
        {
            CurrentTheme = Wpf.Ui.Appearance.Theme.GetAppTheme();
            CurrentBackdrop = AppSettings.Instance.Backdrop;
            AppVersion = $"MultipleSSH - {GetAssemblyVersion()}";

            _isInitialized = true;
        }

        private string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
                ?? String.Empty;
        }

        [RelayCommand]
        private void OnChangeTheme(string parameter)
        {
            switch (parameter)
            {
                case "theme_light":
                    if (CurrentTheme == Wpf.Ui.Appearance.ThemeType.Light)
                        break;
                    CurrentTheme = Wpf.Ui.Appearance.ThemeType.Light;
                    Wpf.Ui.Appearance.Theme.Apply(CurrentTheme, CurrentBackdrop);
                    break;

                default:
                    if (CurrentTheme == Wpf.Ui.Appearance.ThemeType.Dark)
                        break;
                    CurrentTheme = Wpf.Ui.Appearance.ThemeType.Dark;
                    Wpf.Ui.Appearance.Theme.Apply(CurrentTheme, CurrentBackdrop);
                    break;
            }
            AppSettings.Instance.Theme = CurrentTheme;
            AppSettings.Save();
        }
        [RelayCommand]
        private void OnChangeBackdrop(string param)
        {
            switch (param)
            {
                case "acrylic":
                    if (CurrentBackdrop == WindowBackdropType.Acrylic)
                        break;
                    CurrentBackdrop = WindowBackdropType.Acrylic;
                    Wpf.Ui.Appearance.Theme.Apply(CurrentTheme, CurrentBackdrop);
                    break;
                case "mica":
                    if (CurrentBackdrop == WindowBackdropType.Mica)
                        break;
                    CurrentBackdrop = WindowBackdropType.Mica;
                    Wpf.Ui.Appearance.Theme.Apply(CurrentTheme, CurrentBackdrop);
                    break;
                case "tabbed":
                    if (CurrentBackdrop == WindowBackdropType.Tabbed)
                        break;
                    CurrentBackdrop = WindowBackdropType.Tabbed;
                    Wpf.Ui.Appearance.Theme.Apply(CurrentTheme, CurrentBackdrop);
                    break;
                default:
                    CurrentBackdrop = WindowBackdropType.Mica;
                    Wpf.Ui.Appearance.Theme.Apply(CurrentTheme, CurrentBackdrop);
                    break;
            }
            AppSettings.Instance.Backdrop = CurrentBackdrop;
            AppSettings.Save();
        }
    }
}
