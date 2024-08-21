namespace YPP.MH.PresentationLayer.ViewModels
{
    public class ResetPasswordViewModel
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
