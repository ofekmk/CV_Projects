<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TicketPrint.aspx.cs" Inherits="TicketPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 197px;
        }
        .style2
        {
            width: 197px;
            height: 22px;
        }
        .style3
        {
            height: 22px;
        }
        .style4
        {
            width: 141px;
        }
        .style5
        {
            height: 22px;
            width: 141px;
        }
        .style6
        {
            width: 216px;
        }
        .style7
        {
            height: 22px;
            width: 216px;
        }
        .style8
        {
            width: 197px;
            height: 43px;
        }
        .style9
        {
            width: 141px;
            height: 43px;
        }
        .style10
        {
            width: 216px;
            height: 43px;
        }
        .style11
        {
            height: 43px;
        }
        .style12
        {
            width: 197px;
            height: 41px;
        }
        .style13
        {
            width: 141px;
            height: 41px;
        }
        .style14
        {
            width: 216px;
            height: 41px;
        }
        .style15
        {
            height: 41px;
        }
        .style16
        {
            width: 197px;
            height: 34px;
        }
        .style17
        {
            width: 141px;
            height: 34px;
        }
        .style18
        {
            width: 216px;
            height: 34px;
        }
        .style19
        {
            height: 34px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Please Bring This Ticket To The Cinema. Thank You.</div>
    <table frame="box" style="width: 100%; height: 291px;">
        <tr>
            <td class="style2">
                <asp:Label ID="lbl_movie_name_printpage" runat="server" Text="Movie"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="lbl_date_printpage" runat="server" Text="Date"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lbl_time" runat="server" Text="Time"></asp:Label>
            </td>
            <td class="style3">
                <asp:Label ID="lbl_cinema_printpage" runat="server" Text="Cinema"></asp:Label>
            </td>
            <td class="style3">
                <asp:Label ID="lbl_seat_printpage" runat="server" Text="Seat"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style12">
                <asp:Label ID="lbl_movie_from_db" runat="server"></asp:Label>
            </td>
            <td class="style13">
                <asp:Label ID="lbl_date_from_db" runat="server"></asp:Label>
            </td>
            <td class="style14">
                <asp:Label ID="lbl_time_from_db" runat="server"></asp:Label>
            </td>
            <td class="style15">
                <asp:Label ID="lbl_cinema_from_db" runat="server"></asp:Label>
            </td>
            <td class="style15">
                <asp:Label ID="lbl_seat_from_db" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                *Deleting Your Movie Ticket Is Available Untill 48 Hourse Before The Screening.</td>
            <td class="style4">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:LinkButton ID="lbtn_homepage" runat="server" onclick="lbtn_homepage_Click">HomePage</asp:LinkButton>
            </td>
            <td class="style4">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </form>
</body>
</html>
