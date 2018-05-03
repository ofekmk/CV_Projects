using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
         HttpCookie cookie = Request.Cookies["Details"];
         if (cookie != null)
         {
             MultiViewloghompage.SetActiveView(viewlogout);
             MultiViewloghompage.Visible = true;

             if (cookie["Password"].Equals("admin123"))
             {
                 MultiView1.SetActiveView(View_admin);
                 MultiView1.Visible = true;
             }
         }
         if (cookie == null)
         {
             MultiViewloghompage.SetActiveView(viewloginhompage);
             MultiViewloghompage.Visible = true;
         }
        }
    }
    //protected void lbtn_register_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Register.aspx");
    //}
    protected void lbtn_login_Click(object sender, EventArgs e)
    {

        Response.Redirect("Login.aspx");
    }
    //protected void lbtn_search_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Search_Option1.aspx");
    //}
    //protected void lbtn_privateprofile_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("PrivateProfile.aspx");

    //}
    protected void lbtn_gotoadmindefalut_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminDefault.aspx");
    }

    protected void lbtn_logouthomepage_Click(object sender, EventArgs e)
    {
        string[] cookies = Request.Cookies.AllKeys;
        foreach (string cookie in cookies)
        {

            Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-100);
        }
        Response.Redirect("Default.aspx");

    }
}
