using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MembersBlazorNet6.Data.Models.MemberModels;

namespace MembersBlazorNet6.Data.Models.UnionInstitutionModels
{
    public class UnionSectionOrClub
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Name { get; set; }

        public int UnionInstitutionId { get; set; }

        [ForeignKey("UnionInstitutionId")]
        public UnionInstitution UnionInstitution { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}

