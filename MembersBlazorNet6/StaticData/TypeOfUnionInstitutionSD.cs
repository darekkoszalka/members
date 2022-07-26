using System;
using System.Collections.Generic;

namespace MembersBlazorNet6.StaticData
{
    public static class TypeOfUnionInstitutionSD
    {
        public static string ZG = "Zarząd Główny";
        public static string District = "Okręg";
        public static string Branch = "Oddział";
        public static string Fire = "Ognisko";
        public static string SEiR = "SEiR";

        public static List<string> UnionInstitutionTypes = new List<string>()
        {
            ZG, District, Branch, Fire, SEiR
        };
    }
}
