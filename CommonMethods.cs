using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System;

namespace WebApplication1
{
    public class CommonMethods
    {
        protected string ConString=string.Empty;
        protected SqlCommand cmd = null;
        protected SqlConnection sqlconn = null;

        protected void comp_dispose()
        {
            cmd.Dispose();
            sqlconn.Close();
        }
        public void insert_log(string email,string time, string action, string usertype)
        {
            ConString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
            using (sqlconn = new SqlConnection())
            {
                sqlconn.ConnectionString = ConString;
                cmd = new SqlCommand("enter_log_details", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("user_email",email);
                cmd.Parameters.AddWithValue("action_time",time);
                cmd.Parameters.AddWithValue("action", action);
                cmd.Parameters.AddWithValue("usertype", usertype);
                cmd.Parameters.AddWithValue("book_id", 0);
                cmd.Parameters.AddWithValue("removed_user", "");
                sqlconn.Open();
                cmd.ExecuteNonQuery();
            }
            comp_dispose();
        }
        public void insert_log(string email, string time, string action, string usertype,int bookid)
        {
            ConString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
            using (sqlconn = new SqlConnection())
            {
                sqlconn.ConnectionString = ConString;
                cmd = new SqlCommand("enter_log_details", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("user_email", email);
                cmd.Parameters.AddWithValue("action_time", time);
                cmd.Parameters.AddWithValue("action", action);
                cmd.Parameters.AddWithValue("usertype", usertype);
                cmd.Parameters.AddWithValue("book_id", bookid);
                cmd.Parameters.AddWithValue("removed_user", "");
                sqlconn.Open();
                cmd.ExecuteNonQuery();
            }
            comp_dispose();
        }
        public void insert_log(string email, string time, string action, string usertype, string removed_user)
        {
            ConString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
            using (sqlconn = new SqlConnection())
            {
                sqlconn.ConnectionString = ConString;
                cmd = new SqlCommand("enter_log_details", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("user_email", email);
                cmd.Parameters.AddWithValue("action_time", time);
                cmd.Parameters.AddWithValue("action", action);
                cmd.Parameters.AddWithValue("usertype", usertype);
                cmd.Parameters.AddWithValue("book_id", 0);
                cmd.Parameters.AddWithValue("removed_user", removed_user);
                sqlconn.Open();
                cmd.ExecuteNonQuery();
            }
            comp_dispose();
        }
        public int book_lending(string email, string lenddate,string duedate, int bookid)
        {
            int n;
            ConString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
            using (sqlconn = new SqlConnection())
            {
                sqlconn.ConnectionString = ConString;
                cmd = new SqlCommand("user_book_entry", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("lend_date", lenddate);
                cmd.Parameters.AddWithValue("return_date", "");
                cmd.Parameters.AddWithValue("due_date", duedate);
                cmd.Parameters.AddWithValue("fine", 0);
                cmd.Parameters.AddWithValue("book_id", bookid);
                sqlconn.Open();
                n=cmd.ExecuteNonQuery();
            }
            comp_dispose();
            return n;
        }
    }
}