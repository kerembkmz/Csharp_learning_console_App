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
            SecuredPwdInfo hashedObj = HashPassword(password);
            string hashedPassword = hashedObj.SecuredPwd;
            string insertUserQuery = "INSERT INTO Map_Weather.users (username, passw) VALUES (@username, @hashedPassword)";


            try
            {
                cnn.Open();
                MySqlCommand cmd = new MySqlCommand(insertUserQuery, cnn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Succefully added user");
                cnn.Close();
            }
            catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            } 

        }

        public bool CheckValidationUser(string username, string password)
        {

            string hashedPasswordFromDB = GetHashedPassFromDB(username);
            

            if (hashedPasswordFromDB != null) //If the user exists in the DB.
            {
                SecuredPwdInfo hashedObj = HashPassword(password);
                var verificationResult = WarBCrypt.Verify(password, hashedObj, All_Keys.GetBlackPepper()); //Checking if the passwords matches the stored hash.
                if (verificationResult)
                {
                    return true;
                }
            }


            return false;

        }

        private SecuredPwdInfo HashPassword(string password) {
            var securedPwdInfo = WarBCrypt.SecurePwd(password, All_Keys.GetBlackPepper(), All_Keys.GetWorkForceLevel());

            return securedPwdInfo;
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

