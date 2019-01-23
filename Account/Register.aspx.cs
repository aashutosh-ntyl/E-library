using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using WebApplication1;
using System.Drawing;

namespace WebApplication1.Account
{
    public partial class Register : Page
    {
        protected string MysqlString = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void insert_user(object sender, EventArgs e)
        {
            try
            {
                string name = UserName.Text;
                string email = Email.Text;
                string pass = Password.Text;
                string user = usertype.SelectedItem.Text;
                MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
                using (SqlConnection sqlconn = new SqlConnection())
                {
                    sqlconn.ConnectionString = MysqlString;
                    SqlCommand cmd = new SqlCommand("user_entry", sqlconn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("userid", name);
                    cmd.Parameters.AddWithValue("pass", pass);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("usertype", user.ToLower());
                    sqlconn.Open();
                    int n = cmd.ExecuteNonQuery();
                    if (n != 0)
                    {
                        new CommonMethods().insert_log(email, DateTime.Now.ToString(), "Registered", user);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "User Registrered", "alert('" + name.Trim() + " :Registered Successfully');window.location='../Default.aspx';", true);
                    }
                    cmd.Dispose();
                    sqlconn.Close();
                }
            }
            catch (SqlException se)
            {
                if (se.Number == 2627)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Cannot Register", "alert('" + Email.Text.Trim() + " :User Already Exists');", true);
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Cannot Register", "alert('Something went wrong in Database. TRY AFTER SOMETIME.');", true);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Cannot Register", "alert('Something went wrong. TRY AFTER SOMETIME.');", true);
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
            using (SqlConnection sqlconn = new SqlConnection())
            {
                sqlconn.ConnectionString = MysqlString;
                SqlCommand cmd = new SqlCommand("staff_check", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("staff_id", TextBox1.Text);
                sqlconn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    TextBox1.BorderColor = Color.Green;
                    reader.Read();
                    UserName.Text = reader[0].ToString();
                }
                else
                {
                    TextBox1.BorderColor = Color.Red;
                    UserName.Text = "";
                }
            }
        }
        protected void usertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserName.Text = "";
            string selected = usertype.SelectedItem.Text;
            if (selected == "User")
            {
                RequiredFieldValidator2.Visible = false;
                UserName.ReadOnly = false;
                Label1.Visible = false;
                TextBox1.Visible = false;
            }
            else if (selected == "Admin")
            {
                RequiredFieldValidator2.Visible = true;
                TextBox1.BorderWidth = 1;
                TextBox1.BorderColor = Color.Red;
                UserName.ReadOnly = true;
                Label1.Visible = true;
                TextBox1.Visible = true;
            }
        }
    }
}