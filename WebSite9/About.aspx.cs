using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class About : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportFormat myformat = null;
        myformat = ReportFormat.ByCode(ReportFormats.Xlsx);
        string Instruction = myformat.Instruction;
    }
}
