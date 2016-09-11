using System;
using MyoSharp.Communication;
using MyoSharp.Device;
using MyoSharp.Exceptions;

namespace MyoQuest.MyoController
{
	public sealed class MyoDal : IMyoDal
	{
		private readonly IMyoObjectFactory myoObjectFactory;

		private IChannel channel;
		private IChannelDriver driver;
		private IChannelBridge bridge;
		private IMyoErrorHandlerBridge errorHandlerBridge;
		private IMyoErrorHandlerDriver errorHandlerDriver;
		private IHub hub;

		private IMyo activeMyo;

		private bool hasDisposed = false;

		public MyoDal(IMyoObjectFactory objectFactory)
		{
			this.myoObjectFactory = objectFactory;
		}

		public void Initialise()
		{
			this.CreateMyoObjects();

			hub.MyoConnected += this.Hub_MyoConnected;
			hub.MyoDisconnected += this.Hub_MyoDisconnected;

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

		private void CreateMyoObjects()
		{
			this.errorHandlerBridge = this.myoObjectFactory.CreateMyoErrorHandlerBridge();
			this.errorHandlerDriver = this.myoObjectFactory.CreateMyoErrorHandlerDriver(this.errorHandlerBridge);
			this.bridge = this.myoObjectFactory.CreateChannelBridge();
			this.driver = this.myoObjectFactory.CreateChannelDriver(this.bridge, this.errorHandlerDriver);
			this.channel = this.myoObjectFactory.CreateChannel(this.driver);
			this.hub = this.myoObjectFactory.CreateHub(this.channel);
		}

		private void Hub_MyoConnected(object sender, MyoEventArgs e)
		{
			if (this.activeMyo == null)
			{
				Console.WriteLine("Myo {0} has connected!", e.Myo.Handle);
				e.Myo.Vibrate(VibrationType.Short);
				e.Myo.Unlock(UnlockType.Hold);

				this.activeMyo = e.Myo;
			}
		}

		private void Hub_MyoDisconnected(object sender, MyoEventArgs e)
		{
			if (e.Myo.Handle == this.activeMyo.Handle)
			{
				this.activeMyo = null;
			}
		}
	}
}
