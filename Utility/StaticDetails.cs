namespace AuthExcelService.Utility
{
    public static class StaticDetails
    {
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public const string TokenCookie = "JWTToken";
        public static string SessionUserId = "SessionUserId";
        public static string SessionRoleAdmin = "Admin";
        public static string SessionRoleStaff = "Staff";
        public static string SessionRoleUser = "User";
        public static string SessionUserName = "UserName";
        public static string SessionUserEmail = "UserEmail";
        public static string SessionUserFullName = "UserFullName";

        public static string CountryIndia = "India";
        public static string CountryUS = "US";
        public static string CountryUK = "UK";

        public static string AuthAPIBase { get; set; } = null!;


        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
    }
}
