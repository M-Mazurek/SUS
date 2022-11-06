using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS
{
    public static class Global
    {
        readonly static string DIR;
        readonly static SqlConnection CONN;

        static Global() 
        {
            DIR = Directory.GetParent(System.IO.Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName;
            string dbPath = Path.Combine(DIR, "Database.mdf");
            string connStr = @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};User ID=ni4;Password=g3r;Trusted_Connection=True;";

            CONN = new(connStr);
            CONN.Open();
        }

        public static bool Login(string login, string pass, out string err)
        {
            string cmdText = "SELECT pass FROM users WHERE login = @login";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@login", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@login"].Value = login;

            object res = cmd.ExecuteScalar();
            if (res is null)
            {
                err = "Podany użytkownik nie istnieje";
                return false;
            }
            if (res.ToString() != pass)
            {
                err = "Podano niepoprawne hasło";
                return false;
            }

            err = "Zalogowano pomyślnie";
            return true;
        }
    }
}
