using System;
using System.Linq;
using Microsoft.Extensions.Configuration;


    public class Helpers{

        public static string connectionStringMaker()
        {
            string connectionURL = Environment.GetEnvironmentVariable("DATABASE_URL");

            connectionURL.Replace("//", "");

            char[] delimeterChars = {'/',':','@','?'};

            string[] strConn = connectionURL.Split(delimeterChars);
            strConn = strConn.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            string User = strConn[1];
            string Pass = strConn[2];
            string Server = strConn[3];
            string Database = strConn[5];
            string Port = strConn[4];

            string connectionString = $"host={Server};port={Port};database={Database};uid={User};pwd={Pass};sslmode=Require;Trust Server Certificate=true;Timeout=1000";
            return connectionString;
        }
    }
    