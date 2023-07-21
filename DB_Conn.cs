using System;
using Microsoft.Data.SqlClient; //For connecting to DB.
using MySql.Data.MySqlClient; //For connectiong to MySQL.

namespace Csharp_weather_app
{
	public class DB_Conn
	{
        private readonly string connectionString;
        private readonly MySqlConnection cnn;

        public DB_Conn()
        {
            connectionString = "Server=127.0.0.1;Database=Map_Weather;Uid=" + All_Keys.GetDBUserId() + ";Pwd=" + All_Keys.GetDBPassword();
            cnn = new MySqlConnection();
            cnn.ConnectionString = connectionString;
            cnn.Open();
            string query1 = "SELECT * FROM Map_Weather.users";
            MySqlCommand cmd = new MySqlCommand(query1, cnn);
            try
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Access data from the reader inside the loop
                        string username = reader["username"].ToString();
                        string passw = reader["passw"].ToString();
                        Console.WriteLine(username + "/" + passw);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        



    }
}

