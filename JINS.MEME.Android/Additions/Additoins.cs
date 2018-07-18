using System;

namespace JINSMEME.Native.Android
{
    public enum MemeCalibStatusWrapped
    {
        CALIB_NOT_FINISHED,
        CALIB_BODY_FINISHED,
        CALIB_EYE_FINISHED,
        CALIB_BOTH_FINISHED,
    }

    public static class MemeCalibStatusExtension
    {
        public static MemeCalibStatusWrapped NativeToEnum(this global::JINSMEME.Native.Android.MemeCalibStatus var)
        {
            if (var.Equals(global::JINSMEME.Native.Android.MemeCalibStatus.CalibNotFinished))
                return MemeCalibStatusWrapped.CALIB_NOT_FINISHED;
            if (var.Equals(global::JINSMEME.Native.Android.MemeCalibStatus.CalibBodyFinished))
                return MemeCalibStatusWrapped.CALIB_BODY_FINISHED;
            if (var.Equals(global::JINSMEME.Native.Android.MemeCalibStatus.CalibEyeFinished))
                return MemeCalibStatusWrapped.CALIB_EYE_FINISHED;
            if (var.Equals(global::JINSMEME.Native.Android.MemeCalibStatus.CalibBothFinished))
                return MemeCalibStatusWrapped.CALIB_BOTH_FINISHED;
            throw new ArgumentException();
        }

        public static global::JINSMEME.Native.Android.MemeCalibStatus EnumToNative(this MemeCalibStatusWrapped var)
        {
            switch (var)
            {
                case MemeCalibStatusWrapped.CALIB_BODY_FINISHED:
                    return global::JINSMEME.Native.Android.MemeCalibStatus.CalibBodyFinished;
                case MemeCalibStatusWrapped.CALIB_BOTH_FINISHED:
                    return global::JINSMEME.Native.Android.MemeCalibStatus.CalibBothFinished;
                case MemeCalibStatusWrapped.CALIB_EYE_FINISHED:
                    return global::JINSMEME.Native.Android.MemeCalibStatus.CalibEyeFinished;
                case MemeCalibStatusWrapped.CALIB_NOT_FINISHED:
                    return global::JINSMEME.Native.Android.MemeCalibStatus.CalibNotFinished;
            }
            throw new ArgumentException();
        }
    }

    public enum MemeFitStatusWrapped
    {
        MEME_FIT_OK,
        MEME_FIT_ERROR_R,
        MEME_FIT_ERROR_L,
        MEME_FIT_ERROR_BRIDGE,
    }

    public static class MemeFitStatusExtension
    {
        public static MemeFitStatusWrapped NativeToEnum(this global::JINSMEME.Native.Android.MemeFitStatus var)
        {
            if (var.Equals(global::JINSMEME.Native.Android.MemeFitStatus.MemeFitErrorBridge))
                return MemeFitStatusWrapped.MEME_FIT_ERROR_BRIDGE;
            if (var.Equals(global::JINSMEME.Native.Android.MemeFitStatus.MemeFitErrorL))
                return MemeFitStatusWrapped.MEME_FIT_ERROR_L;
            if (var.Equals(global::JINSMEME.Native.Android.MemeFitStatus.MemeFitErrorR))
                return MemeFitStatusWrapped.MEME_FIT_ERROR_R;
            if (var.Equals(global::JINSMEME.Native.Android.MemeFitStatus.MemeFitOk))
                return MemeFitStatusWrapped.MEME_FIT_OK;
            throw new ArgumentException();
        }

        public static global::JINSMEME.Native.Android.MemeFitStatus EnumToNative(this MemeFitStatusWrapped var)
        {
            switch (var)
            {
                case MemeFitStatusWrapped.MEME_FIT_ERROR_BRIDGE:
                    return global::JINSMEME.Native.Android.MemeFitStatus.MemeFitErrorBridge;
                case MemeFitStatusWrapped.MEME_FIT_ERROR_L:
                    return global::JINSMEME.Native.Android.MemeFitStatus.MemeFitErrorL;
                case MemeFitStatusWrapped.MEME_FIT_ERROR_R:
                    return global::JINSMEME.Native.Android.MemeFitStatus.MemeFitErrorR;
                case MemeFitStatusWrapped.MEME_FIT_OK:
                    return global::JINSMEME.Native.Android.MemeFitStatus.MemeFitOk;

            }
            throw new ArgumentException();
        }
    }

