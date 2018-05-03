using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BL;

//localhost4



/// <summary>
/// Summary description for ValidationService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ValidationService : System.Web.Services.WebService {

    public ValidationService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]

    public string CheckMoviesName(string MovieID)
    {

        WebSystem act = new WebSystem();
        string msg = act.CheckMoviesName(MovieID);
        return msg;
    }

    [WebMethod]

    public string CheckUserIDRegister(int UserID)
    {
        WebSystem act = new WebSystem();
        string msg = act.CheckUserIDRegister(UserID);
        return msg;
    }
  

    
}

