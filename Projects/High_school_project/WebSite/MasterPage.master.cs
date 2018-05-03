using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using localhost2;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies["Details"];
            if (cookie != null)
            {
                MultiViewlog.SetActiveView(viewlogout);
                MultiViewlog.Visible = true;

                string sID = cookie["UserID"].ToString();
                int id = int.Parse(sID);

                WS_UserProfile us = new WS_UserProfile();
                string fname = us.RetrieveFirstName(id);
                lbl_mpage_fname.Text = fname;
                string lname = us.RetrieveFLastName(id);
                lbl_mpage_lname.Text = lname;
                DateTime dt = DateTime.Now;
                string sdt = dt.ToString();
                lbl_mpage_datenow.Text = sdt;
            }
            else
            {
                MultiViewlog.SetActiveView(view_login);
                MultiViewlog.Visible = true;
            }







        }
    }

    protected void lbtn_homepagefromms_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");

    }
    protected void lbtn_pprofilefromms_Click(object sender, EventArgs e)
    {
        Response.Redirect("PrivateProfile.aspx");
    }
    protected void lbtn_smoviefromms_Click(object sender, EventArgs e)
    {
        Response.Redirect("Search_Option1.aspx");
    }
protected void  lbtn_loginmp_Click(object sender, EventArgs e)
{
    Response.Redirect("Login.aspx");
}

protected void lbtn_logout_Click(object sender, EventArgs e)
{
      string[] cookies = Request.Cookies.AllKeys;
      foreach (string cookie in cookies)
      {

          Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-100);
      }
      Response.Redirect("Default.aspx");

}
}
