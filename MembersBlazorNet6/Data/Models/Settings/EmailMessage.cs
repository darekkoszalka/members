using System;
using System.ComponentModel.DataAnnotations;

namespace MembersBlazorNet6.Data.Models.Settings
{
    public class EmailMessage
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Temat wiadomości")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string? Title { get; set; }

        [Display(Name = "Treść")]
        public string? Content { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Adresat")]
        public string? Adresat { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Pole wymagane")]
        [EmailAddress(ErrorMessage = "Wpisz prawidłowy adres email")]
        public string? AdresEmail { get; set; }

        [Display(Name = "Data wysłania")]
        public DateTime EmailDate { get; set; }
    }
}

