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

public partial class AdminAddNewScreening : System.Web.UI.Page
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
                ddl_movienewscreening.Items.Add(li);
            }


            AdminActions ac = new AdminActions();
            DataTable dt = ac.Cinema();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem li = new ListItem(dt.Rows[i]["CinemaID"].ToString());
               ddl_cinemanewscreening.Items.Add(li);
            }
        }

    }
    protected void btn_addnewscreening_Click(object sender, EventArgs e)
    {
        string date = txt_datenewscreening.Text;
        int price = int.Parse(txt_pricenewscreening.Text);
        string movie = ddl_movienewscreening.SelectedValue;
        string scinema = ddl_cinemanewscreening.SelectedValue;
        int cinema = int.Parse(scinema);
        AdminActions ac = new AdminActions();
        Service s = new Service();
        DateTime start = DateTime.Parse(s.MovieDateStart(movie));
        DateTime end = DateTime.Parse(s.MovieDateEnd(movie));
        DateTime d = DateTime.Parse(date);
        DateTime today = DateTime.Now;
        if (d > end)
            Response.Write("Date Can't Be After Date End");
        else
            if (d < today)
            {
                Response.Write("Date Can't Be In The Past");
            }
            else
            {
                ac.InsertNewScreening(date, cinema, price, movie);
                Response.Write("New Screening Added");
            }
            
           
      

       



    }
    protected void lbtn_backtoadminsdefault_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminDefault.aspx");
    }
    protected void lbtn_backtoadminsdefault_Click1(object sender, EventArgs e)
    {
        Response.Redirect("AdminDefault.aspx");
    }
}
