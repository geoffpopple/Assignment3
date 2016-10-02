<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bookstore.aspx.cs" Inherits="BookStoreApp.bookstore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
                    <columns>
                      <asp:TemplateField HeaderText="Num">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:boundfield datafield="ID" headertext="Book ID"/>
                        <asp:boundfield datafield="Name" headertext="Name"/>
                        <asp:boundfield datafield="Author" headertext="Author Name"/> 
                       <asp:boundfield datafield="Year" headertext="Year"/>                   
                      <asp:boundfield datafield="Price" headertext="Price" DataFormatString="{0:C}"/>
                      <asp:boundfield datafield="Stock" headertext="Stock"/>

                    </columns>
                  </asp:GridView>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>
        Manage Books<br />
        <br />
        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" ToolTip="Add a book" ValidationGroup="Add" />
&nbsp;<asp:Label ID="lblID" runat="server" Text="ID"></asp:Label>
        :&nbsp;
        <asp:TextBox ID="txtID" runat="server" Width="85px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RFV1" runat="server" ControlToValidate="txtID" ErrorMessage="Must Enter an ID" ValidationGroup="Add"></asp:RequiredFieldValidator>
&nbsp;<asp:Label ID="lblName" runat="server" Text="Name" ToolTip="Book Name"></asp:Label>
        :&nbsp;
        <asp:TextBox ID="txtName" runat="server" Width="169px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RFV2" runat="server" ControlToValidate="txtName" ErrorMessage="Must Enter a Name" ValidationGroup="Add"></asp:RequiredFieldValidator>
&nbsp;
        <asp:Label ID="lblAuthor" runat="server" Text="Author"></asp:Label>
        :&nbsp;
        <asp:TextBox ID="txtAuthor" runat="server" Width="146px"></asp:TextBox>
&nbsp;
        <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
        :&nbsp;
        <asp:TextBox ID="txtYear" runat="server" Width="61px"></asp:TextBox>
&nbsp;
        <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
        :&nbsp;
        <asp:TextBox ID="txtPrice" runat="server" Width="57px"></asp:TextBox>
&nbsp;
        <asp:Label ID="lblStock" runat="server" Text="Stock"></asp:Label>
        :&nbsp;
        <asp:TextBox ID="txtStock" runat="server" Width="72px"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" />
&nbsp;<asp:DropDownList ID="dropDelete" runat="server">
            <asp:ListItem>Year</asp:ListItem>
            <asp:ListItem>ID</asp:ListItem>
            <asp:ListItem>Num</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="txtDelete" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnSearch" runat="server" Text="Search" />
        <asp:TextBox ID="txtSearch" runat="server" Width="175px"></asp:TextBox>
        <br />
        <br />
        Purchase Books<br />
        <br />
        <asp:Label ID="lblBudget" runat="server" Text="Total Budget"></asp:Label>
&nbsp;<asp:TextBox ID="txtBudget" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblBookNumber" runat="server" Text="Book Number"></asp:Label>
                <asp:TextBox ID="txtBookNumber" runat="server"></asp:TextBox>
                <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
                <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                &nbsp;
                <asp:Button ID="btnMore" runat="server" Text="More" />
                <br />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <asp:Button ID="btnPurchase" runat="server" Text="Purchase" />
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblResponse" runat="server"></asp:Label>
    </form>
</body>
</html>
