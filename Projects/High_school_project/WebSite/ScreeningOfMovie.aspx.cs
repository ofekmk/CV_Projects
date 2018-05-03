using System;
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
public partial class ScreeningOfMovie : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            HttpCookie cookie = Request.Cookies["Details"];
            if (cookie == null)
                Response.Redirect("Login.aspx");

            string movie = Request.QueryString["movieid"];
            lb_movie.Text = movie;
            lb_movie.PostBackUrl = "MovieDetails.aspx?movie="+movie;
            Service s = new Service();
            AdminActions aa = new AdminActions();
            DataSet ds = s.SearchMovieByMoviesName(movie);
            DataTable dt = ds.Tables[0];
            dt.Columns.Add("InvitationsNum");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int screening = int.Parse(dt.Rows[i]["ScreeningID"].ToString());
                int num = aa.NumOfInvitationsForScreeningID(screening);
                dt.Rows[i]["invitationsNum"] = num;
            }
            Session.Add("dsScreenings", ds);
            gv_screeningsID.DataSource = dt;
            gv_screeningsID.DataBind();
        }






    }


    protected void gv_screeningsID_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DeleteScreening"))
        {
            HttpCookie cookie = Request.Cookies["Details"];
            if (cookie == null)
                Response.Redirect("Login.aspx");
            else
            {
                DataSet ds = (DataSet)Session["dsScreenings"];
                DataTable dt = ds.Tables[0];
                int i = int.Parse(e.CommandArgument.ToString());
                int screening = int.Parse(dt.Rows[i]["ScreeningID"].ToString());
                AdminActions aa = new AdminActions();
                int msg = aa.DeleteScreeningID(screening);
                if (msg == 1)
                {
                    dt.Rows.RemoveAt(i);
                    Response.Write("Screening Deleted");
                    gv_screeningsID.DataSource = dt;
                    gv_screeningsID.DataBind();
                    Session["dsScreenings"] = dt;
                }
                else
                    Response.Write("Screening Was not Deleted");
            }
        }
    }
    
}
