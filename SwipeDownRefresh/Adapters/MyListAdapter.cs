using Android.Content;
using Android.Views;
using Android.Widget;

using SwipeDownRefresh.Data;

namespace SwipeDownRefresh.Adapters
{
    public class MyListAdapter : BaseAdapter
    {
        private readonly Context context;

        private SampleDataItemCollection data;

        public MyListAdapter(Context context, SampleDataItemCollection data)
        {
            this.context = context;
            this.data = data;
        }

        public override int Count
        {
            get 
            {  
                return this.data == null ? 0 : this.data.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if(convertView == null)
            {
                convertView = LayoutInflater.From(this.context).Inflate(Resource.Layout.MyListItem, parent, false);
            }

            var lblDescription = (TextView)convertView.FindViewById(Resource.Id.lblDescription);

            if (this.data != null && position < this.data.Count)
            {
                var item = this.data[position];
                lblDescription.SetText(item.Description, TextView.BufferType.Normal);
            }

            return convertView;
        }
    }
}

