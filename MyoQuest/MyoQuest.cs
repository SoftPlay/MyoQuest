using System;
using MyoQuest.Common;
using MyoQuest.UnityModule;

namespace MyoQuest
{
	public sealed class MyoQuest
	{
		static void Main(string[] args)
		{
			IGameRunner gameRunner = CreateUnityRunner();

			//var myoDal = new MyoController.MyoDal(new MyoController.MyoObjectFactory());
			//myoDal.Initialise();
			gameRunner.Initialise();
			gameRunner.RunGame();
			while (Console.ReadKey().Key != ConsoleKey.Escape)
			{
			}
			//myoDal.Shutdown();
			gameRunner.Shutdown();
		}

		private static IGameRunner CreateUnityRunner()
		{
			return new UnityBootstrapper();
		}
	}
}
