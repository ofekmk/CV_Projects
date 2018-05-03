<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScreeningOfMovie.aspx.cs" Inherits="ScreeningOfMovie" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 9px;
        }
        .style2
        {
            height: 9px;
            width: 216px;
        }
        .style3
        {
            width: 216px;
        }
        .style4
        {
            height: 9px;
            width: 212px;
        }
        .style5
        {
            width: 212px;
        }
        .style6
        {
            width: 216px;
            height: 21px;
        }
        .style7
        {
            width: 212px;
            height: 21px;
        }
        .style8
        {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="height: 418px; width: 604px">
        <tr>
            <td class="style2">
                <asp:LinkButton ID="lb_movie" runat="server"></asp:LinkButton>
                <asp:GridView ID="gv_screeningsID" runat="server" 
                    Height="58px" Width="16px" 
                    onrowcommand="gv_screeningsID_RowCommand" 
                    AutoGenerateColumns="False" onselectedindexchanged="Page_Load">
                    <Columns>
                        <asp:BoundField DataField="Date" HeaderText="Date" />
                        <asp:BoundField DataField="Time" HeaderText="Time" />
                        <asp:BoundField DataField="Price" HeaderText="Price" />
                        <asp:BoundField DataField="InvitationsNum" HeaderText="Number Of Invitations" />
                        <asp:ButtonField ButtonType="Button" CommandName="DeleteScreening" 
                            Text="Delete Screening" />
                    </Columns>
                    <SelectedRowStyle BackColor="#99CCFF" />
                </asp:GridView>
            </td>
            <td class="style4">
                </td>
            <td class="style1">
                </td>
        </tr>
        <tr>
            <td class="style6">
            </td>
            <td class="style7">
                </td>
            <td class="style8">
                </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

