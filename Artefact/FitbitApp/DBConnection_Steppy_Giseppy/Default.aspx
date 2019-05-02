<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DBConnection_Steppy_Giseppy._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>  Steppy Giuseppe - Login  </title>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


  <section id="banner">
      <div class="inner">
        <header>
            <h1>Steppy Giuseppe</h1>
            <p>Welcome to SMEEDSGaming</p>        
        </header>
      </div>
  </section>

            <div id="main">
                <section class="wrapper style1">
						<div class="inner">
							<header class="align-center">
								<h2>To Play Steppy Giuseppe</h2>
								<p>To play this game you must have a fitbit device and a fitbit account</p>
							</header>
							<div class="flex flex-3">
								<div class="col align-center">
									<div class="image round fit">
										<img src="images/fitbit-png--400.png" alt="" />
									</div>
									<p>To sign up with fitbit and buy a device, follow the link below to go to the fitbit main page. </p>
									<a href="https://www.fitbit.com/au/home" class="button">Fitbit Home</a>
								</div>
								<div class="col align-center">
									<div class="image round fit">
										<img src="images/Giuseppe.png" alt="" />
									</div>
									<p>If you already have an account with fitbit and have updated your steps today, click below to login to the game. </p>
									<asp:Button ID="Button1" Text="Login Here" OnClick="openURL" runat="server"/>  
								</div>
								<div class="col align-center">
									<div class="image round fit">
										<img src="images/Logo.png" alt="" />
									</div>
									<p>To find out more about SMEEDSGaming, click below to go to our about page. </p>
									<a href="About.aspx" class="button">Learn More</a>
								</div>
							</div>
						</div>
					</section>

            </div>
    <a href="http://www.freepik.com">Designed by alexphotos / Freepik</a> <br />
    <a href="http://pluspng.com/fitbit-png-6336.html" title="Image from pluspng.com">Fitbit.png</a>
</asp:Content>
