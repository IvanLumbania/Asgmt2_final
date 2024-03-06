using System.Diagnostics;

namespace Asgmt2
{
    public partial class MainPage : ContentPage
    {
        //reseravtion searchbar input
        string reservationSelectedInput = "";

        //List of all the flights

        public MainPage()
        {
            InitializeComponent();
        }

        public void OnFindReservation(object sender, EventArgs e)
        {
            //this is meant to reset the whole list of reservations
            //JUST THE ONE IN THE CODE NOT THE FILE
            //if you can figure out a better way to fix the table, be my guest
            List<String> reservationList = new List<String>();

            string RESERVATIONS_TXT = (@"..\..\..\..\..\Resources\Res\reservationData.txt");
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.GetFullPath(Path.Combine(baseDirectory, RESERVATIONS_TXT));
            string[] reservations = File.ReadAllLines(fullPath);
            string[] reservationData;

            foreach (string reservation in reservations)
            {
                //splitting into words
                reservationData = reservation.Split(',');
                //Checks if text file is empty and proceeds to break loop if it is
                if (reservations.Length <= 0)
                {
                    reservationsDropDown.ItemsSource = new List<String> { "No Reservations found!" };
                    break;
                }


                //find resevervaion via CODE
                if (codeSearch.Text != null && reservationData[0].ToUpper() == codeSearch.Text.ToUpper() )
                {
                    if ((airlineSearch.Text == null || airlineSearch.Text == "" )&&( nameSearch.Text == null || nameSearch.Text == ""))
                    {
                        reservationList.Add(reservation.ToString());
                    }
                    else if ((nameSearch.Text == null || nameSearch.Text == "") && reservationData[1].ToLower() == airlineSearch.Text.ToLower())
                    {
                        reservationList.Add(reservation.ToString());
                    }
                    else if ((airlineSearch.Text == null || airlineSearch.Text == "") && reservationData[4].ToLower() == nameSearch.Text.ToLower())
                    {
                        reservationList.Add(reservation.ToString());
                    }
                    else if ((airlineSearch.Text != null && reservationData[1].ToLower() == airlineSearch.Text.ToLower()) && (nameSearch.Text != null && reservationData[4].ToLower() == nameSearch.Text.ToLower()))
                    {
                        reservationList.Add(reservation.ToString());
                    }

                }
                //find reservation via AIRLINE
                else if(airlineSearch.Text != null && reservationData[1].ToLower() == airlineSearch.Text.ToLower() )
                {
                    if ((codeSearch.Text == null || codeSearch.Text == "") && (nameSearch.Text == null ||nameSearch.Text == ""))
                    {
                        reservationList.Add(reservation.ToString());
                    }
                    else if ((nameSearch.Text == null || nameSearch.Text == "") && reservationData[0].ToUpper() == codeSearch.Text.ToUpper())
                    {
                        reservationList.Add(reservation.ToString());
                    }
                    else if ((codeSearch.Text == null || codeSearch.Text == "" || codeSearch == null)&& reservationData[4].ToLower() == nameSearch.Text.ToLower())
                    {
                        reservationList.Add(reservation.ToString());
                    }
                    else if ((codeSearch.Text != null && reservationData[0].ToUpper() == codeSearch.Text.ToUpper()) && (nameSearch.Text != null && reservationData[4] == nameSearch.Text.ToLower()))
                    {
                        reservationList.Add(reservation.ToString());
                    }
                }
                //find reservation via NAME
                else if (nameSearch.Text != null && reservationData[4].ToLower() == nameSearch.Text.ToLower() )
                {
                    if ((codeSearch.Text == null || codeSearch.Text == "") && (airlineSearch.Text == null || airlineSearch.Text == ""))
                    {
                        reservationList.Add(reservation.ToString());
                    }
                    else if ((airlineSearch.Text == null || airlineSearch.Text == "") && reservationData[0].ToLower() == codeSearch.Text.ToLower())
                    {
                        reservationList.Add(reservation.ToString());
                    }
                    else if ((codeSearch.Text == null || codeSearch.Text == "") && reservationData[1].ToLower() == airlineSearch.Text.ToLower())
                    {
                        reservationList.Add(reservation.ToString());
                    }
                    else if ((codeSearch.Text != null && reservationData[0].ToUpper() == codeSearch.Text.ToUpper()) && (airlineSearch.Text != null && reservationData[1].ToLower() == airlineSearch.Text.ToLower()))
                    {
                        reservationList.Add(reservation.ToString());
                    }

                }
            }
            //check for empty reservation list
            if ((codeSearch.Text == null || codeSearch.Text == "") && (airlineSearch.Text == null || airlineSearch.Text == "") && (nameSearch.Text == null || nameSearch.Text == ""))
            {
                reservationsDropDown.ItemsSource = new List<String> { "No Reservations found!" };
            }
            else if (reservationList.Count == 0)
            {
                reservationsDropDown.ItemsSource = new List<String> { "No Reservations found!" };
            }
            //for each reserveration in the list, add it to the drop down
            else
            {
                reservationsDropDown.ItemsSource = reservationList;
            }
        }
    }

}
