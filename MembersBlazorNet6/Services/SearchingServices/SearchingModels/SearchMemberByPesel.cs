using System;
using System.ComponentModel.DataAnnotations;
using MembersBlazorNet6.Services.Validations;

namespace MembersBlazorNet6.Services.SearchingServices.SearchingModels
{
    public class SearchMemberByPesel
    {
        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(11, ErrorMessage = "Pesel powinien zawierać 11 cyfr.")]
        [SearchByPeselValidator(ErrorMessage = "Wpisz poprawny numer pesel")]
        public string? Pesel { get; set; }
    }
}

