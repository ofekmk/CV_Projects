using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        lbl_msgadminactions.Text = Request.QueryString["msg"];

    }
   
    

    protected void lbtn_setmovieprice_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminSetPrice.aspx");
    }
    protected void lbn_add_new_movie_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminAddNewMovie.aspx");
    }
    protected void lbtn_deletemovie_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminDeleteMovies.aspx");
    }
    protected void lbtn_addnewscreening_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminAddNewScreening.aspx");
    }
    protected void lbtn_add_new_cinema_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminAddNewCinema.aspx");
    }
    protected void lbtn_moviedetails_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMovieDetails.aspx");
    }
    protected void lbtn_deleteusers_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminDeleteUsers.aspx");
    }
}
