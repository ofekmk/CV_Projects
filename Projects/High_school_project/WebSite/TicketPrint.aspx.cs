using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using localhost;

public partial class TicketPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           

            HttpCookie cookie = Request.Cookies["Details"];
            if (cookie == null)
                Response.Redirect("Login.aspx");

            string sScreeningID = Request.QueryString["screening"];
            string sSeat = Request.QueryString["seat"];
            Service s = new Service();
            int screeningID = int.Parse(sScreeningID);
            DataSet ds = s.ScreeningDetails(screeningID);
            DataTable dt = ds.Tables[0];
            lbl_movie_from_db.Text = dt.Rows[0]["MovieID"].ToString();
            lbl_date_from_db.Text = dt.Rows[0]["Date"].ToString();
            DateTime hour = DateTime.Parse(dt.Rows[0]["Date"].ToString());
            string time = hour.ToShortTimeString();
            lbl_time_from_db.Text = time;
            lbl_cinema_from_db.Text = dt.Rows[0]["CinemaID"].ToString();
            lbl_seat_from_db.Text = sSeat;

        }
        
        


    }
    protected void lbtn_homepage_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}
