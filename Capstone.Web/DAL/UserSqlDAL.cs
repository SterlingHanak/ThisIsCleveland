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
        readonly string SQL_GetUser = "SELECT * FROM user WHERE username = @username;";
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

                    User thisUser = new User();
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
            throw new NotImplementedException();
        }

        private User PopulateUserObject(SqlDataReader reader)
        {
            User user = new User();
            user.FirstName = Convert.ToString(reader["first_name"]);
            user.LastName = Convert.ToString(reader["last_name"]);
            user.Username = Convert.ToString(reader["username"]);
            user.Password = Convert.ToString(reader["password"]);
            user.Salt = Convert.ToString(reader["salt"]);

            return user;
        }
    }
}