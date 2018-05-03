using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SqlProject;

namespace BL
{
    public class AdminMng
    {

        public int DeleteScreeningID(int ScreeningID)//checked
        {
            DataTable dt = Invitation.InvitationsToDeleteTable(ScreeningID);
            for (int i=0;i<dt.Rows.Count;i++)
            {
                int inv = int.Parse(dt.Rows[i]["invitationID"].ToString());
                Invitation.DeleteInvitation(inv);
            }
            int msg = Screening.DeleteScreeningID(ScreeningID);
            return msg;
        }




        public string UpdateDateEndOfMovieAndDeleteEmptyScreenings(string MovieID, string DateEnd)//checked
        {
            string msg1 = "no effects";
            DateTime dend = DateTime.Parse(DateEnd);
            DateTime today = DateTime.Now;
            DataTable dt = Invitation.LastScreeningDateWithInvitationsForMovieID(MovieID);
            if (dend < today)
                msg1 = "Date End can't Be In The Past";
            else
            {
                if (dt.Rows.Count == 0)
                {
                    msg1 = "Updated";
                    Screening.DeleteAllEmptyScreenings(MovieID, DateEnd);
                    Movies.UpdateDateEnd(MovieID, DateEnd);
                }

                else
                {
                    DateTime lastdate = DateTime.Parse(dt.Rows[0][0].ToString());
                    if (dend <= lastdate)
                        msg1 = "Date End Of Movie Can't Be Earlier Than The Last Screening Date With invitations";
                    else
                        if (dend > lastdate)
                        {
                            
                            msg1 = "Updated";
                            Screening.DeleteAllEmptyScreenings(MovieID, DateEnd);
                            Movies.UpdateDateEnd(MovieID, DateEnd);
                        }
                }
            }
            return msg1;
        }



                


        //    string msg = Screening.LastScreeningDateForMovieID(MovieID);
        //    if (msg == "No Screenings For This Movie")
        //    {
        //        DateTime today = DateTime.Today;

        //        if (dend < today)
        //            msg1 = msg1 = "Date End can't be in the past";
        //        else
        //        {
        //            msg1 = "success";
        //            Movies.UpdateDateEnd(MovieID, DateEnd);
        //        }
        //    }
        //    else
        //    {
        //        DateTime today2 = DateTime.Today;
        //        string slastscreening = Screening.LastScreeningDateForMovieID(MovieID);
        //        DateTime lastscreening = DateTime.Parse(slastscreening);
        //        if (dend < lastscreening)
        //            msg1 = "Date End Can't Be Earlier Than The Movie's Last Screening Date";
        //        else
        //            if (dend < today2)
        //            {
        //                msg1 = "Date End can't be in the past";
        //            }
        //            else
        //                if (dend > today2)
        //                {
        //                    msg1 = "success";
        //                    Movies.UpdateDateEnd(MovieID, DateEnd);
        //                }
        //    }
        //    return msg1;
        //}


                   















        public int InsertNewScreening(string Date, int CinemaID, int Price, string MovieID)
        {
            //WebSystem act = new WebSystem();
            //string msg = act.InsertNewScreeningDateCheck(Date, MovieID);
            //if (msg == "Success")
             int msg =   Screening.NewScreening(Date, CinemaID, Price, MovieID);
            return msg;
        }

            



        public int DeleteOldMovies()//checked
        {
            DataTable dt = Movies.OldMoviesList();
            DataTable dt2 = Screening.ScreeningsOfOldMoviesTable();
            for (int i = 0; i < dt2.Rows.Count;i++ )
            {
                int screening = int.Parse(dt2.Rows[i]["ScreeningID"].ToString());
                DataTable dtchange = Invitation.InvitationsToDeleteTable(screening);
                for (int j = 0; j < dtchange.Rows.Count; j++)
                {
                    int invitation = int.Parse(dtchange.Rows[j]["InvitationID"].ToString());
                    Invitation.DeleteInvitation(invitation);
                }
            }
            Screening.DeleteOldScreenings();
            int k = Movies.DeleteOldMovies();
            return k;
        }
            




   
            





