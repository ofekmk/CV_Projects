<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 1px;
        }
        .style2
        {
            height: 6px;
        }
        .style3
        {
            height: 26px;
        }
        .style4
        {
        width: 222px;
    }
        .style5
        {
        height: 26px;
        width: 222px;
    }
        .style6
        {
            height: 11px;
            width: 222px;
        }
    .style7
    {
        height: 11px;
    }
        .style17
        {
            width: 140px;
        }
        .style18
        {
            height: 14px;
            width: 140px;
        }
        .style19
        {
            height: 11px;
            width: 140px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    
    <div>
    
    </div>
    <table style="width:106%; height: 362px;">
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_ID" runat="server" Text="UserID:"></asp:Label>
            </td>
            <td class="style4">
                <asp:TextBox ID="txt_userID" runat="server" Height="19px" Width="128px"></asp:TextBox>
                </td>
            <td class="style17">
                *</td>
            <td>
                <asp:RequiredFieldValidator ID="rfv_uid" runat="server" 
                    ControlToValidate="txt_userID" ErrorMessage="Must Input UserID "></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_fname" runat="server" Text="First Name:"></asp:Label>
            </td>
            <td class="style4">
                <asp:TextBox ID="txt_fname" runat="server" 
                    ontextchanged="txt_fname_TextChanged"></asp:TextBox>
            </td>
            <td class="style17">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                <asp:Label ID="lbl_lname" runat="server" Text="Last Name:"></asp:Label>
            </td>
            <td class="style5">
                <asp:TextBox ID="txt_lname" runat="server" ontextchanged="txt_TextChanged"></asp:TextBox>
            </td>
            <td class="style18">
                </td>
            <td class="style3">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_age" runat="server" Text="Year Of Birth:"></asp:Label>
            </td>
            <td class="style4">
                <asp:DropDownList ID="ddl_years" runat="server">
                    <asp:ListItem>Year</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style17">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_country" runat="server" Text="Country:"></asp:Label>
            </td>
            <td class="style4">
                <asp:TextBox ID="txt_country" runat="server"></asp:TextBox>
            </td>
            <td class="style17">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_gender" runat="server" Text="Gender:"></asp:Label>
            </td>
            <td class="style4">
                <asp:RadioButtonList ID="rbl_gender" runat="server">
                    <asp:ListItem>Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="style17">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style7">
                <asp:Label ID="lbl_email" runat="server" Text="Email:"></asp:Label>
            </td>
            <td class="style6">
                <asp:TextBox ID="txt_email" runat="server" ontextchanged="TextBox4_TextChanged"></asp:TextBox>
            </td>
            <td class="style19">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="txt_email" ErrorMessage="Must be [text]@[text].[text]" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            <td class="style7">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="lbl_creditcard" runat="server" Text="Credit Card:"></asp:Label>
            </td>
            <td class="style5">
                <asp:TextBox ID="txt_creditcard" runat="server" Height="22px" Width="128px"></asp:TextBox>
                </td>
            <td class="style18">
                *<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txt_creditcard" ErrorMessage="CreditCard Required"></asp:RequiredFieldValidator>
            </td>
            <td class="style3">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lbl_password" runat="server" Text="Password:"></asp:Label>
            </td>
            <td class="style4">
                <asp:TextBox ID="txt_password" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td class="style17">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txt_password" ErrorMessage="Must Input Password"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_password2" runat="server" Text="ReWrite Password:"></asp:Label>
            </td>
            <td class="style4">
                <asp:TextBox ID="txt_password2" runat="server" 
                    ontextchanged="TextBox1_TextChanged" TextMode="Password"></asp:TextBox>
            </td>
            <td class="style17">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txt_password2" ErrorMessage="Must Input Password"></asp:RequiredFieldValidator>
                </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Button ID="btn_register" runat="server" onclick="Button1_Click" 
                    Text="Done " Width="75px" style="height: 26px" />
            </td>
            <td class="style4">
                &nbsp;</td>
            <td class="style17">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:LinkButton ID="lbtn_hp1" runat="server" onclick="lbtn_hp1_Click" 
                    CausesValidation="False">Home Page</asp:LinkButton>
            </td>
            <td class="style4">
                &nbsp;</td>
            <td class="style17">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
   
</asp:Content>

