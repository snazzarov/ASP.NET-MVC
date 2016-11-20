using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NoEF.Models;
using System.Linq;

namespace NoEF.Repository
{
    public class RepositoryBase
    {
        protected SqlConnection con => new SqlConnection(ConfigurationManager.ConnectionStrings["HomeBank"].ToString());
    }

    public class AccRepository: RepositoryBase
    {
        
        //To Handle connection related activities
        //string conString = "Server=(localdb)\\mssqllocaldb;Database=HomeBank;Trusted_Connection=True;MultipleActiveResultSets=true";
        
        //To Add Accounts
        // Creates User account (not Currency) for which Balance has to equal 0
        public bool AddAccount(Account obj, int curFlag)
        {
            SqlCommand com = new SqlCommand("AddAccount", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Title", obj.Title);
            if (curFlag == 0)
                com.Parameters.AddWithValue("@Balance", 0);
            else
                com.Parameters.AddWithValue("@Balance", obj.Balance);
            com.Parameters.AddWithValue("@CurrencyId", obj.CurrencyId);

            using (com.Connection = con)
            {
                com.Connection.Open();
                int i = com.ExecuteNonQuery();

                if (i >= 1)
                    return true;
                else
                    return false;
            }
        }

        //To view account details with generic list 
        public List<AccountCurrency> GetAccounts()
        {
            
            List<AccountCurrency> AccList = new List<AccountCurrency>();
            SqlCommand com = new SqlCommand("dbo.GetAccounts", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            using (com.Connection = con)
            {
                com.Connection.Open();
                da.Fill(dt);
            }

            //Bind Account Model generic list using LINQ 
            AccList = (from DataRow dr in dt.Rows
                       select new AccountCurrency()
                       {
                           Id = Convert.ToInt32(dr["Id"]),
                           // PrefixTitle = GlobalVars.IncomeAccountPrefix,
                           Title = Convert.ToString(dr["Title"]),
                           Balance = Convert.ToDecimal(dr["Balance"]),
                           CurName = Convert.ToString(dr["CurName"]),
                           CurrencyId = Convert.ToInt32(dr["CurrencyId"])
                       }).ToList();
            return AccList;
        }

        //To Update Account details
        public bool UpdateAccount(Account obj)
        {
            
            SqlCommand com = new SqlCommand("UpdateAccount", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", obj.Id);
            com.Parameters.AddWithValue("@Title", obj.Title);
            com.Parameters.AddWithValue("@Balance", obj.Balance);
            com.Parameters.AddWithValue("@CurrencyId", obj.CurrencyId);
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

        //To delete Account details
        public bool DeleteAccount(int Id)
        {
            
            SqlCommand com = new SqlCommand("DeleteAccount", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", Id);

            using (com.Connection = con)
            {
                com.Connection.Open();
                int i = com.ExecuteNonQuery();

                if (i >= 1)
                    return true;
                else
                    return false;
            }
        }
    }
}