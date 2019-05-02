using DBConnection_Steppy_Giseppy.DBConn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;
using System.IO;
using Microsoft.WindowsAzure.Management.Storage;
using Sandboxable.Microsoft.WindowsAzure.Storage.Blob;
using Sandboxable.Microsoft.WindowsAzure.Storage;
using Sandboxable.Microsoft.WindowsAzure.Storage.File;
using Microsoft.Azure;

namespace DBConnection_Steppy_Giseppy
{
    public partial class Signin_fitbit : System.Web.UI.Page
    {
        // Objects made from classes
        protected FitBitData fbd = new FitBitData();
        protected Queries qs = new Queries();
        Activity act = new Activity();

        // stores the different scores of the user
        public int newScore = 0, oldScore = 0, fitbitSteps=0;

        // Stores new speed/jump for after they load the game
        public int newSpeed = 0, oldSpeed = 0, newJump = 0, oldJump = 0; 

        // Other variables
        private static string user;
        public string url, fitbitActivity, updatedDate, previousUser;
        public int steps;

        // Determines whether or not the user already updated their step coins today
        bool accessed = false;

        public const string publish_clientID = "22D8L6", debug_clientID = "22D4SC";
        public const string publish_uri = "https://smeedsgaming.azurewebsites.net/Signin-fitbit", debug_uri = "http://localhost:52675/Signin-fitbit";
        public const string publish_secret = "dcd7df5684f508a1fda053cd42f5a1c5", debug_secret = "9cbb582df0b6817eefcdae7c0fef6d91";

        /* ---------------------------------------------------------------------------------------- *
         * ------------------------------------- ON PAGE LOAD ------------------------------------- *
         * ---------------------------------------------------------------------------------------- */
        protected void Page_Load(object sender, EventArgs e)
        {
            // ----------------- READING PREVIOUSLY STORED GAME STATS ----------------- //

            // When the page loads, this gets the path that the application currently runs in.

            // Get Steps button cannot be clicked. 
            getSteps.Enabled = false;
            user = "";
        }

        /* ---------------------------------------------------------------------------------------- *
         * ----------------------------------- GET ACCESS TOKEN ----------------------------------- *
         * ---------------------------------------------------------------------------------------- */
        public void getAccessToken(object sender, EventArgs e)
        {
            if (!accessed)
            {

                // ----------------- PULLING THE DATA FROM FITBIT ----------------- //

                // Gets the access token response from the webpage
                string authentication_code = Request.QueryString["code"];
                AccessToken token = GetToken(authentication_code).Result;
                tokenInput.Value = token.access_token;

                // A string pulled from fitbit that has the steps, userID and access token.
                string fitbitActivity = fbd.getStepsFitBitData(token.access_token);
                XmlDocument json = JsonConvert.DeserializeXmlNode(fitbitActivity);

                // Separates the data in the fitbitActivity string. 
                XDocument doc = XDocument.Parse(json.InnerXml);

                // Separates the date and number of steps. 
                var root = doc.Descendants("value").FirstOrDefault();
                var date_new = doc.Descendants("dateTime").FirstOrDefault();
                string stepString = root.Value;
                act.Current_Steps = root.Value;
                act.Updated_Date = date_new.Value;

                // ---------------------- UPDATES THE USER ---------------------- //

                // If the user does not exist yet. If not, it adds the user with default values.
                if (!qs.checkIfUserExists(user)) qs.addNewUser(user);

                

                // ----------------- SHOWING DATA ON THE SCREEN ----------------- //

                // <p> tags dynamically change when clicking 'Add Steps'
                currentScoreP.Visible = false;
                newScoreP.Visible = true;

                // Puts the date and stepcoins store onto the screen:
                date.Value = act.Updated_Date;
                oldScore = qs.selectStepCount(user);
                oldStepCoins.Value = oldScore.ToString();

                // Displays the number of steps from fitbit
                fitbitSteps = Convert.ToInt32(stepString); // change to stepString.
                stepsFromFitbit.Value = fitbitSteps.ToString();

                // ----------------- CHECK FOR IF THEY ALREADY UPDATED THEIR STEPS ----------------- //

                // Gets yesterday's date
                string yesterday = Convert.ToDateTime(DateTime.Today.AddDays(-1)).ToString("yyyy-MM-dd");

                // Calls method in Query class
                // Checks if the user already updated their steps (if their lastAccessed Day is today).
                bool alreadyAccessed = qs.alreadyUpdatedSteps(yesterday, user);

                // Updates the step count in the DB with their added steps from the fitbit
                // (If they have not already updated it yet today).
                qs.updateStepCount(oldScore, fitbitSteps, alreadyAccessed, user);

                // After the calculation for their new steps, the new value displays onto the screen
                newScore = qs.selectStepCount(user);
                newStepCoins.Value = newScore.ToString();

                // If they have already updated their steps into the game:
                if (alreadyAccessed)
                {
                    // An error message will show and their steps will not be updated a second time. 
                    newScoreP.InnerHtml = "You have already updated your step coins today. Please Try again tomorrow.";
                    stepsFromFitbit.Value = "0";
                }
                // Their new step count displays. 
                else newScoreP.InnerHtml = "Your new Step Coins Score is " + newScore;

                // The last accessed is updated to yesterday's date (last accessed meaning - last accessed fitbit data)
                qs.updateLastAccessed(yesterday, user);

                // They can select the 'start game' button
                Button2.Enabled = false;
                getSteps.Enabled = true;

                // Writes the new steps to the file
                UpdateDataUrl("data.txt", "WRITE", newScore.ToString());
                UpdateDataUrl("speed.txt", "WRITE", qs.selectSpeedLevel(user).ToString());
                UpdateDataUrl("jump.txt", "WRITE", qs.selectJumpLevel(user).ToString());
                // Save their userID in the file on Azure.
                UpdateDataUrl("currentUser.txt", "WRITE", user);
            }
        }

