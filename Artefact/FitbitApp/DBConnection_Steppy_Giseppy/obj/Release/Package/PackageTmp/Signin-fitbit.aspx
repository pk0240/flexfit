<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Signin-fitbit.aspx.cs" Inherits="DBConnection_Steppy_Giseppy.Signin_fitbit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>  Steppy Giuseppe - Start Game  </title>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section id="banner">
      <div class="inner"> 

        <header>
                <!-- eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiI2U05SSFAiLCJhdWQiOiIyMkNZNFQiLCJpc3MiOiJGaXRiaXQiLCJ0eXAiOiJhY2Nlc3NfdG9rZW4iLCJzY29wZXMiOiJyYWN0IiwiZXhwIjoxNTM5MDc3OTA2LCJpYXQiOjE1MzkwMzg4Mzl9.rQSQD3rMKF_4FalL2k8P133C7_Wf1RtgWmd02SvvLXAeyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiI2U05SSFAiLCJhdWQiOiIyMkNZNFQiLCJpc3MiOiJGaXRiaXQiLCJ0eXAiOiJhY2Nlc3NfdG9rZW4iLCJzY29wZXMiOiJyYWN0IiwiZXhwIjoxNTM5MDc3OTA2LCJpYXQiOjE1MzkwMzg4Mzl9.rQSQD3rMKF_4FalL2k8P133C7_Wf1RtgWmd02SvvLXA -->
         <br /> <br />
         <p runat="server" id="currentScoreP"> Click 'Add Steps' to see your new in-game Step Coins.</p>
         <p runat="server" id="newScoreP" visible="false"> </p>
         <asp:Button ID="Button2" Text="Add Steps" OnClick="getAccessToken" runat="server"/>
           <br /><br />
                <form method="POST" action="Default.aspx.cs">
                    <!--<p> Token </p>  <input type="text" name="token" id="tokenInput" runat="server" /> <br /><br /> -->
                    

                    <div style="width:200px; text-align:center; display:inline-block">
                        <input type="text" style="color:white; font-size:14px; text-align:right; background:rgba(0,0,0,0); border:none;" Value="Date:" runat="server" disabled/> <br />
                        <input type="text" style="color:white; font-size:14px; text-align:right; background:rgba(0,0,0,0); border:none;" Value="Previous Step Coins Score:" runat="server" disabled/> <br />
                        <input type="text" style="color:white; font-size:14px; text-align:right; background:rgba(0,0,0,0); border:none;" Value="Steps from Fitbit: " runat="server" disabled/><br />
                        <input type="text" style="color:white; font-size:14px; text-align:right; background:rgba(0,0,0,0); border:none;" Value="New Step Coins Score: " runat="server" disabled/>
                    </div>
                    <div style="width:200px; text-align:center; display:inline-block">
                        <input type="text" style="color:white; font-size:14px;" name="date" id="date" runat="server" disabled/> <br />
                        <input type="text" style="color:white; font-size:14px;" width="50px" name="oldStepCoins" id="oldStepCoins" runat="server" disabled/> <br />
                        <input type="text" style="color:white; font-size:14px;" name="stepsFromFitbit" id="stepsFromFitbit" runat="server" disabled/><br />
                        <input type="text" style="color:white; font-size:14px;" name="newStepCoins" id="newStepCoins" runat="server" disabled/>
                     </div>
                </form>
           
                <br /><br />
                <asp:Button ID="getSteps" Text="Start Game" OnClick="updateSteps" runat="server"/>
            
                <br /><br />
         </header>
          </div>
         </section>
            

</asp:Content>
        

