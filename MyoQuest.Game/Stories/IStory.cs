namespace MyoQuest.Game.Stories
{
	public interface IStory
	{
		int ID { get; }
		string StoryText { get; }

		IActionResult GetActionResult(StoryAction action);
	}
}
