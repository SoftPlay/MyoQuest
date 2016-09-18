using MyoQuest.Common.Enums;

namespace MyoQuest.Game.Stories
{
	/// <summary>
	/// Essentially represents the <see cref="ControllerState"/> but is contextual to the game world.
	/// </summary>
	public enum StoryAction
	{
		NoAction = 0,
		Left = 1,
		Right = 2,
		Force = 3,
		Use = 4,
		Inspect = 5
	}
}
