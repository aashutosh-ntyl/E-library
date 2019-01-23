using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void login_check(object sender, EventArgs e)
        {
            try
            {
                string email = Email.Text.Trim();
                string pass=Password.Text;
                string MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
                using (SqlConnection sqlconn = new SqlConnection())
                {
                    sqlconn.ConnectionString = MysqlString;
                    SqlCommand cmd = new SqlCommand("login_check", sqlconn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("email",email);
                    sqlconn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            if (pass == reader[2].ToString())
                            {
                                Session["username"]=reader[0].ToString();
                                Session["usertype"]=reader[1].ToString();
                                Session["useremail"] = email;
                                new CommonMethods().insert_log(email, DateTime.Now.ToString(), "Logged In", Session["usertype"].ToString());
                                if(Session["usertype"].ToString().Trim()=="user")
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "User Found", "alert('Welcome : " + Session["username"].ToString() + "');window.location='../Func.aspx'", true);
                                else if(Session["usertype"].ToString().Trim()=="admin")
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "User Found", "alert('Welcome : " + Session["username"].ToString() + "');window.location='../FuncAdmin.aspx'", true);
                            }
                            else
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Incorrect Information", "alert('" + email.Trim() + " :Password Incorrect');", true);
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "User Not Found", "alert('" + email.Trim() + " :No such user');", true);
                        }
                    cmd.Dispose();
                    reader.Close();
                    reader.Dispose();
                    sqlconn.Close();
                }
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Incorrect Information", "alert('Something Went Wrong. TRY AGAIN.');", true);
            }
        }

    }

}