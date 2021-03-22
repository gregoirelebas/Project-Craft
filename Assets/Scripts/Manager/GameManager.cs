using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; } = null;

	private Player player = null;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		LockCursor(null);

		EventManager.Instance.StartListening(EventType.OnMenuOpened, UnlockCursor);
		EventManager.Instance.StartListening(EventType.OnMenuClosed, LockCursor);
	}

	private void LockCursor(EventParameters parameters)
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void UnlockCursor(EventParameters parameters)
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
	}

	public Player GetPlayer()
	{
		return player;
	}
}
