using DBConnection_Steppy_Giseppy.DBConn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Collections.Specialized;
using Microsoft.Bot.Connector.Authentication;
using System.IO;
using System.Text;


namespace DBConnection_Steppy_Giseppy
{
    public partial class _Default : Page
    {
        // objects made from FitBitData and Queries classes
        protected FitBitData fbd = new FitBitData();
        protected Queries qs = new Queries();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void openURL(object sender, EventArgs e)
        {
            // For publishing:
            var url = "https://www.fitbit.com/oauth2/authorize?response_type=code&client_id=22D8L6&redirect_uri=https://smeedsgaming.azurewebsites.net/Signin-fitbit&scope=activity&expires_in=604800&prompt=login";

            // For debugging:
            //var url = "https://www.fitbit.com/oauth2/authorize?response_type=code&client_id=22D4SC&redirect_uri=http://localhost:52675/Signin-fitbit&scope=activity&expires_in=604800&prompt=login";
            

            //string url = "https://www.fitbit.com/oauth2/authorize?response_type=token&client_id=22CY4T&&redirect_uri=http%3A%2F%2F127.0.0.1%2Fhello.html&scope=activity&prompt=login";
            

            // opens the fitbit website
            Response.Redirect(url, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}