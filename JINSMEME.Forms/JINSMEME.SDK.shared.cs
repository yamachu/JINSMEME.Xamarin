using System;
using System.Collections.Generic;

namespace JINSMEME.Forms
{
    public static partial class MemeLib
    {
        public static event EventHandler<bool> Connected;
        public static event EventHandler Disconnected;
        public static event EventHandler<string> Found;
        public static event EventHandler<MemeRealtimeData> RealtimeDataRecieved;

        public static bool IsConnected => PlatformIsConnected;
        public static string SDKVersion => PlatformSDKVersion;
        public static string FWVersion => PlatformFWVersion;
        public static int HWVersion => PlatformHWVersion;
        public static bool IsDataReceiving => PlatformIsDataReceiving;
        public static MemeCalibStatus CalibrateStatus => PlatformCalibrateStatus;

        public static MemeStatus StartScan(Action<string> scanCallback) => PlatformStartScan();
        public static MemeStatus StopScan() => PlatformStopScan();

        public static bool AutoConnect
        {
            private get
            {
                return true; // dummy
            }
            set
            {
                PlatformSetAutoConnect(value);
            }
        }

        public static MemeStatus Connect(string device) => PlatformConnect(device);
        public static void Disconnect() => PlatformDisconnect();
        
        // add realtime listener
        public static MemeStatus StartDataReport() => PlatformStartDataReport();
        public static void StopDataReport() => PlatformStopDataReport();

        public static IEnumerable<string> GetConnectedByOthers() => PlatformGetConnectedByOthers();
        public static int GetConnectedDeviceType() => PlatformGetConnectedDeviceType();
        public static int GetConnectedDeviceSubType() => PlatformGetConnectedDeviceSubType();
    }

    public struct MemeRealtimeData
    {
        public int EyeMoveUp;
        public int EyeMoveDown;
        public int EyeMoveLeft;
        public int EyeMoveRight;
        public int BlinkSpped;
        public int BlinkStrength;
        public bool IsWalking;
        public float Roll;
        public float Pitch;
        public float Yaw;
        public float AccX;
        public float AccY;
        public float AccZ;
        public bool NoiseStatus;
        public MemeFitStatus FitError;
        public int PowerLeft;
    }

    public struct MemeResponse
    {
        public bool CommandResult;
        public int EventCode;
    }

    public enum MemeStatus
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

    public enum MemeFitStatus
    {
        MEME_FIT_OK,
        MEME_FIT_ERROR_R,
        MEME_FIT_ERROR_L,
        MEME_FIT_ERROR_BRIDGE,
    }

    public enum MemeCalibStatus
    {
        CALIB_NOT_FINISHED,
        CALIB_BODY_FINISHED,
        CALIB_EYE_FINISHED,
        CALIB_BOTH_FINISHED,
    }
}
