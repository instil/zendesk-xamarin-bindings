# README #

## Install Dependencies

From the top level folder run the following script:

```bash
$ ./INSTALL-DEPENDENCIES.sh
```

## iOS

### Generate iOS Bindings

We use Objective Sharpie from top level dir

```bash
$ sharpie bind --sdk=iphoneos14.5 --output="-Definitions/ChatSDK" --namespace="ZendeskiOS" --scope="chat_sdk_ios/ChatSDK.framework/Headers" "chat_sdk_ios/ChatSDK.framework/Headers/ChatSDK-Swift.h"

$ sharpie bind --sdk=iphoneos14.5 --output="Definitions/ChatProvidersSDK" --namespace="ZendeskiOS" --scope="chat_providers_sdk_ios/ChatProvidersSDK.framework/Headers" "chat_providers_sdk_ios/ChatProvidersSDK.framework/Headers/ChatProvidersSDK-Swift.h"

$ sharpie bind --sdk=iphoneos14.5 --output="Definitions/MessagingSDK" --namespace="ZendeskiOS" --scope="messaging_sdk_ios/MessagingSDK.framework/Headers" "messaging_sdk_ios/MessagingSDK.framework/Headers/MessagingSDK-Swift.h"

$ sharpie bind --sdk=iphoneos14.5 --output="Definitions/MessagingAPI" --namespace="ZendeskiOS" --scope="messagingapi_sdk_ios/MessagingAPI.framework/Headers" "messagingapi_sdk_ios/MessagingAPI.framework/Headers/MessagingAPI-Swift.h"

$ sharpie bind --sdk=iphoneos14.5 --output="Definitions/SDKConfigurations" --namespace="ZendeskiOS" --scope="sdkconfigurations_sdk_ios/SDKConfigurations.framework/Headers" "sdkconfigurations_sdk_ios/SDKConfigurations.framework/Headers/SDKConfigurations-Swift.h"

$ sharpie bind --sdk=iphoneos14.5 --output="Definitions/CommonUISDK" --namespace="ZendeskiOS" --scope="commonui_sdk_ios/CommonUISDK.framework/Headers" "commonui_sdk_ios/CommonUISDK.framework/Headers/CommonUISDK-Swift.h"
```

Currently the process from here is quite manual unfortunately. We need to copy and paste the contents of each of the generated `ApiDefinitions.cs` and `StructsAndEnums.cs` files into the main files included in the bindings project. Some parts will not compile and are safe to remove such as all `IsEqual`, `DebugDescription` and `Description` methods. And the Swift categories are safe to add into their main class definitions.

However, with the main interfaces in place for these packages, it's unlikely to be necessary to run `sharpie` again here. Instead, consider manually adding / adjusting the definitions.

If other SDK packages need to be supported, then `sharpie` can help produce the binding definitions.

Objective Sharpie does not produce perfect binding definitions, some adjustments will almost always need to be made. Particularly around `[Protocol]` which seems to require a `[BaseType(typeof(NSObject))]` to work in generated code.


Open the bindings project and check that the definitions are correct for your usage. Then build & run. The output will be produced within the projects `/bin` folder

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
