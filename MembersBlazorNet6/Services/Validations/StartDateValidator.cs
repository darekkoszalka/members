using System;
using System.ComponentModel.DataAnnotations;
using MembersBlazorNet6.Data.Models.MemberModels;

namespace MembersBlazorNet6.Services.Validations
{
    public class StartDateValidator: ValidationAttribute
    {
        protected override ValidationResult? IsValid(
       object? value, ValidationContext validationContext)
        {
            var member = (Member)validationContext.ObjectInstance;

            if (member.StartDate == null)
            {
                return new ValidationResult("Pole wymagane");
            }

            if (!startDateCorrect(member.StartDate))
            {
                return new ValidationResult("Data nie może być z przyszłości");
            }

            return ValidationResult.Success;
        }

        public bool startDateCorrect(DateTime? startDate)
        {
            bool result = false;
            if (startDate > DateTime.Now)
            {
                return result;
            }

            return true;
        }
    }
}

