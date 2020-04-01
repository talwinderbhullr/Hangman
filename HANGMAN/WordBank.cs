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
    public class WordBank // Class to call to populate list of words from the saved file 
    {
        public int WordID { get; set; }
        public string Word { get;  set; }               
    }
}