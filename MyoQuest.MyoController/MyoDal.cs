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

		/// <summary>
		/// Note: This class only ever handles a single Myo device. No intent to make this flexible for this project.
		/// </summary>
		private IMyo activeMyo;

		private bool hasDisposed = false;

		public MyoDal(IMyoObjectFactory objectFactory)
		{
			this.myoObjectFactory = objectFactory;
		}

		public event EventHandler<NewPoseEventArgs> PoseChanged;

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
				this.activeMyo = e.Myo;

				Console.WriteLine("Myo {0} has connected!", this.activeMyo.Handle);
				this.activeMyo.Vibrate(VibrationType.Short);
				this.activeMyo.Unlock(UnlockType.Hold);

				this.activeMyo.PoseChanged += this.ActiveMyo_PoseChanged;
			}
		}

		private void Hub_MyoDisconnected(object sender, MyoEventArgs e)
		{
			if (e.Myo.Handle == this.activeMyo.Handle)
			{
				this.activeMyo.PoseChanged -= this.ActiveMyo_PoseChanged;
				this.activeMyo = null;
			}
		}

		private void ActiveMyo_PoseChanged(object sender, PoseEventArgs e)
		{
			this.PoseChanged?.Invoke(sender, new NewPoseEventArgs(e.Pose));
		}
	}
}
