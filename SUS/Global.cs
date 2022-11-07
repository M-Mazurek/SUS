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


        private static (string l, byte t) _cu = ("", 0);
        public static (string Login, byte Type) CurrentUser
        {
            get
            {
                return _cu;
            }
            set
            {
                if (_cu is ("", 0))
                    return;
                _cu = value;
            }
        }

        public static void Init() { /* nie zadajemy pytań ok? */ }

        static Global() 
        {
            DIR = Directory.GetParent(System.IO.Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName;
            string dbPath = Path.Combine(DIR, "Database.mdf");
            string connStr = @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};User ID=ni4;Password=g3r;Trusted_Connection=True;";

            CONN = new(connStr);
            CONN.Open();
        }

        #region accounts
        public static bool Login(string login, string pass, out string err)
        {
            string cmdText = "SELECT pass, type FROM users WHERE login = @login";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@login", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@login"].Value = login;

            var reader = cmd.ExecuteReader();
            
            if (!reader.Read())
            {
                err = "Podany użytkownik nie istnieje.";
                reader.Close();
                return false;
            }
            if (reader["pass"].ToString() != pass)
            {
                err = "Podano niepoprawne hasło.";
                reader.Close();
                return false;
            }

            err = "";
            CurrentUser = (login, (byte)reader["type"]);

            reader.Close();
            return true;
        }

        public static bool Register(string login, string pass, int type, out string err)
        {
            string cmdText = "SELECT type FROM users WHERE login = @login";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@login", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@login"].Value = login;

            if (cmd.ExecuteScalar() is not null)
            {
                err = "Użytkowik o podanym loginie już istnieje.";
                return false;
            }

            string _cmdText = "INSERT INTO users VALUES (@login, @pass, @type)";
            SqlCommand _cmd = new(_cmdText, CONN);
            _cmd.Parameters.Add("@login", System.Data.SqlDbType.NVarChar, 50);
            _cmd.Parameters["@login"].Value = login;
            _cmd.Parameters.Add("@pass", System.Data.SqlDbType.NVarChar, 50);
            _cmd.Parameters["@pass"].Value = pass;
            _cmd.Parameters.Add("@type", System.Data.SqlDbType.TinyInt);
            _cmd.Parameters["@type"].Value = type;

            _cmd.ExecuteNonQuery();

            err = "Zarejestrowano pomyślnie.";
            return true;
        }

        public static void Logout() => _cu = ("", 0);

        public static bool SetPass(string newPass)
        {
            try
            {
                string cmdText = "UPDATE users SET pass = @pass WHERE login = @login";
                SqlCommand cmd = new(cmdText, CONN);
                cmd.Parameters.Add("@login", System.Data.SqlDbType.NVarChar, 50);
                cmd.Parameters["@login"].Value = CurrentUser.Login;
                cmd.Parameters.Add("@pass", System.Data.SqlDbType.NVarChar, 50);
                cmd.Parameters["@pass"].Value = newPass;

                cmd.ExecuteNonQuery();
            }
            catch { return false; }
            return true;
        }
        #endregion
    }
}
