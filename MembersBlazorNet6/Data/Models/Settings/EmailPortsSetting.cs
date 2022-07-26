using System;
using System.ComponentModel.DataAnnotations;

namespace MembersBlazorNet6.Data.Models.Settings
{
    public class EmailPortsSetting
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Opis ustawień")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Email wychodzący")]
        [EmailAddress(ErrorMessage = "Wpisz prawidłowy adres email")]
        public string? EmailFrom { get; set; }

        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Nazwa serwera smtp")]
        public string? SmtpName { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Nr portu smtp")]
        public int SmtpPort { get; set; }
    }
}

