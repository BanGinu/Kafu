﻿namespace Kafu.Model.ViewModel
{
    public class EmailConfig
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; }
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
    }
}
