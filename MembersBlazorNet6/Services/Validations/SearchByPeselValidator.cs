using System;
using System.ComponentModel.DataAnnotations;
using MembersBlazorNet6.Services.SearchingServices.SearchingModels;

namespace MembersBlazorNet6.Services.Validations
{
    public class SearchByPeselValidator: ValidationAttribute
    {
        protected override ValidationResult? IsValid(
    object? value, ValidationContext validationContext)
        {
            var search = (SearchMemberByPesel)validationContext.ObjectInstance;

            if (search.Pesel == null)
            {
                return new ValidationResult("Pole 'pesel' jest wymagane");
            }


            if (!IsValidPESEL(search.Pesel))
            {
                return new ValidationResult("Wpisz poprawny numer pesel");
            }

            return ValidationResult.Success;
        }


        public bool IsValidPESEL(string pesel)
        {
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            bool result = false;

            if (pesel != null && pesel.Length == 11)
            {
                int controlSum = CalculateControlSum(pesel, weights);
                int controlNum = controlSum % 10;
                controlNum = 10 - controlNum;
                if (controlNum == 10)
                {
                    controlNum = 0;
                }
                int lastDigit = int.Parse(pesel[pesel.Length - 1].ToString());
                result = controlNum == lastDigit;
            }


            return result;
        }

        private static int CalculateControlSum(string pesel, int[] weights, int offset = 0)
        {
            int controlSum = 0;
            for (int i = 0; i < pesel.Length - 1; i++)
            {
                controlSum += weights[i + offset] * int.Parse(pesel[i].ToString());
            }
            return controlSum;
        }
    }
}

