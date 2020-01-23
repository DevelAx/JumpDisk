public abstract class SelfSignaledBaseEvent<TEvent> : BaseEvent
	where TEvent : BaseEvent
{
	protected virtual void Signal()
	{
		base.Signal<TEvent>();
	}
}

public abstract class BaseEvent
{
	public bool RequireReceiver { get; private set; }

	public BaseEvent(bool requireReceiver = false)
	{
		RequireReceiver = requireReceiver;
	}

	protected virtual void Signal<EventT>() where EventT : BaseEvent
	{
		Events.SendEvent((EventT)this);
	}
}
