namespace SpaData
{
#if DEBUG
    public static class Constant
    {
        #region Spa Api

        public const string SpaApiHomeUriHttps = "https://localhost:44315/";
        public const string SpaApiHomeUriHttp = "http://localhost:44315/";
        public const string SpaApiScope = "spaApi";
        #endregion

        #region SpaApp

        public const string SpaAppHomeUriHttp = "http://localhost:57717/";
        //public const string SpaAppHomeUriHttps = "https://localhost:44335/";
        public const string SpaAppLogoutRedirectUri = "http://localhost:57717/logout";
        public const string SpaAngularClientName = "angular2client";
        public const string SpaAngularClientId = "angular2client";

        #endregion

        #region Swagger

        public const string SwaggerClientName = "swaggerclient";
        public const string SwaggerClientId = "swaggerclient";
        public const string SwaggerEndpoint = "/swagger/v1/swagger.json";
        public const string SwaggerDescription = "SpaApi";
        public const string SwaggerRedirectUriHttps = "https://localhost:44315/swagger/o2c.html";
        public const string SwaggerRedirectUriHttp = "http://localhost:44315/swagger/o2c.html";
        public const string SwaggerHomeUriHttp = "http://localhost:44315/swagger";
        public const string SwaggerHomeUriHttps = "https://localhost:44315/swagger";
        public const string DeveloperEmailSwagger = "nishant.h@mindfiresolutions.com";
        public const string DeveloperNameSwagger = "Nishant";
        public const string TitleSwagger = "SpaApi Docs";
        public const string VersionSwagger = "v1";

        #endregion

        #region Postman

        public const string PostmanClientName = "postman";
        public const string PostmanClientId = "postman";
        public const string PostmanRedirectUri = "https://www.getpostman.com/oauth2/callback";

        #endregion

        #region AuthServer

        public const string AuthorizeEndpointHttps = "https://localhost:44313/connect/authorize";
        public const string TokenEndpointHttps = "https://localhost:44313/connect/token";
        public const string AuthAuthorityUriHttps = "https://localhost:44313/";
        public const string AdminRoleString = "spa.admin";
        public const string UserRoleString = "spa.user";
        #endregion
    }
#else
    public static class Constant
    {
    #region Spa Api

        public const string SpaApiHomeUriHttps = "https://192.168.11.128:44315/";
        public const string SpaApiHomeUriHttp = "http://192.168.11.128:44315/";
        public const string SpaApiScope = "spaApi";

    #endregion

    #region SpaApp

        public const string SpaAppHomeUriHttp = "http://192.168.11.128:57717/";
        public const string SpaAppHomeUriHttps = "https://192.168.11.128:57717/";
        public const string SpaAppLogoutRedirectUri = SpaAppHomeUriHttp + "logout";
        public const string SpaAngularClientName = "angular2client";
        public const string SpaAngularClientId = "angular2client";

    #endregion

    #region Swagger

        public const string SwaggerClientName = "swaggerclient";
        public const string SwaggerClientId = "swaggerclient";
        public const string SwaggerEndpoint = "/swagger/v1/swagger.json";
        public const string SwaggerDescription = "SpaApi";
        public const string SwaggerRedirectUriHttps = SpaApiHomeUriHttps + "swagger/o2c.html";
        public const string SwaggerRedirectUriHttp = SpaApiHomeUriHttp + "swagger/o2c.html";
        public const string SwaggerHomeUriHttp = SpaApiHomeUriHttp + "swagger";
        public const string SwaggerHomeUriHttps = SpaApiHomeUriHttps + "swagger";
        public const string DeveloperEmailSwagger = "nishant.h@mindfiresolutions.com";
        public const string DeveloperNameSwagger = "Nishant";
        public const string TitleSwagger = "SpaApi Docs";
        public const string VersionSwagger = "v1";

    #endregion

    #region Postman

        public const string PostmanClientName = "postman";
        public const string PostmanClientId = "postman";
        public const string PostmanRedirectUri = "https://www.getpostman.com/oauth2/callback";

    #endregion

    #region AuthServer

        public const string AuthorizeEndpointHttps = AuthAuthorityUriHttps + "connect/authorize";
        public const string TokenEndpointHttps = AuthAuthorityUriHttps + "connect/token";
        public const string AuthAuthorityUriHttps = "https://192.168.11.128:44313/";
        public const string AdminRoleString = "spa.admin";
        public const string UserRoleString = "spa.user";

    #endregion
    }
#endif
}
