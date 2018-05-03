using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using localhost3;


public partial class AdminSetPrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
      
protected void  btn_setprices_Click(object sender, EventArgs e)
{

        string stringsundaytothrusday = txt_stot.Text;
        int sundaytothursday = int.Parse(stringsundaytothrusday);
        string sweekend = txt_weekend.Text;
        int weekend = int.Parse(sweekend);
        string svip = txt_vip.Text;
        int vip = int.Parse(svip);

        AdminActions ac = new AdminActions();
        ac.UpdateTicketPrice(sundaytothursday, weekend);
        ac.VipPrice(vip);
        Response.Write("Prices Changed");
    }










protected void lbtn_backtoadmindfltfromsetprice_Click(object sender, EventArgs e)
{
    Response.Redirect("AdminDefault.aspx");
}
}
