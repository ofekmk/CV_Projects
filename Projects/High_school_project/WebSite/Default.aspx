<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 229px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Image ID="Image1" runat="server" Height="223px" 
            ImageUrl="~/images/KolnoaOK2.JPG" Width="924px" />
    
    </div>
    <table style="width:100%;">
        <tr>
            <td class="style1">
                <asp:Menu ID="Menu1" runat="server">
                    <Items>
                        <asp:MenuItem Text="Menu" Value="Menu">
                            <asp:MenuItem NavigateUrl="~/Register.aspx" Text="Register" Value="Register">
                            </asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/Search_Option1.aspx" Text="Search" Value="Search">
                            </asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/PrivateProfile.aspx" Text="Watch Private Profile" 
                                Value="Watch Private Profile"></asp:MenuItem>
                        </asp:MenuItem>
                    </Items>
                </asp:Menu>
            </td>
            <td>
                <asp:MultiView ID="MultiViewloghompage" runat="server" Visible="False">
                    <asp:View ID="viewlogout" runat="server">
                        <asp:LinkButton ID="lbtn_logouthomepage" runat="server" 
                            onclick="lbtn_logouthomepage_Click"><center>Log Out</center></asp:LinkButton>
                    </asp:View>
                    <asp:View ID="viewloginhompage" runat="server">
                        <asp:LinkButton ID="lbtn_login" runat="server" onclick="lbtn_login_Click"><center>Log In</center></asp:LinkButton>
                    </asp:View>
                </asp:MultiView>
            </td>
            <td>
        <asp:MultiView ID="MultiView1" runat="server" Visible="False">
            <asp:View ID="View_admin" runat="server">
                <asp:LinkButton ID="lbtn_gotoadmindefalut" runat="server" 
                    onclick="lbtn_gotoadmindefalut_Click"><center>Admin</center></asp:LinkButton>
            </asp:View>
            <asp:View ID="View1" runat="server">
            </asp:View>
        </asp:MultiView>
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
