using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CSharpAdvance
{
    class Program
    {
        static SqlConnection connection;

        static void Main(string[] args)
        {
            string connectionString = @"Data Source=DESKTOP-P4UN171\MSSQLSERVER_2022;Initial Catalog=demo;User ID=sa;Password=au4a83";
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Select();
            }
            Console.ReadKey();
        }

        static void Insert()
        {
            string insertSql = "INSERT INTO Person (id, name) VALUES (@id, @name)";
            SqlCommand command = new SqlCommand(insertSql, connection);
            command.Parameters.AddWithValue("@id", 2);
            command.Parameters.AddWithValue("@name", "Value2");
            command.ExecuteNonQuery();
        }

        static void Select()
        {
            string selectQuery = "SELECT * FROM Person";
            SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                // 讀取資料行的值
                var id = Convert.ToInt32(row["id"]);
                string name = row["name"].ToString();

                //在這裡對資料進行處理
                Console.WriteLine($"id: {id}, name: {name}");
                //Console.WriteLine($"row: {row}");
            }

        }

        static void Update()
        {
            string updateQuery = "UPDATE Person SET name = @name WHERE id = @id";
            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
            updateCommand.Parameters.AddWithValue("@name", "Name2");
            updateCommand.Parameters.AddWithValue("@id", 1);
            int rowsAffected = updateCommand.ExecuteNonQuery();
            Console.WriteLine($"rowsAffected: {rowsAffected}");
        }

        static void Delete()
        {
            string deleteQuery = "DELETE FROM Person WHERE id = @id";
            SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
            deleteCommand.Parameters.AddWithValue("@id", 1);
            int rowsAffected = deleteCommand.ExecuteNonQuery();
            Console.WriteLine($"rowsAffected: {rowsAffected}");
        }
    }
}
