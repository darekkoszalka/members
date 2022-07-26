using System;
using System.Security.Claims;
using MembersBlazorNet6.Data.Models.MemberModels;
using MembersBlazorNet6.Data.Repositories.IRepository;
using MembersBlazorNet6.Services.Validations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MembersBlazorNet6.Data.Repositories.Repository
{
    public class MemberRepository : IMemberRepository
    {

        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;


        public MemberRepository(IDbContextFactory<ApplicationDbContext> contextFactory,
            AuthenticationStateProvider authenticationStateProvider
            )
        {
            _contextFactory = contextFactory;
            _authenticationStateProvider = authenticationStateProvider;

        }

        public StatusOperation StatusOperation { get; set; } = new StatusOperation();
        public ApplicationUser ApplicationUser { get; set; }

        public async Task<StatusOperation> CreateMember(Member? member)
        {
            if (member != null)
            {
                try
                {
                    AuthenticationState? authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                    ClaimsPrincipal? user = authState.User;
                    using (var _context = _contextFactory.CreateDbContext())
                    {
                        ApplicationUser = await _context.ApplicationUser
                           .FirstOrDefaultAsync(u => u.Email == user.Identity.Name);
                        if (ApplicationUser == null)
                        {
                            StatusOperation.Status = false;
                            StatusOperation.Message = "Wystąpił błąd w zapisie. Spróbuj ponownie";
                            return StatusOperation;
                        }
                        var peselExist = await _context.Member
                            .FirstOrDefaultAsync(m => m.Pesel == member.Pesel);

                        if (peselExist != null)
                        {
                            StatusOperation.Status = false;
                            StatusOperation.Message = "Osoba o podanym nr pesel jest już wpisana";
                            return StatusOperation;
                        }

                        member.RegisterDate = DateTime.Now;
                        member.RegisterDateUserId = ApplicationUser.Id;
                        member.UpdateDate = DateTime.Now;
                        _context.Member.Add(member);
                        await _context.SaveChangesAsync();
                        StatusOperation.Status = true;
                        StatusOperation.Message = $"Pomyślnie dodano osobę - {member.FullName}";
                        return StatusOperation;
                    }
                }
                catch
                {
                    StatusOperation.Status = false;
                    StatusOperation.Message = $"Osoba {member.FullName} nie została dodana, spróbuj ponownie";
                    return StatusOperation;
                }
            }
            else
            {
                StatusOperation.Status = false;
                StatusOperation.Message = "Wystąpił błąd w zapisie. Spróbuj ponownie";
                return StatusOperation;
            }
        }

        public async Task<bool> Deletemember(int? memberId)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var memberToDelete = await _context.Member.FindAsync(memberId);

                if (memberToDelete != null)
                {

                    //trzeba usunąć jeszcze funkcje, sekcje

                    _context.Member.Remove(memberToDelete);

                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        public async Task<bool> FindExistMemberByPesel(string? pesel)
        {
            using var _context = _contextFactory.CreateDbContext();
            var member = await _context.Member.FirstOrDefaultAsync(m => m.Pesel == pesel);
            if (member != null) return true;
            else return false;
        }

        public async Task<IList<Member>> GetAllMembers()
        {
            try
            {
                using (var _context = _contextFactory.CreateDbContext())
                {
                    IList<Member> members = await _context
                .Member
                .Include(m => m.WorkPlace)
                .Include(m => m.UnionInstitution)
                .Include(m => m.Education)
                .Include(m => m.UnionInstitution.MotherUnionInstitution)
                .ToListAsync();

                    return members;
                }


            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public async Task<Member> GetMemberById(int? memberId)
        {
            if (memberId != null)
            {
                using (var _context = _contextFactory.CreateDbContext())
                {
                    var member = await _context.Member
                        .Include(m => m.Education)
                        .Include(m => m.UnionInstitution)
                        .ThenInclude(u => u.MotherUnionInstitution)
                        .ThenInclude(m => m.MotherUnionInstitution)
                        .Include(m => m.WorkPlace)
                        .FirstOrDefaultAsync(m => m.Id == memberId);
                    return member;
                }
            }
            else
            {
                return null;
            }

        }

        public async Task<Member> GetMemberByPesel(string? pesel)
        {
            if (pesel != null)
            {
                using (var _context = _contextFactory.CreateDbContext())
                {
                    var member = await _context.Member
                        .Include(m => m.Education)
                        .Include(m => m.UnionInstitution)
                        .ThenInclude(u => u.MotherUnionInstitution)
                        .ThenInclude(m => m.MotherUnionInstitution)
                        .Include(m => m.WorkPlace)
                        .FirstOrDefaultAsync(m => m.Pesel == pesel);

                    return member;
                }
            }
            else
            {
                return null;
            }

        }

        public async Task<MemberContact> GetMemberContactByMemberId(int? memberId)
        {
            try
            {
                if (memberId != null)
                {
                    using var _context = _contextFactory.CreateDbContext();
                    var memberContact = await _context.MemberContact
                        .FirstOrDefaultAsync(c => c.MemberId == memberId);
                    return memberContact;
                }
                else return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IList<Member>> GetMembersInBranch(int? branchId)
        {
            try
            {
                using (var _context = _contextFactory.CreateDbContext())
                {
                    IList<Member> members = await _context
                .Member
                .Where(m => m.UnionInstitution.MotherInstitutionId == branchId)
                .Include(m => m.WorkPlace)
                .Include(m => m.UnionInstitution)
                .Include(m => m.UnionInstitution.MotherUnionInstitution)
                .Include(m => m.Education)
                .ToListAsync();

                    return members;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<IList<Member>> GetMembersInDistrict(int? districtId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Member>> GetMembersInFire(int? fireId)
        {
            try
            {
                using (var _context = _contextFactory.CreateDbContext())
                {
                    IList<Member> members = await _context
                .Member
                .Where(m => m.UnionInstitutionId == fireId)
                .Include(m => m.WorkPlace)
                .Include(m => m.UnionInstitution)
                .Include(m => m.UnionInstitution.MotherUnionInstitution)
                .Include(m => m.Education)
                .ToListAsync();
                    return members;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IList<UnionStructureFunction>> UnionFunctions(int? memberId)
        {
            if (memberId != null)
            {
                using (var _context = _contextFactory.CreateDbContext())
                {
                    var functions = await _context.UnionStructureFunction
                        .Where(f => f.MemberId == memberId)
                        .Include(f => f.UnionFunctionType)
                        .Include(f => f.UnionStrukture)
                        .Include(f => f.UnionStrukture.UnionStructureType)
                        .Include(f => f.UnionStrukture.UnionInstitution)
                        .ToListAsync();

                    return functions;
                }

            }
            else
            {
                return null;
            }
        }

        public async Task<StatusOperation> UpdateMember(Member? member)
        {
            try
            {
                if (member == null)
                {
                    StatusOperation.Status = false;
                    StatusOperation.Message = "Wystąpił błąd w zapisie. Spróbuj ponownie";
                    return StatusOperation;
                }
                AuthenticationState? authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                ClaimsPrincipal? user = authState.User;
                using (var _context = _contextFactory.CreateDbContext())
                {
                    if (member != null)
                    {
                        ApplicationUser = await _context.ApplicationUser
                               .FirstOrDefaultAsync(u => u.Email == user.Identity.Name);
                        if (ApplicationUser == null)
                        {
                            StatusOperation.Status = false;
                            StatusOperation.Message = "Wystąpił błąd w zapisie. Spróbuj ponownie";
                            return StatusOperation;
                        }
                        member.RegisterDateUserId = ApplicationUser.Id;
                        member.UpdateDate = DateTime.Now;
                        _context.Member.Update(member);

                        await _context.SaveChangesAsync();
                        StatusOperation.Status = true;
                        StatusOperation.Message = "Pomyślnie zaktualizowano dane";
                        return StatusOperation;
                    }
                    else
                    {
                        StatusOperation.Status = false;
                        StatusOperation.Message = "Wystąpił błąd w zapisie. Spróbuj ponownie";
                        return StatusOperation;
                    }
                }
            }
            catch
            {
                StatusOperation.Status = false;
                StatusOperation.Message = "Wystąpił błąd w zapisie. Spróbuj ponownie";
                return StatusOperation;
            }

        }


    }
}

