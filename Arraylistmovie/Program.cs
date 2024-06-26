using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.OleDb;

namespace Arraylistmovie
{
    class Program
    {
        static void Main(string[] args)
        {
            //Personal Challenge
            //Add the movies to any database of your choice
            //you should be able to retrieve or delete data from the database
            ArrayList movieDatabase = new ArrayList();

            while (true)
            {
                Menu(movieDatabase);
            }
        }

        public static void Menu(ArrayList movie)
        {
            int choice;

            Console.WriteLine("Select an Option!");
            Console.WriteLine("1. Add a new Movie\n2. Display all movies\n3. Search for a movie by title\n4. Remove a movie\n5. Exit");
            choice = Convert.ToInt32(Console.ReadLine());
            try
            {
                switch (choice)
                {
                    case 1:

                        Console.WriteLine("How many movies do you want to add? ");
                        int numMovies = Convert.ToInt32(Console.ReadLine());

                        for (int i = 0; i < numMovies; i++)
                        {

                            Console.WriteLine("Add a Movie");
                            Console.WriteLine("Movie Title");
                            string title = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Enter Director: ");
                            string director = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Release Year: ");
                            string releaseyear = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Enter Genre: ");
                            string genre = Convert.ToString(Console.ReadLine());



                            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Elvis\\Documents\\Database files for projects\\MovieList.accdb";

                            using (OleDbConnection con = new OleDbConnection(connectionString))
                            {
                                con.Open();

                                string query = "INSERT INTO Movies (Title, Director, RYear, Genre) VALUES (?, ?, ?, ?)";

                                using (OleDbCommand cmd = new OleDbCommand(query, con))
                                {
                                    cmd.Parameters.AddWithValue("@Title", title);
                                    cmd.Parameters.AddWithValue("@Director", director);
                                    cmd.Parameters.AddWithValue("@RYear", releaseyear);
                                    cmd.Parameters.AddWithValue("@Genre", genre);

                                    cmd.ExecuteNonQuery();

                                    Movie addMovie = new Movie(title, director, releaseyear, genre);
                                    movie.Add(addMovie);
                                }
                                Console.WriteLine("Movies successfully submitted" + " Congrats!!");
                            }
                        }

                        Console.WriteLine("\n");

                        break;

                    case 2:

                        foreach (var m in movie)
                        {
                            Console.WriteLine(m + " ");
                        }

                        Console.WriteLine("\n");

                        DisplayRecordsFromDatabase();

                        break;

                    case 3:

                        Search();

                        break;

                    case 4:
                        RemoveMovie();

                        break;

                    case 5:

                        Environment.Exit(0);

                        break;

                    default:

                        Console.WriteLine("Wrong Option");

                        break;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: " + error.Message);
            }
        }
        public static void DisplayRecordsFromDatabase()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Elvis\\Documents\\Database files for projects\\MovieList.accdb";
            string query = "SELECT * FROM Movies";

            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, con))
                {
                    con.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string title = reader["Title"].ToString();
                            string director = reader["Director"].ToString();
                            string releaseYear = reader["RYear"].ToString();
                            string genre = reader["Genre"].ToString();
                            Console.WriteLine($"Title: {title}, Director: {director}, Release Year: {releaseYear}, Genre: {genre}");
                        }
                    }
                }
            }
        }

        public static void Search()
        {
            Console.WriteLine("Enter movie title to search");
            string searchTitle = Console.ReadLine();

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Elvis\\Documents\\Database files for projects\\MovieList.accdb";
            string query = "SELECT * FROM Movies WHERE Title = ?";

            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, con))
                {
                    //checks if the title matches searchtitle
                    cmd.Parameters.AddWithValue("@Title", searchTitle);
                    con.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string title = reader["Title"].ToString();
                            string director = reader["Director"].ToString();
                            string releaseYear = reader["RYear"].ToString();
                            string genre = reader["Genre"].ToString();
                            Console.WriteLine($"Title: {title}, Director: {director}, Release Year: {releaseYear}, Genre: {genre}");
                        }
                    }
                }
            }
        }

        public static void RemoveMovie()
        {
            Console.WriteLine("Enter movie to delete");
            string deleteMovie = Console.ReadLine();

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Elvis\\Documents\\Database files for projects\\MovieList.accdb";
            string query = "DELETE FROM Movies WHERE Title = ?";

            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                using(OleDbCommand cmd = new OleDbCommand(query,con))
                {
                    cmd.Parameters.AddWithValue("@Title", deleteMovie);
                    con.Open();

                    int rowsAffeced = cmd.ExecuteNonQuery();
                    if(rowsAffeced > 0)
                    {
                        Console.WriteLine("Movie Successfully deleted");
                    }
                    else
                    {
                        Console.WriteLine("Movie does not Exist");
                    }
                }
            }
        }
    }
}
