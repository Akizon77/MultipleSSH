﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using MultipleSSH.Models;
using MultipleSSH.Resources;
using MultipleSSH.ViewModels.Windows;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls;

namespace MultipleSSH.Views.Windows
{
    public partial class MainWindow
    {
        public I18n i18N
        {
            get { return I18n.Instance; }
        }
        public MainWindowViewModel ViewModel { get; }
        

        public MainWindow(
            MainWindowViewModel viewModel,
            INavigationService navigationService,
            IServiceProvider serviceProvider,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService
        )
        {
            
            //Wpf.Ui.Appearance.App.Watch(this);

            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            navigationService.SetNavigationControl(NavigationView);
            snackbarService.SetSnackbarPresenter(SnackbarPresenter);
            contentDialogService.SetContentPresenter(RootContentDialog);

            NavigationView.SetServiceProvider(serviceProvider);
        }
    }
}