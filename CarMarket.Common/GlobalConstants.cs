using System.Collections.Generic;

namespace CarMarket.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "CarMarket";

        public const string AdministratorRoleName = "Administrator";

        public const string UserRoleName = "User";

        public const string AdministratorUsername = "admin";

        public const string AdministratorPassword = "adminPassword";

        public const string AdministratorEmail = "admin@admin.admin";

        public static Dictionary<string, string> VwParams => new Dictionary<string, string> { { "MakeId", "1" } };

        public static Dictionary<string, string> MercParams => new Dictionary<string, string> { { "MakeId", "2" } };

        public static Dictionary<string, string> AudiParams => new Dictionary<string, string> { { "MakeId", "3" } };

        public static Dictionary<string, string> BmwParams => new Dictionary<string, string> { { "MakeId", "4" } };
    }
}
