namespace MultipleSSH.Models
{
    public class SshHost
    {
        public string FriendlyName { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public string? PrivateKey { get; set; }
        public LoginMethod LoginMethod { get; set; }
    }

    public enum LoginMethod
    {
        Password,
        PrivateKey
    }
}