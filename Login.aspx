<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" MasterPageFile="Main.master" Title="Login" %>
<asp:Content ContentPlaceHolderID="content" Runat="Server">
    <h2>Login</h2>
    <table>
        <tr>
            <td align="right">User:</td>
            <td><asp:TextBox runat="server" ID="inputUser"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right">Password:</td>
            <td><asp:TextBox runat="server" ID="inputPassword" TextMode="Password"></asp:TextBox></td>
        </tr>
    </table>
    <asp:Button runat="server" OnClick="cmdLogin_Click" Text="Login"/>
</asp:Content>
