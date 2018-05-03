<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminAddNewScreening.aspx.cs" Inherits="AdminAddNewScreening" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style13
        {
            width: 356px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:103%; height: 360px;">
        <tr>
            <td class="style13">
                Date:</td>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td>
                <asp:TextBox ID="txt_datenewscreening" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfv_date" runat="server" 
                                ControlToValidate="txt_datenewscreening" 
                                ErrorMessage="the date of the screening is required"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style13">
                Cinema:</td>
            <td>
                <asp:DropDownList ID="ddl_cinemanewscreening" runat="server">
                    <asp:ListItem>Cinema</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style13">
                Price:</td>
            <td>
                <asp:TextBox ID="txt_pricenewscreening" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txt_pricenewscreening" ErrorMessage="Must Input Price"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style13">
                Movie:</td>
            <td>
                <asp:DropDownList ID="ddl_movienewscreening" runat="server">
                    <asp:ListItem>Movie</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style13">
                <asp:Button ID="btn_addnewscreening" runat="server" 
                    onclick="btn_addnewscreening_Click" Text="Add" Width="53px" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style13">
                <asp:LinkButton ID="lbtn_backtoadminsdefault" runat="server" 
                    CausesValidation="False" onclick="lbtn_backtoadminsdefault_Click1">Back To Admin&#39;s Default</asp:LinkButton>
            </td>
            <td>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

