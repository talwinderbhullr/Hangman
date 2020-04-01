using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;


namespace HANGMAN
{
    [Activity(Label = "GameActivity", Icon = "@drawable/hmsetup3", ScreenOrientation = ScreenOrientation.Portrait)] 
    public class GameActivity : Activity
    {
        //  Global Variables         
        TextView tvScore;
        TextView tvTries;
        TextView tvLength;
        TextView tvUsed;
        TextView tvWordToGuess;
        TextView tvPatentG;
        ImageView ivStatus;
        
        List<Button> listofbuttons; // Creates a list of buttons
       
        // Initialize the buttons
        Button btnA, btnB, btnC, btnD, btnE, btnF, btnG, btnH, btnI, btnJ, btnK, btnL, btnM, btnN, btnO, btnP, btnQ, btnR, btnS, btnT, btnU, btnV, btnW, btnX, btnY, btnZ;

        static string dbName = "HangmanDB.sqlite"; // String file to read from SQLite
        string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
        DatabaseManager dbmGame;

        // Instantiate the parameters
        string WordToGuess; 
        List<WordBank> ListOfWords = new List<WordBank>();
        char[] charArray = new char[10];
        char[] blindArray = new char[10];
        List<int> listofcorrectguessindexes;
             
        int tries = 6; // Initialize the value of tries for the game    
        int score = 0; // Initialize the value of score for the game      

        bool win = true; // Initialize a boolean value of win to be true

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "game" layout resource
            SetContentView(Resource.Layout.Game);

            // Initialize the Components
             tvScore = FindViewById<TextView>(Resource.Id.tvScore);
             tvTries = FindViewById<TextView>(Resource.Id.tvTries);
             tvLength = FindViewById<TextView>(Resource.Id.tvLength);
             tvUsed = FindViewById<TextView>(Resource.Id.tvUsed);
             tvWordToGuess = FindViewById<TextView>(Resource.Id.tvWordToGuess);
             tvPatentG = FindViewById<TextView>(Resource.Id.tvPatentG);
             ivStatus = FindViewById<ImageView>(Resource.Id.ivStatus);
                      
            // Instantiate lists            
            listofcorrectguessindexes = new List<int>();

            // Initialize the Buttons through a function
             setupbuttons();
                         
             dbmGame = new DatabaseManager(); // Creating an instance of DatabaseManager

             GetRandomWord(); // Get a random word from Database  

             listofbuttons = new List<Button> { btnA, btnB, btnC, btnD, btnE, btnF, btnG, btnH, btnI, btnJ, btnK, btnL, btnM, btnN, btnO, btnP, btnQ, btnR, btnS, btnT, btnU, btnV, btnW, btnX, btnY, btnZ };

             for (var i = 0; i < listofbuttons.Count; i++) // Creates a loop in every letter pressed while game is on play
             {
                 List<string> idlist = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                 var button = listofbuttons[i];
                 button.Tag = idlist[i];
                 button.Click += b_Click;
             }                            
                                   
        }

        public void setupbuttons() // Function to call to initialize buttons
        {            
            btnA = FindViewById<Button>(Resource.Id.btnA);
            btnB = FindViewById<Button>(Resource.Id.btnB);
            btnC = FindViewById<Button>(Resource.Id.btnC);
            btnD = FindViewById<Button>(Resource.Id.btnD);
            btnE = FindViewById<Button>(Resource.Id.btnE);
            btnF = FindViewById<Button>(Resource.Id.btnF);
            btnG = FindViewById<Button>(Resource.Id.btnG);
            btnH = FindViewById<Button>(Resource.Id.btnH);
            btnI = FindViewById<Button>(Resource.Id.btnI);
            btnJ = FindViewById<Button>(Resource.Id.btnJ);
            btnK = FindViewById<Button>(Resource.Id.btnK);
            btnL = FindViewById<Button>(Resource.Id.btnL);
            btnM = FindViewById<Button>(Resource.Id.btnM);
            btnN = FindViewById<Button>(Resource.Id.btnN);
            btnO = FindViewById<Button>(Resource.Id.btnO);
            btnP = FindViewById<Button>(Resource.Id.btnP);
            btnQ = FindViewById<Button>(Resource.Id.btnQ);
            btnR = FindViewById<Button>(Resource.Id.btnR);
            btnS = FindViewById<Button>(Resource.Id.btnS);
            btnT = FindViewById<Button>(Resource.Id.btnT);
            btnU = FindViewById<Button>(Resource.Id.btnU);
            btnV = FindViewById<Button>(Resource.Id.btnV);
            btnW = FindViewById<Button>(Resource.Id.btnW);
            btnX = FindViewById<Button>(Resource.Id.btnX);
            btnY = FindViewById<Button>(Resource.Id.btnY);
            btnZ = FindViewById<Button>(Resource.Id.btnZ);
        }  
               

