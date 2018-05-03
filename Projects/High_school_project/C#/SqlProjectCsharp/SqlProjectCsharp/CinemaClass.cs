using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace SqlProject
{
    public class CinemaClass
    {

        public static DataTable Cinema()
        {
            string sqlString = "Select CinemaID From CinemaTable";
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
        }//checked


        public static int InsertNewCinema(string Genre, int Seats, string Kind)
        {
            return SqlProject.SqlHelper.ExecuteNonQuery("insert into CinemaTable values('" + Genre + "','" + Seats + "','" + Kind + "')");
        }


        public static DataTable Generes()//table of all genres name, without Hazarot ---CHECKED//////
        {
            string sqlString = "Select Genre From CinemaTable";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string genre = dt.Rows[i]["genre"].ToString();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (i != j)
                        {
                            string genre2 = dt.Rows[j]["genre"].ToString();
                            if (genre == genre2)
                                dt.Rows.RemoveAt(i);
                        }
                        }
                    }
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

        public static int NewCinema(int CinemaID, string Genre, int Seats, string Kind)
        {

            return SqlProject.SqlHelper.ExecuteNonQuery("insert into CinemaTable values( '" + CinemaID + "', '" + Genre + "','" + Seats + "','" + Kind + "')");
        }

        public static DataTable RetrieveSeatsUnAvilable(int ScreeningID)
        {
            string sqlString = " SELECT SeatNumber From InvitationTable Where ScreeningID=" + ScreeningID;
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            return dt;
            //Console.WriteLine("The Unavilable Seats:");
            //Console.WriteLine();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dt.Columns.Count; j++)
            //    {
            //        Console.Write(dt.Rows[i][j] + "  ");
            //    }
            //    Console.WriteLine();
            //}
        }

        public static int NumberOfSeatsInCinema(int CinemaID)//checked
        {
          string sqlString ="Select Seats From CinemaTable Where CinemaID="+CinemaID;
          DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString,"dt");
          string sSeats = dt.Rows[0][0].ToString();
          int seats = int.Parse(sSeats);
          return seats;
        }



        public static  DataTable CinemaKindTable()
        {
            string sqlString = "SELECT CinemaID,Kind FROM CinemaTable";
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString,"dt");
            //Console.WriteLine(dt.Rows[1][1]);
            //Console.ReadKey();
         
            
            return dt;
        }
        public static string CinemaKind(int CinemaID)//
        {

            string sqlString = "Select Kind From CinemaTable Where CinemaID =" + CinemaID;
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            string ckind = dt.Rows[0][0].ToString();
      
            //Console.WriteLine(ckind);
            //Console.ReadKey();
            return ckind;
        }










        //static void Main(string[] args)
        //{
        //    Generes();
            //Generes();
            //Console.WriteLine(NumberOfSeatsInCinema(2));
            //   //Console.WriteLine(NumberOfSeatsInCinema(1));
            //    //Console.WriteLine(CinemaKindTable());
            //    //    RetrieveSeatsUnAvilable(1);
            //    //CinemaKind(1);
        //    Console.ReadKey();
        //}
    }
}
