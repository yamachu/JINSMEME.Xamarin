using System;
using CoreBluetooth;
using Foundation;
using ObjCRuntime;

namespace JINSMEME.Native.iOS
{
    // @interface MEMEData : NSObject
    [BaseType(typeof(NSObject))]
    interface MEMEData
    {
        // byte* => ref byte

        // -(instancetype)initWithDataPacket:(UInt8 *)data;
        [Export("initWithDataPacket:")]
        IntPtr Constructor(ref byte data);

        // -(uint64_t)getUnixTime:(UInt8 *)data;
        [Export("getUnixTime:")]
        ulong GetUnixTime(ref byte data);

        // -(UInt16)getUInt16:(UInt8 *)data;
        [Export("getUInt16:")]
        ushort GetUInt16(ref byte data);

        // -(SInt16)getSInt16:(UInt8 *)data;
        [Export("getSInt16:")]
        short GetSInt16(ref byte data);

        // -(float)getFloat16:(UInt8 *)data;
        [Export("getFloat16:")]
        float GetFloat16(ref byte data);

        // -(float)getFloat24:(UInt8 *)data;
        [Export("getFloat24:")]
        float GetFloat24(ref byte data);
    }

    // @interface MEMERealTimeData : MEMEData
    [BaseType(typeof(MEMEData))]
    interface MEMERealTimeData
    {
        // @property UInt8 fitError;
        [Export("fitError")]
        byte FitError { get; set; } // 0: normal, 1: error

        // @property BOOL isWalking;
        [Export("isWalking")]
        bool IsWalking { get; set; }

        // @property BOOL noiseStatus;
        [Export("noiseStatus")]
        bool NoiseStatus { get; set; }

        // @property UInt8 powerLeft;
        [Export("powerLeft")]
        byte PowerLeft { get; set; } // 0: empty, 5: full

        // @property UInt8 eyeMoveUp;
        [Export("eyeMoveUp")]
        byte EyeMoveUp { get; set; }

        // @property UInt8 eyeMoveDown;
        [Export("eyeMoveDown")]
        byte EyeMoveDown { get; set; }

        // @property UInt8 eyeMoveLeft;
        [Export("eyeMoveLeft")]
        byte EyeMoveLeft { get; set; }

        // @property UInt8 eyeMoveRight;
        [Export("eyeMoveRight")]
        byte EyeMoveRight { get; set; }

        // @property UInt8 blinkSpeed;
        [Export("blinkSpeed")]
        byte BlinkSpeed { get; set; } // ms

        // @property UInt16 blinkStrength;
        [Export("blinkStrength")]
        ushort BlinkStrength { get; set; }

        // @property float roll;
        [Export("roll")]
        float Roll { get; set; }

        // @property float pitch;
        [Export("pitch")]
        float Pitch { get; set; }

        // @property float yaw;
        [Export("yaw")]
        float Yaw { get; set; }

        // @property float accX;
        [Export("accX")]
        float AccX { get; set; }

        // @property float accY;
        [Export("accY")]
        float AccY { get; set; }

        // @property float accZ;
        [Export("accZ")]
        float AccZ { get; set; }
    }

    [Static]
    // [Verify (ConstantsInterfaceAssociation)]
    partial interface Constants
    {
        // extern double MEMEVersionNumber;
        [Field("MEMEVersionNumber", "__Internal")]
        double MEMEVersionNumber { get; }

        // extern const unsigned char [] MEMEVersionString;
        [Field("MEMEVersionString", "__Internal")]
        // byte[] MEMEVersionString { get; }
        IntPtr MEMEVersionString { get; }
    //}

    //[Static]
    // [Verify (ConstantsInterfaceAssociation)]
    //partial interface Constants
    //{
        // extern NSString * MEMELibAppAuthorizedNotification;
        [Field("MEMELibAppAuthorizedNotification", "__Internal")]
        NSString MEMELibAppAuthorizedNotification { get; }

        // extern NSString * MEMELibPeripheralFoundNotification;
        [Field("MEMELibPeripheralFoundNotification", "__Internal")]
        NSString MEMELibPeripheralFoundNotification { get; }

