using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using static System.Text.Json.JsonElement;

namespace Project_B
{
    class Movies
    {
        public int movieID { get; set; }
        public string MovieName { get; set; }
        public DateTime startTime { get; set; }
        public int runTime { get; set; }
        public string genre { get; set; }
        public string director { get; set; }
        public Screen whichScreen { get; set; }
        public int screenNumber { get; set; }
        public string movieType { get; set; }
        public string Synopsis { get; set; }
        public List<string> ScreenRows { get; set; }

        public Movies(int movieID, string movieName, DateTime startTime,
             int screenNumber, int runTime, string genre, string director, string movieType, string synopsis)
        {
            this.movieID = movieID;
            this.MovieName = movieName;
            this.startTime = startTime;
            this.runTime = runTime;
            this.genre = genre;
            this.director = director;
            this.whichScreen = (Screen)Screen.screenList[screenNumber];
            this.screenNumber = screenNumber;
            this.movieType = movieType;
            this.Synopsis = synopsis;
        }

        public static void DisplayMovies()
        {
            foreach (Movies movie in Program.movies)
            {
                movieCatalog(movie);
            }

        }

        //Overflow functie als de user op sleutelwoorden wilt zoeken.
        public static void DisplayMovies(string[] terms)
        {
            foreach (Movies movie in Program.movies)
            {
                for (int i = 0; i < terms.Length; i++)
                {
                    bool movieNameChecker = false;
                    bool movieGenreChecker = false;
                    //Deze foreach loops kijken of individuele termen overeenkomen met filmtitels of genres.
                    foreach (String part in movie.MovieName.Split())
                    {
                        if (part.ToLower() == terms[i].ToLower())
                        {
                            movieNameChecker = true;
                            break;
                        }
                    }
                    foreach (String part in movie.genre.Split(","))
                    {
                        if (part.ToLower() == terms[i].ToLower())
                        {
                            movieGenreChecker = true;
                            break;
                        }
                    }
                    if (terms[i].ToLower() == movie.genre.ToLower() || movieNameChecker || movieGenreChecker ||
                    terms[i] == "" + movie.movieID || terms[i].ToLower() == movie.director.ToLower())
                    {
                        movieCatalog(movie);
                        break;
                    }
                }
            }
        }

        public static void DisplayMovies(int overFlow)
        {
            string print = "";
            foreach(Movies movie in Program.movies)
            {
                print += movie.movieID + ")" + movie.MovieName + " : " + movie.startTime.ToString("dd/MM/yyyy HH:mm") +"\n";
            }
            Console.WriteLine(print);
        }

        //deze method print de status van een zaal.

