namespace MyoQuest.Stories
{
	public interface IStory
	{
		int ID { get; }
		string StoryText { get; }

		string GetActionResult(StoryAction action);
	}
}
