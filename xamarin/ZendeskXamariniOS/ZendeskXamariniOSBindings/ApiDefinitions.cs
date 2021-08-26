using System;
using CoreFoundation;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace ZendeskiOS {

	[BaseType (typeof (NSObject))]
	[Model, Protocol]
	interface ZDKEngine {
	}

	// @interface ZDKChatEngine : NSObject
	[BaseType(typeof(ZDKEngine))]
	[DisableDefaultCtor]
	interface ZDKChatEngine {
		// -(void)isConversationOngoing:(void (^ _Nonnull)(BOOL))completion;
		[Export("isConversationOngoing:")]
		void IsConversationOngoing(Action<bool> completion);

		// +(ZDKChatEngine * _Nullable)engineAndReturnError:(NSError * _Nullable * _Nullable)error __attribute__((warn_unused_result("")));
		[Static]
		[Export("engineAndReturnError:")]
		[return: NullAllowed]
		ZDKChatEngine EngineAndReturnError([NullAllowed] out NSError error);
	}

	// @interface ZDKChat : NSObject
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface ZDKChat {
		// +(void)initializeWithAccountKey:(NSString * _Nonnull)accountKey appId:(NSString * _Nullable)appId queue:(dispatch_queue_t _Nonnull)queue;
		[Static]
		[Export("initializeWithAccountKey:appId:queue:")]
		void InitializeWithAccountKey(string accountKey, [NullAllowed] string appId, DispatchQueue queue);

		// +(void)initializeWithAccountKey:(NSString * _Nonnull)accountKey queue:(dispatch_queue_t _Nonnull)queue;
		[Static]
		[Export("initializeWithAccountKey:queue:")]
		void InitializeWithAccountKey(string accountKey, DispatchQueue queue);
	}

	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface ZDKMessaging {
		// @property (readonly, nonatomic, strong, class) ZDKMessaging * _Nonnull instance;
		[Static]
		[Export("instance", ArgumentSemantic.Strong)]
		ZDKMessaging Instance { get; }

		// @property (readonly, nonatomic) BOOL isMessagingPresented;
		[Export("isMessagingPresented")]
		bool IsMessagingPresented { get; }

		// -(UIViewController * _Nullable)buildUIWithEngines:(NSArray<id<ZDKEngine>> * _Nonnull)engines configs:(NSArray<id<ZDKConfiguration>> * _Nonnull)configs error:(NSError * _Nullable * _Nullable)error __attribute__((warn_unused_result("")));
		[Export("buildUIWithEngines:configs:error:")]
		[return: NullAllowed]
		UIViewController BuildUIWithEngines(ZDKEngine[] engines, ZDKConfiguration[] configs, [NullAllowed] out NSError error);
	}

	// @interface ZDKMessagingConfiguration : NSObject <ZDKConfiguration>
	[BaseType(typeof(ZDKConfiguration))]
	interface ZDKMessagingConfiguration {
	}

	[Protocol]
	[BaseType(typeof(NSObject))]
	interface ZDKConfiguration {
	}
}
