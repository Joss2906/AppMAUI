using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Divisas.Utilities
{
    public static class DBConnection
    {
        public static string ReturnRoute(string dbName)
        {
            string dbRoute = string.Empty;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                dbRoute = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                dbRoute = Path.Combine(dbRoute, dbName);
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                dbRoute = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dbRoute = Path.Combine(dbRoute, "..", "Library", dbName);
            }

            return dbRoute;
        }

    }
}
