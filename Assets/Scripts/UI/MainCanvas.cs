using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI cursorText = null;

	private void Awake()
	{
		cursorText.text = "";
	}

	private void OnEnable()
	{
		EventManager.Instance.StartListening(EventType.OnCursorEnter, SetTextOnCursor);
		EventManager.Instance.StartListening(EventType.OnCursorExit, SetTextOnCursor);
	}

	private void OnDisable()
	{
		EventManager.Instance.StopListening(EventType.OnCursorEnter, SetTextOnCursor);
		EventManager.Instance.StopListening(EventType.OnCursorExit, SetTextOnCursor);
	}

	private void SetTextOnCursor(EventParameters parameters)
	{
		cursorText.text = parameters.@string;
	}
}
