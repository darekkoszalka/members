using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MembersBlazorNet6.Data.Models.MemberModels;
using MembersBlazorNet6.Data.Models.Settings;

namespace MembersBlazorNet6.Data.Models.UnionInstitutionModels
{
    public class UnionStructure
    {
        [Key]
        public int Id { get; set; }

        public int UnionInstitutionId { get; set; }

        [ForeignKey("UnionInstitutionId")]
        public UnionInstitution? UnionInstitution { get; set; }

        [Required(ErrorMessage = "Wybierz typ struktury")]
        public int? UnionStructureTypeId { get; set; }

        [ForeignKey("UnionStructureTypeId")]
        public UnionStructureType? UnionStructureType { get; set; }

        public ICollection<Member>? Members { get; set; }
    }
}

