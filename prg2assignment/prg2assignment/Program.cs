
//============================================================
// Student Number : S10204620, S10206093
// Student Name : Nicholas Chng, Nicolas Teo
// Module Group : P06
//============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Versioning;
using System.Net.NetworkInformation;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace prg2assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BusinessLocation> businessLocationList = new List<BusinessLocation>(); // Creation of businesslocation List
            List<Person> personList = new List<Person>(); // Creation of Person List
            List<SHNFacility> shnfacilList = new List<SHNFacility>(); // Creation of SHN Facility List
            bool[] is_loaded = { false, false }; // Array whether the appropriate data is loaded before option is chosen. first element is person status, 2nd is shn
            while (true) { // Loop for program to run indefinitely unless user exits

                // Could have some logical error in intialising data as if the person is alr staying in an SHN facility, it is not recorded into the code....
                MainMenu(); //Prints Main Menu
                Console.WriteLine("Input Choice"); //Asks user for input
                string choice = Console.ReadLine(); //Reads input
                //shnfacilList = LoadShnFacilityData(shnfacilList);

                switch (choice)
                {
                    case "1":
                        //1) Load Person and Business Location Data
                        if (is_loaded[1])
                        {
                            businessLocationList = LoadBusinessData(businessLocationList);
                            personList = LoadPersonData(shnfacilList, personList);
                            is_loaded[0] = true;
                        }
                        else
                        {
                            Console.WriteLine("SHN Facility Data not loaded.");
                        }
                        break;
                    case "2":
                        // 2) Load SHN Facility Data 
                        shnfacilList = LoadShnFacilityData(shnfacilList);
                        is_loaded[1] = true;
                        break;
                    case "3":
                        //3) List all Visitors 
                        if (is_loaded[0])
                            ListVisitors(personList);
                        else
                            Console.WriteLine("Person Data not loaded.");
                        break;
                    case "4":
                        //4)List Person Details
                        if (is_loaded[0])
                            ListPersonDetails(personList);
                        else
                            Console.WriteLine("Person Data not loaded.");
                        break;
                    case "5":
                        // 5) Assign / Replace TraceTogether Token
                        if (is_loaded[0])
                            AssignToken(personList);
                        else
                            Console.WriteLine("Person Data not loaded.");
                        break;
                    case "6":
                        // 6) List all business locations
                        if (is_loaded[0])
                            ListBizLocation(businessLocationList);
                        else
                            Console.WriteLine("Business Location Data not loaded.");
                        break;
                    case "7":
                        // 7) Edit business location capacity
                        if (is_loaded[0])
                            EditBizCapacity(businessLocationList);
                        else
                            Console.WriteLine("Business Location Data not loaded.");
                        break;
                    case "8":
                        // 8) SafeEntry Check-In
                        if (is_loaded[0])
                            CheckIn(personList, businessLocationList);
                        else
                            Console.WriteLine("Business Location and Person Data not loaded.");
                        break;
                    case "9":
                        // 9) SafeEntry Check-Out
                        if (is_loaded[0])
                            CheckOut(personList, businessLocationList);
                        else
                            Console.WriteLine("Business Location and Person Data not loaded.");
                        break;
                    case "10":
                        //Travel Entry Part10) List all SHN Facilities
                        if (is_loaded[1])
                            ListSHNFacilList(shnfacilList);
                        else
                            Console.WriteLine("SHN Facility Data not loaded.");
                        break;
                    case "11":
                        //Travel Entry Part11) Create Visitor
                        CreateVisitor();
                        break;
                    case "12":
                        //Travel Entry Part12) Create TravelEntry Record 
                        if (is_loaded[1] && is_loaded[0])
                            CreateTravelEntry(personList, shnfacilList);
                        else
                            Console.WriteLine("Person and SHN Facility Data not loaded.");
                        break;
                    case "13":
                        //Travel Entry Part13) Calculate SHN Charges 
                        if (is_loaded[1])
                            CalculateSHNChargesForPerson(personList);
                        else
                            Console.WriteLine("SHN Facility Data not loaded.");
                        break;
                    case "14":
                        //Advanced 3.1, Contact tracing reporting
                        if (is_loaded[0])
                            ContactTracing(personList, businessLocationList);
                        else
                            Console.WriteLine("Business Location Data not loaded.");
                        break;
                    case "15":
                        //Advanced 3.2, SHN Status Reporting
                        if (is_loaded[1])
                            SHNStatusReporting(personList);
                        else
                            Console.WriteLine("SHN Facility Data not loaded.");
                        break;
                    case "16":
                        //= Exit Menu 

                        return;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            }
        }
        static void MainMenu() // Function to print the Main Menu
        {
            Console.WriteLine();
            Console.WriteLine("[1]  Load Person and Business Location Data");
            Console.WriteLine("[2]  Load SHN Facility Data ");
            Console.WriteLine("[3]  List all Visitors");
            Console.WriteLine("[4]  List Person Details");
            Console.WriteLine("[5]  Assign/Replace TraceTogether Token");
            Console.WriteLine("[6]  List all Business Locations");
            Console.WriteLine("[7]  Edit Business Location Capacity");
            Console.WriteLine("[8]  SafeEntry Check-in");
            Console.WriteLine("[9]  SafeEntry Check-out");
            Console.WriteLine("[10] List all SHN Facilities");
            Console.WriteLine("[11] Create Visitor");
            Console.WriteLine("[12] Create TravelEntry Record");
            Console.WriteLine("[13] Calculate SHN Charges");
            Console.WriteLine("[14] Contact Tracing Reporting");
            Console.WriteLine("[15] SHN Status Reporting");
            Console.WriteLine("[16] Exit");

        }
        static List<BusinessLocation> LoadBusinessData(List<BusinessLocation> businessLocationList) // Method to load data from Business location Csv file 
                                                                                                    // To businessLocationList
        {

            //1) Load Person and Business Location Data
            //1 .load both given CSV and populate two lists 
            //Reading business Location Csv and adding into a list

            string[] Bdata = File.ReadAllLines("BusinessLocation.csv"); // Reads csv file
            for (int i = 1; i < Bdata.Length; i++) // For loop to loop through file 
            {
                string[] heading = Bdata[i].Split(','); // Splits the data 
                BusinessLocation x = new BusinessLocation(heading[0], heading[1], Convert.ToInt32(heading[2])); // Creation of Business Location Object
                businessLocationList.Add(x); // Adds newly created business location object to list.
            }
            return businessLocationList; // Returns the list filled with data/
        }
        static List<Person> LoadPersonData(List<SHNFacility> shnfacilList, List<Person> personList) // Method to Load data from Person csv file 
        {
            //============================================================
            string[] Pdata = File.ReadAllLines("Person.csv"); // Reads csv file 
            for (int i = 1; i < Pdata.Length; i++) // For loop to loop through file
            {

                string[] heading = Pdata[i].Split(','); // Splits the data 

                if (heading[0] == "resident") // Checks if the person object in question is a resident object or visitor object
                {                             // In this case. It is a resident object 
                    DateTime t = DateTime.ParseExact(heading[3], "dd/MM/yyyy", null); // DateTime Parse to process the datetime format of Csv file. Creates Datetime object t for the data
                    Resident x = new Resident(heading[1], heading[2], t); // Creates resident object
                    personList.Add(x); // Adds resident object to person list 
                    if (heading[6] == "") // Checks if token serial is blank.
                    {
                        // as it is blank no tracetogetherinformation is in the csv and thus no code was written. 
                    }
                    else
                    {   // There is tracetogetherinformation in csv, thus tracetogethertoken object has been created
                        TraceTogetherToken y = new TraceTogetherToken(heading[6], heading[7], Convert.ToDateTime(heading[8]));
                        x.Token = y; // Adds the tracetogethertoken object y into the resident class (x).
                    }
                    if (heading[9] == "") // Checks for travel entry data.
                    {
                        // as it is blank no TravelEntryData is in the csv and thus no code was written. 
                    }
                    else
                    {
                        // There is TravelEntryData in csv, thus TravelEntry object has been created
                        TravelEntry w = new TravelEntry(heading[9], heading[10], Convert.ToDateTime(heading[11])); // Creation of TravelEntry object w
                        w.ShnEndDate = DateTime.ParseExact(heading[12], "dd/MM/yyyy HH:mm", null); // Setting ShnEndDate of travelentry object w to TravelShnEndDate
                        w.IsPaid = Convert.ToBoolean(heading[13]); // Setting IsPaid of travel entry object w to IsShnPaid boolean value.
                        x.AddTravelEntry(w); // Adds newly created TravelEntry object w to TravelEntry List that is found in Resident Object x
                        if (heading[14] == "")
                        {
                            // Checks if FacilityName is present in Person data file. In this case it is not present
                        }
                        else
                        {
                            SHNFacility facilQ21 = null;  // Creates a Shn Facility object which is empty.
                            facilQ21 = SearchSHNFacil(shnfacilList, heading[14]); // Searches for the shnFacility with the name found in the csv file against ShnFacilityList
                            // SearchSHNFacil is a Method to look for a ShnFacility in any given ShnFacility list. Returns a SHNFacility object
                            facilQ21.FacilityVacancy -= 1; // As Resident is staying in SHNFacility, Vacancy of facility - 1 ERROR IS HERE
                            w.AssignSHNFacility(facilQ21);// Assigns a SHNFacility to travel entry object w. ( shnStay = shnfacility object )
                        }
                    }


                }
                else if (heading[0] == "visitor")
                {
                    // In this case. It is a Visitor object 
                    Visitor v = new Visitor(heading[1], heading[4], heading[5]); // Creates visitor object
                    personList.Add(v); // Adds visitor object to list.
                    if (heading[9] == "") // Checks for travel entry data.
                    {
                        // as it is blank no TravelEntryData is in the csv and thus no code was written. 

                    }
                    else
                    { // There is travel entry data.
                        DateTime oDate = DateTime.ParseExact(heading[11], "dd/MM/yyyy HH:mm", null); // DateTime Parse to process the datetime format of Csv file. Creates Datetime object oDate for the data
                        TravelEntry y = new TravelEntry(heading[9], heading[10], oDate); // Creates travel entry object y.
                        y.ShnEndDate = DateTime.ParseExact(heading[12], "dd/MM/yyyy HH:mm", null); // Setting ShnEndDate of travelentry object w to TravelShnEndDate
                        y.IsPaid = Convert.ToBoolean(heading[13]); // Setting IsPaid of travel entry object w to IsShnPaid boolean value. 
                        v.AddTravelEntry(y); // Adds TravelEntryObject y to person class v.
                        if (heading[14] == "")
                        {
                            // Checks if FacilityName is present in Person data file. In this case it is not present
                        }
                        else
                        {
                            SHNFacility facilQ21 = null; // Creates a Shn Facility object which is empty.
                            facilQ21 = SearchSHNFacil(shnfacilList, heading[14]); // Searches for the shnFacility with the name found in the csv file against ShnFacilityList
                            // SearchSHNFacil is a Method to look for a ShnFacility in any given ShnFacility list. Returns a SHNFacility object
                            facilQ21.FacilityVacancy -= 1; // As Visitor is staying in SHNFacility, Vacancy of facility - 1
                            y.AssignSHNFacility(facilQ21); // Assigns a SHNFacility to travel entry object y. ( shnStay = shnfacility object )
                        }
                    }

                }

            }
            return personList; // Returns person list. 
        }
        static List<SHNFacility> LoadShnFacilityData(List<SHNFacility> shnfacilList) // Method to load Shn Facility data into SHnFacility list
        {
            //2) Load SHN Facility Data
            //call API and populate a list 

            using (HttpClient client = new HttpClient()) // Code to call ShnFacility API
            {
                client.BaseAddress = new Uri("https://covidmonitoringapiprg2.azurewebsites.net");
                Task<HttpResponseMessage> responseTask = client.GetAsync("/facility");
                responseTask.Wait();
                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    Task<string> readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    string apidata = readTask.Result;
                    shnfacilList = JsonConvert.DeserializeObject<List<SHNFacility>>(apidata); // Populates ShnFacility List

                }
                foreach(var s in shnfacilList) // Loop to set Facility Vacancy to be equal to Facility Capacity as it can be assumed that the facility is vacant upon loading of shnFacilityData into list
                {
                    s.FacilityVacancy = s.FacilityCapacity;
                }

            }
            return shnfacilList; // Returns ShnFacility List. 
        }
        static void ListBizLocation(List<BusinessLocation> businessLocationList)
        {
            Console.WriteLine($"{"BusinessName",-7}|{"BranchCode",-7}|{"MaximumCapacity"}");
            foreach (var b in businessLocationList)
            {
                Console.WriteLine(b);
            }
        }
        static Person SearchPerson(string name, List<Person> personList) // Method to find person object while only knowing name of person and returning a person object
        {
            for (int i = 0; i < personList.Count; i++) // Loop to look at each variable in PersonList 
            {
                if (name == personList[i].Name) // If their name matches
                {
                    return personList[i]; // Returns person object
                }
            }
            return null; // Otherwise return null 
        }
        static bool checkforFacil(TravelEntry x) // Method to check if the person with TravelEntry x has stayed in a facility before
        {
            if (x.EntryDate.AddDays(14) == x.ShnEndDate) // Resident and Visitor only stays in SHN Facility if they need to serve 14 days.
            {
                return true; // Returns true if stayed before
            }
            else
            {
                return false; // Returns false if never stayed before 
            }
        }
        static void ListSHNFacilList(List<SHNFacility> shnList) // Method to print out all ShnFacility data in any given ShnFacility List 
        {
            Console.WriteLine("{0,-7}|{1,-7}|{2,-7}|{3,-7}|{4,-7}|{5,-7}", "FacilityName", "FacilityCapacity","Facility Vacancy", "distFromAirCheckpoint", "distFromSeaCheckpoint", "distFromLandCheckpoint");
            foreach (var s in shnList)
            {
                Console.WriteLine(s);
            }
        }
        static SHNFacility SearchSHNFacil(List<SHNFacility> shnFacilList, string name) // Method to look for a ShnFacility in any given ShnFacility list. Returns a SHNFacility object
        {
            for (int i = 0; i < shnFacilList.Count; i++) // Loop to look through ShnFacility list 
            {
                if (name == shnFacilList[i].FacilityName) // If Shnfacility name and name given is the same. Return ShnFacility object
                {
                    return shnFacilList[i];
                }
            }
            return null;
        }
        static void AddPersonToSHNFacil(List<SHNFacility> shnFacilList, string chosen, TravelEntry x) // Method to add Person to SHNFacility
        {
            SHNFacility facil = null;
            if (chosen == "1")
            {
                facil = SearchSHNFacil(shnFacilList, "A'Resort");
                // SearchSHNFacil is a Method to look for a ShnFacility in any given ShnFacility list. Returns a SHNFacility object
                // Searches for the shnFacility with the name given against ShnFacilityList
                // As someone is staying in SHNFacility, Vacancy of facility - 1
                // Assigns a SHNFacility to travel entry object x. ( shnStay = shnfacility object )
                facil.FacilityVacancy -= 1;
                x.AssignSHNFacility(facil);
            }
            else if (chosen == "2")
            {
                facil = SearchSHNFacil(shnFacilList, "Yozel");
                facil.FacilityVacancy -= 1;
                x.AssignSHNFacility(facil);
            }
            else if (chosen == "3")
            {
                facil = SearchSHNFacil(shnFacilList, "Mandarin Orchid");
                facil.FacilityVacancy -= 1;
                x.AssignSHNFacility(facil);
            }
            else if (chosen == "4")
            {
                facil = SearchSHNFacil(shnFacilList, "Small Hostel");
                facil.FacilityVacancy -= 1;
                x.AssignSHNFacility(facil);
            }

        }
        static TravelEntry SearchUnpaidSHN(Person x) // Method to check if someone has an unpaid Shn. Takes in person object
        {
            for (int i = 0; i < x.TravelEntryList.Count; i++)
            {
                if (x.TravelEntryList[i].IsPaid == false && DateTime.Compare(x.TravelEntryList[i].ShnEndDate, DateTime.Now) < 0)
                {
                    // If SHN is not paid and the ShnEndDate is later than or the same day as today. Returns TravelEntry object
                    return x.TravelEntryList[i];
                }
                else { }

            } // if none is found. Returns null
            return null;
        }
        static bool CheckDateTimeFormat(string x) // Method to check datetime format
        {
            try // Try to check if datetime meets requirements. if requirements not met then try and catch to prevent program from crashing
            {
                DateTime y = DateTime.ParseExact(x, "dd/MM/yyyy", null);
                return true; // Returns true if datetimeformat is correct
            }
            catch
            {
                return false; // Returns false if datetimeformat is wrong.
            }
        }
        static void ListVisitors(List<Person> pList) { // Method to list all visitors in personlist 
            foreach (var p in pList) // Loop through list to check through object
            {
                if (p.GetType().Equals(typeof(Visitor))) // If the object is a visitor, it will print the visitor object
                    Console.WriteLine(p);
            }
        }
        static void ListPersonDetails(List<Person> pList) { // Method to print all details of person ( Resident or visitor ) 
            Console.Write("Enter Person Name: "); // Asks for name
            string pName = Console.ReadLine(); // Reads name
            var p = SearchPerson(pName, pList); // Method to find person object while only knowing name of person and returning a person object or null if none is found
            if (p is null) { // If person object is not found
                Console.WriteLine($"'{pName}' does not exist in the database.");
            }
            else if (p.GetType().Equals(typeof(Resident))) {
                Console.WriteLine((Resident)p); // If Person object is found and it is a resident 
            }
            else
            { // If Person object is found and it is a Visitor 
                Console.WriteLine((Visitor)p);
            }
        }
        static void AssignToken(List<Person> pList) {
            string rx = @"^T\d{5}\b";
            Console.Write("Enter Person Name: ");
            string pName = Console.ReadLine();
            var p = SearchPerson(pName, pList);
            if (p is null) {
                Console.WriteLine($"'{pName}' does not exist in the database.");
            }
            else if (p.GetType().Equals(typeof(Resident))) {
                Resident r = (Resident)p;
                //Console.WriteLine(p.GetType()); // OUTPUTS RESIDENT
                if (r.Token is null || r.Token.IsEligibleForReplacement()) {
                    Console.Write("Enter Serial No (Txxxx): ");
                    string serial = Console.ReadLine();
                    bool serialValid = Regex.IsMatch(serial, rx);
                    while (!serialValid)
                    {
                        Console.Write("Enter Valid Serial No (Txxxx): ");
                        serial = Console.ReadLine();
                        serialValid = Regex.IsMatch(serial, rx);
                    }
                    Console.Write("Enter Pickup Location: ");
                    string pickup = Console.ReadLine();
                    if (r.Token is null)
                        r.Token = new TraceTogetherToken();
                    r.Token.ReplaceToken(serial, pickup);
                    Console.WriteLine("Token has been assigned.");

                }
                else {
                    Console.WriteLine("Token not eligible for replacement/assignment.");
                }
            }
            else {
                Console.WriteLine($"'{pName}' is not a Resident.");
            }
        }
        static void CreateVisitor() // Creates visitor class
        {
            Console.WriteLine("Please input your name"); // Asks user for name 
            string name11 = Console.ReadLine().Trim(); // .Trim() removes unnecessary spacing. No validation is needed as the name would be a string regardless
            Console.WriteLine("Please input your passportNo"); // .Trim() removes unnecessary spacing. No validation is needed as there are no passportNo requirements 
            string passportNo11 = Console.ReadLine().Trim();
            Console.WriteLine("Please input your Nationality"); // .Trim() removes unnecessary spacing. No validation is needed as there are no nataionality requirements given
            string nationality11 = Console.ReadLine().Trim();
            Visitor a = new Visitor(name11, passportNo11, nationality11); // Creates visitor class a
            Console.WriteLine("Visitor Object has been created."); // Print statement to inform user that a visitor class has been created 
        }
        static void CreateTravelEntry(List<Person> personList, List<SHNFacility> shnfacilList) // Method to create travel entry object
        {
            Console.WriteLine("Please enter a name"); // Asks user for name 
            string name12 = Console.ReadLine().Trim(); 
            Person search_person_12 = SearchPerson(name12, personList); 
            while (SearchPerson(name12, personList) == null) // Validation. If no person found from personList, asks user to input valid name 
            { // code will keep on looping until valid name is given 
                Console.WriteLine("There is no such name in the database. Please enter a valid name.");
                name12 = Console.ReadLine().Trim(); // INPUT
                search_person_12 = SearchPerson(name12, personList);

            }
            Console.WriteLine("Please enter your Last country of Embarkation.");
            string lastCont12 = Console.ReadLine().Trim(); // .Trim() removes unnecessary spacing. No validation is needed as there are no last country of embarkation requirements
            Console.WriteLine("Please enter your mode of entry");
            string entrymod12 = Console.ReadLine().Trim(); // .Trim() removes unnecessary spacing. validation is needed as user can only fill in Land Sea or Air as mode of entry 
            while (true)
            { // Code will loop and ask user for input until Land Sea or Air are given as inputs
                if (entrymod12 == "Land")
                {
                    break;
                }
                else if (entrymod12 == "Sea")
                {
                    break;
                }
                else if (entrymod12 == "Air")
                {
                    break;
                }
                Console.WriteLine("Invalid input. Pls input Mode of Entry as either Land, Sea or Air.");
                entrymod12 = Console.ReadLine().Trim(); // INPUT
            }
            Console.WriteLine("Please enter your date of entry in the format (dd/MM/YYYY)"); // Asks for user input of date
            string entrystringdate12 = Console.ReadLine().Trim(); // INPUT
            if (CheckDateTimeFormat(entrystringdate12) == true) // Method to check datetime format requirements
            {
                // Method to check datetime format requirements. If met returns true 
            }
            else if (CheckDateTimeFormat(entrystringdate12) == false)
            { // Method to check datetime format requirements. If not met, puts user in a loop until datetime format requirement of input is correct.
                while (CheckDateTimeFormat(entrystringdate12) == false)
                {
                    Console.WriteLine("Input error. Please enter Date of entry in the format (dd/MM/YYYY) ");
                    entrystringdate12 = Console.ReadLine().Trim();
                }

            }
            DateTime entrydate12 = DateTime.ParseExact(entrystringdate12, "dd/MM/yyyy", null); // Creates  datetime object entrydate 12.
            TravelEntry b = new TravelEntry(lastCont12, entrymod12, entrydate12); // Creates TravelEntry object b with user inputted values
            b.CalculateSHNDuration(); // Calculate the ShnEndDate
            if (checkforFacil(b) == true) // Checks if person needs to stay at ShnFacility.
            {
                ListSHNFacilList(shnfacilList); // List all avaliable shn facilities 
                Console.WriteLine("Please choose a facility. ( A'Resort being 1 and so forth )"); // Lets user choose facility
                string chosen = Console.ReadLine(); 
                while (true)
                { // Validation to check if user input is within 1-5 
                    // Requires adding some code
                    if (chosen == "1")
                    {
                        break;
                    }
                    else if (chosen == "2")
                    {
                        break;
                    }
                    else if (chosen == "3")
                    {
                        break;
                    }
                    else if (chosen == "4")
                    {
                        break;
                    }
                    else if (chosen == "5")
                    {
                        break;
                    }
                    Console.WriteLine("Pls select a number between 1 and 5.");
                    chosen = Console.ReadLine();

                }
                AddPersonToSHNFacil(shnfacilList, chosen, b); // Adds person to chosen facility
                search_person_12.AddTravelEntry(b); // Adds TravelEntry b to person object.

            }
            else
            {
                Console.WriteLine("Facility not needed!");
                // Add validaition and loop here.
            }
        }
        static void CalculateSHNChargesForPerson(List<Person> personList) // Method to calculate SHN Charges for a person 
        {
            
            Console.WriteLine("Please input name");
            string name13 = Console.ReadLine().Trim(); // User input their name 
            Person search_person_13 = SearchPerson(name13, personList); // Validation to check if person can be found in given personList
            while (SearchPerson(name13, personList) == null)
            {
                Console.WriteLine("There is no such name in the database. Please enter a valid name.");
                Console.WriteLine("Please enter a name");
                name13 = Console.ReadLine().Trim(); // INPUT
                search_person_13 = SearchPerson(name13, personList);

            }
            TravelEntry search_person_travelentry_13 = SearchUnpaidSHN(search_person_13); // Method to check if someone has an unpaid Shn. Takes in person object. If no such travel entry is found, returns null

            double unpaid = 0.0; //Unpaid is the amount user needs to pay 
            if (search_person_13 is Resident && search_person_travelentry_13 != null) // If travel entry found and person is a resident 
            {
                int days = 0; // Int days variable to check if perosn stayed needs SHN 
                if (search_person_travelentry_13.LastCountryOfEmbarkation == "New Zealand" ||
                    search_person_travelentry_13.LastCountryOfEmbarkation == "Vietnam")
                {

                }
                else if (search_person_travelentry_13.LastCountryOfEmbarkation == "Marcao SAR")
                {
                    days = 7;
                }
                else
                {
                    days = 14;

                }
                if (days == 0)
                    unpaid += 200 * 1.07; // If no SHN Needed ( Swab test cost with GST ) 
                else if (days == 7)
                {
                    unpaid += 220 * 1.07; // If no SHN Needed. Swab test and Transportation fees included. ( With GST ) 
                }
                else if (days == 14)
                {
                    unpaid += search_person_13.CalculateSHNCharges(); // If SHN Needed 
                }
                Console.WriteLine("User pls make payment of: {0:C2}", unpaid); // Asks user to make payment 
                search_person_travelentry_13.IsPaid = true; // User has paid 
            }
            else if (search_person_13 is Visitor && search_person_travelentry_13 != null) // If travel entry found and person is a visitor 
            {
                int days = 0;

                if (search_person_travelentry_13.LastCountryOfEmbarkation == "New Zealand" ||
                    search_person_travelentry_13.LastCountryOfEmbarkation == "Vietnam")
                {

                }
                else if (search_person_travelentry_13.LastCountryOfEmbarkation == "Marcao SAR")
                {
                    days = 7;
                }
                else
                {
                    days = 14;

                }
                if (days == 0)
                    unpaid += 280 * 1.07; // If no SHN Needed ( Swab test cost with GST ) 
                else if (days == 7)
                {
                    unpaid += 280 * 1.07; // If no SHN Needed. Swab test and Transportation fees included. ( With GST )
                }
                else if (days == 14)
                {
                    unpaid += search_person_13.CalculateSHNCharges() + search_person_travelentry_13.ShnStay.CalculateTravelCost(search_person_travelentry_13.EntryMode, search_person_travelentry_13.EntryDate);
                    // If SHN Needed 
                }
                Console.WriteLine("User pls make payment of: {0:C2}", unpaid); // Asks user to make payment 
                search_person_travelentry_13.IsPaid = true; // User has paid 
            }
            else if (search_person_travelentry_13 is null) // If no travelentry that is unpaid found, tell user no payment needed. 
            {
                Console.WriteLine("There is no Unpaid SHN for " + search_person_13.Name);
            }
        }
        static void SHNStatusReporting(List<Person> personList) // Method to check if anybody have SHN given a date. 
        {
            Console.WriteLine("Please input a date: (In the format of dd/MM/yyyy)");
            string advanced_string_3 = Console.ReadLine().Trim();
            if (CheckDateTimeFormat(advanced_string_3) == true) // Asks user to input date and validates that date
            {

            }
            else if (CheckDateTimeFormat(advanced_string_3) == false)
            {
                while (CheckDateTimeFormat(advanced_string_3) == false)
                {
                    Console.WriteLine("Input error. Please enter Date of entry in the format (dd/MM/YYYY) ");
                    advanced_string_3 = Console.ReadLine().Trim();
                }

            }
            DateTime advanced_datetime_3 = DateTime.ParseExact(advanced_string_3, "dd/MM/yyyy", null);
            string headers = "Name,ShnEndDate,ShnFacilityName\n"; // headers for csv file 
            File.WriteAllText("ShnStatusReport.csv", headers); // writes in csv file 
            foreach (var s in personList)
            {
                foreach (var t in s.TravelEntryList)
                {
                    
                    if(DateTime.Compare(advanced_datetime_3, t.EntryDate) >= 0 && DateTime.Compare(advanced_datetime_3,t.ShnEndDate) <=0 )
                    {
                        if (t.ShnStay == null) // if person did not stay in SHN facility  
                        {

                        }
                        else
                        {

                            // if person stayed in SHN Facility 
                            string appended = s.Name + "," + t.ShnEndDate + "," + t.ShnStay.FacilityName + "\n";
                            File.AppendAllText("ShnStatusReport.csv", appended);
                            // Appends new information to csv file 
                        }

                    }
                    else
                    {

                    }
                }
            }
        }
        static BusinessLocation SearchBizLoc(List<BusinessLocation> businessLocationList, string name)
        {
            foreach(var b in businessLocationList)
            {
                if (name == b.BusinessName)
                {
                    return b;
                }
            }
            return null;
        }
        static void EditBizCapacity(List<BusinessLocation> businessLocationList)
        {
            Console.Write("Enter Business Name: ");
            string bizname = Console.ReadLine();
            BusinessLocation searchResult = SearchBizLoc(businessLocationList, bizname);
            while (searchResult is null)
            {
                Console.Write("Enter Valid Business Name: ");
                bizname = Console.ReadLine();
                searchResult = SearchBizLoc(businessLocationList, bizname);
            }
            Console.Write("Enter new maximum capacity: ");
            bool success = int.TryParse(Console.ReadLine(), out int c);
            while (!success || c < 0) {
                Console.Write("Enter new valid maximum capacity: ");
                success = int.TryParse(Console.ReadLine(), out c);
            }
            int index = businessLocationList.IndexOf(searchResult);
            businessLocationList[index].MaximumCapacity = c;

        }
        static void CheckIn(List<Person> pList, List<BusinessLocation> bList)
        {
            Console.Write("Enter Person Name: ");
            string pname = Console.ReadLine();
            Person searchResult1 = SearchPerson(pname, pList);
            while (searchResult1 is null)
            {
                Console.Write("Enter Valid Person Name: ");
                pname = Console.ReadLine();
                searchResult1 = SearchPerson(pname, pList);
            }

            ListBizLocation(bList);
            Console.Write("Enter Business Name: ");
            string bizname = Console.ReadLine();
            BusinessLocation searchResult2 = SearchBizLoc(bList, bizname);
            while (searchResult2 is null)
            {
                Console.Write("Enter Valid Business Name: ");
                bizname = Console.ReadLine();
                searchResult2 = SearchBizLoc(bList, bizname);
            }

            if (!searchResult2.IsFull())
            {
                SafeEntry se = new SafeEntry(DateTime.Now, searchResult2);
                searchResult1.AddSafeEntry(se);
                searchResult2.VisitorsNow++;
                Console.WriteLine("SafeEntry added.");
            }
            else
            {
                Console.WriteLine("Location has reached its maximum capacity.");
            }

        }
        static void CheckOut(List<Person> pList, List<BusinessLocation> bList)
        {
            Console.Write("Enter Person Name: ");
            string pname = Console.ReadLine();
            Person searchResult = SearchPerson(pname, pList);
            while (searchResult is null)
            {
                Console.Write("Enter Valid Person Name: ");
                pname = Console.ReadLine();
                searchResult = SearchPerson(pname, pList);
            }
            if (searchResult.SafeEntryList.Count() != 0)
            {
                ListSafeEntry(searchResult.SafeEntryList);
                Console.Write("Select Business Name from Record to Check Out: ");
                string bizname = Console.ReadLine();
                BusinessLocation location = SearchBizLoc(bList, bizname);
                while (location is null)
                {
                    Console.Write("Enter Valid Business Name: ");
                    bizname = Console.ReadLine();
                    location = SearchBizLoc(bList, bizname);
                }
                SafeEntry entryResult = SearchSafeEntry(searchResult.SafeEntryList, location);
                int index = searchResult.SafeEntryList.IndexOf(entryResult);
                searchResult.SafeEntryList[index].PerformCheckOut();
                Console.WriteLine("Successfully check out.");
                location.VisitorsNow--;

            }
            else
            {
                Console.WriteLine($"No Safe Entry Records for {searchResult.Name}.");
            }
            

        }
        static void ListSafeEntry(List<SafeEntry> sList)
        {
            Console.WriteLine($"{"Location",-7}|{"CheckIn"}");
            foreach(var s in sList)
            {
                if (s.CheckOut == DateTime.MinValue) //hack, min value equivalent to null
                    Console.WriteLine(s);
            }
        }
        static SafeEntry SearchSafeEntry(List<SafeEntry> sList, BusinessLocation location)
        {
            foreach (var s in sList)
            {
                if (location == s.Location)
                {
                    return s;
                }
            }
            return null;
        }
        static void ContactTracing(List<Person> pList, List<BusinessLocation> bList)
        {
            //List<String> exportList = new List<String>();
            string headers = "Name,Check In,Check Out";

            Console.Write("Enter Datetime: ");
            string dt = Console.ReadLine();
            while (!CheckDateTimeFormat(dt))
            {
                Console.Write("Enter Datetime: ");
                dt = Console.ReadLine();
            }
            DateTime dtdt = Convert.ToDateTime(dt); // type casting to new variable
            Console.Write("Enter Business Name: ");
            string bizname = Console.ReadLine();
            BusinessLocation biz = SearchBizLoc(bList, bizname);
            while (biz is null)
            {
                Console.Write("Enter Valid Business Name: ");
                bizname = Console.ReadLine();
                biz = SearchBizLoc(bList, bizname);
            }

            File.WriteAllText("ContactTracingReport.csv", headers + Environment.NewLine);

            foreach(Person p in pList)
            {
                List<SafeEntry> safeEntryList = p.SafeEntryList;
                foreach (SafeEntry se in safeEntryList)
                {
                    if (se.Location == biz) // wont add any more to next if statement cus its TOO LONG/ different purposes
                    {
                        if (se.CheckOut == default)
                        {
                            se.CheckOut = DateTime.MaxValue;
                        }
                        //Console.WriteLine(DateTime.Compare(se.CheckOut, dtdt) >= 0); // DEBUG 1
                        //Console.WriteLine(DateTime.Compare(se.CheckIn, dtdt) < 0);  // DEBUG 2
                        //Console.WriteLine(); // DEBUG 3
                        if (DateTime.Compare(se.CheckOut, dtdt) >= 0 && DateTime.Compare(se.CheckIn, dtdt) < 0)
                        {
                            /* -1 : checkout < dt
                             *  0 : checkout = dt
                             *  1 : checkout > dt
                             *  Check out time being greater than time given means that they're currently not checked out
                             *  Check in time being less than time given means they're currently checked in
                             *  Extra condition is to ensure check out actually occurred.
                            */
                            Console.WriteLine(se);
                            //exportList.Add($"{p.Name},{se.CheckIn},{se.CheckOut}");
                            if (se.CheckOut == DateTime.MaxValue)
                            {
                                File.AppendAllText("ContactTracingReport.csv", $"{p.Name},{se.CheckIn}," + Environment.NewLine);
                            }
                            else
                                File.AppendAllText("ContactTracingReport.csv", $"{p.Name},{se.CheckIn},{se.CheckOut}" + Environment.NewLine);
                        }
                    }
                }
            }

        }
    }
}
