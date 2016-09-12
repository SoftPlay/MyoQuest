using System;
using MyoSharp.Device;

namespace MyoQuest.MyoController
{
	public interface IMyoDal : IDisposable
	{
		event EventHandler<NewPoseEventArgs> PoseChanged;
		void Initialise();
		void Shutdown();
	}
}
