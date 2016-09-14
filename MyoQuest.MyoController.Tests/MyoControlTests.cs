using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyoQuest.Common.Enums;
using MyoSharp.Poses;
using Shouldly;

namespace MyoQuest.MyoController.Tests
{
	[TestClass]
	public class MyoControlTests
	{
		private readonly Mock<IMyoDal> mockMyoDal = new Mock<IMyoDal>();
		private readonly Mock<IPoseToControllerStateConverter> mockPoseConverter = new Mock<IPoseToControllerStateConverter>();

		[TestMethod]
		public void CurrentPose_WhenDalRaisesPoseChangedEvent_UsesValueFromConverter()
		{
			// Arrange
			var sut = this.CreateSut();

			var raisedPose = Pose.Fist;
			var convertedState = ControllerState.Forcing;
			this.mockPoseConverter.Setup(x => x.ConvertFrom(raisedPose)).Returns(convertedState);

			// Act
			this.mockMyoDal.Raise(x => x.PoseChanged += null, new NewPoseEventArgs(raisedPose));

			// Assert
			sut.CurrentState.ShouldBe(convertedState);
		}

		private MyoControl CreateSut()
		{
			return new MyoControl(
				this.mockMyoDal.Object,
				this.mockPoseConverter.Object);
		}
	}
}
