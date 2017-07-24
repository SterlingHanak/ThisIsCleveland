using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface IUserDAL
    {
        User GetUser(string userName, string password);
        User GetUser(string userName);
        bool ChangePassword(string username, string hashedPassword);
        bool RegisterUser(User newUser);
    }
}
