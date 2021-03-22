using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
	OnItemPickUp,
	OnCursorEnter,
	OnCursorExit,
	OnMenuOpened,
	OnMenuClosed,
	OnPlayerJump,
	OnPlayerInteract,
}

public class EventParameters
{
	public Item @item = null;
	public string @string = "";
}

public class EventManager : MonoBehaviour
{
	private static bool isQuitting = false;

	private static EventManager instance = null;
	public static EventManager Instance
	{
		get
		{
			if (instance == null && !isQuitting)
			{
				GameObject go = new GameObject("@EventManager");
				instance = go.AddComponent<EventManager>();
			}

			return instance;
		}
	}

	private Dictionary<EventType, Action<EventParameters>> eventHandler = null;

	private void Awake()
	{
		eventHandler = new Dictionary<EventType, Action<EventParameters>>();
	}

	private void OnApplicationQuit()
	{
		isQuitting = true;
		Destroy(gameObject);		
	}

	/// <summary>
	/// Subscribe to the event and invoke [listener] when triggered.
	/// </summary>
	public void StartListening(EventType type, Action<EventParameters> listener)
	{
		Action<EventParameters> thisEvent;
		if (eventHandler.TryGetValue(type, out thisEvent))
		{
			thisEvent += listener;
			eventHandler[type] = thisEvent;
		}
		else
		{
			thisEvent += listener;
			eventHandler.Add(type, thisEvent);
		}
	}

	/// <summary>
	/// Unsubscribe from the event.
	/// </summary>
	public void StopListening(EventType type, Action<EventParameters> listener)
	{
		Action<EventParameters> thisEvent;
		if (eventHandler.TryGetValue(type, out thisEvent))
		{
			thisEvent -= listener;
			eventHandler[type] = thisEvent;
		}
		else
		{
			Debug.LogWarning("The event handler do not contain an event of type : " + type.ToString());
		}
	}

	/// <summary>
	/// Clear all action attached to this event.
	/// </summary>
	public void RemoveEvent(EventType type)
	{
		if (eventHandler.ContainsKey(type))
		{
			eventHandler.Remove(type);
		}
		else
		{
			Debug.LogWarning("The event handler do not contain an event of type : " + type.ToString());
		}
	}

	/// <summary>
	/// Trigger an event and invoke all attached actions.
	/// </summary>
	public void TriggerEvent(EventType type)
	{
		Action<EventParameters> thisEvent;
		if (eventHandler.TryGetValue(type, out thisEvent))
		{
			thisEvent?.Invoke(null);
		}
		else
		{
			Debug.LogWarning("The event handler do not contain an event of type : " + type.ToString());
		}
	}

	/// <summary>
	/// Trigger an event an invoke all attached actions with parameters.
	/// </summary>
	public void TriggerEvent(EventType type, EventParameters parameters)
	{
		Action<EventParameters> thisEvent;
		if (eventHandler.TryGetValue(type, out thisEvent))
		{
			thisEvent?.Invoke(parameters);
		}
		else
		{
			Debug.LogWarning("The event handler do not contain an event of type : " + type.ToString());
		}
	}
}
