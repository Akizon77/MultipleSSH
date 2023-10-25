// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using MultipleSSH.Models;
using MultipleSSH.Resources;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace MultipleSSH.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public static MainWindowViewModel Instance { get; private set; }

        public double frameHeight;


        //导航栏
        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = I18n.Instance.NaviMenu_Home,
                FontSize = 12,
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DashboardPage)
            },
            new NavigationViewItem()
            {
                Content = I18n.Instance.NaviMenu_Terminal,
                FontSize = 12,
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(Views.Pages.DataPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = I18n.Instance.NaviMenu_Settings,
                FontSize = 12,
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };

        [RelayCommand]
        private void OnStartApplication()
        {
            Instance = this;
        }
    }
}