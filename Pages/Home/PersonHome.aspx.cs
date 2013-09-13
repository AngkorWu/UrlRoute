using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Home_PersonHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("Person Name:" + RouteData.Values["userName"].ToString());
        Response.Write("<br />");
        Response.Write("Person Age:" + RouteData.Values["age"].ToString());
    }
}