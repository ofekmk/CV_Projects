<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminDeleteUsers.aspx.cs" Inherits="AdminDeleteUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:GridView ID="gv_deleteusers" runat="server" AutoGenerateColumns="False" 
                                onrowcommand="gv_deleteusers_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="UserID" HeaderText="User ID" />
                                    <asp:BoundField DataField="Fname" HeaderText="First Name" />
                                    <asp:BoundField DataField="Lname" HeaderText="Last Name" />
                                    <asp:BoundField DataField="Age" HeaderText="Age" />
                                    <asp:BoundField DataField="Country" HeaderText="Country" />
                                    <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:BoundField DataField="CreditCard" HeaderText="Credit Card" />
                                    <asp:BoundField DataField="Password" HeaderText="Password" />
                                    <asp:ButtonField ButtonType="Button" CommandName="DeleteUser" Text="Delete" />
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtn_toadmindfromusers" runat="server" 
                                CausesValidation="False" onclick="lbtn_toadmindfromusers_Click">Back To Admin&#39;s Default</asp:LinkButton>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

