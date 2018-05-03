using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using localhost;
using localhost2;

public partial class UserInvitationDetails : System.Web.UI.Page
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
                string sID = cookie["UserID"].ToString();
                int id = int.Parse(sID);
                WS_UserProfile up = new WS_UserProfile();
                DataTable dt = up.UserTicketView(id);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                Session.Add("ticketTable", ds);
                gv_usertickets.DataSource = ds;
                gv_usertickets.DataBind();








            }




        }
    }

    protected void gv_usertickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("CancelInvitation"))
        {
            HttpCookie cookie = Request.Cookies["Details"];
            if (cookie == null)
                Response.Redirect("Login.aspx");
            else
            {
                string sID = cookie["UserID"].ToString();
                int id = int.Parse(sID);
                DataSet ds = (DataSet)Session["ticketTable"];
                DataTable dt = ds.Tables[0];
                int i = int.Parse(e.CommandArgument.ToString());
                string sinvitationid = dt.Rows[i]["InvitationID"].ToString();
                int invitationid = int.Parse(sinvitationid);
                WS_UserProfile up = new WS_UserProfile();
                string msg = up.CancelInvite(invitationid);
                Response.Write(msg);
                Response.Redirect("UserInvitationDetails.aspx");
                //if (msg == "Invitation Has Been Canceled")

                //    Response.Write(msg);
                //    //string sprice = dt.Rows[i]["Price"].ToString();
                //    //int price = int.Parse(sprice);
                //else
                //    Response.Write
            }
        }
    }












    protected void lbtn_pp_Click(object sender, EventArgs e)
    {
        Response.Redirect("PrivateProfile.aspx");
    }
}
