using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;
using localhost2;
using localhost3;
using localhost4;

public partial class AdminAddNewCinema : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
       

    }



    protected void  lbtn_backtoadmindefaultfromaddnewcinema_Click(object sender, EventArgs e)
{
        Response.Redirect("AdminDefault.aspx");
}


protected void  btn_addnewcinema_Click(object sender, EventArgs e)
{
     string genre = txt_genreaddnewcinema.Text;
     int seats = int.Parse(txt_seats_addnewcinema.Text);
     string kind = rbl_kind_addnewcinema.Text;
     AdminActions ac = new AdminActions();
     int msg = ac.InsertNewCinema(genre,seats,kind);
     if (msg ==1)
     Response.Redirect("AdminDefault.aspx");
     else
         Response.Write("Error");
}



}
