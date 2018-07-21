using System;
using Foundation;
using UIKit;
using JINSMEME.Native.iOS;

namespace SampleApp.Native.iOS
{
    public partial class MMDataViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        private UIView indicatorView;
        private NSDateFormatter dateFormatter;
        private MEMERealTimeData latestRealtimeData;

        public MMDataViewController() : base("MMDataViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            this.Title = "Data Browser";

            this.indicatorView = new UIView(new CoreGraphics.CGRect(0, 0, 24, 24));
            this.indicatorView.Alpha = (nfloat)0.20;
            this.indicatorView.BackgroundColor = UIColor.White;
            this.indicatorView.Layer.CornerRadius = (nfloat)(this.indicatorView.Frame.Size.Height * 0.5);

            this.NavigationItem.RightBarButtonItem = new UIBarButtonItem(this.indicatorView);
            this.NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Disconnect", UIBarButtonItemStyle.Plain, this, new ObjCRuntime.Selector("disconnectButtonPressed:"));

            this.dateFormatter = new NSDateFormatter();
            this.dateFormatter.Locale = NSLocale.CurrentLocale;
            this.dateFormatter.DateStyle = NSDateFormatterStyle.Medium;
            this.dateFormatter.TimeStyle = NSDateFormatterStyle.Short;
        }

        // IBAction
        private void disconnectButtonPressed(NSObject sender)
        {
            var status = MEMELib.SharedInstance().DisconnectPeripheral();
            this.checkMEMEStatus(status);
        }

        private void checkMEMEStatus(MEMEStatus status)
        {
            if (status == MEMEStatus.ErrorAppAuth)
            {
                new UIAlertView("App Auth Faild", "Invalid Application ID or Client Secret.", null, null, new[] { "OK" }).Show();
            }
            else if (status == MEMEStatus.ErrorSdkAuth)
            {
                new UIAlertView("SDK Auth Faild", "Invalid SDK. Please update to the latest SDK.", null, null, new[] { "OK" }).Show();
            }
            else if (status == MEMEStatus.Ok)
            {
                System.Diagnostics.Debug.WriteLine("Status: MEME_OK");
            }
        }

        private void memeRealTimeModeDataReceived(MEMERealTimeData data)
        {
            //self.latestRealTimeData = data;
            this.blinkIndicator();
            //[self.dataTableView reloadData];
        }

        // IBAction
        private void startDataReportButtonPressed(NSObject sender)
        {
            //start data reporting
            var status = MEMELib.SharedInstance().StartDataReport();
            this.checkMEMEStatus(status);
        }

        // IBAction
        private void stopDataReportButtonPressed(NSObject sender)
        {
            //stop data reporting
            var status = MEMELib.SharedInstance().StopDataReport();
            this.checkMEMEStatus(status);
        }

        private void blinkIndicator()
        {
            UIView.Animate(0.05, 0.25, UIViewAnimationOptions.TransitionNone, () =>
            {
                this.indicatorView.BackgroundColor = UIColor.Red;
            }, () =>
            {
                UIView.Animate(0.05, 0.25, UIViewAnimationOptions.TransitionNone, () =>
                {
                    this.indicatorView.BackgroundColor = UIColor.White;
                }, () => { });
            });
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return 16;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("DataCellIdentifier", indexPath);
            var label = "";
            var value = "";

            var data = this.latestRealtimeData;

            switch (indexPath.Row)
            {
                case 0:
                    label = @"Fit Status";
                    value = FORMAT_INT(data.FitError);
                    break;

                case 1:
                    label = @"Walking";
                    value = FORMAT_INT(data.IsWalking);
                    break;

                case 2:
                    label = @"NoiseStatus";
                    value = FORMAT_INT(data.NoiseStatus);
                    break;

                case 3:
                    label = @"Power Left";
                    value = FORMAT_INT(data.PowerLeft);
                    break;

                case 4:
                    label = @"Eye Move Up";
                    value = FORMAT_INT(data.EyeMoveUp);
                    break;

                case 5:
                    label = @"Eye Move Down";
                    value = FORMAT_INT(data.EyeMoveDown);
                    break;

                case 6:
                    label = @"Eye Move Left";
                    value = FORMAT_INT(data.EyeMoveLeft);
                    break;

                case 7:
                    label = @"Eye Move Right";
                    value = FORMAT_INT(data.EyeMoveRight);
                    break;

                case 8:
                    label = @"Blink Streangth";
                    value = FORMAT_INT(data.BlinkStrength);
                    break;

                case 9:
                    label = @"Blink Speed";
                    value = FORMAT_INT(data.BlinkSpeed);
                    break;

                case 10:
                    label = @"Roll";
                    value = FORMAT_FLOAT(data.Roll);
                    break;

                case 11:
                    label = @"Pitch";
                    value = FORMAT_FLOAT(data.Pitch);
                    break;

                case 12:
                    label = @"Yaw";
                    value = FORMAT_FLOAT(data.Yaw);
                    break;

                case 13:
                    label = @"Acc X";
                    value = FORMAT_FLOAT(data.AccX);
                    break;

                case 14:
                    label = @"Acc Y";
                    value = FORMAT_FLOAT(data.AccY);
                    break;

                case 15:
                    label = @"Acc Z";
                    value = FORMAT_FLOAT(data.AccZ);
                    break;

                default:
                    break;
            }

            cell.textLabel.text = label;
            cell.detailTextLabel.text = value;

            return cell;
        }

        [Export("numberOfSectionsInTableView:")]
        public nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }
    }
}

