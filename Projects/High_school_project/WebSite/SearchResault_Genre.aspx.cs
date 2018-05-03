using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using localhost;

public partial class SearchResault_Genre : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

          
            //HttpCookie cookie = Request.Cookies["Details"];
            //if (cookie == null)
            //    Response.Redirect("Login.aspx");


            string genre = Request.QueryString["genre"];
            Service s = new Service();
            DataTable dt = s.SearchMovieByGenre(genre);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Session.Add("dsGenre", ds);
            gv_searchresault_genre.DataSource = ds;
            gv_searchresault_genre.DataBind();

        }

    }


    protected void gv_searchresault_genre_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("Order"))
        {
            int i = int.Parse(e.CommandArgument.ToString());
            DataSet ds = (DataSet)Session["dsGenre"];
            string sScreeningID = ds.Tables[0].Rows[i]["ScreeningID"].ToString();
            //int screeningID = int.Parse(sScreeningID);
            Response.Redirect("OrderTicket.aspx?num=" + sScreeningID);
        }
        else
            if (e.CommandName.Equals("MovieDetails"))
            {
                int i = int.Parse(e.CommandArgument.ToString());
                DataSet ds2 = (DataSet)Session["dsGenre"];
                string moviedetails = ds2.Tables[0].Rows[i]["MovieID"].ToString();
                Response.Redirect("MovieDetails.aspx?movie=" + moviedetails);
            }


    }




    protected void lbtn_back_to_search_from_genretable_Click(object sender, EventArgs e)
    {
        Response.Redirect("Search_Option1.aspx");
    }
}
