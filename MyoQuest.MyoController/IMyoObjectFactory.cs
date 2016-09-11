using MyoSharp.Communication;
using MyoSharp.Device;
using MyoSharp.Exceptions;

namespace MyoQuest.MyoController
{
	/// <summary>
	/// Wraps the static method calls for creation in  
	/// </summary>
	public interface IMyoObjectFactory
	{
		IMyoErrorHandlerBridge CreateMyoErrorHandlerBridge();
		IMyoErrorHandlerDriver CreateMyoErrorHandlerDriver(IMyoErrorHandlerBridge bridge);

		IChannelBridge CreateChannelBridge();
		IChannelDriver CreateChannelDriver(IChannelBridge bridge, IMyoErrorHandlerDriver errorHandlerDriver);
		IChannel CreateChannel(IChannelDriver driver);

		IHub CreateHub(IChannel channel);
	}
}
