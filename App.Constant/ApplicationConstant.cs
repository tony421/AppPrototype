using System;
using System.Collections.Generic;
using System.Text;

namespace App.Constant
{
    public class ApplicationConstant
    {
        public const string APP_SETTING_PATH = "/../App.API/appsettings.json";
        public const string APP_SETTING_PATH_WITH_ENV = "/../App.API/appsettings.{0}.json";
        public class DatabaseContext
        {
            public const string MASTER_DATABASE = "AppPrototype_Master";
            public const string PRODUCTION_MASTER_DATABASE = "AppPrototype_Production_Master";
            public const string MASTER_CONNECTION_STRING = "master";
            public const string PRODUCTION_MASTER_CONNECTION_STRING = "production_master";
            public const string PRODUCTION_FORMAT_CONNECTION_STRING = "production_format";
        }

        public class EnvironmentVariableNames
        {
            public const string ASPNETCORE_ENV = "ASPNETCORE_ENVIRONMENT";
        }
    }
}
