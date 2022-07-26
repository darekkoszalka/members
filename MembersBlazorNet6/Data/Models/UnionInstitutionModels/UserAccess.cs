using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MembersBlazorNet6.Data.Models.UnionInstitutionModels
{
    public class UserAccess
    {
        [Key]
        public int Id { get; set; }
        public string? Access { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
        public int? UnionInstitutionId { get; set; }
        [ForeignKey("UnionInstitutionId")]
        public UnionInstitution? UnionInstitution { get; set; }
        public bool Lock { get; set; }
    }
}

