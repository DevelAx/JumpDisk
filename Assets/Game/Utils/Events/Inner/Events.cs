using System;
using System.Collections.Generic;
using UnityEngine;

internal delegate void EventDelegate<TEvent>(TEvent @event) where TEvent : BaseEvent;

internal static class Events
{
	#region Inner Types

	private class Subscription<TEvent>
		where TEvent : BaseEvent
	{
		private readonly Dictionary<EventDelegate<TEvent>, Component> _subscribers = new Dictionary<EventDelegate<TEvent>, Component>();

		public int SubscribersCount
		{
			get => _subscribers.Count;
		}
		
		public void Subsribe(Component component, EventDelegate<TEvent> callback)
		{
			if (_subscribers.ContainsKey(callback))
				new GameException($"The '{callback.Method.DeclaringType.Name}.{callback.Method.Name}' callback has alredy been added to the subscription of the '{typeof(TEvent).Name}' event!");
			//throw new GameException($"The '{component.GetType().Name}.{callback.Method.Name}' of the '{component.name}' object (InstanceId={component.GetInstanceID()}) already has a subscription to the '{typeof(TEvent).Name}' event!");

			_subscribers.Add(callback, component);
		}

		public void Subsribe(EventDelegate<TEvent> callback)
		{
			if (_subscribers.ContainsKey(callback))
				throw new GameException($"The '{callback.Method.DeclaringType.Name}.{callback.Method.Name}' callback has alredy been added to the subscription of the '{typeof(TEvent).Name}' event!");

			_subscribers.Add(callback, null);
		}

		//public bool Unsubscribe(Component component)
		//{
		//	return _componentSubscribers.Remove(component);
		//}

		public bool Unsubscribe(EventDelegate<TEvent> callback)
		{
			return _subscribers.Remove(callback);
		}

		public int Send(TEvent @event, Component component)
		{
			List<EventDelegate<TEvent>> toDelete = new List<EventDelegate<TEvent>>();

			foreach (var subscriber in _subscribers)
			{
				if (subscriber.Value)
					subscriber.Key(@event);
				else
					toDelete.Add(subscriber.Key);
			}

			foreach (var del in toDelete)
			{
				_subscribers.Remove(del);
			}

			return _subscribers.Count;
		}

		public int Send(TEvent @event)
		{
			foreach (var subscriber in _subscribers)
				subscriber.Key(@event);

			return _subscribers.Count;
		}
	}

	#endregion Inner Types

	private static Dictionary<Type, object> _subscribers = new Dictionary<Type, object>();

	public static void Reset()
	{
		_subscribers.Clear();
	}

	public static void SubscribeTo<TEvent>(this Component component, EventDelegate<TEvent> callback)
		where TEvent : BaseEvent
	{
		var subscription = FindOrAddSubscription<TEvent>();
		subscription.Subsribe(component, callback);
	}

	public static void SubscribeTo<TEvent>(this EventDelegate<TEvent> callback)
		where TEvent : BaseEvent
	{
		var subscription = FindOrAddSubscription<TEvent>();
		subscription.Subsribe(callback);
	}

	public static bool UnsubscribeFrom<TEvent>(this Component component, EventDelegate<TEvent> callback)
		where TEvent : BaseEvent
	{
		return UnsubscribeFrom(callback);
	}

	public static bool UnsubscribeFrom<TEvent>(this EventDelegate<TEvent> callback)
		where TEvent : BaseEvent
	{

		var subscription = FindSubscription<TEvent>();

		if (subscription == null)
		{
			// No subscription found to unsubscribe from.
			return false;
		}

		return subscription.Unsubscribe(callback);
	}

	public static int SendEvent<TEvent>(this Component component, TEvent @event)
		where TEvent : BaseEvent
	{
		return Send(@event);
	}

	public static int SendEvent<TEvent>(this TEvent @event)
		where TEvent : BaseEvent
	{
		return Send(@event);
	}

	private static int Send<TEvent>(this TEvent @event)
		where TEvent : BaseEvent
	{
		Type eventType = typeof(TEvent);
		var subscription = FindSubscription<TEvent>();

		if (subscription == null || subscription.SubscribersCount == 0)
		{
			if (@event.RequireReceiver)
				Debug.LogError($"No subscription found to send the '{eventType.Name}' event!");

			if (subscription != null)
				_subscribers.Remove(eventType);

			return 0;
		}

		return subscription.Send(@event);
	}

	#region Helpers

	private static Subscription<TEvent> FindOrAddSubscription<TEvent>()
		where TEvent : BaseEvent
	{
		Type type = typeof(TEvent);
		Subscription<TEvent> subscription;

		if (_subscribers.TryGetValue(type, out object sub))
		{
			subscription = (Subscription<TEvent>)sub;
		}
		else
		{
			subscription = new Subscription<TEvent>();
			_subscribers.Add(type, subscription);
		}

		return subscription;
	}

	private static Subscription<TEvent> FindSubscription<TEvent>()
		where TEvent : BaseEvent
	{
		Type type = typeof(TEvent);

		if (_subscribers.TryGetValue(type, out object sub))
		{
			return (Subscription<TEvent>)sub;
		}

		return null;
	}

	#endregion
}
