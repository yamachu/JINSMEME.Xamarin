# JINSMEME SDK for Xamarin

JINSMEME SDKの非公式ラッパーライブラリ

AndroidおよびiOSのインタフェースをベースにラップした JINSMEME.Native.Android, JINSMEME.Native.iOS と、コールバックをdelegateで置き換えたXamarin.Forms向けの JINSMEME.Forms の3つがあります。

Xamarin.AndroidおよびXamarin.iOSで使用する場合はJINSMEME.Nativeを、Xamarin.Formsを使用する場合はJINSMEME.Formsを使用してください。

バグ報告はIssueか[Twitter](https://twitter.com/y_chu5)まで。

## Usage

JINSMEME SDKの公式ドキュメントは[こちら](https://jins-meme.github.io/sdkdoc/)

本ラッパーのiOS版ではiPhone Simulatorをサポートしていません。

### Native

Native版は公式ドキュメントおよび公式サンプルを参照してください。

JINSMEME.Nativeのサンプルコードはこちら

[Android版のサンプル（SampleApp.Native.Android）](https://github.com/yamachu/JINSMEME.Xamarin/tree/master/SampleApp.Native.Android)

iOS版のサンプル（用意していません [Issue](https://github.com/yamachu/JINSMEME.Xamarin/issues/2)）

### Forms

各プラットフォームで機能を使用する前に `JINSMEME.Forms.MemeLib.Init` を呼んで初期化をしてください。
その後共通コードからJINSMEMEの機能を使用することが出来ます。

Native版との差分として各種イベントのハンドリング方法が変わっています。

例：Realtimeモードで測定データを取得する場合

```cs
MemeLib.RealtimeDataRecieved += (sender, e /* MemeRealtimeData */) =>
{
    Console.WriteLine(JsonConvert.SerializeObject(e, Formatting.Indented));
};
```

の様な形でハンドリングを行います。

その他の使い方については、SampleApp.Forms{,Android,iOS}の3つのプロジェクトを参照してください。

## LICENSE

配布しているnupkgに含まれている公式のライブラリはJINSの許諾を得て再配布しています。

ラッパーライブラリのコード自体のライセンスはMITの形態をとります。
