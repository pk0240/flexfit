using System;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace DBConnection_Steppy_Giseppy.DBConn
{
    public class DBAccess
    {
        // connection variable for connecting to the SQL Database
        public String connection;
        SqlConnection sqlconn = new SqlConnection();

        // Default constructor for CustomerAccountDL
        public DBAccess()
        {
            // The connection connects to the SQL Database Server on Sharlene's laptop 
            // It is called INFT3050_Merchkraft_DB
            connection = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            //"Data Source=LAPTOP-55E0CCU5\\SQLEXPRESS;Initial Catalog=INFT3050_Merchkraft_DB;Persist Security Info=True;User ID=sa;Password=Leo_mia*1234";
            sqlconn.ConnectionString = connection;

        }

        public ArrayList selectData(String query, String column)
        {

            ArrayList attributes = new ArrayList();

            // Will open the connection to the DB
            if (ConnectionState.Closed == sqlconn.State)
                sqlconn.Open();

            // sql command obj made with the sql query passed into the method and the connection
            SqlCommand cmd = new SqlCommand(query, sqlconn);

            try
            {
                // Will execute the query
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if(column.Equals("lastAccessed")) attributes.Add(rd[column].ToString());
                    else if(column.Equals("user_ID")) attributes.Add(rd[column].ToString());
                    // If the column holds int values, then convert it to an int and store it in the array
                    else attributes.Add(Int32.Parse(rd[column].ToString()));
                }
            }
            catch (Exception e)
            {
                attributes.Add("Something went wrong");
            }

            // Close the connection
            if (ConnectionState.Open == sqlconn.State)
                sqlconn.Close();

            // return the values in the array
            return attributes;

        }

        // Inserts a new Login record into the Database based on a query

        public void updateRecord(String query)
        {
            // Will open the connection to the DB
            if (ConnectionState.Closed == sqlconn.State)
                sqlconn.Open();

            SqlCommand cmd = new SqlCommand(query, sqlconn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { }
        }

    }
}