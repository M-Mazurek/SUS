using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SUS
{
    public enum ORDER_STATUS
    {
        PENDING = 1,
        CONFIRMED = 2
    }

    public record struct Seller(int Id, string Name);
    public record struct Ware(int Id, string Name, int SellerId, float Price);
    public record struct Order(int Id, int SellerId, DateTime CreationTime, ORDER_STATUS Status, WareStack[] Wares);
    public record struct WareStack(Ware Ware, int Amount);
    public record struct Correction(int Id, int OrderId, DateTime CreationTime, WareStack[] InexactWares);
    public record struct HistoryEvent(string Text, DateTime EventTime);
    public static class Global
    {
        private static WareStack[] _storageUnit = Array.Empty<WareStack>();
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
            SyncStorageUnit();
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
            NewEvent($"Zarejstrowano użytkownika: {login}");
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
            NewEvent($"Dodano sprzedawcę: {name}");
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

        public static string GetSellerName(int id)
        {
            string cmdText = "SELECT name FROM sellers WHERE id = @id";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int);
            cmd.Parameters["@id"].Value = id;
            var reader = cmd.ExecuteReader();

            string res = reader.Read() ? (string)reader[0] : String.Empty;
            reader.Close();
            return res;
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
            NewEvent($"Dodano towar: {name}");
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

        public static Ware[] GetWaresByName(string name)
        {
            List<Ware> wares = new();
            string cmdText = "SELECT * FROM wares WHERE name LIKE '%' + @name + '%'";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 50);
            cmd.Parameters["@name"].Value = name;
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                wares.Add(new((int)reader["id"], (string)reader["name"], (int)reader["seller_id"], Convert.ToSingle(reader["price"])));
            }
            reader.Close();

            return wares.ToArray();
        }

        public static bool GetWareById(int id, out Ware ware)
        {
            string cmdText = "SELECT * FROM wares WHERE id = @id";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int);
            cmd.Parameters["@id"].Value = id;
            //MessageBox.Show("e spisz? v0");
            var reader = cmd.ExecuteReader();
            //MessageBox.Show("e spisz? v1");
            if (reader.Read())
            {
                //MessageBox.Show("e spisz? v2");
                ware = new((int)reader["id"], (string)reader["name"], (int)reader["seller_id"], Convert.ToSingle(reader["price"]));
                reader.Close();
                //MessageBox.Show($"WARE: {ware}");
                return true;
            }
            //MessageBox.Show("e spisz? v3a");
            ware = new();
            reader.Close();
            return false;
        }
        #endregion
        #region orders
        public static bool ParseWareStacks(string str, out WareStack[] stacks)
        {
            //MessageBox.Show($"PARSE WARE STACKS str: {str}");
            try
            {
                stacks = str.Split(' ').Select(x =>
                {
                    string[] strs = x.Split('_');
                    //strs.ToList().ForEach(e => MessageBox.Show($"PARSE WARE STACKS strs: {e}"));
                    //MessageBox.Show($"PARSE WARE STACKS GetWareById(): {GetWareById(int.Parse(strs[0]), out Ware ware)}"); // nie zwraca niczego ?
                    GetWareById(int.Parse(strs[0]), out Ware ware);
                    return new WareStack(ware, int.Parse(strs[1]));
                }).ToArray();
            }
            catch
            {
                stacks = Array.Empty<WareStack>();
                return false;
            }
            return true;
        }

        public static string ToDBString(this WareStack[] stacks)
        {
            return String.Join(' ', stacks.Select(x => $"{x.Ware.Id}_{x.Amount}"));
        }

        public static bool NewOrder(int sellerId, ORDER_STATUS status, WareStack[] stacks, out string err)
        {
            if (GetSellerName(sellerId) == String.Empty)
            {
                err = "Sprzedawca o danym id nie istnieje.";
                return false;
            }
            err = String.Empty;
            string cmdText = "INSERT INTO orders (seller_id, creation_time, status, wares) VALUES (@seller_id, @creation_time, @status, @wares)";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@seller_id", System.Data.SqlDbType.Int);
            cmd.Parameters["@seller_id"].Value = sellerId;
            cmd.Parameters.Add("@creation_time", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@creation_time"].Value = DateTime.Now;
            cmd.Parameters.Add("@status", System.Data.SqlDbType.TinyInt);
            cmd.Parameters["@status"].Value = (byte)status;
            cmd.Parameters.Add("@wares", System.Data.SqlDbType.VarChar, 255);
            cmd.Parameters["@wares"].Value = stacks.ToDBString();

            cmd.ExecuteNonQuery();
            NewEvent($"Dodano zamówienie.");
            return true;
        }

        public static Order[] GetOrderById(int orderId)
        {
            List<Order> orders = new();
            string cmdText = "SELECT * FROM orders WHERE id = @id";
           
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int);
            cmd.Parameters["@id"].Value = orderId;
            
            var reader = cmd.ExecuteReader();
            List<string> stacksTemp = new();
            while (reader.Read())
            {
                //MessageBox.Show($"GLOBAL PARSE: {ParseWareStacks((string)reader["wares"], out var stacks)}");
                orders.Add(new((int)reader["id"],
                                    (int)reader["seller_id"],
                                    (DateTime)reader["creation_time"],
                                    (ORDER_STATUS)(byte)reader["status"],
                                    Array.Empty<WareStack>()));
                stacksTemp.Add((string)reader["wares"]);
                //MessageBox.Show($"GLOBAL MESSAGE: {stacks}");
            }
            reader.Close();
            for (int i = 0; i < orders.Count; i++)
            {
                ParseWareStacks(stacksTemp[i], out var stacks);
                var retOrder = orders[i];
                retOrder.Wares = stacks;
                orders[i] = retOrder;
            }

            return orders.ToArray();
        }

        public static Order[] GetOrders(int sellerId = -1, DateTime? dateFrom = null, DateTime? dateTo = null, ORDER_STATUS status = ORDER_STATUS.PENDING | ORDER_STATUS.CONFIRMED)
        {
            List<Order> orders = new();
            string cmdText = "SELECT * FROM orders WHERE creation_time >= @date_from AND creation_time <= @date_to AND (status & @status) > 0";
            if (sellerId > -1)
                cmdText += " AND seller_id = @seller_id";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@date_from", System.Data.SqlDbType.DateTime);
                cmd.Parameters["@date_from"].Value = dateFrom is null ? DateTime.Parse("2010-01-01T00:00:00.000") : dateFrom;
            cmd.Parameters.Add("@date_to", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@date_to"].Value = dateTo is null ? DateTime.Parse("2050-01-01T00:00:00.000") : dateTo;
            cmd.Parameters.Add("@status", System.Data.SqlDbType.TinyInt);
            cmd.Parameters["@status"].Value = (byte)status;
            if (sellerId > -1)
            {
                cmd.Parameters.Add("@seller_id", System.Data.SqlDbType.Int);
                cmd.Parameters["@seller_id"].Value = sellerId;
            }
            var reader = cmd.ExecuteReader();
            List<string> stacksTemp = new();
            while (reader.Read())
            {
                //MessageBox.Show($"GLOBAL PARSE: {ParseWareStacks((string)reader["wares"], out var stacks)}");
                orders.Add(new((int)reader["id"], 
                                    (int)reader["seller_id"],
                                    (DateTime)reader["creation_time"], 
                                    (ORDER_STATUS)(byte)reader["status"],
                                    Array.Empty<WareStack>()));
                stacksTemp.Add((string)reader["wares"]);
                //MessageBox.Show($"GLOBAL MESSAGE: {stacks}");
            }
            reader.Close();
            for (int i = 0; i < orders.Count; i++)
            {
                ParseWareStacks(stacksTemp[i], out var stacks);
                var retOrder = orders[i];
                retOrder.Wares = stacks;
                orders[i] = retOrder;
            }

            return orders.ToArray();
        }

        public static void ConfirmOrder(Order order)
        {
            string cmdtext = "UPDATE orders SET status = 2 WHERE id = @id";
            SqlCommand cmd = new(cmdtext, CONN);
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int);
            cmd.Parameters["@id"].Value = order.Id;

            cmd.ExecuteNonQuery();

            order.Wares.ToList().ForEach(x =>
            {
                ChangeWareAmount(GetWareAmount(x.Ware.Id) + x.Amount, x.Ware.Id);
            });
            NewEvent($"Potwierdzono zamówienie nr. {order.Id.ToString().PadLeft(5, '0')}");
        }
        #endregion
        #region corrections
        public static bool NewCorrection(int orderId, WareStack[] stacks, out string err)
        {
            err = String.Empty;
            string cmdText = "INSERT INTO corrections (order_id, creation_time, inexact_goods) VALUES (@order_id, @creation_time, @inexact_goods)";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@order_id", System.Data.SqlDbType.Int);
            cmd.Parameters["@order_id"].Value = orderId;
            cmd.Parameters.Add("@creation_time", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@creation_time"].Value = DateTime.Now;
            cmd.Parameters.Add("@inexact_goods", System.Data.SqlDbType.VarChar, 255);
            cmd.Parameters["@inexact_goods"].Value = stacks.ToDBString();

            cmd.ExecuteNonQuery();
            NewEvent($"Dodano korektę do zamówienia nr. {orderId.ToString().PadLeft(5, '0')}");
            return true;
        }

        public static Correction[] GetCorrections(DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            List<Correction> corrs = new();
            string cmdText = "SELECT * FROM corrections WHERE creation_time >= @date_from AND creation_time <= @date_to";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@date_from", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@date_from"].Value = dateFrom is null ? DateTime.Parse("2010-01-01T00:00:00.000") : dateFrom;
            cmd.Parameters.Add("@date_to", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@date_to"].Value = dateTo is null ? DateTime.Parse("2050-01-01T00:00:00.000") : dateTo;

            var reader = cmd.ExecuteReader();
            List<string> stacksTemp = new();
            while (reader.Read())
            {
                corrs.Add(new((int)reader["id"],
                                    (int)reader["order_id"],
                                    (DateTime)reader["creation_time"],
                                    Array.Empty<WareStack>()));
                stacksTemp.Add((string)reader["inexact_goods"]);
            }
            reader.Close();
            for (int i = 0; i < corrs.Count; i++)
            {
                ParseWareStacks(stacksTemp[i], out var stacks);
                var retOrder = corrs[i];
                retOrder.InexactWares = stacks;
                corrs[i] = retOrder;
            }

            return corrs.ToArray();
        }
        #endregion
        #region storage_unit
        private static void SyncStorageUnit()
        {
            string cmdText = "SELECT * FROM storage_unit";
            SqlCommand cmd = new(cmdText, CONN);

            var reader = cmd.ExecuteReader();
            List<(int WareId, int Amount)> waresTemp = new();
            while (reader.Read())
            {
                waresTemp.Add(((int)reader[0], (int)reader[1]));
            }
            reader.Close();

            _storageUnit = waresTemp.Select(x =>
            {
                GetWareById(x.WareId, out var ware);
                return new WareStack(ware, x.Amount);
            }).ToArray();
        }

        public static void ChangeWareAmount(int amount, int wareId)
        {
            string cmdText = "UPDATE storage_unit SET amount = @amount WHERE goods_id = @ware_id";
            SqlCommand cmd = new(cmdText, CONN);

            cmd.Parameters.Add("@ware_id", System.Data.SqlDbType.Int);
            cmd.Parameters["@ware_id"].Value = wareId;
            cmd.Parameters.Add("@amount", System.Data.SqlDbType.Int);
            cmd.Parameters["@amount"].Value = amount;

            cmd.ExecuteNonQuery();
            _storageUnit[_storageUnit.ToList().FindIndex(x => x.Ware.Id == wareId)].Amount = amount;
        }

        public static int GetWareAmount(int wareId)
        {
            return _storageUnit.ToList().Find(x => x.Ware.Id == wareId).Amount;
        }
        #endregion
        #region history
        private static void NewEvent(string text)
        {
            string cmdText = "INSERT INTO history VALUES (@text, @event_time)";
            SqlCommand cmd = new(cmdText, CONN);
            cmd.Parameters.Add("@text", System.Data.SqlDbType.NVarChar, 255);
            cmd.Parameters["@text"].Value = text;
            cmd.Parameters.Add("@event_time", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@event_time"].Value = DateTime.Now;

            cmd.ExecuteNonQuery();
        }
        public static HistoryEvent[] GetEvents()
        {
            List<HistoryEvent> events = new();
            string cmdText = "SELECT * FROM history";
            SqlCommand cmd = new(cmdText, CONN);
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                events.Add(new((string)reader[0], (DateTime)reader[1]));
            }
            reader.Close();
            return events.OrderByDescending(x => x.EventTime.Ticks).ToArray();
        }
        #endregion
    }
}