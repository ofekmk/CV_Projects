using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using localhost;
using localhost2;
using localhost3;
using localhost4;

public partial class AdminDeleteUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //HttpCookie cookie = Request.Cookies["Details"];
            //if (cookie == null)
            //    Response.Redirect("Login.aspx");


            AdminActions ac = new AdminActions();
            DataTable dt = ac.AllUsersDetails();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Session.Add("usersTable", ds);
            gv_deleteusers.DataSource = ds;
            gv_deleteusers.DataBind();
        }

    }
    protected void gv_deleteusers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DeleteUser"))
        {
            HttpCookie cookie = Request.Cookies["Details"];
            if (cookie == null)
                Response.Redirect("Login.aspx");
            else
            {
                WS_UserProfile ws = new WS_UserProfile();
                DataSet ds = (DataSet)Session["usersTable"];
                DataTable dt = ds.Tables[0];
                int i = int.Parse(e.CommandArgument.ToString());
                int id = int.Parse(dt.Rows[i]["UserID"].ToString());
                AdminActions ac = new AdminActions();
                DataTable dt2 = ws.UserTicketView(id);
                for (int q = 0; q < dt2.Rows.Count; q++)
                {
                    int invitation = int.Parse(dt2.Rows[q]["InvitationID"].ToString());
                    ws.CancelInvite(invitation);
                }
                int j = ac.DeleteUser(id);
                if (j == 1)
                    Response.Write("User Was Deleted");
                else
                    Response.Write("Delete Failed");
                Response.Redirect("AdminDeleteUsers.aspx");
            }
        }
    }

    protected void lbtn_toadmindfromusers_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminDefault.aspx");
    }
}

