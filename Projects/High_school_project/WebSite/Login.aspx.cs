using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["Details"];
        if (cookie != null)
            Response.Redirect("Default.aspx");


    }
    

    protected void btn_login_Click(object sender, EventArgs e)
    {
        int id = int.Parse(txt_ID.Text);
        string password = txt_password.Text;
        Service login = new Service();
        string msg = login.Login(id, password);
        if (msg.Equals("Login Success"))
        {

            HttpCookie cookie = new HttpCookie("Details");
            cookie["UserID"] = txt_ID.Text;
            cookie["Password"] = txt_password.Text;
            DateTime dt = DateTime.Now;
            TimeSpan ts = new TimeSpan(0, 0, 50, 0);
            cookie.Expires = dt.Add(ts);
            Response.Cookies.Add(cookie);


            //HttpCookie cookie = Request.Cookies["Details"];
            //if (CookieParameter == null)
            //    Response.Redirect("login.aspx");
            //else
            //    Label2.Text = cookie["UserID"]




            //HttpCookie cookie = Request.Cookies["Details"];
            //cookie.Expires = DateTime.Now.AddHours(-1);
            //Response.Cookies.Add(cookie);


            Response.Redirect("Default.aspx?msg=" + msg);
            //Label1.Text = msg + "please try again";

        }

        else
            lbl_error_msg.Text = msg;
            //Response.Redirect("Register.aspx?msg=" + msg);








       
    }




    protected void lb_register_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }
}
