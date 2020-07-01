<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientPage.aspx.cs" Inherits="WebRole1.FRS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .auto-style1 {
            width: 55%;
            float: left;
            margin: 20px 20px 10px 20px;
        }

        .auto-style2 {
            width: 37%;
            float: left;
            margin: 20px 20px 10px 20px;
        }

        .auto-style3 {
            width: 31%;
            float: left;
            margin: 10px 10px 10px 10px;
        }

        .auto-style4 {
            width: 98%;
            margin: 5px 0px 0px 5px;
            text-align: center;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="GoToAdminButton" runat="server" Style="margin-left: 20px" Text="<= Go to AdminPage" Width="172px" Height="37px" OnClick="GoToAdminButton_Click" />
        
        <asp:RadioButtonList ID="ClientRadionButton" runat="server">
            <asp:ListItem  Selected="True">SQL</asp:ListItem>
            <asp:ListItem>NoSQL</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <div style="display: inline-block; height: 200px; border-style: outset;"
            class="auto-style1">

            <asp:Label ID="Label6" runat="server" Text="Charter Resor" Style="margin: 10px 0px 0px 300px;"></asp:Label>

            <br />
            <asp:Label ID="Label1" runat="server" Width="120px" Text="Passport Number:" Style="margin: 20px 0px 0px 20px;"></asp:Label>

            <asp:TextBox ID="PassportTextBox" runat="server" Style="margin: 20px 0px 0px 0px;" Width="200px"></asp:TextBox>

            <asp:Label ID="Label2" runat="server" Width="120px" Text="Passenger's Name:" Style="margin: 20px 0px 0px 20px;"></asp:Label>

            <asp:TextBox ID="PassengerNameTextBox" runat="server" Style="margin: 2px 0px 0px 0px;" Width="200px"></asp:TextBox>
            <br />



            <asp:Label ID="Label3" runat="server" Width="120px" Text="FlightNumber:" Style="margin: 15px 0px 0px 20px;"></asp:Label>

            <asp:TextBox ID="FlightNumTextBox" runat="server" Style="margin: 15px 0px 0px 0px;" Width="200px"></asp:TextBox>

            <asp:Label ID="Label4" runat="server" Width="210px" Text="Departure's Date(yyyy-mm-dd):" Style="margin: 15px 0px 0px 20px;"></asp:Label>

            <asp:TextBox ID="DepartureDateTextBox" runat="server" Style="margin: 15px 0px 0px 0px;" Width="110px"></asp:TextBox>
            <br />
            <asp:Label ID="Label5" runat="server" Width="120px" Text="Passenger Type:" Style="margin: 15px 0px 0px 20px;"></asp:Label>

            <asp:DropDownList runat="server" ID="PassengerTypeList" Style="margin: 15px 0px 0px 0px;" Width="200px">
                <asp:ListItem>Infant</asp:ListItem>
                <asp:ListItem>Child</asp:ListItem>
                <asp:ListItem>Adult</asp:ListItem>
                <asp:ListItem>Senior</asp:ListItem>
            </asp:DropDownList>



            <asp:Button ID="BookFlight" runat="server" Text="Book Flight" Style="margin: 15px 0px 0px 20px;" BackColor="Gray" Width="335px" Height="30" OnClick="BookFlight_Click" />
            <br />
            <asp:Label ID="Label7" runat="server" Width="120px" Text="The Price is: " Style="margin: 15px 0px 0px 20px;"></asp:Label>
            <asp:TextBox ID="PriceTextBox" runat="server" Style="margin: 15px 0px 0px 0px; text-align: end" Width="145px" ReadOnly="True"></asp:TextBox>
            <asp:Label ID="Label9" runat="server" Width="50px" Text="SEK" Style="margin: 15px 0px 0px 1px;"></asp:Label>

            <asp:Label ID="FlightResults" runat="server" Width="300px" Style="margin: 15px 0px 0px 20px;"></asp:Label>
        </div>

        <div style="display: inline-block; height: 200px; border-style: outset;"
            class="auto-style2">

            <asp:Label ID="Label10" runat="server" Text="Payment Service" Style="margin: 10px 0px 0px 200px;"></asp:Label>

            <br />
            <asp:Label ID="Label11" runat="server" Width="120px" Text="Card Number:" Style="margin: 20px 0px 0px 15px;"></asp:Label>

            <asp:TextBox ID="CardNumberTextBox" runat="server" Style="margin: 20px 0px 0px 0px;" Width="195px"></asp:TextBox>
            <asp:Button ID="GetCustomersBtn" runat="server" Text="Get Customer" Style="margin: 20px 0px 0px 0px;" BackColor="Gray" Width="125px" Height="25px" OnClick="GetCustomersBtn_Click" />


            <br />
            <asp:Label ID="Label12" runat="server" Width="120px" Text="Card Holder" Style="margin: 20px 0px 0px 15px;"></asp:Label>

            <asp:TextBox ID="CardHolderTextBox" ReadOnly="True" runat="server" Style="margin: 2px 0px 0px 0px;" Width="325px"></asp:TextBox>
            <br />
            <asp:Label ID="Label13" runat="server" Width="200px" Text="Expiry Date(yyyy-mm-dd):" Style="margin: 20px 0px 0px 15px;"></asp:Label>

            <asp:TextBox ID="ExpiryDateTextBox" ReadOnly="True" runat="server" Style="margin: 2px 0px 0px 0px;" Width="245px"></asp:TextBox>
            <br />
            <asp:Button ID="ConfirmBookingBtn" runat="server" Text="Confirm Booking" Style="margin: 10px 0px 0px 15px;" BackColor="Gray" Width="150px" Height="25px" OnClick="ConfirmBookingBtn_Click" />

            <asp:Label ID="Label14" runat="server" Width="110px" Text="New Balance is:" Style="margin: 10px 0px 0px 50px;"></asp:Label>

            <asp:TextBox ID="BalanceTextBox" runat="server" Style="margin: 2px 0px 0px 0px;" Width="100px" ReadOnly="True"></asp:TextBox>
            <asp:Label ID="Label15" runat="server" Width="30px" Text="SEK" Style="margin: 10px 0px 0px 0px;"></asp:Label>
            <br />
            <asp:Label ID="ConfirmBookingResults" runat="server" Width="300px" Style="margin: 5px 0px 0px 40px;"></asp:Label>


        </div>
        <br />

        <div style="display: inline-block; height: 400px; border-style: outset;"
            class="auto-style3">

            <asp:Label ID="Label22" runat="server" Text="Transaction of a specifc card number" Style="margin: 10px 0px 0px 80px;"></asp:Label>
            <br />
            <asp:Label ID="Label23" runat="server" Width="120px" Text="Card Number:" Style="margin: 5px 0px 0px 60px;"></asp:Label>
            <br />
            <asp:TextBox ID="SpecCardNumber" runat="server" Style="margin: 5px 0px 0px 60px;" Width="250px"></asp:TextBox>
            <br />
            <asp:Button ID="GetSpecTransBtn" runat="server" Text="Get Transactions" Style="margin: 10px 0px 0px 60px;" BackColor="Gray" Width="255px" Height="25px" OnClick="GetSpecTransBtn_Click" />
            <br />
            <asp:Label ID="SpecTransResults" runat="server" Width="200px" Text="" Style="margin: 5px 0px 0px 60px;"></asp:Label>
            <br />
            <asp:GridView class="auto-style4" ID="SpecTransTable" runat="server"></asp:GridView>
        </div>
        <div style="display: inline-block; height: 400px; border-style: outset;"
            class="auto-style3">

            <asp:Label ID="Label16" runat="server" Text="List of routes from certain airport" Style="margin: 10px 0px 0px 90px;"></asp:Label>
            <br />
            <asp:Label ID="Label19" runat="server" Width="120px" Text="Airport Code:" Style="margin: 5px 0px 0px 60px;"></asp:Label>
            <br />
            <asp:TextBox ID="AirCode" runat="server" Style="margin: 5px 0px 0px 60px;" Width="260px"></asp:TextBox>
            <br />
            <asp:Button ID="SpecRoutesButton" runat="server" Text="Get Routes" Style="margin: 10px 0px 0px 60px;" BackColor="Gray" Width="265px" Height="25px" OnClick="SpecRoutesButton_Click" />
            <br />
            <asp:Label ID="SpecRouteResults" runat="server" Width="200px" Text="" Style="margin: 5px 0px 0px 60px;"></asp:Label>

            <br />
            <asp:GridView class="auto-style4" ID="SpecRoutesTable" runat="server"></asp:GridView>
        </div>

        <div style="display: inline-block; height: 400px; border-style: outset;"
            class="auto-style3">

            <asp:Label ID="Label20" runat="server" Text="List of card holders" Style="margin: 10px 0px 0px 140px;"></asp:Label>
            <br />
            <asp:Button ID="FetchCardHoldersButton" runat="server" Text="Get CardHolders" Style="margin: 10px 0px 0px 60px;" BackColor="Gray" Width="280px" Height="25px" OnClick="FetchCardHoldersButton_Click" />

            <br />
            <asp:GridView class="auto-style4" ID="CustomersTable" runat="server"></asp:GridView>
        </div>
        <div style="display: inline-block; height: 400px; border-style: outset;"
            class="auto-style3">

            <asp:Label ID="Label8" runat="server" Text="Revenue for specific flight and date" Style="margin: 10px 0px 0px 80px;"></asp:Label>
            <br />
            <asp:Label ID="Label17" runat="server" Width="120px" Text="Flight Number:" Style="margin: 5px 0px 0px 60px;"></asp:Label>
            <br />
            <asp:TextBox ID="RevFlgithNr" runat="server" Style="margin: 5px 0px 0px 60px;" Width="250px"></asp:TextBox>
            <br />
            <asp:Label ID="Label18" runat="server" Width="200px" Text="Departure Date(yyyy-mm-dd):" Style="margin: 5px 0px 0px 60px;"></asp:Label>
            <br />
            <asp:TextBox ID="RevDepDate" runat="server" Style="margin: 5px 0px 0px 60px;" Width="250px"></asp:TextBox>
            <br />
            <asp:Button ID="RevButton" runat="server" Text="Get Revenue" Style="margin: 10px 0px 0px 60px;" BackColor="Gray" Width="250px" Height="25px" OnClick="RevButton_Click" />
            <br />
            <asp:Label ID="RevResults" runat="server" Width="200px" Text="" Style="margin: 5px 0px 0px 60px;"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Label ID="Label21" runat="server" Width="120px" Text="Revenue (SEK):" Style="margin: 5px 0px 0px 60px;"></asp:Label>
            <br />
            <asp:TextBox ID="RevFetchedTextBox" runat="server" ReadOnly="true" Style="margin: 5px 0px 0px 60px;" Width="250px"></asp:TextBox>
            <br />
            <asp:Label ID="Label232" runat="server" Width="200px" Text="Departure City:" Style="margin: 5px 0px 0px 60px;"></asp:Label>
            <br />
            <asp:TextBox ID="RevFetchedDepCityTextBox" runat="server" ReadOnly="true" Style="margin: 5px 0px 0px 60px;" Width="250px"></asp:TextBox>
            <br />
            <asp:Label ID="Label25" runat="server" Width="200px" Text="Arrival City:" Style="margin: 5px 0px 0px 60px;"></asp:Label>
            <br />
            <asp:TextBox ID="RevFetchedArrCityTextBox" runat="server" ReadOnly="true" Style="margin: 5px 0px 0px 60px;" Width="250px"></asp:TextBox>

        </div>
    </form>

</body>

</html>