        public static void ScreenSeats(Movies movie)
        {
            Console.WriteLine($"\n\nStatus van zaal {movie.whichScreen.ScreenNumber} tijdens {movie.MovieName} om {movie.startTime.ToString("dd/MM/yyyy HH:mm")}:");
            Console.WriteLine("\"X\" = bezet, \"O\" = vrij\n");
            int i = 1;
            foreach (string row in movie.ScreenRows)
            {
                if (i < 10)
                {
                    if (i == 1)
                    {
                        Console.Write("           ");
                        for (int j = 1; j < row.Length+1; j++)
                        {
                            if (j < 10)
                            {
                                Console.Write(j + "  ");
                            }
                            else
                            {
                                Console.Write(j + " ");
                            }
                        }
                        Console.Write("\n Rij 1   : ");
                        for (int j = 0; j < row.Length; j++)
                        {
                            Console.Write(row[j] + "  ");
                        }
                    }
                    else
                    {
                        Console.Write($"\n Rij {i}   : ");
                        for (int j = 0; j < row.Length; j++)
                        {
                            Console.Write(row[j] + "  ");
                        }
                    }
                }
                else if (i >= 10 && i < movie.ScreenRows.Count)
                {
                    Console.Write($"\n Rij {i}  : ");
                    for (int j = 0; j < row.Length; j++)
                    {
                        Console.Write(row[j] + "  ");
                    }
                }
                else
                {
                    Console.Write($"\n Vip rij : ");
                    for (int j = 0; j < row.Length; j++)
                    {
                        Console.Write(row[j] + "  ");
                    }
                    Console.WriteLine();
                }
                i++;
            }
        }
        //in deze methode wordt een rij gekozen door de klant
        // het nummer van de rij wordt gereturned en gebruikt in SelectSeat
        public int SelectRow(bool vip)
        {
            Console.WriteLine("\nVoer het nummer in van de rij waar u wilt zitten:");
            if (vip)
            {
                return whichScreen.AmountOfRows;
            }
            else
            {
                int choice = -1;
                while (choice < 1 || choice > whichScreen.AmountOfRows - 1)
                {
                    try
                    {
                        choice = int.Parse(Console.ReadLine());
                        if (choice < 1 || choice > whichScreen.AmountOfRows - 1)
                        {
                            Console.WriteLine($"Voer een getal tussen 1 en {whichScreen.AmountOfRows - 1} in.");
                        }
                        else
                        {
                            return choice;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Voer een getal tussen 1 en {whichScreen.AmountOfRows - 1} in.");
                    }
                }
                return choice;
            }
        }
        //deze methode vult de ScreenRows list op basis van het aantal geselecteerde stoelen door de klant
        //en op basis van welke rij en stoel de klant gekozen heeft. 
        void fillScreenLayout(int seatsPerRow, int selectedRow, int selectedSeat, int numberOfPeople)
        {
                for (int i = 0; i < ScreenRows.Count; i++)
            {
                string oldRow = ScreenRows[i];
                string newRow = "";
                string[] rowChars = new string[seatsPerRow];
                for (int k = 0; k < ScreenRows[i].Length; k++)
                {
                    rowChars[k] = oldRow[k].ToString();
                }

                for(int j = 0; j < rowChars.Length - 1; j++)
                {
                    if (rowChars[j] == "O" && i == selectedRow && j == selectedSeat && numberOfPeople > 0)
                    {
                        //deze loop zorgt er voor dat de klanten naast elkaar zitten zodra de meest linkerstoel
                        //gekozen is (de j++ buiten de initiele loop zorgt er voor dat de rij niet langer wordt)
                        for (int k = 0; k < numberOfPeople; k++)
                        {
                            newRow = newRow + "X";
                            if (k < numberOfPeople - 1)
                            {
                                j++;
                            }
                        }
                    }
                    else if(rowChars[j] == "X")
                    {
                        newRow = newRow + "X";
                    }
                    else
                    {
                        newRow = newRow + "O";
                    }
                }
                ScreenRows[i] = newRow;
            }
        }

        
        /**In deze methode wordt de stoel gekozen door de klant.
        Ook wordt op basis van het aantal mensen bepaald wat de meest rechter stoel is die de klant kan kiezen.
        Dit omdat bij meerdere personen de stoelen rechts van de gekozen stoel worden geselecteerd voor de overige klanten.
        Dit voorkomt dat een klant op een niet bestaand stoelnummer geplaatst wordt */
        public int SelectSeat(int row, int numberOfPeople)
        {
            Console.WriteLine("Voer het nummer in van de stoel waar u wilt zitten\n" +
                "Als u voor meerdere personen reserveert, selecteert u de linker stoel.\n" +
                "De stoelen rechts van uw keuze worden automatisch geselecteerd:");
            int seatsPerRow = whichScreen.AmountOfSeats / whichScreen.AmountOfRows;
            seatsPerRow = (seatsPerRow - numberOfPeople) + 1;
            
            int choice = -1;
            while (choice < 1 || choice > seatsPerRow)
            {
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    bool occupied = false;

                    for(int i = 0; i < numberOfPeople; i++)
                    {
                        if(ScreenRows[row - 1][(choice -1) + i] == 'X')
                        {
                            occupied = true;
                        }
                    }

                    if (choice < 1 || choice > seatsPerRow)
                    {
                        Console.WriteLine($"Voer een getal tussen 1 en {seatsPerRow} in.");
                    }
                    else if (occupied == true)
                    {
                        Console.WriteLine("Deze stoel is helaas al bezet, kies een ander stoelnummer:");
                        choice = -1;
                        occupied = false;
                    }
                    else
                    {
                        fillScreenLayout(seatsPerRow + numberOfPeople, row - 1, choice - 1, numberOfPeople);
                        return choice;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine($"Voer een getal tussen 1 en {seatsPerRow} in.");
                }
            }

            return choice;
        }

        public void saveMovieScreenJson(){

            Movies movieToDelete = Program.movies.Find(x => x.movieID == this.movieID);
            Program.movies.Remove(movieToDelete);
            Program.movies.Insert(this.movieID-1, this);

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            var jsonString = JsonSerializer.Serialize(Program.movies, options);

            File.WriteAllText("movies.json", jsonString);
        }

        public static void fillMovieList()
        {
            string jsonText = File.ReadAllText("movies.json");
            using (JsonDocument document = JsonDocument.Parse(jsonText))
            {
                JsonElement root = document.RootElement;
                JsonElement moviesList = root;
                foreach (JsonElement movie in moviesList.EnumerateArray())
                {
                    if (movie.TryGetProperty("movieID", out JsonElement movieIDElement) &&
                        movie.TryGetProperty("MovieName", out JsonElement MovieNameElement) &&
                        movie.TryGetProperty("startTime", out JsonElement startTimeElement) &&
                        movie.TryGetProperty("runTime", out JsonElement runTimeElement) &&
                        movie.TryGetProperty("genre", out JsonElement genreElement) &&
                        movie.TryGetProperty("director", out JsonElement directorElement) &&
                        movie.TryGetProperty("screenNumber", out JsonElement screenNumberElement) &&
                        movie.TryGetProperty("movieType", out JsonElement movieTypeElement) &&
                        movie.TryGetProperty("Synopsis", out JsonElement SynopsisElement)&&
                        movie.TryGetProperty("ScreenRows", out JsonElement ScreenRowsElement))
                    {
                        int movieID = movieIDElement.GetInt32();
                        string MovieName = MovieNameElement.GetString();
                        DateTime startTime = startTimeElement.GetDateTime();
                        int runTime = runTimeElement.GetInt32();
                        string genre = genreElement.GetString();
                        string director = directorElement.GetString();
                        int screenNumber = screenNumberElement.GetInt32();
                        string movieType = movieTypeElement.GetString();
                        string Synopsis = SynopsisElement.GetString();
                        List<string> screenRows = new List<string>();
                        ArrayEnumerator arrayEnum = ScreenRowsElement.EnumerateArray();

                        while (arrayEnum.MoveNext())
                        {
                            screenRows.Add(arrayEnum.Current.GetString());
                        }


                        Movies fillMovies = new Movies(movieID, MovieName, startTime, screenNumber, runTime, genre, director, movieType, Synopsis);
                        fillMovies.ScreenRows = screenRows;
                        Program.movies.Add(fillMovies);
                    }
                }
            }

        }

        public static void dayOverview()
        {
            Console.WriteLine("Voor welke dag wilt u het dagoverzicht zien?");
            Console.WriteLine("1) Vandaag \n2) Andere ");
            int dayChoice = -1;
            while (dayChoice < 1 || dayChoice > 2)
            {
                try
                {
                    dayChoice = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Maak a.u.b. een keuze uit één van de opties:");
                }

            }
            switch (dayChoice)
            {
                case 1:
                    Console.WriteLine($"Films van vandaag {DateTime.Today.ToString("dd/MM/yyyy")}");
                    int counter1 = 0;
                    foreach(Movies movie in Program.movies)
                    {
                        if (movie.startTime.Date == DateTime.Today.Date)
                        {
                            movieCatalog(movie);
                            counter1++;
                        }
                    }
                    if (counter1 == 0)
                    {
                        Console.WriteLine("Er zijn geen films gevonden voor vandaag.\n");
                    }
                    break;
                case 2:
                    Console.WriteLine("Vul een datum in. (schrijf als dd/mm/yyyy)");
                    DateTime searchDate = new DateTime(01 / 01 / 1960);
                    while (searchDate == new DateTime(01 / 01 / 1960))
                    {
                        try
                        {
                            string searchDatestring = Console.ReadLine();
                            searchDate = DateTime.Parse(searchDatestring);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("De datum moet als dd/mm/yyyy ingevoerd worden, bijv: 01/01/2020");
                        }
                    }
                    int counter2 = 0;
                    foreach (Movies movie in Program.movies)
                    {
                        if (movie.startTime.Date == searchDate)
                        {
                            movieCatalog(movie);
                            counter2++;
                        }
                    }
                    if (counter2 == 0)
                    {
                        Console.WriteLine($"Er zijn geen films gevonden voor de datum {searchDate.Date.ToString("dd/MM/yyyy")}\n");
                    }
                    break;
            }

        }
        //deze method maakt per film die meegegeven wordt een mooi weergegeven detail overzicht
        public static void movieCatalog(Movies movie)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Filmnummer: " + movie.movieID);
            Console.WriteLine(movie.MovieName);
            Console.WriteLine("Zaal " + movie.whichScreen.ScreenNumber);
            Console.WriteLine(movie.startTime.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("Filmlengte: " + movie.runTime + " minuten");
            Console.WriteLine("Genre: " + movie.genre);
            Console.WriteLine("Regisseur: " + movie.director);
            Console.WriteLine("Beschrijving: " + movie.Synopsis);

        }
        //Deze methods stellen nieuwe waardes voor films in. In de management functies worden ze aangeroepen.
        public static int ChangeID()
        {
            int movieID = Program.movies.Count + 1;
            return movieID;
        }
        public static DateTime ChangeStart()
        {
            Console.WriteLine("Voorbeeld = 20 Juli 2020 15:00:00");
            while (true)
            {
                try
                {
                    string startString = Console.ReadLine();
                    CultureInfo dutchCI = new CultureInfo("nl-NL", false);
                    DateTime newStart = DateTime.Parse(startString, dutchCI);
                    Console.WriteLine($"Nieuwe datum is {newStart}");
                    return newStart;
                }
                catch (Exception e)
                {
                    Console.WriteLine("De datum is niet goed ingevuld. Probeer het opnieuw.");
                    Console.WriteLine("Voorbeeld = 20 Juli 2020 15:00:00");
                }
            }
        }

        public static int ChangeScreen()
        {
            int choice = -1;
            while (choice < 1 || choice > 5)
            {
                try
                {
                    int storage = int.Parse(Console.ReadLine());
                    if (storage < 1 || storage > 5)
                    {
                        Console.WriteLine("Het ingevoerde getal moet tussen 1 en 5 zijn.");
                    }
                    else
                    {
                        choice = storage;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Vul een getal tussen 1 en 5 in.");
                }
            }
            return choice - 1;
        }
    }
}
