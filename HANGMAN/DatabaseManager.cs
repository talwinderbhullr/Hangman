using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace HANGMAN
{
    public class DatabaseManager
    {
        static string dbName = "HangmanDB.sqlite";
        string dbPath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, dbName);

        public DatabaseManager()
        {
            Console.WriteLine(dbPath);
        }
        public List<WordBank> ViewWords() // Creates a command to by calling the function to populate data of words from the saved file from the database
        {
            try
            {
                using (var conn = new SQLiteConnection(dbPath))
                {
                    var cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "Select * from WORDBANK";
                    var GetWord = cmd.ExecuteQuery<WordBank>();
                    return GetWord;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                return null;
            }
        }

        public List<Score> GetScore() // Creates a command to by calling the function to populate data of scores from the saved file from the database
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    cmd.CommandText = "Select * from scoretbl";
                    var ScoreList = cmd.ExecuteQuery<Score>();
                    return ScoreList;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                return null;
            }
        }

        public void AddScore(string player, int xscore) // Creates a command to by calling the function to add score data to the saved file from the database
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    string s = "Insert into scoretbl(player,score) values ('" + player + "'," + xscore + ")";
                    Console.WriteLine("the string sent to db is :  " + s);
                    cmd.CommandText = s;
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);

            }
        }

    }
}
