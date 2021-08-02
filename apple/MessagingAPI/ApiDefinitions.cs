using System;
using Foundation;

namespace ZendeskiOS
{
	// @interface ZDKObservationToken : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface ZDKObservationToken
	{
		// -(void)cancel;
		[Export ("cancel")]
		void Cancel ();

		// -(BOOL)isEqual:(id _Nullable)object __attribute__((warn_unused_result("")));
		[Export ("isEqual:")]
		bool IsEqual ([NullAllowed] NSObject @object);
	}

	// @interface ZDKTransferOptionDescription : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface ZDKTransferOptionDescription
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull engineId;
		[Export ("engineId")]
		string EngineId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull displayName;
		[Export ("displayName")]
		string DisplayName { get; }

		// -(instancetype _Nonnull)initWithEngineId:(NSString * _Nonnull)engineId displayName:(NSString * _Nonnull)displayName __attribute__((objc_designated_initializer));
		[Export ("initWithEngineId:displayName:")]
		[DesignatedInitializer]
		IntPtr Constructor (string engineId, string displayName);

		// -(BOOL)isEqual:(id _Nullable)object __attribute__((warn_unused_result("")));
		[Export ("isEqual:")]
		bool IsEqual ([NullAllowed] NSObject @object);
	}

	// @protocol ZDKEngine
	/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol (Name = "_TtP12MessagingAPI9ZDKEngine_")]
	interface ZDKEngine
	{
	}
}
