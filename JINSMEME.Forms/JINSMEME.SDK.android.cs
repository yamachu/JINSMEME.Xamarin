using System;
using System.Collections.Generic;
using JINSMEME.Native.Android;

namespace JINSMEME.Forms
{
    public static partial class MemeLib
    {
        private static JINSMEME.Native.Android.MemeLib Instance = JINSMEME.Native.Android.MemeLib.Instance;
        public static void Init(Android.Content.Context context, string clientId, string clientSecret)
        {
            JINSMEME.Native.Android.MemeLib.SetAppClientID(Android.App.Application.Context, clientId, clientSecret);
            MemeLib.Instance = JINSMEME.Native.Android.MemeLib.Instance;
        }

        internal static bool PlatformIsConnected => Instance.IsConnected;
        internal static string PlatformSDKVersion => Instance.SDKVersion;
        internal static string PlatformFWVersion => Instance.FWVersion;
        internal static int PlatformHWVersion => Instance.HWVersion;
        internal static bool PlatformIsDataReceiving => Instance.IsDataReceiving;
        internal static MemeCalibStatus PlatformCalibrateStatus => Instance.IsCalibrated().NativeToEnum();

        private static ScanListenerImpl BaseScanListener = new ScanListenerImpl
        (
                (address) => Found?.Invoke(MemeLib.Instance, address)
        );
        internal static MemeStatus PlatformStartScan() => Instance.StartScan(BaseScanListener).NativeToEnum();
        internal static MemeStatus PlatformStopScan() => Instance.StopScan().NativeToEnum();

        private static ConnectListenerImpl BaseConnectListener = new ConnectListenerImpl
        (
            (status) => Connected?.Invoke(MemeLib.Instance, status),
            () => Disconnected?.Invoke(MemeLib.Instance, null)
        );
        internal static void PlatformSetAutoConnect(bool value) => Instance.AutoConnect = value;
        internal static MemeStatus PlatformConnect(string device)
        {
            Instance.SetMemeConnectListener(BaseConnectListener);
            return Instance.Connect(device).NativeToEnum();
        }
        internal static void PlatformDisconnect() => Instance.Disconnect();

        private static RealtimeListenerImpl BaseRealtimeListener = new RealtimeListenerImpl
        (
                (realtimeData) => RealtimeDataRecieved?.Invoke(MemeLib.Instance, realtimeData.ToManaged())
        );
        internal static MemeStatus PlatformStartDataReport()
        {
            return Instance.StartDataReport(BaseRealtimeListener).NativeToEnum();
        }
        internal static void PlatformStopDataReport() => Instance.StopDataReport();

        internal static IEnumerable<string> PlatformGetConnectedByOthers() => Instance.ConnectedByOthers;
        internal static int PlatformGetConnectedDeviceType() => Instance.ConnectedDeviceType;
        internal static int PlatformGetConnectedDeviceSubType() => Instance.ConnectedDeviceSubType;

        internal static MemeCalibStatus NativeToEnum(this global::JINSMEME.Native.Android.MemeCalibStatus var)
        {
            if (var.Equals(global::JINSMEME.Native.Android.MemeCalibStatus.CalibNotFinished))
                return MemeCalibStatus.CALIB_NOT_FINISHED;
            if (var.Equals(global::JINSMEME.Native.Android.MemeCalibStatus.CalibBodyFinished))
                return MemeCalibStatus.CALIB_BODY_FINISHED;
            if (var.Equals(global::JINSMEME.Native.Android.MemeCalibStatus.CalibEyeFinished))
                return MemeCalibStatus.CALIB_EYE_FINISHED;
            if (var.Equals(global::JINSMEME.Native.Android.MemeCalibStatus.CalibBothFinished))
                return MemeCalibStatus.CALIB_BOTH_FINISHED;
            throw new ArgumentException();
        }

