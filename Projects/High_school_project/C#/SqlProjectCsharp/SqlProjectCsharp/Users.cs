using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;



namespace SqlProject
{
    public class Users
    {

        /// //////-----Private Profile------//////
        /// 

        public static DataTable AllUsersDetails()/////checked
        {
            string sqlString = "Select * From UserTable";
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

        public static int DeleteUser(int UserID)//////checked
        {
            string sqlString = "Delete From UserTable Where UserID=" + UserID;
            int msg = SqlHelper.ExecuteNonQuery(sqlString);
            return msg;
        }

        public static DataTable AllUsersID()//////checked
        {
            string sqlString = "Select UserID From UserTable";
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

        public static int NewPassword(int UserID, string Password)
        {
            string sqlString = "Update UserTable Set Password = '" + Password + "' Where UserID=" + UserID;
            return SqlProject.SqlHelper.ExecuteNonQuery(sqlString);
        }

      
        public static int RetrieveUserID(int UserID)//1
    {
            string sqlString = "Select UserID From UserTable Where UserID=" + UserID;
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            string suserID = dt.Rows[0][0].ToString();
            int id = int.Parse(suserID);
            return id;
    }
         public static string RetrieveFirstName(int UserID)//2
    {
        string sqlString = "Select Fname From UserTable Where UserID=" + UserID;
        DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
        string firstname = dt.Rows[0][0].ToString();
        return firstname;
    }

         public static string RetrieveFLastName(int UserID)//3
         {
             string sqlString = "Select Lname From UserTable Where UserID=" + UserID;
             DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
             string lastname = dt.Rows[0][0].ToString();
             return lastname;
         }

         public static int RetrieveAge(int UserID)
         {
             string sqlString = "Select Age From UserTable Where UserID=" + UserID;
             DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
             string sAge = dt.Rows[0][0].ToString();
             int age = int.Parse(sAge);
             return age;
         }

         public static string RetrieveCountry(int UserID)
         {
             string sqlString = "Select Country From UserTable Where UserID=" + UserID;
             DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
             string country= dt.Rows[0][0].ToString();
             return country;
         }

         public static string RetrieveGender(int UserID)
         {
             string sqlString = "Select Gender From UserTable Where UserID=" + UserID;
             DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
             string gender = dt.Rows[0][0].ToString();
             return gender;
         }

         public static string RetrieveEmail(int UserID)
         {
             string sqlString = "Select Email From UserTable Where UserID=" + UserID;
             DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
             string email = dt.Rows[0][0].ToString();
             return email;
         }



        
         /////end privateprofile///////

        //-------------------------------------------------------------------

        //Updtate PrivateProfile///


       




        public static int Register(int UserID, string Fname, string Lname, int Age, string Country, string Gender, string Email, int CreditCard, string Password)
        {
            return SqlProject.SqlHelper.ExecuteNonQuery("insert into UserTable values('" + UserID + "','" + Fname + "','" + Lname + "','" + Age + "','" + Country + "','" + Gender + "','" + Email + "','" + CreditCard + "','" + Password + "')");
        }

        public static string UserPassword(int UserID)
        {
            string sqlString = "Select Password From UserTable Where UserID=" + UserID;
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            string pass = dt.Rows[0][0].ToString();
            return pass;
        }




        public static int UserProfileUpdate(int UserID, string Fname, string Lname, int Age, string Country, string Gender,string Email, int CreditCard, string Password)
        {
            return SqlProject.SqlHelper.ExecuteNonQuery("UPDATE UserTable SET Fname='" + Fname + "', Lname = '" + Lname+ "', Age='" + Age + "', Country = '" + Country + "', Gender = '"+Gender+"',Email ='"+Email+"',CreditCard = '"+CreditCard+"', Password = '"+Password+"' WHERE UserID=" + UserID );
        }








        public static bool CheckUserID(int UserID)//checked
        {
            string sqlString = "SElECT UserID FROM UserTable";
            bool flag = false;
            int i = 0;
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            //Console.WriteLine(dt.Rows.Count);
            while ((i < dt.Rows.Count) && (flag == false))
            {
                string dtsUserID = dt.Rows[i]["UserID"].ToString();
                int dtUserID = int.Parse(dtsUserID);
                //Console.WriteLine(dtUserID);
                if (UserID == dtUserID)
                
                    flag = true;
                
                    i = i + 1;
                    
                }
            
            return flag;
        }






        public static bool Login(int UserID, string Password)
        {

            string sqlString = "SELECT * FROM UserTable WHERE UserID=" + UserID;
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            string DBpass = dt.Rows[0]["Password"].ToString().Trim();
            if (DBpass.Equals(Password))

                return true;

            else

                return false;


        }

        public static int RetrieveCreditCard(int UserID)
        {
            string sqlString = "Select CreditCard From UserTable Where UserID =" + UserID;
            DataTable dt = SqlProject.SqlHelper.GetDataTable(sqlString, "dt");
            string sCredit = dt.Rows[0][0].ToString();
            int credit = int.Parse(sCredit);
            return credit;
        }










        static void Main(string[] args)
        {
            Console.WriteLine(RetrieveCreditCard(182));
        //    AllUsersDetails();
        //    //Console.WriteLine(UserProfileUpdate(182,"Noa","Kettner",11,"Israel","Female","noakettner@gmail.com",164,"5678"));
        //    Console.WriteLine(NewPassword(2, "sasa")); 
                   


        //    Console.WriteLine(UserPassword(2));
        //    Console.WriteLine(RetrieveCreditCard(2));


        //    Console.WriteLine(Login(2, "aa"));
            //    Console.WriteLine(CheckUserID(3055));
            //    Console.ReadKey();
            //}






            //Login

            //Console.WriteLine(Login(2, "aa"));
            //Console.ReadKey();



            //Register(2, "ronaldinho", "r", 18, "brazil", "Male", "ronaldo", 1111, "aa");
            //Console.ReadKey();

            //Uupdate(30, "Kameroon", "samuel@", 1212,2);
            //Console.ReadKey();




            // Console.WriteLine("Enter User ID");
            //string strUserID = Console.ReadLine();
            //int UserID = int.Parse(strUserID);
            //Console.WriteLine("Choose a country to Update");
            //string Country = Console.ReadLine();
            //string SqlString = "UPDATE UserTable SET Country = '"+Country+"' WHERE UserID = '"+UserID+"' ";
            //Console.WriteLine(SqlHelper.ExecuteNonQuery(SqlString));
            //Console.ReadKey();



            //    DATTETIME
            //    DateTime today = DateTime.Now;
            //    DateTime dateOfBirth = new DateTime(1992, 05, 13);
            //    string dateOfBirthToString = dateOfBirth.ToShortDateString();
            //    string dateOfBirthToString = dateOfBirth.Day +"/" +dateOfBirth.Month + "/" + dateOfBirth.Year;
            //    Console.WriteLine(dateOfBirth.ToShortDateString());
            //    Console.WriteLine(dateOfBirth.ToShortTimeString());
            //    Console.WriteLine(dateOfBirth.ToUniversalTime());
            //    int year = dateOfBirth.Year;
            //    TimeSpan ts = new TimeSpan();
            //    ts = today - dateOfBirth;
        //    //    Console.WriteLine(ts.ToString());
            Console.ReadKey();
        }

      










    }
}


             

