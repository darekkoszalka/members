using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MembersBlazorNet6.Data.Models.UnionInstitutionModels
{
    public class UnionInstitution
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "*Pole wymagane")]
        [Display(Name = "*Pełna nazwa")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "*Pole wymagane")]
        [Display(Name = "*Nazwa skrócona")]
        public string? ShortName { get; set; }


        public int TypeOfUnionInstitutionId { get; set; }

        [ForeignKey("TypeOfUnionInstitutionId")]
        public TypeOfUnionInstitution TypeOfUnionInstitution { get; set; }

        [Required(ErrorMessage = "*Pole wymagane")]
        [Display(Name = "*Miejscowość")]
        public string? City { get; set; }

        public int? MotherInstitutionId { get; set; }

        [ForeignKey("MotherInstitutionId")]
        public UnionInstitution MotherUnionInstitution { get; set; }
    }
}

