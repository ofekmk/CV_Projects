using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using localhost;

public partial class OrderTicket : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

          

            HttpCookie cookie = Request.Cookies["Details"];
            if (cookie == null)
                Response.Redirect("Login.aspx");


            string num = Request.QueryString["num"];
            //Response.Write("SHALOMMMM!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            int screeningID = int.Parse(num);
            Service s = new Service();
            int cinemaid = s.RetrieveCinemaID(screeningID);
            int seats = s.NumberOfSeatsInCinema(cinemaid);
            string cinemakind = s.CinemaKind(cinemaid);
            img_cinemaview.ImageUrl = "~/images/" + cinemakind + +seats+ ".jpg";
            DataSet ds = s.TickectInvitationTable(screeningID);
            DataTable dt = ds.Tables[0];
            dt.Columns.Add("SeatImage");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Status"].ToString() == "Free")

                    dt.Rows[i]["SeatImage"] = "~/images/seatfree.JPG";
                else
                {
                    if (dt.Rows[i]["Status"].ToString() == "Taken")
                        dt.Rows[i]["SeatImage"] = "~/images/seattaken.JPG";
                }
            }
            //DataSet ds2 = new DataSet();
            //ds2.Tables.Add(d) ;
    
            Session.Add("dsOrder",ds);
            gv_cinema_status.DataSource = dt;
            gv_cinema_status.DataBind();

            
        }





    }

    protected void gv_cinema_status_RowCommand(object sender, GridViewCommandEventArgs e)
    {

         //string sID;
        if (e.CommandName.Equals("InviteSeat"))
        {
            HttpCookie cookie = Request.Cookies["Details"];
            if (cookie == null)
                Response.Redirect("Login.aspx");
            else
            {

                //sID = cookie["UserID"].ToString();
                //int id = int.Parse(sID);

                int i = int.Parse(e.CommandArgument.ToString());
                DataSet ds = (DataSet)Session["dsOrder"];
                string sSeat = ds.Tables[0].Rows[i]["Seat"].ToString();
                int seatNumber = int.Parse(sSeat);
                Service s = new Service();

                string sScreeningID = Request.QueryString["num"];
                int screeningID = int.Parse(sScreeningID);

                bool flag = s.IsSeatFree(seatNumber, screeningID);
                if (flag == true)
                
                    Response.Redirect("BuyingConfirmation.aspx?screening=" + sScreeningID+"&seat="+sSeat);
                


                else
                    Response.Write("Seat Is Already Taken, Please Choose a Different Seat.");

                //string msg = s.InviteSeat(id, screeningID, seatNumber);
                //Response.Redirect("Default.aspx?msg=" + msg);
                //Response.Write(msg);
            }
        }




    }

           






        
    }





   

