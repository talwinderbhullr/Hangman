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
    public class AddScore // Class to call in Database Manager to add score / data to the database
    {
        public int ScoreID { get; set; }
        public string AddPlayer { get; set; }
        public int AddPlayerScore { get; set; }
    }
}