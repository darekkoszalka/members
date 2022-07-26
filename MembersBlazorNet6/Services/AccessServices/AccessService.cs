using System;
using System.Security.Claims;
using MembersBlazorNet6.Data;
using MembersBlazorNet6.Data.Models.UnionInstitutionModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MembersBlazorNet6.Services.AccessServices
{
    public class AccessService : IAccessService
    {

        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AccessService(IDbContextFactory<ApplicationDbContext> contextFactory, AuthenticationStateProvider authenticationStateProvider
            )
        {
            _contextFactory = contextFactory;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public UnionInstitution? UnionInstitution { get; set; }
        public IList<UserAccess> UserAccessesList { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public UserAccess? UserAccess { get; set; }

        public async Task<UnionInstitution?> AccessBranchToFire(int? fireId)
        {
            try
            {
                AuthenticationState? authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                ClaimsPrincipal? user = authState.User;
                if (user != null)
                {
                    if (user.Identity.IsAuthenticated)
                    {
                        using (var _context = _contextFactory.CreateDbContext())
                        {
                            ApplicationUser = await _context.ApplicationUser
                           .FirstOrDefaultAsync(u => u.Email == user.Identity.Name);
                            if (ApplicationUser != null)
                            {
                                var acceessList = await _context.UserAccess
                                .Where(a => a.UserId == ApplicationUser.Id)
                                .ToListAsync();
                                UserAccess = acceessList.Find(a => a.UnionInstitutionId == fireId);
                                if (UserAccess != null)
                                {
                                    if (!UserAccess.Lock)
                                    {
                                        UnionInstitution = await _context.UnionInstitution
                                        .Include(u => u.TypeOfUnionInstitution)
                                        .Include(u => u.MotherUnionInstitution)
                                    .FirstOrDefaultAsync(u => u.MotherInstitutionId == UserAccess.UnionInstitutionId);
                                        return UnionInstitution;
                                    }
                                    return null;
                                }
                                return null;
                            }
                            return null;
                        }
                    }
                    return null;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AccessByInsitutionId(int? unionInstitutionId)
        {
            try
            {
                AuthenticationState? authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                ClaimsPrincipal? user = authState.User;
                if (user != null)
                {
                    if (user.Identity.IsAuthenticated)
                    {
                        using (var _context = _contextFactory.CreateDbContext())
                        {
                            var applicationUser = await _context.ApplicationUser
                            .FirstOrDefaultAsync(u => u.Email == user.Identity.Name);
                            if (applicationUser != null)
                            {
                                var accessList = await _context.UserAccess
                                .Where(a => a.UserId == applicationUser.Id)
                                .ToListAsync();
                                UserAccess = accessList.Find(a => a.UnionInstitutionId == unionInstitutionId);
                                if (UserAccess != null)
                                {
                                    if (!UserAccess.Lock)
                                    {
                                        return true;
                                    }
                                    return false;
                                }
                                return false;
                            }
                            return false;
                        }
                    }
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> CreateSuperAdminAccess(ApplicationUser? user, string? role)
        {
            try
            {
                if (user != null && role != null)
                {
                    UserAccess = new UserAccess()
                    {
                        UserId = user.Id,
                        Access = role,
                        Lock = false
                    };
                    using (var _context = _contextFactory.CreateDbContext())
                    {
                        _context.UserAccess.Add(UserAccess);
                        await _context.SaveChangesAsync();
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateUserAccess(ApplicationUser? user, int? unionInstitutionId, string? role)
        {
            try
            {
                if (user != null && unionInstitutionId != null)
                {
                    UserAccess = new UserAccess()
                    {
                        UserId = user.Id,
                        UnionInstitutionId = unionInstitutionId,
                        Access = role,
                        Lock = false
                    };
                    using (var _context = _contextFactory.CreateDbContext())
                    {
                        _context.UserAccess.Add(UserAccess);
                        await _context.SaveChangesAsync();
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ApplicationUser?> GetApplicationUser()
        {
            try
            {
                AuthenticationState? authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                ClaimsPrincipal? user = authState.User;
                if (user != null)
                {
                    using (var _context = _contextFactory.CreateDbContext())
                    {
                        ApplicationUser = await _context.ApplicationUser
                            .FirstOrDefaultAsync(u => u.Email == user.Identity.Name);
                    }
                    return ApplicationUser;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IList<UserAccess>> GetUnionInstitutionAccessessByInstId(int? unionInstitutionId)
        {
            try
            {
                if (unionInstitutionId != null)
                {
                    using (var _context = _contextFactory.CreateDbContext())
                    {
                        UserAccessesList = await _context.UserAccess
                            .Where(a => a.UnionInstitutionId == unionInstitutionId)
                            .Include(a => a.ApplicationUser)
                            .Include(a => a.UnionInstitution)
                            .ToListAsync();
                    }
                    return UserAccessesList;
                }
                else return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserAccess> GetUserAccessesByUserIdAndUnionInstId(string? userId, int? unionInstitutionId, string? role)
        {
            try
            {
                if (userId == null && unionInstitutionId == null)
                {
                    return null;
                }
                using (var _context = _contextFactory.CreateDbContext())
                {
                    UserAccess = await _context.UserAccess
                        .FirstOrDefaultAsync(a => a.UserId == userId && a.UnionInstitutionId == unionInstitutionId && a.Access == role);
                }
                return UserAccess;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IList<UserAccess>> UserAccessesIfAuthentication()
        {
            try
            {
                AuthenticationState? authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                ClaimsPrincipal? user = authState.User;
                if (user != null)
                {
                    using (var _context = _contextFactory.CreateDbContext())
                    {
                        var applicationUser = await _context.ApplicationUser
                            .FirstOrDefaultAsync(u => u.Email == user.Identity.Name);
                        if (applicationUser != null)
                        {
                            UserAccessesList = await _context.UserAccess
                                .Where(a => a.UserId == applicationUser.Id)
                                .ToListAsync();
                        }
                    }
                    return UserAccessesList;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}

