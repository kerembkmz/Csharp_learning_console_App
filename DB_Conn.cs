using System;
using MySql.Data.MySqlClient; //For connectiong to MySQL.
using WarNov.CryptAndHash; //For hashing the password 


namespace Csharp_weather_app
{
	public class DB_Conn
	{
        private readonly string connectionString;
        private readonly MySqlConnection cnn;


        public DB_Conn()
        {
            connectionString = "Server=127.0.0.1;Database=Map_Weather;Uid=" + All_Keys.GetDBUserId() + ";Pwd=" + All_Keys.GetDBPassword();
            cnn = new MySqlConnection(connectionString); //One line connection to the server.
        }

        public void RegisterUser(string username, string password)
        {
            // Generate Salt and RedPepper using WarBCrypt.SecurePwd
            var securedPwdInfo = WarBCrypt.SecurePwd(password, All_Keys.GetBlackPepper(), All_Keys.GetWorkForceLevel());
            string hashedPassword = securedPwdInfo.SecuredPwd;
            string salt = securedPwdInfo.Salt;
            string redPepper = securedPwdInfo.RedPepper;

            string insertUserQuery = "INSERT INTO users (username, passw, salt, redPepper) VALUES (@username, @hashedPassword, @salt, @redPepper)";

            try
            {
                using MySqlConnection cnn = new MySqlConnection(connectionString);
                cnn.Open();

                using MySqlCommand cmd = new MySqlCommand(insertUserQuery, cnn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                cmd.Parameters.AddWithValue("@salt", salt); 
                cmd.Parameters.AddWithValue("@redPepper", redPepper);
                //salt and redPepper added to the database for correct userValidation.

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Successfully added user");
                }
                else
                {
                    Console.WriteLine("Failed to add user");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }




        public bool CheckValidationUser(string username, string password)
        {
            string hashedPasswordFromDB = GetHashedPassFromDB(username);
            string saltFromDB = GetSaltFromDB(username);
            string redPepperFromDB = GetRedPepperFromDB(username);

            if (!string.IsNullOrEmpty(hashedPasswordFromDB)) // If the user exists in the DB.
            {
                var securedPwdInfo = new SecuredPwdInfo
                {
                    RedPepper = redPepperFromDB,
                    Salt = saltFromDB,
                    SecuredPwd = hashedPasswordFromDB
                };

                // Verify if the user-entered password matches the hashed password from the database
                var verificationResult = WarBCrypt.Verify(password, securedPwdInfo, All_Keys.GetBlackPepper());

                if (verificationResult)
                {
                    return true;
                }
            }

            return false;
        }
        public string GetSaltFromDB(string username)
        {
            string getSaltQuery = "SELECT salt FROM Map_Weather.users WHERE username = @username";
            MySqlCommand cmd = new MySqlCommand(getSaltQuery, cnn);
            cmd.Parameters.AddWithValue("@username", username);
            string saltFromDB = "";

            try
            {
                cnn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        saltFromDB = reader["salt"].ToString();
                    }
                }
                cnn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return saltFromDB;
        }

        public string GetRedPepperFromDB(string username)
        {
            string getRedPepperQuery = "SELECT redPepper FROM Map_Weather.users WHERE username = @username";
            MySqlCommand cmd = new MySqlCommand(getRedPepperQuery, cnn);
            cmd.Parameters.AddWithValue("@username", username);
            string redPepperFromDB = "";

            try
            {
                cnn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        redPepperFromDB = reader["redPepper"].ToString();
                    }
                }
                cnn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return redPepperFromDB;
        }

        public string GetHashedPassFromDB(string username) {
            string getPassQuery = "SELECT passw FROM Map_Weather.users WHERE username = @username";
            MySqlCommand cmd = new MySqlCommand(getPassQuery, cnn);
            cmd.Parameters.AddWithValue("@username", username);
            String hashedPassOfGivenUser = "";

            try
            {
                cnn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        hashedPassOfGivenUser = reader["passw"].ToString();
                    }
                }
                cnn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return hashedPassOfGivenUser;
        }


    }
}

