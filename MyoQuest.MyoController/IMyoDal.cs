using System;

namespace MyoQuest.MyoController
{
	public interface IMyoDal : IDisposable
	{
		void Initialise();
		void Shutdown();
	}
}
