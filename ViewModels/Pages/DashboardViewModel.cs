// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Microsoft.Win32;
using MultipleSSH.Models;
using MultipleSSH.Resources;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MultipleSSH.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        private string fastHost = String.Empty;

        [ObservableProperty]
        private string fastPort = "22";

        [ObservableProperty]
        private string fastUsername = String.Empty;

        [ObservableProperty]
        private string fastPwd = String.Empty;

        [ObservableProperty]
        private string fastKeyPath = String.Empty;

        [ObservableProperty]
        private
        //0 密码；1 私钥
        int fastVerigyMethod = 0;

        [ObservableProperty]
        private ObservableCollection<Models.SshHost> _hosts = new(AppSettings.Instance.Hosts);

        [RelayCommand]
        private async Task OnFastConnect()
        {
            await OnConnect(
                new Models.SshHost()
                {
                    Host = FastHost,
                    Port = FastPort,
                    Username = FastUsername,
                    Password = FastPwd,
                    PrivateKey = FastKeyPath,
                    LoginMethod = (LoginMethod)FastVerigyMethod
                }
            );
        }

        [RelayCommand]
        private void OnFastSelectPrivateKey()
        {
            OpenFileDialog fileDialog = new();
            fileDialog.ShowDialog();
            if (String.IsNullOrEmpty(fileDialog.FileName))
                return;
            FastKeyPath = fileDialog.FileName;
        }

        [RelayCommand]
        private async Task OnConnect(object o)
        {
            var host = o as Models.SshHost;
            if (String.IsNullOrEmpty(host.Host))
                host.Host = "example.com";
            if (String.IsNullOrEmpty(host.Port))
                host.Port = "22";
            if (String.IsNullOrEmpty(host.Username))
                host.Username = "root";
            ProcessStartInfo psi = new();
            psi.UseShellExecute = true;
            psi.FileName = "cmd.exe";
            if (host.LoginMethod == LoginMethod.PrivateKey)
            {
                psi.Arguments =
                    $"/c ssh {host.Username}@{host.Host} -p {host.Port} -i {host.PrivateKey} & pause";
                Process.Start(psi);
                return;
            }
            psi.Arguments = $"/c ssh {host.Username}@{host.Host} -p {host.Port} & pause";
            Wpf.Ui.Controls.MessageBox box =
                new()
                {
                    Title = "终端",
                    Content = "SSH无法直接通过命令行传入密码，是否将密码复制到剪切板",
                    IsPrimaryButtonEnabled = true,
                    PrimaryButtonText = "确定",
                    CloseButtonText = "关闭",
                };
            var messageBoxResult = await box.ShowDialogAsync();
            if (messageBoxResult == Wpf.Ui.Controls.MessageBoxResult.None)
                return;
            Clipboard.SetText(host.Password);
            Process.Start(psi);
        }
    }
}