using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using JINSMEME.Native.iOS;

namespace JINSMEME.Forms
{
    public static partial class MemeLib
    {
        private static JINSMEME.Native.iOS.MEMELib Instance;
        private static MEMELibDelegate DelegateInstance = new DelegateImpl();
        // ToDo: Clean...
        internal static Dictionary<string, CoreBluetooth.CBPeripheral> FoundPeripherals = new Dictionary<string, CoreBluetooth.CBPeripheral>();
        public static void Init(string clientId, string clientSecret)
        {
            JINSMEME.Native.iOS.MEMELib.SetAppClientId(clientId, clientSecret);
            MemeLib.Instance = JINSMEME.Native.iOS.MEMELib.SharedInstance();
            Instance.Delegate = DelegateInstance;
        }

        internal static bool PlatformIsConnected => Instance.IsConnected;
        internal static string PlatformSDKVersion => Instance.SDKVersion();
        internal static string PlatformFWVersion => Instance.FWVersion();
        internal static int PlatformHWVersion => Instance.HWVersion();
        internal static bool PlatformIsDataReceiving => Instance.IsDataReceiving;
        internal static MemeCalibStatus PlatformCalibrateStatus => Instance.IsCalibrated.NativeToEnum();

        internal static MemeStatus PlatformStartScan() => Instance.StartScanningPeripherals().NativeToEnum();
        internal static MemeStatus PlatformStopScan() => Instance.StopScanningPeripherals().NativeToEnum();

        internal static void PlatformSetAutoConnect(bool value) => Instance.SetAutoConnect(value);
        internal static MemeStatus PlatformConnect(string address)
        {
            var peripheral = FoundPeripherals.GetValueOrDefault(address);
            if (peripheral == default(CoreBluetooth.CBPeripheral)) return MemeStatus.MEME_ERROR_CONNECTION;
            return Instance.ConnectPeripheral(peripheral).NativeToEnum();
        }
        internal static void PlatformDisconnect() => Instance.DisconnectPeripheral().NativeToEnum();

        internal static MemeStatus PlatformStartDataReport() => Instance.StartDataReport().NativeToEnum();
        internal static void PlatformStopDataReport() => Instance.StopDataReport().NativeToEnum();

        internal static IEnumerable<string> PlatformGetConnectedByOthers() => Instance.ConnectedByOthers().Select((v) => v.Name);
        internal static int PlatformGetConnectedDeviceType() => Instance.ConnectedDeviceType();
        internal static int PlatformGetConnectedDeviceSubType() => Instance.ConnectedDeviceSubType();

        internal static MemeCalibStatus NativeToEnum(this global::JINSMEME.Native.iOS.MEMECalibStatus var)
        {
            if (var.Equals(global::JINSMEME.Native.iOS.MEMECalibStatus.NotFinished))
                return MemeCalibStatus.CALIB_NOT_FINISHED;
            if (var.Equals(global::JINSMEME.Native.iOS.MEMECalibStatus.BodyFinished))
                return MemeCalibStatus.CALIB_BODY_FINISHED;
            if (var.Equals(global::JINSMEME.Native.iOS.MEMECalibStatus.EyeFinished))
                return MemeCalibStatus.CALIB_EYE_FINISHED;
            if (var.Equals(global::JINSMEME.Native.iOS.MEMECalibStatus.BothFinished))
                return MemeCalibStatus.CALIB_BOTH_FINISHED;
            throw new ArgumentException();
        }

