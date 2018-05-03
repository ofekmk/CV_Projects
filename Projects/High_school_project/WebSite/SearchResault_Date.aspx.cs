using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using localhost;

public partial class SearchResault_Date : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            //HttpCookie cookie = Request.Cookies["Details"];
            //if (cookie == null)
            //    Response.Redirect("Login.aspx");

            string date = Request.QueryString["date"];
            Service s = new Service();
            DataSet ds = s.SearchMovieByDate(date);
            Session.Add("dsDate", ds);
            gv_searchresault_date.DataSource = ds;
            gv_searchresault_date.DataBind();

        }
    }



    protected void gv_searchresault_date_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Order"))
        {
            int i = int.Parse(e.CommandArgument.ToString());
            DataSet ds = (DataSet)Session["dsDate"];
            string sScreeningID = ds.Tables[0].Rows[i]["ScreeningID"].ToString();
            //int screeningID = int.Parse(sScreeningID);
            Response.Redirect("OrderTicket.aspx?num=" + sScreeningID);
        }
        else
            if (e.CommandName.Equals("MovieDetails"))
            {
                int i = int.Parse(e.CommandArgument.ToString());
                DataSet ds2 = (DataSet)Session["dsDate"];
                string moviedetails = ds2.Tables[0].Rows[i]["MovieID"].ToString();
                Response.Redirect("MovieDetails.aspx?movie=" + moviedetails);
            }


    }
    protected void lbtn_backtosearch_from_date_table_Click(object sender, EventArgs e)
    {
  
        Response.Redirect("Search_Option1.aspx");
}

    
}
