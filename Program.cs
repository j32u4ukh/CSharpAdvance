using MySqlConnector;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CSharpAdvance
{
    public class MsSql : IDisposable
    {
        private SqlConnection connection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server">DESKTOP-P4UN171\MSSQLSERVER_2022</param>
        /// <param name="database">demo</param>
        /// <param name="user">sa</param>
        /// <param name="password"></param>
        public MsSql(string server, string database, string user, string password)
        {
            string connectionString = $"Data Source={server};Initial Catalog={database};User ID={user};Password={password}";
            Console.WriteLine($"connectionString: {connectionString}");
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void Dispose()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                Console.WriteLine("Close connection...");
            }
        }

        public int Insert(int id, string name)
        {
            string insertSql = "INSERT INTO Person (id, name) VALUES (@id, @name)";
            SqlCommand command = new SqlCommand(insertSql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            return command.ExecuteNonQuery();
        }

        public void Select(Action<DataTable> func)
        {
            string selectQuery = "SELECT * FROM Person";
            SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (func != null)
            {
                func(dataTable);
            }

            //foreach (DataRow row in dataTable.Rows)
            //{
            //    // 讀取資料行的值
            //    var id = Convert.ToInt32(row["id"]);
            //    string name = row["name"].ToString();

            //    //在這裡對資料進行處理
            //    Console.WriteLine($"id: {id}, name: {name}");
            //    //Console.WriteLine($"row: {row}");
            //}
        }

        public int Update(int id, string name)
        {
            string updateQuery = "UPDATE Person SET name = @name WHERE id = @id";
            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
            updateCommand.Parameters.AddWithValue("@name", name);
            updateCommand.Parameters.AddWithValue("@id", id);
            return updateCommand.ExecuteNonQuery();
        }

        public int Delete(int id)
        {
            string deleteQuery = "DELETE FROM Person WHERE id = @id";
            SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
            deleteCommand.Parameters.AddWithValue("@id", id);
            return deleteCommand.ExecuteNonQuery();
        }
    }

    public class MySql : IDisposable
    {
        private MySqlConnection connection;

        public MySql(string server, int port, string user, string password)
        {
            string connectionString = "server=localhost;port=3306;database=demo;uid=root;password=Birth=821018;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        public void Dispose()
        {
            if(connection.State == ConnectionState.Open)
            {
                connection.Close();
                Console.WriteLine("Close connection...");
            }
        }

        public void Insert(int id, string name)
        {
            string sql = $"INSERT INTO person (id, name) VALUES ({id}, '{name}')";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public void Select()
        {
            string sql = "SELECT * FROM person WHERE id < @id";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", 5);

            //執行SELECT語句，並讀取結果
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32("id");
                string name = reader.GetString("name");

                Console.WriteLine("id:{0}, name:{1}", id, name);
            }
            reader.Close();
        }

        public void Update(int id, string name)
        {
            string sql = "UPDATE person SET name=@name WHERE id=@id";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@id", id);

            //執行UPDATE語句，並獲取受影響的行數
            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("受影響的行數：{0}", rowsAffected);
        }

        public void Delete(int id)
        {
            //準備DELETE語句
            string sql = "DELETE FROM person WHERE id=@id";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);

            //執行DELETE語句，並獲取受影響的行數
            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("受影響的行數：{0}", rowsAffected);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MySqlDemo();
            //MsSqlDemo();
            Console.ReadKey();
        }

        static void MySqlDemo()
        {
            using (MySql ms = new MySql(server: "localhost", port: 3306, user: "root", password: "password"))
            {
                Console.WriteLine("===== Insert =====");
                ms.Insert(3, "Kintama");
                ms.Select();

                Console.WriteLine("===== Update =====");
                ms.Update(3, "Ginntama");
                ms.Select();

                Console.WriteLine("===== Delete =====");
                ms.Delete(3);
                ms.Select();
            }
        }

        static void MsSqlDemo()
        {
            using (MsSql ms = new MsSql(server: @"DESKTOP-P4UN171\MSSQLSERVER_2022", database: "demo", user: "sa", password: "password"))
            {
                Console.WriteLine("===== Insert =====");
                ms.Insert(3, "Kintama");
                ms.Select((DataTable dataTable) =>
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // 讀取資料行的值
                        var id = Convert.ToInt32(row["id"]);
                        string name = row["name"].ToString();

                        //在這裡對資料進行處理
                        Console.WriteLine($"id: {id}, name: {name}");
                    }
                });

                Console.WriteLine("===== Update =====");
                ms.Update(3, "Ginntama");
                ms.Select((DataTable dataTable) =>
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // 讀取資料行的值
                        var id = Convert.ToInt32(row["id"]);
                        string name = row["name"].ToString();

                        //在這裡對資料進行處理
                        Console.WriteLine($"id: {id}, name: {name}");
                    }
                });

                Console.WriteLine("===== Delete =====");
                ms.Delete(3);
                ms.Select((DataTable dataTable) =>
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // 讀取資料行的值
                        var id = Convert.ToInt32(row["id"]);
                        string name = row["name"].ToString();

                        //在這裡對資料進行處理
                        Console.WriteLine($"id: {id}, name: {name}");
                    }
                });
            };           
        }
    }
}
