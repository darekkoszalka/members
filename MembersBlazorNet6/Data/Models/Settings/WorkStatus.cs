using System;
using System.ComponentModel.DataAnnotations;

namespace MembersBlazorNet6.Data.Models.Settings
{
    public class WorkStatus
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Status")]
        public string? Name { get; set; }
    }
}

