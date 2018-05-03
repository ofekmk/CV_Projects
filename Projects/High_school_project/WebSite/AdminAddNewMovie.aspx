<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminAddNewMovie.aspx.cs" Inherits="AdminAddNewMovie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .style1
    {
        width: 197px;
    }
    .style2
    {
        width: 131px;
    }
    .style13
    {
        width: 254px;
        height: 14px;
        
    }
    .style14
    {
        width: 583px;
        height: 104px;
    }
    .style15
    {
        width: 254px;
        height: 104px;
      
    }
    .style16
    {
        height: 104px;
    }
        .style17
        {
            width: 521px;
        }
        .style18
        {
            width: 521px;
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:103%; height: 383px;">
    <tr>
        <td class="style17">
            Movie&#39;s Name:&nbsp;
        </td>
        <td class="style13">
            <asp:TextBox ID="txt_mnameadmin" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txt_mnameadmin" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style17">
            Director:</td>
        <td class="style13">
            <asp:TextBox ID="txt_dnameadmin" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txt_dnameadmin" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style17">
            Main Actor:</td>
        <td class="style13">
            <asp:TextBox ID="txt_mainactioradmin" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="txt_mainactioradmin" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style17">
            Year Filmed:</td>
        <td class="style13">
            <asp:TextBox ID="txt_yearfilmedadmin" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="txt_yearfilmedadmin" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style17">
&nbsp;Length:</td>
        <td class="style13">
            <asp:TextBox ID="txt_lengthadmin" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ControlToValidate="txt_lengthadmin" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style17">
            Genre:</td>
        <td class="style13">
            <asp:DropDownList ID="ddl_genresadmin" runat="server">
                <asp:ListItem>Genre</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style18">
&nbsp;Rate:</td>
        <td class="style15">
            <asp:TextBox ID="txt_rateadmin" runat="server"></asp:TextBox>
        </td>
        <td class="style16">
            *Pay attention - you give the beginning rate<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_rateadmin" 
                ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style17">
            Date Start:</td>
        <td class="style13">
            <asp:TextBox ID="txt_datestartadmin" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ControlToValidate="txt_datestartadmin" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style17">
            Date End:</td>
        <td class="style13">
            <asp:TextBox ID="txt_dateendadmin" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                ControlToValidate="txt_dateendadmin" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style17">
            Age Limit
        </td>
        <td class="style13">
            <asp:TextBox ID="txt_agelimitadmin" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                ControlToValidate="txt_agelimitadmin" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style17">
            <asp:Button ID="btn_addnewmovie" runat="server" Text="Add New Movie" 
                onclick="btn_addnewmovie_Click" />
        </td>
        <td class="style13">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style17">
            <asp:LinkButton ID="lbtn_returntoadmindefaultfromaddnewmovie" runat="server" 
                onclick="lbtn_returntoadmindefaultfromaddnewmovie_Click" 
                CausesValidation="False">Return To Admin&#39;s Default</asp:LinkButton>
        </td>
        <td class="style13">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
</asp:Content>

