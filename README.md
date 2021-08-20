# README #

Currently, only Chat is supported so the following instructions focus on that. Should another SDK be required, the same pattern should be followed but some instructions here (especially around the Android dependencies) should be updated accordingly.

Let's get going…

---

## Install Dependencies

From the top level folder run the following script:

```bash
$ ./INSTALL-DEPENDENCIES.sh
```

---

## iOS

### Generate iOS Bindings

1. Open the ZendeskXamariniOSBindings solution
2. Build and run the ZendeskXamariniOSBindings project for whichever Configuration is needed (Release is preferred)
3. Locate the `ZendeskiOS.dll` file and the `ZendeskiOS.resources` folder within `{Configuration}/bin/` folder 
4. Copy both files to the project where there are to be used. It's very important that the `.resources` folder lives in the same location as the `.dll` package.


### Use iOS Bindings

Once the bindings have been produced, add the `.dll` as a native reference to the iOS project. Presenting a chat view controller will then look something like this:

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


### Adding support for another Zendesk iOS SDK

The required surface area for the API is quite slim compared to everything that the SDKs offer. To get messaging with chat for example, we only need a handful of classes and methods. Objective Sharpie can help produce API Definitions but it will often make mistakes and will attempt to output everything that the SDK exposes. So ultimately the definitions are handcrafted, but it's useful to begin by pasting content from the Objective Sharpie output.

To run Objective Sharpie on an SDK:


```bash
$ sharpie bind --sdk=iphoneos14.5 --output="-Definitions/ChatSDK" --namespace="ZendeskiOS" --scope="chat_sdk_ios/ChatSDK.framework/Headers" "chat_sdk_ios/ChatSDK.framework/Headers/ChatSDK-Swift.h"
```

Note: The tool does not produce perfect binding definitions, some adjustments will almost always need to be made. Particularly around `[Protocol]` which seems to require a `[BaseType(typeof(NSObject))]` to work in generated code.


Open the ZendeskXamariniOSBindings solution and check that the definitions in the `ApiDefinitions.cs` and `StructsAndEnums.cs` files are correct for your usage. Any methods or classes you require within the Xamarin project will need to be declared here. Anything that doesn't need exposed can simply be omitted without affecting the core  functionality of the bindings.

For reference:
https://github.com/xamarin/xamarin-macios/issues/6246#issuecomment-825584496


That said, iOS can avail of a 'fat' `.dll` containing all the required frameworks so adding something new is quite simple.

