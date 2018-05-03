using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;
using localhost2;
using localhost3;
using localhost4;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            HttpCookie cookie = Request.Cookies["Details"];
            if (cookie != null)
                Response.Redirect("Default.aspx");


            DateTime today = DateTime.Now;
            int datetoday = today.Year;
            int dateavailable = datetoday - 16;
            for (int i = 1900; i < dateavailable; i++)
            {
                string si = i.ToString();
                ListItem li = new ListItem(si);
                ddl_years.Items.Add(li);
            }

            String msg = Request.QueryString["msg"];
            Response.Write(msg);


        }
        
    }


    protected void txt_fname_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txt_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
     int userID = int.Parse(txt_userID.Text);
     string fName = txt_fname.Text;
     string Lname = txt_lname.Text;
     string country = txt_country.Text;
     string gender = rbl_gender.Text;
     string email = txt_email.Text;
     int creditCard = int.Parse(txt_creditcard.Text);
     string password = txt_password.Text;
     string password2 = txt_password2.Text;
    
    
     string sage = ddl_years.SelectedValue;
     int Age = int.Parse(sage);

     Service reg = new Service();
     ValidationService vs = new ValidationService();
     if (password == password2)
     {
         string msg = vs.CheckUserIDRegister(userID);
         if (msg == "Available")
         {
             reg.Register(userID, fName, Lname, Age, country, gender, email, creditCard, password);
             Response.Redirect("Default.aspx");
         }
         else
             Response.Write("UserID Already Taken");
     }
     else
         Response.Write("Error - passwords do no match");
    }




    protected void lbtn_hp1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}
