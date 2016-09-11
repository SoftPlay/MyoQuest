using System;
using MyoSharp.Communication;
using MyoSharp.Device;
using MyoSharp.Exceptions;

namespace MyoQuest.MyoController
{
	public sealed class MyoDal : IMyoDal
	{
		private IChannel channel;
		private IChannelDriver driver;
		private IChannelBridge bridge;
		private IMyoErrorHandlerBridge errorHandlerBridge;
		private IMyoErrorHandlerDriver errorHandlerDriver;
		private IHub hub;

		private IMyo activeMyo;

		private bool hasDisposed = false;

		public void Initialise()
		{
			this.errorHandlerBridge = MyoErrorHandlerBridge.Create();
			this.errorHandlerDriver = MyoErrorHandlerDriver.Create(this.errorHandlerBridge);

			this.bridge = ChannelBridge.Create();
			this.driver = ChannelDriver.Create(this.bridge, this.errorHandlerDriver);
			this.channel = Channel.Create(this.driver);

			this.hub = Hub.Create(this.channel);

			hub.MyoConnected += (sender, e) =>
			{
				if (this.activeMyo == null)
				{
					Console.WriteLine("Myo {0} has connected!", e.Myo.Handle);
					e.Myo.Vibrate(VibrationType.Short);
					e.Myo.Unlock(UnlockType.Hold);

					this.activeMyo = e.Myo;
				}
			};

			hub.MyoDisconnected += (sender, e) =>
			{
				if (e.Myo.Handle == this.activeMyo.Handle)
				{
					this.activeMyo = null;
				}
			};

			this.channel.StartListening();
		}

		public void Shutdown()
		{
			this.activeMyo?.Lock();
			this.activeMyo = null;
			this.channel.StopListening();
		}

		public void Dispose()
		{
			Dispose(true);
		}

		private void Dispose(bool disposing)
		{
			if (!hasDisposed)
			{
				if (disposing)
				{
					this.hub.Dispose();
					this.channel.Dispose();
				}

				hasDisposed = true;
			}
		}
	}
}
