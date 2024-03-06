
using MauiColor = Microsoft.Maui.Graphics.Color;


namespace Asgmt2
{
    public partial class FlightsPage
    {
        //Flight searchbar input
        string fromInput = "";
        string toInput = "";
        string dayInput = "";
        string flightSelectedInput = "";

        //Flight information
        string flightCode = "";
        string flightAirline = "";
        string flightDay = "";
        string flightTime = "";
        string flightCost = "";
        string flightName = "";
        string flightCitizenship = "";

        //List of all the flights
        List<String> flightList = new List<String>();


        public FlightsPage()
        {
            InitializeComponent();
            flightFinderMethod();
        }

        //When find flight button is clicked, Flight list data will add to filteredFlightList depending on the search
        public void OnFindFlights(object sender, EventArgs e) 
        {
            List<String> filteredFlightList = new List<String>();

            foreach(string flightData in flightList)
            {
                string[] data = flightData.Split(',');

                if ((data[2] == fromInput || fromInput == "Any") && fromInput != toInput)
                {

                    if ((toInput == "" && dayInput == "") || (toInput == "Any" && dayInput == "Any"))
                    {
                        filteredFlightList.Add(flightData);

                    }
                    else if ((data[3] == toInput && dayInput == "") || (data[3] == toInput && dayInput == "Any"))
                    {
                        filteredFlightList.Add(flightData);

                    }
                    else if ((data[4] == dayInput && toInput == "") || (data[4] == dayInput && toInput == "Any"))
                    {
                        filteredFlightList.Add(flightData);

                    }
                    else if ((data[3] == toInput && data[4] == dayInput))
                    {
                        filteredFlightList.Add(flightData);

                    }
                }
                else if ((data[3] == toInput || toInput == "Any") && fromInput != toInput)
                {
                    if ((fromInput == "" && dayInput == "") || (fromInput == "Any" && dayInput == "Any"))
                    {
                        filteredFlightList.Add(flightData);

                    }
                    else if ((data[2] == fromInput && dayInput == "") || (data[2] == fromInput && dayInput == "Any"))
                    {
                        filteredFlightList.Add(flightData);

                    }
                    else if ((data[4] == dayInput && fromInput == "") || (data[4] == dayInput && fromInput == "Any"))
                    {
                        filteredFlightList.Add(flightData);

                    }
                    else if ((data[3] == fromInput && data[4] == dayInput))
                    {
                        filteredFlightList.Add(flightData);

                    }
                }
                else if (data[4] == dayInput || dayInput == "Any" )
                {
                    if ((toInput == "" && fromInput == "") || (toInput == "Any" && fromInput == "Any"))
                    {
                        filteredFlightList.Add(flightData);

                    }
                    else if ((data[3] == toInput && fromInput == "") || (data[3] == toInput && fromInput == "Any"))
                    {
                        filteredFlightList.Add(flightData);

                    }
                    else if ((data[2] == fromInput && toInput == "") || (data[2] == fromInput && toInput == "Any"))
                    {
                        filteredFlightList.Add(flightData);

                    }
                    else if ((data[2] == fromInput && data[3] == toInput))
                    {
                        filteredFlightList.Add(flightData);
                    }
                }
                else if (fromInput == "Any" && toInput == "Any" && dayInput == "Any")
                {
                    filteredFlightList.Add(flightData);
                }
            }
            //If filtered list is 0 then it will display an error message
            if (filteredFlightList.Count == 0 )
            {
                flightsDropDown.ItemsSource = new List<String> { "No Flights found!" };

            }
            else
            {
                flightsDropDown.ItemsSource = filteredFlightList;

                flightsDropDown.SelectedIndexChanged += selectedFlightMethod;
            }
        }

