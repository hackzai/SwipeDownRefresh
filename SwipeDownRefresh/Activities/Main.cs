using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Widget;

using SwipeDownRefresh.Fragments;

namespace SwipeDownRefresh.Activities
{
    [Activity (Label = "Swipe Down Refresh", MainLauncher = true, LaunchMode = LaunchMode.SingleInstance, ScreenOrientation = ScreenOrientation.Portrait, Icon = "@drawable/icon")]
    public class Main : Android.Support.V4.App.FragmentActivity
    {
        private SwipeRefreshLayout layoutRefresher;

        private MyListFragment fragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            SetContentView (Resource.Layout.Main);

            // load the fragment content
            this.fragment = new MyListFragment();
            var ft = SupportFragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.frameContainer, this.fragment);
            ft.Commit();

            this.layoutRefresher = FindViewById<SwipeRefreshLayout> (Resource.Id.layoutRefresher);
            this.layoutRefresher.Refresh += LayoutRefreshHandler;

            // change the refresh animation icon colors
            this.layoutRefresher.SetColorScheme(
                        Resource.Color.Red,
                        Resource.Color.Orange,
                        Resource.Color.Purple,
                        Resource.Color.Magenta);
        }

        async void LayoutRefreshHandler (object sender, System.EventArgs e)
        {
            // invoke the fragment function
            await this.fragment.RefreshContents();

            // cancel the refresh animation UI
            this.layoutRefresher.Refreshing = false;
        }
    }
}


