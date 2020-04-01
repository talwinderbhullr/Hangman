using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HANGMAN
{
    public class Score // Class to call in DataBase Manager to run and show scores saved file from database
    {
        public int scoreid { get; set; }
        public string player { get; set; }
        public int score { get; set; }
    }

   
}