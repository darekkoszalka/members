using System;
using MembersBlazorNet6.Data.Models.Settings;
using MembersBlazorNet6.Data.Models.UnionInstitutionModels;
using MembersBlazorNet6.StaticData;
using Microsoft.EntityFrameworkCore;

namespace MembersBlazorNet6.Data
{
    public class DbInitializer
    {
        public static void InitialMigrations(ApplicationDbContext context)
        {
            try
            {
                if (context.Database.GetPendingMigrations().Count() > 0)
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void AddTypeOfUnionInstitution(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.TypeOfUnionInstitution.Any())
            {
                return;
            }

            foreach (var name in TypeOfUnionInstitutionSD.UnionInstitutionTypes)
            {
                var type = new TypeOfUnionInstitution()
                { Name = name };

                context.Add(type);
            }

            context.SaveChanges();

        }

        public static void AddZG(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.UnionInstitution.Any())
            {
                return;
            }

            var type = context.TypeOfUnionInstitution.FirstOrDefault(t => t.Name == TypeOfUnionInstitutionSD.ZG);

            
            if(type == null)
            {
                return;
            }
            var zg = new UnionInstitution()
            {
                FullName = "Związek Nauczycielstwa Polskiego Zarząd Główny",
                ShortName = "Zarząd Główny ZNP",
                City = "Warszawa",
                TypeOfUnionInstitutionId = type.Id

            };

            var unionStructuresTypes = context.UnionStructureType.ToList();

                context.UnionInstitution.Add(zg);
            context.SaveChanges();

            foreach (var item in unionStructuresTypes)
            {
                var structure = new UnionStructure()
                {
                    UnionInstitutionId = zg.Id,
                    UnionStructureTypeId = item.Id
                };

                context.UnionStructure.Add(structure);
            }

            context.SaveChanges();


        }

        public static void AddMemberWorkPlaces(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.WorkPlace.Any())
            {
                return;
            }

            foreach (var name in MemberWorkPlaceSD.WorkPlaces)
            {
                var workPlace = new WorkPlace()
                { Name = name };

                context.Add(workPlace);
            }

            context.SaveChanges();
        }

        public static void AddMemberEducation(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Education.Any())
            {
                return;
            }

            foreach (var name in MemberEducationSD.Educations)
            {
                var education = new Education()
                { Name = name };

                context.Add(education);
            }

            context.SaveChanges();
        }

        public static void AddUnionFuction(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.UnionFuncionType.Any())
            {
                return;
            }

            foreach (var name in UnionFunctionSD.UnionFunctions)
            {
                var function = new UnionFunctionType()
                { Name = name };

                context.Add(function);
            }

            context.SaveChanges();
        }

        public static void AddUnionStuctureType(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.UnionStructureType.Any())
            {
                return;
            }

            foreach (var name in UnionStructureSD.Structures)
            {
                var structure = new UnionStructureType()
                { Name = name };

                context.Add(structure);
            }

            context.SaveChanges();
        }
    }


}

