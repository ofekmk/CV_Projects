using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;
using localhost2;
using localhost3;
using localhost4;
using System.Data;

public partial class AdminAddNewMovie : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Service s = new Service();
        DataTable dt = s.Generes();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ListItem li = new ListItem(dt.Rows[i]["Genre"].ToString());
           ddl_genresadmin.Items.Add(li);
        }

    }
    protected void btn_addnewmovie_Click(object sender, EventArgs e)
    {
        string movieid = txt_mnameadmin.Text;
        string director = txt_dnameadmin.Text;
        string mainactor = txt_mainactioradmin.Text;
        int yearfilmed = int.Parse(txt_yearfilmedadmin.Text);
        int length = int.Parse(txt_lengthadmin.Text);
        string genre = ddl_genresadmin.SelectedValue;
        int rate = int.Parse(txt_rateadmin.Text);
        string datestart = txt_datestartadmin.Text;
        string dateend = txt_dateendadmin.Text;
        int agelimit = int.Parse(txt_agelimitadmin.Text);

        ValidationService vs = new ValidationService();
        AdminActions aa = new AdminActions();
        AdminActions aa2 = new AdminActions();
        string msg = vs.CheckMoviesName(movieid);
        //string msg2 = aa.InsertNewMovieDateChecking(movieid);
        if (msg == "Movie's name is already in DataBase")
            Response.Write(msg);
        else
        {
            DateTime dstart = DateTime.Parse(datestart);
            DateTime dend = DateTime.Parse(dateend);
            if (dend < dstart)
                Response.Write("Date End Can't Be Earlier Than Date Start");
            else
            {


                //if (msg2 != "ok")
                //    Response.Write(msg2);
                //else
                
                    int num = aa2.InsertNewMovie(movieid, director, mainactor, yearfilmed, length, genre, rate, datestart, dateend, agelimit);
                    if (num == 1)
                        Response.Write("Movie added");
                    else
                        Response.Write("Movie Was Not Added");

                

            }


        }
    }
    protected void lbtn_returntoadmindefaultfromaddnewmovie_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminDefault.aspx");
    }
}
