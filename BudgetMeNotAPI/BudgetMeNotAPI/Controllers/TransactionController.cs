using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using BudgetMeNotAPI.Models;
namespace BudgetMeNotAPI.Controllers
{
    public class TransactionController : ApiController
    { 
        public Transaction Get (int txn_Id)
        {
            DataTable dt = new DataTable();
            Transaction trnsDetails = new Transaction();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AppDB"].ConnectionString);
            var sqlcomm = new SqlCommand("dbo.SP_GET_TRANSACTION", con);
            con.Open();
            sqlcomm.CommandType = CommandType.StoredProcedure;

            SqlParameter transactionID = new SqlParameter("@TXN_ID", SqlDbType.Int);
            transactionID.Direction = ParameterDirection.Input;
            transactionID.Value = txn_Id;
            sqlcomm.Parameters.Add(transactionID);

            SqlDataAdapter da = new SqlDataAdapter(sqlcomm);
            da.Fill(dt);

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    

                    trnsDetails.Txn_ID = dr.Field<int>("TXN_ID");
                    trnsDetails.Category_ID = dr.Field<int>("CATEGORY_ID");
                    trnsDetails.Sub_Category_ID = dr.Field<int>("SUB_CATEGORY_ID");
                    trnsDetails.Amount = dr.Field<decimal>("AMOUNT");
                    trnsDetails.Direction = dr.Field<string>("DIRECTION");
                    trnsDetails.Comment = dr.Field<string>("COMMENT");
                    trnsDetails.Attachment_ID = dr.Field<int>("ATTACHMENT_ID");
                    trnsDetails.Account_ID = dr.Field<int>("ACCOUNT_ID");
                    trnsDetails.Create_TS = dr.Field<DateTime>("CREATE_TS");
                    
                }

                return trnsDetails;
            }

