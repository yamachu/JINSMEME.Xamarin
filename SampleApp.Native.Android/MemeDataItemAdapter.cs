using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using JINSMEME.Native.Android;

namespace SampleApp.Native.Android
{
    public class MemeDataItemAdapter : BaseAdapter
    {
        private List<string[]> items;
        private Context context;
        private LayoutInflater inflater;

        public MemeDataItemAdapter(Context context)
        {
            this.context = context;
            this.inflater = LayoutInflater.From(context);
            items = new List<string[]>();
        }

        public override int Count => items.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return items[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = this.inflater.Inflate(Resource.Layout.MemeDataItem, null);
            }

            var labelTextView = (TextView)convertView.FindViewById(Resource.Id.item_label);
            var valueTextView = (TextView)convertView.FindViewById(Resource.Id.item_value);

            var item = items[position];
            labelTextView.Text = item[0];
            valueTextView.Text = item[1];

            return convertView;
        }

        public void updateMemeData(MemeRealtimeData d)
        {
            items = new List<string[]>();
            addItem(Resource.String.fit_status, d.FitError);
            addItem(Resource.String.walking, d.IsWalking);
            addItem(Resource.String.noise_status, d.NoiseStatus);
            addItem(Resource.String.power_left, d.PowerLeft);
            addItem(Resource.String.eye_move_up, d.EyeMoveUp);
            addItem(Resource.String.eye_move_down, d.EyeMoveDown);
            addItem(Resource.String.eye_move_left, d.EyeMoveLeft);
            addItem(Resource.String.eye_move_right, d.EyeMoveRight);
            addItem(Resource.String.blink_streangth, d.BlinkStrength);
            addItem(Resource.String.blink_speed, d.BlinkSpeed);
            addItem(Resource.String.roll, d.Roll);
            addItem(Resource.String.pitch, d.Pitch);
            addItem(Resource.String.yaw, d.Yaw);
            addItem(Resource.String.acc_x, d.AccX);
            addItem(Resource.String.acc_y, d.AccY);
            addItem(Resource.String.acc_z, d.AccZ);
        }

        private string getLabel(int resouceId)
        {
            return this.context.Resources.GetString(resouceId);
        }

        private void addItem(int resourceId, object value)
        {
            items.Add(new string[] { getLabel(resourceId), value.ToString() });
        }
    }
}
