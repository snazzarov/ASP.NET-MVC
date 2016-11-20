using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NoEF.Models;
using System.Linq;

namespace NoEF.Repository
{

    public class OperRepository : RepositoryBase
    {

        //To Handle connection related activities
        //string conString = "Server=(localdb)\\mssqllocaldb;Database=HomeBank;Trusted_Connection=True;MultipleActiveResultSets=true";

        //To Add Operation
        public bool AddOperation(Operation obj)
        {
            int result = -1;
            //var resultParam = new ObjectParameter("result", typeof(int));

            SqlCommand com = new SqlCommand("AddOperation", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Amount", obj.Amount);
            com.Parameters.AddWithValue("@DebetAccId", obj.DebetAccId);
            com.Parameters.AddWithValue("@CreditAccId", obj.CreditAccId);
            com.Parameters.AddWithValue("@Comment", obj.Comment);
            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = "@Result";
            outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
            outPutParameter.Direction = System.Data.ParameterDirection.Output;
            com.Parameters.Add(outPutParameter);

            using (com.Connection = con)
            {
                com.Connection.Open();
                int i = com.ExecuteNonQuery();
                result = (int)outPutParameter.Value;
                if (result == 0)
                    return true;
                else
                    return false;
            }
        }

        //To view account details with generic list 
        public List<OperationAccount> GetOperations()
        {

            List<OperationAccount> OpList = new List<OperationAccount>();
            SqlCommand com = new SqlCommand("dbo.GetOperations", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            using (com.Connection = con)
            {
                com.Connection.Open();
                da.Fill(dt);
            }

            //Bind Account Model generic list using LINQ 
            OpList = (from DataRow dr in dt.Rows
                       select new OperationAccount()
                       {
                           Id = Convert.ToInt32(dr["Id"]),
                           Amount = Convert.ToDecimal(dr["Amount"]),
                           DebetAccId = Convert.ToInt32(dr["DebetAccId"]),
                           CreditAccId = Convert.ToInt32(dr["DebetAccId"]),
                           Comment = Convert.ToString(dr["Comment"]),
                           DebetAcc = Convert.ToString(dr["DebetAcc"]),
                           CreditAcc = Convert.ToString(dr["CreditAcc"])
                       }).ToList();
            return OpList;
        }

        //To Update Account details
/*
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
*/
        //To delete Account details
        public bool DeleteOperation(int Id)
        {

            SqlCommand com = new SqlCommand("DeleteOperation", con);
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
        public bool CheckCurrencies(int DebetAccId, int CreditAccId)
        {
            int result;

            SqlCommand com = new SqlCommand("CheckCurrencies", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@DebetAccId", DebetAccId);
            com.Parameters.AddWithValue("@CreditAccId", CreditAccId);
            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = "@Ret";
            outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
            outPutParameter.Direction = System.Data.ParameterDirection.Output;
            com.Parameters.Add(outPutParameter);
            using (com.Connection = con)
            {
                com.Connection.Open();
                int i = com.ExecuteNonQuery();
                result = (int)outPutParameter.Value;
                if (result == 0)
                    return false;
                else
                    return true;                
            }
        }
    }
}
