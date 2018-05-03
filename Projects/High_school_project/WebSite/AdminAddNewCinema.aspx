<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminAddNewCinema.aspx.cs" Inherits="AdminAddNewCinema" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
    *Notice: adding new cinema with different seat number will require to add a new 
    Cinema Map View.</p>
<p>
    <table style="width:100%;">
        <tr>
            <td class="style7" style="width: 112px">
                Genre:</td>
            <td style="width: 284px">
                <asp:TextBox ID="txt_genreaddnewcinema" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txt_genreaddnewcinema" ErrorMessage="Genre Is Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style7" style="width: 112px">
                Amount of seats:</td>
            <td style="width: 284px">
                <asp:TextBox ID="txt_seats_addnewcinema" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txt_seats_addnewcinema" 
                    ErrorMessage="Amount Of Seats Is Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style7" style="width: 112px">
                Cinema Type</td>
            <td style="width: 284px">
                <asp:RadioButtonList ID="rbl_kind_addnewcinema" runat="server">
                    <asp:ListItem>economy</asp:ListItem>
                    <asp:ListItem>vip</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style7" style="width: 112px">
                <asp:Button ID="btn_addnewcinema" runat="server" 
                    onclick="btn_addnewcinema_Click" Text="Add" Width="52px" />
            </td>
            <td style="width: 284px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style7" style="width: 112px">
                <asp:LinkButton ID="lbtn_backtoadmindefaultfromaddnewcinema" runat="server" 
                    onclick="lbtn_backtoadmindefaultfromaddnewcinema_Click" 
                    CausesValidation="False">Back To Admin&#39;s Default</asp:LinkButton>
            </td>
            <td style="width: 284px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</p>
</asp:Content>