            catch (Exception ex)
            {
                trnsDetails.Comment = ex.Message;
                return trnsDetails;
            }

        }
            


        

       public string Post(Transaction trns)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AppDB"].ConnectionString);
            var sqlcomm = new SqlCommand("dbo.SP_ADD_TRANSACTION", con);
            con.Open();
            sqlcomm.CommandType = CommandType.StoredProcedure;

            try
            {
                IncludeAddTransactionParameters(trns, sqlcomm);

                sqlcomm.ExecuteNonQuery();

                return "Success";

                sqlcomm.Dispose();
            }
            catch (Exception ex)
            {

                return string.Concat(sqlcomm.CommandText, "failure", ex.Message);
            }
        }

        public string Put(Transaction trns)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AppDB"].ConnectionString);
            var sqlcomm = new SqlCommand("dbo.SP_UPDATE_TRANSACTION", con);
            con.Open();
            sqlcomm.CommandType = CommandType.StoredProcedure;

            try
            {
                IncludeUpdateTransactionParameters(trns, sqlcomm);

                sqlcomm.ExecuteNonQuery();

                return "Success";

                sqlcomm.Dispose();
            }
            catch (Exception ex)
            {

                return string.Concat(sqlcomm.CommandText, "failure", ex.Message);
            }

        }

        public string Delete(Transaction trns)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AppDB"].ConnectionString);
            var sqlcomm = new SqlCommand("dbo.SP_DELETE_TRANSACTION", con);
            con.Open();
            sqlcomm.CommandType = CommandType.StoredProcedure;

            try
            {
                IncludeDeleteTransactionParameters(trns, sqlcomm);

                sqlcomm.ExecuteNonQuery();

                return "Success";
                
                sqlcomm.Dispose();
            }
            catch (Exception ex)
            {

                return string.Concat(sqlcomm.CommandText, "failure", ex.Message);
            }

        }
        /// <summary>
        /// Adds all the Stored Procedure Parameters
        /// </summary>
        /// <param name="trns"></param>
        /// <param name="sqlcomm"></param>
        private static void IncludeAddTransactionParameters(Transaction trns, SqlCommand sqlcomm)
        {
            SqlParameter transactionID = new SqlParameter("@TXN_ID", SqlDbType.Int);
            transactionID.Direction = ParameterDirection.Input;
            transactionID.Value = trns.Txn_ID;
            sqlcomm.Parameters.Add(transactionID);

            SqlParameter catergoryID = new SqlParameter("@CATEGORY_ID", SqlDbType.Int);
            catergoryID.Direction = ParameterDirection.Input;
            catergoryID.Value = trns.Category_ID;
            sqlcomm.Parameters.Add(catergoryID);

            SqlParameter subCategoryID = new SqlParameter("@SUB_CATEGORY_ID", SqlDbType.Int);
            subCategoryID.Direction = ParameterDirection.Input;
            subCategoryID.Value = trns.Sub_Category_ID;
            sqlcomm.Parameters.Add(subCategoryID);

            SqlParameter amount = new SqlParameter("@AMOUNT", SqlDbType.Decimal);
            amount.Direction = ParameterDirection.Input;
            amount.Value = trns.Amount;
            sqlcomm.Parameters.Add(amount);

            SqlParameter direction = new SqlParameter("@DIRECTION", SqlDbType.NVarChar);
            direction.Direction = ParameterDirection.Input;
            direction.Value = trns.Direction;
            sqlcomm.Parameters.Add(direction);

            SqlParameter commentField = new SqlParameter("@COMMENTFIELD", SqlDbType.NVarChar);
            commentField.Direction = ParameterDirection.Input;
            commentField.Value = trns.Comment;
            sqlcomm.Parameters.Add(commentField);

            SqlParameter attachmentId = new SqlParameter("@ATTACHMENT_ID", SqlDbType.Int);
            attachmentId.Direction = ParameterDirection.Input;
            attachmentId.Value = trns.Attachment_ID;
            sqlcomm.Parameters.Add(attachmentId);

            SqlParameter accountId = new SqlParameter("@ACCOUNT_ID", SqlDbType.Int);
            accountId.Direction = ParameterDirection.Input;
            accountId.Value = trns.Account_ID;
            sqlcomm.Parameters.Add(accountId);

            SqlParameter create_Timestamp = new SqlParameter("@CREATE_TS", SqlDbType.DateTime);
            create_Timestamp.Direction = ParameterDirection.Input;
            create_Timestamp.Value = DateTime.Now;
            sqlcomm.Parameters.Add(create_Timestamp);

            SqlParameter update_TimeStamp = new SqlParameter("@UPDATE_TS", SqlDbType.DateTime);
            update_TimeStamp.Direction = ParameterDirection.Input;
            update_TimeStamp.Value = DateTime.Now;
            sqlcomm.Parameters.Add(update_TimeStamp);
        }

    
        private static void IncludeUpdateTransactionParameters(Transaction trns, SqlCommand sqlcomm)
        {
            SqlParameter transactionID = new SqlParameter("@TXN_ID", SqlDbType.Int);
            transactionID.Direction = ParameterDirection.Input;
            transactionID.Value = trns.Txn_ID;
            sqlcomm.Parameters.Add(transactionID);

            SqlParameter catergoryID = new SqlParameter("@CATEGORY_ID", SqlDbType.Int);
            catergoryID.Direction = ParameterDirection.Input;
            catergoryID.Value = trns.Category_ID;
            sqlcomm.Parameters.Add(catergoryID);

            SqlParameter subCategoryID = new SqlParameter("@SUB_CATEGORY_ID", SqlDbType.Int);
            subCategoryID.Direction = ParameterDirection.Input;
            subCategoryID.Value = trns.Sub_Category_ID;
            sqlcomm.Parameters.Add(subCategoryID);

            SqlParameter amount = new SqlParameter("@AMOUNT", SqlDbType.Decimal);
            amount.Direction = ParameterDirection.Input;
            amount.Value = trns.Amount;
            sqlcomm.Parameters.Add(amount);

            SqlParameter direction = new SqlParameter("@DIRECTION", SqlDbType.NVarChar);
            direction.Direction = ParameterDirection.Input;
            direction.Value = trns.Direction;
            sqlcomm.Parameters.Add(direction);

            SqlParameter commentField = new SqlParameter("@COMMENTFIELD", SqlDbType.NVarChar);
            commentField.Direction = ParameterDirection.Input;
            commentField.Value = trns.Comment;
            sqlcomm.Parameters.Add(commentField);

            SqlParameter attachmentId = new SqlParameter("@ATTACHMENT_ID", SqlDbType.Int);
            attachmentId.Direction = ParameterDirection.Input;
            attachmentId.Value = trns.Attachment_ID;
            sqlcomm.Parameters.Add(attachmentId);

            SqlParameter accountId = new SqlParameter("@ACCOUNT_ID", SqlDbType.Int);
            accountId.Direction = ParameterDirection.Input;
            accountId.Value = trns.Account_ID;
            sqlcomm.Parameters.Add(accountId);

            SqlParameter update_TimeStamp = new SqlParameter("@UPDATE_TS", SqlDbType.DateTime);
            update_TimeStamp.Direction = ParameterDirection.Input;
            update_TimeStamp.Value = DateTime.Now;
            sqlcomm.Parameters.Add(update_TimeStamp);
        }


        private static void IncludeDeleteTransactionParameters(Transaction trns, SqlCommand sqlcomm)
        {
            SqlParameter transactionID = new SqlParameter("@TXN_ID", SqlDbType.Int);
            transactionID.Direction = ParameterDirection.Input;
            transactionID.Value = trns.Txn_ID;
            sqlcomm.Parameters.Add(transactionID);

            SqlParameter softDeleted = new SqlParameter("@SOFT_DELETED", SqlDbType.NVarChar);
            softDeleted.Direction = ParameterDirection.Input;
            softDeleted.Value = 'Y';
            sqlcomm.Parameters.Add(softDeleted);

            SqlParameter update_TimeStamp = new SqlParameter("@UPDATE_TS", SqlDbType.DateTime);
            update_TimeStamp.Direction = ParameterDirection.Input;
            update_TimeStamp.Value = DateTime.Now;
            sqlcomm.Parameters.Add(update_TimeStamp);
        }

    }
}
