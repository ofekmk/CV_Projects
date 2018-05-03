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


public partial class AdminDeleteMovies : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        Service s = new Service();
        DataSet ds = s.AllMoviesTable();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ListItem li = new ListItem(ds.Tables[0].Rows[i]["MovieID"].ToString());
            ddl_moviestoupdate.Items.Add(li);
        }
        }

    }
    //protected void ddl_moviestoupdate_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string movie = ddl_moviestoupdate.SelectedValue;
    //    Response.Redirect("AdminDeleteMovies.aspx?movieID="+movie);
       


    //}
    protected void btn_updatedateend_Click(object sender, EventArgs e)
    {
        string movie = ddl_moviestoupdate.SelectedValue;
        AdminActions aa = new AdminActions();
        string msg = aa.UpdateDateEndOfMovieAndDeleteEmptyScreenings(movie, txt_newdateend.Text);
        Response.Write(msg);
    }
    //    if (msg2 == 1)

    //        Response.Redirect("AdminDefault.aspx?msg="+msg1);
    //    else
    //        Response.Write("Update Failed");
    //}





    protected void btn_deletemovies_Click(object sender, EventArgs e)
    {

        AdminActions act = new AdminActions();
        int msg = act.DeleteOldMovies();
        string msg2 = msg.ToString();
        Response.Write(msg + "Movies Deleted");
        

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminDefault.aspx");
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Redirect("ScreeningOfMovie.aspx");
    }
}
