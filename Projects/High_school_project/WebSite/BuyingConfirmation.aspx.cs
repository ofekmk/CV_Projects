using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using localhost;

public partial class BuyingConfirmation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies["Details"];
            if (cookie == null)
                Response.Redirect("Login.aspx");
            else
            {
                Service s = new Service();
                string sScreeningID = Request.QueryString["screening"];
                int screeningID = int.Parse(sScreeningID);
                int price = s.TotalCost(screeningID);
                string sPrice = price.ToString();
                lbl_ticketprice_from_db.Text = sPrice;
            }


         




    }
}



    protected void Btn_Buy_Click(object sender, EventArgs e)
    {

        string sID;
        HttpCookie cookie = Request.Cookies["Details"];
        if (cookie == null)
            Response.Redirect("Login.aspx");
        else
        {

            Service s = new Service(); 
            sID = cookie["UserID"].ToString();
            int id = int.Parse(sID);
            string password = txt_pass_buyingconfirmation.Text;
            string screditcard = txt_creditcard.Text;
            int creditcard = int.Parse(screditcard);
            bool flag = s.BuyingConfirmation(id, creditcard, password);
            if (flag == true)
            {
                string sScreeningID = Request.QueryString["screening"];
                string sSeat = Request.QueryString["seat"];
                int screeningID = int.Parse(sScreeningID);
                int seat = int.Parse(sSeat);
                string msg = s.InviteSeat(id, screeningID, seat);
                if (msg == "Screening is unavilable")
                    Response.Write("Screening Is Unavailable");
                else
                    Response.Redirect("TicketPrint.aspx?screening=" + sScreeningID+"&seat="+sSeat);
            }
            else
                Response.Write("CreditCard Number or Password Do Not Exist");
        }

    }



    protected void lbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}