        void b_Click(object sender, EventArgs e)
        {            
            listofcorrectguessindexes.Clear(); // Clears the listofcorrectguessindexes everytime it loops to insert correct character
            var pressedbutton = sender as Button;
            var letter = Convert.ToString(pressedbutton.Tag);
            letter = letter.ToUpper();
                                  
            pressedbutton.Enabled = false; // Dsiables the character button when pressed        

            string a = "";
            foreach (var item in letter) // 
            {
                a = a + item.ToString();                 
            }
            var b = a;
            tvUsed.Text += b;        
                                                                  
            int index = 0;
            foreach (var item in charArray) // Creates a loop to compare the button pressed per character in charArray
            {
                if (item == Convert.ToChar(letter)) 
                    listofcorrectguessindexes.Add(index);
                    index += 1;
            }

            if (listofcorrectguessindexes.Count == 0) // Calls the function badguess when guessed letter is not in the charArray
            {
                badguess();
            }
            else 
            {               
                foreach (int i in listofcorrectguessindexes) // Creates a loop to check and convert to character every correct guessed letter, then calls the function increasescore
                {               
                    blindArray[i] = Convert.ToChar(letter);
                    increasescore();
                }                            
            }

            String holder = ""; // Creates a string holder then concatenate every correct character into a new string to show to player
            foreach (var item in blindArray)
            {
                holder = holder + item.ToString(); 
            }
            tvWordToGuess.Text = holder;

            //}

            //else 
            //{
            //    Toast.MakeText(this, "Letter has been choosen. Choose again.", ToastLength.Long).Show();
            //    return;
            //}
        }

        public void badguess() // Runs the sub based on players remaining tries in decreasing order. Background image changes as tries decreases
        {
            tries -= 1;
            tvTries.Text = tries.ToString();
            if (tries == 6)
            {
                ivStatus.SetBackgroundResource(Resource.Drawable.hangman6);
            }
            if (tries == 5)
            {
                ivStatus.SetBackgroundResource(Resource.Drawable.hangman7);
            }
            if (tries == 4)
            {
                ivStatus.SetBackgroundResource(Resource.Drawable.hangman8);
            }
            if (tries == 3)
            {
                ivStatus.SetBackgroundResource(Resource.Drawable.hangman9);
            }
            if (tries == 2)
            {
                ivStatus.SetBackgroundResource(Resource.Drawable.hangman10);
            }
            if (tries == 1)
            {
                ivStatus.SetBackgroundResource(Resource.Drawable.hangman11);
            } if (tries == 0) // When tries is zero, boolean value turns to false and creates an intent to pass on and switch into Score Activity
            {
                win = false;
                var intent = new Intent(this, typeof(ScoreActivity));
                intent.PutExtra("score", score); // Intent score gained by player to pass on to
                intent.PutExtra("word", WordToGuess); // Intent to pass on to show the random word (Word to Guess) 
                intent.PutExtra("result", win); // Intent result to pass when win is false and player lost

                StartActivity(intent);
                Finish();
            }
        }

        private void increasescore() // Runs the sub to increase the score in every correct character guessed by player
        {
            
            score += 1;
            tvScore.Text = score.ToString();
            if (WordToGuess.Length == score) // When the character length of the word to guess is the same as that of score, creates an intent to pass and switch into Score Activity
            {
                win = true;
                var intent = new Intent(this, typeof(ScoreActivity));
                intent.PutExtra("score", score); // Intent score gained by player to pass on to
                intent.PutExtra("word", WordToGuess); // Intent to pass on to show the random word (Word to Guess) 
                intent.PutExtra("result", win); // Intent result to pass when win is true and player wins!!!
                
                StartActivity(intent);
                Finish();               
            }
        }       

        public void GetRandomWord() // Function to call to get a random word from the database
        {
            ListOfWords = dbmGame.ViewWords();
            Random r1 = new Random(DateTime.Now.Millisecond);            
            int x = r1.Next(0, ListOfWords.Count);
           
            WordToGuess = ListOfWords[x].Word; // The random word taken from ListOfWords
            WordToGuess = WordToGuess.ToUpper(); // Changes the word into uppercase      

           Toast.MakeText(this, WordToGuess, ToastLength.Long).Show(); // sample checking                       
            
            tvTries.Text = tries.ToString(); // Display remaining tries left to play

            tvLength.Text = WordToGuess.Length.ToString(); //  Display the number of characters in the word to guess
            
            charArray = WordToGuess.ToCharArray(); //Splits the word to guess into an array
                      
           
            for (int i = 0; i < WordToGuess.Length; i++) // Loops into the charArray and creates a new char array to show blank letters to player based on number of lengths
            {
                blindArray[i] = '*';
            }
                        
            Console.WriteLine(blindArray); // Console view to check blindArray appearance and existence
            
        }

        public override void OnBackPressed()
        {
            //This prevents the user from being able to exit with the back button while game is played
            bool ceasebackkey = true;
            if (ceasebackkey)
            {
                Toast.MakeText(this, "NOPE, FINISH THE GAME. THANKS!", ToastLength.Long).Show();
                return;
            }

            base.OnBackPressed();
        }
        
    }
}