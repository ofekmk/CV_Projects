<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:103%; height: 365px;">
    <tr>
        <td>
            <asp:Label ID="lbl_ID" runat="server" Text="UserID:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txt_ID" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvuserlogin" runat="server" 
                ControlToValidate="txt_ID" ErrorMessage="Must Input UserID"></asp:RequiredFieldValidator>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lbl_password" runat="server" Text="Password:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txt_password" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txt_password" ErrorMessage="Password Is Required"></asp:RequiredFieldValidator>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btn_login" runat="server" Height="35px" 
                onclick="btn_login_Click" Text="Log In" Width="124px" />
        </td>
        <td>
            <asp:Label ID="lbl_error_msg" runat="server"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lb_register" runat="server" onclick="lb_register_Click" 
                CausesValidation="False">Register</asp:LinkButton>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
</asp:Content>

