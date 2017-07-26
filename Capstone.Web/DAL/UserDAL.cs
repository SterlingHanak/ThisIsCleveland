using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class UserDAL : IUserDAL
    {
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
            throw new NotImplementedException();
        }

        public bool RegisterUser(User newUser)
        {
            throw new NotImplementedException();
        }
    }
}