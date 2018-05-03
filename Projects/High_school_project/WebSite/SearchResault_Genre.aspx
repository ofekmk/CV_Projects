<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchResault_Genre.aspx.cs" Inherits="SearchResault_Genre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%; height: 220px;">
        <tr>
            <td>
                <asp:GridView ID="gv_searchresault_genre" runat="server" 
                    AutoGenerateColumns="False" onrowcommand="gv_searchresault_genre_RowCommand" 
                    onselectedindexchanged="Page_Load">
                    <Columns>
                        <asp:ButtonField ButtonType="Button" CommandName="Order" Text="Order Ticket" />
                        <asp:BoundField DataField="Date" HeaderText="Date" />
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
                <asp:LinkButton ID="lbtn_back_to_search_from_genretable" runat="server" 
                    onclick="lbtn_back_to_search_from_genretable_Click">Back To Search</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

