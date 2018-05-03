<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserInvitationDetails.aspx.cs" Inherits="UserInvitationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 53px;
        }
        .style2
        {
            height: 57px;
        }
    .style3
    {
        width: 501px;
    }
    .style4
    {
        height: 57px;
        width: 501px;
    }
    .style5
    {
        height: 53px;
        width: 501px;
    }
        .style18
        {
            height: 14px;
            width: 88px;
            
        }
        .style19
        {
            height: 53px;
            width: 88px;
        }
        .style20
        {
            width: 88px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:103%; height: 317px;">
    <tr>
        <td class="style3">
            <asp:GridView ID="gv_usertickets" runat="server" AutoGenerateColumns="False" 
                Height="145px" onrowcommand="gv_usertickets_RowCommand" 
                onselectedindexchanged="Page_Load" Width="251px">
                <Columns>
                    <asp:BoundField DataField="MovieID" HeaderText="Movie" />
                    <asp:BoundField DataField="SeatNumber" HeaderText="Seat" />
                    <asp:BoundField DataField="CinemaID" HeaderText="Cinema" />
                    <asp:BoundField DataField="Price" HeaderText="Price" />
                    <asp:BoundField DataField="InvitationDate" HeaderText="Date Invited" />
                    <asp:BoundField DataField="Date" HeaderText="Screening Date" />
                    <asp:ButtonField ButtonType="Button" CommandName="CancelInvitation" 
                        Text="Cancel" />
                </Columns>
            </asp:GridView>
        </td>
        <td>
            &nbsp;</td>
        <td class="style20">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style4">
            <asp:LinkButton ID="lbtn_pp" runat="server" onclick="lbtn_pp_Click">Private Profile</asp:LinkButton>
            </td>
        <td class="style2">
            </td>
        <td class="style18">
            </td>
    </tr>
    <tr>
        <td class="style5">
            &nbsp;</td>
        <td class="style1">
            </td>
        <td class="style19">
            </td>
    </tr>
</table>
</asp:Content>

