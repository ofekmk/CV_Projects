using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using localhost;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

            //HttpCookie cookie = Request.Cookies["Details"];
            //if (cookie == null)
            //    Response.Redirect("Login.aspx");
     

            Service s = new Service();
            DataSet ds = s.AllMoviesTable();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ListItem li = new ListItem(ds.Tables[0].Rows[i]["MovieID"].ToString());
                ddl_MoviesNames.Items.Add(li);
            }

            DataSet ds2 = s.AllDatesTable();
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                ListItem li2 = new ListItem(ds2.Tables[0].Rows[i]["Date"].ToString());
                ddl_ScreeningDates.Items.Add(li2);
            }

            DataSet ds3 = s.AllGenresTable();
            for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
            {
                ListItem li3 = new ListItem(ds3.Tables[0].Rows[i]["Genre"].ToString());
                ddl_Genres.Items.Add(li3);
            }

        }
        



    }
  
  
    protected void ddl_MoviesNames_SelectedIndexChanged(object sender, EventArgs e)
    {

        string moviePicked = ddl_MoviesNames.SelectedValue;
        Response.Redirect("SearchResault_MovieName.aspx?movieID=" + moviePicked);

    }


    //protected void ddl_ScreeningDates_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string datePicked = ddl_ScreeningDates.SelectedValue;
    //    Response.Redirect("SearchResault_Date.aspx?date=" + datePicked);

    //}


   

    //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //string moviePicked = ddl_MoviesNames.SelectedValue;
    //    //Service s2 = new Service();
    //    //DataSet ds5 = s2.SearchMovieByMoviesName(moviePicked);
    //    //gv_search_resault.DataSource = ds5;
    //    //gv_search_resault.DataBind();


    //}

    protected void ddl_ScreeningDates_SelectedIndexChanged(object sender, EventArgs e)
    {
        string datePicked = ddl_ScreeningDates.SelectedValue;
        DateTime dt = DateTime.Parse(datePicked);
        string dt2 = dt.Month + "/" + dt.Day + "/" + dt.Year + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second;
        datePicked = dt2;
        Response.Redirect("SearchResault_Date.aspx?date=" + datePicked);

    }

    protected void ddl_Genres_SelectedIndexChanged(object sender, EventArgs e)
    {
        string genrePicked = ddl_Genres.SelectedValue;
        Response.Redirect("SearchResault_Genre.aspx?genre=" + genrePicked);
    }


    protected void lbtn_hp2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}
