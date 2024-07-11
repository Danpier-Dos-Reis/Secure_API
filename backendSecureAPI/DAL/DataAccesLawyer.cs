using System.Data.SQLite;

namespace DAL
{
    public class DataAccesLawyer
    {
        private string _connectionString = "Data Source=DB/secure_api.db;Version=3;";

        public bool IsDatabaseConnected()
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validate is an user are registered
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UserExist(string username)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(1) FROM Users WHERE _Name = @Name";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", username);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0 ? true:false;
                    }
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool RegisterUser(string username, string password)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Users (_Name, _Password) VALUES (@Name, @Password)";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }

    
}