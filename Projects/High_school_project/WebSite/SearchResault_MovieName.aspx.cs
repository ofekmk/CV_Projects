using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;


public partial class SearchResault_Option1_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //string genre = Request.QueryString["genre"];
            //Service s2 = new Service();
            //DataTable dt = s2.SearchMovieByGenre(genre);
            //gv_searchresault_genre_option1.DataSource = dt;
            //gv_searchresault_genre_option1.DataBind();

        
            //HttpCookie cookie = Request.Cookies["Details"];
            //if (cookie == null)
            //    Response.Redirect("Login.aspx");


               string movieID = Request.QueryString["movieID"];
               Service s = new Service();
               DataSet ds = s.SearchMovieByMoviesName(movieID);
               Session.Add("dsMovie", ds);
               gv_searchresault_moviename.DataSource = ds;
               gv_searchresault_moviename.DataBind();

          
        }
    }
  


    protected void  lbtn_backtosearchoption1_Click(object sender, EventArgs e)
{
        Response.Redirect("Search_Option1.aspx");
}





   
    protected void gv_searchresault_moviename_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         
    
        if (e.CommandName.Equals("Order"))
        {
            int i = int.Parse(e.CommandArgument.ToString());
            DataSet ds = (DataSet)Session["dsMovie"];
            string sScreeningID = ds.Tables[0].Rows[i]["ScreeningID"].ToString();
            //int screeningID = int.Parse(sScreeningID);
            Response.Redirect("OrderTicket.aspx?num=" + sScreeningID);
        }
        else
            if (e.CommandName.Equals("MovieDetails"))
            {
                int i = int.Parse(e.CommandArgument.ToString());
                DataSet ds2 = (DataSet)Session["dsMovie"];
                string moviedetails = ds2.Tables[0].Rows[i]["MovieID"].ToString();
                Response.Redirect("MovieDetails.aspx?movie=" + moviedetails);
            }



    }
}
