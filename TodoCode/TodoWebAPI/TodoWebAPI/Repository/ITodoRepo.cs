using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoWebAPI.Models;

namespace TodoWebAPI.Repository
{
    public interface ITodoRepo
    {
        // User-related
        user AddUser(user newUser);
        List<user> GetUsers();
        user GetUser(int userID);
        void DeleteUser(int userID);
    }
}