        // extern NSString * MEMELibPeripheralConnectedNotification;
        [Field("MEMELibPeripheralConnectedNotification", "__Internal")]
        NSString MEMELibPeripheralConnectedNotification { get; }

        // extern NSString * MEMELibPeripheralDisconnetedNotification;
        [Field("MEMELibPeripheralDisconnetedNotification", "__Internal")]
        NSString MEMELibPeripheralDisconnetedNotification { get; }

        // extern NSString * MEMELibRealtimeModeDataReceivedNotification;
        [Field("MEMELibRealtimeModeDataReceivedNotification", "__Internal")]
        NSString MEMELibRealtimeModeDataReceivedNotification { get; }

        // extern NSString * MEMELibCommandResponseNotification;
        [Field("MEMELibCommandResponseNotification", "__Internal")]
        NSString MEMELibCommandResponseNotification { get; }

        // extern NSString * MEMELibDeviceInfoUpdatedNotification;
        [Field("MEMELibDeviceInfoUpdatedNotification", "__Internal")]
        NSString MEMELibDeviceInfoUpdatedNotification { get; }

        // extern NSString * MEMELibStatusCodeUserInfoKey;
        [Field("MEMELibStatusCodeUserInfoKey", "__Internal")]
        NSString MEMELibStatusCodeUserInfoKey { get; }

        // extern NSString * MEMELibPeripheralUserInfoKey;
        [Field("MEMELibPeripheralUserInfoKey", "__Internal")]
        NSString MEMELibPeripheralUserInfoKey { get; }

        // extern NSString * MEMELibMacAddressUserInfoKey;
        [Field("MEMELibMacAddressUserInfoKey", "__Internal")]
        NSString MEMELibMacAddressUserInfoKey { get; }

        // extern NSString * MEMELibStandardDataUserInfoKey;
        [Field("MEMELibStandardDataUserInfoKey", "__Internal")]
        NSString MEMELibStandardDataUserInfoKey { get; }

        // extern NSString * MEMELibRealtimeDataUserInfoKey;
        [Field("MEMELibRealtimeDataUserInfoKey", "__Internal")]
        NSString MEMELibRealtimeDataUserInfoKey { get; }

        // extern NSString * MEMELibResponseUserInfoKey;
        [Field("MEMELibResponseUserInfoKey", "__Internal")]
        NSString MEMELibResponseUserInfoKey { get; }

        // extern NSString * MEMELibResponseEventCodeUserInfoKey;
        [Field("MEMELibResponseEventCodeUserInfoKey", "__Internal")]
        NSString MEMELibResponseEventCodeUserInfoKey { get; }

        // extern NSString * MEMELibResponseCommandResultUserInfoKey;
        [Field("MEMELibResponseCommandResultUserInfoKey", "__Internal")]
        NSString MEMELibResponseCommandResultUserInfoKey { get; }
    }

    // @protocol MEMELibDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MEMELibDelegate
    {
        // @optional -(void)memePeripheralFound:(CBPeripheral *)peripheral withDeviceAddress:(NSString *)address;
        [Export("memePeripheralFound:withDeviceAddress:")]
        void MemePeripheralFound(CBPeripheral peripheral, string address);

        // @optional -(void)memePeripheralConnected:(CBPeripheral *)peripheral;
        [Export("memePeripheralConnected:")]
        void MemePeripheralConnected(CBPeripheral peripheral);

        // @optional -(void)memePeripheralDisconnected:(CBPeripheral *)peripheral;
        [Export("memePeripheralDisconnected:")]
        void MemePeripheralDisconnected(CBPeripheral peripheral);

        // @optional -(void)memeRealTimeModeDataReceived:(MEMERealTimeData *)data;
        [Export("memeRealTimeModeDataReceived:")]
        void MemeRealTimeModeDataReceived(MEMERealTimeData data);

        // @optional -(void)memeCommandResponse:(MEMEResponse)response;
        [Export("memeCommandResponse:")]
        void MemeCommandResponse(MEMEResponse response);

        // @optional -(void)memeAppAuthorized:(MEMEStatus)status;
        [Export("memeAppAuthorized:")]
        void MemeAppAuthorized(MEMEStatus status);

        // @optional -(void)memeDeviceInfoUpdated;
        [Export("memeDeviceInfoUpdated")]
        void MemeDeviceInfoUpdated();
    }

