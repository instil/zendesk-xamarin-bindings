# README #

Mostly to be written…

### Get SDKs

```bash
$ git submodule update --init --recursive
```

### Generate iOS Bindings

https://github.com/xamarin/xamarin-macios/issues/6246#issuecomment-825584496

We use Objective Sharpie from top level dir

```bash
$ sharpie bind --sdk=iphoneos14.5 --output="ChatSDK" --namespace="ZendeskiOS" --scope="build/frameworks/iOS/ChatSDK.framework/Headers" "build/frameworks/iOS/ChatSDK.framework/Headers/ChatSDK-Swift.h"

$ sharpie bind --sdk=iphoneos14.5 --output="ChatProvidersSDK" --namespace="ZendeskiOS" --scope="build/frameworks/iOS/ChatProvidersSDK.framework/Headers" "build/frameworks/iOS/ChatProvidersSDK.framework/Headers/ChatProvidersSDK-Swift.h"

$ sharpie bind --sdk=iphoneos14.5 --output="MessagingSDK" --namespace="ZendeskiOS" --scope="build/frameworks/iOS/MessagingSDK.framework/Headers" "build/frameworks/iOS/MessagingSDK.framework/Headers/MessagingSDK-Swift.h"

$ sharpie bind --sdk=iphoneos14.5 --output="MessagingAPI" --namespace="ZendeskiOS" --scope="build/frameworks/iOS/MessagingAPI.framework/Headers" "build/frameworks/iOS/MessagingAPI.framework/Headers/MessagingAPI-Swift.h"

$ sharpie bind --sdk=iphoneos14.5 --output="SDKConfigurations" --namespace="ZendeskiOS" --scope="build/frameworks/iOS/SDKConfigurations.framework/Headers" "build/frameworks/iOS/SDKConfigurations.framework/Headers/SDKConfigurations-Swift.h"

$ sharpie bind --sdk=iphoneos14.5 --output="CommonUISDK" --namespace="ZendeskiOS" --scope="build/frameworks/iOS/CommonUISDK.framework/Headers" "build/frameworks/iOS/CommonUISDK.framework/Headers/CommonUISDK-Swift.h"
```

Currently the process from here is quite manual unfortunately. We need to copy and paste the contents of each of the generated `ApiDefinitions.cs` and `StructsAndEnums.cs` files into the main files included in the bindings project. Some parts will not compile and are safe to remove such as all `IsEqual`, `DebugDescription` and `Description` methods. And the Swift categories are safe to add into their main class definitions.

However, with the main interfaces in place for these packages, it's unlikely to be necessary to run `sharpie` again here. Instead, consider manually adding / adjusting the definitions.

If other SDK packages need to be supported, then `sharpie` can help produce the binding definitions.

Objective Sharpie does not produce perfect binding definitions, some adjustments will almost always need to be made. Particularly around `[Protocol]` which seems to require a `[BaseType(typeof(NSObject))]` to work in generated code.



Once bindings are generated, locate the `ZendeskiOS.dll` file and the `ZendeskiOS.resources` folder and copy them to the project where there are to be used. It's very important that the `.resources` folder lives in the same location as the `.dll` package.


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