using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SampleApp.Forms
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.ScanButton.Clicked += (_, __) =>
            {
                JINSMEME.Forms.MemeLib.StartScan(null);

                JINSMEME.Forms.MemeLib.Found += (sender_, e) =>
                {
                    Console.WriteLine($"Found: {e}");
                    JINSMEME.Forms.MemeLib.StopScan();
                    JINSMEME.Forms.MemeLib.Connect(e);
                };

                JINSMEME.Forms.MemeLib.Connected += (sender, e) =>
                {
                    Console.WriteLine($"Connected?: {e}");
                    JINSMEME.Forms.MemeLib.StartDataReport();
                };

                JINSMEME.Forms.MemeLib.RealtimeDataRecieved += (sender, e) =>
                {
                    Console.WriteLine(JsonConvert.SerializeObject(e, Formatting.Indented));
                };
            };
        }
    }
}
