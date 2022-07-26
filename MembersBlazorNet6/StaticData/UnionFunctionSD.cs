using System;
namespace MembersBlazorNet6.StaticData
{
    
        public static class UnionFunctionSD
        {
            public static string President = "Prezes";

            public static string VicePresident = "Wiceprezes";

            public static string Secretary = "Sekretarz";

            public static string MemberOfTheBoard = "Członek";


            public static List<string> UnionFunctions = new List<string>()
        {
            President, VicePresident, Secretary, MemberOfTheBoard

        };

        }
    
}

