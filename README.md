# README #

Mostly to be written…

### Get SDKs

```bash
$ git submodule update --init --recursive
```

### Generate iOS Bindings

We use Objective Sharpie

```bash
$ sharpie bind --sdk=iphoneos14.5 --output="XamarinApiDef" --namespace="Binding" --scope="sdk_messaging_ios/ZendeskSDKMessaging.framework/Headers/" "sdk_messaging_ios/ZendeskSDKMessaging.framework/Headers/ZendeskSDKMessaging-Swift.h"
```