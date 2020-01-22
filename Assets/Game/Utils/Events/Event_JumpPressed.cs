using UnityEngine;

internal class Event_JumpPressed : SelfSignaledBaseEvent<Event_JumpPressed>
{
	public Vector3 Position { get; }

	public Event_JumpPressed(Vector3 position)
	{
		Position = position;
		Signal();
	}
}