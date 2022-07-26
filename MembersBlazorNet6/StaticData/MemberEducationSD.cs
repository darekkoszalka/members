using System;
namespace MembersBlazorNet6.StaticData
{
    public class MemberEducationSD
    {
        public static string Master = "Wyższe z tytułem magistra";
        public static string HigherVocational = "Wyższe zawodowe z tytułem licencjata lub inżyniera";
        public static string Middle = "Średnie";
        public static string TechnicalSecondary = "Średnie z tytułem technika";
        public static string Professional = "Zasadnicze zawodowe";
        public static string Basic = "Podstawowe";

        public static List<string> Educations = new List<string>()
        { Master, HigherVocational, Middle, TechnicalSecondary, Professional, Basic};
    }
}

