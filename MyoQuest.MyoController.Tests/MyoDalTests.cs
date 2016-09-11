using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyoSharp.Communication;
using MyoSharp.Device;
using MyoSharp.Exceptions;

namespace MyoQuest.MyoController.Tests
{
	[TestClass]
	public sealed class MyoDalTests
	{
		private readonly Mock<IMyoObjectFactory> mockObjectFactory = new Mock<IMyoObjectFactory>();

		private readonly Mock<IMyoErrorHandlerBridge> mockMyoErrorHandlerBridge = new Mock<IMyoErrorHandlerBridge>();
		private readonly Mock<IMyoErrorHandlerDriver> mockMyoErrorHandlerDriver = new Mock<IMyoErrorHandlerDriver>();
		private readonly Mock<IChannelBridge> mockChannelBridge = new Mock<IChannelBridge>();
		private readonly Mock<IChannelDriver> mockChannelDriver = new Mock<IChannelDriver>();
		private readonly Mock<IChannel> mockChannel = new Mock<IChannel>();
		private readonly Mock<IHub> mockHub = new Mock<IHub>();

		[TestInitialize]
		public void InitialiseHarness()
		{
			// Set up the factory to return all objects properly if they are constructed appropriately with the creation.
			// Inflexible, but bite me
			this.mockObjectFactory
				.Setup(x => x.CreateMyoErrorHandlerBridge())
				.Returns(this.mockMyoErrorHandlerBridge.Object);
			this.mockObjectFactory
				.Setup(x => x.CreateMyoErrorHandlerDriver(this.mockMyoErrorHandlerBridge.Object))
				.Returns(this.mockMyoErrorHandlerDriver.Object);
			this.mockObjectFactory
				.Setup(x => x.CreateChannelBridge())
				.Returns(this.mockChannelBridge.Object);
			this.mockObjectFactory
				.Setup(x => x.CreateChannelDriver(this.mockChannelBridge.Object, this.mockMyoErrorHandlerDriver.Object))
				.Returns(this.mockChannelDriver.Object);
			this.mockObjectFactory
				.Setup(x => x.CreateChannel(this.mockChannelDriver.Object))
				.Returns(this.mockChannel.Object);
			this.mockObjectFactory
				.Setup(x => x.CreateHub(this.mockChannel.Object))
				.Returns(this.mockHub.Object);
		}

		[TestMethod]
		public void Initialise_Always_CreatesMyoObjectsExpectedly()
		{
			// Arrange
			var sut = this.CreateSut();

			// Act
			sut.Initialise();

			// Assert
			this.mockObjectFactory.Verify(x => x.CreateMyoErrorHandlerBridge(), Times.Once);
			this.mockObjectFactory.Verify(x => x.CreateMyoErrorHandlerDriver(this.mockMyoErrorHandlerBridge.Object), Times.Once);
			this.mockObjectFactory.Verify(x => x.CreateChannelBridge(), Times.Once);
			this.mockObjectFactory.Verify(x => x.CreateChannelDriver(this.mockChannelBridge.Object, this.mockMyoErrorHandlerDriver.Object), Times.Once);
			this.mockObjectFactory.Verify(x => x.CreateChannel(this.mockChannelDriver.Object));
		}

		[TestMethod]
		public void Initialise_Always_CallsChannelStartListening()
		{
			// Arrange
			var sut = this.CreateSut();

			// Act
			sut.Initialise();

			// Assert
			this.mockChannel.Verify(x => x.StartListening(), Times.Once);
		}

		[TestMethod]
		public void Shutdown_AfterInitialised_CallsChannelStopListening()
		{
			// Arrange
			var sut = this.CreateSut();
			sut.Initialise();

			// Act
			sut.Shutdown();

			// Assert
			this.mockChannel.Verify(x => x.StopListening(), Times.Once);
		}

		[TestMethod]
		public void Shutdown_InitialisedAndWithConnectedMyo_CallsMyoLock()
		{
			// Arrange
			var sut = this.CreateSut();
			sut.Initialise();

			var mockMyo = new Mock<IMyo>();
			this.mockHub.Raise(x => x.MyoConnected += null, new MyoEventArgs(mockMyo.Object, DateTime.MinValue));

			// Act
			sut.Shutdown();

			// Assert
			mockMyo.Verify(x => x.Lock(), Times.Once);
		}

		[TestMethod]
		public void MyoDal_InitialisedAndWhenHubRaisesMyoConnected_CallsMyoVibrateWithShortVibrationType()
		{
			// Arrange
			var sut = this.CreateSut();
			sut.Initialise();

			var mockMyo = new Mock<IMyo>();

			// Act
			this.mockHub.Raise(x => x.MyoConnected += null, new MyoEventArgs(mockMyo.Object, DateTime.MinValue));

			// Assert
			mockMyo.Verify(x => x.Vibrate(VibrationType.Short), Times.Once);
		}

		[TestMethod]
		public void MyoDal_InitialisedAndWhenHubRaisesMyoConnected_CallsMyoUnlockWithHoldUnlockType()
		{
			// Arrange
			var sut = this.CreateSut();
			sut.Initialise();

			var mockMyo = new Mock<IMyo>();

			// Act
			this.mockHub.Raise(x => x.MyoConnected += null, new MyoEventArgs(mockMyo.Object, DateTime.MinValue));

			// Assert
			mockMyo.Verify(x => x.Unlock(UnlockType.Hold), Times.Once);
		}

		[TestMethod]
		public void MyoDal_InitialisedAndWithConnectedMyoWhenAnotherMyoConnects_IgnoresSecondMyo()
		{
			// Arrange
			var sut = this.CreateSut();
			sut.Initialise();

			var mockMyo = new Mock<IMyo>();
			this.mockHub.Raise(x => x.MyoConnected += null, new MyoEventArgs(mockMyo.Object, DateTime.MinValue));

			var mockSecondMyo = new Mock<IMyo>();

			// Act
			this.mockHub.Raise(x => x.MyoConnected += null, new MyoEventArgs(mockSecondMyo.Object, DateTime.MinValue));

			// Assert
			mockSecondMyo.Verify(x => x.Vibrate(It.IsAny<VibrationType>()), Times.Never);
			mockSecondMyo.Verify(x => x.Unlock(It.IsAny<UnlockType>()), Times.Never);
		}

		[TestMethod]
		public void MyoDal_InitialisedWithConnectedMyoWhenThatMyoDisconnects_AllowsAnotherMyoToConnect()
		{
			// Arrange
			var sut = this.CreateSut();
			sut.Initialise();

			var mockMyo = new Mock<IMyo>();
			this.mockHub.Raise(x => x.MyoConnected += null, new MyoEventArgs(mockMyo.Object, DateTime.MinValue));

			var mockSecondMyo = new Mock<IMyo>();

			// Act
			this.mockHub.Raise(x => x.MyoDisconnected += null, new MyoEventArgs(mockMyo.Object, DateTime.MinValue));
			this.mockHub.Raise(x => x.MyoConnected += null, new MyoEventArgs(mockSecondMyo.Object, DateTime.MinValue));

			// Assert
			mockSecondMyo.Verify(x => x.Vibrate(It.IsAny<VibrationType>()), Times.Once);
			mockSecondMyo.Verify(x => x.Unlock(It.IsAny<UnlockType>()), Times.Once);
		}

		private MyoDal CreateSut()
		{
			return new MyoDal(
				this.mockObjectFactory.Object);
		}

	}
}
