using System;
namespace MembersBlazorNet6.StaticData
{
    public class UnionStructureSD
    {
        //struktura
        public static string Management = "Zarząd";
        public static string Presidium = "Prezydium";
        public static string Secretariat = "Sekretariat";
        public static string RevisionCommittee = "Komisja Rewizyjna";

        public static List<string> Structures = new List<string>()
        {
            new string(Management),
            new string(Presidium),
            new string(Secretariat),
            new string(RevisionCommittee)
        };
    }
}

