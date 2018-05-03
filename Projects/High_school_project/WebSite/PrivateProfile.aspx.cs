using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using localhost2;

public partial class PrivateProfile : System.Web.UI.Page
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
                int id = int.Parse(sID);
                WS_UserProfile up = new WS_UserProfile();


                int userid = up.RetrieveUserID(id);
                lbl_useridprivateprofile2.Text = userid.ToString();

                string firstname = up.RetrieveFirstName(id);
                lbl_fnameprivateprofile2.Text = firstname;

                string lname = up.RetrieveFLastName(id);
                lbl_lnameprivateprofile2.Text = lname;

                int age = up.RetrieveAge(id);
                lbl_ageprivateprofile2.Text = age.ToString();

                string country = up.RetrieveCountry(id);
                lbl_countryprivateprofile2.Text = country;

                string gender = up.RetrieveGender(id);
                lbl_genderprivateprofile2.Text = gender;

                string email = up.RetrieveEmail(id);
                lbl_emailprivateprofile2.Text = email;

            }
        }
    }








    protected void lbtn_UpdateInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdatePersonalInformation.aspx");


    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangePassword.aspx");
    }
    protected void btn_userinvitations_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserInvitationDetails.aspx");
    }
  
}
