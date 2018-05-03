using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using SqlProject;


//localhost 2

/// <summary>
/// Summary description for WS_UserProfile
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WS_UserProfile : System.Web.Services.WebService 
{

    [WebMethod]

    public int RetrieveUserID(int UserID)
    {
        int id = SqlProject.Users.RetrieveUserID(UserID);
        return id;
    }

    [WebMethod]

    public string RetrieveFirstName(int UserID)
    {
        string fname = SqlProject.Users.RetrieveFirstName(UserID);
        return fname;
    }

    [WebMethod]

    public string RetrieveFLastName(int UserID)
    {
        string lname = SqlProject.Users.RetrieveFLastName(UserID);
        return lname;
    }

    [WebMethod]

    public int RetrieveAge(int UserID)
    {
        int age = SqlProject.Users.RetrieveAge(UserID);
        return age;
    }

    [WebMethod]

    public string RetrieveCountry(int UserID)
    {
        string country = SqlProject.Users.RetrieveCountry(UserID);
        return country;
    }

    [WebMethod]

    public string RetrieveGender(int UserID)
    {
        string gender = SqlProject.Users.RetrieveGender(UserID);
        return gender;
    }

    [WebMethod]

    public string RetrieveEmail(int UserID)
    {
        string email = SqlProject.Users.RetrieveEmail(UserID);
        return email;
    }

    [WebMethod]

    public int UserProfileUpdate(int UserID, string Fname, string Lname, int Age, string Country, string Gender, string Email, int CreditCard, string Password)
    {
        int msg = SqlProject.Users.UserProfileUpdate(UserID, Fname, Lname, Age, Country, Gender, Email, CreditCard, Password);
        return msg;
    }

    [WebMethod]

    public string UserPassword(int UserID)
    {
        string pass = SqlProject.Users.UserPassword(UserID);
        return pass;
    }

    [WebMethod]

    public int NewPassword(int UserID, string Password)
    {
        int msg = SqlProject.Users.NewPassword(UserID, Password);
        return msg;
    }

    [WebMethod]

    public DataTable UserTicketView(int UserID)
    {
        DataTable dt = SqlProject.Invitation.UserTicketView(UserID);
        return dt;
    }

    [WebMethod]

    public string CancelInvite(int InvitationID)
    {
        BL.Customer act = new BL.Customer();

        string msg = act.CancelInvite(InvitationID);
        return msg;
    }

    [WebMethod]

    public int RetrieveCreditNum(int UserID)
    {
        int msg = SqlProject.Users.RetrieveCreditCard(UserID);
        return msg;
    }

















      
    
       
   



 
}

