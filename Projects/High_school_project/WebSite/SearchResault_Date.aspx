<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchResault_Date.aspx.cs" Inherits="SearchResault_Date" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:103%; height: 361px;">
        <tr>
            <td>
                <asp:GridView ID="gv_searchresault_date" runat="server" Height="151px" 
                    onselectedindexchanged="Page_Load" Width="278px" 
                    AutoGenerateColumns="False" onrowcommand="gv_searchresault_date_RowCommand">
                    <Columns>
                        <asp:ButtonField ButtonType="Button" CommandName="Order" 
                            Text="Order Ticket/s" />
                        <asp:BoundField DataField="Date" HeaderText="Date" 
                            DataFormatString="{0:MM-dd-yyyy}" HtmlEncode=false/>
                        <asp:BoundField DataField="Time" HeaderText="Time" />
                        <asp:BoundField DataField="Price" HeaderText="Price" />
                        <asp:ButtonField CommandName="MovieDetails" DataTextField="MovieID" 
                            HeaderText="Movie" />
                    </Columns>
                </asp:GridView>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lbtn_backtosearch_from_date_table" runat="server" 
                    onclick="lbtn_backtosearch_from_date_table_Click">Back To Search</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

