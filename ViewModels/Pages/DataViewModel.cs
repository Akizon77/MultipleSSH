// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using MultipleSSH.Models;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Media;
using Wpf.Ui.Controls;

namespace MultipleSSH.ViewModels.Pages
{
    public partial class DataViewModel : ObservableObject, INavigationAware
    {
        public DataViewModel(ISnackbarService snackbarService)
        {
            _snackbarService = snackbarService;
        }
        private readonly ISnackbarService _snackbarService;



        [ObservableProperty]
        private IEnumerable<DataColor> _colors;

        public void OnNavigatedTo()
        {
            InitializeViewModel();
            _snackbarService.Show("警告","内嵌Windows Terminal正处于待开发状态。微软尚未对WPF独立添加WT控件。本项目使用ConPTY模拟终端，兼容性未知。",ControlAppearance.Caution,null,TimeSpan.FromSeconds(5));

        }

        public void OnNavigatedFrom() { }

        private void InitializeViewModel()
        {
            

        }
    }
}
