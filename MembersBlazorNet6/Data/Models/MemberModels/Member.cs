using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MembersBlazorNet6.Data.Models.Settings;
using MembersBlazorNet6.Data.Models.UnionInstitutionModels;
using MembersBlazorNet6.Services.Validations;

namespace MembersBlazorNet6.Data.Models.MemberModels
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string? FirstName { get; set; }

        [Display(Name = "Drugie imię")]
        public string? SecondName { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string? LastName { get; set; }

        [NotMapped]
        [Display(Name = "Imię i nazwisko")]
        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }

        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(11, ErrorMessage ="Pesel powinien zawierać 11 cyfr.")]
        [PeselValidator(ErrorMessage = "Wpisz poprawny numer pesel")]
        public string? Pesel { get; set; }


        [Display(Name = "Data Przyjęcia")]
        [StartDateValidator(ErrorMessage ="Wpisz poprawną datę przyjęcia do ZNP.")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Wybierz datę przyjęcia do ZNP.")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Data wpisu")]
        public DateTime? RegisterDate { get; set; }

        [Display(Name = "Data aktualizacji")]
        public DateTime? UpdateDate { get; set; }

        [Display(Name = "Osoba aktualizująca")]
        public string? RegisterDateUserId { get; set; }

        
        [Display(Name = "Nr legitymacji")]
        public string? UnionCard { get; set; }

        [Display(Name = "Ognisko")]
        public int? UnionInstitutionId { get; set; }

        [ForeignKey("UnionInstitutionId")]
        public UnionInstitution? UnionInstitution { get; set; }

        [Display(Name = "Stanowisko pracy")]
        [Required(ErrorMessage = "Wybierz stanowisko pracy.")]
        public int? MemberWorkPlaceId { get; set; }

        [ForeignKey("MemberWorkPlaceId")]
        public WorkPlace? WorkPlace { get; set; }

        [Display(Name = "Wykształcenie")]
        [Required(ErrorMessage = "Dodaj wykształcenie.")]
        public int? EducationId { get; set; }

        [ForeignKey("EducationId")]
        public Education? Education { get; set; }


        public int getBirthYear(string pesel)
        {
            int num0 = (int)Char.GetNumericValue(pesel[0]);
            int num1 = (int)Char.GetNumericValue(pesel[1]);
            int num2 = (int)Char.GetNumericValue(pesel[2]);
            int num3 = (int)Char.GetNumericValue(pesel[3]);

            var year = 10 * num0;
            year += num1;
            var month = 10 * num2;
            month += num3;

            if (month > 80 && month < 93)
            {
                year += 1800;
            }
            else if (month > 0 && month < 13)
            {
                year += 1900;
            }
            else if (month > 20 && month < 33)
            {
                year += 2000;
            }
            else if (month > 40 && month < 53)
            {
                year += 2100;
            }
            else if (month > 60 && month < 73)
            {
                year += 2200;
            }

            return year;

        }

        public int getBirthMont(string pesel)
        {
            int num2 = (int)Char.GetNumericValue(pesel[2]);
            int num3 = (int)Char.GetNumericValue(pesel[3]);

            var month = 10 * num2;
            month += num3;
            if (month > 80 && month < 93)
            {
                month -= 80;
            }
            else if (month > 20 && month < 33)
            {
                month -= 20;
            }
            else if (month > 40 && month < 53)
            {
                month -= 40;
            }
            else if (month > 60 && month < 73)
            {
                month -= 60;
            }

            var result = month;

            return result;
        }

        public int getBirthDay(string pesel)
        {
            int num4 = (int)Char.GetNumericValue(pesel[4]);
            int num5 = (int)Char.GetNumericValue(pesel[5]);
            var day = 10 * num4;
            day += num5;
            return day;
        }

        public string getGender(string pesel)
        {
            if (pesel[9] % 2 == 1)
            {
                return "Mężczyzna";
            }
            else
            {
                return "Kobieta";
            }
        }

        public DateTime dateOfBirth(string pesel)
        {


            var year = getBirthYear(pesel);
            var month = getBirthMont(pesel);
            var day = getBirthDay(pesel);



            var dateBirth = new DateTime(year, month, day);

            return dateBirth;

        }

        public int getAge(string pesel)
        {
            var today = DateTime.Today;

            var age = today.Year - dateOfBirth(pesel).Year;

            return age;

        }
    }
}

