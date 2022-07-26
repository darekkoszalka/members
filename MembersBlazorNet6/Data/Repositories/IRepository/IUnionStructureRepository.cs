using System;
using MembersBlazorNet6.Data.Models.MemberModels;
using MembersBlazorNet6.Data.Models.Settings;
using MembersBlazorNet6.Data.Models.UnionInstitutionModels;

namespace MembersBlazorNet6.Data.Repositories.IRepository
{
    public interface IUnionStructureRepository
    {
        public Task<bool> CreateUnionStructure(UnionStructure structure, int? unionInstitutionId);

        public Task<bool> UpdateUnionStructure(UnionStructure structure);

        public Task<bool> DeleteUnionStructure(int structureId);

        public Task<IList<UnionStructure>> GetAllUnionStructures();

        public Task<IList<UnionStructure>> GetUnionStructuresInUnionInstitution(int unionInstitutionId);

        public Task<UnionStructure> GetUnionStructureById(int? unionStructureId);

        public Task<IList<UnionStructureFunction>> MembersInStructureByStructureId(int? unionStructureId);

        public Task<IList<UnionFunctionType>> GetUnionFunctionTypes();

        public Task<bool> AddMemberToStructure(UnionStructureFunction unionStructureFunction);
    }
}

