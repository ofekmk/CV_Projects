<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MovieDetails.aspx.cs" Inherits="MovieDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 84px;
        }
        .style2
        {
            width: 84px;
            height: 39px;
        }
        .style3
        {
            height: 39px;
        }
        .style4
        {
            width: 84px;
            height: 33px;
        }
        .style5
        {
            height: 33px;
        }
        .style6
        {
            width: 84px;
            height: 35px;
        }
        .style7
        {
            height: 35px;
        }
        .style8
        {
            width: 84px;
            height: 40px;
        }
        .style9
        {
            height: 40px;
        }
        .style10
        {
            width: 531px;
        }
        .style11
        {
            height: 33px;
            width: 531px;
        }
        .style12
        {
            height: 35px;
            width: 531px;
        }
        .style13
        {
            height: 39px;
            width: 531px;
        }
        .style14
        {
            height: 40px;
            width: 531px;
        }
        .style15
        {
            width: 54px;
        }
        .style16
        {
            height: 33px;
            width: 54px;
        }
        .style17
        {
            height: 35px;
            width: 54px;
        }
        .style18
        {
            height: 39px;
            width: 54px;
        }
        .style19
        {
            height: 40px;
            width: 54px;
        }
    .style20
    {
        width: 127px;
        height: 158px;
    }
    .style21
    {
        height: 158px;
        width: 531px;
        color: #009999;
    }
    .style22
    {
        height: 158px;
        width: 131px;
    }
    .style23
    {
        height: 158px;
        width: 54px;
    }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p style="margin-bottom: 13px">
        <table style="width:103%; height: 348px;">
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style10">
                    <asp:Image ID="img_movie" runat="server" style="margin-left: 0px" Height="147px" 
                        Width="345px" />
                </td>
                <td>
                    &nbsp;</td>
                <td class="style15">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td align="center" class="style10">
                    <asp:Label ID="lbl_MovieName" runat="server" Font-Size="XX-Large"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td class="style15">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style4">
                    Length:</td>
                <td class="style11">
                    <asp:Label ID="lbl_length" runat="server" style="color: #FFFFFF"></asp:Label>
                </td>
                <td class="style5">
                    </td>
                <td class="style16">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style6">
                    Year Filmed:</td>
                <td class="style12">
                    <asp:Label ID="lbl_yeafilmed" runat="server" style="color: #FFFFFF"></asp:Label>
                </td>
                <td class="style7">
                    </td>
                <td class="style17">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style20">
                    Director:</td>
                <td class="style21">
                    <asp:Label ID="lbl_director" runat="server" style="color: #FFFFFF"></asp:Label>
                </td>
                <td class="style22">
                    &nbsp;</td>
                <td class="style23">
                    </td>
            </tr>
            <tr>
                <td class="style4">
                    Actor:</td>
                <td class="style11">
                    <asp:Label ID="lbl_actor" runat="server" style="color: #FFFFFF"></asp:Label>
                </td>
                <td class="style5">
                    </td>
                <td class="style16">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    Genre:</td>
                <td class="style13">
                    <asp:Label ID="lbl_genre" runat="server"></asp:Label>
                </td>
                <td class="style3">
                    </td>
                <td class="style18">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style8">
                    Age Limit:</td>
                <td class="style14">
                    <asp:Label ID="lbl_agelimit" runat="server"></asp:Label>
                </td>
                <td class="style9">
                    </td>
                <td class="style19">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style8">
                    <asp:Label ID="lbl_moviesrate" runat="server" Text="Movie's Rate:"></asp:Label>
                </td>
                <td class="style14">
                    <asp:Label ID="lbl_moviesratedb" runat="server"></asp:Label>
                </td>
                <td class="style9">
                    &nbsp;</td>
                <td class="style19">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style8">
                    <asp:LinkButton ID="lbtn_hp" runat="server" onclick="lbtn_hp_Click">HomePage</asp:LinkButton>
                </td>
                <td class="style14">
                    <asp:LinkButton ID="lbtn_search1" runat="server" onclick="lbtn_search1_Click">Back To Search</asp:LinkButton>
                </td>
                <td class="style9">
                    &nbsp;</td>
                <td class="style19">
                    &nbsp;</td>
            </tr>
        </table>
    </p>
</asp:Content>

