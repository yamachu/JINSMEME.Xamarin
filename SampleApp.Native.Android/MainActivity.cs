using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using JINSMEME.Native.Android;

namespace SampleApp.Native.Android
{
    [Activity(Label = "SampleApp.Android", MainLauncher = true)]
    public class MainActivity : ActionBarActivity, IMemeConnectListener, IMemeScanListener
    {
        private string appClientId = "MUST REPLACE";
        private string appClientSecret = "MUST REPLACE";

        private MemeLib memeLib;
        private List<string> scannedAddresses;
        private ArrayAdapter<string> scannedAddressAdapter;

        private ListView deviceListView;

        public void MemeConnectCallback(bool p0)
        {
            Intent intent = new Intent(this, typeof(MemeDataActivity));
            StartActivity(intent);
        }

        public void MemeDisconnectCallback()
        {
            System.Console.WriteLine("Disconnect");
        }

        private bool scanning = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SupportRequestWindowFeature((int)WindowFeatures.IndeterminateProgress);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            if (savedInstanceState == null)
            {
                init();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main, menu);
            return true;
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            if (scanning)
            {
                menu.FindItem(Resource.Id.action_scan).SetVisible(false);
                menu.FindItem(Resource.Id.action_stop).SetVisible(true);
            }
            else
            {
                menu.FindItem(Resource.Id.action_scan).SetVisible(true);
                menu.FindItem(Resource.Id.action_stop).SetVisible(false);
            }
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var itemId = item.ItemId;
            if (itemId == Resource.Id.action_scan)
            {
                startScan();
                scanning = true;
                return true;
            }
            else if (itemId == Resource.Id.action_stop)
            {
                stopScan();
                clearList();
                scanning = false;
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void init()
        {
            MemeLib.SetAppClientID(ApplicationContext, this.appClientId, this.appClientSecret);
            memeLib = MemeLib.Instance;
            memeLib.SetVerbose(true);

            deviceListView = FindViewById<ListView>(Resource.Id.deviceListView);

            deviceListView.ItemClick += (sender, e) =>
            {
                stopScan();

                var address = scannedAddressAdapter.GetItem(e.Position);
                memeLib.Connect(address);

                clearList();
                this.scanning = false;
            };
        }

        protected override void OnResume()
        {
            base.OnResume();

            memeLib.SetMemeConnectListener(this);
            memeLib.AutoConnect = true;
        }

        private void startScan()
        {
            memeLib.SetMemeConnectListener(this);

            scannedAddresses = new List<string>();
            scannedAddressAdapter = new ArrayAdapter<string>(this, global::Android.Resource.Layout.SimpleListItem1, scannedAddresses);
            deviceListView.Adapter = scannedAddressAdapter;

            var status = memeLib.StartScan(this);

            if (status == MemeStatus.MemeErrorAppAuth)
            {
                Toast.MakeText(this.BaseContext, "App Auth Failed", ToastLength.Long).Show();
            }
            else if (status == MemeStatus.MemeErrorSdkAuth)
            {
                Toast.MakeText(this.BaseContext, "SDK Auth Failed", ToastLength.Long).Show();
            }
            else if (status == MemeStatus.MemeErrorFwCheck)
            {
                Toast.MakeText(this.BaseContext, "FW Update is Required", ToastLength.Long).Show();
            }
            else
            {
                SetSupportProgressBarIndeterminateVisibility(false);
                InvalidateOptionsMenu();
            }
        }

        private void stopScan()
        {
            SetSupportProgressBarIndeterminateVisibility(false);
            InvalidateOptionsMenu();

            if (memeLib.IsScanning)
            {
                memeLib.StopScan();
            }
        }

        private void clearList()
        {
            scannedAddressAdapter.Clear();
            deviceListView.DeferNotifyDataSetChanged();
        }

        public void MemeFoundCallback(string p0)
        {
            scannedAddressAdapter.Add(p0);
            this.RunOnUiThread(() =>
            {
                scannedAddressAdapter.NotifyDataSetChanged();
            });
        }
    }
}

