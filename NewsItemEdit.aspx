<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsItemEdit.aspx.cs" MasterPageFile="Main.master"
    Inherits="ArticleEdit" Title="Article - Edit" ValidateRequest="false" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
        <h1>News Article - Edit</h1>
    <h2 style="font-size:14px;display:inline">Title:</h2><asp:RequiredFieldValidator runat="server" ControlToValidate="inputTitle" Display="Dynamic">*</asp:RequiredFieldValidator><br/>
    <asp:TextBox ID="inputTitle" runat="server" Columns="100" /><br /><br />
    <h2 style="font-size:14px;display:inline">Date:</h2>
    <asp:RequiredFieldValidator runat="server" ControlToValidate="inputReleaseDate" Display="Dynamic">*</asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator runat="server" ControlToValidate="inputReleaseDate" ValidationExpression="\d\d\d\d-\d\d-\d\d" Display="Dynamic">&nbsp;yyyy-mm-dd</asp:RegularExpressionValidator>
    <asp:CustomValidator runat="server" OnServerValidate="validatorReleaseDate_Validate" Display="Dynamic">&nbsp;yyyy-mm-dd</asp:CustomValidator>
    <br/>
    <asp:TextBox runat="server" ID="inputReleaseDate" Columns="20" /><br/><br />
    <h2 style="font-size:14px;display:inline">Content:</h2><asp:RequiredFieldValidator runat="server" ControlToValidate="inputContent" Display="Dynamic">*</asp:RequiredFieldValidator><br/>
    <asp:TextBox ID="inputContent" runat="server" TextMode="MultiLine" Columns="100" Rows="10" /><br /><br />
    <asp:Button runat="server" OnClick="cmdSave_Click" Text="Save" /><input type="button" value="Cancel" onclick="location='NewsEdit.aspx'" />

</asp:Content>
