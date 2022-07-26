using System;
using MembersBlazorNet6.Data.Models.MemberModels;
using MembersBlazorNet6.Data.Models.Settings;
using MembersBlazorNet6.Data.Models.UnionInstitutionModels;
using MembersBlazorNet6.Data.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MembersBlazorNet6.Data.Repositories.Repository
{
    public class UnionStructureRepository : IUnionStructureRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public UnionStructureRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> AddMemberToStructure(UnionStructureFunction unionStructureFunction)
        {
            if(unionStructureFunction != null)
            {
                try
                {
                    using (var _context = _contextFactory.CreateDbContext())
                    {
                        _context.UnionStructureFunction.Add(unionStructureFunction);

                        await _context.SaveChangesAsync();

                        return true;
                    }
                }
                catch
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CreateUnionStructure(UnionStructure structure, int? unionInstitutionId)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {               
                             
                    _context.UnionStructure.Add(structure);

                    await _context.SaveChangesAsync();
                
            }

            return true;
        }

        public async Task<bool> DeleteUnionStructure(int structureId)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var structureToDelete = await _context.UnionStructure.FindAsync(structureId);

                if (structureToDelete != null)
                {
                    _context.UnionStructure.Remove(structureToDelete);

                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<IList<UnionStructure>> GetAllUnionStructures()
        {
            try
            {
                using (var _context = _contextFactory.CreateDbContext())
                {
                    IList<UnionStructure> structures = await _context.UnionStructure
                .ToListAsync();

                    return structures;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IList<UnionFunctionType>> GetUnionFunctionTypes()
        {
            using(var _context = _contextFactory.CreateDbContext())
            {
                var functionTypes = await _context.UnionFuncionType
                    .ToListAsync();

                return functionTypes;
            }
        }

        public async Task<UnionStructure> GetUnionStructureById(int? unionStructureId)
        {
            if(unionStructureId != null)
            {
                using(var _context = _contextFactory.CreateDbContext())
                {
                    var structure = await _context.UnionStructure
                        .Include(s=>s.UnionInstitution)
                        .Include(s=>s.UnionStructureType)
                        .FirstOrDefaultAsync(s => s.Id == unionStructureId);

                    return structure;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<IList<UnionStructure>> GetUnionStructuresInUnionInstitution(int unionInstitutionId)
        {
            try
            {
                using (var _context = _contextFactory.CreateDbContext())
                {
                    IList<UnionStructure> structures = await _context
                        .UnionStructure
                        .Include(s=>s.UnionInstitution)
                        .Include(s=>s.UnionStructureType)
                        .Where(u=>u.UnionInstitutionId == unionInstitutionId)
                .ToListAsync();

                    return structures;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IList<UnionStructureFunction>> MembersInStructureByStructureId(int? unionStructureId)
        {
            if(unionStructureId != null)
            {
                using(var _context = _contextFactory.CreateDbContext())
                {
                    var membersInStructure = await _context.UnionStructureFunction
                        .Where(s=>s.UnionStructureId == unionStructureId)
                        .Include(s=>s.UnionFunctionType)
                        .Include(s=>s.Member)
                        .Include(s=>s.Member.UnionInstitution)
                        .Include(s => s.Member.WorkPlace)
                        .Include(s => s.Member.Education)
                        .Include(s => s.Member.UnionInstitution.MotherUnionInstitution)
                        .Include(s => s.Member.UnionInstitution.MotherUnionInstitution.MotherUnionInstitution)
                        .Include(s=>s.UnionStrukture)
                        .OrderBy(s=>s.Member.LastName)
                        .ToListAsync();

                    return membersInStructure;
                }       
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateUnionStructure(UnionStructure structure)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {

                if (structure != null)
                {

                    _context.UnionStructure.Update(structure);

                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

