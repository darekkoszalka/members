using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MembersBlazorNet6.Data.Models.UnionInstitutionModels;
using Microsoft.AspNetCore.Identity;

namespace MembersBlazorNet6.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Imię")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Nazwisko")]
        public string? LastName { get; set; }

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Pole wymagane")]
        [MinLength(9, ErrorMessage = "nr telefonu musi mieć 9 znaków")]
        public override string? PhoneNumber { get; set; }

        [Display(Name = "Data rejestracji")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "Ostatnie logowanie")]
        public DateTime? LastLoginDate { get; set; }

        //public virtual ICollection<UserAccess>? UserAccessesList { get; set; }
    }
}

