// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using MultipleSSH.Resources;
using MultipleSSH.ViewModels.Pages;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace MultipleSSH.Views.Pages
{
    public partial class SettingsPage : INavigableView<SettingsViewModel>
    {
        public SettingsViewModel ViewModel { get; }

        public SettingsPage(SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
            ui_ThemeCombobox.SelectedIndex = AppSettings.Instance.Theme switch
            {
                Wpf.Ui.Appearance.ApplicationTheme.Light => 0,
                Wpf.Ui.Appearance.ApplicationTheme.Dark => 1,
            };
            ui_BackdropCombobox.SelectedIndex = AppSettings.Instance.Backdrop switch
            {
                WindowBackdropType.Mica => 0,
                WindowBackdropType.Acrylic => 1,
                WindowBackdropType.Tabbed => 2,
            };
        }

        private void ui_ThemeCombobox_SelectionChanged(
            object sender,
            System.Windows.Controls.SelectionChangedEventArgs e
        )
        {
            var cb = sender as ComboBox;
            switch (cb.SelectedIndex)
            {
                case 0:
                    ViewModel.ChangeThemeCommand.Execute("theme_light");
                    break;
                case 1:
                    ViewModel.ChangeThemeCommand.Execute("theme_dark");
                    break;
            }
        }

        private void ui_BackdropCombobox_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e
        )
        {
            var cb = sender as ComboBox;
            switch (cb.SelectedIndex)
            {
                case 0:
                    ViewModel.ChangeBackdropCommand.Execute("miac");
                    break;
                case 1:
                    ViewModel.ChangeBackdropCommand.Execute("acrylic");
                    break;
                case 2:
                    ViewModel.ChangeBackdropCommand.Execute("tabbed");
                    break;
            }
        }
    }
}
