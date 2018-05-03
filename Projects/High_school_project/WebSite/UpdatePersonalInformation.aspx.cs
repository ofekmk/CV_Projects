using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using localhost;
using localhost2;

public partial class UpdatePersonalInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies["Details"];
            if (cookie == null)
                Response.Redirect("Login.aspx");
            else
            {
                string sID = cookie["UserID"].ToString();      
                lbl_useridupdatepage2.Text = sID;
            }
        }
    }








    protected void btn_updateprofile_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["Details"];
        string sID = cookie["UserID"].ToString();
        int id = int.Parse(sID);
        string fname, lname, country, gender, email,pass;
        int age, updatemsg,credit;

        WS_UserProfile us = new WS_UserProfile();
        Service s = new Service();

        if (txt_fname_updatepage.Text == "")
            fname = us.RetrieveFirstName(id);
        else
            fname = txt_fname_updatepage.Text;

        if (txt_lname_updatepage.Text == "")
            lname = us.RetrieveFLastName(id);
        else
            lname = txt_lname_updatepage.Text;

        if (txt_age_updatepage.Text == "")
  
            age = us.RetrieveAge(id);
            
        else
            age = int.Parse(txt_age_updatepage.Text);

        if (txt_country_updatepage.Text == "")
            country = us.RetrieveCountry(id);
        else
            country = txt_country_updatepage.Text;

        if (txt_gender_updatepage.Text == "")
            gender = us.RetrieveGender(id);
        else
            gender = txt_gender_updatepage.Text;

        if (txt_email_updatepage.Text == "")
            email = us.RetrieveEmail(id);
        else
            email = txt_email_updatepage.Text;

        if (txt_creditcard_updatepage.Text == "")
            credit = us.RetrieveCreditNum(id);
        else
            credit = int.Parse(txt_creditcard_updatepage.Text);
        
    

       
            pass = us.UserPassword(id);
            updatemsg = us.UserProfileUpdate(id, fname, lname, age, country, gender, email, credit, pass);
            if (updatemsg == 1)
                Response.Write("Update Successful.");
            else
                Response.Write("Update failed.");

        }
    protected void lnkbtn_backtouserprofile_Click(object sender, EventArgs e)
    {
        Response.Redirect("PrivateProfile.aspx");
    }
    
}







