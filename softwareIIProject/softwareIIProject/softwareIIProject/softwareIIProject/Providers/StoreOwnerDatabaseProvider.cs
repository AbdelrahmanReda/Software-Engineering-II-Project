using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace softwareIIProject.Providers
{
    public class StoreOwnerDatabaseProvider : IDatabaseProvider
    {
       
        public StoreOwnerDatabaseProvider()
        {

        }
        public void Delete()
        {
            throw new NotImplementedException();
        }


        
        public bool Insert(Dictionary<String, String> credentials)
        {
            int rowsAffected;
            
         
            String query = "insert into storeOwner(Email,password) values(";
            foreach(var key in credentials)
            {
                query += "'" + key.Value + "'" + ",";
            }
            query = query.Remove(query.Length - 1);
            query += ")";
            SqlDataAdapter adapter = new SqlDataAdapter
            {
                InsertCommand = new SqlCommand(query, DatabaseHelper.OpenDataBaseConnection())
            };
            rowsAffected = adapter.InsertCommand.ExecuteNonQuery();
            DatabaseHelper.CloseConnection();
            if (rowsAffected==1)
            { return true; }
            return false;
            
        }

        public List<Dictionary<String, Object>> Select()
        {
            List<Dictionary<String, Object>> data = new List<Dictionary<String, Object>>();
            String query = "select Email from storeOwner";

            SqlCommand command = new SqlCommand(query, DatabaseHelper.OpenDataBaseConnection());
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Dictionary<String, object> row = new Dictionary<string, object>();
                row.Add("Email", dataReader.GetValue(0));
                data.Add(row);
            }
            DatabaseHelper.CloseConnection();
            return data;
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}