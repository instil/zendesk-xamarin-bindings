using ObjCRuntime;

namespace ZendeskiOS
{
	[Native]
	public enum ZDKApplicationEvent : long
	{
		WillEnterForeground = 0,
		DidEnterBackground = 1
	}
}
