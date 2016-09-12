using System;
using MyoSharp.Device;
using MyoSharp.Poses;

namespace MyoQuest.MyoController
{
	/// <summary>
	/// Skinnier pose event arge to avoid exposing the <see cref="IMyo"/> reference.
	/// </summary>
	public sealed class NewPoseEventArgs : EventArgs
	{
		public NewPoseEventArgs(Pose newPose)
		{
			this.Pose = newPose;
		}

		public Pose Pose { get; private set; }
	}
}
