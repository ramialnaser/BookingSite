<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="WebRole1.AddAirportValues" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>



    <style type="text/css">
        .auto-style1 {
            width: 22%;
            float: left;
            margin: 10px 10px 10px 10px;
        }

        .auto-style2 {
            width: 29%;
            float: left;
            margin: 20px 30px 20px 10px;
        }

        .auto-style3 {
            width: 98%;
            margin: 5px 5px 5px 5px;
            text-align: center;
        }

        .auto-style4 {
            margin-right: 70px;
            float: right;
        }
    </style>


</head>

<body>
    <form id="form1" runat="server">

        <asp:RadioButtonList ID="AdminRadioBtn" runat="server">
            <asp:ListItem Selected="True">SQL</asp:ListItem>
            <asp:ListItem>NoSQL</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="GoToFRSButton" runat="server" Class="auto-style4" Text="Go to ClientPage =>" Width="172px" Height="37px" OnClick="GoToFRSButton_Click" />
        <br />
        <br />

        <div style="display: inline-block; height: 200px; border-style: outset;"
            class="auto-style1">
            <asp:Label ID="Label6" runat="server" Text="Insert airport to the database" Style="margin: 15px 0px 0px 50px;"></asp:Label>
            <br />
            <asp:Label ID="Label1" runat="server" Width="50px" Text="Code:" Style="margin: 10px 0px 0px 20px;"></asp:Label>

            <asp:TextBox ID="AirportCodeTextBox" runat="server" Style="margin: 10px 0px 0px 20px;" Width="180px"></asp:TextBox>

            <br />

            <asp:Label ID="Label2" runat="server" Width="50px" Text="City:" Style="margin: 5px 0px 0px 20px;"></asp:Label>

            <asp:TextBox ID="CityNameTextBox" runat="server" Style="margin: 5px 5px 0px 20px;" Width="180px"></asp:TextBox>

            <br />
            <asp:Label ID="Label4" runat="server" Width="50px" Text="Latitude:" Style="margin: 5px 0px 0px 20px;"></asp:Label>
            <asp:TextBox ID="LatitudeTextBox" runat="server" Style="margin: 5px 0px 0px 20px;" Width="180px"></asp:TextBox>
            <br />
            <asp:Label ID="Label3" runat="server" Width="50px" Text="Longitude:" Style="margin: 5px 0px 0px 20px;"></asp:Label>

            <asp:TextBox ID="LongitudeTextBox" runat="server" Style="margin: 5px 10px 0px 20px;" Width="180px"></asp:TextBox>
            <br />

            <asp:Button ID="AddAirportBtn" runat="server" Text="Add Airport" Style="margin: 10px 0px 0px 20px;" BackColor="Gray" Width="260px" OnClick="AddAirportBtn_Click" />
            <br />
            <asp:Label ID="AirportResult" runat="server" Style="margin: 10px 0px 0px 20px;" Width="260px" Text=""></asp:Label>

        </div>

        <div style="display: inline-block; border-style: outset; height: 200px;"
            class="auto-style1">
            <asp:Label ID="Label7" runat="server" Text="Insert airline to the database" Style="margin: 15px 0px 0px 50px;"></asp:Label>

            <br />
            <asp:Label ID="Label5" runat="server" Width="50px" Text="Code:" Style="margin: 25px 0px 0px 20px;"></asp:Label>

            <asp:TextBox ID="AirlineCodeTextBox" runat="server" Style="margin: 25px 0px 0px 20px;" Width="180px"></asp:TextBox>

            <br />

            <asp:Label ID="Label8" runat="server" Width="50px" Text="Name:" Style="margin: 25px 0px 0px 20px;"></asp:Label>

            <asp:TextBox ID="AirlineNameTextBox" runat="server" Style="margin: 25px 0px 0px 20px;" Width="180px"></asp:TextBox>
            <asp:Button ID="AddAirlineBtn" runat="server" Text="Add Airline" Style="margin: 25px 0px 0px 20px;" BackColor="Gray" Width="260px" OnClick="AddAirlineBtn_Click" />
            <br />
            <asp:Label ID="AirlineResult" runat="server" Style="margin: 20px 0px 0px 20px;" Width="260px" Text=""></asp:Label>

        </div>

        <div style="display: inline-block; border-style: outset; height: 200px;"
            class="auto-style1">

            <asp:Label ID="Label9" runat="server" Text="Insert route to the database" Style="margin: 15px 0px 0px 60px;"></asp:Label>
            <br />
            <asp:Label ID="Label14" runat="server" Width="120px" Text="FlightNr:" Style="margin: 10px 0px 0px 15px;"></asp:Label>

            <asp:TextBox ID="FlightNrTextBox" runat="server" Style="margin: 10px 0px 0px 0px;" Width="135px"></asp:TextBox>

            <br />
            <asp:Label ID="Label10" runat="server" Width="120px" Text="Airline's Code:" Style="margin: 10px 0px 0px 15px;"></asp:Label>

            <asp:TextBox ID="CarrierTextBox" runat="server" Style="margin: 10px 0px 0px 0px;" Width="135px"></asp:TextBox>

            <br />

            <asp:Label ID="Label11" runat="server" Width="120" Text="Departure's Code:" Style="margin: 10px 0px 0px 15px;"></asp:Label>

            <asp:TextBox ID="DepartureTextBox" runat="server" Style="margin: 10px 0px 0px 0px;" Width="135px"></asp:TextBox>

            <br />

            <asp:Label ID="Label12" runat="server" Width="120px" Text="Arrival's Code:" Style="margin: 10px 0px 0px 15px;"></asp:Label>

            <asp:TextBox ID="ArrivalTextBox" runat="server" Style="margin: 10px 0px 0px 0px;" Width="135px"></asp:TextBox>

            <br />

            <asp:Button ID="AddRouteBtn" runat="server" Text="Add Route" Style="margin: 10px 0px 0px 15px;" BackColor="Gray" Width="265px" OnClick="AddRouteBtn_Click" />
            <br />
            <asp:Label ID="RouteResult" runat="server" Style="margin: 10px 0px 0px 15px;" Width="265px" Text=""></asp:Label>
        </div>

        <div style="display: inline-block; border-style: outset; height: 200px;"
            class="auto-style1">

            <asp:Label ID="Label15" runat="server" Text="Insert customer to the database" Style="margin: 15px 0px 0px 50px;"></asp:Label>
            <br />
            <asp:Label ID="Label16" runat="server" Width="145px" Text="Card Number:" Style="margin: 10px 0px 0px 10px;"></asp:Label>

            <asp:TextBox ID="CardNrTextBox" runat="server" Style="margin: 10px 0px 0px 0px;" Width="120px"></asp:TextBox>

            <br />
            <asp:Label ID="Label17" runat="server" Width="145px" Text="Holder Name:" Style="margin: 10px 0px 0px 10px;"></asp:Label>

            <asp:TextBox ID="HolderNameTextBox" runat="server" Style="margin: 10px 0px 0px 0px;" Width="120px"></asp:TextBox>

            <br />

            <asp:Label ID="Label19" runat="server" Width="145px" Text="Expiry(yyyy-mm-dd):" Style="margin: 10px 0px 0px 10px;"></asp:Label>

            <asp:TextBox ID="ExpDateTextBox" runat="server" Style="margin: 10px 0px 0px 0px;" Width="120px"></asp:TextBox>

            <br />

            <asp:Label ID="Label20" runat="server" Width="145px" Text="Balance(SEK):" Style="margin: 10px 0px 0px 10px;"></asp:Label>

            <asp:TextBox ID="BalanceTextBox" runat="server" Style="margin: 10px 0px 0px 0px;" Width="120px"></asp:TextBox>

            <br />

            <asp:Button ID="AddCustomerButton" runat="server" Text="Add Customer" Style="margin: 10px 0px 0px 10px;" BackColor="Gray" Width="275px" OnClick="AddCustomerButton_Click" />
            <br />
            <asp:Label ID="CustomerResult" runat="server" Style="margin: 10px 0px 0px 10px;" Width="275px" Text=""></asp:Label>
        </div>

        <br />

        <div style="display: inline-block; border-style: outset; height: 380px;"
            class="auto-style2">
            <asp:Label ID="Label13" runat="server" Text="List of airports" Style="margin: 15px 0px 0px 140px;"></asp:Label>
            <br />
            <asp:Button ID="GetAirportsBtn" runat="server" Text="Get Airports" Style="margin: 10px 0px 0px 40px;" BackColor="Gray" Width="300px" OnClick="GetAirportsBtn_Click" />
            <br />
            <asp:GridView class="auto-style3" ID="airportTable" runat="server"></asp:GridView>

        </div>

        <div style="display: inline-block; border-style: outset; height: 380px;"
            class="auto-style2">
            <asp:Label ID="Label18" runat="server" Text="List of airlines" Style="margin: 15px 0px 0px 140px;"></asp:Label>
            <br />
            <asp:Button ID="GetAirlinesBtn" runat="server" Text="Get Airlines" Style="margin: 10px 0px 0px 40px;" BackColor="Gray" Width="300px" OnClick="GetAirlinesBtn_Click" />
            <br />
            <asp:GridView class="auto-style3" ID="airlineTable" runat="server"></asp:GridView>


        </div>

        <div style="display: inline-block; border-style: outset; height: 380px;"
            class="auto-style2">

            <asp:Label ID="Label21" runat="server" Text="List of routes" Style="margin: 15px 0px 0px 140px;"></asp:Label>
            <br />
            <asp:Button ID="GetRoutesBtn" runat="server" Text="Get Routes" Style="margin: 10px 0px 0px 40px;" BackColor="Gray" Width="300px" OnClick="GetRoutesBtn_Click" />
            <br />
            <asp:GridView class="auto-style3" ID="routesTable" runat="server"></asp:GridView>


        </div>

    </form>
</body>
</html>
