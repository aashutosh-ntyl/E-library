using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1;

namespace WebApplication1
{
    public partial class FuncAdmin : Page
    {
        protected string MysqlString = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void clear_page()
        {
            Panel2.Visible = false;
            TextBox1.Text = "";
            Panel3.Visible = false;
            usertext.Text = "";
            Panel4.Visible = false;
            bookauthor.Text = "";
            bookname.Text = "";
            bookid.Text = "";
            Panel5.Visible = false;
            Panel6.Visible = false;
            Panel7.Visible = false;
            Label6.Visible = false;
            Label7.Visible = false;
            DropDownList1.Visible = false;
            bookid.Visible = false;
            RequiredFieldValidator4.Visible = false;
        }
        protected void func_select(object sender, EventArgs e)
        {
            clear_page();
            string selected = functions.SelectedItem.Value;
            if (selected == "1")
            {
                Panel2.Visible = true;
            }
            else if (selected == "2")
            {
            }
            else if (selected == "3")
            {
                Panel2.Visible = true;
                Panel3.Visible = true;
            }
            else if (selected == "4")
            {
                Panel4.Visible = true;
                Label6.Visible = true;
                bookid.Visible = true;
                RequiredFieldValidator4.Visible = true;
            }
            else if (selected == "5")
            {
                loadbook();
                bookauthor.ReadOnly = true;
                bookname.ReadOnly = true;
                DropDownList1.Visible = true;
                Label7.Visible = true;
                Panel4.Visible = true;
            }
            else if (selected == "6")
            {
                Panel3.Visible = true;
            }
            else if (selected == "7")
            {
                Panel3.Visible = true;
            }
            Panel5.Visible = true;
        }
        protected void loadbook()
        {
            MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
            using (SqlConnection sqlconn = new SqlConnection())
            {
                sqlconn.ConnectionString = MysqlString;
                SqlCommand cmd = new SqlCommand("select_book_id", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlconn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DropDownList1.DataSource = reader;
                    DropDownList1.DataTextField = "bookid";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("Select Book ID"));
                }
                else
                    DropDownList1.Items.Insert(0,new ListItem("No Books in Database"));

                reader.Close();
                cmd.Dispose();
                sqlconn.Close();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (Calendar1.Visible == true)
                Calendar1.Visible = false;
            else
                Calendar1.Visible = true;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            TextBox1.Text = Calendar1.SelectedDate.ToString("M/dd/yyyy");
            Calendar1.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = functions.SelectedItem.Value;
                MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
                using (SqlConnection sqlconn = new SqlConnection())
                {
                    sqlconn.ConnectionString = MysqlString;
                    SqlCommand cmd = null;
                    
                    if (selected == "1")
                    {
                        string date = TextBox1.Text;
                        
                        cmd = new SqlCommand("get_login", sqlconn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("email", Session["useremail"].ToString());
                        cmd.Parameters.AddWithValue("date", date);
                        sqlconn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            GridView1.DataSource = reader;
                            GridView1.DataBind();
                            Panel6.Visible = true;
                        }
                        else
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "User Not Found", "alert('No Log to show.');window.location='FuncAdmin.aspx';", true);

                        reader.Close();
                        Button2.Visible = true;
                    }
                    else if (selected == "2")
                    {
                            cmd = new SqlCommand("get_delete_log", sqlconn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("email", Session["useremail"].ToString());
                            sqlconn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                //reader.Read();
                                GridView1.DataSource = reader;
                                GridView1.DataBind();
                                Panel6.Visible = true;
                            }
                            else
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "User Not Found", "alert('No Log to show.');window.location='FuncAdmin.aspx';", true);

                            reader.Close();
                            Button2.Visible = true;
                    }
                    else if (selected == "3")
                    {
                        string date = TextBox1.Text;
                        string email = usertext.Text.Trim();
                        cmd = new SqlCommand("get_user_login", sqlconn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("email", email);
                        cmd.Parameters.AddWithValue("date", date);
                        sqlconn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            //reader.Read();
                            GridView1.DataSource = reader;
                            GridView1.DataBind();
                            Panel6.Visible = true;
                        }
                        else
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "User Not Found", "alert('No Log to show.');window.location='FuncAdmin.aspx';", true);

