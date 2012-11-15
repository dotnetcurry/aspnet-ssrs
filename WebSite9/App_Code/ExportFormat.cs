using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public enum ReportFormats
{
    Html = 1,
    MHtml,
    Pdf,
    Xlsx,
    Docx
}

public class ReportFormat
{
    static ReportFormat()
    {
        Html = new ReportFormat { Code = ReportFormats.Html, Instruction = "HTML4.0" };
        MHtml = new ReportFormat { Code = ReportFormats.MHtml, Instruction = "MHTML" };
        Pdf = new ReportFormat { Code = ReportFormats.Pdf, Instruction = "PDF" };
        Xlsx = new ReportFormat { Code = ReportFormats.Xlsx, Instruction = "EXCELOPENXML" };
        Docx = new ReportFormat { Code = ReportFormats.Docx, Instruction = "WORDOPENXML" };
    }

    private ReportFormat()
    {
    }

    public ReportFormats Code { get; set; }
    public String Instruction { get; set; }

    public static ReportFormat Html { get; private set; }
    public static ReportFormat MHtml { get; private set; }
    public static ReportFormat Pdf { get; private set; }
    public static ReportFormat Xlsx { get; private set; }
    public static ReportFormat Docx { get; private set; }

    public static ReportFormat ByCode(ReportFormats code)
    {
        switch (code)
        {
            case ReportFormats.Html: return Html;
            case ReportFormats.MHtml: return Html; //<<======================
            case ReportFormats.Pdf: return Pdf;
            case ReportFormats.Xlsx: return Xlsx;
            case ReportFormats.Docx: return Docx;
            default: return null;
        }
    }
}


