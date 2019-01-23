<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>ASP .Net webpage for library.</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>We suggest the following:</h3>
    <ol class="round">
        <li class="one">
            <h2><a id="loginLink" runat="server" href="~/Account/Login" style="text-decoration:none">Log in</a></h2>
        </li>
        <li class="two">
            <h2><a id="registerLink" runat="server" href="~/Account/Register" style="text-decoration:none">Register</a></h2>
        </li>
    </ol>
</asp:Content>
