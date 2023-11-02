// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using MultipleSSH.Models;
using MultipleSSH.Resources;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using Wpf.Ui.Controls;

namespace MultipleSSH.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject, INavigationAware
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
        private int fastVerigyMethod = 0; //0 密码 1 私钥

        [ObservableProperty]
        private string hitokoto = string.Empty;

        [ObservableProperty]
        private string welcomeText = String.Empty;

        [ObservableProperty]
        string flyoutKeyPath = String.Empty;

        [ObservableProperty]
        private List<Models.SshHost> _hosts = AppSettings.Instance.Hosts;

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
                if (host.PrivateKey.Contains("-----"))
                {
                    var ac = System.IO.Path.GetTempPath();
                    Random random = new Random();
                    var attach = random.Next(100000000, 999999999);
                    var tempKeyFile = Path.Combine(ac, DateTime.Now.ToString("HHmmss-") + attach);
                    File.WriteAllText(tempKeyFile, host.PrivateKey);
                    host.PrivateKey = tempKeyFile;
                }
                psi.Arguments =
                    $"/c ssh {host.Username}@{host.Host} -p {host.Port} -i \"{host.PrivateKey}\" & pause";
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

        [RelayCommand]
        private async Task OnRefreshHitokoto()
        {
            try
            {
                string myVar;
                var hitokoto = await GetHitokotoAsync();
                Hitokoto = hitokoto;
                async Task<string> GetHitokotoAsync()
                {
                    myVar = "嵌套函数给父级的变量赋值"; 
                    Task.Run(() => myVar = "233"); ;
                    var httpclient = new HttpClient();
                    var request = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("https://v1.hitokoto.cn/"),
                    };
                    var resopnse = await httpclient.SendAsync(request);
                    var jsonRaw = await resopnse.Content.ReadAsStringAsync();
                    return JObject.Parse(jsonRaw)["hitokoto"].ToString();
                }
            }
            catch
            {
                Hitokoto = "成长是触不及防的！";
            }
        }

        [RelayCommand]
        private void OnDelete(object obj)
        {
            var delItem = obj as SshHost;
            Hosts.Remove(delItem);
            OnSaveConfig();
        }

        [RelayCommand]
        private void OnAddHost(object obj)
        {
            var host = obj as SshHost;
            Hosts.Insert(0, host);
            OnSaveConfig();
        }

        [RelayCommand]
        void OnAddHostFast()
        {
            var host = new SshHost()
            {
                FriendlyName = $"快速添加 - {DateTime.Now.ToString("yyyy-MM-dd_HH-mm")}",
                Host = FastHost,
                Port = FastPort ?? "22",
                LoginMethod = (LoginMethod)FastVerigyMethod,
                Username = FastUsername ?? "root",
                Password = FastPwd,
                PrivateKey = FastKeyPath
            };
            Hosts.Insert(0, host);
            OnSaveConfig();
        }

        [RelayCommand]
        private void OnAddHostFull(object obj)
        {
            var o = obj as object[];
            var sshHost = new SshHost()
            {
                FriendlyName = (string)o[0],
                Host = (string)o[1],
                Port = (string)o[2] ?? "22",
                Username = (string)o[3] ?? "root",
                LoginMethod = (LoginMethod)o[4],
                Password = (string)o[5],
                PrivateKey = (string)o[6]
            };
            Hosts.Insert(0, sshHost);
            OnSaveConfig();
        }

        [RelayCommand]
        void OnSaveConfig()
        {
            AppSettings.Instance.Hosts = Hosts.ToList();
            AppSettings.Save();
        }

        [RelayCommand]
        void OnSelecetFlyoutKeyPath()
        {
            OpenFileDialog fileDialog = new();
            fileDialog.ShowDialog();
            if (String.IsNullOrEmpty(fileDialog.FileName))
                return;
            FlyoutKeyPath = fileDialog.FileName;
        }

        public void OnNavigatedTo()
        {
            WelcomeText = double.Parse(DateTime.Now.ToString("HH")) switch
            {
                < 6d => "要好好休息呀！身体是最重要的哦",
                < 11d => "早上好，又是美好的一天呢！",
                < 14d => "中午好，记得午休哦！",
                < 18d => "今天会有晚霞嘛？",
                < 22d => "晚上去吃什么呢？",
                < 24d => "夜深了，又在忙什么工作呢？",
                _ => "辛苦啦！",
            };
        }

        public void OnNavigatedFrom() { }
    }
}
