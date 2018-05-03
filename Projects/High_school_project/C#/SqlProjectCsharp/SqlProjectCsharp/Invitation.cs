using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;



namespace SqlProject
{




   public class Invitation
    {

       //public static LastInviationDateForMovieID(string MovieID)////
       //{
       //    string max;
       //    string sqlstring ="Select Date,ScreeningID From InvitationTable";
       //    DataTable dt1 = SqlProject.SqlHelper.GetDataTable(sqlstring,"dt1");
       //    DataTable dt2=Screening.AllScreeningsIDForMovieID(MovieID);
       //    DataTable dt3 = new DataTable();
       //    dt3.Columns.Add("Date");
       //    int k=0;
       //    for (int i=0;i<dt1.Rows.Count;i++)
       //    {
       //        int screening1 = int.Parse(dt1.Rows[i]["ScreeningID"].ToString());
       //        for (int j=0;j<dt2.Rows.Count;j++)
       //        {
       //        int screening2 = int.Parse(dt2.Rows[j]["ScreeningID"].ToString());
       //            if (screening1==screening2)
       //            {
       //                string date1=dt1.Rows[i]["Date"].ToString();
       //                dt3.Rows[k]["Date"] = date1;
       //                k = k+1;
       //            }
       //        }
       //    }
       //    if (dt3.Rows.Count>0)
       //         max = dt3.Rows[0]["Date"].ToString();
       //    for (int q=1;q<dt3.Rows.Count;q++)
       //    {
       //        max2 = dt3.Rows[q]["Date"].ToString();



       public static DataTable LastScreeningDateWithInvitationsForMovieID(string MovieID)//checked
       {
           string sqlString = "Select Top 1 Date From TableScreening Inner Join InvitationTable On TableScreening.ScreeningID=InvitationTable.ScreeningID Where TableScreening.MovieID='" + MovieID + "' order by TableScreening.Date DESC";
           DataTable dt = SqlHelper.GetDataTable(sqlString, "dt");
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               for (int j = 0; j < dt.Columns.Count; j++)
               {
                   Console.Write(dt.Rows[i][j] + "  ");
               }
               Console.WriteLine();
           }

           return dt;
       }

       public static int NumOfInvitationsForScreeningID(int ScreeningID)//checked
       {
           string sqlString = "Select InvitationID From InvitationTable Where ScreeningID=" + ScreeningID;
           DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
           int num = dt.Rows.Count;
           return num;
       }


