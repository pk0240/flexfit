using System;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Linq;

namespace DBConnection_Steppy_Giseppy.DBConn
{
    public class FitBitData
    {

        public static string yesterdays_Date = GetYesterdayDate();
        private string Updated_Date;
        public string ReturnYesterdayDate()
        {
            return yesterdays_Date;
        }
        public string getStepsFitBitData(string strToken)
        {
            

            StringBuilder dataRequestUrl = new StringBuilder();

            //  dataRequestUrl.Append("https://api.fitbit.com/1/user/-/activity.json");
            //1d is one day, can use 1d, 7d, 30d, 1w, 1m, 3m, 6m, 1y
            dataRequestUrl.Append("https://api.fitbit.com/1/user/-/activities/tracker/steps/date/" + yesterdays_Date + "/1d.json");


            HttpWebRequest dataRequest = (HttpWebRequest)WebRequest.Create(dataRequestUrl.ToString());

            String accessToken = strToken;
            dataRequest.Method = "GET";
            dataRequest.Headers.Add("Authorization", "Bearer " + accessToken);
            dataRequest.ContentType = "application/json";

            string responseJson;
            HttpWebResponse response = null;
            try
            {
                response = dataRequest.GetResponse() as HttpWebResponse;
            }
            catch (WebException webEx)
            {
                response = webEx.Response as HttpWebResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseJson = reader.ReadToEnd();

            }
            Updated_Date = yesterdays_Date;
            
            
            return responseJson;
            

        }
        public string ReturnUpdatedDate()
        {
            return Updated_Date;
        }
        public static string GetYesterdayDate()
        {
            var date = "";
            if (DateTime.Now.Day == 1)
            {
                //we know that we are on the first day of the month, if we are the first day of Jan then we need to go back to the last day of Dec
                if (DateTime.Today.Month == 1)
                {
                    date += DateTime.Now.Year - 1;
                    date += "-12-31";
                    return date;
                }
                //else we aren't Jan so we can just subtract a month and go to the last day of that month.
                else
                {
                    date += DateTime.Now.Year
                        + "-" + (DateTime.Today.Month - 1)
                            + "-" + (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - 1));
                    return date;
                }
            }
            date += DateTime.Now.Year;

            //Months
            if (DateTime.Now.Month < 10)
            {
                date += "-" + "0" + DateTime.Now.Month;
            }
            else
            {
                date += "-" + DateTime.Now.Month;
            }

            //Days
            if (DateTime.Now.Day - 1 < 10)
            {
                date += "-" + "0" + (DateTime.Now.Day - 1);
            }
            else
            {
                date += "-" + (DateTime.Now.Day - 1);
            }
            return date;
        }
    }
}