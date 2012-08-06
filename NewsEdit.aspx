<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsEdit.aspx.cs" MasterPageFile="Main.master"
    Inherits="NewsEdit" Title="News - Edit" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
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
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:HyperLink runat="server" ImageUrl="images/add.png" NavigateUrl="NewsItemEdit.aspx" />
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td valign="top">
                    <asp:HyperLink runat="server" ImageUrl="images/edit.png" NavigateUrl='<%# "NewsItemEdit.aspx?Source=" + Eval("Source") %>' />
                    <asp:ImageButton runat="server" ImageUrl="images/delete.png" Width="16" Height="16"
                        OnCommand="cmdDelete_Click" CommandArgument='<%# Eval("Source") %>' />
                </td>
                <td>
                    <div>
                        <h2 style="font-size: 14px"><%# Eval("Title") %></h2>
                        <h4 style="color: Gray; font-size: 10px;"><%# Eval("ReleaseDate", "{0:MMMM d, yyyy}") %>
                        </h4>
                    </div>
                    <div>
                        <h5>
                            <%# Eval("Content") %>
                        </h5>
                    </div>
                    <asp:ImageButton runat="server" ImageUrl="images/switch.png" OnCommand="cmdSwitch_Click"
                        CommandArgument='<%# Eval("Source") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
