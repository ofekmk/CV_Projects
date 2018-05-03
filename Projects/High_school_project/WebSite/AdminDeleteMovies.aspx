<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminDeleteMovies.aspx.cs" Inherits="AdminDeleteMovies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .style16
    {
        height: 22px;
        width: 584px;
    }
    .style14
   
    .style15
  
    .style13
   
    .style17
    {
        width: 158px;
    }
    .style18
    {
        width: 161px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
    <tr>
        <td class="style16">
            <span class="style14"><span class="style15">Welcome!</span><span 
                class="style13"><br class="style15" />
            </span><span class="style15">Here you can delete all the old movies by pressing 
            &quot;Delete&quot; button.</span></span><span class="style13"><span class="style14"><br 
                class="style15" />
            <span class="style15">If you would like to keep screening an old movie, you can 
            update it&#39;s last screening date, by pressing &quot;Update&quot; button.<br />
            <br />
            <br />
            <br />
            </span>
            </span></span>
        </td>
        <td class="style4">
        </td>
        <td class="style4">
        </td>
    </tr>
</table>
<span class="style13"><span class="style14">
            <table style="width:103%; height: 223px;">
                <tr>
                    <td class="style17">
                        <asp:Button ID="btn_deletemovies" runat="server" Height="35px" Text="Delete" 
                            Width="94px" onclick="btn_deletemovies_Click" />
                    </td>
                    <td class="style18">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style17">
                        Update:</td>
                    <td class="style18">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style17">
                        <span class="style13"><span class="style14">
                        <asp:DropDownList ID="ddl_moviestoupdate" runat="server" 
                            AutoPostBack="True">
                            <asp:ListItem>Movie</asp:ListItem>
                        </asp:DropDownList>
                        </span></span>
                    </td>
                    <td class="style18">
                        <asp:TextBox ID="txt_newdateend" runat="server" Width="143px"></asp:TextBox>
                    </td>
                    <td style="color: #FF0000">
                        <asp:Button ID="btn_updatedateend" runat="server" 
                            onclick="btn_updatedateend_Click" style="margin-bottom: 0px" Text="Update" 
                            Width="69px" />
                    &nbsp;mm/dd/yyyy</td>
                </tr>
                <tr>
                    <td class="style17">
                        <span class="style13"><span class="style14">
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            onclick="LinkButton2_Click">back to admin&#39;s default</asp:LinkButton>
            </span></span>
                    </td>
                    <td class="style18">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            </span></span>
        </asp:Content>

