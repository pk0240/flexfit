using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace DBConnection_Steppy_Giseppy.DBConn
{
    public class Queries
    {
        // Object to connect & access DB directly
        DBAccess dba = new DBAccess();

        /* ---------------------------------------------------------------------------------------- *
         * ---------------------------------- SELECT STEP COUNT ----------------------------------- *
         * ---------------------------------------------------------------------------------------- */
        public int selectStepCount(string user)
        {
            // Query to get the step count to 
            ArrayList numStepCounts = dba.selectData("SELECT numberOfStepCoins FROM GameAccountState WHERE user_ID='" + user + "' ", "numberOfStepCoins");
            return Int32.Parse(numStepCounts[0].ToString());
        }

        public int selectSpeedLevel(string user)
        {
            // Query to get the step count to 
            string test = user;
            ArrayList speedLevel = dba.selectData("SELECT currentSpeed FROM GameAccountState WHERE user_ID='" + user + "' ", "currentSpeed");
            return Int32.Parse(speedLevel[0].ToString());
        }

        public int selectJumpLevel(string user)
        {
            // Query to get the step count to 
            ArrayList jumpLevel = dba.selectData("SELECT currentJump FROM GameAccountState WHERE user_ID='" + user + "' ", "currentJump");
            return Int32.Parse(jumpLevel[0].ToString());
        }

        /* ---------------------------------------------------------------------------------------- *
         * ---------------------------------- SELECT STEP COUNT ----------------------------------- *
         * ---------------------------------------------------------------------------------------- */
        public bool alreadyUpdatedSteps(string newAccessDate, string user)
        {
            ArrayList accessDate = dba.selectData("SELECT lastAccessed FROM GameAccountState WHERE user_ID='" + user + "' ", "lastAccessed");
            string Compare1 = accessDate[0].ToString();
            string Compare2 = newAccessDate;
            if (accessDate[0].ToString().Equals(newAccessDate))
                return true;
            else return false;
        }

        public int updateStepCount(int resOldScore, int resFitbitSteps, bool alreadyAccessed, string user)
        {
            int res = 0;
            String query = "";
            // If the numberOfSteps have not already been updated from fitbit (today)
            if (!alreadyAccessed) res = resOldScore + resFitbitSteps;
            // If the number of steps has already been added, they only see their old score. 
            else res = resOldScore;

            // Updated the database for their new steps they will use before they start the game.
            query = "UPDATE GameAccountState SET numberOfStepCoins = " + res + " WHERE  user_ID='" + user + "'";
            dba.updateRecord(query);

            return selectStepCount(user);
        }

        public void updateSpeed(int res, string user)
        {
            string query = "UPDATE GameAccountState SET currentSpeed = " + res + " WHERE  user_ID='" + user + "'";
            dba.updateRecord(query);
        }

        public void updateJump(int res, string user)
        {
            string query = "UPDATE GameAccountState SET currentJump = " + res + " WHERE  user_ID='" + user + "'";
            dba.updateRecord(query);
        }

        public void updateLastAccessed(string res, string user)
        {
            string query = "UPDATE GameAccountState SET lastAccessed = '" + res + "' WHERE  user_ID='" + user + "'";
            dba.updateRecord(query);
        }

        public bool checkIfUserExists(string userID)
        {
            ArrayList listOfUsers = dba.selectData("SELECT user_ID FROM GameAccountState", "user_ID");
            for(int i=0; i< listOfUsers.Count; i++)
            {
                if (userID.Equals(listOfUsers[i])) return true;
            }
            return false;
        }

        public void addNewUser(string newUser)
        {  
            // Inserts new User
            string query = "INSERT INTO GameUser VALUES('" + newUser + "', '"+ newUser + "', 0, 0, 0, 0, 0)";
            dba.updateRecord(query);

            // Inserts new game Account for the user
            string newAccountID = newUser + "00";
            query = "INSERT INTO GameAccountState VALUES('"+ newAccountID + "', 0, 0, 1, 1, 0, 0, 'NA', 'idk', '"+ newUser + "')";
            dba.updateRecord(query);
        }


    }
}