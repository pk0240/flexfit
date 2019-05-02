<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="DBConnection_Steppy_Giseppy.About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>  Steppy Giuseppe - About Us  </title>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section id="banner">
				<div class="inner">
					<header>
						<h1>About SMEEDS Gaming</h1>
                        <!--
                        <video width="500" controls>
                          <source src="Resources/movie.mp4" type="video/mp4">
                          <source src="Resources/movie.ogg" type="video/ogg">
                            Your browser does not support the video tag.
                        </video>-->
					</header>					
				</div>
			</section>

     <div id="main">
                <section class="wrapper style1">
						<div class="inner">
							<header class="align-center">
								<h2>Meet the Team</h2>
								<p>SMEEDS Gaming comes from the first letter of each developers name</p>
							</header>
							<div class="flex flex-3">
								<div class="col align-center">
									<div class="image round fit">
										<img src="images/husky-dog.jpg" alt="" />
									</div>
                                    <h3>Sharlene Von Drehnen</h3>
									<p> Back-end Database Engineer </p>
								</div>
								<div class="col align-center">
									<div class="image round fit">
										<img src="images/MountainDog.jpg" alt="" />
									</div>
									<h3>Max Peterson</h3>
									<p> Front-end Developer </p>
								</div>
								<div class="col align-center">
									<div class="image round fit">
										<img src="images/GoldenRetDog.jpg" alt="" />
									</div>
									<h3>Eddie Redding</h3>
									<p> Project Manager </p>
								</div>
							</div>
						</div>
					</section>
                <section class="wrapper style1">
						<div class="inner">
							<div class="flex flex-3">
								<div class="col align-center">
									<div class="image round fit">
										<img src="images/Border-Collie_dog.jpg" alt="" />
									</div>
									<h3>Emily Smit</h3>
									<p> Graphics Designer </p>
								</div>
								<div class="col align-center">
									<div class="image round fit">
										<img src="images/RotweillerDog.jpg" alt="" />
									</div>
									<h3>Duncan Whittaker</h3>
									<p> Lead Game Developer </p>
                                </div>
								<div class="col align-center">
									<div class="image round fit">
										<img src="images/Happy_dog.jpg" alt="" />
									</div>
									<h3>Simon Mather</h3>
									<p> Fitbit API Developer </p>
								</div>
							</div>
						</div>
					</section>
         <section class="wrapper style1">
						<div class="inner">
								<div class="flex flex-2">
									<div class="col col1">
										<div class="image round fit">
										    <img src="images/Logo.png" alt="" />
										</div>
									</div>
									<div class="col col2">
										<h3>Who we are and What do we do?</h3>
										<p>We are a Team Information Technology students from The University of Newcastle. The team formed mid 2018 and decided on finding a way to integrate a physical activity within a game production.
                                           The Team decided on Exergaming with a focus on fitbit intergration. And so the development of Steppy Giuseppe was underway. 
										</p>
									</div>
								</div>
						</div>
		 </section>
            </div>
</asp:Content>