    public enum MemeStatusWrapped
    {
        MEME_OK,
        MEME_ERROR,
        MEME_ERROR_SDK_AUTH,
        MEME_ERROR_APP_AUTH,
        MEME_ERROR_CONNECTION,
        MEME_DEVICE_INVALID,
        MEME_CMD_INVALID,
        MEME_ERROR_FW_CHECK,
        MEME_ERROR_BL_OFF,
    }

    public static class MemeStatusExtension
    {
        public static MemeStatusWrapped NativeToEnum(this global::JINSMEME.Native.Android.MemeStatus var)
        {
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeCmdInvalid))
                return MemeStatusWrapped.MEME_CMD_INVALID;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeDeviceInvalid))
                return MemeStatusWrapped.MEME_DEVICE_INVALID;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeError))
                return MemeStatusWrapped.MEME_ERROR;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeErrorAppAuth))
                return MemeStatusWrapped.MEME_ERROR_APP_AUTH;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeErrorBlOff))
                return MemeStatusWrapped.MEME_ERROR_BL_OFF;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeErrorConnection))
                return MemeStatusWrapped.MEME_ERROR_CONNECTION;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeErrorFwCheck))
                return MemeStatusWrapped.MEME_ERROR_FW_CHECK;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeErrorSdkAuth))
                return MemeStatusWrapped.MEME_ERROR_SDK_AUTH;
            if (var.Equals(global::JINSMEME.Native.Android.MemeStatus.MemeOk))
                return MemeStatusWrapped.MEME_OK;
            throw new ArgumentException();
        }

        public static global::JINSMEME.Native.Android.MemeStatus EnumToNative(this MemeStatusWrapped var)
        {
            switch (var)
            {
                case MemeStatusWrapped.MEME_CMD_INVALID:
                    return global::JINSMEME.Native.Android.MemeStatus.MemeCmdInvalid;
                case MemeStatusWrapped.MEME_DEVICE_INVALID:
                    return global::JINSMEME.Native.Android.MemeStatus.MemeDeviceInvalid;
                case MemeStatusWrapped.MEME_ERROR:
                    return global::JINSMEME.Native.Android.MemeStatus.MemeError;
                case MemeStatusWrapped.MEME_ERROR_APP_AUTH:
                    return global::JINSMEME.Native.Android.MemeStatus.MemeErrorAppAuth;
                case MemeStatusWrapped.MEME_ERROR_BL_OFF:
                    return global::JINSMEME.Native.Android.MemeStatus.MemeErrorBlOff;
                case MemeStatusWrapped.MEME_ERROR_CONNECTION:
                    return global::JINSMEME.Native.Android.MemeStatus.MemeErrorConnection;
                case MemeStatusWrapped.MEME_ERROR_FW_CHECK:
                    return global::JINSMEME.Native.Android.MemeStatus.MemeErrorFwCheck;
                case MemeStatusWrapped.MEME_ERROR_SDK_AUTH:
                    return global::JINSMEME.Native.Android.MemeStatus.MemeErrorSdkAuth;
                case MemeStatusWrapped.MEME_OK:
                    return global::JINSMEME.Native.Android.MemeStatus.MemeOk;
            }
            throw new ArgumentException();
        }
    }

    public partial class MemeRealtimeData : global::Java.Lang.Object
    {
        public global::JINSMEME.Native.Android.MemeFitStatusWrapped FitErrorWrapped
        {
            get
            {
                return this.FitError.NativeToEnum();
            }
        }
    }

    public sealed partial class MemeLib : global::Java.Lang.Object
    {
        public global::JINSMEME.Native.Android.MemeStatusWrapped ConnectWrapped(string p0)
        {
            return this.Connect(p0).NativeToEnum();
        }

        public global::JINSMEME.Native.Android.MemeStatusWrapped StartScanWrapped(global::JINSMEME.Native.Android.IMemeScanListener p0)
        {
            return this.StartScan(p0).NativeToEnum();
        }

        public global::JINSMEME.Native.Android.MemeStatusWrapped StopScanWrapped()
        {
            return this.StopScan().NativeToEnum();
        }

        public global::JINSMEME.Native.Android.MemeStatusWrapped SetMemeConnectListenerWrapped(global::JINSMEME.Native.Android.IMemeConnectListener p0)
        {
            return this.SetMemeConnectListener(p0).NativeToEnum();
        }

        public global::JINSMEME.Native.Android.MemeStatusWrapped StartDataReportWrapped(global::JINSMEME.Native.Android.IMemeRealtimeListener p0)
        {
            return this.StartDataReport(p0).NativeToEnum();
        }

        public global::JINSMEME.Native.Android.MemeCalibStatusWrapped IsCalibratedWrapped()
        {
            return this.IsCalibrated().NativeToEnum();
        }
    }
}
