using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected string MysqlString = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void clear_all()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            Label14.Text = "";
            Label12.Text = "";
            Label6.Text = "";
            Label8.Text = "";
            Label19.Text = "";
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            clear_all();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = true;
            Panel3.Visible = false;
            Panel4.Visible = false;
            clear_all();
            loadbook();
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = true;
            Panel4.Visible = false;
            clear_all();
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = true;
            clear_all();
            fines_books(Session["useremail"].ToString().Trim());
        }

        protected void search_book(object sender, EventArgs e)
        {
            Button1.Enabled = false;
            Button1.BorderColor = Color.Red;
            TextBox3.BorderColor = Color.Red;
            int bid = Convert.ToInt32(TextBox3.Text);
            MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
            using (SqlConnection sqlconn = new SqlConnection())
            {
                sqlconn.ConnectionString = MysqlString;
                SqlCommand cmd = new SqlCommand("selected_book_id", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("book_id", bid);
                sqlconn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    TextBox3.BorderColor = Color.Green;
                    reader.Read();
                    TextBox1.Text = reader[0].ToString();
                    TextBox2.Text = reader[1].ToString();
                    Button1.Enabled = true;
                    Button1.BorderColor = Color.Green;
                }
                else
                {
                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    TextBox3.BorderColor = Color.Red;
                }
                reader.Close();
                cmd.Dispose();
                sqlconn.Close();
            }
        }
        protected void add_book(object sender, EventArgs e)
        {
            string user_email = Session["useremail"].ToString();
            int bid = Convert.ToInt32(TextBox3.Text);
            MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
            using (SqlConnection sqlconn = new SqlConnection())
            {
                sqlconn.ConnectionString = MysqlString;
                SqlCommand cmd = new SqlCommand("check_book_lend", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("email", user_email);
                cmd.Parameters.AddWithValue("book_id", bid);
                sqlconn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Checking Book Pending", "alert('" + bid + " :Already lended or outstanding fine.');", true);
                else
                {
                    DateTime time=DateTime.Now;
                    string lenddate=time.ToString();
                    //string duedate=time.AddMonths(2).ToString();
                    string duedate = "1/8/2019 1:56:19 PM";
                    int n = new CommonMethods().book_lending(user_email, lenddate, duedate, bid);
                    if (n != 0)
                    {
                        new CommonMethods().insert_log(user_email, lenddate, "Book Lend", "user", bid);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Lended Book", "alert('" + bid + " : Book Lended Sucessfully.');window.location='Func.aspx';", true);
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Cannot Lend Book", "alert('Something went Wrong.');", true);
                }
                reader.Close();
                cmd.Dispose();
                sqlconn.Close();
            }
        }

        protected void loadbook()
        {
            Button2.Enabled = false;
            MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
            using (SqlConnection sqlconn = new SqlConnection())
            {
                sqlconn.ConnectionString = MysqlString;
                SqlCommand cmd = new SqlCommand("lended_books", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("email", Session["useremail"].ToString());
                sqlconn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DropDownList1.DataSource = reader;
                    DropDownList1.DataTextField = "book_id";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("Select Book ID"));
                    Button2.Enabled = true;
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "No Book", "alert('No Books Lended.');window.location='Func.aspx';", true);

                reader.Close();
                cmd.Dispose();
                sqlconn.Close();
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedItem.Text == "Select Book ID")
            {
                Label14.Text = "";
                Label12.Text = "";
                Label6.Text = "";
                Label8.Text = "";
                Label19.Text = "";
            }
            else
            {
                int book_id = Convert.ToInt32(DropDownList1.SelectedItem.Text);
                MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
                using (SqlConnection sqlconn = new SqlConnection())
                {
                    sqlconn.ConnectionString = MysqlString;
                    SqlCommand cmd = new SqlCommand("lended_book_details", sqlconn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("book_id", book_id);
                    cmd.Parameters.AddWithValue("email", Session["useremail"].ToString());
                    sqlconn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    Label14.Text = reader[0].ToString().Trim();
                    Label12.Text = reader[1].ToString().Trim();
                    Label6.Text = reader[2].ToString().Trim();
                    Label8.Text = reader[3].ToString().Trim();
                    Label19.Text = reader[4].ToString().Trim();

                    reader.Close();
                    cmd.Dispose();
                    sqlconn.Close();
                }
            }
        }
        protected void remove_book(object sender, EventArgs e)
        {
            int book_id = Convert.ToInt32(DropDownList1.SelectedItem.Text);
            MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
            using (SqlConnection sqlconn = new SqlConnection())
            {
                sqlconn.ConnectionString = MysqlString;
                SqlCommand cmd = new SqlCommand("return_book", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("book_id", book_id);
                cmd.Parameters.AddWithValue("email", Session["useremail"].ToString());
                string time = DateTime.Now.ToString();
                cmd.Parameters.AddWithValue("return_time", time);

                SqlParameter parm=new SqlParameter("@fine_check",SqlDbType.Int);
                parm.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parm);

                sqlconn.Open();
                cmd.ExecuteNonQuery();
                int fine_check = Convert.ToInt32(parm.Value);
                if (fine_check == 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Book Return", "alert('FINE ALERT:as you are returning the book after its due date.Pay as Soon as Possible.');window.location='Func.aspx';", true);
                    new CommonMethods().insert_log(Session["useremail"].ToString(), time, "Book Return", "user", book_id);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Book Return", "alert('Book Returned Successfully.');window.location='Func.aspx';", true);
                    new CommonMethods().insert_log(Session["useremail"].ToString(), time, "Book Return", "user",book_id);
                }
                cmd.Dispose();
                sqlconn.Close();
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected=DropDownList2.SelectedItem.Text;
            if (selected == "Select Function")
            {
                GridView1.Visible = false;
            }
            else
            {
                MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
                using (SqlConnection sqlconn = new SqlConnection())
                {
                    sqlconn.ConnectionString = MysqlString;
                    SqlCommand cmd = new SqlCommand("select_user_details", sqlconn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("selection", selected);
                    cmd.Parameters.AddWithValue("email", Session["useremail"].ToString());
                    sqlconn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        GridView1.DataSource = reader;
                        GridView1.DataBind();
                        GridView1.Visible = true;
                        DropDownList2.SelectedIndex = -1;
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "NO Data", "alert('No data to show');", true);

                    reader.Close();
                    cmd.Dispose();
                    sqlconn.Close();
                }
            }
        }
        protected void fines_books(string email)
        {
            MysqlString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString.ToString();
            using (SqlConnection sqlconn = new SqlConnection())
            {
                sqlconn.ConnectionString = MysqlString;
                SqlCommand cmd = new SqlCommand("check_book_lend", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("book_id", 0);
                sqlconn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    GridView2.DataSource = reader;
                    GridView2.DataBind();
                    Panel4.Visible = true;
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "NO Data", "alert('No data to show');", true);

                reader.Close();
                cmd.Dispose();
                sqlconn.Close();
            }
        }

    }
}