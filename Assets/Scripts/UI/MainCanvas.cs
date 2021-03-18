using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCanvas : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI cursorText = null;

	private void Awake()
	{
		cursorText.text = "";
	}

	private void OnEnable()
	{
		EventManager.Instance.StartListening(EventManager.EventType.OnCursorEnter, SetTextOnCursor);
		EventManager.Instance.StartListening(EventManager.EventType.OnCursorExit, SetTextOnCursor);
	}

	private void OnDisable()
	{
		EventManager.Instance.StopListening(EventManager.EventType.OnCursorEnter, SetTextOnCursor);
		EventManager.Instance.StopListening(EventManager.EventType.OnCursorExit, SetTextOnCursor);
	}

	private void SetTextOnCursor(EventParameters parameters)
	{
		cursorText.text = parameters.@string;
	}
}
