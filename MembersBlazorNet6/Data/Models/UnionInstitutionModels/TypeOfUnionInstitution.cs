using System;
using System.ComponentModel.DataAnnotations;

namespace MembersBlazorNet6.Data.Models.UnionInstitutionModels
{
    public class TypeOfUnionInstitution
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        public virtual ICollection<UnionInstitution> UnionInstitutions { get; set; }
    }
}