1. Add a new submodule dependency for the xcframework that you want to add (e.g. `git@github.com:zendesk/answer_bot_sdk_ios.git`)
2. Open the ZendeskXamariniOSBindings solution and the `.xcframework` as a Native Reference
3. Add any required public APIs to the bindings through an entry in `ApiDefinitions.cs` (and `StructsAndEnums.cs` if required)
    (You can use Objective Sharpie as described above and/or [use the official reference](https://docs.microsoft.com/en-us/xamarin/cross-platform/macios/binding/binding-types-reference)).
4. Build and run as usual

---

## Android

### Generate Android Bindings

1. Open the ZendeskXamarinAndroid solution
2. Build and run the ZendeskChatAndroidBindings project for whichever Configuration is needed (Release is preferred)
3. Build and run the ZendeskMessagingAndroidBindings project for whichever Configuration is needed (Release is preferred)
4. Locate the required `.dll` files within `{Configuration}/bin/` folder (see below for all required files)
5. Copy all files to the project where there are to be used.


### Use Android Bindings

Once the bindings have been produced, add all the following `.dll` files as native references to the Android project:

- ZendeskChatAndroid
- ZendeskChatProvidersAndroid
- ZendeskCommonUiAndroid
- ZendeskMessagingAndroid
- ZendeskMessagingApiAndroid
- ZendeskSdkConfigurationsAndroid

Now add the required Nuget dependencies:

- Google.Gson
- Softeq.ZendeskBelvedere
- Square.OkHttp3
- Square.OkHttp3.LoggingInterceptor
- Square.Picasso
- Square.Retrofit2
- Square.Retrofit2.ConverterGson
- Xamarin.Google.Dagger
- Xamarin.JavaX.Inject

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


### Adding support for another Zendesk Android SDK

Since it is not possible to compile more than one JAR or AAR into a single binding, a native reference will be required for any API, class or resource that is publicly required to compile and run the required SDK on Android. For this reason, most Zendesk dependencies are packaged as individual binding projects and compiled as dependencies of the main Chat and Messaging bindings. When adding a new SDK, care must be taken to satisify the dependencies of that SDK. The simplest way to achieve that is to inspect the `.pom` file in the Maven repo of the SDK you wish to import, and ensure that each dependency is met.

For Zendesk native AAR or JAR files, a bindings project should be set up within the main Android bindings solution and the JAR / AAR added as a native reference with the `LibraryProjectZip` build action. For any additional dependencies, it's advised to first determine whether the dependency is available via Nuget already or if it requires its own JAR or AAR file to be included. Each case is treated separately:

#### Nuget Available

_Example_: `AndroidX.Annotations`

_Solution_: Add as a nuget dependency (i.e. `PackageReference`) to the bindings project and ensure that the main project in which the binding is intended to be used, also contains a reference to this `PackageReference`

#### JAR (Not required in public API)

_Example:_ `chat-visitor-client.jar`

_Solution_: Download the JAR and add a native reference to the bindings project using the `EmbeddedReferenceJar` build action

#### JAR / AAR (Required in public API)

_Example_: `common-ui.aar`

_Solution_: Create a separate bindings project, adding this as native reference there using the `LibraryProjectZip` build action. Care must be taken to satisfy this dependency's sub-dependencies by inspecting the `.pom` file and adding any other references or Nuget packages. This binding can then be used as a 'reference project' within the binding project where it is required. It will be compiled first and its `.dll` output alongside its parent bindings with the parent's `/bin` folder as expected.


### Adding support for another Zendesk Android SDK (cont.)

Once you have the necessary dependencies in place, the Xamarin bindings generator will automagically produce the C# bindings classes and bundle them alongside the JAR or AAR inside the `dll` artifacts. However, the bindings generator will often need a little guidance. By default it will look at _every_ Java class inside the packages and attempt to create a C# binding class to match. It can hit some issues along the way which can be tricky to solve but as a rule of thumb, _all_ of the output is rarely required: the bindings generator should be assisted with producing only the publicly facing APIs required. The SDK will still function as expected but the public API is greatly reduced, as is the room for error in the bindings.

This guidance primarily is provided through tags in the `Transforms/Metadata.xml` file.

Reference: https://docs.microsoft.com/en-us/xamarin/android/platform/binding-java-library/customizing-bindings/java-bindings-metadata

The most common operation here will be to remove unused notes and reduce the load on the bindings generator, restricting output solely to what is required in the public API. Generally speaking, whole packages that are not required can simply be removed:

```xml
<remove-node path="/api/package[@name='zendesk.messaging.ui']"/>
```

However, where the package is required but many of the classes or methods within are not, are some handy notations for removing these API items by matching predicates. For example:

```xml
<remove-node path="/api/package[@name='zendesk.commonui']/class[contains(@name, 'AlmostRealProgressBar')]"/>
<remove-node path="/api/package[@name='zendesk.messaging']/class[contains(@name, 'Factory')]"/>
<remove-node path="/api/package[@name='zendesk.messaging']/class[starts-with(@name, 'MessagingItem')]"/>
```

Paths to classes and methods can be found by either inspecting the `obj/{Configuration}/api.xml` file (which contains _all_ of the gathered classes, methods and interfaces found by scanning the JAR / AAR, not just those left after `remove-node`), or by finding and opening a class in the `obj/{Configuration}/generated/src/` folder and noticing the trace left by the generator, for example:

```csharp
// Metadata.xml XPath class reference: path="/api/package[@name='zendesk.messaging']/class[@name='MessagingActivity']"
[global::Android.Runtime.Register ("zendesk/messaging/MessagingActivity", DoNotGenerateAcw=true)]
public partial class MessagingActivity : global::AndroidX.AppCompat.App.AppCompatActivity {
}
```

After making any amendments to the `Transforms/Metadata.xml` file, the project will require a Rebuild to apply the latest changes.

For any additional JAR / AAR dependencies, please add these to the `INSTALL-DEPENDENCIES.sh` script to ensure that they are installed when required. Note the entries for `android/*.aar` and `android/*.jar` in the `.gitignore`…




