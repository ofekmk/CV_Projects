<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OrderTicket.aspx.cs" Inherits="OrderTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:103%; height: 106px;">
        <tr>
            <td style="width: 351px">
                <asp:Image ID="img_cinemaview" runat="server" Height="234px" Width="363px" 
                    ImageAlign="Top" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 351px">
    <asp:GridView ID="gv_cinema_status" runat="server" Height="257px" Width="287px" 
                    onselectedindexchanged="Page_Load" AutoGenerateColumns="False" 
                    onrowcommand="gv_cinema_status_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Seat" HeaderText="Seat" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:ButtonField ButtonType="Button" CommandName="InviteSeat" Text="Invite Now">
                        <FooterStyle ForeColor="#66FF33" />
                        </asp:ButtonField>
                        <asp:ImageField DataImageUrlField="SeatImage" HeaderText="Image">
                        </asp:ImageField>
                    </Columns>
                </asp:GridView>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 351px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

