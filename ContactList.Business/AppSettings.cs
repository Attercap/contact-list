using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ContactList.Business
{
    /// <summary>
    /// Instantiates any settings used from appsettings.json, to be called on site/app start up
    /// </summary>
    public static class AppSettings
    {
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Connects to the appsettings.json to populate AppSetting properties
        /// </summary>
        public static void Initialize()
        {
            var configurationBuilder = new ConfigurationBuilder();

            try
            {
                configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            }
            catch(Exception ex)
            {
                ErrorLog.LogError(ex);
                return;
            }

            var configuration = configurationBuilder.Build();

            try
            {
                ConnectionString = configuration["ConnectionStrings:contactdatabase"];
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
        }
    }
}
