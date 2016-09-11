using System;
using MyoSharp.Communication;
using MyoSharp.Device;
using MyoSharp.Exceptions;

namespace MyoQuest.MyoController
{
	public sealed class MyoObjectFactory : IMyoObjectFactory
	{
		public IChannel CreateChannel(IChannelDriver driver)
		{
			if (driver == null)
			{
				throw new ArgumentNullException("driver");
			}

			return Channel.Create(driver);
		}

		public IChannelBridge CreateChannelBridge()
		{
			return ChannelBridge.Create();
		}

		public IChannelDriver CreateChannelDriver(IChannelBridge bridge, IMyoErrorHandlerDriver errorHandlerDriver)
		{
			if (bridge == null)
			{
				throw new ArgumentNullException("bridge");
			}
			else if (errorHandlerDriver == null)
			{
				throw new ArgumentNullException("errorHandlerDriver");
			}

			return ChannelDriver.Create(bridge, errorHandlerDriver);
		}

		public IHub CreateHub(IChannel channel)
		{
			if (channel == null)
			{
				throw new ArgumentNullException("channel");
			}

			return Hub.Create(channel);
		}

		public IMyoErrorHandlerBridge CreateMyoErrorHandlerBridge()
		{
			return MyoErrorHandlerBridge.Create();
		}

		public IMyoErrorHandlerDriver CreateMyoErrorHandlerDriver(IMyoErrorHandlerBridge bridge)
		{
			if (bridge == null)
			{
				throw new ArgumentNullException("bridge");
			}

			return MyoErrorHandlerDriver.Create(bridge);
		}
	}
}
