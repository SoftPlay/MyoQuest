using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyoQuest.Common.Enums;
using MyoSharp.Poses;
using Shouldly;

namespace MyoQuest.MyoController.Tests
{
	[TestClass]
	public class StaticPoseToControllerStateConverterTests
	{
		/// <summary>
		/// Hard-configured to left arm only for now
		/// </summary>
		[TestMethod]
		public void ConvertFrom_WhenPassedWaveInPose_ReturnsRight()
		{
			// Arrange
			var sut = this.CreateSut();

			// Act
			var state = sut.ConvertFrom(Pose.WaveIn);

			// Assert
			state.ShouldBe(ControllerState.Right);
		}

		/// <summary>
		/// Hard-configured to left arm only for now
		/// </summary>
		[TestMethod]
		public void ConvertFrom_WhenPassedWaveOutPose_ReturnsLeft()
		{
			// Arrange
			var sut = this.CreateSut();

			// Act
			var state = sut.ConvertFrom(Pose.WaveOut);

			// Assert
			state.ShouldBe(ControllerState.Left);
		}

		[TestMethod]
		public void ConvertFrom_WhenPassedFistPost_ReturnsForcing()
		{
			// Arrange
			var sut = this.CreateSut();

			// Act
			var state = sut.ConvertFrom(Pose.Fist);

			// Assert
			state.ShouldBe(ControllerState.Forcing);
		}

		[TestMethod]
		public void ConvertFrom_WhenPassedDoubleTapPose_ReturnsUsing()
		{
			// Arrange
			var sut = this.CreateSut();

			// Act
			var state = sut.ConvertFrom(Pose.DoubleTap);

			// Assert
			state.ShouldBe(ControllerState.Using);
		}

		[TestMethod]
		public void ConvertFrom_WhenPassedFingerSpreadPose_ReturnsInspecting()
		{
			// Arrange
			var sut = this.CreateSut();

			// Act
			var state = sut.ConvertFrom(Pose.FingersSpread);

			// Assert
			state.ShouldBe(ControllerState.Inspecting);
		}

		[TestMethod]
		public void ConvertFrom_WhenPassedRestPose_ReturnsNoState()
		{
			// Arrange
			var sut = this.CreateSut();

			// Act
			var state = sut.ConvertFrom(Pose.Rest);

			// Assert
			state.ShouldBe(ControllerState.NoState);
		}

		[TestMethod]
		public void ConvertFrom_WhenPassedUnknownPose_ReturnsNoState()
		{
			// Arrange
			var sut = this.CreateSut();

			// Act
			var state = sut.ConvertFrom(Pose.Unknown);

			// Assert
			state.ShouldBe(ControllerState.NoState);
		}

		[TestMethod]
		public void ConvertFrom_WhenPassedUndefinedPose_ReturnsNoState()
		{
			// Arrange
			var sut = this.CreateSut();

			// Act
			var state = sut.ConvertFrom((Pose)80808); // Undefined value in the Pose enum.

			// Assert
			state.ShouldBe(ControllerState.NoState);
		}

		private StaticPoseToControllerStateConverter CreateSut()
		{
			return new StaticPoseToControllerStateConverter();
		}
	}
}
