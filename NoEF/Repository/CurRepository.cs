using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NoEF.Models;
using System.Linq;

namespace NoEF.Repository
{
    public class CurRepository: RepositoryBase
    {
  

        //To Add Currencies
        public bool AddCurrency(Currency obj)
        {
            
            SqlCommand com = new SqlCommand("AddCurrency");
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", obj.Name);
            using (com.Connection = con)
            {
                com.Connection.Open();
                int i = com.ExecuteNonQuery();
                //con.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //To view currency details with generic list 
        public List<Currency> GetCurrencies()
        {
            
            List<Currency> CurList = new List<Currency>();
            SqlCommand com = new SqlCommand("dbo.GetCurrencies", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            using (com.Connection = con)
            {
                com.Connection.Open();
                da.Fill(dt);
            }

            //Bind Currency Model generic list using LINQ 
            CurList = (from DataRow dr in dt.Rows
                       select new Currency()
                       {
                           Id = Convert.ToInt32(dr["Id"]),
                           Name = Convert.ToString(dr["Name"])
                       }).ToList();          
            return CurList;
        }

        //To Update Currency details
        public bool UpdateCurrency(Currency obj)
        {

            SqlCommand com = new SqlCommand("UpdateCurrency", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", obj.Id);
            com.Parameters.AddWithValue("@Name", obj.Name);
            using (com.Connection = con)
            {
                com.Connection.Open();
                int i = com.ExecuteNonQuery();

                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //To delete Currency details
        public bool DeleteCurrency(int Id)
        {

            SqlCommand com = new SqlCommand("DeleteCurrency", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", Id);
            using (com.Connection = con)
            {
                com.Connection.Open();
                int i = com.ExecuteNonQuery();


                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}