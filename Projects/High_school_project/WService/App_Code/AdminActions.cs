using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using BL;



//localhost 3



/// <summary>
/// Summary description for AdminActions
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AdminActions : System.Web.Services.WebService {

    public AdminActions () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    
   public void UpdateTicketPrice(int SundayToThursday, int Weekend)
    {
        AdminMng act = new AdminMng();
        act.UpdateTicketPrice(SundayToThursday,Weekend);
    }

    [WebMethod]
    public void VipPrice(int VipCost)
    {
        AdminMng act = new AdminMng();
        act.VipPrice(VipCost);
    }

   

    //[WebMethod]

    //public string InsertNewMovieDateChecking(string MovieID)
    //{
    //    AdminMng act = new AdminMng();
    //    string msg = act.InsertNewMovieDateChecking(MovieID);
    //    return msg;
    //}

    
    [WebMethod]

    public int InsertNewMovie(string MovieID, string Director, string MainActor, int YearFilmed, int Length, string Genre, int Rate, string DateStart, string DateEnd, int AgeLimit)
    {
        int msg = SqlProject.Movies.InsertNewMovie(MovieID, Director, MainActor, YearFilmed, Length, Genre, Rate, DateStart, DateEnd, AgeLimit);
        return msg;
    }

    [WebMethod]

    public int DeleteOldMovies()
    {
        AdminMng act = new AdminMng();
        int msg = act.DeleteOldMovies();
        return msg;
    }

    [WebMethod]

   public string UpdateDateEndOfMovieAndDeleteEmptyScreenings(string MovieID, string DateEnd)
    {
        AdminMng aa = new AdminMng();
        string msg = aa.UpdateDateEndOfMovieAndDeleteEmptyScreenings(MovieID,DateEnd);
        return msg;
    }
    
    
    [WebMethod]

    public int InsertNewScreening(string Date, int CinemaID, int Price, string MovieID)
    {
        AdminMng act = new AdminMng();
        int msg = act.InsertNewScreening(Date, CinemaID, Price, MovieID);
        return msg;
    }

    [WebMethod]

    public DataTable Cinema()
    {
        DataTable dt = SqlProject.CinemaClass.Cinema();
        return dt;
    }

    [WebMethod]

    public int InsertNewCinema(string Genre, int Seats, string Kind)
    {
        int k = SqlProject.CinemaClass.InsertNewCinema(Genre, Seats, Kind);
        return k;
    }

    [WebMethod]

    public DataSet Details()
    {
        AdminMng mng = new AdminMng();
        DataSet ds = mng.Details();
        return ds;
    }

    [WebMethod]

    public DataTable AllUsersDetails()
    {
        DataTable dt = SqlProject.Users.AllUsersDetails();
        return dt;
    }

    [WebMethod]

    public int DeleteUser(int UserID)
    {
        int msg = SqlProject.Users.DeleteUser(UserID);
        return msg;
    }

    [WebMethod]

    public int NumOfInvitationsForScreeningID(int ScreeningID)
    {
        int msg = SqlProject.Invitation.NumOfInvitationsForScreeningID(ScreeningID);
        return msg;
    }

    [WebMethod]

    public int DeleteScreeningID(int ScreeningID)
    {
        AdminMng mng = new AdminMng();
        int msg = mng.DeleteScreeningID(ScreeningID);
        return msg;
    }

    [WebMethod]

    public DataTable AllScreeningsIDForMovieID(string MovieID)
    {
        DataTable dt = SqlProject.Screening.AllScreeningsIDForMovieID(MovieID);
        return dt;
    }



    



        


    
}

