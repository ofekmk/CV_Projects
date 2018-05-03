using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SqlProject;

namespace BL
{
    public class WebSystem
    {

        public String LogInCheck(int UserID, string Password)//checked
        {
            bool flag = Users.CheckUserID(UserID);
            if (flag == true)
            {
                bool flag2 = Users.Login(UserID, Password);
                if (flag2 == true)
                    return "Login Success";
                else
                    return "Wrong Password";
            }

            else
                return "Wrong User ID";

        }

        public string CheckMoviesName(string MovieID)//checked
        {
            DataTable dt = Movies.Movie();
            string msg = "ok";
            int i = 0;
            while ((i < dt.Rows.Count) & (msg == "ok"))
            {
                string name = dt.Rows[i]["MovieID"].ToString();
                if (MovieID == name)
                    msg = "Movie's name is already in DataBase";
                i = i + 1;
            }
            return msg;
        }

        public string InsertNewMovieDateChecking(string MovieID)//PROBLEM!!
        {
            string msg;
            msg = "ok";
            DateTime datestart = DateTime.Parse(Movies.MovieDateStart(MovieID));
            DateTime dateend = DateTime.Parse(Movies.MovieDateEnd(MovieID));
            DateTime datetoday = DateTime.Now;
            if (dateend == datestart)
                msg = "Error - Dates Must Be Different";
            else
                if (dateend < datestart)
                    msg = "Error - Date End is earlier than date start";
                else
                    if (dateend<datetoday)
                        msg = "Error";
            return msg;
        }

        public string InsertNewScreeningDateCheck(string Date, string MovieID)//checked
        {
           
            string msg = "Error";
            DateTime date = DateTime.Parse(Date);
            DateTime datestart = DateTime.Parse(Movies.MovieDateStart(MovieID));
            DateTime dateend = DateTime.Parse(Movies.MovieDateEnd(MovieID));
            if ((date > datestart) & (date < dateend))
                msg = "Success";
            return msg;
        }



        public string CheckUserIDRegister(int UserID)//checked
        {
            string msg;
            DataTable dt = Users.AllUsersID();
            bool flag = true;
            int k = 0;
            while ((k < dt.Rows.Count) & (flag == true))
            {
                int user = int.Parse(dt.Rows[k]["UserID"].ToString());
                if (user == UserID)
                    flag = false;
                k = k + 1;
            }
            if (flag == true)
                msg = "Available";
            else
                msg = "UserID Already Taken";
            return msg;
        }



















        //static void Main(string[] args)
        //{
        //    WebSystem ws = new WebSystem();
        //    Console.WriteLine(ws.CheckUserIDRegister(181));
            //    WebSystem system = new WebSystem();
            //    Console.WriteLine(system.InsertNewScreeningDateCheck("12/12/1989", "spiderman"));
            //    //Console.WriteLine(system.CheckMoviesName("spidermann"));
            //    //system.LogInCheck(2,"aaa");
        //        Console.ReadKey();
        //    }
    }
    }
