# README #

Mostly to be written…

### Get SDKs

```bash
$ git submodule update --init --recursive
```

### Generate iOS Bindings

We use Objective Sharpie from top level dir

```bash
$ sharpie bind --sdk=iphoneos14.5 --output="XamarinZendeskDefinition" --namespace="ZendeskMessaging" --scope="apple/build/frameworks/iOS/ZendeskiOS.framework/Headers/ZendeskiOS.h"
```