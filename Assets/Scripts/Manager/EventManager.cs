using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventParameters
{
	public Item @item = null;
	public string @string = "";
}

public class EventManager : MonoBehaviour
{
	public enum EventType
	{
		OnItemPickUp,
		OnCursorEnter,
		OnCursorExit
	}

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
