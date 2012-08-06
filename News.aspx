<%@ Page Language="C#" AutoEventWireup="true" CodeFile="News.aspx.cs" MasterPageFile="Main.master" Inherits="News" Title="nonPareil Institute - News"%>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        td {
            padding-bottom: 10px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="content">
        <h1>nP.News</h1>
        <asp:Repeater ID="dataArticles" runat="server">
            <HeaderTemplate>
                <table style="padding: 30px">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                <td>
                    <div>
                        <h2 style="font-size:14px"><%# Eval("Title") %></h2>
                        <h4 style="color:Gray; font-size:10px;"><%# Eval("ReleaseDate", "{0:MMMM d, yyyy}") %></h4>
                    </div>
                    <div>
                        <h5>
                            <%# Eval("Content") %>
                        </h5>
                    </div>
                    </td>
                    </tr>
                </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
</asp:Content>
