namespace YPP.MH.BusinessLogicLayer.DTOs
{
    public class CreateSpaceGroupDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Url { get; set; } = string.Empty;
        public int TenantId { get; set; }
        public bool HideMemberCount { get; set; } = false;
        public bool HideFromNonMembers { get; set; } = false;
        public bool ShowJoinedSpaces { get; set; } = false;
        public bool AutoAddToNewSpaces { get; set; } = false;
        public bool AutoAddToGroupOnJoin { get; set; } = false;
    }
}
