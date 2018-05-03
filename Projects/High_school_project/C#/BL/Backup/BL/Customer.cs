using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SqlProject;

namespace BL
{

    public class Customer
    {

        

        public void RegisterAccount(int UserID, string Fname, string Lname, int Age, string Country, string Gender, string Email, int CreditCard, string Password)
        {
            Users.Register(UserID,Fname,Lname,Age,Country,Gender,Email,CreditCard,Password);
        }


        public DataSet RetrieveSeatsAvilable(int ScreeningID)//checked
        {
            DataTable dtUnAvilable = CinemaClass.RetrieveSeatsUnAvilable(ScreeningID);
            int iCinemaID = Screening.RetrieveCinemaID(ScreeningID);
            DataTable dt2 = new DataTable();
            DataColumn dc = new DataColumn();
            dc.ColumnName = "freeSeats";
            dt2.Columns.Add(dc);
            int seats= CinemaClass.NumberOfSeatsInCinema(iCinemaID);
            for (int i = 0; i < seats; i++)
            {
                //Console.WriteLine(i);
                int z = i + 1;
                int j = 0;
                bool flag = true;
                while ((j<dtUnAvilable.Rows.Count)&&(flag==true))
                {
                    string snum = dtUnAvilable.Rows[j][0].ToString();
                    int num = int.Parse(snum);
                    if (z==num)
                        flag = false;

                    j = j + 1;
                }
                if (flag==true )
                {
                    DataRow dr = dt2.NewRow();
                    dr["freeSeats"] = z;
                    dt2.Rows.Add(dr);
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

                   

             
            //Console.WriteLine(dt2.Rows[2][0]);

            //Console.WriteLine(dt2.Rows[0][0]);
            //Console.ReadKey();
            //Console.WriteLine(dt2.Rows.Count);
              DataSet ds = new DataSet();
              ds.Tables.Add(dt2);
              return ds;
 

            //ds.Tables.Add
        }

        public DataSet TickectInvitationTable(int ScreeningID)//checked
        {
            DataSet ds = RetrieveSeatsAvilable(ScreeningID);
            DataTable dt = ds.Tables[0];
            bool flag;
            int iCinemaID = Screening.RetrieveCinemaID(ScreeningID);
            int seats = CinemaClass.NumberOfSeatsInCinema(iCinemaID);
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Seat");
            dt2.Columns.Add("Status");
            for (int i = 0; i < seats; i++)
            {
                int z = i + 1;
                flag=false;
                dt2.Rows.Add();
                dt2.Rows[i]["Seat"] = z;
                int j = 0;
                while ((j < dt.Rows.Count) & (flag == false))
                {
                    string sSeat = dt.Rows[j]["freeSeats"].ToString();
                    int seat = int.Parse(sSeat);
                    if (seat == z)
                    {
                        dt2.Rows[i]["Status"] = "Free";
                        flag = true;
                    }
                    else

                        dt2.Rows[i]["Status"] = "Taken";

                    j = j + 1;
                }
            }
            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dt2.Columns.Count; j++)
            //    {
            //        Console.Write(dt2.Rows[i][j] + "  ");
            //    }
            //    Console.WriteLine();
            //}
            DataSet ds2 = new DataSet();
            ds2.Tables.Add(dt2);
            return ds2;
        }
                        


                









        public bool IsSeatFree(int SeatNumber, int ScreeningID)//checked
        {
            bool flag;
            int i;
            i = 0;
            flag = true;//true-Free False-Taken
          

            DataTable dt = CinemaClass.RetrieveSeatsUnAvilable(ScreeningID);
            while ((flag == true) && (i < dt.Rows.Count))
            {
                string sSeat = dt.Rows[i][0].ToString();
                int iSeat = int.Parse(sSeat);
                if (iSeat == SeatNumber)

                    flag = false;

                i = i + 1;
            }
            return flag;
        }




        public int TotalCost(int ScreeningID)
        {
            int screeningCost = Screening.RetrieveScreeningPrice(ScreeningID);
            return screeningCost;
        }





        public bool BuyingConfirmation(int UserID, int CreditCardNumber, string Password)//
        {

            bool flag;
            int cred = Users.RetrieveCreditCard(UserID);
            string pass = Users.UserPassword(UserID);
            if ((CreditCardNumber == cred)&(pass==Password))
            
                flag = true;
            
            else
            
                flag = false;
           
            return flag;


        }





        public string InviteSeat(int UserID, int ScreeningID, int SeatNumber)//checked
        {
            string dateNow = Invitation.DateToday();     
            string screeningDate = Screening.ScreeningDate(ScreeningID);
            DateTime dt1 = DateTime.Today; 
            DateTime dt2 = DateTime.Parse(screeningDate);
            TimeSpan dateDiff = dt2 - dt1;
            string msg;
            //Console.WriteLine(dateDiff);
            if (dateDiff.Days > 1)
            {
             

                    string time = Invitation.DateToday();
                    Invitation.OrderTicket(UserID, time, SeatNumber, ScreeningID);
                    msg = "Buying Success";
            }
            else
                msg = "Screening is unavilable";

            return msg;
        }
        



        public string CancelInvite(int InvitationID)
        {
            //int iInvitationID = Invitation.RetrieveInvitationID(UserID, ScreeningID, SeatNumber);
            //string iInvitationTime = Invitation.InvitationDateTime(InvitationID);

            int screeningid = Invitation.InvitationsScreeningID(InvitationID);
            string screeningDate = Screening.ScreeningDate(screeningid);
            string msg;
            DateTime invitationdate = DateTime.Parse(Invitation.InvitationDateTime(InvitationID));
            DateTime timetoday = DateTime.Today;
            DateTime sdate = DateTime.Parse(screeningDate);
            //int dateComp = dt1.CompareTo(dt2);
            TimeSpan dateDiff = sdate - timetoday;
            if (dateDiff.Days > 2)
            {
                Invitation.DeleteInvitation(InvitationID);
                msg = "Invitation Has Been Canceled";
            }
            else
                if (sdate < invitationdate)
                {
                    Invitation.DeleteInvitation(InvitationID);
                    msg = "Invitation Has Been Canceled";
                }
                else

            
                msg = "System Can not Delete Your Invitation";

            Console.WriteLine(msg);
              
            return msg;
                

        }

       





        //public void MultiInvite(int ScreeningID, int UserID, int SeatStart, int SeatEnd, int CreditCardNumber)//checked
        //{
            
        //    int i =SeatStart;
        //    bool flag = true;
        //    int cost = (TotalCost(ScreeningID)*(SeatEnd-SeatStart+1)); 
        //    while ((flag ==true)&(i<SeatEnd))
        //    {
        //        flag = IsSeatFree(i,ScreeningID);
        //        i = i+1;
        //    }
        //    i = SeatStart;
        //    if (flag==true)
        //    {
        //        Console.WriteLine(cost);
        //        if (BuyingConfirmation(UserID, CreditCardNumber)==true)
                    
        //     while (i<SeatEnd)
        //     {
        //         InviteSeat(UserID,ScreeningID,i, CreditCardNumber);
        //         i= i+1;
        //     }
        //    }
        //    else
        //        Console.WriteLine("One or More of the seats are unavilable, please choose Avilable seats.");
        //}

                


        //static void Main(string[] args)
        //{

        //    Customer customer = new Customer();
        //    customer.CancelInvite(39);
        //    //Console.WriteLine(customer.BuyingConfirmation(2, 1212, "aaa"));
        //    //Console.WriteLine(customer.InviteSeat(2, 1, 5));
        //    //customer.TickectInvitationTable(1);
        //    //Console.WriteLine(customer.IsSeatFree(12, 1));
        //    //customer.RegisterAccount(182, "Ofek", "Kettner", 18, "Israel", "Male", "dakar64@gmail.com", 1234, "12341234");
        //////    //Console.WriteLine(customer.IsSeatFree(5,1));
        //////    //customer.TotalFreeSeats(1);
        //////    //Console.WriteLine(customer.TotalFreeSeats(1));
        //////    //Console.WriteLine(customer.RetrieveSeatsAvilable(1));
        //////    //Console.WriteLine(customer.IsSeatFree(5, 1));
        ////    customer.InviteSeat(3055,14,12);
        //////    Console.ReadKey();

        ////    DateTime today = DateTime.Now;
        ////    DateTime date = new DateTime(2000, 12, 1);
        ////    TimeSpan dif = today - date;
        ////    Console.WriteLine(dif.ToString());
          
        ////    Console.WriteLine("Days"+ dif.Days+"  Hours" + dif.Hours + "  Minutes" + dif.Minutes);
         
        ////    Console.WriteLine("TotalDays"+dif.TotalDays + "  TotalHours"+dif.TotalHours+" TotalMinutes"+dif.TotalMinutes);
        //    //Console.ReadKey();
        ////    customer.CancelInvite(3055,1,12);
        //    //customer.InviteSeat(3055, 1, 21);
        //    //customer.InviteSeat(3055, 1, 12);
        //    //customer.MultiInvite(14, 2, 4, 10);
        //    //customer.MultiInvite(1, 2, 24, 25, 1212);
        //    Console.ReadKey();

        //    //-------------------------STRING------------------------------------

        //    //string str = "hello world";
        //    //str.Contains("world");//give true or false if its inside the string
        //    //str.IndexOf("l");//gives the place of the string(starts from 0)
        //    //Console.WriteLine(str.Length);//gives the lenght
        //    //str.Replace("o", "a");//sam bimkom efo she o - a
        //    //str.StartsWith("hello");//if it starts with this string
        //    //string res = str.ToLower();//all letters to otiot ktanot
        //    //res = str.ToUpper();//a efeh
        //    //str.Trim();//mohek revahim ba athla ve basof
        //    //substring(6,10) - gives the string from place 6 to 10;
        //    //remove - removing chars (1,2) - 1 from 2 how much
        //    //string[] strArr = str.splitSplit(' '); - sam akol be maarah lefi revah;
        //    //Console.WriteLine(str1.CompareTo(str2));// -1 if its before 0 - same 1 - ahrei str2
        //    // system.text.regularexpression-----ma she nozzar- regx
        //    //string str1 ="dwd";
        //    //string sPattern = "[^a-z]";{bodek aim str1 mesug sPattern(using [] -)
        //    //!Regex.IsMatch9str1, sPattern);//[^ - she lo yahil , ve lahen samim !( not ) kedei she yeye rak a-z(afuh al afuh)

        //  //-------------------array---------------------------
        //    //int[] arr1 = { 1, 2, 3, 4 };
        //    //int[] arr2 = { 1, 2, 3, 4 };
        //    //int[] arr3 = arr2;
        //    //Console.WriteLine(arr1 equals( arr2));//if the arahim shavim - true
        //    //Console.WriteLine(arr1==arr2);//if the position is the same- answer-false




        //}






        }




    }

