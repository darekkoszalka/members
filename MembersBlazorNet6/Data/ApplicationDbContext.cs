
using MembersBlazorNet6.Data.Models.MemberModels;
using MembersBlazorNet6.Data.Models.Settings;
using MembersBlazorNet6.Data.Models.UnionInstitutionModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MembersBlazorNet6.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUser { get; set; }
    public DbSet<UserAccess> UserAccess { get; set; }

    //unionInstitution

    public DbSet<TypeOfUnionInstitution> TypeOfUnionInstitution { get; set; }
    public DbSet<UnionInstitution> UnionInstitution { get; set; }
    public DbSet<UnionInstitutionAddress> UnionInstitutionAddress { get; set; }
    public DbSet<UnionInstitutionContact> UnionInstitutionContact { get; set; }
    public DbSet<UnionStructure> UnionStructure { get; set; }
    public DbSet<UnionSectionOrClub> UnionSectionOrClub { get; set; }

    //settings
    public DbSet<EmailPortsSetting> EmailPortsSetting { get; set; }
    public DbSet<EmailMessage> EmailMessages { get; set; }
    public DbSet<UnionFunctionType> UnionFuncionType { get; set; }
    public DbSet<WorkPlace> WorkPlace { get; set; }
    public DbSet<Education> Education { get; set; }
    public DbSet<UnionStructureType> UnionStructureType { get; set; }
    public DbSet<UnionSectionFunctionType> UnionSectionType { get; set; }

    //member

    public DbSet<Member> Member { get; set; }
    public DbSet<MemberContact> MemberContact { get; set; }
    public DbSet<UnionStructureFunction> UnionStructureFunction { get; set; }
    public DbSet<MemberPayment> MemberPayment { get; set; }




}

