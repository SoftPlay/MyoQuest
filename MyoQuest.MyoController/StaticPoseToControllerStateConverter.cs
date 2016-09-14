using System;
using MyoQuest.Common.Enums;
using MyoSharp.Poses;

namespace MyoQuest.MyoController
{
	/// <summary>
	/// Designed as a hard-coded mapping of pose-to-action.
	/// </summary>
	public sealed class StaticPoseToControllerStateConverter : IPoseToControllerStateConverter
	{
		public ControllerState ConvertFrom(Pose pose)
		{
			switch (pose)
			{
				case Pose.DoubleTap:
					return ControllerState.Using;
				case Pose.FingersSpread:
					return ControllerState.Inspecting;
				case Pose.Fist:
					return ControllerState.Forcing;
				case Pose.WaveIn:
					return ControllerState.Right; // Hard configuration for left arm
				case Pose.WaveOut:
					return ControllerState.Left; // Hard configuration for left arm
				case Pose.Rest:
				case Pose.Unknown:
				default:
					return ControllerState.NoState;
			}
		}
	}
}
