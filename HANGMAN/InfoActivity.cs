using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;


namespace HANGMAN
{
    [Activity(Label = "InfoActivity", Icon = "@drawable/hmsetup3", ScreenOrientation = ScreenOrientation.Portrait)] 
    public class InfoActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "info" layout resource
            SetContentView(Resource.Layout.Info);

            // Initilize the Textview
            TextView tvInfoMenu = FindViewById<TextView>(Resource.Id.tvInfoMenu);

            tvInfoMenu.Click += delegate { base.OnBackPressed(); };
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