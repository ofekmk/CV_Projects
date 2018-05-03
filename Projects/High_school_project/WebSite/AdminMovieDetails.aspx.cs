using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using localhost;
using localhost2;
using localhost3;
using localhost4;

public partial class AdminMovieDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {



            HttpCookie cookie = Request.Cookies["Details"];
            if (cookie == null)
                Response.Redirect("Login.aspx");
            AdminActions ac = new AdminActions();
            DataSet ds = ac.Details();
            Session.Add("dsMovieDetails", ds);
            gv_details.DataSource = ds;
            gv_details.DataBind();
        }
    }







    protected void lbtn_backtoadmindfrommdetails_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminDefault.aspx");
    }
}
