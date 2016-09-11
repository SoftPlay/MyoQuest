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
			throw new NotImplementedException();
		}

		public IChannelBridge CreateChannelBridge()
		{
			throw new NotImplementedException();
		}

		public IChannelDriver CreateChannelDriver(IChannelBridge bridge, IMyoErrorHandlerDriver errorHandlerDriver)
		{
			throw new NotImplementedException();
		}

		public IHub CreateHub(IChannel channel)
		{
			throw new NotImplementedException();
		}

		public IMyoErrorHandlerBridge CreateMyoErrorHandlerBridge()
		{
			throw new NotImplementedException();
		}

		public IMyoErrorHandlerDriver CreateMyoErrorHandlerDriver(IMyoErrorHandlerBridge bridge)
		{
			throw new NotImplementedException();
		}
	}
}
