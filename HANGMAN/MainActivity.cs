using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using System.IO;


namespace HANGMAN
{
    [Activity(Label = "HANGMAN", MainLauncher = true, Icon = "@drawable/hmsetup3", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        // Global Variables        
        static string dbName = "HangmanDB.sqlite";
        string dbPath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, dbName);


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Initialize the components
            TextView tvOff = FindViewById<TextView>(Resource.Id.tvOff);
            TextView tvPlay = FindViewById<TextView>(Resource.Id.tvPlay);
            TextView tvHScore = FindViewById<TextView>(Resource.Id.tvHScore);
            TextView tvInfo = FindViewById<TextView>(Resource.Id.tvInfo);
            CopyDatabase();
            //Placing the base.OnBackPressed(); below make the program exits when textview is pressed            
            tvOff.Click += delegate { base.OnBackPressed(); };

            tvPlay.Click += delegate
            {
                var intent = new Intent(this, typeof(GameActivity));
                StartActivity(intent);
                Finish();
            };

            tvInfo.Click += delegate
            {
                var intent = new Intent(this, typeof(InfoActivity));
                StartActivity(intent);
            };

            tvHScore.Click += delegate
            {
                var intent = new Intent(this, typeof(HighScoreActivity));
                StartActivity(intent);
            };
        }

        public void CopyDatabase() // Copies the data from the database into the saved file
        {
            if (!File.Exists(dbPath)) // Toggle the ! to force install 
            {
                using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }
        }

        public override void OnBackPressed()
        {
            //This prevents the user from being able to exit with the back button.
            bool ceasebackkey = true;
            if (ceasebackkey)
            {
                Toast.MakeText(this, "PRESS 'X' TO EXIT. THANKS!", ToastLength.Long).Show();
                return;
            }

            base.OnBackPressed();
        }
    }
}
