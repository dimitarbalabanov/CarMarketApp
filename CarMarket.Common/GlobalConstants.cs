namespace CarMarket.Common
{
    using System.Collections.Generic;

    public static class GlobalConstants
    {
        public const string SystemName = "CarMarket";

        public const string AdministratorRoleName = "Administrator";

        public const string UserRoleName = "User";

        public const string AdministratorUsername = "admin";

        public const string AdministratorPassword = "adminPassword";

        public const string AdministratorEmail = "admin@admin.admin";

        public const string LogoCarImgUrl = "https://res.cloudinary.com/diyxxy7dq/image/upload/v1593707051/app-images/logo_car_gky8fn.png";

        public const string VwImgUrl = "https://res.cloudinary.com/diyxxy7dq/image/upload/v1593707064/app-images/vw_tzqbnn.png";

        public const string MercImgUrl = "https://res.cloudinary.com/diyxxy7dq/image/upload/v1593707064/app-images/merc_nv4odk.png";

        public const string AudiImgUrl = "https://res.cloudinary.com/diyxxy7dq/image/upload/v1593707064/app-images/audi_kzrpow.png";

        public const string BmwImgUrl = "https://res.cloudinary.com/diyxxy7dq/image/upload/v1593707064/app-images/bmw_wsbth6.png";

        public static Dictionary<string, string> VwParams => new Dictionary<string, string> { { "MakeId", "1" } };

        public static Dictionary<string, string> MercParams => new Dictionary<string, string> { { "MakeId", "15" } };

        public static Dictionary<string, string> AudiParams => new Dictionary<string, string> { { "MakeId", "14" } };

        public static Dictionary<string, string> BmwParams => new Dictionary<string, string> { { "MakeId", "13" } };
    }
}
