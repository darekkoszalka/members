using System;
using System.ComponentModel.DataAnnotations;

namespace MembersBlazorNet6.Data.Models.Settings
{
    public class UnionStructureType
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Nazwa struktury")]
        public string? Name { get; set; }
    }
}

