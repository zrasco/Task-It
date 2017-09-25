using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoWebAPI.Models;

namespace TodoWebAPI.Repository
{
    public class TodoRepo : ITodoRepo
    {
        ToDoDataModelContainer _context = new ToDoDataModelContainer();

        public user AddUser(user newUser)
        {
            return new user();
        }
        public List<user> GetUsers()
        {
            return _context.users.ToList();
        }
        public void DeleteUser(int userID)
        {
            user target = _context.users.Where(x => x.id == userID).FirstOrDefault();

            if (target != null)
                _context.users.Remove(target);
        }

        public user GetUser(int userID)
        {
            return _context.users.Where(x => x.id == userID).FirstOrDefault();
        }
    }
}