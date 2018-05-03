<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UpdatePersonalInformation.aspx.cs" Inherits="UpdatePersonalInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:103%;">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
            </td>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_useridupdatepage1" runat="server" Text="User ID:"></asp:Label>
            </td>
            <td class="style1">
                <asp:Label ID="lbl_useridupdatepage2" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_fname_updatepage" runat="server" Text="First Name:"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="txt_fname_updatepage" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_lname_updatepage" runat="server" Text="Last Name:"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="txt_lname_updatepage" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_age_updatepage" runat="server" Text="Age:"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="txt_age_updatepage" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_country_updatepage" runat="server" Text="Country:"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="txt_country_updatepage" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_gender_updatepage" runat="server" Text="Gender:"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="txt_gender_updatepage" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_email_updatepage" runat="server" Text="Email:"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="txt_email_updatepage" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="CreditCard_updatepage" runat="server" Text="Credit Card Number:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_creditcard_updatepage" runat="server"></asp:TextBox>
            </td>
        </tr>
      
        <tr>
            <td>
                <asp:Button ID="btn_updateprofile" runat="server" Text="Update" 
                    onclick="btn_updateprofile_Click" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lnkbtn_backtouserprofile" runat="server" 
                    onclick="lnkbtn_backtouserprofile_Click" CausesValidation="False">Back To Profile</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
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
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

