// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using MultipleSSH.Models;
using MultipleSSH.Resources;
using MultipleSSH.ViewModels.Pages;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace MultipleSSH.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }
        public I18n i18N
        {
            get { return I18n.Instance; }
        }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        private void AutoSuggestBox_QuerySubmitted(
            AutoSuggestBox sender,
            AutoSuggestBoxQuerySubmittedEventArgs args
        )
        {
            var splitText = sender.Text.Split(' ');
            if (String.IsNullOrWhiteSpace(sender.Text))
            {
                ViewModel.Hosts = AppSettings.Instance.Hosts;
            }
            else
            {
                var result = ViewModel.Hosts;
                foreach (var text in splitText)
                {
                    result = result
                        .Where(
                            x =>
                                x.Host.Contains(text)
                                || x.FriendlyName.Contains(text)
                                || x.Username.Contains(text)
                        )
                        .ToList();
                }
                ViewModel.Hosts = result.OrderBy(x => x.FriendlyName).ToList();
            }
            
        }
    }
}