        public string UpdateDataUrl(string fileName, string method, string newData)
        {
            string res = "No file exists";
            CloudStorageAccount storage = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=steppygiuseppegame;AccountKey=PxIu1wyeibGpZtlv3IlMtTL1ySyhXR3GCOypdGW5+ZaBhfjDlF3RvQ2t7C/TT9D/p50OcmmFUftCQoFY6V49Tg==;EndpointSuffix=core.windows.net");

            CloudBlobClient serviceClient = storage.CreateCloudBlobClient();

            CloudBlobContainer share = serviceClient.GetContainerReference("test");

            // Ensure that the share exists.
            if (share.Exists())
            {
                CloudBlockBlob blob = share.GetBlockBlobReference(fileName);

                // Ensure that the file exists.
                if (blob.Exists())
                {
                    if (method.Equals("READ")) res = blob.DownloadTextAsync().Result;
                    else
                    {
                        // Write the contents of the file to the console window.
                        blob.UploadTextAsync(newData);
                        res = blob.DownloadTextAsync().Result;
                    }
                       
                }

            }

            return res;
        }

        /* ---------------------------------------------------------------------------------------- *
         * -------------------------------------- GET TOKEN --------------------------------------- *
         * ---------------------------------------------------------------------------------------- */
        static async Task<AccessToken> GetToken(string authCode)
        {
            // Called in the above method.
            // Sets headers and data to send to the fitbit site as part of the request
            var bytes = System.Text.Encoding.UTF8.GetBytes(publish_clientID + ":"+ publish_secret);
            var encoded_Text = Convert.ToBase64String(bytes);
            using (var wb = new WebClient())
            {
                // authentication for this app to request their data
                var data = new NameValueCollection();
                data["grant_type"] = "authorization_code";
                data["client_id"] = publish_clientID;
                data["redirect_uri"] = publish_uri;
                data["code"] = authCode;

                wb.Headers["Authorization"] = "Basic " + encoded_Text;

                // posts the data/headers to the fitbit Oauth  
                var response = wb.UploadValues("https://api.fitbit.com/oauth2/token", "POST", data);
                
                // Gets their response
                var responseString = Encoding.ASCII.GetString(response);

                // Does some substring magic to get the value of the userID 
                // An example of the userID is '2D3GHY' - every login has their own set userID from fitbit.
                user = responseString;
                var userIndexBegin = user.IndexOf("user_id");
                var userIndexEnd = user.LastIndexOf("\"}");
                user = user.Substring(userIndexBegin + 10, 6);


                // returns the response
                return JsonConvert.DeserializeObject<AccessToken>(responseString);
            };

        }

        /* ---------------------------------------------------------------------------------------- *
         * ------------------------------------- UPDATE STEPS ------------------------------------- *
         * ---------------------------------------------------------------------------------------- */
        public void updateSteps(object sender, EventArgs e)
        {

            // Redirects to the index.html file hosted on the server (the game is saved with the php files)
            // The updated data from the DB is pushed to the php files on the server
            // which is then accessed directly during the game for dynamic gameplay
            Response.Redirect(@"http://10.222.168.98/index.html");
        }

/* ----------------------------------------- METHOD NOT BEING USED---------------------------------------- */

        /* ---------------------------------------------------------------------------------------- *
         * ------------------------------------- RETURN STEPS ------------------------------------- *
         * ---------------------------------------------------------------------------------------- */
        public void returnSteps(string fitbitjson)
        {
            // Method not being used. 
            var stepsData = JArray.Parse(fitbitjson);

            foreach (JObject root in stepsData)
            {
                foreach (KeyValuePair<String, JToken> app in root)
                {
                    var activityStepsData = app.Key;
                    var date_time_Steps = (String)app.Value["dateTime"];
                    var stepsValue = (String)app.Value["value"];

                    steps = Convert.ToInt32(stepsValue);
                    updatedDate = date_time_Steps;
                }
            }

            stepsFromFitbit.Value = Convert.ToString(steps);
        }

    }
}
 