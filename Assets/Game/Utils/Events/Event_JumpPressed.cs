using UnityEngine;

internal class Event_JumpPressed : SelfSignaledBaseEvent<Event_JumpPressed>
{
	public Vector3 WorldPosition { get; }
	public Vector3 ScreenPosition { get; }

	public Event_JumpPressed(Vector3 screenPosition, Vector3 worldPosition)
	{
		WorldPosition = worldPosition;
		ScreenPosition = screenPosition;
		Signal();
	}
}