    // @interface MEMELib : NSObject <CBCentralManagerDelegate, CBPeripheralDelegate>
    [BaseType(typeof(NSObject))]
    interface MEMELib : ICBCentralManagerDelegate, ICBPeripheralDelegate
    {
        [Wrap("WeakDelegate")]
        MEMELibDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<MEMELibDelegate> delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // @property (readonly) BOOL isConnected;
        [Export("isConnected")]
        bool IsConnected { get; }

        // @property (readonly) MEMECalibStatus isCalibrated;
        [Export("isCalibrated")]
        MEMECalibStatus IsCalibrated { get; }

        // @property (readonly) BOOL isDataReceiving;
        [Export("isDataReceiving")]
        bool IsDataReceiving { get; }

        // @property (readonly) BOOL isAutoConnect;
        [Export("isAutoConnect")]
        bool IsAutoConnect { get; }

        // +(MEMELib *)sharedInstance;
        [Static]
        [Export("sharedInstance")]
        // [Verify (MethodToProperty)]
        // MEMELib SharedInstance { get; }
        MEMELib SharedInstance();

        // +(void)setAppClientId:(NSString *)clientId clientSecret:(NSString *)clientSecret;
        [Static]
        [Export("setAppClientId:clientSecret:")]
        void SetAppClientId(string clientId, string clientSecret);

        // -(MEMEStatus)startScanningPeripherals;
        [Export("startScanningPeripherals")]
        // [Verify (MethodToProperty)]
        // MEMEStatus StartScanningPeripherals { get; }
        MEMEStatus StartScanningPeripherals();

        // -(MEMEStatus)stopScanningPeripherals;
        [Export("stopScanningPeripherals")]
        // [Verify (MethodToProperty)]
        // MEMEStatus StopScanningPeripherals { get; }
        MEMEStatus StopScanningPeripherals();

        // -(MEMEStatus)connectPeripheral:(CBPeripheral *)peripheral;
        [Export("connectPeripheral:")]
        MEMEStatus ConnectPeripheral(CBPeripheral peripheral);

        // -(MEMEStatus)disconnectPeripheral;
        [Export("disconnectPeripheral")]
        // [Verify (MethodToProperty)]
        // MEMEStatus DisconnectPeripheral { get; }
        MEMEStatus DisconnectPeripheral();

        // -(NSArray *)getConnectedByOthers;
        [Export("getConnectedByOthers")]
        // [Verify (MethodToProperty), Verify (StronglyTypedNSArray)]
        // NSObject[] ConnectedByOthers { get; }
        CBPeripheral[] ConnectedByOthers();

        // -(void)setAutoConnect:(BOOL)flag;
        [Export("setAutoConnect:")]
        void SetAutoConnect(bool flag);

        // -(MEMEStatus)startDataReport;
        [Export("startDataReport")]
        // [Verify (MethodToProperty)]
        // MEMEStatus StartDataReport { get; }
        MEMEStatus StartDataReport();

        // -(MEMEStatus)stopDataReport;
        [Export("stopDataReport")]
        // [Verify (MethodToProperty)]
        // MEMEStatus StopDataReport { get; }
        MEMEStatus StopDataReport();

        // -(NSString *)getSDKVersion;
        [Export("getSDKVersion")]
        // [Verify (MethodToProperty)]
        // string SDKVersion { get; }]
        string SDKVersion();

        // -(NSString *)getFWVersion;
        [Export("getFWVersion")]
        // [Verify (MethodToProperty)]
        // string FWVersion { get; })]
        string FWVersion();

        // -(UInt8)getHWVersion;
        [Export("getHWVersion")]
        // [Verify (MethodToProperty)]
        // byte HWVersion { get; }
        byte HWVersion();

        // -(int)getConnectedDeviceType;
        [Export("getConnectedDeviceType")]
        // [Verify (MethodToProperty)]
        // int ConnectedDeviceType { get; }
        int ConnectedDeviceType();

        // -(int)getConnectedDeviceSubType;
        [Export("getConnectedDeviceSubType")]
        // [Verify (MethodToProperty)]
        // int ConnectedDeviceSubType { get; }
        int ConnectedDeviceSubType();
    }
}
