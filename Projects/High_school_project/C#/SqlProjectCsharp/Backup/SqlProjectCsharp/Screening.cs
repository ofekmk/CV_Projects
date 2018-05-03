using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SqlProject
{
    public class Screening
    {

                    public static int NewScreening(string Date,int CinemaID, int Price, string MovieID)//Checked
        {
            return SqlProject.SqlHelper.ExecuteNonQuery("Insert Into TableScreening (Date,CinemaID,Price,MovieID) values('" + Date + "','" + CinemaID + "','" + Price + "','" + MovieID + "')");

        }

                    public static int DeleteAllEmptyScreenings(string MovieID, string date)//checked
                    {
                        string sqlString = "Delete From TableScreening Where MovieID='" + MovieID + "' and Date>'" + date + "'";
                        int msg = SqlProject.SqlHelper.ExecuteNonQuery(sqlString);
                        return msg;
                    }

                    public static int DeleteScreeningID(int ScreeningID)//checked
                    {
                        string sqlString = "Delete From TableScreening Where ScreeningID=" + ScreeningID;
                        int msg = SqlProject.SqlHelper.ExecuteNonQuery(sqlString);
                        return msg;
                    }

      


                    public static DataTable AllScreeningsIDForMovieID(string MovieID)////
                    {
                        string sqlString = "Select ScreeningID From TableScreening Where MovieID='" + MovieID + "'";
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



        public static int NumOfScreeningForMovieID(string MovieID)////
        {
          DataTable dt =  AllScreeningsIDForMovieID(MovieID);
            int sum = dt.Rows.Count;
            return sum;
        }


                    public static string LastScreeningDateForMovieID(string MovieID)////
                    {
                        string msg;
                        string sqlStringcehck = "select Date From TableScreening Where MovieID='" + MovieID + "'";
                        DataTable dtcheck = SqlProject.SqlHelper.GetDataTable(sqlStringcehck,"dtcheck");
                        if (dtcheck.Rows.Count!=0)
                        {
                        string sqlString = "Select max( Date ) From TableScreening Where MovieID='" + MovieID + "'";
                        DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
                        msg = dt.Rows[0][0].ToString();
                        }
                        else
                            msg="No Screenings For This Movie";

                        return msg;
                    }



                    public static string RetrieveMovieID(int ScreeningID)////
                    {
                        string sqlString = "Select MovieID From TableScreening Where ScreeningID =" + ScreeningID;
                        DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
                        string movie = dt.Rows[0][0].ToString();
                        return movie;
                    }



                    public static DataTable ScreeningsOfOldMoviesTable()////////checked
                    {
                        DataTable dt = Movies.OldMoviesList();
                        DataTable dt2 = new DataTable();
                        dt2.Columns.Add("ScreeningID");
                        int k = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string movie = dt.Rows[i]["MovieID"].ToString();
                            DataTable dt3 = Movies.AllScreeningForMovieIDTable(movie);
                            for (int j = 0; j < dt3.Rows.Count; j++)
                            {
                                int screening = int.Parse(dt3.Rows[j]["ScreeningID"].ToString());
                                dt2.Rows.Add();
                                dt2.Rows[k]["ScreeningID"] = screening;
                                k = k + 1;
                            }
                        }
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt2.Columns.Count; j++)
                            {
                                Console.Write(dt2.Rows[i][j] + "  ");
                            }
                            Console.WriteLine();
                        }
                        return dt2;
                    }

      







        public static int DeleteOldScreenings()//checked
        {//2
            DataTable dt = Movies.OldMoviesList();
            int k = 0;
            string sqlString = "Select ScreeningID,MovieID From TableScreening";
            DataTable dt2 = SqlHelper.GetDataTable(sqlString,"dt2");
            for (int i=0;i<dt.Rows.Count;i++)
            {
                string movie = dt.Rows[i]["MovieID"].ToString();
                for (int j=0;j<dt2.Rows.Count;j++)
                {
                 string movie2 = dt2.Rows[j]["MovieID"].ToString();
                 int screeningid = int.Parse(dt2.Rows[j]["ScreeningID"].ToString());
                 if (movie == movie2)
                {
                    string sqlString2 = "Delete From TableScreening Where ScreeningID="+screeningid;
                    SqlHelper.ExecuteNonQuery(sqlString2);
                     k = k+1;
                 }
                }
            }
            return k;
        }









        public static DataSet ScreeningDetails(int ScreeningID)
        {
            string sqlString = "Select * From TableScreening Where ScreeningID =" + ScreeningID;
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
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


        public static DataTable Dates()
    {
           string sqlString = "Select Date From TableScreening";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            return dt;
        }

        public static bool ScreeningIDValidation(int ScreeningID)
        {
            bool flag = false;
            string sqlString = " Select ScreeningID From TableScreening";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sID = dt.Rows[i]["ScreeningID"].ToString();
                int id = int.Parse(sID);
                if (id == ScreeningID)
                    flag = true;

            }
            return flag;
        }

               


        public static string ScreeningDate(int ScreeningID)
        {
            string sqlString = "Select Date From TableScreening Where ScreeningID =" + ScreeningID;
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            string date = dt.Rows[0][0].ToString();
            return date;
        }

        public static int RetrieveCinemaID(int ScreeningID)
        {
            string sqlString = "Select CinemaID From TableScreening Where ScreeningID =" + ScreeningID;
            DataTable dt = SqlHelper.GetDataTable(sqlString, "CinemaID");
            return int.Parse(dt.Rows[0][0].ToString());
        }
        public static DataTable RetrieveCinemaIDTable()//checked
        {
            string sqlString = "SELECT CinemaID From TableScreening";
            DataTable dt = SqlHelper.GetDataTable(sqlString, "dt");
            //Console.WriteLine(dt.Rows[0][0]);
            //Console.ReadKey();
            return dt;
        }

        public static int RetrieveScreeningPrice(int ScreeningID)
        {
            string sqlString = "SELECT Price FROM TableScreening WHERE ScreeningID=" +ScreeningID;
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString,"dt");
            string sPrice = dt.Rows[0][0].ToString();
            int price = int.Parse(sPrice);
            //Console.WriteLine(price);
            return price;
        }

        public static void InputPrice(int Price, int ScreeningID)
        {
            string sqlString = "Update TableScreening Set Price='"+Price+"' WHERE ScreeningID="+ScreeningID;
            SqlProject.SqlHelper.ExecuteNonQuery(sqlString);
        }

        public static string getScreeningHour(int ScreeningID)
        {
            string sqlString = "Select Date From TableScreening Where ScreeningID=" + ScreeningID;
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            string sDate = dt.Rows[0][0].ToString();
            DateTime date = DateTime.Parse(sDate);
            string hour = date.ToShortTimeString();
            //Console.WriteLine(hour);
            return hour;
        }


       


        public static DataTable ScreeningDates()//WAS CHECKED
        {
            string sqlString = "Select ScreeningID,Date From TableScreening";
            return SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            //DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString,"dt");
            //        for (int i = 0; i < dt.Rows.Count; i++) 
            //        {

            //               for (int j = 0; j < dt.Columns.Count; j++)
            //               {
            //                   Console.Write(dt.Rows[i][j] + "  ");
            //               }
            //               Console.WriteLine();
            //           }
            //   }
        }

        public static DataTable AdminSchedule()
        {
            string sqlString1 = "Select TableScreening.ScreeningID,TableScreening.Date,TableScreening.Price, MoviesTable.MovieID FROM TableScreening INNER JOIN MoviesTable ON TableScreening.MovieID=MoviesTable.MovieID";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString1, "dt");
            string sqlString2 = "Select dt.ScreeningID,dt.Date,dt.Price, dt.MovieID, CinemaTable.CinemaID,CinemaTable.Seats,CinemaTable.Kind FROM dt INNER JOIN CinemaTable ON dt.CinemaID=CinemaTable.CinemaID Order by dt.Date";
            DataTable dt2 = SqlProject.SqlHelper.GetDataTable(sqlString2, "dt2");
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                for (int j = 0; j < dt2.Columns.Count; j++)
                {
                    Console.Write(dt2.Rows[i][j] + "  ");
                }
                Console.WriteLine();
            }

            return dt;
        }

        public static string DateOnly(string Date)//gives date without hour..
        {
            DateTime date = DateTime.Parse(Date);
            string newDate = date.ToShortDateString();
            
            //Console.WriteLine(newDate);
            return newDate;
        }




        public static DataSet SearchMovieByMoviesName(string MovieID )
        {
            string sqlString = "Select * From TableScreening Where MovieID='"+ MovieID+"' order by TableScreening.Date ";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            dt.Columns.Add("Time");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int screeningID = int.Parse(dt.Rows[i]["ScreeningID"].ToString());
                string hour = getScreeningHour(screeningID);
                dt.Rows[i]["Time"] = hour;
                string date = dt.Rows[i]["Date"].ToString();

                string newDate = DateOnly(date);
                dt.Rows[i]["Date"] = newDate;
            }

 

                
                //section 2 - deleting Fromn Date's column the Hour:

           
            //Console.WriteLine(dt.Rows.Count);


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
            //Console.WriteLine(ds.Tables.Count);
            return ds;


      
        }

      
            
   
        public static DataSet SearchMovieByGenre(string Genre)//checked!!!!!!!!!
        {
            string sqlString1 = "Select CinemaID,Genre From CinemaTable Where Genre='" + Genre + "'";
            DataTable dt1 = SqlProject.SqlHelper.GetDataTable(sqlString1, "dt1");
            string sqlString2 = "Select * From TableScreening";
            DataTable dt2 = SqlProject.SqlHelper.GetDataTable(sqlString2, "dt2");
            DataTable dt3 = new DataTable();
            if (dt1.Rows.Count >= 1)
            {

                dt3.Columns.Add("ScreeningID");
                dt3.Columns.Add("Date");
                dt3.Columns.Add("CinemaID");
                dt3.Columns.Add("Price");
                dt3.Columns.Add("MovieID");
                dt3.Columns.Add("Genre");
                int k = 0;

                for (int i = 0; i < dt2.Rows.Count; i++)
                {

                    string sCinemaID = dt2.Rows[i]["CinemaID"].ToString();
                    int cinemaID = int.Parse(sCinemaID);
                    int j = 0;
                    
                    bool flag = false;
                    while ((flag == false) & (j < dt1.Rows.Count))
                    {
                        string sCinemaID2 = dt1.Rows[j]["CinemaID"].ToString();
                        int cinemaID2 = int.Parse(sCinemaID2);
                        if (cinemaID == cinemaID2)
                        {
                            string genre = dt1.Rows[j]["Genre"].ToString();
                            if (genre == Genre)
                            {
                                dt3.Rows.Add();
                                dt3.Rows[k]["ScreeningID"] = dt2.Rows[i]["ScreeningID"];
                                dt3.Rows[k]["Date"] = dt2.Rows[i]["Date"];
                                dt3.Rows[k]["CinemaID"] = dt2.Rows[i]["CinemaID"];
                                dt3.Rows[k]["Price"] = dt2.Rows[i]["Price"];
                                dt3.Rows[k]["MovieID"] = dt2.Rows[i]["MovieID"];
                                dt3.Rows[k]["Genre"] = dt1.Rows[j]["Genre"];

                                k = k + 1;
                                flag = true;
                            }
                        }
                        j = j + 1;
                    }
                }
            }
              dt3.Columns.Add("Time");
              for (int i = 0; i < dt3.Rows.Count; i++)
              {
                  int screeningID = int.Parse(dt3.Rows[i]["ScreeningID"].ToString());
                  string hour = getScreeningHour(screeningID);
                  dt3.Rows[i]["Time"] = hour;
                  string date = dt3.Rows[i]["Date"].ToString();
              }
             



            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                for (int j = 0; j < dt3.Columns.Count; j++)
                {
                    Console.Write(dt3.Rows[i][j] + "  ");
                }
                Console.WriteLine();
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt3);
            //Console.WriteLine(ds.Tables.Count);



            return ds;

        }

             
           public static DataSet DataSetSearchMovieByDate(string Date)//checked
           {
               string sqlString = "Select * From TableScreening Where Date ='"+Date+"'";
               DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString,"dt");
               dt.Columns.Add("Time");
                for (int i = 0; i < dt.Rows.Count; i++)
               {
                   int screeningID = int.Parse(dt.Rows[i]["ScreeningID"].ToString());
                   string hour = getScreeningHour(screeningID);
                   dt.Rows[i]["Time"] = hour;
                   string date = dt.Rows[i]["Date"].ToString();
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
               Console.WriteLine(ds.Tables.Count);

               return ds;
           }

























           //static void Main(string[] args)
           //{
           //    Console.WriteLine(DeleteAllEmptyScreenings("rush hour","12/12/2010"));
           //    Console.WriteLine(DeleteScreeningID(90));
           //    ScreeningsOfOldMoviesTable();

           //    Console.WriteLine(LastScreeningDateForMovieID("fewf"));

               //    Console.WriteLine( RetrieveMovieID(55));
               //    //    Console.WriteLine(DeleteOldScreenings());
               //    //Console.WriteLine(NewScreening("12/12/2012", 1, 12, "spiderman"));
               //    //    //ScreeningDetails(14);
               //    //    //ScreeningDetails(1);
               //    //    //SearchMovieByMoviesName("spiderman");
               //    //    //DateOnly("05/04/2008 00:00:00");
               //    //    //getScreeningHour(1);
               //    //    //DataSetSearchMovieByDate("05/04/2008 00:00:00");
               //    //    //SearchMovieByGenre("comedy");

               //    //    //DataSetSearchMovieByDate("05/04/2008 00:00:00");
               //    //    //SearchMovieByMoviesName("spiderman");
               //    //    //Console.WriteLine(SearchMovieByMoviesName("spiderman").Tables[0].Rows.Count);

               //    //    //SearchMovieByMoviesName("spiderman");
               //    //    //RetrieveCinemaIDTable();
               //    //    //RetrieveScreeningPrice(1);
               //    //    //InputPrice(5, 14);
               //    //    ////RetrieveScreeningPrice(1);
               //    //    //DateTime today = DateTime.Now;
               //    //    //DayOfWeek dayOfWeek = today.DayOfWeek;
               //    //    //Console.WriteLine(dayOfWeek.ToString());
               //    //    //ScreeningDates();
               //    //    //Console.WriteLine(ScreeningDate(1));

               //    //    //AdminSchedule();
               //    //    //Console.WriteLine(ScreeningIDValidation(14));

           //    Console.ReadKey();
           //}









           }

    }
