using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics;

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
        readonly static string CHROME = @"C\Program Files (x86)\Google\Chrome\Application\chrome.exe";

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
            if (GetWareAmount(wareId) > 0)
            {
                string cmdText = "UPDATE storage_unit SET amount = @amount WHERE goods_id = @ware_id";
                SqlCommand cmd = new(cmdText, CONN);

                cmd.Parameters.Add("@ware_id", System.Data.SqlDbType.Int);
                cmd.Parameters["@ware_id"].Value = wareId;
                cmd.Parameters.Add("@amount", System.Data.SqlDbType.Int);
                cmd.Parameters["@amount"].Value = amount;

                cmd.ExecuteNonQuery();
                //MessageBox.Show($"{String.Join(',',_storageUnit)} \nWareID {wareId}, Amount {amount}");

                _storageUnit[_storageUnit.ToList().FindIndex(x => x.Ware.Id == wareId)].Amount = amount;
                return;
            }
            string _cmdText = "INSERT INTO storage_unit VALUES (@ware_id, @amount)";
            SqlCommand _cmd = new(_cmdText, CONN);

            _cmd.Parameters.Add("@ware_id", System.Data.SqlDbType.Int);
            _cmd.Parameters["@ware_id"].Value = wareId;
            _cmd.Parameters.Add("@amount", System.Data.SqlDbType.Int);
            _cmd.Parameters["@amount"].Value = amount;

            _cmd.ExecuteNonQuery();
            //MessageBox.Show($"{String.Join(',',_storageUnit)} \nWareID {wareId}, Amount {amount}");

            SyncStorageUnit();
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
        #region pfds
        public static string HTML2PDF(string htmlPath, string filename, string dir)
        {
            string output = Path.Combine(dir, filename + ".pdf");
            using Process p = new();
            p.StartInfo.FileName = CHROME;
            p.StartInfo.Arguments = $"--headless --disable-gpu --print-to-pdf={output} {htmlPath}";
            p.Start();
            p.WaitForExit();
            return output;
        }
        public static void OpenWithChrome(string path)
        {
            using Process p = new();
            p.StartInfo.FileName = CHROME;
            p.StartInfo.Arguments = path;
            p.Start();
        }
        public static void GenerateInvoicePDF(Order order)
        {
            FolderBrowserDialog fbd = new();
            if (fbd.ShowDialog() != DialogResult.OK)
                return;
            string idStr = order.Id.ToString().PadLeft(5, '0');
            string filename = $"faktura{idStr}";
            string seller = GetSellerName(order.SellerId);
            double nettoSum = 0, vatSum = 0, bruttoSum = 0;
            string street = new string[]
            {
                "Słoneczna",
                "Wróblowa",
                "Dąbrowskiego",
                "Hutnicza",
                "Polna"
            }[order.SellerId % 5];
            int sellerSeed = seller.ToCharArray().Select(x => (int)x).Sum();
            int streetNr = sellerSeed % 71;
            string html = $@"<!DOCTYPE html>
            <html lang=""pl"">
            <head>
                <meta charset=""UTF-8"">
                <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>{idStr}</title>
                <style>
                    * {{
                        font-family: Arial, Helvetica, sans-serif;
                        font-weight: 500;
                        padding: 10px;
                        margin-bottom: 25px;
                    }}
                    .top {{
                        text-align: end;
                    }}
                    .top>img {{
                        float: left;
                        margin-top: -20px;
                    }}
                    .title {{
                        border-top: 3px solid black;
                        border-bottom: 3px solid black;
                        font-size: x-large;
                        font-weight: 700;
                        background-color: blanchedalmond;
                    }}
                    .info {{
                        width: fit-content;
                        max-width: 45%;
                        font-weight: 600;
                        float: left ;
                        margin-top: -40px;
                    }}
                    .info:last-child {{
                        float: right;
                    }}
                    .info>p {{
                        border-bottom: 3px solid black;
                        font-weight: 700;
                        font-size: large;
                    }}
                    .wares {{
                        border-collapse: collapse;
                        width: 100%;
                    }}
                    .wares th, .wares td {{
                        border: 2px solid black;
                        padding: 3px;
                    }}
                    .wares tr:nth-child(odd):not(:nth-child(1)) {{
                        background-color: aliceblue;
                    }}
                    .wares th {{
                        font-weight: bold;
                    }}
                    .wares:last-of-type tr:last-child td:first-child {{
                        text-align: end;
                    }}
                    .wares tr:first-child, .wares:last-child tr:last-child {{
                        background-color: blanchedalmond;
                    }}
                    .sum {{
                        margin-top: 175px;
                        background-color: blanchedalmond;
                        font-weight: bold;
                        text-decoration: underline;
                    }}
            </style>
            </head>
            <body>
                <div class=""top"">
                    <img src=""logo.png"" alt=""logo"">
                    <span>Data wystawienia faktury: {order.CreationTime.ToShortDateString()}</span><br/>
                    <span>Data sprzedaży: {order.CreationTime.ToShortDateString()}</span>
                </div>
                <div class=""title"">
                    Faktura nr: {idStr}
                </div>
                <div class=""info"">
                    <p>Sprzedawca</p>
                    {seller}<br/>
                    ul. {street} {streetNr}<br/>
                    {(sellerSeed % 100).ToString().PadLeft(2, '0')}-{(sellerSeed % 1000).ToString().PadLeft(3, '0')}<br/><br/>
                    <span>NIP:</span> {Math.Abs(sellerSeed * 97341276 % 10000000000).ToString().PadRight(10, '0')}<br/>
                    <span>e-mail:</span> {seller.ToLower()}-contact@gmail.com<br/><br/>
                    Nr konta: 00 1122 3344 5566 7788 9999 0000
                </div>
                <div class=""info"">
                    <p>Nabywca</p>
                    Brubelek sp. z o.o.<br/>
                    ul. Wróblowa 19/11<br/>
                    96-240<br/><br/>
                    <span>NIP:</span> 8564342193<br/>
                    <span>e-mail:</span> biblestudioes001@gmail.com<br/><br/>
                    Nr konta: 00 2211 4433 6655 8877 0000 9999
                </div>
                <table class=""wares"">
                    <tr>
                        <th>lp.</th>
                        <th>Nazwa towaru</th>
                        <th>Ilość</th>
                        <th>Jednostka</th>
                        <th>Cena netto</th>
                        <th>VAT</th>
                        <th>Kwota netto</th>
                        <th>Kwota VAT</th>
                        <th>Kwota brutto</th>
                    </tr>
                    {
                        string.Join(' ', 
                        order.Wares.ToList().Select((stack, i) =>
                        {
                            var nettoS = Math.Round(stack.Ware.Price / 1.23, 2);
                            var netto = Math.Round(nettoS * stack.Amount, 2);
                            var brutto = Math.Round(stack.Ware.Price * stack.Amount, 2);
                            var vat = Math.Round(brutto - netto, 2);

                            nettoSum += netto;
                            vatSum += vat;
                            bruttoSum += brutto;

                            return $@"
                            <tr>
                                <td>{i + 1}.</td> 
                                <td>{stack.Ware.Name}</td>
                                <td>{stack.Amount}</td>
                                <td>szt.</td>
                                <td>{nettoS}</td>
                                <td>23%</td>
                                <td>{netto}</td>
                                <td>{vat}</td>
                                <td>{brutto}</td>
                            </tr>";
                        }))
                    }
                </table>
                <table class=""wares"" style=""width: 40%; float: right"">
                    <tr>
                        <th>Stawka VAT</th>
                        <th>Netto</th>
                        <th>VAT</th>
                        <th>Brutto</th>
                    </tr>
                    <tr>
                        <td>23%</td>
                        <td>{Math.Round(nettoSum, 2)}</td>
                        <td>{Math.Round(vatSum, 2)}</td>
                        <td>{Math.Round(bruttoSum, 2)}</td>
                    </tr>
                    <tr>
                        <td>Razem</td>
                        <td>{Math.Round(nettoSum, 2)}</td>
                        <td>{Math.Round(vatSum, 2)}</td>
                        <td>{Math.Round(bruttoSum, 2)}</td>
                    </tr>
                </table>
                <h2 class=""sum"">Razem do zapłaty: {bruttoSum} zł</h2>
            </body>
            </html>";
            string path = Path.Combine(DIR, filename + ".html");
            File.WriteAllText(path, html);
            OpenWithChrome(HTML2PDF(path, filename, fbd.SelectedPath));
        }
        public static void GenerateCorrectionPDF(Correction corr)
        {
            FolderBrowserDialog fbd = new();
            if (fbd.ShowDialog() != DialogResult.OK)
                return;
            Order order = GetOrderById(corr.OrderId)[0];
            string idStr = order.Id.ToString().PadLeft(5, '0');
            string filename = $"korekta{idStr}";
            string seller = GetSellerName(order.SellerId);
            string street = new string[]
            {
                "Słoneczna",
                "Wróblowa",
                "Dąbrowskiego",
                "Hutnicza",
                "Polna"
            }[order.SellerId % 5];
            int sellerSeed = seller.ToCharArray().Select(x => (int)x).Sum();
            int streetNr = sellerSeed % 71;
            string html = $@"<!DOCTYPE html>
            <html lang=""pl"">
            <head>
                <meta charset=""UTF-8"">
                <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>{idStr}</title>
                <style>
                    * {{
                        font-family: Arial, Helvetica, sans-serif;
                        font-weight: 500;
                        padding: 10px;
                        margin-bottom: 25px;
                    }}
                    .top {{
                        text-align: end;
                    }}
                    .top>img {{
                        float: left;
                        margin-top: -20px;
                    }}
                    .title {{
                        border-top: 3px solid black;
                        border-bottom: 3px solid black;
                        font-size: x-large;
                        font-weight: 700;
                        background-color: blanchedalmond;
                    }}
                    .info {{
                        width: fit-content;
                        max-width: 45%;
                        font-weight: 600;
                        float: left ;
                        margin-top: -40px;
                    }}
                    .info:last-child {{
                        float: right;
                    }}
                    .info>p {{
                        border-bottom: 3px solid black;
                        font-weight: 700;
                        font-size: large;
                    }}
                    .wares {{
                        border-collapse: collapse;
                        width: 100%;
                    }}
                    .wares th, .wares td {{
                        border: 2px solid black;
                        padding: 3px;
                    }}
                    .wares tr:nth-child(odd):not(:nth-child(1)) {{
                        background-color: aliceblue;
                    }}
                    .wares th {{
                        font-weight: bold;
                    }}
                    .wares tr:first-child {{
                        background-color: blanchedalmond;
                    }}
            </style>
            </head>
            <body>
                <div class=""top"">
                    <img src=""logo.png"" alt=""logo"">
                    <span>Data wystawienia faktury: {order.CreationTime.ToShortDateString()}</span><br/>
                    <span>Data wystawienia korekty: {corr.CreationTime.ToShortDateString()}</span>
                </div>
                <div class=""title"">
                    Korekta do faktury nr: {idStr}
                </div>
                <div class=""info"">
                    <p>Sprzedawca</p>
                    {seller}<br/>
                    ul. {street} {streetNr}<br/>
                    {(sellerSeed % 100).ToString().PadLeft(2, '0')}-{(sellerSeed % 1000).ToString().PadLeft(3, '0')}<br/><br/>
                    <span>NIP:</span> {Math.Abs(sellerSeed * 97341276 % 10000000000).ToString().PadRight(10, '0')}<br/>
                    <span>e-mail:</span> {seller.ToLower()}-contact@gmail.com<br/><br/>
                    Nr konta: 00 1122 3344 5566 7788 9999 0000
                </div>
                <div class=""info"">
                    <p>Nabywca</p>
                    Brubelek sp. z o.o.<br/>
                    ul. Wróblowa 19/11<br/>
                    96-240<br/><br/>
                    <span>NIP:</span> 8564342193<br/>
                    <span>e-mail:</span> biblestudioes001@gmail.com<br/><br/>
                    Nr konta: 00 2211 4433 6655 8877 0000 9999
                </div>
                <table class=""wares"">
                    <tr>
                        <th>lp.</th>
                        <th>Nazwa towaru</th>
                        <th>Jednostka</th>
                        <th>Przewidywana ilość</th>
                        <th>Otrzymana ilość</th>
                        <th>Różnica</th>
                    </tr>
                    {
                        string.Join(' ',
                        corr.InexactWares.ToList().Select((stack, i) =>
                        {
                            var _stack = order.Wares.First(x => x.Ware.Id == stack.Ware.Id);
                            if (_stack.Amount == stack.Amount)
                                return "";
                            return $@"
                            <tr>
                                <td>{i + 1}.</td> 
                                <td>{stack.Ware.Name}</td>
                                <td>szt.</td>
                                <td>{_stack.Amount}</td>
                                <td>{stack.Amount}</td>
                                <td>{-(_stack.Amount - stack.Amount)}</td>
                            </tr>";
                        }))
                    }
                </table>
            </body>
            </html>";
            string path = Path.Combine(DIR, filename + ".html");
            File.WriteAllText(path, html);
            OpenWithChrome(HTML2PDF(path, filename, fbd.SelectedPath));
        }
        #endregion
    }
}