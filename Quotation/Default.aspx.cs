using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Quotation_Default : System.Web.UI.Page
{
    public string RadStrSerch = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
        
        this.Button1.Attributes["onclick"] = "javascript:CustomAdd();";
    }
}