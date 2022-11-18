using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS
{
    public record struct Seller(int Id, string Name);
    public record struct Ware(int Id, string Name, int SellerId, float Price);
    public static class Global
    {
        public readonly static string DIR;
        readonly static SqlConnection CONN;

        public static (string Login, byte Type) CurrentUser { get; private set; }

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
            string cmdText = "SELECT pass, type FROM users WHERE login LIKE @login";
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

            err = String.Empty;
            CurrentUser = (login, (byte)reader["type"]);
            reader.Close();
            return true;
        }

        public static bool Register(string login, string pass, int type, out string err)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                err = "Login nie może być pusty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(pass))
            {
                err = "Hasło nie może być puste.";
                return false;
            }
            if (type > 2 || type < 0)
            {
                err = "Typ może przyjmować tylko wartości 0, 1 lub 2.";
                return false;
            }

            string cmdText = "SELECT type FROM users WHERE login LIKE @login";
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

            err = String.Empty;
            return true;
        }

        public static void Logout() => CurrentUser = ("", 0);

        public static bool SetPass(string newPass, out string err)
        {
            if (CurrentUser == ("", 0))
            {
                err = "Musisz być zalogowany aby użyć tej komendy.";
                return false;
            }

            string cmdText = "UPDATE users SET pass = @pass WHERE login LIKE @login";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@login", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@login"].Value = CurrentUser.Login;
            cmd.Parameters.Add("@pass", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@pass"].Value = newPass;

            err = String.Empty;
            return cmd.ExecuteNonQuery() == 1;
        }

        public static string GetPass(string login) 
        {
            string cmdText = "SELECT pass FROM users WHERE login LIKE @login";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@login", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@login"].Value = login;

            var reader = cmd.ExecuteReader();
            reader.Read();

            string pass = (string)reader["pass"];
            reader.Close();
            return pass;
        }

        public static bool HasUser(string login)
        {
            string cmdText = "SELECT login FROM users WHERE login LIKE @login";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@login", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@login"].Value = login;

            if (cmd.ExecuteScalar() is not null)
                return true;

            return false;
        }
        public static bool DeleteUser(string login) 
        {
            string cmdText = "DELETE FROM users WHERE login LIKE @login";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@login", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@login"].Value = login;

            return cmd.ExecuteNonQuery() == 1;
        }
        #endregion

        #region wares
        public static bool AddSeller(string name, out string err)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                err = "Nazwa nie może być pusta.";
                return false;
            }

            string cmdText = "SELECT name FROM sellers WHERE name LIKE @name";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@name"].Value = name;

            if (cmd.ExecuteScalar() is not null)
            {
                err = "Firma o podanej nazwie już istnieje.";
                return false;
            }

            string _cmdText = "INSERT INTO sellers (name) VALUES (@name)";
            SqlCommand _cmd = new(_cmdText, CONN);
            _cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 50);
            _cmd.Parameters["@name"].Value = name;
            _cmd.ExecuteNonQuery();

            err = String.Empty;
            return true;
        }

        public static Seller[] GetSellers()
        {
            List<Seller> sellers = new();
            string cmdText = "SELECT * FROM sellers";
            SqlCommand cmd = new(cmdText, CONN);

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //MessageBox.Show(reader[0] + " " + (string)reader[1]);
                sellers.Add(new((int)reader[0], (string)reader[1]));
            }
            reader.Close();

            return sellers.ToArray();
        }

        public static bool AddWare(string name, float price, int sellerId, out string err)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                err = "Nazwa nie może być pusta.";
                return false;
            }
            if (price < 0)
            {
                err = "Cena towaru nie może być ujemna.";
                return false;
            }

            string cmdText = "SELECT name FROM wares WHERE name LIKE @name AND seller_id = @seller_id";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@name"].Value = name;
            cmd.Parameters.Add("@seller_id", System.Data.SqlDbType.Int);
            cmd.Parameters["@seller_id"].Value = sellerId;

            if (cmd.ExecuteScalar() is not null)
            {
                err = "Firma już posiada towar o podanej nazwie.";
                return false;
            }

            string _cmdText = "SELECT id FROM sellers WHERE id = @seller_id";
            SqlCommand _cmd = new(_cmdText, CONN);
            _cmd.Parameters.Add("@seller_id", System.Data.SqlDbType.Int);
            _cmd.Parameters["@seller_id"].Value = sellerId;

            if (_cmd.ExecuteScalar() is null)
            {
                err = "Firma o podanym id nie istnieje.";
                return false;
            }

            string __cmdText = "INSERT INTO wares (name, seller_id, price) VALUES (@name, @seller_id, @price)";
            SqlCommand __cmd = new(__cmdText, CONN);
            __cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 50);
            __cmd.Parameters["@name"].Value = name;
            __cmd.Parameters.Add("@seller_id", System.Data.SqlDbType.Int);
            __cmd.Parameters["@seller_id"].Value = sellerId;
            __cmd.Parameters.Add("@price", System.Data.SqlDbType.Float);
            __cmd.Parameters["@price"].Value = price;
            __cmd.ExecuteNonQuery();

            err = String.Empty;
            return true;
        }

        public static Ware[] GetWaresFrom(int sellerId)
        {
            List<Ware> wares = new();
            string cmdText = "SELECT id, name, price FROM wares WHERE seller_id = @seller_id";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@seller_id", System.Data.SqlDbType.Int);
            cmd.Parameters["@seller_id"].Value = sellerId;
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                wares.Add(new((int)reader[0], (string)reader[1], sellerId, Convert.ToSingle(reader[2])));
            }
            reader.Close();

            return wares.ToArray();
        }
        #endregion
    }
}
