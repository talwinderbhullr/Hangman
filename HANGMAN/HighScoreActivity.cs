using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;

namespace HANGMAN
{
    [Activity(Label = "HighScoreActivity", Icon = "@drawable/hmsetup3", ScreenOrientation = ScreenOrientation.Portrait)]
    public class HighScoreActivity : Activity
    {
        // Global Variables
        static string dbName = "HangmanDB.sqlite";
        string dbPath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, dbName);
        DatabaseManager dbmHScore;
        List<Score> ListOfScores;
        ListView lvHScore;
        TextView tvHSMenu;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "highscore" layout resource
            SetContentView(Resource.Layout.HighScore);

            // Initialize the components
            lvHScore = FindViewById<ListView>(Resource.Id.lvHScore);
            tvHSMenu = FindViewById<TextView>(Resource.Id.tvHSMenu);

            // Initialize parameters
            dbmHScore = new DatabaseManager();
            ListOfScores = new List<Score>();
            ListOfScores = dbmHScore.GetScore();
            lvHScore.Adapter = new DataAdapter(this, ListOfScores);

            foreach (var item in ListOfScores) // Console view to show line readings from database
            {
                Console.WriteLine(item.score);
            }

            tvHSMenu.Click += delegate { base.OnBackPressed(); };
        }

        public override void OnBackPressed()
        {
            //This prevents the user from being able to exit with the back button.
            bool ceasebackkey = true;
            if (ceasebackkey)
            {
                Toast.MakeText(this, "NOPE, PRESS MENU. THANKS!", ToastLength.Long).Show();
                return;
            }

            base.OnBackPressed();
        }
    }
}
