
using System;

using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using JINS.MEME.Android;

namespace SampleApp.Android
{
    [Activity(Label = "MemeDataActivity")]
    public class MemeDataActivity : ActionBarActivity, IMemeRealtimeListener, IMemeResponseListener
    {
        private MemeLib memeLib;
        private ListView dataItemListView;
        private MemeDataItemAdapter dataItemAdapter;

        public void MemeRealtimeCallback(MemeRealtimeData p0)
        {
            this.RunOnUiThread(() =>
            {
                SetSupportProgressBarIndeterminateVisibility(true);
                dataItemAdapter.updateMemeData(p0);
                dataItemAdapter.NotifyDataSetChanged();
                SetSupportProgressBarIndeterminateVisibility(false);
            });
        }

        public void MemeResponseCallback(MemeResponse p0)
        {
            switch (p0.EventCode)
            {
                case 0x02:
                    Console.WriteLine($"MemeResponse: start data report : result {(p0.CommandResult ? 1 : 0)}");
                    break;
                case 0x04:
                    Console.WriteLine($"MemeResponse: stop data report : result {(p0.CommandResult ? 1 : 0)}");
                    break;
                default:
                    break;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SupportRequestWindowFeature((int)WindowFeatures.IndeterminateProgress);
            SetContentView(Resource.Layout.MemeDataActivity);
            init();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MemeData, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var id = item.ItemId;
            if (id == Resource.Id.action_disconnect)
            {
                memeLib.Disconnect();

                memeLib.AutoConnect = false;
                this.Finish();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void init()
        {
            memeLib = MemeLib.Instance;

            dataItemListView = (ListView)FindViewById(Resource.Id.data_item_list_view);
            dataItemAdapter = new MemeDataItemAdapter(this);
            dataItemListView.Adapter = dataItemAdapter;
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);

            memeLib.SetResponseListener(this);
            memeLib.StartDataReport(this);
        }
    }
}
