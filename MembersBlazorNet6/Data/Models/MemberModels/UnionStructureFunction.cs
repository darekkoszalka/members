using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MembersBlazorNet6.Data.Models.Settings;
using MembersBlazorNet6.Data.Models.UnionInstitutionModels;

namespace MembersBlazorNet6.Data.Models.MemberModels
{
    public class UnionStructureFunction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UnionStructureId { get; set; }

        [ForeignKey("UnionStructureId")]
        public UnionStructure? UnionStrukture { get; set; }

        [Required]
        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Member? Member { get; set; }

        [Required(ErrorMessage ="Wybierz funkcję")]
        public int? UnionFunctionTypeId { get; set; }

        [ForeignKey("UnionFunctionTypeId")]
        public UnionFunctionType? UnionFunctionType { get; set; }
    }
}

