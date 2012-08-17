<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" MasterPageFile="Main.master" Title="Login" %>
<asp:Content ContentPlaceHolderID="content" Runat="Server">
    <h2>Login</h2>
    <table>
        <tr>
            <td align="right">User:</td>
            <td><asp:TextBox runat="server" ID="inputUser"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator runat="server" ControlToValidate="inputUser">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="right">Password:</td>
            <td><asp:TextBox runat="server" ID="inputPassword" TextMode="Password"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator runat="server" ControlToValidate="inputPassword">*</asp:RequiredFieldValidator></td>
        </tr>
    </table>
    <asp:Button runat="server" OnClick="cmdLogin_Click" Text="Login"/><br/>
    <asp:CustomValidator ID="CustomValidator1" runat="server" OnServerValidate="ValidateAuthentication">Login failed: username or password is incorrect</asp:CustomValidator>
</asp:Content>
