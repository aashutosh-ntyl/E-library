<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Func.aspx.cs" Inherits="WebApplication1.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel Height="100%" ID="Panel5" runat="server" style="margin-left: 10px; margin-top: 10px;">
        <br />
        <asp:Button ID="Button5" runat="server" Text="Lend Book" Height="80px" OnClick="Button5_Click" Width="200px" />
        <asp:Button ID="Button6" runat="server" Height="80px" OnClick="Button6_Click" Text="Return Book" Width="200px" />

        <asp:Button ID="Button8" runat="server" Height="80px" OnClick="Button8_Click" Text="Your Activities" Width="200px" />

        <asp:Button ID="Button10" runat="server" Height="80px" OnClick="Button10_Click" Text="Active Books and Late Fine" Width="256px" />

    </asp:Panel>
    <asp:Panel Height="100%" ID="Panel1" runat="server" style="margin-bottom: 0px; margin-left: 10px; margin-top: 0px;" Visible="False">
        <br />
        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="XX-Large" Font-Underline="False" Text="LEND BOOK" style="margin-left: 6px" Width="195px"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Height="32px" Text="Enter Book Id" Width="100px" Font-Bold="True" style="margin-left: 6px"></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server" Height="20px" Width="337px" AutoPostBack="true" OnTextChanged="search_book"></asp:TextBox>
        <asp:Label ID="Label20" runat="server" Height="36px" style="margin-left: 2px" Text="Press Enter" Width="73px"></asp:Label>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox3" CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Cannot be Empty and Can't contain any Alphabet." ValidationExpression="[0-9]{1,}"></asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Height="41px" style="margin-top: 0px; margin-left: 6px;" Text="Book Name (Auto Fill)" Width="100px"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True" Width="337px"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Height="43px" style="margin-left: 6px" Text="Book Author (Auto Fill)" Width="100px"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" ReadOnly="True" Width="337px"></asp:TextBox>
        <br />
        &nbsp;<br />
        <asp:Button ID="Button1" runat="server" Text="Lend Book" Width="171px" OnClick="add_book" Enabled="False"/>
        <br />
        <br />
     </asp:Panel>
     <asp:Panel Height="100%" ID="Panel2" runat="server" style="margin-left: 10px" Visible="False">
            <br />
            &nbsp;<asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="XX-Large" Font-Underline="False" Text="RETURN BOOK" style="margin-left: 6px" Width="262px"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Height="22px" Text="Your Books" Width="100px" Font-Bold="True" style="margin-left: 10px"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" Height="26px" AutoPostBack="true" Width="240px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="field-validation-error" ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList1" InitialValue="-1" Text="Selection is required.">
            </asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="Label13" runat="server" Font-Bold="True" style="margin-left: 10px" Text="Lend Date :" Width="100px"></asp:Label>
            <asp:Label ID="Label14" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label11" runat="server" Font-Bold="True" style="margin-left: 10px" Text="Due Date :" Width="100px"></asp:Label>
            <asp:Label ID="Label12" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Text="Book Name :" Font-Bold="True" style="margin-left: 10px" Width="100px"></asp:Label>
            <asp:Label ID="Label6" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label7" runat="server" Text="Book Author :" Font-Bold="True" style="margin-left: 10px" Width="100px"></asp:Label>
            <asp:Label ID="Label8" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label18" runat="server" Font-Bold="True" style="margin-left: 10px" Text="Fine Status :" Width="100px"></asp:Label>
            <asp:Label ID="Label19" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="remove_book" Text="Return Book" Enabled="False" Width="117px" />
            <br />
        </asp:Panel>
    <asp:Panel ID="Panel3" runat="server" Height="100%" style="margin-left: 10px; margin-top: 0px;" Visible="False">
        <br />
        <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="XX-Large" Font-Underline="False" style="margin-left: 6px" Text="YOUR ACTIVITIES" Width="288px"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label16" runat="server" Font-Bold="True" Height="21px" Text="Select" Width="100px" style="margin-left: 10px"></asp:Label>
        <asp:DropDownList ValidationGroup="activity" ID="DropDownList2" runat="server" Height="27px" Width="240px" style="margin-top: 0px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" >
            <asp:ListItem Enabled="true" Text="Select Function" Value="-1"></asp:ListItem>
            <asp:ListItem Text="Log Details"></asp:ListItem>
            <asp:ListItem Text="Book Details"></asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ValidationGroup="activity" CssClass="field-validation-error" ID="Reqfunc" runat="server" ControlToValidate="DropDownList2" InitialValue="-1" Text="Selection is required.">
            </asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" BorderColor="White" BorderStyle="Solid" BorderWidth="10px" CellPadding="4" Visible="False" Width="597px" HorizontalAlign="Left" BackColor="White">
            <EditRowStyle HorizontalAlign="Center" BorderStyle="None" VerticalAlign="Middle" />
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" HorizontalAlign="Left" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
        &nbsp;<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        </asp:Panel>
        <asp:Panel Height="100%" ID="Panel4" runat="server" style="margin-bottom: 24px; margin-left: 10px; margin-top: 0px;" Visible="False">
            <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="XX-Large" Font-Underline="False" style="margin-left: 6px" Text="ACTIVE FINEs AND BOOKs" Width="488px"></asp:Label>
            <br />
            <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Text="* For Fine Payment Contact the Administrator"></asp:Label>
            <br />
            <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="10px" CellPadding="4" HorizontalAlign="Left" Width="1237px">
                <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" HorizontalAlign="Left" VerticalAlign="Middle" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            </asp:Panel>
</asp:Content>
