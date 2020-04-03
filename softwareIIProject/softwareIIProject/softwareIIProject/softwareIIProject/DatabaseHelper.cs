using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace softwareIIProject
{
    public  class DatabaseHelper
    {
        private static SqlConnection connection;
       
        public static SqlConnection OpenDataBaseConnection()
        {
            connection = new SqlConnection("Data Source=DESKTOP-85QS9MQ;Initial Catalog=onlineStorePlatform;Integrated Security=True");
            connection.Open();
            return connection;
           
        }
        
        public static void CloseConnection()
        {
            connection.Close();
        }
    }
}