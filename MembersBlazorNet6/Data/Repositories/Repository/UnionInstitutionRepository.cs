using System;
using MembersBlazorNet6.Data.Models.Settings;
using MembersBlazorNet6.Data.Models.UnionInstitutionModels;
using MembersBlazorNet6.Data.Repositories.IRepository;
using MembersBlazorNet6.StaticData;
using Microsoft.EntityFrameworkCore;

namespace MembersBlazorNet6.Data.Repositories.Repository
{
    public class UnionInstitutionRepository : IUnionInstitutionRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public UnionInstitutionRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TypeOfUnionInstitution? TypeOfUnionInstitution { get; set; }

        public IList<UnionStructureType>? UnionStructureTypes { get; set; }

        public UnionStructure UnionStructure { get; set; }

        public async Task<bool> CreateUnionInstitution(UnionInstitution unionInstitution)
        {
            try
            {
                using (var _context = _contextFactory.CreateDbContext())
                {
                    _context.UnionInstitution.Add(unionInstitution);

                    await _context.SaveChangesAsync();
                }

                using (var _context = _contextFactory.CreateDbContext())
                {
                    TypeOfUnionInstitution = await _context.TypeOfUnionInstitution
                        .FirstOrDefaultAsync(t => t.Name == TypeOfUnionInstitutionSD.Fire);

                    UnionStructureTypes = await _context.UnionStructureType
                        .ToListAsync();

                    if (unionInstitution.TypeOfUnionInstitutionId == TypeOfUnionInstitution.Id)
                    {
                        var type = await _context.UnionStructureType
                            .FirstOrDefaultAsync(t => t.Name == UnionStructureSD.Management);
                        UnionStructure = new UnionStructure()
                        {
                            UnionInstitutionId = unionInstitution.Id,
                            UnionStructureTypeId = type.Id
                        };
                        _context.UnionStructure.Add(UnionStructure);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        foreach (var item in UnionStructureTypes)
                        {
                            UnionStructure = new UnionStructure()
                            {
                                UnionInstitutionId = unionInstitution.Id,
                                UnionStructureTypeId = item.Id
                            };
                            _context.UnionStructure.Add(UnionStructure);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }




        }

        public async Task<bool> DeleteUnionInstitution(int? unionInstitutionId)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var institutionToDelete = await _context.UnionInstitution
                    .Include(u => u.MotherUnionInstitution)
                    .FirstOrDefaultAsync(u => u.Id == unionInstitutionId);

                if (institutionToDelete != null)
                {
                    var unionStructures = await _context.UnionStructure
                        .Where(s => s.UnionInstitutionId == unionInstitutionId)
                        .ToListAsync();
                    foreach (var structure in unionStructures)
                    {
                        _context.UnionStructure.Remove(structure);
                    }
                    await _context.SaveChangesAsync();

                    _context.UnionInstitution.Remove(institutionToDelete);

                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<IList<UnionInstitution>> GetAllBranches(int? typeOfUnionInstitutionId)
        {
            try
            {
                using (var _context = _contextFactory.CreateDbContext())
                {
                    TypeOfUnionInstitution = await _context.TypeOfUnionInstitution
                    .FirstOrDefaultAsync(t => t.Name == TypeOfUnionInstitutionSD.Branch);

                    var branches = await _context.UnionInstitution
                        .Where(u => u.TypeOfUnionInstitutionId == TypeOfUnionInstitution.Id)
                        .OrderBy(u => u.FullName)
                        .ToListAsync();

                    return branches;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IList<UnionInstitution>> GetAllDictricts()
        {
            try
            {
                using (var _context = _contextFactory.CreateDbContext())
                {
                    TypeOfUnionInstitution = await _context.TypeOfUnionInstitution
                    .FirstOrDefaultAsync(t => t.Name == TypeOfUnionInstitutionSD.District);

                    var districs = await _context.UnionInstitution
                        .Where(u => u.TypeOfUnionInstitutionId == TypeOfUnionInstitution.Id)
                        .OrderBy(u => u.FullName)
                        .ToListAsync();

                    return districs;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public Task<IList<UnionInstitution>> GetAllFires(int? typeOfUnionInstitutionId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<UnionInstitution>> GetBranchesInDistrict(int? districtId)
        {

            if (districtId != null)
            {
                using (var _context = _contextFactory.CreateDbContext())
                {

                    var branches = await _context.UnionInstitution
                        .Where(u => u.MotherInstitutionId == districtId)
                        .OrderBy(u => u.FullName)
                        .ToListAsync();

                    return branches;
                }
            }
            else
            {
                return null;
            }



        }

        public async Task<UnionInstitution> GetFireByBranchId(int? branchId)
        {
            if (branchId != null)
            {
                using var _context = _contextFactory.CreateDbContext();
                var fire = await _context.UnionInstitution
                    .FirstOrDefaultAsync(u => u.MotherInstitutionId == branchId);
                return fire;
            }
            else
            {
                return null;
            }
        }

        public async Task<IList<UnionInstitution>> GetFiresInBranch(int? branchId)
        {
            if (branchId != null)
            {
                using (var _context = _contextFactory.CreateDbContext())
                {
                    var fires = await _context.UnionInstitution
                        .Where(u => u.MotherInstitutionId == branchId)
                        .Include(u => u.MotherUnionInstitution)
                        .Include(u => u.TypeOfUnionInstitution)
                        .OrderBy(u => u.FullName)
                        .ToListAsync();
                    return fires;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<UnionInstitution> GetUnionInstitutionById(int? unionInstitutionId)
        {
            if (unionInstitutionId != null)
            {
                using (var _context = _contextFactory.CreateDbContext())
                {
                    var unionInstitution = await _context.UnionInstitution
                        .Include(u => u.MotherUnionInstitution)
                        .Include(u => u.TypeOfUnionInstitution)
                        .FirstOrDefaultAsync(u => u.Id == unionInstitutionId);
                    return unionInstitution;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateUnionInstitution(UnionInstitution unionInstitution)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                try
                {
                    _context.UnionInstitution.Update(unionInstitution);

                    await _context.SaveChangesAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}

