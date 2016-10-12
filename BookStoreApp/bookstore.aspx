<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bookstore.aspx.cs" Inherits="BookStoreApp.Bookstore" %>

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
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
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
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblName" runat="server" Text="Name" ToolTip="Book Name"></asp:Label>
        :&nbsp;
        <asp:TextBox ID="txtName" runat="server" Width="169px"></asp:TextBox>
&nbsp;
        <asp:Label ID="lblAuthor" runat="server" Text="Author"></asp:Label>
        :&nbsp;
        <asp:TextBox ID="txtAuthor" runat="server" Width="146px"></asp:TextBox>
&nbsp;
        <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
        :&nbsp;
        <asp:TextBox ID="txtYear" runat="server" Width="61px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
        :&nbsp;
        <asp:TextBox ID="txtPrice" runat="server" Width="57px"></asp:TextBox>
&nbsp;
        <asp:Label ID="lblStock" runat="server" Text="Stock"></asp:Label>
        :&nbsp;
        <asp:TextBox ID="txtStock" runat="server" Width="72px"></asp:TextBox>
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RFV1" runat="server" ControlToValidate="txtID" ErrorMessage=" Enter an ID" ValidationGroup="Add"></asp:RequiredFieldValidator>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RFV2" runat="server" ControlToValidate="txtName" ErrorMessage="Enter a Name" ValidationGroup="Add"></asp:RequiredFieldValidator>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RFV3" runat="server" ControlToValidate="txtAuthor" ErrorMessage="Enter an Author" ValidationGroup="Add"></asp:RequiredFieldValidator>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RFV4" runat="server" ControlToValidate="txtYear" ErrorMessage="Enter Year" ValidationGroup="Add"></asp:RequiredFieldValidator>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RFV5" runat="server" ControlToValidate="txtPrice" ErrorMessage="Enter Price" ValidationGroup="Add"></asp:RequiredFieldValidator>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RFV6" runat="server" ControlToValidate="txtStock" ErrorMessage="Enter Stock" ValidationGroup="Add"></asp:RequiredFieldValidator>
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="ID must be Unique" OnDataBinding="btnAdd_Click" OnServerValidate="ServerValidation" ValidationGroup="Add" ControlToValidate="txtID"></asp:CustomValidator>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtYear" ErrorMessage="Only Int &gt;0 &amp; &lt; 2018" MaximumValue="2017" MinimumValue="1" Type="Integer" ValidationGroup="Add"></asp:RangeValidator>
        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtPrice" ErrorMessage="Must be &gt;0" MaximumValue="9999" MinimumValue="0" Type="Double" ValidationGroup="Add"></asp:RangeValidator>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtStock" ErrorMessage="Must Be Int &gt;0" MaximumValue="9999" MinimumValue="1" Type="Integer" ValidationGroup="Add"></asp:RangeValidator>
        <br />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" ValidationGroup="Delete" />
&nbsp;<asp:DropDownList ID="dropDelete" runat="server">
            <asp:ListItem>Year</asp:ListItem>
            <asp:ListItem>ID</asp:ListItem>
            <asp:ListItem>Num</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="txtDelete" runat="server" ValidationGroup="Delete"></asp:TextBox>
        <br />
        <asp:CustomValidator ID="CVDelete" runat="server" ControlToValidate="dropDelete" ErrorMessage="CustomValidator" OnServerValidate="ServerValidation_1" ValidationGroup="Delete"></asp:CustomValidator>
        <br />
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="Search" />
        <asp:DropDownList ID="DropSearch" runat="server">
            <asp:ListItem Value="Clear">&lt;Clear Search&gt;</asp:ListItem>
            <asp:ListItem>ID</asp:ListItem>
            <asp:ListItem>Name</asp:ListItem>
            <asp:ListItem>Author</asp:ListItem>
            <asp:ListItem>Year</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="txtSearch" runat="server" Width="105px"></asp:TextBox>
        <br />
        <asp:CustomValidator ID="CVSearch" runat="server" ControlToValidate="DropSearch" ErrorMessage="CustomValidator" OnServerValidate="ServerValidation_2" ValidationGroup="Search"></asp:CustomValidator>
        <br />
        Purchase Books<br />
        <br />
        <asp:Label ID="lblBudget" runat="server" Text="Total Budget"></asp:Label>
&nbsp;<asp:TextBox ID="txtBudget" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnMore" runat="server" OnClick="btnMore_Click" Text="More" />
                <br />
                <asp:PlaceHolder ID="PH1" runat="server"></asp:PlaceHolder>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <asp:Button ID="btnPurchase" runat="server" Text="Purchase" OnClick="btnPurchase_Click" />
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblResponse" runat="server"></asp:Label>
    </form>
</body>
</html>
