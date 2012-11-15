using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SSRS_ext;
using SSRS;
using System.Text;
using System.IO;
using System.Web.Services.Protocols;
using System.Drawing;
using SSRS.Common;
using System.Configuration;
using System.Web.Mail;

public partial class _Default : System.Web.UI.Page
{
    string SSRSLibrary = System.Configuration.ConfigurationManager.AppSettings["SSRSLibrary"];
    string graphicspath = System.Configuration.ConfigurationManager.AppSettings["graphicspath"];
    string webpath = System.Configuration.ConfigurationManager.AppSettings["webpath"];

    protected void Page_Load(object sender, EventArgs e)    {

        msgText.Text = "";
    }

    private string GenerateReport(Dictionary<string, string> dictParam, bool bGetImagePath, bool preservecomments, string Report_Name)
    {
        string reportPath = string.Empty;
        string sReportFileName = string.Empty;
        FileInfo fi = null;

        reportPath = SSRSLibrary + "/" + Report_Name;
       
        SSRS.ReportExecutionService rs1 = new SSRS.ReportExecutionService();
        rs1.Credentials = new System.Net.NetworkCredential(ClientConfiguration.WebServiceUserName, ClientConfiguration.WebServicePWD, ClientConfiguration.WebServiceUserDomain);

        ExecutionInfo2 execInfo = new ExecutionInfo2();
        execInfo = rs1.LoadReport2(reportPath, null);
        Int32 counter = 0;


        SSRS.ParameterValue[] parameters = new SSRS.ParameterValue[dictParam.Count];
        foreach (KeyValuePair<string, string> kvp in dictParam)
        {
            Console.WriteLine("Key = {0}, Value = {1}",
                kvp.Key, kvp.Value);

            // Prepare report parameters
           
            parameters[counter] = new SSRS.ParameterValue();
            parameters[counter].Name = kvp.Key;
            parameters[counter].Value = kvp.Value;
            counter = counter + 1;
        }
        rs1.SetExecutionParameters2(parameters, null);

        //See Readme for different Types    

        // Render arguments
        string encoding;
        string mimeType = "html/text";
        string extension;
        SSRS.Warning[] warnings = null;
        string[] streamIDs = null;
        string format = "HTML4.0";
        string devInfo = null;
        byte[] result = null;

        try
        {
            
            result = rs1.Render2(format, devInfo, PageCountMode.Estimate, out extension, out mimeType, out encoding, out warnings, out streamIDs);
            execInfo = rs1.GetExecutionInfo2();

            UTF8Encoding enc = new UTF8Encoding();
            string str = enc.GetString(result);
            //Hide Logo or parse string for anything
            str = str.Replace("<a", "<a style='display:none' ");
            string s = System.Text.ASCIIEncoding.ASCII.GetString(result);
            SendEmail(str, "yourname@yourdomain.com");


 
        }
        catch (SoapException ex)
        {
            Console.WriteLine(ex.Detail.OuterXml);
           
            return string.Empty;
        }

        sReportFileName =Report_Name;

        String ReportName = graphicspath + "\\" + sReportFileName + ".html";
        // Write the contents of the report to an MHTML file.
        fi = new FileInfo(ReportName);
        if (fi.Exists)
        {
            fi.Delete();
        }
        using (FileStream stream = File.Create(ReportName, result.Length))
        {
            stream.Write(result, 0, result.Length);
            stream.Close();
        }
      
           return string.Empty;


    }
    //********************************************************************************
    public bool SendEmail(string msg, string emailTo)
    {
        //********************************************************************************      
        string sMsg = "";
        string sSuccess = "";

        try
        {
            sMsg = msg;

            string sFrom = ConfigurationSettings.AppSettings["AdminEmailAddress"] + "";
            string sServer = ConfigurationSettings.AppSettings["SMTP_Postoffice"] + "";
            string sSMTP_Password = ConfigurationSettings.AppSettings["SMTP_Password"] + "";
            string Devemail = ConfigurationSettings.AppSettings["devEmail"] + "";

            string sName = "";
            string sEmail = "";

            if (!emailTo.Contains("@"))
            {
                sEmail = Devemail;
            }
            else
            {
                sEmail = emailTo;
            }

            MailMessage Mailer = new MailMessage();

            try
            {
                //Mailer.Bcc = "";
                //Mailer.Cc = "";
            }
            catch
            {
            }

            System.Web.Mail.SmtpMail.SmtpServer = sServer;
            Mailer.BodyFormat = System.Web.Mail.MailFormat.Html;

            Mailer.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
            Mailer.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = sFrom;
            Mailer.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = sSMTP_Password;
            var _with1 = Mailer;
            _with1.From = sFrom;
            _with1.To = sEmail;
            _with1.Subject = "An Action has been assigned to you " + sName + " please see below ";
            _with1.Body = sMsg;
            _with1.BodyFormat = System.Web.Mail.MailFormat.Html;

            SmtpMail.Send(Mailer);
            sSuccess = "Success";

        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        //Call Report with parameter Values
        Dictionary<string, string> dictParam =
     new Dictionary<string, string>();
        dictParam.Add("pRig", "Kendall 21");
        dictParam.Add("pStartDate", "3/14/2012");
        dictParam.Add("pEndDate", "11/30/2012");
       
        GenerateReport(dictParam, true, false, "Open Action Items By Rig");
        msgText.Text = "Message Sent!";
    }
}
