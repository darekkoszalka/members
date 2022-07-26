using System;
using MembersBlazorNet6.Data.Models.MemberModels;
using MembersBlazorNet6.Services.Validations;

namespace MembersBlazorNet6.Data.Repositories.IRepository
{
    public interface IMemberRepository
    {
        /// <summary>
        /// Tworzy new Member i zwraca StatusOperation
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task<StatusOperation> CreateMember(Member? member);
        public Task<StatusOperation> UpdateMember(Member? member);
        public Task<bool> Deletemember(int? memberId);
        public Task<Member> GetMemberById(int? memberId);
        public Task<bool> FindExistMemberByPesel(string? pesel);
        public Task<Member> GetMemberByPesel(string? pesel);
        public Task<IList<Member>> GetAllMembers();
        public Task<IList<Member>> GetMembersInFire(int? fireId);
        public Task<IList<Member>> GetMembersInBranch(int? branchId);
        public Task<IList<Member>> GetMembersInDistrict(int? districtId);
        public Task<IList<UnionStructureFunction>> UnionFunctions(int? memberId);
        /// <summary>
        /// Pobiera dane kontaktowe za pomocą 'memberId'
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public Task<MemberContact> GetMemberContactByMemberId(int? memberId);

    }
}

