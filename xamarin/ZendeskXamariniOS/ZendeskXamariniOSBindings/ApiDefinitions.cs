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

	// @interface ZDKConfigurations : NSObject
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface ZDKConfigurations {
		// @property (readonly, copy, nonatomic) NSArray<id<ZDKConfiguration>> * _Nonnull configs;
		[Export("configs", ArgumentSemantic.Copy)]
		ZDKConfiguration[] Configs { get; }

		// -(instancetype _Nonnull)initWithConfigs:(NSArray<id<ZDKConfiguration>> * _Nonnull)configs __attribute__((objc_designated_initializer));
		[Export("initWithConfigs:")]
		[DesignatedInitializer]
		IntPtr Constructor(ZDKConfiguration[] configs);

		[Export("startIndex")]
		nint StartIndex { get; }

		// @property (readonly, nonatomic) NSInteger endIndex;
		[Export("endIndex")]
		nint EndIndex { get; }

		// -(void)insert:(id<ZDKConfiguration> _Nonnull)configuration;
		[Export("insert:")]
		void Insert(ZDKConfiguration configuration);

		// -(id<ZDKConfiguration> _Nonnull)objectAtIndexedSubscript:(NSInteger)index __attribute__((warn_unused_result("")));
		[Export("objectAtIndexedSubscript:")]
		ZDKConfiguration ObjectAtIndexedSubscript(nint index);

		// -(void)addDefaultConfigIfNeeded:(id<ZDKConfiguration> _Nonnull)config;
		[Export("addDefaultConfigIfNeeded:")]
		void AddDefaultConfigIfNeeded(ZDKConfiguration config);
	}
}
