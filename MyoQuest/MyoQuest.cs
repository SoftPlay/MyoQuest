using System;

namespace MyoQuest
{
	public sealed class MyoQuest
	{
		static void Main(string[] args)
		{
			var myoDal = new MyoController.MyoDal(new MyoController.MyoObjectFactory());
			myoDal.Initialise();
			while (Console.ReadKey().Key != ConsoleKey.Escape)
			{
			}
			myoDal.Shutdown();
		}
	}
}
