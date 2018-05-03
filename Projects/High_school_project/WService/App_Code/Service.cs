using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Services;
using BL;

//localhost (no number)

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public Service()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }



    [WebMethod]
    public void UpdateTicketPrice(int SundayToThursday, int Weekend)
    {
        AdminMng action = new AdminMng();
        action.UpdateTicketPrice(SundayToThursday, Weekend);

    }





    [WebMethod]
    public String Login(int UserID, string Password)
    {
        WebSystem login = new WebSystem();
        return login.LogInCheck(UserID, Password);
    }




    [WebMethod]
    public void Register(int UserID, string Fname, string Lname, int Age, string Country, string Gender, string Email, int CreditCard, string Password)
    {
     
        Customer register = new Customer();
        register.RegisterAccount(UserID, Fname, Lname, Age, Country, Gender, Email, CreditCard, Password);
    }



    [WebMethod]
    public DataSet SearchMovieByMoviesName(string MovieID)
    {
        DataSet ds = SqlProject.Screening.SearchMovieByMoviesName(MovieID);
        return ds;
    }


    [WebMethod]
    public DataTable SearchMovieByGenre(string Genre)
    {
        DataSet ds = SqlProject.Screening.SearchMovieByGenre(Genre);
        DataTable dt = ds.Tables[0];
        return dt;
    }


    [WebMethod]
    public DataSet SearchMovieByDate(string Date)
    {
        DataSet ds = SqlProject.Screening.DataSetSearchMovieByDate(Date);
        return ds;
    }


    [WebMethod]
    public DataSet AllMoviesTable()
    {
        DataTable dt = SqlProject.Movies.Movie();
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        return ds;

    }
    [WebMethod]
    public DataSet AllDatesTable()
    {
        DataTable dt = SqlProject.Screening.Dates();
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        return ds;
    }

    [WebMethod]
    public DataSet AllGenresTable()
    {
        DataTable dt = SqlProject.CinemaClass.Generes();
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        return ds;
    }
    [WebMethod]
    public DataSet TickectInvitationTable(int ScreeningID)
    {
        Customer tickets = new Customer();
        DataSet ds = tickets.TickectInvitationTable(ScreeningID);
        return ds;
    }

    [WebMethod]
    public string InviteSeat(int UserID, int ScreeningID, int SeatNumber)
    {
        Customer act = new Customer();
        string msg = act.InviteSeat(UserID, ScreeningID, SeatNumber);
        return msg;
    }


    [WebMethod]

    public  bool BuyingConfirmation(int UserID, int CreditCardNumber, string Password)
    {
        Customer act = new Customer();
        bool flag = act.BuyingConfirmation(UserID, CreditCardNumber, Password);
        return flag;
    }

    [WebMethod]

    public int TotalCost(int ScreeningID)
    {
        Customer act = new Customer();
        int cost = act.TotalCost(ScreeningID);
        return cost;
    }

    [WebMethod]
    public bool IsSeatFree(int SeatNumber, int ScreeningID)
    {
        Customer act = new Customer();
        bool seat = act.IsSeatFree(SeatNumber, ScreeningID);
        return seat;
    }




   



    /// <summary>
    /// ///////////////////////////////////
    /// </summary>
    /// <param name="MovieID"></param>
    /// <returns></returns>




    [WebMethod]

    public string MovieDirector(string MovieID)
    {
        string moviedirector = SqlProject.Movies.MovieDirector(MovieID);
        return moviedirector;
    }



    [WebMethod]

    public string MovieMainActor(string MovieID)
    {
        string moviemainactor = SqlProject.Movies.MovieMainActor(MovieID);
        return moviemainactor;
    }

    [WebMethod]

    public int MovieYearFilmed(string MovieID)
    {
        int movieyearfilmed = SqlProject.Movies.MovieYearFilmed(MovieID);
        return movieyearfilmed;
    }

    [WebMethod]

    public int MovieLength(string MovieID)
    {
        int movielength = SqlProject.Movies.MovieLength(MovieID);
        return movielength;
    }

    [WebMethod]

    public string MovieGenre(string MovieID)
    {
        string moviegenre = SqlProject.Movies.MovieGenre(MovieID);
        return moviegenre;
    }


    [WebMethod]

    public double MovieRate(string MovieID)
    {
        double movierate = SqlProject.Movies.MovieRate(MovieID);
        return movierate;
    }


    [WebMethod]

    public string MovieDateStart(string MovieID)
    {
        string moviedatestart = SqlProject.Movies.MovieDateStart(MovieID);
        return moviedatestart;
    }

    [WebMethod]

    public string MovieDateEnd(string MovieID)
    {
        string moviedateend = SqlProject.Movies.MovieDateEnd(MovieID);
        return moviedateend;
    }

    [WebMethod]

    public int MovieAgeLimit(string MovieID)
    {
        int movieagelimit = SqlProject.Movies.MovieAgeLimit(MovieID);
        return movieagelimit;
    }

    [WebMethod]

    public DataSet ScreeningDetails(int ScreeningID)
    {
        DataSet ds = SqlProject.Screening.ScreeningDetails(ScreeningID);
        return ds;
    }

    [WebMethod]

    public int NumberOfSeatsInCinema(int CinemaID)
    {
        int k = SqlProject.CinemaClass.NumberOfSeatsInCinema(CinemaID);
        return k;
    }

    [WebMethod]

    public string CinemaKind(int CinemaID)
    {
        string k = SqlProject.CinemaClass.CinemaKind(CinemaID);
        return k;
    }

    [WebMethod]

    public int RetrieveCinemaID(int ScreeningID)
    {
        int k = SqlProject.Screening.RetrieveCinemaID(ScreeningID);
        return k;
    }
    [WebMethod]

    public DataTable Generes()
    {
        DataTable dt = SqlProject.CinemaClass.Generes();
        return dt;
    }

   



    


















   
   }


