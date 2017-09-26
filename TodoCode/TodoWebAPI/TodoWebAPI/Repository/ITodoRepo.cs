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
        user                    AddUser(user newUser);
        List<user>              GetUsers();
        user                    GetUser(int userID);
        void                    DeleteUser(int userID);

        // Achievements (master list)
        achivement              AddAchivement_Master(string description);
        void                    DeleteAchievement_Master(int achievementID);
        List<achivement>        GetAchievements_Master();
        achivement              GetAchievement_Master(int achievementID);
        int                     GetAchievementCount_Master();

        // Achievements (user list)
        achivement              AddAchivement_User(user target, int achivementID);
        void                    DeleteAchievement_User(user target, int achievementID);
        int                     GetAchievementCount_User(user target);

        // Friends
        user                    AddFriend(user from, user to);
        void                    DeleteFriend(user from, user to);

        // Todo items
        task                    AddTask(user target, task item);
        void                    DeleteTodoItem(user target, int taskID);

        // Reminders
        reminder                AddReminder(user target, reminder item);
        void                    DeleteReminder(user target, int reminderID);


    }
}