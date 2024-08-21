﻿namespace YPP.MH.PresentationLayer.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; internal set; } = false;
    }
}
