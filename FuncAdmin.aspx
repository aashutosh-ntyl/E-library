<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/FuncAdmin.aspx.cs" Inherits="WebApplication1.FuncAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="Panel1" runat="server" style="margin-left: 30px" Height="30px">
            <asp:Label ID="Label1" runat="server" Text="Select" Width="100px" Height="24px"></asp:Label>
            <asp:DropDownList  ID="functions" runat="server" Height="20px" OnSelectedIndexChanged="func_select" AutoPostBack="true" Width="232px" style="margin-left: 2px">
            <asp:ListItem Enabled="true" Text="Select Function" Value="-1"></asp:ListItem>
            <asp:ListItem Text="My Log" Value="1"></asp:ListItem>
            <asp:ListItem Text="My User Removal Log" Value="2"></asp:ListItem>
            <asp:ListItem Text="User Log" Value="3"></asp:ListItem>
            <asp:ListItem Text="Add Book" Value="4"></asp:ListItem>
            <asp:ListItem Text="Remove Book" Value="5"></asp:ListItem>
            <asp:ListItem Text="Delete User" Value="6"></asp:ListItem>
            <asp:ListItem Text="User Fine and Book" Value="7"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ValidationGroup="functionsGrp" CssClass="field-validation-error" ID="Reqfunc" runat="server" ControlToValidate="functions" InitialValue="-1" Text="Selection is required.">
            </asp:RequiredFieldValidator>
       </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" style="margin-left: 30px; margin-bottom: 8px;" Visible="False" Font-Italic="False" Height="100%">
            <asp:Label ID="Label2" runat="server" Height="34px" Text="Select Date" Width="100px"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" Height="16px" ReadOnly="true" Width="223px" MaxLength="10"></asp:TextBox>
            <asp:LinkButton ID="LinkButton1" runat="server" Height="28px" Width="31px" OnClick="LinkButton1_Click" EnableTheming="False" Font-Bold="False" Font-Size="Medium" Font-Underline="False" >Here</asp:LinkButton>
            <asp:Calendar  ID="Calendar1" runat="server" Height="190px" Visible="False" Width="363px" OnSelectionChanged="Calendar1_SelectionChanged" TodayDayStyle-BackColor="Red" TodayDayStyle-ForeColor="White" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black">
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" HorizontalAlign="Center" VerticalAlign="Middle" />
                <DayStyle HorizontalAlign="Center" VerticalAlign="Top" />
                <NextPrevStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <OtherMonthDayStyle ForeColor="#808080" />
                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" />
                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                <WeekendDayStyle BackColor="#FFFFCC" />
            </asp:Calendar>
        </asp:Panel>
        
        <asp:Panel ID="Panel3" runat="server" style="margin-left: 30px" Visible="False">
            <asp:Label ID="user" runat="server" Height="32px" style="margin-top: 0px" Text="User Email" Width="100px"></asp:Label>
            <asp:TextBox ID="usertext" runat="server" Width="224px" Height="16px"></asp:TextBox>
        </asp:Panel>
        <asp:Panel ID="Panel4" runat="server" style="margin-left: 30px" Visible="False">
                 <asp:Label ID="Label7" runat="server" Height="24px" Text="Book Id" Width="100px"></asp:Label>
                 <asp:DropDownList ID="DropDownList1" runat="server" Width="235px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
                 </asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="field-validation-error" ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList1" InitialValue="-1" Text="Selection is required.">
            </asp:RequiredFieldValidator>
                 <br />
                 <asp:Label ID="Label4" runat="server" Height="30px" Text="Book Name" Width="100px"></asp:Label>
                 <asp:TextBox ID="bookname" runat="server" Width="224px" ValidationGroup="bookGrp"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="bookGrp" CssClass="field-validation-error" runat="server" ControlToValidate="bookname" Text="All field are required.">
                 </asp:RequiredFieldValidator>
                 <br />
                 <asp:Label ID="Label5" runat="server" Height="30px" Text="Book Author" Width="100px"></asp:Label>
                 <asp:TextBox ID="bookauthor" runat="server" Width="224px" ValidationGroup="bookGrp"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="bookGrp" CssClass="field-validation-error" runat="server" ControlToValidate="bookauthor" Text="All field are required.">
                 </asp:RequiredFieldValidator>
                 <br />
                 <asp:Label ID="Label6" runat="server" Height="30px" Text="Book ID" Width="100px"></asp:Label>
                 <asp:TextBox ID="bookid" runat="server" Width="224px" ValidationGroup="bookGrp"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="bookGrp" CssClass="field-validation-error" runat="server" ControlToValidate="bookid" Text="All field are required.">
                 </asp:RequiredFieldValidator>
        </asp:Panel>
        <asp:Panel ID="Panel5" runat="server" style="margin-left: 30px; margin-top: 8px;" Visible="False">
            <asp:Button ID="Button1" runat="server" Height="35px" Text="Submit" Width="83px" onClick="Button1_Click" ValidationGroup="functionsGrp"/>
            <asp:Button ID="Button2" runat="server" Height="36px" Text="Clear" Width="83px" Visible="False" OnClick="Button2_Click" />
        </asp:Panel>
        <asp:Panel ID="Panel6" runat="server" Height="100%" style="margin-left: 0px" Visible="false">
            <asp:GridView ID="GridView1" runat="server" CellPadding="3" Height="331px" Width="99%" BorderColor="White" BorderStyle="Solid" BorderWidth="10px" HorizontalAlign="Left" BackColor="White">
            <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Font-Names="Arial" Font-Size="Medium" HorizontalAlign="Center" VerticalAlign="Middle"/>
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left"/>
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
       </asp:Panel>
        <asp:Panel ID="Panel7" runat="server" Height="100%" style="margin-left: 0px" Visible="False">
            <asp:GridView ID="GridView2" runat="server" CellPadding="0" Height="331px" Width="99%" BorderColor="White" BorderStyle="Solid" BorderWidth="10px" HorizontalAlign="Left" BackColor="White" OnRowCommand="GridView2_RowCommand">
                <Columns>
                    <asp:ButtonField CommandName="finecheckout" Text="Pay" />
                </Columns>
            <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Black" BorderWidth="0px" />
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Font-Names="Arial" Font-Size="Medium" HorizontalAlign="Center" VerticalAlign="Middle"/>
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left"/>
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
       </asp:Panel>
     
</asp:Content>
