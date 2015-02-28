using System;
using System.Threading.Tasks;

using Android.App;
using Android.Views;
using Android.Widget;

using SwipeDownRefresh.Data;
using SwipeDownRefresh.Adapters;

namespace SwipeDownRefresh.Fragments
{
    [Activity(Label = "My List")]
    public class MyListFragment : Android.Support.V4.App.Fragment
    {
        private SampleDataItemCollection source;

        private ListView lvwItems;

        private MyListAdapter adapter;

        private int requestCounter;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            this.HasOptionsMenu = false;

            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.MyList, null);

            this.source = new SampleDataItemCollection();
            this.requestCounter++;
            this.CreateDataSource();

            this.adapter = new MyListAdapter(Application.Context, this.source);

            this.lvwItems = view.FindViewById<ListView>(Resource.Id.lvwItems);
            this.lvwItems.Adapter = this.adapter;
            this.lvwItems.ItemClick += this.ListViewItemClickHandler;
            this.lvwItems.ItemLongClick += this.ItemLongClickHandler;

            return view;
        }

        private void ListViewItemClickHandler (object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this.Activity, this.source[e.Position].Description, ToastLength.Long).Show();
        }

        private void ItemLongClickHandler (object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Toast.MakeText(this.Activity, this.source[e.Position].Description, ToastLength.Long).Show();
        }

        public async Task RefreshContents()
        {
            await Task.Run(() => {
                var start = DateTime.Now;
                while (DateTime.Now.Subtract(start).TotalSeconds < 5)
                {
                }
            });

            this.requestCounter++;
            this.CreateDataSource();
            this.Refresh();
        }

        private void CreateDataSource()
        {
            this.source.Clear();

            var endIndex = this.requestCounter * 30;
            var startIndex = endIndex - 30;

            for (var index = startIndex; index <= endIndex; index++)
            {
                this.source.Add(new SampleDataItem(string.Format("Description Item #{0}", index)));
            }
        }

        private void Refresh()
        {
            if (this.adapter == null || this.source == null)
            {
                return;
            }

            this.adapter.NotifyDataSetChanged();
        }
    }
}

