using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MembersBlazorNet6.Data.Models.UnionInstitutionModels
{
    public class UnionInstitutionAddress
    {
        [Key]
        public int Id { get; set; }

        public int UnionInstitutionId { get; set; }


        [ForeignKey("UnionInstitutionId")]
        public UnionInstitution UnionInstitution { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Kod pocztowy")]
        [RegularExpression("^\\d{2}[- ]{0,1}\\d{3}$", ErrorMessage = "Poprawny format xx-xxx")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Miejscowość")]
        public string City { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Poczta")]
        public string PostCity { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Nr domu")]
        public string HouseNumber { get; set; }

        [Display(Name = "Nr lokalu")]
        public string? ApartamentNumber { get; set; }
    }
}