        //Auto fills flight information based on the selected flight from the flight list
        public void selectedFlightMethod(object sender, EventArgs e)
        {
            if (flightsDropDown.SelectedItem != null)
            {
                string selectedFlightValue = flightsDropDown.SelectedItem.ToString();
                flightSelectedInput = selectedFlightValue;
                foreach (string flightData in flightList)
                {
                    string[] data = flightData.Split(',');

                    if (flightSelectedInput == flightData)
                    {
                        flightCodeDropDown.Text = $"Flight Code : {data[0]}";
                        flightCode = data[0];

                        airlineCodeDropDown.Text = $"Airline : {data[1]}";
                        flightAirline = data[1];

                        dayCodeDropDown.Text = $"Day : {data[4]}";
                        flightDay = data[4];

                        timeCodeDropDown.Text = $"Time : {data[5]}";
                        flightTime = data[5];

                        costCodeDropDown.Text = $"Cost : {data[7]}";
                        flightCost = data[7];
                    }
                }
            }
            //Reset text if no flight is found
            else
            {
                flightCodeDropDown.Text = $"Flight Code : ";

                airlineCodeDropDown.Text = $"Airline : ";

                dayCodeDropDown.Text = $"Day : ";

                timeCodeDropDown.Text = $"Time : ";

                costCodeDropDown.Text = $"Cost : ";

                errorText.Text = "";
            }
        }
        //Creates an exception to validate each requirement in order to make a reservation
        public void makeReservationButton(object sender, EventArgs e)
        {
            try
            {
                //File is located in the Res fodlder
                string RESERVATIONS_TXT = (@"..\..\..\..\..\Resources\Res\reservationData.txt");
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string fullPath = Path.GetFullPath(Path.Combine(baseDirectory, RESERVATIONS_TXT));

                //Validates booking
                if (flightsDropDown.SelectedItem == null || flightSelectedInput == "")
                {
                    throw new Exception($"No flight has been selected");
                }
                if (File.ReadAllLines(fullPath).Any(line => line.Contains(flightCode)))
                {
                    throw new Exception($"Flight is already booked!");
                }
                if (nameInput.Text == "" || !nameInput.Text.All(char.IsLetter))
                {
                    throw new Exception($"Please enter a valid name!");
                }
                if (citizenshipInput.Text == "" || !citizenshipInput.Text.All(char.IsLetter))
                {
                    throw new Exception($"Please enter a valid citizenship!");
                }

                errorText.Text = $"Reservation Successful";
                errorText.TextColor = new MauiColor(0,225,0);
                flightName = nameInput.Text;
                flightCitizenship = citizenshipInput.Text;
                using (StreamWriter newData = File.AppendText(fullPath))
                {
                    newData.WriteLine($"{flightCode},{flightAirline},{flightDay},{flightTime},{flightName},{flightCitizenship},${flightCost}");
                }
            }
            //If fails then displays an error message
            catch (Exception ex)
            {
                errorText.Text = $"{ex.Message}";
                errorText.TextColor = new MauiColor(255, 0, 0);
            }
        }

        //Adding flight, From, To, and Day by going through the flights.csv file
        public void flightFinderMethod()
        {

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativeFilePath = @"..\..\..\flights.csv";
            string absoluteFilePath = Path.GetFullPath(Path.Combine(baseDirectory, relativeFilePath));
            ;
            //Creates the drop down list for From, To, and Day. No duplicates
            List<String> fromValue = new List<string>();
            List<String> toValue = new List<string>();
            List<String> dayValue = new List<string>();

            using (StreamReader reader  = new StreamReader(absoluteFilePath))
            {
                fromValue.Add("Any");
                toValue.Add("Any");
                dayValue.Add("Any");

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] data = line.Split(',');
                    flightList.Add(line);
                    if (!fromValue.Contains(data[2] ))
                    {
                        fromValue.Add(data[2]);
                    }
                    if (!toValue.Contains(data[3]))
                    {
                        toValue.Add(data[3]);
                    }
                    if (!dayValue.Contains(data[4]))
                    {
                        dayValue.Add(data[4]);
                    }
                    
                }

            }
            fromDropDown.ItemsSource = fromValue;
            toDropDown.ItemsSource = toValue;
            dayDropDown.ItemsSource = dayValue;
            fromDropDown.SelectedIndexChanged += fromDropDownPicked;
            toDropDown.SelectedIndexChanged += toDropDownPicked;
            dayDropDown.SelectedIndexChanged += dayDropDownPicked;
        }

        //Sets value From, To, and Day when they are selected from drop down
        public void fromDropDownPicked(object sender, EventArgs e)
        {

            string valueSelected = fromDropDown.SelectedItem.ToString();
            fromInput = valueSelected;
        }
        public void toDropDownPicked(object sender, EventArgs e)
        {

            string valueSelected = toDropDown.SelectedItem.ToString();
            toInput = valueSelected;
        }
        public void dayDropDownPicked(object sender, EventArgs e)
        {

            string valueSelected = dayDropDown.SelectedItem.ToString();
            dayInput = valueSelected;
        }


    }

}
