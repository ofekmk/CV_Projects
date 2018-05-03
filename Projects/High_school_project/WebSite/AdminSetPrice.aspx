<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminSetPrice.aspx.cs" Inherits="AdminSetPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .style1
    {
        width: 378px;
    }
        .style17
        {
            width: 106px;
        }
        .style18
        {
            width: 166px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:103%; height: 354px;">
    <tr>
        <td class="style1">
            Sunday To Thrusday:</td>
        <td class="style18">
            <asp:TextBox ID="txt_stot" runat="server"></asp:TextBox>
            $</td>
        <td class="style17">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txt_stot" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style1">
            Weekend Price Added(In %):</td>
        <td class="style18">
            <asp:TextBox ID="txt_weekend" runat="server"></asp:TextBox>
            %</td>
        <td class="style17">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txt_weekend" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style1">
            Vip Price Added(In %):</td>
        <td class="style18">
            <asp:TextBox ID="txt_vip" runat="server"></asp:TextBox>
            %</td>
        <td class="style17">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="txt_vip" ErrorMessage="Must Be Filled"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Button ID="btn_setprices" runat="server" Text="Set Prices" 
                onclick="btn_setprices_Click" />
        </td>
        <td class="style18">
            &nbsp;</td>
        <td class="style17">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            <asp:LinkButton ID="lbtn_backtoadmindfltfromsetprice" runat="server" 
                onclick="lbtn_backtoadmindfltfromsetprice_Click" CausesValidation="False">Back To Admin&#39;s Default</asp:LinkButton>
        </td>
        <td class="style18">
            &nbsp;</td>
        <td class="style17">
            &nbsp;</td>
    </tr>
</table>
</asp:Content>

