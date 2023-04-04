using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

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

    class Program
    {
        static void Main(string[] args)
        {
            MsSqlDemo();
            Console.ReadKey();
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
