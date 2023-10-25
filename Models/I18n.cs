using MultipleSSH.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleSSH.Models
{
    public class I18n
    {
        public static I18n Instance;
        public string? AppTitle {  get; set; }
        public string? NaviMenu_Terminal { get; set; }
        public string? NaviMenu_Home { get; set; }
        public string? NaviMenu_Settings { get; set; }
        public string? Universe_Add { get; set; }
        public string? Universe_None { get; set; }
        public string? Universe_Delete { get; set; }
        public string? Universe_About { get; set; }
        public string? Page_Main_SearchBoxPlaceholder { get; set; }
        public string? Page_Main_FastConnect { get; set; }
        public string? Model_SSHhost_FriendlyName { get; set; }
        public string? Model_SSHhost_Host { get; set; }
        public string? Model_SSHhost_IP { get; set; }
        public string? Model_SSHhost_Port { get; set; }
        public string? Model_SSHhost_User { get; set; }
        public string? Model_SSHhost_Password { get; set; }
        public string? Model_SSHhost_PrivateKey { get; set; }
        public string? Model_SSHhost_LoginMethod { get; set; }
        public string? Page_Settings_Appearance { get; set; }
        public string? Page_Settings_Appearance_Color { get; set; }
        public string? Page_Settings_Appearance_Color_SubText { get; set; }
        public string? Page_Settings_Appearance_Color_Light { get; set; }
        public string? Page_Settings_Appearance_Color_Dark { get; set; }
        public string? Page_Settings_Appearance_Backdrop { get; set; }
        public string? Page_Settings_Appearance_Backdrop_SubText { get; set; }
        public string? Page_Settings_Appearance_Backdrop_Arcylic { get; set; }
        public string? Page_Settings_Appearance_Language { get; set; }
        public string? Page_Settings_Appearance_Language_SubText { get; set; }
        public string? Page_Settings_Debug { get; set; }
        public string? Page_Settings_Debug_GenerateLangFile { get; set; }
        public string? Page_Settings_Debug_GenerateLangFile_SubText { get; set; }
        public string? Page_Settings_Debug_GenerateLangFile_Button { get; set; }

        public I18n()
        {
            Instance = this;
        }
        public static void SwitchTo(Language language)
        {
            var langPath = Path.Combine(Environment.CurrentDirectory, "lang");
            string targetLang = language switch
            {
                Language.English => "en_us.json",
                Language.简体中文 => "zh_cn.json",
                Language.日本語 => "ja_jp.json",
            };
            var langFile = Path.Combine(langPath, targetLang);
            var content = File.ReadAllText(langFile);
            Instance = JObject.Parse(content).ToObject<I18n>();
            AppSettings.Instance.Lang = language;
            AppSettings.Save();
        }

        public static void GenerateLanguageFile()
        {
            var j = JObject.FromObject(Instance);
            var langPath = Path.Combine(Environment.CurrentDirectory, "lang");
            string targetLang = AppSettings.Instance.Lang switch
            {
                Language.English => "en_us.json",
                Language.简体中文 => "zh_cn.json",
                Language.日本語 => "ja_jp.json",
            };
            var cn_file = Path.Combine(langPath, targetLang);
            if (!Directory.Exists(langPath))
                Directory.CreateDirectory(langPath);
            if (!File.Exists(cn_file))
                File.WriteAllText(cn_file, j.ToString());
        }

        private void Load() { }
    }

    public enum Language
    {
        English,
        简体中文,
        日本語
    }
}
