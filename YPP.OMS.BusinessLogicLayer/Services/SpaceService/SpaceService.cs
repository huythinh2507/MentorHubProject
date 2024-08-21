using Microsoft.EntityFrameworkCore;
using YPP.MH.DataAccessLayer.Models;

namespace YPP.MH.BusinessLogicLayer.Services.SpaceService
{
    public class SpaceService
    {
        private readonly IDbContextFactory<MHDbContext> _dbContextFactory;

        public SpaceService(IDbContextFactory<MHDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<SpaceGroup> CreateSpaceGroup(SpaceGroup info)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var spaceGroup = new SpaceGroup()
            {
                Name = info.Name,
                TenantId = info.TenantId,
                HideMemberCount = info.HideMemberCount,
                HideFromNonMembers = info.HideFromNonMembers,
                AutoAddToGroupOnJoin = info.AutoAddToGroupOnJoin,
                AutoAddToNewSpaces = info.AutoAddToNewSpaces,
                ShowJoinedSpaces = info.ShowJoinedSpaces,
                Url = info.Url,
            };
            context.SpaceGroup.Add(spaceGroup);

            return spaceGroup;
        }
    }
}
