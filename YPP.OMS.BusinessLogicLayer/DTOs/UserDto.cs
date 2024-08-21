namespace YPP.MH.BusinessLogicLayer.DTOs
{
    public class UserDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime? JoinedOn { get; set; }
        public string JobTitle { get; set; } = string.Empty;
    }
}