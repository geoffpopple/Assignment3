<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Items.ascx.cs" Inherits="BookStoreService.Items" %>
 <br />
<table style="width:33%;">
    <tr>
        <td>
            Book Number
        </td>
        <td>
            <asp:TextBox ID="txtBookNum" runat="server"></asp:TextBox>
        </td>
        <td>
            Amount
        </td>
        <td>
            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
        </td>
    </tr>
    </table>