using ObjCRuntime;

namespace ZendeskiOS
{
	[Native]
	public enum ZDKChatMenuAction : long
	{
		ndChat = 0,
		mailTranscript = 1
	}

	[Native]
	public enum ZDKFormFieldStatus : long
	{
		Required = 0,
		Optional = 1,
		Hidden = 2
	}
}
