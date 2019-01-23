<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication1.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

     <section id="RegisterForm">
         <h2>Enter your details to Register.</h2>
                    <fieldset>
                        <legend>Registration Form</legend>
                        <ol>
                            <li>
                                <asp:RadioButtonList ID="usertype" runat="server" RepeatDirection="Horizontal" Width="241px" OnSelectedIndexChanged="usertype_SelectedIndexChanged" AutoPostBack="true" Height="21px">
                                    <asp:ListItem Text="User" Value="user" Selected="False" />
                                    <asp:ListItem Text="Admin" Value="admin" Selected="False"/>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="usertype"
                                    CssClass="field-validation-error" ErrorMessage="The usertype is required." />
                                <br />
                                <asp:Label ID="Label1" runat="server" Height="21px" Text="Staff ID" Width="90px" Visible="False" Style="font-weight:600"></asp:Label><br />

                                <asp:TextBox ID="TextBox1" runat="server" Width="151px" Visible="False" OnTextChanged="TextBox1_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox1"
                                    CssClass="field-validation-error" ErrorMessage="The Staff ID field is required." EnableTheming="True" Visible="False" />
                            </li>
                            <li>
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="UserName" Width="376px">User name</asp:Label>
                                <asp:TextBox runat="server" ID="UserName" Width="244px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="UserName"
                                    CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                            </li>
                            <li>
                                <asp:Label ID="Label3" runat="server" AssociatedControlID="Email">Email address</asp:Label>
                                <asp:TextBox runat="server" ID="Email" TextMode="Email" Width="244px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Email"
                                    CssClass="field-validation-error" ErrorMessage="The email address field is required." />
                                <asp:RegularExpressionValidator CssClass="field-validation-error" ID="regexEmailValid" runat="server" Display="Dynamic" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="Email" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                            </li>
                            <li>
                                <asp:Label ID="Label4" runat="server" AssociatedControlID="Password" Width="567px">Password</asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" Width="244px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Password"
                                    CssClass="field-validation-error" ErrorMessage="The password field is required." />
                                <asp:RegularExpressionValidator CssClass="field-validation-error" ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ValidationExpression="[0-9a-zA_Z]{6,}" ControlToValidate="Password" ErrorMessage="Password should be minimum of 6 and no special characters."></asp:RegularExpressionValidator>
                            </li>
                            <li>
                                <asp:Label ID="Label5" runat="server" AssociatedControlID="ConfirmPassword">Confirm password</asp:Label>
                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" Width="243px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                            </li>
                            <li>
                        <asp:Button id="register" runat="server" Text="Register" OnClick="insert_user" Width="116px" />
                            </li>
                        </ol>
                        </fieldset>
         </section>
</asp:Content>