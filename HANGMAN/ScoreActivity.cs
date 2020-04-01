using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using System;

namespace HANGMAN
{
    [Activity(Label = "ScoreActivity", Icon = "@drawable/hmsetup3", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ScoreActivity : Activity
    {       
        // Global Variables
        int score;
        string word;
        DatabaseManager dbm;
        TextView tvPName;        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "score" layout resource
            SetContentView(Resource.Layout.Score);
            
            // Initialize components
            TextView tvResult = FindViewById<TextView>(Resource.Id.tvResult);
            TextView tvCorrectWord = FindViewById<TextView>(Resource.Id.tvCorrectWord);
            TextView tvMenu = FindViewById<TextView>(Resource.Id.tvMenu);
            TextView tvPlayAgain = FindViewById<TextView>(Resource.Id.tvPlayAgain);
            ImageView ivResult = FindViewById<ImageView>(Resource.Id.ivResult);
            tvPName = FindViewById<TextView>(Resource.Id.tvPName);
            TextView tvSubmit = FindViewById<TextView>(Resource.Id.tvSubmit);

            tvMenu.Click += tvMenu_Click;
            tvPlayAgain.Click += tvPlayAgain_Click;           

            // Below gets the intents passed on from Game Activity for use to show on component fields
            score = Intent.GetIntExtra("score", 0);
            word = Intent.GetStringExtra("word");
            var result = Intent.GetBooleanExtra("result", true);
            if (result == true) // Sets the background to freeman when player wins and win is true
            {
                ivResult.SetBackgroundResource(Resource.Drawable.freeman);
            }

            else // Sets the background to hangman12/endgame when player losts and win is false
            {
                ivResult.SetBackgroundResource(Resource.Drawable.hangman12);
            }
                     
            
            tvResult.Text = score.ToString(); // Shows the player score through a textview
            tvCorrectWord.Text = word; // Shows the word to guess through a textview   

            tvSubmit.Click += delegate
            {
                if (tvPName.Text == "")
                {
                    tvPName.Text = "Guest";
                }
                dbm = new DatabaseManager();
                dbm.AddScore(tvPName.Text, score);

                tvSubmit.Text = "SUBMITTED..BUTTON DISABLED";
                tvSubmit.Enabled = false; // Disables the TextView so no repeat of resubmission of data
            };            
        }               

        void tvPlayAgain_Click(object sender, EventArgs e) // Event handler to play again
        {
            var intent = new Intent (this, typeof(GameActivity));
            StartActivity(intent);
            Finish();
        }

        void tvMenu_Click(object sender, EventArgs e) // Event handler to switch to main setup page
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }

        public override void OnBackPressed()
        {
            //This prevents the user from being able to exit with the back button while game is played
            bool ceasebackkey = true;
            if (ceasebackkey)
            {
                Toast.MakeText(this, "NOPE, PRESS OTHER OPTIONS. THANKS!", ToastLength.Long).Show();
                return;
            }

            base.OnBackPressed();
        }

    }
}