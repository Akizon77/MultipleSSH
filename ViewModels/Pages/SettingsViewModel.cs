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
        private Wpf.Ui.Appearance.ApplicationTheme _currentTheme = Wpf.Ui
            .Appearance
            .ApplicationTheme
            .Unknown;

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
            CurrentTheme = AppSettings.Instance.Theme;
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
                    if (CurrentTheme == Wpf.Ui.Appearance.ApplicationTheme.Light)
                        break;
                    CurrentTheme = Wpf.Ui.Appearance.ApplicationTheme.Light;
                    Wpf.Ui.Appearance.ApplicationThemeManager.Apply(CurrentTheme, CurrentBackdrop);
                    break;

                default:
                    if (CurrentTheme == Wpf.Ui.Appearance.ApplicationTheme.Dark)
                        break;
                    CurrentTheme = Wpf.Ui.Appearance.ApplicationTheme.Dark;
                    Wpf.Ui.Appearance.ApplicationThemeManager.Apply(CurrentTheme, CurrentBackdrop);
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
                    break;

                case "mica":
                    if (CurrentBackdrop == WindowBackdropType.Mica)
                        break;
                    CurrentBackdrop = WindowBackdropType.Mica;

                    break;

                case "tabbed":
                    if (CurrentBackdrop == WindowBackdropType.Tabbed)
                        break;
                    CurrentBackdrop = WindowBackdropType.Tabbed;
                    break;

                default:
                    CurrentBackdrop = WindowBackdropType.Mica;
                    break;
            }
            Wpf.Ui.Appearance.ApplicationThemeManager.Apply(CurrentTheme, CurrentBackdrop);
            AppSettings.Instance.Backdrop = CurrentBackdrop;
            AppSettings.Save();
        }
    }
}
