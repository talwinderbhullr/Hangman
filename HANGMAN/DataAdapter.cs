using System;
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
    public class DataAdapter : BaseAdapter<Score> // The class to call to populate score data to show into the ListView
    {
        List<Score> items;
        Activity context;

        public DataAdapter(Activity context, List<Score> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Score this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomRowHS, null);
                view.FindViewById<TextView>(Resource.Id.txtviewPlayer).Text = item.player;
                view.FindViewById<TextView>(Resource.Id.txtviewScore).Text = item.score.ToString();
            return view;
        }
    }
}