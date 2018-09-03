using ObjCRuntime;

[assembly: LinkWith("MEMELib.framework", Frameworks = "Foundation CoreBluetooth CoreGraphics MobileCoreServices Security SystemConfiguration UIKit", IsCxx = true, SmartLink = true, ForceLoad = true)]
