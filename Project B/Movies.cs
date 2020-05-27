using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
            Console.WriteLine($"\n\nStatus van zaal {movie.whichScreen.ScreenNumber} tijdens {movie.MovieName} om {movie.startTime.ToString("dd/MM/yyyy HH:mm")}:\n");
            string[] RowBlueprint = new string[movie.whichScreen.AmountOfSeatsPerRow];
            for (int i = 0; i < RowBlueprint.Length; i++)
            {
                string RowI = "";
                for (int j = 0; j < RowBlueprint.Length; j++)
                {
                    //hier komt de check of de zitplaats niet in de json staat.
                    RowI += $"{j + 1} ";
                }
                RowBlueprint[i] = RowI;
            }
            for (int rowCounter = 1; rowCounter < 10; rowCounter++)
            {
                Console.WriteLine($"Rij {rowCounter} :   {RowBlueprint[rowCounter - 1]}");
            }
            for (int rowCounter = 10; rowCounter < movie.whichScreen.AmountOfRows; rowCounter++)
            {
                Console.WriteLine($"Rij {rowCounter} :  {RowBlueprint[rowCounter - 1]}");
            }
            string vipRow = "";
            for (int k = 0; k < RowBlueprint.Length; k++)
            {
                //hier komt de check of de zitplaats niet in de json staat.
                vipRow += $"{k + 1} ";
            }
            Console.WriteLine($"VIP Rij : {vipRow}");
        }

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

        void fillScreenLayout(int seatsPerRow, int selectedRow, int selectedSeat)
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

                for(int j = 0; j < rowChars.Length; j++)
                {
                    if (rowChars[j] == "O" && i == selectedRow && j == selectedSeat)
                    {
                        newRow = newRow + "X";
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

        

        public int SelectSeat(int row, int numberOfPeople)
        {
            Console.WriteLine("Voer het nummer in van de stoel waar u wilt zitten\n" +
                "Als u voor meerdere personen reserveert, selecteert u de linker stoel.\n" +
                "De stoelen rechts van uw keuze worden automatisch geselecteerd:");
            int seatsPerRow = whichScreen.AmountOfSeats / whichScreen.AmountOfRows;
            
            
            int choice = -1;
            while (choice < 1 || choice > seatsPerRow)
            {
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    if (choice < 1 || choice > seatsPerRow)
                    {
                        Console.WriteLine($"Voer een getal tussen 1 en {seatsPerRow} in.");
                    }
                    else
                    {
                        fillScreenLayout(seatsPerRow, row - 1, choice - 1);
                        return choice;
                    }
                }
                catch (Exception e)
                {
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
    }
}
