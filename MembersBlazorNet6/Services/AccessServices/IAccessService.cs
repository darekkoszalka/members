using System;
using MembersBlazorNet6.Data;
using MembersBlazorNet6.Data.Models.UnionInstitutionModels;

namespace MembersBlazorNet6.Services.AccessServices
{
    public interface IAccessService
    {
        public Task<bool> AccessByInsitutionId(int? unionIstitutionId);
        public Task<UnionInstitution?> AccessBranchToFire(int? fireId);
        public Task<ApplicationUser?> GetApplicationUser();
        public Task<IList<UserAccess>> UserAccessesIfAuthentication();
        public Task<bool> CreateUserAccess(ApplicationUser? user, int? unionInstitutionId, string? role);
        public Task<bool> CreateSuperAdminAccess(ApplicationUser? user, string? role);
        public Task<UserAccess> GetUserAccessesByUserIdAndUnionInstId(string? userId, int? unionInstitutionId, string? role);
        public Task<IList<UserAccess>> GetUnionInstitutionAccessessByInstId(int? unionInstitutionId);
    }
}