       public static DataTable InvitationsToDeleteTable(int ScreeningID)
       {
           string sqlString = "Select InvitationID From InvitationTable Where ScreeningID=" + ScreeningID;
           DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               for (int j = 0; j < dt.Columns.Count; j++)
               {
                   Console.Write(dt.Rows[i][j] + "  ");
               }
               Console.WriteLine();
           }
           return dt;
       }

       public static int NumOfInviationsForMovieID(string MovieID)////
       {
           int k = 1;
           string sqlString = "Select ScreeningID From InvitationTable";
           DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               string sscreening = dt.Rows[i]["ScreeningID"].ToString();
               int screening = int.Parse(sscreening);
               string movie = Screening.RetrieveMovieID(screening);
               if (movie == MovieID)

                   k = k + 1;
           }
           return k;
       }


         

                 





           //DataTable dtmovies = Movies.Movie();
           //for (int i=0;i<dtmovies.Rows.Count;i++)
           //{
           //    string movie = dtmovies.Rows[i]["MovieID"].ToString();
           //    for (int j=0; j<dt.Rows.Count;j++)
           //    {

               


       public static int InvitationsScreeningID(int InvitationID)
       {
           string sqlString = "Select ScreeningID From InvitationTable Where InvitationID =" + InvitationID;
           DataTable dt = SqlHelper.GetDataTable(sqlString, "dt");
           string sScreeningid = dt.Rows[0][0].ToString();
           int screeningid = int.Parse(sScreeningid);
           Console.WriteLine(screeningid);
           return screeningid;
       }
         

       public static int RetrieveInvitationID(int UserID, int ScreeningID, int SeatNumber)//checked
       {
           string sqlString = "Select InvitationID From InvitationTable Where UserID='" + UserID + "' AND ScreeningID='" + ScreeningID + "' AND SeatNumber=" + SeatNumber;
           DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
           string sInvitationID = dt.Rows[0][0].ToString();
           int iInvitationID = int.Parse(sInvitationID);
           return iInvitationID;
       }

       public static string InvitationDateTime(int InvitationID)//checked
       {
        string sqlString = "Select InvitationDate  From InvitationTable  Where InvitationID="+InvitationID;
        DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString,"dt");
        string date = dt.Rows[0][0].ToString();
        return date;
       }

       public static void DeleteInvitation(int InvitationID)
       {
           string sqlString = "Delete FROM InvitationTable Where InvitationID=" + InvitationID;
           SqlProject.SqlHelper.ExecuteNonQuery(sqlString);
       }



       public static int OrderTicket(int UserID, string InvitationTime, int SeatNumber, int ScreeningID)//Checked
       {
           string sqlString = "insert into InvitationTable Values('" + UserID + "','" + InvitationTime + "','" + SeatNumber + "','" + ScreeningID + "')";
           return (SqlProject.SqlHelper.ExecuteNonQuery(sqlString));
       }


        public static DataTable SearchMovieByGenre(string Genre)
        {

            string sqlString = "Select MovieID From MoviesTable Where Genre = '" + Genre + "')";
            return SqlProject.SqlHelper.GetDataTable(sqlString, "MoviessTable");
        }



        public static DataTable SearchMovieByDirector(string Director)
        {
            string sqlString = "Select MovieID From MoviesTable Where Director= '" + Director+ "')";
            return SqlProject.SqlHelper.GetDataTable(sqlString, "MoviesTable");
        }




        public static string DateToday()
        {
            DateTime dtime = DateTime.Now;
            string time = dtime.Month + "/" + dtime.Day + "/" + dtime.Year+" "+dtime.Hour+":"+dtime.Minute;
            return time;
        }

        public static DataTable UsersInvitationTable(int UserID)
        {
            string sqlString = "Select * From InvitationTable Where UserID =" + UserID;
            DataTable dt1 = SqlProject.SqlHelper.GetDataTable(sqlString, "dt1");

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 0; j < dt1.Columns.Count; j++)
                {
                    Console.Write(dt1.Rows[i][j] + "  ");
                }
                Console.WriteLine();
            }
            return dt1;
        }

        public static DataTable UserTicketView(int UserID)//checked
        {
            string sqlString = "Select InvitationTable.InvitationID, InvitationTable.InvitationDate,InvitationTable.SeatNumber,InvitationTable.ScreeningID,TableScreening.Date,TableScreening.CinemaID,TableScreening.Price,TableScreening.MovieID FROM InvitationTable INNER JOIN TableScreening ON InvitationTable.ScreeningID = TableScreening.ScreeningID Where UserID =" + UserID;
            DataTable dt = SqlHelper.GetDataTable(sqlString,"dt");
             for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Console.Write(dt.Rows[i][j] + "  ");
                }
                Console.WriteLine();
            }
            return dt;
        }





        //public static  






        //    Console.WriteLine("Pick A Movie");
        //    string moviePicked = Console.ReadLine();
        //    //string sqlString = "SELECT * FROM UserTable WHERE MovieID=" + moviePicked;
        //    //DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");

        //    //for (int i = 0; i <= dt.Rows.Count; i++)
        //    //{
        //    //    for (int j = 0; j < dt.Columns.Count; j++)
        //    //    {
        //    //        Console.Write(dt.Rows[i][j] + "  ");
        //    //    }
        //    //    Console.WriteLine();
        //    //}


        //    Console.WriteLine("Select ScreeningID");
        //    string strScreeningPicked = Console.ReadLine();
        //    int screeningPicked = int.Parse(strScreeningPicked);

        //    string sqlString2 = " SELECT SeatNumber From InvitationTable Where ScreeningID=" + strScreeningPicked;
        //    DataTable dt2 = SqlProject.SqlHelper.GetDataTable(sqlString2, "dt2");


        //    SqlProject.CinemaClass.RetrieveSeatsUnAvilable(screeningPicked);

        //    Console.WriteLine();
        //    Console.WriteLine("Choose a Seat");
        //    string strSeat = Console.ReadLine();
        //    int seat = int.Parse(strSeat);






        //    int id  = 2;

        //    SqlProject.SqlHelper.ExecuteNonQuery("insert into InvitationTable(UserID,SeatNumber,ScreeningID) VALUES ('"+id+"','"+seat+"','" + screeningPicked + "')");





        //}




        //    public static int InviteMovie(int UserID, string InvitationTime, int SeatNumber, int ScreeningID);
        //{

        //public staytic int InvitationCost(int Seats, int  ScreeningID)
        //{
        //    sqlString = "Select Price 
        //}

        //static void Main(string[] args)
        //{
        //    Console.WriteLine(LastScreeningDateWithInvitationsForMovieID("rush hour"));
            //Console.WriteLine(NumOfInvitationsForScreeningID(83));
        //    InvitationsToDeleteTable(77);
        //    //Console.WriteLine(InvitationDateTime(3));
        //    Console.WriteLine(NumOfInviationsForMovieID("iroboth"));
        //    //InvitationsScreeningID(40);
        //    //UserTicketView(182);
        ////    UsersInvitationTable(182);
        ////    //Console.WriteLine(OrderTicket(2, "1/2/1991", 5, 1));
        ////    //OrderTicket(2, "11/11/2009", 15, 14);
        ////    //Console.ReadKey();
        ////    //Console.WriteLine(RetrieveInvitationID(3055, 14, 12));
        //    Console.ReadKey();



        //}

        }

    }
