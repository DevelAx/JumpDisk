public class Event_JumpCompleted : SelfSignaledBaseEvent<Event_JumpCompleted>
{
	public Event_JumpCompleted()
	{
		Signal();
	}
}