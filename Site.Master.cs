using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class SiteMaster : MasterPage
    {
    //    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    //    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    //    private string _antiXsrfTokenValue;

    //    protected void Page_Init(object sender, EventArgs e)
    //    {


    //        // The code below helps to protect against XSRF attacks
    //        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
    //        Guid requestCookieGuidValue;
    //        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
    //        {
    //            // Use the Anti-XSRF token from the cookie
    //            _antiXsrfTokenValue = requestCookie.Value;
    //            Page.ViewStateUserKey = _antiXsrfTokenValue;
    //        }
    //        else
    //        {
    //            // Generate a new Anti-XSRF token and save to the cookie
    //            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
    //            Page.ViewStateUserKey = _antiXsrfTokenValue;

    //            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
    //            {
    //                HttpOnly = true,
    //                Value = _antiXsrfTokenValue
    //            };
    //            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
    //            {
    //                responseCookie.Secure = true;
    //            }
    //            Response.Cookies.Set(responseCookie);
    //        }

    //        Page.PreLoad += master_Page_PreLoad;
    //    }

    //    protected void master_Page_PreLoad(object sender, EventArgs e)
    //    {
    //        if (!IsPostBack)
    //        {
    //            // Set Anti-XSRF token
    //            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
    //            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
    //        }
    //        else
    //        {
    //            // Validate the Anti-XSRF token
    //            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
    //                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
    //            {
    //                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
    //            }
    //        }
    //    }

        protected void Page_Load(object sender, EventArgs e)
        {
            fine_updation();

            if (Session["username"].ToString() == "")
            {
                LinkButton1.Visible = false;
                labeluser.Text = "Guest";
            }
            else if (Session["username"].ToString() != "")
            {
                title_link.Disabled = true;
                LinkButton1.Visible = true;
                labeluser.Text = Session["username"].ToString();
            }
        }
        protected void fine_updation()
        {
                string MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
                using (SqlConnection sqlconn = new SqlConnection())
                {
                    sqlconn.ConnectionString = MysqlString;
                    SqlCommand cmd = new SqlCommand("generate_fine", sqlconn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    sqlconn.Close();
                }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            new WebApplication1.CommonMethods().insert_log(Session["useremail"].ToString(), DateTime.Now.ToString(), "Logged Out", Session["usertype"].ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Logout", "alert('Successfully Logged Out.');window.location='Default.aspx'", true);
        }
    }
}