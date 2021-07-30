using ObjCRuntime;

namespace Binding
{
	[Native]
	public enum ZDKPushResponsibility : long
	{
		MessagingShouldDisplay = 0,
		MessagingShouldNotDisplay = 1,
		NotFromMessaging = 2
	}

	[Native]
	public enum ZDKURLSource : long
	{
		Text = 0,
		Carousel = 1,
		File = 2,
		Image = 3
	}
}
