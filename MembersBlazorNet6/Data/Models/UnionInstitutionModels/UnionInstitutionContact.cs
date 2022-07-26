using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MembersBlazorNet6.Data.Models.UnionInstitutionModels
{
    public class UnionInstitutionContact
    {
        [Key]
        public int Id { get; set; }

        public int? UnionInstitutionId { get; set; }

        [ForeignKey("UnionInstitutionId")]
        public UnionInstitution UnionInstitution { get; set; }

        
        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Pole wymagane")]
        [MinLength(9, ErrorMessage = "nr telefonu musi mieć 9 znaków")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Wpisz prawidłowy adres email")]
        [Required(ErrorMessage = "Pole wymagane")]       
        public string Email { get; set; }

        [Display(Name = "Strona internetowa")]
        public string? WWW { get; set; }

        [Display(Name = "Link do strony")]
        public string? LinkWww { get; set; }
    }
}

