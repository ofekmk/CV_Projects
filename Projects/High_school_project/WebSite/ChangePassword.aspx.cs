using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using localhost2;

public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies["Details"];
            if (cookie == null)
                Response.Redirect("Login.aspx");
        }

    }

    protected void lnkbtnbacktopproilefrompasschange_Click(object sender, EventArgs e)
    {
        Response.Redirect("PrivateProfile.aspx");
    }

    protected void btn_changepass_Click(object sender, EventArgs e)
    {
        string msg;
        HttpCookie cookie = Request.Cookies["Details"];
        string sID = cookie["UserID"].ToString();
        int id = int.Parse(sID);
        WS_UserProfile us = new WS_UserProfile();
        string oldpass = us.UserPassword(id);
        string newpass1 = txt_newpass.Text;
        string newpass2 = txt_passconfirmation.Text;
        if (txt_oldpass.Text == oldpass)
        {
            if (newpass1 == newpass2)
            {
                us.NewPassword(id,newpass1);
                msg = "Password changed";
            }
            else
                msg = " Change Failed. Wrong password confirmation";

        }
        else
            msg = "Change failed. old password is wrong";

        Response.Write(msg);


    }






    
}