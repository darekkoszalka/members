using System;
namespace MembersBlazorNet6.StaticData
{
    public static class MemberWorkPlaceSD
    {
        public static string Teacher = "Nauczyciel";
        public static string AdministrationAndService = "Administracja i obsługa";
        public static string Other = "Inni";
        public static string Pensioner = "Emeryt - rencista";

        public static List<string> WorkPlaces = new List<string>()
        { Teacher, AdministrationAndService, Pensioner, Other};
    }
}