        public void UpdateTicketPrice(int SundayToThursday, int Weekend)//{weekend-perecents}
        {
            DataTable dt = Screening.ScreeningDates();




            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string sScreeningID = dt.Rows[i]["ScreeningID"].ToString();
                int iScreeningID = int.Parse(sScreeningID);
                string sdate = dt.Rows[i]["Date"].ToString();
                DateTime dtDate = DateTime.Parse(sdate);
                DayOfWeek dayOfWeek = dtDate.DayOfWeek;
                Console.WriteLine(dayOfWeek);
                string sDayOfWeek = dayOfWeek.ToString();
                if ((sDayOfWeek == "Friday") || (sDayOfWeek == "Saturday"))
                {
                    int sum = SundayToThursday + (SundayToThursday * Weekend / (100));
                    Screening.InputPrice(sum, iScreeningID);
                }
                else
                    Screening.InputPrice(SundayToThursday, iScreeningID);
            }

        }



                public void VipPrice(int VipCost)
                {
            DataTable dt = Screening.RetrieveCinemaIDTable();
            DataTable dt2 = Screening.ScreeningDates();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sScreeningID = dt2.Rows[i]["ScreeningID"].ToString();
                int iScreeningID = int.Parse(sScreeningID);
                string sCinemaID = dt.Rows[i]["CinemaID"].ToString();
                int iCinemaID = int.Parse(sCinemaID);
                string kind =CinemaClass.CinemaKind(iCinemaID);
                if (kind == "vip") 
                {
                 int normalPrice =Screening.RetrieveScreeningPrice(iScreeningID); 
                 int sum = normalPrice + (normalPrice * VipCost / (100));
                 Screening.InputPrice(sum,iScreeningID);
                }




                               


 


                

                

            }

        }


                public DataSet Details()
                {
                    int k = 0;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("MovieID");
                    dt.Columns.Add("InvitationNumber");
                    dt.Columns.Add("LastScreeningDate");
                    dt.Columns.Add("DateEndOfMovie");
                    dt.Columns.Add("NumberOfScreenings");
                    DataTable dt1 = Movies.AllMovies();
                    for (int i = 0; i <dt1.Rows.Count; i++)
                    {
                        string movie = dt1.Rows[i]["MovieID"].ToString();
                        dt.Rows.Add();
                        dt.Rows[k]["MovieID"] = movie;
                        k = k + 1;
                    }
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        string movie2 = dt.Rows[j]["MovieID"].ToString();
                        int sum = Invitation.NumOfInviationsForMovieID(movie2);
                        string lastscreeningdate = Screening.LastScreeningDateForMovieID(movie2);
                        string dateend = Movies.MovieDateEnd(movie2);
                        int numofscreenings = Screening.NumOfScreeningForMovieID(movie2);
                        dt.Rows[j]["InvitationNumber"] = sum;
                        dt.Rows[j]["LastScreeningDate"] = lastscreeningdate;
                        dt.Rows[j]["DateEndOfMovie"] = dateend;
                        dt.Rows[j]["NumberOfScreenings"] = numofscreenings;
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            Console.Write(dt.Rows[i][j] + "  ");
                        }
                        Console.WriteLine();
                    }
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    return ds;
                }


                static void Main(string[] args)
                {
                   
                    AdminMng admin = new AdminMng();
                    //Console.WriteLine(admin.UpdateDateEndOfMovie("rush hour", "02/02/2010"));
                //    Console.WriteLine(admin.UpdateDateEndOfMovie("Harry Potter", "01/01/2016"));
                    //Console.WriteLine(admin.InsertNewScreening("12/12/2009",1,20,"spiderman"));
                    
                    //Console.WriteLine(admin.InsertNewMovieDateChecking("Harry Potter"));
                    //admin.VipPrice(22);
                    //admin.UpdateTicketPrice(50, 100);

                    Console.ReadKey();



                }



    }
}
