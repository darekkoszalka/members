using System;
using MembersBlazorNet6.Data.Models.UnionInstitutionModels;

namespace MembersBlazorNet6.Data.Repositories.IRepository
{
    public interface IUnionInstitutionRepository
    {
        public Task<bool> CreateUnionInstitution(UnionInstitution unionInstitution);

        public Task<bool> UpdateUnionInstitution(UnionInstitution unionInstitution);

        public Task<bool> DeleteUnionInstitution(int? unionInstitutionId);

        public Task<UnionInstitution> GetUnionInstitutionById(int? unionInstitutionId);

        public Task<IList<UnionInstitution>> GetAllDictricts();

        public Task<IList<UnionInstitution>> GetAllBranches(int? typeOfUnionInstitutionId);

        public Task<IList<UnionInstitution>> GetBranchesInDistrict(int? districtId);

        public Task<IList<UnionInstitution>> GetAllFires(int? typeOfUnionInstitutionId);

        public Task<IList<UnionInstitution>> GetFiresInBranch(int? branchId);
        /// <summary>
        /// Pobiera pojedyncze ognisko za pomocą id oddziału
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public Task<UnionInstitution> GetFireByBranchId(int? branchId);

    }
}

