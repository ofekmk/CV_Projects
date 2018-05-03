<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 249px;
        }
        .style17
        {
            width: 125px;
        }
        .style18
        {
            width: 119px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:103%; height: 354px;">
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_oldpassword" runat="server" Text="Old Password:"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td class="style17">
                <asp:TextBox ID="txt_oldpass" runat="server"></asp:TextBox>
            </td>
            <td class="style18">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txt_oldpass" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_newpass" runat="server" Text="New Password:"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td class="style17">
                <asp:TextBox ID="txt_newpass" runat="server"></asp:TextBox>
            </td>
            <td class="style18">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txt_newpass" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_password_confirmation" runat="server" 
                    Text="Password Confirmation:"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td class="style17">
                <asp:TextBox ID="txt_passconfirmation" runat="server"></asp:TextBox>
            </td>
            <td class="style18">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txt_passconfirmation" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Button ID="btn_changepass" runat="server" onclick="btn_changepass_Click" 
                    Text="Done" />
            </td>
            <td>
                &nbsp;</td>
            <td class="style17">
                &nbsp;</td>
            <td class="style18">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:LinkButton ID="lnkbtnbacktopproilefrompasschange" runat="server" 
                    onclick="lnkbtnbacktopproilefrompasschange_Click" CausesValidation="False">Back To Private Profile</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
            <td class="style17">
                &nbsp;</td>
            <td class="style18">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

