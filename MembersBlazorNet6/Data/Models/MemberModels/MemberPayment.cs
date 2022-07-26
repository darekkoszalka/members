using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MembersBlazorNet6.Data.Models.MemberModels
{
    public class MemberPayment
    {
        [Key]
        public int Id { get; set; }

        public int? MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Member? Member { get; set; }

        [Required(ErrorMessage = "Podaj kwotę")]
        public double? Payment { get; set; }

        [Required]
        public DateTime? PaymentDate { get; set; }
    }
}

