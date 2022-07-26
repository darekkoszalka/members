using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MembersBlazorNet6.Data.Models.MemberModels
{
    public class MemberContact
    {
        [Key]
        public int Id { get; set; }

        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Member? Member { get; set; }


        [Display(Name = "Telefon")]
        [MinLength(9, ErrorMessage = "nr telefonu musi mieć 9 znaków")]
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "Wpisz prawidłowy adres email lub pozostaw pole puste")]
        public string? Email { get; set; }

        [Display(Name = "Kod pocztowy")]
        [RegularExpression("^\\d{2}[- ]{0,1}\\d{3}$", ErrorMessage = "Poprawny format xx-xxx")]
        public string? PostCode { get; set; }

        [Display(Name = "Miejscowość")]
        public string? City { get; set; }

        [Display(Name = "Poczta")]
        public string? PostCity { get; set; }

        [Display(Name = "Ulica")]
        public string? Street { get; set; }

        [Display(Name = "Nr domu")]
        public string? HouseNumber { get; set; }

        [Display(Name = "Nr lokalu")]
        public string? ApartamentNumber { get; set; }


    }
}

