using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleSSH.Models
{
    public class I18n
    {
        public static I18n Instance;
        public string? AppTitle;
        public string? NaviMenu_Terminal;
        public string? NaviMenu_Home;
        public string? NaviMenu_Settings;
        public string? Universe_Add;
        public string? Universe_None;
        public string? Universe_Delete;
        public string? Universe_About;
        public string? Page_Main_SearchBoxPlaceholder;
        public string? Page_Main_FastConnect;
        public string? Model_SSHhost_FriendlyName;
        public string? Model_SSHhost_Host;
        public string? Model_SSHhost_IP;
        public string? Model_SSHhost_Port;
        public string? Model_SSHhost_User;
        public string? Model_SSHhost_Password;
        public string? Model_SSHhost_PrivateKey;
        public string? Model_SSHhost_LoginMethod;
        public string? Page_Settings_Personalized;
        public string? Page_Settings_Personalized_Color;
        public string? Page_Settings_Personalized_Color_Light;
        public string? Page_Settings_Personalized_Color_Dark;
        public string? Page_Settings_Personalized_Backdrop;
        public string? Page_Settings_Personalized_Backdrop_Arcylic;
        public string? Page_Settings_Personalized_Language;
        public static void Init()
        {
            Instance = new I18n();
            
        }
        public static void SwitchTo(Language language)
        {
            
        }
        private void Load()
        {

        }
    }
    public enum Language
    {
        English,
        简体中文,
        日本語
    }
}