        internal static MemeStatus NativeToEnum(this global::JINSMEME.Native.iOS.MEMEStatus var)
        {
            if (var.Equals(global::JINSMEME.Native.iOS.MEMEStatus.CmdInvalid))
                return MemeStatus.MEME_CMD_INVALID;
            if (var.Equals(global::JINSMEME.Native.iOS.MEMEStatus.DeviceInvalid))
                return MemeStatus.MEME_DEVICE_INVALID;
            if (var.Equals(global::JINSMEME.Native.iOS.MEMEStatus.Error))
                return MemeStatus.MEME_ERROR;
            if (var.Equals(global::JINSMEME.Native.iOS.MEMEStatus.ErrorAppAuth))
                return MemeStatus.MEME_ERROR_APP_AUTH;
            if (var.Equals(global::JINSMEME.Native.iOS.MEMEStatus.ErrorBlOff))
                return MemeStatus.MEME_ERROR_BL_OFF;
            if (var.Equals(global::JINSMEME.Native.iOS.MEMEStatus.ErrorConnection))
                return MemeStatus.MEME_ERROR_CONNECTION;
            if (var.Equals(global::JINSMEME.Native.iOS.MEMEStatus.ErrorFwCheck))
                return MemeStatus.MEME_ERROR_FW_CHECK;
            if (var.Equals(global::JINSMEME.Native.iOS.MEMEStatus.ErrorSdkAuth))
                return MemeStatus.MEME_ERROR_SDK_AUTH;
            if (var.Equals(global::JINSMEME.Native.iOS.MEMEStatus.Ok))
                return MemeStatus.MEME_OK;
            throw new ArgumentException();
        }

        private class DelegateImpl : MEMELibDelegate
        {
            [Export("memeAppAuthorized:")]
            public override void MemeAppAuthorized(MEMEStatus status)
            {
                // ToDo: Error handling
                switch (status)
                {
                    case MEMEStatus.ErrorAppAuth:
                        System.Diagnostics.Debug.WriteLine("Auth: ErrorAppAuth, Invalid Application ID or Client Secret");
                        break;
                    case MEMEStatus.ErrorSdkAuth:
                        System.Diagnostics.Debug.WriteLine("Auth: ErrorSdkAuth, Invalid SDK. Please update to the latest SDK.");
                        break;
                    case MEMEStatus.Ok:
                        System.Diagnostics.Debug.WriteLine("Auth: Ok");
                        break;
                    default:
                        break;
                }
            }

            [Export("memePeripheralFound:withDeviceAddress:")]
            public override void MemePeripheralFound(CoreBluetooth.CBPeripheral peripheral, string address)
            {
                FoundPeripherals.Remove(address);
                FoundPeripherals.Add(address, peripheral);
                Found?.Invoke(this, address);
            }

            [Export("memePeripheralConnected:")]
            public override void MemePeripheralConnected(CoreBluetooth.CBPeripheral peripheral)
            {
                Connected?.Invoke(this, true);
            }

            [Export("memePeripheralDisconnected:")]
            public override void MemePeripheralDisconnected(CoreBluetooth.CBPeripheral peripheral)
            {
                Disconnected?.Invoke(this, null);
            }

            [Export("memeRealTimeModeDataReceived:")]
            public override void MemeRealTimeModeDataReceived(MEMERealTimeData data)
            {
                RealtimeDataRecieved?.Invoke(this, new MemeRealtimeData
                {
                    EyeMoveUp = data.EyeMoveUp,
                    EyeMoveDown = data.EyeMoveDown,
                    EyeMoveLeft = data.EyeMoveLeft,
                    EyeMoveRight = data.EyeMoveRight,
                    BlinkSpped = data.BlinkSpeed,
                    BlinkStrength = data.BlinkStrength,
                    IsWalking = data.IsWalking,
                    Roll = data.Roll,
                    Pitch = data.Pitch,
                    Yaw = data.Yaw,
                    AccX = data.AccX,
                    AccY = data.AccY,
                    AccZ = data.AccZ,
                    NoiseStatus = data.NoiseStatus,
                    FitError = (JINSMEME.Forms.MemeFitStatus)data.FitError, /* Is correct? */
                    PowerLeft = data.EyeMoveLeft,
                });
            }

            [Export("memeCommandResponse:")]
            public override void MemeCommandResponse(MEMEResponse response)
            {
                System.Diagnostics.Debug.WriteLine($"Command: ${response.eventCode} - ${response.commandResult}");
            }

            [Export("memeDeviceInfoUpdated")]
            public override void MemeDeviceInfoUpdated()
            {
                System.Diagnostics.Debug.WriteLine("DeviceInfoUpdated");
            }
        }
    }
}
