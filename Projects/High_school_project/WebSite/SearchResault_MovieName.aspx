<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchResault_MovieName.aspx.cs" Inherits="SearchResault_Option1_" %>

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
                <asp:GridView ID="gv_searchresault_moviename" runat="server" 
                    Height="58px" Width="16px" 
                    onrowcommand="gv_searchresault_moviename_RowCommand" 
                    AutoGenerateColumns="False" onselectedindexchanged="Page_Load">
                    <Columns>
                        <asp:ButtonField ButtonType="Button" Text="Order Ticket/s" CommandName="Order">
                        <ItemStyle BackColor="Lime" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="Date" HeaderText="Date" />
                        <asp:BoundField DataField="Time" HeaderText="Time" />
                        <asp:BoundField DataField="Price" HeaderText="Price" />
                        <asp:ButtonField CommandName="MovieDetails" DataTextField="MovieID" 
                            HeaderText="Movie" />
                    </Columns>
                    <SelectedRowStyle BackColor="#99CCFF" />
                </asp:GridView>
    <table style="height: 418px; width: 604px">
        <tr>
            <td class="style2">
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
                <asp:LinkButton ID="lbtn_backtosearchoption1" runat="server" 
                    onclick="lbtn_backtosearchoption1_Click">Back To Search</asp:LinkButton>
            </td>
            <td class="style5">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

