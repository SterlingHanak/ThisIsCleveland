using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class UserSqlDAL : IUserDAL
    {
        readonly string SQL_GetUser = "SELECT * FROM city_tours_user WHERE username = @username;";
        readonly string SQL_RegisterUser = "INSERT INTO city_tours_user VALUES(@firstName, @lastName, @email, " +
            "@username, @password, @salt, @accountCreationDate);";
        private string connectionString;

        public UserSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool ChangePassword(string username, string hashedPassword)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string userName)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetUser, conn);
                    cmd.Parameters.AddWithValue("username", userName);

                    User thisUser = null;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        thisUser = PopulateUserObject(reader);
                    }
                    return thisUser;
                }
            }
            catch(SqlException)
            {
                throw;
            }
        }

        public bool RegisterUser(User newUser)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_RegisterUser, conn);
                    cmd.Parameters.AddWithValue("firstName", newUser.FirstName);
                    cmd.Parameters.AddWithValue("lastName", newUser.LastName);
                    cmd.Parameters.AddWithValue("email", newUser.Email);
                    cmd.Parameters.AddWithValue("username", newUser.Username);
                    cmd.Parameters.AddWithValue("password", newUser.Password);
                    cmd.Parameters.AddWithValue("salt", newUser.Salt);
                    cmd.Parameters.AddWithValue("accountCreationDate", DateTime.Now);

                    int rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());
                    return rowsAffected > 0;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        private User PopulateUserObject(SqlDataReader reader)
        {
            User user = new User();
            user.FirstName = Convert.ToString(reader["first_name"]);
            user.LastName = Convert.ToString(reader["last_name"]);
            user.Email = Convert.ToString(reader["email"]);
            user.Username = Convert.ToString(reader["username"]);
            user.Password = Convert.ToString(reader["password"]);
            user.Salt = Convert.ToString(reader["salt"]);
            user.AccountCreationDate = Convert.ToDateTime(reader["account_creation_date"]);

            return user;
        }
    }
}