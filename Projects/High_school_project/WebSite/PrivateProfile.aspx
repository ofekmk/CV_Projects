<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PrivateProfile.aspx.cs" Inherits="PrivateProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            height: 31px;
        }
        .style3
        {
            height: 28px;
        }
        .style4
        {
            width: 180px;
        }
        .style5
        {
            height: 31px;
            width: 180px;
        }
        .style6
        {
            height: 28px;
            width: 180px;
        }
        .style7
        {
            height: 30px;
        }
        .style8
        {
            height: 30px;
            width: 180px;
        }
        .style9
        {
            width: 155px;
        }
        .style10
        {
            height: 31px;
            width: 155px;
        }
        .style11
        {
            height: 28px;
            width: 155px;
        }
        .style12
        {
            height: 30px;
            width: 155px;
        }
    .style17
    {
        width: 141px;
        height: 26px;
    }
    .style18
    {
        width: 77px;
        height: 26px;
    }
    .style19
    {
        height: 26px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        &nbsp;</p>
    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton 
            ID="LinkButton2" runat="server" onclick="LinkButton2_Click">Change Password</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lbtn_UpdateInfo" runat="server" 
            onclick="lbtn_UpdateInfo_Click">Update/Change Your Personal Information</asp:LinkButton>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:LinkButton ID="btn_userinvitations" runat="server" 
            onclick="btn_userinvitations_Click">View/Delete Personal Invitations</asp:LinkButton>
    </p>
    <p>
                <asp:Label ID="lbl_privateprofile" runat="server" 
            Text="Private Profile:"></asp:Label>
    <table style="width: 105%; margin-bottom: 0px; height: 330px;">
        <tr>
            <td class="style9">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style17">
                <asp:Label ID="lbl_useridprivateprofile1" runat="server" Text="User ID:"></asp:Label>
            </td>
            <td class="style18">
                <asp:Label ID="lbl_useridprivateprofile2" runat="server"></asp:Label>
            </td>
            <td class="style19">
                </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="lbl_fnameprivateprofile1" runat="server" Text="First Name:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="lbl_fnameprivateprofile2" runat="server"></asp:Label>
            </td>
            <td class="style2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style11">
                <asp:Label ID="lbl_lnameprivateprofile1" runat="server" Text="Last Name:"></asp:Label>
            </td>
            <td class="style6">
                <asp:Label ID="lbl_lnameprivateprofile2" runat="server"></asp:Label>
            </td>
            <td class="style3">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style11">
                <asp:Label ID="lbl_ageprivateprofile1" runat="server" Text="Age:"></asp:Label>
            </td>
            <td class="style6">
                <asp:Label ID="lbl_ageprivateprofile2" runat="server"></asp:Label>
            </td>
            <td class="style3">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style11">
                <asp:Label ID="lbl_countryprivateprofile1" runat="server" Text="Country:"></asp:Label>
            </td>
            <td class="style6">
                <asp:Label ID="lbl_countryprivateprofile2" runat="server"></asp:Label>
            </td>
            <td class="style3">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style11">
                <asp:Label ID="lbl_genderprivateprofile1" runat="server" Text="Gender:"></asp:Label>
            </td>
            <td class="style6">
                <asp:Label ID="lbl_genderprivateprofile2" runat="server"></asp:Label>
            </td>
            <td class="style3">
            </td>
        </tr>
        <tr>
            <td class="style12">
                <asp:Label ID="lbl_emailprivateprofile1" runat="server" Text="Email:"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lbl_emailprivateprofile2" runat="server"></asp:Label>
            </td>
            <td class="style7">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style8">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
        </tr>
        </table>
    </p>
    </asp:Content>