        internal static MemeStatus NativeToEnum(this global::JINSMEME.Native.Android.MemeStatus var)
        {
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeCmdInvalid))
                return MemeStatus.MEME_CMD_INVALID;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeDeviceInvalid))
                return MemeStatus.MEME_DEVICE_INVALID;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeError))
                return MemeStatus.MEME_ERROR;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeErrorAppAuth))
                return MemeStatus.MEME_ERROR_APP_AUTH;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeErrorBlOff))
                return MemeStatus.MEME_ERROR_BL_OFF;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeErrorConnection))
                return MemeStatus.MEME_ERROR_CONNECTION;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeErrorFwCheck))
                return MemeStatus.MEME_ERROR_FW_CHECK;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeErrorSdkAuth))
                return MemeStatus.MEME_ERROR_SDK_AUTH;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeOk))
                return MemeStatus.MEME_OK;
            throw new ArgumentException();
        }

        internal static MemeFitStatus NativeToEnum(this global::JINSMEME.Native.Android.MemeFitStatus var)
        {
            if (var.Equals(global::JINSMEME.Native.Android.MemeFitStatus.MemeFitErrorBridge))
                return MemeFitStatus.MEME_FIT_ERROR_BRIDGE;
            if (var.Equals(global::JINSMEME.Native.Android.MemeFitStatus.MemeFitErrorL))
                return MemeFitStatus.MEME_FIT_ERROR_L;
            if (var.Equals(global::JINSMEME.Native.Android.MemeFitStatus.MemeFitErrorR))
                return MemeFitStatus.MEME_FIT_ERROR_R;
            if (var.Equals(global::JINSMEME.Native.Android.MemeFitStatus.MemeFitOk))
                return MemeFitStatus.MEME_FIT_OK;
            throw new ArgumentException();
        }

        internal class ConnectListenerImpl : Java.Lang.Object, JINSMEME.Native.Android.IMemeConnectListener
        {
            private readonly Action<bool> connectAction;
            private readonly Action disconnectAction;

            public ConnectListenerImpl(Action<bool> connectAction, Action disconnectAction)
            {
                this.connectAction = connectAction;
                this.disconnectAction = disconnectAction;
            }

            public void MemeConnectCallback(bool status) => connectAction(status);

            public void MemeDisconnectCallback() => disconnectAction();
        }

        internal class ScanListenerImpl : Java.Lang.Object, JINSMEME.Native.Android.IMemeScanListener
        {
            private readonly Action<string> foundAction;

            public ScanListenerImpl(Action<string> foundAction)
            {
                this.foundAction = foundAction;
            }

            public void MemeFoundCallback(string address) => foundAction(address);
        }

        internal class RealtimeListenerImpl : Java.Lang.Object, JINSMEME.Native.Android.IMemeRealtimeListener
        {
            private readonly Action<JINSMEME.Native.Android.MemeRealtimeData> realtimeAction;

            public RealtimeListenerImpl(Action<JINSMEME.Native.Android.MemeRealtimeData> realtimeAction)
            {
                this.realtimeAction = realtimeAction;
            }

            public void MemeRealtimeCallback(Native.Android.MemeRealtimeData realtimeData) => realtimeAction(realtimeData);
        }

        internal static MemeRealtimeData ToManaged(this global::JINSMEME.Native.Android.MemeRealtimeData var)
        {
            return new MemeRealtimeData
            {
                EyeMoveUp = var.EyeMoveUp,
                EyeMoveDown = var.EyeMoveDown,
                EyeMoveLeft = var.EyeMoveLeft,
                EyeMoveRight = var.EyeMoveRight,
                BlinkSpped = var.BlinkSpeed,
                BlinkStrength = var.BlinkStrength,
                IsWalking = var.IsWalking,
                Roll = var.Roll,
                Pitch = var.Pitch,
                Yaw = var.Yaw,
                AccX = var.AccX,
                AccY = var.AccY,
                AccZ = var.AccZ,
                NoiseStatus = var.NoiseStatus,
                FitError = var.FitError.NativeToEnum(),
                PowerLeft = var.EyeMoveLeft,
            };
        }
    }
}
