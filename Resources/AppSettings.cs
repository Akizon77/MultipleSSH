using MultipleSSH.Models;
using Newtonsoft.Json.Linq;
using System.IO;
using Wpf.Ui.Controls;

namespace MultipleSSH.Resources
{
    public class AppSettings
    {
        public static AppSettings Instance;

        public static AppSettings GetSettings() => Instance;

        //Light Dark
        public Wpf.Ui.Appearance.ApplicationTheme Theme;

        //Acrylic Mica
        public WindowBackdropType Backdrop;

        public List<SshHost> Hosts;

        public AppSettings()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "config.json");
            string jsonStr;
            if (File.Exists(path))
            {
                jsonStr = File.ReadAllText(path);
                JObject json = JObject.Parse(jsonStr);
                Theme = json["Theme"].ToObject<Wpf.Ui.Appearance.ApplicationTheme>();
                Backdrop = json["Backdrop"].ToObject<WindowBackdropType>();
                try
                {
                    Hosts = json["Hosts"].ToObject<List<SshHost>>();
                }
                catch (Exception)
                {
                    Hosts = new List<SshHost>();
                }
            }
            else
            {
                Backdrop = WindowBackdropType.Mica;
                Theme = Wpf.Ui.Appearance.ApplicationTheme.Light;
                Hosts = new List<SshHost>();
                Save();
            }
            Instance = this;
        }

        //保存
        public static bool Save()
        {
            try
            {
                var path = Environment.CurrentDirectory;
                path = Path.Combine(path, "config.json");
                var json = JObject.FromObject(Instance);
                File.WriteAllText(path, json.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}