                        reader.Close();
                        Button2.Visible = true;
                    }
                    else if (selected == "4")
                    {
                        string bname=bookname.Text;
                        string bauthor=bookauthor.Text;
                        int bid=Convert.ToInt32(bookid.Text);
                        
                        cmd = new SqlCommand("book_insert", sqlconn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("bookuser", Session["useremail"].ToString());
                        cmd.Parameters.AddWithValue("bookname", bname);
                        cmd.Parameters.AddWithValue("bookauthor", bauthor);
                        cmd.Parameters.AddWithValue("bookid", bid);
                        sqlconn.Open();
                        int n = cmd.ExecuteNonQuery();
                        if (n != 0)
                        {
                            new CommonMethods().insert_log(Session["useremail"].ToString(), DateTime.Now.ToString(), "Book Added", Session["usertype"].ToString(),bid);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Book Addded", "alert('" + bid + " :Book Added Successfully.');window.location='FuncAdmin.aspx';", true);
                        }
                        else
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Deleting Error", "alert('Something Went Wrong. TRY AGAIN.');window.location='FuncAdmin.aspx';", true);

                    }
                    else if (selected == "5")
                    {
                        string bname = bookname.Text;
                        int bid = Convert.ToInt32(DropDownList1.SelectedItem.Text);

                        cmd = new SqlCommand("book_delete", sqlconn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("bookname", bname);
                        cmd.Parameters.AddWithValue("bookid", bid);
                        sqlconn.Open();
                        int n = cmd.ExecuteNonQuery();
                        if (n != 0)
                        {
                            new CommonMethods().insert_log(Session["useremail"].ToString(), DateTime.Now.ToString(), "Book Removed", Session["usertype"].ToString(), bid);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Book Addded", "alert('" + bid + " :Book Removed Successfully.');window.location='FuncAdmin.aspx';", true);
                        }
                        else
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Deleting Error", "alert('Something Went Wrong. TRY AGAIN.');window.location='FuncAdmin.aspx';", true);

                    }
                    else if (selected == "6")
                    {
                        string user_email = usertext.Text;

                        cmd = new SqlCommand("check_book_lend", sqlconn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("email", user_email);
                        cmd.Parameters.AddWithValue("book_id", 0);
                        sqlconn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Checking Book Pending", "alert('" + user_email.Trim() + " :user has some pending BOOKS or FINE. CLEAR DUES TO DELETE YOUR ACCOUNT.');", true);
                            GridView1.DataSource = reader;
                            GridView1.DataBind();
                            Panel6.Visible = true;
                        }
                        else
                        {
                            cmd = null;
                            cmd = new SqlCommand("delete_user", sqlconn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("email", user_email);
                            int n = cmd.ExecuteNonQuery();
                            if (n != 0)
                            {
                                new CommonMethods().insert_log(Session["useremail"].ToString(), DateTime.Now.ToString(), "User Removed", Session["usertype"].ToString(), user_email);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "User Removed", "alert('" + user_email.Trim() + " :User Removed Successfully.');window.location='FuncAdmin.aspx';", true);
                            }
                            else
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Deleting Error", "alert('Something Went Wrong. TRY AGAIN.');window.location='FuncAdmin.aspx';", true);
                        }
                        reader.Close();
                    }
                    else if (selected == "7")
                    {
                        string user_email = usertext.Text;

                        cmd = new SqlCommand("fine_checking", sqlconn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("email", user_email);
                        sqlconn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            GridView2.DataSource = reader;
                            GridView2.DataBind();
                            Panel7.Visible = true;
                        }
                        else
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Pendancy Check", "alert('No Record to Show');", true);
                    }

                    cmd.Dispose();
                    sqlconn.Close();
                    Button2.Visible = true;
                }
            }
            catch(Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Book Addded", "alert('Something Went Wrong. TRY AGAIN');window.location='FuncAdmin.aspx';", true);
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bid = Convert.ToInt32(DropDownList1.SelectedItem.Text);

            string MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
            using (SqlConnection sqlconn = new SqlConnection())
            {
                sqlconn.ConnectionString = MysqlString;
                SqlCommand cmd = new SqlCommand("selected_book_id", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("book_id", bid);
                sqlconn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                bookname.Text = reader[0].ToString();
                bookauthor.Text = reader[1].ToString();

                reader.Close();
                cmd.Dispose();
                sqlconn.Close();
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "finecheckout")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView2.Rows[rowIndex];
                string user_email = usertext.Text;

                if(row.Cells[7].Text.ToLower()=="paid")
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Fine Submitted", "alert('Already Paid');", true);
                else if(row.Cells[7].Text=="")
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Fine Submitted", "alert('No Fine to Collect');", true);
                else if (row.Cells[7].Text.ToLower()== "not paid")
                {
                    string MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
                    using (SqlConnection sqlconn = new SqlConnection())
                    {
                        sqlconn.ConnectionString = MysqlString;
                        SqlCommand cmd = new SqlCommand("pay_fine", sqlconn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("user_email", user_email);
                        cmd.Parameters.AddWithValue("admin_email", Session["useremail"].ToString());
                        cmd.Parameters.AddWithValue("lend_date", row.Cells[3].Text);
                        cmd.Parameters.AddWithValue("book_id", row.Cells[1].Text);
                        string time = DateTime.Now.ToString();
                        cmd.Parameters.AddWithValue("pay_date", time);
                        sqlconn.Open();
                        int n = cmd.ExecuteNonQuery();
                        if (n != 0)
                        {
                            new CommonMethods().insert_log(Session["useremail"].ToString(), time, "Fine Collected", Session["usertype"].ToString(), Convert.ToInt32(row.Cells[1].Text));
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Fine Submitted", "alert('" + row.Cells[1].Text + " :Fine Submitted.');", true);
                            Panel7.Visible = false;
                        }
                        else
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Submitting Error", "alert('Something Went Wrong. TRY AGAIN.');window.location='FuncAdmin.aspx';", true);

                        cmd.Dispose();
                        sqlconn.Close();
                    }
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Panel6.Visible == true)
                Panel6.Visible = false;
            else if (Panel7.Visible == true)
                Panel7.Visible = false;

            Button2.Visible = false;
        }

        protected void check_user(string user_email)
        {

        }
    }
}