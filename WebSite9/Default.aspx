<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Calling SSRS and Send E-mail!
    </h2>
<h2>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Send Report" />
        <asp:Label ID="msgText" runat="server" style="color: #FF0000"></asp:Label>
    </h2>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</asp:Content>
