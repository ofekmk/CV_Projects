<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminMovieDetails.aspx.cs" Inherits="AdminMovieDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td>
                <asp:GridView ID="gv_details" runat="server" AutoGenerateColumns="False" 
                    Height="134px" Width="672px">
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="MovieID" 
                            DataNavigateUrlFormatString="ScreeningOfMovie.aspx?movieid={0}" 
                            DataTextField="MovieID" Text="movie" />
                        <asp:BoundField DataField="InvitationNumber" 
                            HeaderText="Nubmer Of Invitations" />
                        <asp:BoundField DataField="LastScreeningDate" 
                            HeaderText="Last Screening Date" />
                        <asp:BoundField DataField="DateEndOfMovie" HeaderText="Date End Of Movie" />
                        <asp:BoundField DataField="NumberOfScreenings" 
                            HeaderText="Number Of Screenings" />
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
                <asp:LinkButton ID="lbtn_backtoadmindfrommdetails" runat="server" 
                    CausesValidation="False" onclick="lbtn_backtoadmindfrommdetails_Click">Back To Admin&#39;s Default</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

