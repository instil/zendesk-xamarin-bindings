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
		// @property (readonly, nonatomic, strong, class) ZDKChat * _Nullable instance;
		[Static]
		[NullAllowed, Export("instance", ArgumentSemantic.Strong)]
		ZDKChat Instance { get; }

		// +(void)initializeWithAccountKey:(NSString * _Nonnull)accountKey appId:(NSString * _Nullable)appId queue:(dispatch_queue_t _Nonnull)queue;
		[Static]
		[Export("initializeWithAccountKey:appId:queue:")]
		void InitializeWithAccountKey(string accountKey, [NullAllowed] string appId, DispatchQueue queue);

		// +(void)initializeWithAccountKey:(NSString * _Nonnull)accountKey queue:(dispatch_queue_t _Nonnull)queue;
		[Static]
		[Export("initializeWithAccountKey:queue:")]
		void InitializeWithAccountKey(string accountKey, DispatchQueue queue);

		// -(void)setIdentityWithAuthenticator:(id<ZDKJWTAuthenticator> _Nonnull)authenticator;
		[Export("setIdentityWithAuthenticator:")]
		void SetIdentityWithAuthenticator(ZDKJWTAuthenticator authenticator);
		
		// -(void)resetIdentity:(void (^ _Nullable)(void))completion;
		[Export ("resetIdentity:")]
		void ResetIdentity ([NullAllowed] Action completion);
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
		// @property (copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; set; }

		// @property (nonatomic, strong) UIImage * _Nonnull botAvatar;
		[Export ("botAvatar", ArgumentSemantic.Strong)]
		UIImage BotAvatar { get; set; }

		// @property (nonatomic) BOOL isMultilineResponseOptionsEnabled;
		[Export ("isMultilineResponseOptionsEnabled")]
		bool IsMultilineResponseOptionsEnabled { get; set; }
	}

	[Protocol]
	[BaseType(typeof(NSObject))]
	interface ZDKConfiguration {
	}

	[Model, Protocol]
	[BaseType(typeof(NSObject))]
	interface ZDKJWTAuthenticator {
		// @required -(void)getToken:(void (^ _Nonnull)(NSString * _Nullable, NSError * _Nullable))completion;
		[Abstract]
		[Export("getToken:")]
		void GetToken(Action<NSString, NSError> completion);
	}
}