using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SqlProject
{
    public class Movies
    {
        public static DataTable AllScreeningForMovieIDTable(string MovieID)
        {
            string sqlString = "Select ScreeningID From TableScreening Where MovieID='" + MovieID + "'";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
           
            return dt;
        }

        public static DataTable AllMovies()////
        {
            string sqlString = "Select MovieID From MoviesTable";
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

        public static int UpdateDateEnd(string MovieID, string DateEnd)//checked
        {
            string sqlString = "Update MoviesTable Set DateEnd='" + DateEnd + "' Where MovieID='" + MovieID + "'";
            int k = SqlHelper.ExecuteNonQuery(sqlString);
            return k;
        }


        public static int InsertNewMovie(string MovieID, string Director, string MainActor, int YearFilmed, int Length, string Genre, int Rate, string DateStart, string DateEnd,int AgeLimit)
        {
            return SqlProject.SqlHelper.ExecuteNonQuery("insert into MoviesTable values('" + MovieID + "','" + Director + "','" + MainActor + "','" + YearFilmed + "','" + Length + "','" + Genre + "','" + Rate + "','" + DateStart + "','" + DateEnd + "','" + AgeLimit + "')");
        }

        public static DateTime LastMoviesScreeningDate(string MovieID)
        {
            string sqlString = "Select DateEnd From MoviesTable Where MovieID= '"+MovieID+"'";
            DataTable dt = SqlHelper.GetDataTable(sqlString, "dt");
            string sdate = dt.Rows[0][0].ToString();
            DateTime date = DateTime.Parse(sdate);
            return date;
        }

        public static DateTime FirstMoviesScreeningDate(string MovieID)
        {
            string sqlString = "Select DateStart From MoviesTable Where MovieID= '" + MovieID + "'";
            DataTable dt = SqlHelper.GetDataTable(sqlString, "dt");
            string sdate = dt.Rows[0][0].ToString();
            DateTime date = DateTime.Parse(sdate);
            return date;
        }



   


        public static string MovieDirector(string MovieID)//
        {
            string sqlString = "Select Director From MoviesTable Where MovieID = '"+ MovieID+"'";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            string director = dt.Rows[0][0].ToString();
            return director;
        }


        public static string MovieMainActor(string MovieID)//
        {
            string sqlString = "Select MainActor From MoviesTable Where MovieID = '" + MovieID + "'";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            string mainactor = dt.Rows[0][0].ToString();
            return mainactor;
        }



        public static int MovieYearFilmed(string MovieID)//
        {
            string sqlString = "Select YearFilmed From MoviesTable Where MovieID = '" + MovieID + "'";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            int yearfilmed = int.Parse(dt.Rows[0][0].ToString());
            return yearfilmed;
        }

        public static int MovieLength(string MovieID)//
        {
            string sqlString = "Select Length From MoviesTable Where MovieID = '" + MovieID + "'";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            int length = int.Parse(dt.Rows[0][0].ToString());
            return length;
            
        }

        public static string MovieGenre(string MovieID)//
        {
            string sqlString = "Select Genre From MoviesTable Where MovieID = '" + MovieID + "'";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            string genre = dt.Rows[0][0].ToString();
            return genre;
        }

        public static double MovieRate(string MovieID)//
        {
            string sqlString = "Select Rate From MoviesTable Where MovieID = '" + MovieID + "'";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            double rate = double.Parse(dt.Rows[0][0].ToString());
            return rate;
        }
        public static string MovieDateStart(string MovieID)//
        {
            string sqlString = "Select DateStart From MoviesTable Where MovieID = '" + MovieID + "'";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            string datestart = dt.Rows[0][0].ToString();
            return datestart;
        }

        public static string MovieDateEnd(string MovieID)//
        {
            string sqlString = "Select DateEnd From MoviesTable Where MovieID = '" + MovieID + "'";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            string dateend = dt.Rows[0][0].ToString();
            return dateend;
        }

        public static int MovieAgeLimit(string MovieID)//
        {
            string sqlString = "Select AgeLimit From MoviesTable Where MovieID = '" + MovieID + "'";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            int agelimit = int.Parse(dt.Rows[0][0].ToString());
            return agelimit;
        }

    


  



        public static DataTable Movie()
        {
            string sqlString = "Select MovieID From MoviesTable";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            return dt;
        }

        public static DataTable OldMoviesList()//gives the table of all movies that their date is old
        {//1
            DateTime now = DateTime.Today;
            string sqlString = "Select * From MoviesTable";
            DataTable dt = SqlHelper.GetDataTable(sqlString, "dt");//CHECKED!(whole function)
            DataTable dtmovies = new DataTable();
            dtmovies.Columns.Add("MovieID");
            int k = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime dateend = DateTime.Parse(dt.Rows[i]["DateEnd"].ToString());
                if (now > dateend)
                {
                    string movie = dt.Rows[i]["MovieID"].ToString();
                    dtmovies.Rows.Add();
                    dtmovies.Rows[k]["MovieID"] = movie;
                    k = k + 1;
                }
            }
            for (int i = 0; i < dtmovies.Rows.Count; i++)
            {
                for (int j = 0; j < dtmovies.Columns.Count; j++)
                {
                    Console.Write(dtmovies.Rows[i][j] + "  ");
                }
                Console.WriteLine();
            }
            return dtmovies;
        }

        public static int DeleteOldMovies()//checked
        {//3
            int k = 0;
            DataTable dt = Movies.OldMoviesList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string movie = dt.Rows[i]["MovieID"].ToString();
                string sqlString = "Delete From MoviesTable Where MovieID='" + movie + "'";
                SqlHelper.ExecuteNonQuery(sqlString);
                k = k + 1;
            }
            return k;
        }













        //static void Main(string[] args)
        //{
        //    OldMoviesList();
        //    AllScreeningForMovieIDTable("spiderman");
        ////    AllMovies();
        ////    //Console.WriteLine(DeleteOldMovies());
        //    Console.Write(UpdateDateEnd("spiderman", "1/2/2028"));
        //    //Movie();
        //    //OldMoviesList();
        //    //Console.WriteLine(DeleteIT("Harry Potter"));
        //    //Console.WriteLine(InsertNewMovie("bat ayam","coco","caca",1111,180,"action",5,"11/11/2011","11/11/2012",18));
        //    //Console.WriteLine(InsertNewMovie("Harry Potter", "Charls Man", "Tom", 2008, 120, "adventure", 5, "12/12/2008", "12/12/2010", 18));
        //    //Console.WriteLine(FirstMoviesScreeningDate("spiderman"));
        //    //Console.WriteLine(MovieAgeLimit("spiderman"));
        //    Console.ReadKey();
        //}












    }


}
