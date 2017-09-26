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

        public achivement AddAchivement_Master(string description)
        {
            return _context.achivements.Add(new achivement() { description = description });
        }

        public void DeleteAchievement_Master(int achievementID)
        {
            _context.achivements.Remove(_context.achivements.Where(x => x.id == achievementID).FirstOrDefault());
        }

        public List<achivement> GetAchievements_Master()
        {
            return _context.achivements.ToList();
        }

        public achivement GetAchievement_Master(int achievementID)
        {
            return _context.achivements.Where(x => x.id == achievementID).FirstOrDefault();
        }

        public int GetAchievementCount_Master()
        {
            return _context.achivements.Count();
        }

        public achivement AddAchivement_User(user target, int achivementID)
        {
            // Get the achievement associated with the target ID
            achivement theAchievement = _context.achivements.Where(x => x.id == achivementID).FirstOrDefault();

            target.Achivements.Add(theAchievement);

            return theAchievement;
        }

        public void DeleteAchievement_User(user target, int achievementID)
        {
            // Get the achievement associated with the target ID
            achivement theAchievement = target.Achivements.Where(x => x.id == achievementID).FirstOrDefault();

            target.Achivements.Remove(theAchievement);

        }

        public int GetAchievementCount_User(user target)
        {
            return target.Achivements.Count();
        }

        public user AddFriend(user from, user to)
        {
            // Add friend for both users
            from.Friends.Add(to);
            to.Friends.Add(from);

            return to;
        }

        public void DeleteFriend(user from, user to)
        {
            // Delete friends from both users
            from.Friends.Remove(to);
            to.Friends.Remove(from);
        }

        public task AddTodoItem(user target, task item)
        {
            target.Tasks.Add(item);

            return item;
        }

        public void DeleteTodoItem(user target, int itemID)
        {
            target.Tasks.Remove(target.Tasks.Where(x => x.id == itemID).FirstOrDefault());
        }

        public reminder AddReminder(user target, reminder item)
        {
            target.Reminders.Add(item);

            return item;
        }

        public void DeleteReminder(user target, int reminderID)
        {
            target.Reminders.Remove(target.Reminders.Where(x => x.id == reminderID).FirstOrDefault());
        }

        public task AddTask(user target, task item)
        {
            throw new NotImplementedException();
        }
    }
}