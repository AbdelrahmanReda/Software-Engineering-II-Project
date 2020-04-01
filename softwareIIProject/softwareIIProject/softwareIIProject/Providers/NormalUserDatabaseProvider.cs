﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace softwareIIProject.Providers
{
    public class NormalUserDatabaseProvider : IDatabaseProvider
    {
       
        public void Delete()
        {
            throw new NotImplementedException();
        }

       

        public bool Insert(Dictionary<String, String> credentials)
        {
            int rowsAffected;

            
           String query = "insert into normalUser(Email,password) values(";
           foreach (var key in credentials)
            {
                query += "'"+key.Value+"'" +",";
            }
            query=query.Remove(query.Length - 1);
            query += ")";
            SqlDataAdapter adapter = new SqlDataAdapter
            {
                InsertCommand = new SqlCommand(query, DatabaseHelper.OpenDataBaseConnection())
            };
            
          
            rowsAffected = adapter.InsertCommand.ExecuteNonQuery();
            DatabaseHelper.CloseConnection();
            if (rowsAffected == 1)
            { return true; }
            return false;
        }

        public String Select()
        {
            String query = "select Email from normalUser";
            string result = "";
            SqlCommand command = new SqlCommand(query, DatabaseHelper.OpenDataBaseConnection());
            SqlDataReader dataReader = command.ExecuteReader();
            while(dataReader.Read())
            {
                result += dataReader.GetValue(0) + " " + "type: store owner\n";
            }
            DatabaseHelper.CloseConnection();
            return result;
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}