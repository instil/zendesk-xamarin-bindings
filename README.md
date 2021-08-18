# README #

## Install Dependencies

From the top level folder run the following script:

```bash
$ ./INSTALL-DEPENDENCIES.sh
```

## iOS

### Generate iOS Bindings

The required surface area for the API is quite slim compared to everything that the SDKs offer. To get messaging with chat for example, we only need a handful of classes and methods. Objective Sharpie can help produce API Definitions but it will often make mistakes and will attempt to output everything that the SDK exposes. So ultimately the definitions are handcrafted, but it's useful to begin by pasting content from the Objective Sharpie output.

To run Objective Sharpie on an SDK:


```bash
$ sharpie bind --sdk=iphoneos14.5 --output="-Definitions/ChatSDK" --namespace="ZendeskiOS" --scope="chat_sdk_ios/ChatSDK.framework/Headers" "chat_sdk_ios/ChatSDK.framework/Headers/ChatSDK-Swift.h"
```

The tool does not produce perfect binding definitions, some adjustments will almost always need to be made. Particularly around `[Protocol]` which seems to require a `[BaseType(typeof(NSObject))]` to work in generated code.


Open the ZendeskXamarinBindings solution and check that the definitions in the `ApiDefinitions.cs` and `StructsAndEnums.cs` files are correct for your usage. Then build & run. The output will be produced within the projects `/bin` folder

Once bindings are generated, locate the `ZendeskiOS.dll` file and the `ZendeskiOS.resources` folder within `/bin` and copy them to the project where there are to be used. It's very important that the `.resources` folder lives in the same location as the `.dll` package.

For reference:
https://github.com/xamarin/xamarin-macios/issues/6246#issuecomment-825584496


### Use iOS Bindings

Once the bindings have been produced, add them as a native reference to the iOS project. Presenting a chat view controller will then look something like this:

```csharp

using ZendeskiOS;

void ShowChat() {
    var configuration = new ZDKMessagingConfiguration();
    var chatEngine = ZDKChatEngine.EngineAndReturnError(out var engineError);
    if (engineError != null) {
        Console.WriteLine($"Zendesk engine error: {engineError.LocalizedDescription}");
        return;
    }

    var viewController = ZDKMessaging.Instance.BuildUIWithEngines(new ZDKEngine[] { chatEngine }, new ZDKConfiguration[] { configuration }, out var buildError);
    if (buildError != null || viewController == null) {
        Console.WriteLine($"Zendesk engine error: {buildError?.LocalizedDescription}");
        return;
    }

    PresentViewController(viewController, true, null);
}
```

For reference:
https://developer.zendesk.com/documentation/classic-sdks/unified-sdk/ios/chat_engine/


## Android


### Use Android Bindings

Once the bindings have been produced, add all the following libraries as native references to the Android project:

- ZendeskChatAndroid
- ZendeskChatProvidersAndroid
- ZendeskCommonUiAndroid
- ZendeskMessagingAndroid
- ZendeskMessagingApiAndroid
- ZendeskSdkConfigurationsAndroid

 Presenting a chat will then look something like this:

```csharp

using Zendesk.Chat;
using Zendesk.Messaging;

void ShowChat() {
    MessagingActivity.Builder()
        .WithEngines(ChatEngine.Engine())
        .Show(this);
}

 ```

 For reference: 
 https://developer.zendesk.com/documentation/classic-sdks/unified-sdk/android/chat_engine/
