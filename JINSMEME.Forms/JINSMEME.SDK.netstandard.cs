using System;
using System.Collections.Generic;

namespace JINSMEME.Forms
{
    public partial class MemeLib
    {
        internal static bool PlatformIsConnected => throw new PlatformNotSupportedException();
        internal static string PlatformSDKVersion => throw new PlatformNotSupportedException();
        internal static string PlatformFWVersion => throw new PlatformNotSupportedException();
        internal static int PlatformHWVersion => throw new PlatformNotSupportedException();
        internal static bool PlatformIsDataReceiving => throw new PlatformNotSupportedException();
        internal static MemeCalibStatus PlatformCalibrateStatus => throw new PlatformNotSupportedException();

        internal static MemeStatus PlatformStartScan() => throw new PlatformNotSupportedException();
        internal static MemeStatus PlatformStopScan() => throw new PlatformNotSupportedException();

        internal static void PlatformSetAutoConnect(bool value) => throw new PlatformNotSupportedException();
        internal static MemeStatus PlatformConnect(string device) => throw new PlatformNotSupportedException();
        internal static void PlatformDisconnect() => throw new PlatformNotSupportedException();

        internal static MemeStatus PlatformStartDataReport() => throw new PlatformNotSupportedException();
        internal static void PlatformStopDataReport() => throw new PlatformNotSupportedException();

        internal static IEnumerable<string> PlatformGetConnectedByOthers() => throw new PlatformNotSupportedException();
        internal static int PlatformGetConnectedDeviceType() => throw new PlatformNotSupportedException();
        internal static int PlatformGetConnectedDeviceSubType() => throw new PlatformNotSupportedException();
    }
}
