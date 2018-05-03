<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BuyingConfirmation.aspx.cs" Inherits="BuyingConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 48px;
        }
        .style2
        {
            height: 55px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 103%; height: 338px;">
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_price" runat="server" Text="Ticket Price:"></asp:Label>
            </td>
            <td class="style1">
                <asp:Label ID="lbl_ticketprice_from_db" runat="server"></asp:Label>
&nbsp;$</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_credit" runat="server" Text="CreditCard Number:"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="txt_creditcard" runat="server"></asp:TextBox>
            </td>
            <td class="style1">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txt_creditcard" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="lbl_pass_buying_confirmation" runat="server" 
                    Text="Your Password:"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="txt_pass_buyingconfirmation" runat="server" 
                    TextMode="Password"></asp:TextBox>
            </td>
            <td class="style2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txt_pass_buyingconfirmation" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Btn_Buy" runat="server" Height="29px" Text="Buy Now!" 
                    Width="85px" onclick="Btn_Buy_Click" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lbtn" runat="server" onclick="lbtn_Click" 
                    CausesValidation="False">HomePage</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

