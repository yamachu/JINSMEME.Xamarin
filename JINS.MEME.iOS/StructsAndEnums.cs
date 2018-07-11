using System.Runtime.InteropServices;

namespace JINS.MEME.iOS
{
    public enum MEMEType : uint
    {
        Es = 0,
        Mt = 1
    }

    public enum MEMECalibStatus : uint
    {
        NotFinished = 0,
        BodyFinished = 1,
        EyeFinished = 2,
        BothFinished = 3
    }

    public enum MEMEStatus : uint
    {
        Ok = 0,
        Error = 1, // Misc Error
        ErrorSdkAuth = 2, // SDK Auth Error
        ErrorAppAuth = 3, // App Auth Error
        ErrorConnection = 4, // No Connection
        DeviceInvalid = 5, // Invalid Device
        CmdInvalid = 6, // Invalid Command
        ErrorFwCheck = 7, // FW Version Error
        ErrorBlOff = 8  // Bluetooth is Off
    }

    // public enum MemeFitStatus?

    [StructLayout(LayoutKind.Sequential)]
    public struct MEMEResponse
    {
        public int eventCode; // 0x02: begin sending, 0x04: stop sending

        public bool commandResult;
    }
}
