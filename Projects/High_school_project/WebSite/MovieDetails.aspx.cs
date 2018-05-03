using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class MovieDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string movieName = Request.QueryString["movie"];
                Service s = new Service();

                lbl_MovieName.Text = movieName;

                int length = s.MovieLength(movieName);
                lbl_length.Text = length.ToString();

                int yearfilmed = s.MovieYearFilmed(movieName);
                lbl_yeafilmed.Text = yearfilmed.ToString();

                string director = s.MovieDirector(movieName);
                lbl_director.Text = director;

                string actor = s.MovieMainActor(movieName);
                lbl_actor.Text = actor;

                string genre = s.MovieGenre(movieName);
                lbl_genre.Text = genre;

                int agelimit = s.MovieAgeLimit(movieName);
                lbl_agelimit.Text = agelimit.ToString();

                double rate = s.MovieRate(movieName);
                string srate = rate.ToString();
                lbl_moviesratedb.Text = srate;

                


                img_movie.ImageUrl = "~/images/" + movieName + ".jpg";
            }
            catch (Exception ex)
            {
                Response.Write("Must choose a movie");
            }
           
            
        } 

    }
    protected void lbtn_hp_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void lbtn_search1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Search_Option1.aspx");
    }
}
