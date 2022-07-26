using System;
using System.ComponentModel.DataAnnotations;

namespace MembersBlazorNet6.Data.Models.Settings
{
    public class Education
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Wykształcenie")]
        public string? Name { get; set; }
    }
}

