<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search_Option1.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 37px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 103%; height: 352px; margin-bottom: 0px;">
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_search" runat="server" Text="Search By:"></asp:Label>
            </td>
            <td class="style1">
                &nbsp;</td>
            <td class="style1">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_MoviesTable" runat="server" Text="Movie's Name:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddl_MoviesNames" runat="server" 
                    onselectedindexchanged="ddl_MoviesNames_SelectedIndexChanged" 
                    Height="16px" Width="76px" AutoPostBack="True">
                    <asp:ListItem>Movie</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_DateTable" runat="server" Text="Date:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddl_ScreeningDates" runat="server" 
                    onselectedindexchanged="ddl_ScreeningDates_SelectedIndexChanged" 
                    AutoPostBack="True">
                    <asp:ListItem>Date</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_GenreTable" runat="server" Text="Genre:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddl_Genres" runat="server" 
                    onselectedindexchanged="ddl_Genres_SelectedIndexChanged" 
                    AutoPostBack="True">
                    <asp:ListItem>Genre</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lbtn_hp2" runat="server" onclick="lbtn_hp2_Click">Home Page</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

